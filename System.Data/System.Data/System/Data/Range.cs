using System;

namespace System.Data
{
	// Token: 0x02000088 RID: 136
	internal struct Range
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x000226BC File Offset: 0x000208BC
		public Range(int min, int max)
		{
			if (min > max)
			{
				throw ExceptionBuilder.RangeArgument(min, max);
			}
			this._min = min;
			this._max = max;
			this._isNotNull = true;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x000226DF File Offset: 0x000208DF
		public int Count
		{
			get
			{
				if (!this.IsNull)
				{
					return this._max - this._min + 1;
				}
				return 0;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x000226FA File Offset: 0x000208FA
		public bool IsNull
		{
			get
			{
				return !this._isNotNull;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00022705 File Offset: 0x00020905
		public int Min
		{
			get
			{
				this.CheckNull();
				return this._min;
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00022713 File Offset: 0x00020913
		internal void CheckNull()
		{
			if (this.IsNull)
			{
				throw ExceptionBuilder.NullRange();
			}
		}

		// Token: 0x040002A6 RID: 678
		private int _min;

		// Token: 0x040002A7 RID: 679
		private int _max;

		// Token: 0x040002A8 RID: 680
		private bool _isNotNull;
	}
}
