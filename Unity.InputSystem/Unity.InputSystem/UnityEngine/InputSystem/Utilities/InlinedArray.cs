using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200023D RID: 573
	internal struct InlinedArray<TValue> : IEnumerable<TValue>, IEnumerable
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0005EFE5 File Offset: 0x0005D1E5
		public int Capacity
		{
			get
			{
				TValue[] array = this.additionalValues;
				if (array == null)
				{
					return 1;
				}
				return array.Length + 1;
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0005EFF7 File Offset: 0x0005D1F7
		public InlinedArray(TValue value)
		{
			this.length = 1;
			this.firstValue = value;
			this.additionalValues = null;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0005F00E File Offset: 0x0005D20E
		public InlinedArray(TValue firstValue, params TValue[] additionalValues)
		{
			this.length = 1 + additionalValues.Length;
			this.firstValue = firstValue;
			this.additionalValues = additionalValues;
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0005F02C File Offset: 0x0005D22C
		public InlinedArray(IEnumerable<TValue> values)
		{
			this = default(InlinedArray<TValue>);
			this.length = values.Count<TValue>();
			if (this.length > 1)
			{
				this.additionalValues = new TValue[this.length - 1];
			}
			else
			{
				this.additionalValues = null;
			}
			int index = 0;
			foreach (TValue value in values)
			{
				if (index == 0)
				{
					this.firstValue = value;
				}
				else
				{
					this.additionalValues[index - 1] = value;
				}
				index++;
			}
		}

		// Token: 0x170005E2 RID: 1506
		public TValue this[int index]
		{
			get
			{
				if (index < 0 || index >= this.length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index == 0)
				{
					return this.firstValue;
				}
				return this.additionalValues[index - 1];
			}
			set
			{
				if (index < 0 || index >= this.length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index == 0)
				{
					this.firstValue = value;
					return;
				}
				this.additionalValues[index - 1] = value;
			}
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0005F12E File Offset: 0x0005D32E
		public void Clear()
		{
			this.length = 0;
			this.firstValue = default(TValue);
			this.additionalValues = null;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0005F14C File Offset: 0x0005D34C
		public void ClearWithCapacity()
		{
			this.firstValue = default(TValue);
			for (int i = 0; i < this.length - 1; i++)
			{
				this.additionalValues[i] = default(TValue);
			}
			this.length = 0;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0005F194 File Offset: 0x0005D394
		public InlinedArray<TValue> Clone()
		{
			return new InlinedArray<TValue>
			{
				length = this.length,
				firstValue = this.firstValue,
				additionalValues = ((this.additionalValues != null) ? ArrayHelpers.Copy<TValue>(this.additionalValues) : null)
			};
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0005F1E4 File Offset: 0x0005D3E4
		public void SetLength(int size)
		{
			if (size < this.length)
			{
				for (int i = size; i < this.length; i++)
				{
					this[i] = default(TValue);
				}
			}
			this.length = size;
			if (size > 1 && (this.additionalValues == null || this.additionalValues.Length < size - 1))
			{
				Array.Resize<TValue>(ref this.additionalValues, size - 1);
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0005F249 File Offset: 0x0005D449
		public TValue[] ToArray()
		{
			return ArrayHelpers.Join<TValue>(this.firstValue, this.additionalValues);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0005F25C File Offset: 0x0005D45C
		public TOther[] ToArray<TOther>(Func<TValue, TOther> mapFunction)
		{
			if (this.length == 0)
			{
				return null;
			}
			TOther[] result = new TOther[this.length];
			for (int i = 0; i < this.length; i++)
			{
				result[i] = mapFunction(this[i]);
			}
			return result;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0005F2A8 File Offset: 0x0005D4A8
		public int IndexOf(TValue value)
		{
			EqualityComparer<TValue> comparer = EqualityComparer<TValue>.Default;
			if (this.length > 0)
			{
				if (comparer.Equals(this.firstValue, value))
				{
					return 0;
				}
				if (this.additionalValues != null)
				{
					for (int i = 0; i < this.length - 1; i++)
					{
						if (comparer.Equals(this.additionalValues[i], value))
						{
							return i + 1;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0005F30C File Offset: 0x0005D50C
		public int Append(TValue value)
		{
			if (this.length == 0)
			{
				this.firstValue = value;
			}
			else if (this.additionalValues == null)
			{
				this.additionalValues = new TValue[1];
				this.additionalValues[0] = value;
			}
			else
			{
				Array.Resize<TValue>(ref this.additionalValues, this.length);
				this.additionalValues[this.length - 1] = value;
			}
			int num = this.length;
			this.length++;
			return num;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0005F388 File Offset: 0x0005D588
		public int AppendWithCapacity(TValue value, int capacityIncrement = 10)
		{
			if (this.length == 0)
			{
				this.firstValue = value;
			}
			else
			{
				int numAdditionalValues = this.length - 1;
				ArrayHelpers.AppendWithCapacity<TValue>(ref this.additionalValues, ref numAdditionalValues, value, capacityIncrement);
			}
			int num = this.length;
			this.length++;
			return num;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0005F3D4 File Offset: 0x0005D5D4
		public void AssignWithCapacity(InlinedArray<TValue> values)
		{
			if (this.Capacity < values.length && values.length > 1)
			{
				this.additionalValues = new TValue[values.length - 1];
			}
			this.length = values.length;
			if (this.length > 0)
			{
				this.firstValue = values.firstValue;
			}
			if (this.length > 1)
			{
				Array.Copy(values.additionalValues, this.additionalValues, this.length - 1);
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0005F450 File Offset: 0x0005D650
		public void Append(IEnumerable<TValue> values)
		{
			foreach (TValue value in values)
			{
				this.Append(value);
			}
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0005F49C File Offset: 0x0005D69C
		public void Remove(TValue value)
		{
			if (this.length < 1)
			{
				return;
			}
			if (EqualityComparer<TValue>.Default.Equals(this.firstValue, value))
			{
				this.RemoveAt(0);
				return;
			}
			if (this.additionalValues != null)
			{
				for (int i = 0; i < this.length - 1; i++)
				{
					if (EqualityComparer<TValue>.Default.Equals(this.additionalValues[i], value))
					{
						this.RemoveAt(i + 1);
						return;
					}
				}
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0005F50C File Offset: 0x0005D70C
		public void RemoveAtWithCapacity(int index)
		{
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index == 0)
			{
				if (this.length == 1)
				{
					this.firstValue = default(TValue);
				}
				else if (this.length == 2)
				{
					this.firstValue = this.additionalValues[0];
					this.additionalValues[0] = default(TValue);
				}
				else
				{
					this.firstValue = this.additionalValues[0];
					int numAdditional = this.length - 1;
					this.additionalValues.EraseAtWithCapacity(ref numAdditional, 0);
				}
			}
			else
			{
				int numAdditional2 = this.length - 1;
				this.additionalValues.EraseAtWithCapacity(ref numAdditional2, index - 1);
			}
			this.length--;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0005F5D0 File Offset: 0x0005D7D0
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index == 0)
			{
				if (this.additionalValues != null)
				{
					this.firstValue = this.additionalValues[0];
					if (this.additionalValues.Length == 1)
					{
						this.additionalValues = null;
					}
					else
					{
						Array.Copy(this.additionalValues, 1, this.additionalValues, 0, this.additionalValues.Length - 1);
						Array.Resize<TValue>(ref this.additionalValues, this.additionalValues.Length - 1);
					}
				}
				else
				{
					this.firstValue = default(TValue);
				}
			}
			else
			{
				int numAdditionalValues = this.length - 1;
				if (numAdditionalValues == 1)
				{
					this.additionalValues = null;
				}
				else if (index == this.length - 1)
				{
					Array.Resize<TValue>(ref this.additionalValues, numAdditionalValues - 1);
				}
				else
				{
					TValue[] newAdditionalValues = new TValue[numAdditionalValues - 1];
					if (index >= 2)
					{
						Array.Copy(this.additionalValues, 0, newAdditionalValues, 0, index - 1);
					}
					Array.Copy(this.additionalValues, index + 1 - 1, newAdditionalValues, index - 1, this.length - index - 1);
					this.additionalValues = newAdditionalValues;
				}
			}
			this.length--;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0005F6F0 File Offset: 0x0005D8F0
		public void RemoveAtByMovingTailWithCapacity(int index)
		{
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int numAdditionalValues = this.length - 1;
			if (index == 0)
			{
				if (this.length > 1)
				{
					this.firstValue = this.additionalValues[numAdditionalValues - 1];
					this.additionalValues[numAdditionalValues - 1] = default(TValue);
				}
				else
				{
					this.firstValue = default(TValue);
				}
			}
			else
			{
				ArrayHelpers.EraseAtByMovingTail<TValue>(this.additionalValues, ref numAdditionalValues, index - 1);
			}
			this.length--;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0005F784 File Offset: 0x0005D984
		public bool RemoveByMovingTailWithCapacity(TValue value)
		{
			int index = this.IndexOf(value);
			if (index == -1)
			{
				return false;
			}
			this.RemoveAtByMovingTailWithCapacity(index);
			return true;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0005F7A8 File Offset: 0x0005D9A8
		public bool Contains(TValue value, IEqualityComparer<TValue> comparer)
		{
			for (int i = 0; i < this.length; i++)
			{
				if (comparer.Equals(this[i], value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0005F7DC File Offset: 0x0005D9DC
		public void Merge(InlinedArray<TValue> other)
		{
			EqualityComparer<TValue> comparer = EqualityComparer<TValue>.Default;
			for (int i = 0; i < other.length; i++)
			{
				TValue value = other[i];
				if (!this.Contains(value, comparer))
				{
					this.Append(value);
				}
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0005F81C File Offset: 0x0005DA1C
		public IEnumerator<TValue> GetEnumerator()
		{
			return new InlinedArray<TValue>.Enumerator
			{
				array = this,
				index = -1
			};
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0005F84C File Offset: 0x0005DA4C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000C47 RID: 3143
		public int length;

		// Token: 0x04000C48 RID: 3144
		public TValue firstValue;

		// Token: 0x04000C49 RID: 3145
		public TValue[] additionalValues;

		// Token: 0x0200023E RID: 574
		private struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x060014F9 RID: 5369 RVA: 0x0005F854 File Offset: 0x0005DA54
			public bool MoveNext()
			{
				if (this.index >= this.array.length)
				{
					return false;
				}
				this.index++;
				return this.index < this.array.length;
			}

			// Token: 0x060014FA RID: 5370 RVA: 0x0005F88C File Offset: 0x0005DA8C
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x060014FB RID: 5371 RVA: 0x0005F895 File Offset: 0x0005DA95
			public TValue Current
			{
				get
				{
					return this.array[this.index];
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x060014FC RID: 5372 RVA: 0x0005F8A8 File Offset: 0x0005DAA8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060014FD RID: 5373 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x04000C4A RID: 3146
			public InlinedArray<TValue> array;

			// Token: 0x04000C4B RID: 3147
			public int index;
		}
	}
}
