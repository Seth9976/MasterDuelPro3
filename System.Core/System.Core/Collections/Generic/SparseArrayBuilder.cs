using System;

namespace System.Collections.Generic
{
	// Token: 0x02000158 RID: 344
	internal struct SparseArrayBuilder<T>
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x0002C648 File Offset: 0x0002A848
		public SparseArrayBuilder(bool initialize)
		{
			this = default(SparseArrayBuilder<T>);
			this._builder = new LargeArrayBuilder<T>(true);
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002C65D File Offset: 0x0002A85D
		public int Count
		{
			get
			{
				return checked(this._builder.Count + this._reservedCount);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0002C671 File Offset: 0x0002A871
		public ArrayBuilder<Marker> Markers
		{
			get
			{
				return this._markers;
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002C679 File Offset: 0x0002A879
		public void AddRange(IEnumerable<T> items)
		{
			this._builder.AddRange(items);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002C688 File Offset: 0x0002A888
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			CopyPosition copyPosition = CopyPosition.Start;
			for (int i = 0; i < this._markers.Count; i++)
			{
				Marker marker = this._markers[i];
				int num2 = Math.Min(marker.Index - num, count);
				if (num2 > 0)
				{
					copyPosition = this._builder.CopyTo(copyPosition, array, arrayIndex, num2);
					arrayIndex += num2;
					num += num2;
					count -= num2;
				}
				if (count == 0)
				{
					return;
				}
				int num3 = Math.Min(marker.Count, count);
				arrayIndex += num3;
				num += num3;
				count -= num3;
			}
			if (count > 0)
			{
				this._builder.CopyTo(copyPosition, array, arrayIndex, count);
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002C730 File Offset: 0x0002A930
		public void Reserve(int count)
		{
			this._markers.Add(new Marker(count, this.Count));
			checked
			{
				this._reservedCount += count;
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002C758 File Offset: 0x0002A958
		public bool ReserveOrAdd(IEnumerable<T> items)
		{
			int num;
			if (EnumerableHelpers.TryGetCount<T>(items, out num))
			{
				if (num > 0)
				{
					this.Reserve(num);
					return true;
				}
			}
			else
			{
				this.AddRange(items);
			}
			return false;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002C784 File Offset: 0x0002A984
		public T[] ToArray()
		{
			if (this._markers.Count == 0)
			{
				return this._builder.ToArray();
			}
			T[] array = new T[this.Count];
			this.CopyTo(array, 0, array.Length);
			return array;
		}

		// Token: 0x04000369 RID: 873
		private LargeArrayBuilder<T> _builder;

		// Token: 0x0400036A RID: 874
		private ArrayBuilder<Marker> _markers;

		// Token: 0x0400036B RID: 875
		private int _reservedCount;
	}
}
