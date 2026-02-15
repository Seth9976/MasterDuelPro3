using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000013 RID: 19
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class SendGameBuildAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000022DB File Offset: 0x000004DB
		public SendGameBuildAnalytic()
			: base("navigation_gamebuild_info", 1, SendEventOptions.kAppendBuildGuid, "")
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022F4 File Offset: 0x000004F4
		[RequiredByNativeCode]
		internal static SendGameBuildAnalytic CreateSendGameBuildAnalytic()
		{
			return new SendGameBuildAnalytic();
		}

		// Token: 0x04000038 RID: 56
		private int navmesh_count;
	}
}
