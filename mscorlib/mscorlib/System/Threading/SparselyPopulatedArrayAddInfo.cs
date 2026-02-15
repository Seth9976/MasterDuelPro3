using System;

namespace System.Threading
{
	// Token: 0x0200023E RID: 574
	internal struct SparselyPopulatedArrayAddInfo<T> where T : class
	{
		// Token: 0x0600152B RID: 5419 RVA: 0x0005541A File Offset: 0x0005361A
		internal SparselyPopulatedArrayAddInfo(SparselyPopulatedArrayFragment<T> source, int index)
		{
			this._source = source;
			this._index = index;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0005542A File Offset: 0x0005362A
		internal SparselyPopulatedArrayFragment<T> Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00055432 File Offset: 0x00053632
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04000A62 RID: 2658
		private SparselyPopulatedArrayFragment<T> _source;

		// Token: 0x04000A63 RID: 2659
		private int _index;
	}
}
