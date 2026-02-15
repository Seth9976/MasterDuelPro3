using System;

namespace System.Collections.Generic
{
	// Token: 0x02000758 RID: 1880
	internal struct ArrayBuilder<T>
	{
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000E7A06 File Offset: 0x000E5C06
		public int Capacity
		{
			get
			{
				T[] array = this._array;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000E7A16 File Offset: 0x000E5C16
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170009C8 RID: 2504
		public T this[int index]
		{
			get
			{
				return this._array[index];
			}
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x000E7A2C File Offset: 0x000E5C2C
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x000E7A54 File Offset: 0x000E5C54
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x000E7A80 File Offset: 0x000E5C80
		private void EnsureCapacity(int minimum)
		{
			int capacity = this.Capacity;
			int num = ((capacity == 0) ? 4 : (2 * capacity));
			if (num > 2146435071)
			{
				num = Math.Max(capacity + 1, 2146435071);
			}
			num = Math.Max(num, minimum);
			T[] array = new T[num];
			if (this._count > 0)
			{
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			this._array = array;
		}

		// Token: 0x04001F27 RID: 7975
		private T[] _array;

		// Token: 0x04001F28 RID: 7976
		private int _count;
	}
}
