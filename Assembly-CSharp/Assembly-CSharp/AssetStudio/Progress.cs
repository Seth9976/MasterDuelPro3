using System;

namespace AssetStudio
{
	// Token: 0x02000176 RID: 374
	public static class Progress
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00018526 File Offset: 0x00016726
		public static void Reset()
		{
			Progress.preValue = 0;
			Progress.Default.Report(0);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00018539 File Offset: 0x00016739
		public static void Report(int current, int total)
		{
			Progress.Report((int)((float)current * 100f / (float)total));
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001854C File Offset: 0x0001674C
		private static void Report(int value)
		{
			if (value > Progress.preValue)
			{
				Progress.preValue = value;
				Progress.Default.Report(value);
			}
		}

		// Token: 0x040009CC RID: 2508
		public static IProgress<int> Default = new Progress<int>();

		// Token: 0x040009CD RID: 2509
		private static int preValue;
	}
}
