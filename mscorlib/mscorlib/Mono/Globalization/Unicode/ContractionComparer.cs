using System;
using System.Collections.Generic;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000057 RID: 87
	internal class ContractionComparer : IComparer<Contraction>
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00003C7C File Offset: 0x00001E7C
		public int Compare(Contraction c1, Contraction c2)
		{
			char[] source = c1.Source;
			char[] source2 = c2.Source;
			int num = ((source.Length > source2.Length) ? source2.Length : source.Length);
			for (int i = 0; i < num; i++)
			{
				if (source[i] != source2[i])
				{
					return (int)(source[i] - source2[i]);
				}
			}
			if (source.Length != source2.Length)
			{
				return source.Length - source2.Length;
			}
			return c1.Index - c2.Index;
		}

		// Token: 0x04000165 RID: 357
		public static readonly ContractionComparer Instance = new ContractionComparer();
	}
}
