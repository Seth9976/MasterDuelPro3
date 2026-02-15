using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001F RID: 31
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class AssetExportAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000024BB File Offset: 0x000006BB
		public AssetExportAnalytic()
			: base("assetExport", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000024D4 File Offset: 0x000006D4
		[RequiredByNativeCode]
		public static AssetExportAnalytic CreateAssetExportAnalytic()
		{
			return new AssetExportAnalytic();
		}

		// Token: 0x04000052 RID: 82
		public string package_name;

		// Token: 0x04000053 RID: 83
		public string error_message;

		// Token: 0x04000054 RID: 84
		public int items_count;

		// Token: 0x04000055 RID: 85
		public string[] asset_extensions;

		// Token: 0x04000056 RID: 86
		public bool include_upm_dependencies;
	}
}
