using System;
using System.Threading;

namespace System.Xml.Linq
{
	// Token: 0x02000011 RID: 17
	internal sealed class XHashtable<TValue>
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000406B File Offset: 0x0000226B
		public XHashtable(XHashtable<TValue>.ExtractKeyDelegate extractKey, int capacity)
		{
			this._state = new XHashtable<TValue>.XHashtableState(extractKey, capacity);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004080 File Offset: 0x00002280
		public bool TryGetValue(string key, int index, int count, out TValue value)
		{
			return this._state.TryGetValue(key, index, count, out value);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004094 File Offset: 0x00002294
		public TValue Add(TValue value)
		{
			TValue tvalue;
			while (!this._state.TryAdd(value, out tvalue))
			{
				lock (this)
				{
					XHashtable<TValue>.XHashtableState xhashtableState = this._state.Resize();
					Thread.MemoryBarrier();
					this._state = xhashtableState;
				}
			}
			return tvalue;
		}

		// Token: 0x04000024 RID: 36
		private XHashtable<TValue>.XHashtableState _state;

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x06000088 RID: 136
		public delegate string ExtractKeyDelegate(TValue value);

		// Token: 0x02000013 RID: 19
		private sealed class XHashtableState
		{
			// Token: 0x06000089 RID: 137 RVA: 0x00004100 File Offset: 0x00002300
			public XHashtableState(XHashtable<TValue>.ExtractKeyDelegate extractKey, int capacity)
			{
				this._buckets = new int[capacity];
				this._entries = new XHashtable<TValue>.XHashtableState.Entry[capacity];
				this._extractKey = extractKey;
			}

			// Token: 0x0600008A RID: 138 RVA: 0x00004128 File Offset: 0x00002328
			public XHashtable<TValue>.XHashtableState Resize()
			{
				if (this._numEntries < this._buckets.Length)
				{
					return this;
				}
				int num = 0;
				for (int i = 0; i < this._buckets.Length; i++)
				{
					int j = this._buckets[i];
					if (j == 0)
					{
						j = Interlocked.CompareExchange(ref this._buckets[i], -1, 0);
					}
					while (j > 0)
					{
						if (this._extractKey(this._entries[j].Value) != null)
						{
							num++;
						}
						if (this._entries[j].Next == 0)
						{
							j = Interlocked.CompareExchange(ref this._entries[j].Next, -1, 0);
						}
						else
						{
							j = this._entries[j].Next;
						}
					}
				}
				if (num < this._buckets.Length / 2)
				{
					num = this._buckets.Length;
				}
				else
				{
					num = this._buckets.Length * 2;
					if (num < 0)
					{
						throw new OverflowException();
					}
				}
				XHashtable<TValue>.XHashtableState xhashtableState = new XHashtable<TValue>.XHashtableState(this._extractKey, num);
				for (int k = 0; k < this._buckets.Length; k++)
				{
					for (int l = this._buckets[k]; l > 0; l = this._entries[l].Next)
					{
						TValue tvalue;
						xhashtableState.TryAdd(this._entries[l].Value, out tvalue);
					}
				}
				return xhashtableState;
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00004280 File Offset: 0x00002480
			public bool TryGetValue(string key, int index, int count, out TValue value)
			{
				int num = XHashtable<TValue>.XHashtableState.ComputeHashCode(key, index, count);
				int num2 = 0;
				if (this.FindEntry(num, key, index, count, ref num2))
				{
					value = this._entries[num2].Value;
					return true;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x0600008C RID: 140 RVA: 0x000042CC File Offset: 0x000024CC
			public bool TryAdd(TValue value, out TValue newValue)
			{
				newValue = value;
				string text = this._extractKey(value);
				if (text == null)
				{
					return true;
				}
				int num = XHashtable<TValue>.XHashtableState.ComputeHashCode(text, 0, text.Length);
				int num2 = Interlocked.Increment(ref this._numEntries);
				if (num2 < 0 || num2 >= this._buckets.Length)
				{
					return false;
				}
				this._entries[num2].Value = value;
				this._entries[num2].HashCode = num;
				Thread.MemoryBarrier();
				int num3 = 0;
				while (!this.FindEntry(num, text, 0, text.Length, ref num3))
				{
					if (num3 == 0)
					{
						num3 = Interlocked.CompareExchange(ref this._buckets[num & (this._buckets.Length - 1)], num2, 0);
					}
					else
					{
						num3 = Interlocked.CompareExchange(ref this._entries[num3].Next, num2, 0);
					}
					if (num3 <= 0)
					{
						return num3 == 0;
					}
				}
				newValue = this._entries[num3].Value;
				return true;
			}

			// Token: 0x0600008D RID: 141 RVA: 0x000043BC File Offset: 0x000025BC
			private bool FindEntry(int hashCode, string key, int index, int count, ref int entryIndex)
			{
				int num = entryIndex;
				int i;
				if (num == 0)
				{
					i = this._buckets[hashCode & (this._buckets.Length - 1)];
				}
				else
				{
					i = num;
				}
				while (i > 0)
				{
					if (this._entries[i].HashCode == hashCode)
					{
						string text = this._extractKey(this._entries[i].Value);
						if (text == null)
						{
							if (this._entries[i].Next > 0)
							{
								this._entries[i].Value = default(TValue);
								i = this._entries[i].Next;
								if (num == 0)
								{
									this._buckets[hashCode & (this._buckets.Length - 1)] = i;
									continue;
								}
								this._entries[num].Next = i;
								continue;
							}
						}
						else if (count == text.Length && string.CompareOrdinal(key, index, text, 0, count) == 0)
						{
							entryIndex = i;
							return true;
						}
					}
					num = i;
					i = this._entries[i].Next;
				}
				entryIndex = num;
				return false;
			}

			// Token: 0x0600008E RID: 142 RVA: 0x000044D0 File Offset: 0x000026D0
			private static int ComputeHashCode(string key, int index, int count)
			{
				int num = 352654597;
				int num2 = index + count;
				for (int i = index; i < num2; i++)
				{
					num += (num << 7) ^ (int)key[i];
				}
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
				return num & int.MaxValue;
			}

			// Token: 0x04000025 RID: 37
			private int[] _buckets;

			// Token: 0x04000026 RID: 38
			private XHashtable<TValue>.XHashtableState.Entry[] _entries;

			// Token: 0x04000027 RID: 39
			private int _numEntries;

			// Token: 0x04000028 RID: 40
			private XHashtable<TValue>.ExtractKeyDelegate _extractKey;

			// Token: 0x02000014 RID: 20
			private struct Entry
			{
				// Token: 0x04000029 RID: 41
				public TValue Value;

				// Token: 0x0400002A RID: 42
				public int HashCode;

				// Token: 0x0400002B RID: 43
				public int Next;
			}
		}
	}
}
