using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000056 RID: 86
	internal class Contraction
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00003C55 File Offset: 0x00001E55
		public Contraction(int index, char[] source, string replacement, byte[] sortkey)
		{
			this.Index = index;
			this.Source = source;
			this.Replacement = replacement;
			this.SortKey = sortkey;
		}

		// Token: 0x04000161 RID: 353
		public int Index;

		// Token: 0x04000162 RID: 354
		public readonly char[] Source;

		// Token: 0x04000163 RID: 355
		public readonly string Replacement;

		// Token: 0x04000164 RID: 356
		public readonly byte[] SortKey;
	}
}
