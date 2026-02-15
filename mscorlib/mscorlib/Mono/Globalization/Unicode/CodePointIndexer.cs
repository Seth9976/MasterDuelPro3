using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000053 RID: 83
	internal class CodePointIndexer
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public CodePointIndexer(int[] starts, int[] ends, int defaultIndex, int defaultCP)
		{
			this.defaultIndex = defaultIndex;
			this.defaultCP = defaultCP;
			this.ranges = new CodePointIndexer.TableRange[starts.Length];
			for (int i = 0; i < this.ranges.Length; i++)
			{
				this.ranges[i] = new CodePointIndexer.TableRange(starts[i], ends[i], (i == 0) ? 0 : (this.ranges[i - 1].IndexStart + this.ranges[i - 1].Count));
			}
			for (int j = 0; j < this.ranges.Length; j++)
			{
				this.TotalCount += this.ranges[j].Count;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003B78 File Offset: 0x00001D78
		public int ToIndex(int cp)
		{
			for (int i = 0; i < this.ranges.Length; i++)
			{
				if (cp < this.ranges[i].Start)
				{
					return this.defaultIndex;
				}
				if (cp < this.ranges[i].End)
				{
					return cp - this.ranges[i].Start + this.ranges[i].IndexStart;
				}
			}
			return this.defaultIndex;
		}

		// Token: 0x04000154 RID: 340
		private readonly CodePointIndexer.TableRange[] ranges;

		// Token: 0x04000155 RID: 341
		public readonly int TotalCount;

		// Token: 0x04000156 RID: 342
		private int defaultIndex;

		// Token: 0x04000157 RID: 343
		private int defaultCP;

		// Token: 0x02000054 RID: 84
		[Serializable]
		internal struct TableRange
		{
			// Token: 0x060000D8 RID: 216 RVA: 0x00003BF3 File Offset: 0x00001DF3
			public TableRange(int start, int end, int indexStart)
			{
				this.Start = start;
				this.End = end;
				this.Count = this.End - this.Start;
				this.IndexStart = indexStart;
				this.IndexEnd = this.IndexStart + this.Count;
			}

			// Token: 0x04000158 RID: 344
			public readonly int Start;

			// Token: 0x04000159 RID: 345
			public readonly int End;

			// Token: 0x0400015A RID: 346
			public readonly int Count;

			// Token: 0x0400015B RID: 347
			public readonly int IndexStart;

			// Token: 0x0400015C RID: 348
			public readonly int IndexEnd;
		}
	}
}
