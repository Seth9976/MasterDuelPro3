using System;

namespace System.Collections.Generic
{
	// Token: 0x02000151 RID: 337
	internal struct ArrayBuilder<T>
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x0002BE48 File Offset: 0x0002A048
		public ArrayBuilder(int capacity)
		{
			this = default(ArrayBuilder<T>);
			if (capacity > 0)
			{
				this._array = new T[capacity];
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002BE61 File Offset: 0x0002A061
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

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0002BE71 File Offset: 0x0002A071
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170001D8 RID: 472
		public T this[int index]
		{
			get
			{
				return this._array[index];
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002BE87 File Offset: 0x0002A087
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002BEAC File Offset: 0x0002A0AC
		public T First()
		{
			return this._array[0];
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002BEBA File Offset: 0x0002A0BA
		public T Last()
		{
			return this._array[this._count - 1];
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002BED0 File Offset: 0x0002A0D0
		public T[] ToArray()
		{
			if (this._count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = this._array;
			if (this._count < array.Length)
			{
				array = new T[this._count];
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			return array;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002BF20 File Offset: 0x0002A120
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002BF4C File Offset: 0x0002A14C
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

		// Token: 0x04000359 RID: 857
		private T[] _array;

		// Token: 0x0400035A RID: 858
		private int _count;
	}
}
