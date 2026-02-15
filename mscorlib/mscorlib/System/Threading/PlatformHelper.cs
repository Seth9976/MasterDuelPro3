using System;

namespace System.Threading
{
	// Token: 0x02000232 RID: 562
	internal static class PlatformHelper
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x000546B4 File Offset: 0x000528B4
		internal static int ProcessorCount
		{
			get
			{
				int tickCount = Environment.TickCount;
				int num = PlatformHelper.s_processorCount;
				if (num == 0 || tickCount - PlatformHelper.s_lastProcessorCountRefreshTicks >= 30000)
				{
					num = (PlatformHelper.s_processorCount = Environment.ProcessorCount);
					PlatformHelper.s_lastProcessorCountRefreshTicks = tickCount;
				}
				return num;
			}
		}

		// Token: 0x04000A3D RID: 2621
		private const int PROCESSOR_COUNT_REFRESH_INTERVAL_MS = 30000;

		// Token: 0x04000A3E RID: 2622
		private static volatile int s_processorCount;

		// Token: 0x04000A3F RID: 2623
		private static volatile int s_lastProcessorCountRefreshTicks;

		// Token: 0x04000A40 RID: 2624
		internal static readonly bool IsSingleProcessor = PlatformHelper.ProcessorCount == 1;
	}
}
