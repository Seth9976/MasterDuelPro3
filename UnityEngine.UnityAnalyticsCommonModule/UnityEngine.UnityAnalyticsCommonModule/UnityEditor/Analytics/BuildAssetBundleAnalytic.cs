using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200000C RID: 12
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class BuildAssetBundleAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000218B File Offset: 0x0000038B
		public BuildAssetBundleAnalytic()
			: base("unity5BuildAssetBundles", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021A4 File Offset: 0x000003A4
		[RequiredByNativeCode]
		internal static BuildAssetBundleAnalytic CreateBuildAssetBundleAnalytic()
		{
			return new BuildAssetBundleAnalytic();
		}

		// Token: 0x0400001F RID: 31
		public bool success;

		// Token: 0x04000020 RID: 32
		public string error;
	}
}
