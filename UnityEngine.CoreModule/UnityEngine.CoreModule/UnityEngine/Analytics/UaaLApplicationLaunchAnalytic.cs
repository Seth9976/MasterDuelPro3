using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x0200031C RID: 796
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class UaaLApplicationLaunchAnalytic : AnalyticsEventBase
	{
		// Token: 0x06001619 RID: 5657 RVA: 0x0002E6BF File Offset: 0x0002C8BF
		public UaaLApplicationLaunchAnalytic()
			: base("UaaLApplicationLaunch", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0002E6D8 File Offset: 0x0002C8D8
		[RequiredByNativeCode]
		public static UaaLApplicationLaunchAnalytic CreateUaaLApplicationLaunchAnalytic()
		{
			return new UaaLApplicationLaunchAnalytic();
		}

		// Token: 0x04000840 RID: 2112
		public int launch_type;

		// Token: 0x04000841 RID: 2113
		public int launch_process_type;
	}
}
