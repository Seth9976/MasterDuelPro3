using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000070 RID: 112
	internal static class HashUtility
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000ACE1 File Offset: 0x00008EE1
		public static int CombineHash(this int h1, int h2)
		{
			return h1 ^ (int)((long)h2 + (long)((ulong)(-1640531527)) + (long)((long)h1 << 6) + (long)(h1 >> 2));
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		public static int CombineHash(int h1, int h2, int h3)
		{
			return h1.CombineHash(h2).CombineHash(h3);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000AD08 File Offset: 0x00008F08
		public static int CombineHash(int h1, int h2, int h3, int h4)
		{
			return HashUtility.CombineHash(h1, h2, h3).CombineHash(h4);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000AD18 File Offset: 0x00008F18
		public static int CombineHash(int h1, int h2, int h3, int h4, int h5)
		{
			return HashUtility.CombineHash(h1, h2, h3, h4).CombineHash(h5);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000AD2A File Offset: 0x00008F2A
		public static int CombineHash(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return HashUtility.CombineHash(h1, h2, h3, h4, h5).CombineHash(h6);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000AD3E File Offset: 0x00008F3E
		public static int CombineHash(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return HashUtility.CombineHash(h1, h2, h3, h4, h5, h6).CombineHash(h7);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000AD54 File Offset: 0x00008F54
		public static int CombineHash(int[] hashes)
		{
			if (hashes == null || hashes.Length == 0)
			{
				return 0;
			}
			int h = hashes[0];
			for (int i = 1; i < hashes.Length; i++)
			{
				h = h.CombineHash(hashes[i]);
			}
			return h;
		}
	}
}
