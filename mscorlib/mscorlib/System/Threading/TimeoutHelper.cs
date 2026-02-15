using System;

namespace System.Threading
{
	// Token: 0x02000233 RID: 563
	internal static class TimeoutHelper
	{
		// Token: 0x060014ED RID: 5357 RVA: 0x00054708 File Offset: 0x00052908
		public static uint GetTime()
		{
			return (uint)Environment.TickCount;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00054710 File Offset: 0x00052910
		public static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
		{
			uint num = TimeoutHelper.GetTime() - startTime;
			if (num > 2147483647U)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}
	}
}
