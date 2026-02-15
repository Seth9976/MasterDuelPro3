using System;

namespace Org.Brotli.Dec
{
	// Token: 0x0200007F RID: 127
	internal sealed class HuffmanTreeGroup
	{
		// Token: 0x06000272 RID: 626 RVA: 0x000091B5 File Offset: 0x000073B5
		internal static void Init(HuffmanTreeGroup group, int alphabetSize, int n)
		{
			group.alphabetSize = alphabetSize;
			group.codes = new int[n * 1080];
			group.trees = new int[n];
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000091DC File Offset: 0x000073DC
		internal static void Decode(HuffmanTreeGroup group, BitReader br)
		{
			int next = 0;
			int i = group.trees.Length;
			for (int j = 0; j < i; j++)
			{
				group.trees[j] = next;
				Org.Brotli.Dec.Decode.ReadHuffmanCode(group.alphabetSize, group.codes, next, br);
				next += 1080;
			}
		}

		// Token: 0x040002EC RID: 748
		private int alphabetSize;

		// Token: 0x040002ED RID: 749
		internal int[] codes;

		// Token: 0x040002EE RID: 750
		internal int[] trees;
	}
}
