using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	/// <summary>Enables compilers to dynamically attach object fields to managed objects.</summary>
	/// <typeparam name="TKey">The reference type to which the field is attached. </typeparam>
	/// <typeparam name="TValue">The field's type. This must be a reference type.</typeparam>
	// Token: 0x020005BD RID: 1469
	public sealed class ConditionalWeakTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : class where TValue : class
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" /> class.</summary>
		// Token: 0x06002B84 RID: 11140 RVA: 0x000ABF07 File Offset: 0x000AA107
		public ConditionalWeakTable()
		{
			this.data = new Ephemeron[13];
			GC.register_ephemeron_array(this.data);
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000ABF34 File Offset: 0x000AA134
		~ConditionalWeakTable()
		{
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000ABF5C File Offset: 0x000AA15C
		private void RehashWithoutResize()
		{
			int num = this.data.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.data[i].key == GC.EPHEMERON_TOMBSTONE)
				{
					this.data[i].key = null;
				}
			}
			for (int j = 0; j < num; j++)
			{
				object key = this.data[j].key;
				if (key != null)
				{
					int num2 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num;
					while (this.data[num2].key != null)
					{
						if (this.data[num2].key == key)
						{
							goto IL_0108;
						}
						if (++num2 == num)
						{
							num2 = 0;
						}
					}
					this.data[num2].key = key;
					this.data[num2].value = this.data[j].value;
					this.data[j].key = null;
					this.data[j].value = null;
				}
				IL_0108:;
			}
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000AC07C File Offset: 0x000AA27C
		private void RecomputeSize()
		{
			this.size = 0;
			for (int i = 0; i < this.data.Length; i++)
			{
				if (this.data[i].key != null)
				{
					this.size++;
				}
			}
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000AC0C4 File Offset: 0x000AA2C4
		private void Rehash()
		{
			this.RecomputeSize();
			uint prime = (uint)HashHelpers.GetPrime(((int)((float)this.size / 0.7f) << 1) | 1);
			if (prime > (float)this.data.Length * 0.5f && prime < (float)this.data.Length * 1.1f)
			{
				this.RehashWithoutResize();
				return;
			}
			Ephemeron[] array = new Ephemeron[prime];
			GC.register_ephemeron_array(array);
			this.size = 0;
			for (int i = 0; i < this.data.Length; i++)
			{
				object key = this.data[i].key;
				object value = this.data[i].value;
				if (key != null && key != GC.EPHEMERON_TOMBSTONE)
				{
					int num = array.Length;
					int num2 = -1;
					int num4;
					int num3 = (num4 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num);
					do
					{
						object key2 = array[num4].key;
						if (key2 == null || key2 == GC.EPHEMERON_TOMBSTONE)
						{
							goto IL_00D3;
						}
						if (++num4 == num)
						{
							num4 = 0;
						}
					}
					while (num4 != num3);
					IL_00ED:
					array[num2].key = key;
					array[num2].value = value;
					this.size++;
					goto IL_0118;
					IL_00D3:
					num2 = num4;
					goto IL_00ED;
				}
				IL_0118:;
			}
			this.data = array;
		}

		/// <summary>Adds a key to the table.</summary>
		/// <param name="key">The key to add. <paramref name="key" /> represents the object to which the property is attached.</param>
		/// <param name="value">The key's property value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> already exists.</exception>
		// Token: 0x06002B89 RID: 11145 RVA: 0x000AC204 File Offset: 0x000AA404
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Null key", "key");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				if ((float)this.size >= (float)this.data.Length * 0.7f)
				{
					this.Rehash();
				}
				int num = this.data.Length;
				int num2 = -1;
				int num4;
				int num3 = (num4 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num);
				for (;;)
				{
					object key2 = this.data[num4].key;
					if (key2 == null)
					{
						break;
					}
					if (key2 == GC.EPHEMERON_TOMBSTONE && num2 == -1)
					{
						num2 = num4;
					}
					else if (key2 == key)
					{
						goto Block_9;
					}
					if (++num4 == num)
					{
						num4 = 0;
					}
					if (num4 == num3)
					{
						goto IL_00C7;
					}
				}
				if (num2 == -1)
				{
					num2 = num4;
					goto IL_00C7;
				}
				goto IL_00C7;
				Block_9:
				throw new ArgumentException("Key already in the list", "key");
				IL_00C7:
				this.data[num2].key = key;
				this.data[num2].value = value;
				this.size++;
			}
		}

		/// <summary>Removes a key and its value from the table.</summary>
		/// <returns>true if the key is found and removed; otherwise, false.</returns>
		/// <param name="key">The key to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06002B8A RID: 11146 RVA: 0x000AC334 File Offset: 0x000AA534
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Null key", "key");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				int num = this.data.Length;
				int num3;
				int num2 = (num3 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num);
				for (;;)
				{
					object key2 = this.data[num3].key;
					if (key2 == key)
					{
						break;
					}
					if (key2 == null)
					{
						goto Block_5;
					}
					if (++num3 == num)
					{
						num3 = 0;
					}
					if (num3 == num2)
					{
						goto Block_7;
					}
				}
				this.data[num3].key = GC.EPHEMERON_TOMBSTONE;
				this.data[num3].value = null;
				return true;
				Block_5:
				Block_7:;
			}
			return false;
		}

		/// <summary>Gets the value of the specified key.</summary>
		/// <returns>true if <paramref name="key" /> is found; otherwise, false.</returns>
		/// <param name="key">The key that represents an object with an attached property.</param>
		/// <param name="value">When this method returns, contains the attached property value. If <paramref name="key" /> is not found, <paramref name="value" /> contains the default value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06002B8B RID: 11147 RVA: 0x000AC40C File Offset: 0x000AA60C
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Null key", "key");
			}
			value = default(TValue);
			object @lock = this._lock;
			lock (@lock)
			{
				int num = this.data.Length;
				int num3;
				int num2 = (num3 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num);
				for (;;)
				{
					object key2 = this.data[num3].key;
					if (key2 == key)
					{
						break;
					}
					if (key2 == null)
					{
						goto Block_5;
					}
					if (++num3 == num)
					{
						num3 = 0;
					}
					if (num3 == num2)
					{
						goto Block_7;
					}
				}
				value = (TValue)((object)this.data[num3].value);
				return true;
				Block_5:
				Block_7:;
			}
			return false;
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000AC4DC File Offset: 0x000AA6DC
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			object @lock = this._lock;
			IEnumerator<KeyValuePair<TKey, TValue>> enumerator;
			lock (@lock)
			{
				IEnumerator<KeyValuePair<TKey, TValue>> enumerator2;
				if (this.size != 0)
				{
					enumerator = new ConditionalWeakTable<TKey, TValue>.Enumerator(this);
					enumerator2 = enumerator;
				}
				else
				{
					enumerator2 = ((IEnumerable<KeyValuePair<TKey, TValue>>)Array.Empty<KeyValuePair<TKey, TValue>>()).GetEnumerator();
				}
				enumerator = enumerator2;
			}
			return enumerator;
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000AC534 File Offset: 0x000AA734
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<TKey, TValue>>)this).GetEnumerator();
		}

		// Token: 0x0400161A RID: 5658
		private Ephemeron[] data;

		// Token: 0x0400161B RID: 5659
		private object _lock = new object();

		// Token: 0x0400161C RID: 5660
		private int size;

		// Token: 0x020005BE RID: 1470
		private sealed class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x06002B8E RID: 11150 RVA: 0x000AC53C File Offset: 0x000AA73C
			public Enumerator(ConditionalWeakTable<TKey, TValue> table)
			{
				this._table = table;
				this._currentIndex = -1;
			}

			// Token: 0x06002B8F RID: 11151 RVA: 0x000AC55C File Offset: 0x000AA75C
			~Enumerator()
			{
				this.Dispose();
			}

			// Token: 0x06002B90 RID: 11152 RVA: 0x000AC588 File Offset: 0x000AA788
			public void Dispose()
			{
				if (Interlocked.Exchange<ConditionalWeakTable<TKey, TValue>>(ref this._table, null) != null)
				{
					this._current = default(KeyValuePair<TKey, TValue>);
					GC.SuppressFinalize(this);
				}
			}

			// Token: 0x06002B91 RID: 11153 RVA: 0x000AC5AC File Offset: 0x000AA7AC
			public bool MoveNext()
			{
				ConditionalWeakTable<TKey, TValue> table = this._table;
				if (table != null)
				{
					object @lock = table._lock;
					lock (@lock)
					{
						object ephemeron_TOMBSTONE = GC.EPHEMERON_TOMBSTONE;
						while (this._currentIndex < table.data.Length - 1)
						{
							this._currentIndex++;
							Ephemeron ephemeron = table.data[this._currentIndex];
							if (ephemeron.key != null && ephemeron.key != ephemeron_TOMBSTONE)
							{
								this._current = new KeyValuePair<TKey, TValue>((TKey)((object)ephemeron.key), (TValue)((object)ephemeron.value));
								return true;
							}
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000AC670 File Offset: 0x000AA870
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (this._currentIndex < 0)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current;
				}
			}

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000AC686 File Offset: 0x000AA886
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002B94 RID: 11156 RVA: 0x00002C89 File Offset: 0x00000E89
			public void Reset()
			{
			}

			// Token: 0x0400161D RID: 5661
			private ConditionalWeakTable<TKey, TValue> _table;

			// Token: 0x0400161E RID: 5662
			private int _currentIndex = -1;

			// Token: 0x0400161F RID: 5663
			private KeyValuePair<TKey, TValue> _current;
		}
	}
}
