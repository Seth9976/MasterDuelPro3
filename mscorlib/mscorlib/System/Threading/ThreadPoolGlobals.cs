using System;

namespace System.Threading
{
	// Token: 0x0200026A RID: 618
	internal static class ThreadPoolGlobals
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x000599F5 File Offset: 0x00057BF5
		public static bool tpHosted
		{
			get
			{
				return ThreadPool.IsThreadPoolHosted();
			}
		}

		// Token: 0x04000AF3 RID: 2803
		public const uint tpQuantum = 30U;

		// Token: 0x04000AF4 RID: 2804
		public static int processorCount = Environment.ProcessorCount;

		// Token: 0x04000AF5 RID: 2805
		public static volatile bool vmTpInitialized;

		// Token: 0x04000AF6 RID: 2806
		public static bool enableWorkerTracking;

		// Token: 0x04000AF7 RID: 2807
		public static readonly ThreadPoolWorkQueue workQueue = new ThreadPoolWorkQueue();
	}
}
