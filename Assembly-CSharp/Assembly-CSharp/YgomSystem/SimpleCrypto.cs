using System;

namespace YgomSystem
{
	// Token: 0x020004CE RID: 1230
	public class SimpleCrypto
	{
		// Token: 0x0600275D RID: 10077 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Encrypt(ref byte[] data, string key)
		{
		}

		// Token: 0x020004CF RID: 1231
		private class SysRand
		{
			// Token: 0x0600275F RID: 10079 RVA: 0x00002739 File Offset: 0x00000939
			public SysRand(int Seed)
			{
			}

			// Token: 0x06002760 RID: 10080 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Next(int minValue, int maxValue)
			{
				return 0;
			}

			// Token: 0x06002761 RID: 10081 RVA: 0x000F165E File Offset: 0x000EF85E
			private double Sample()
			{
				return 0.0;
			}

			// Token: 0x04002840 RID: 10304
			private const int MBIG = 2147483647;

			// Token: 0x04002841 RID: 10305
			private const int MSEED = 161803398;

			// Token: 0x04002842 RID: 10306
			private int inext;

			// Token: 0x04002843 RID: 10307
			private int inextp;

			// Token: 0x04002844 RID: 10308
			private int[] seedArray;
		}
	}
}
