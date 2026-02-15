using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000052 RID: 82
	internal sealed class Set<TElement>
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0000B464 File Offset: 0x00009664
		public Set(IEqualityComparer<TElement> comparer)
		{
			this._comparer = comparer ?? EqualityComparer<TElement>.Default;
			this._buckets = new int[7];
			this._slots = new Set<TElement>.Slot[7];
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B494 File Offset: 0x00009694
		public bool Add(TElement value)
		{
			int num = this.InternalGetHashCode(value);
			for (int i = this._buckets[num % this._buckets.Length] - 1; i >= 0; i = this._slots[i]._next)
			{
				if (this._slots[i]._hashCode == num && this._comparer.Equals(this._slots[i]._value, value))
				{
					return false;
				}
			}
			if (this._count == this._slots.Length)
			{
				this.Resize();
			}
			int count = this._count;
			this._count++;
			int num2 = num % this._buckets.Length;
			this._slots[count]._hashCode = num;
			this._slots[count]._value = value;
			this._slots[count]._next = this._buckets[num2] - 1;
			this._buckets[num2] = count + 1;
			return true;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B58C File Offset: 0x0000978C
		public bool Remove(TElement value)
		{
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			int num3 = -1;
			for (int i = this._buckets[num2] - 1; i >= 0; i = this._slots[i]._next)
			{
				if (this._slots[i]._hashCode == num && this._comparer.Equals(this._slots[i]._value, value))
				{
					if (num3 < 0)
					{
						this._buckets[num2] = this._slots[i]._next + 1;
					}
					else
					{
						this._slots[num3]._next = this._slots[i]._next;
					}
					this._slots[i]._hashCode = -1;
					this._slots[i]._value = default(TElement);
					this._slots[i]._next = -1;
					return true;
				}
				num3 = i;
			}
			return false;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B694 File Offset: 0x00009894
		private void Resize()
		{
			int num = checked(this._count * 2 + 1);
			int[] array = new int[num];
			Set<TElement>.Slot[] array2 = new Set<TElement>.Slot[num];
			Array.Copy(this._slots, 0, array2, 0, this._count);
			for (int i = 0; i < this._count; i++)
			{
				int num2 = array2[i]._hashCode % num;
				array2[i]._next = array[num2] - 1;
				array[num2] = i + 1;
			}
			this._buckets = array;
			this._slots = array2;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B718 File Offset: 0x00009918
		public TElement[] ToArray()
		{
			TElement[] array = new TElement[this._count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = this._slots[num]._value;
			}
			return array;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B758 File Offset: 0x00009958
		public List<TElement> ToList()
		{
			int count = this._count;
			List<TElement> list = new List<TElement>(count);
			for (int num = 0; num != count; num++)
			{
				list.Add(this._slots[num]._value);
			}
			return list;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000B797 File Offset: 0x00009997
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B7A0 File Offset: 0x000099A0
		public void UnionWith(IEnumerable<TElement> other)
		{
			foreach (TElement telement in other)
			{
				this.Add(telement);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B7EC File Offset: 0x000099EC
		private int InternalGetHashCode(TElement value)
		{
			if (value != null)
			{
				return this._comparer.GetHashCode(value) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x040000D3 RID: 211
		private readonly IEqualityComparer<TElement> _comparer;

		// Token: 0x040000D4 RID: 212
		private int[] _buckets;

		// Token: 0x040000D5 RID: 213
		private Set<TElement>.Slot[] _slots;

		// Token: 0x040000D6 RID: 214
		private int _count;

		// Token: 0x02000053 RID: 83
		private struct Slot
		{
			// Token: 0x040000D7 RID: 215
			internal int _hashCode;

			// Token: 0x040000D8 RID: 216
			internal int _next;

			// Token: 0x040000D9 RID: 217
			internal TElement _value;
		}
	}
}
