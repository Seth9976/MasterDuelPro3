using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001D RID: 29
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class AssetImportStatusAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000245B File Offset: 0x0000065B
		public AssetImportStatusAnalytic()
			: base("assetImportStatus", 1, SendEventOptions.kAppendBuildTarget, "")
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002474 File Offset: 0x00000674
		[RequiredByNativeCode]
		public static AssetImportStatusAnalytic CreateAssetImportStatusAnalytic()
		{
			return new AssetImportStatusAnalytic();
		}

		// Token: 0x04000046 RID: 70
		public string package_name;

		// Token: 0x04000047 RID: 71
		public int package_items_count;

		// Token: 0x04000048 RID: 72
		public int package_import_status;

		// Token: 0x04000049 RID: 73
		public string error_message;

		// Token: 0x0400004A RID: 74
		public int project_assets_count;

		// Token: 0x0400004B RID: 75
		public int unselected_assets_count;

		// Token: 0x0400004C RID: 76
		public int selected_new_assets_count;

		// Token: 0x0400004D RID: 77
		public int selected_changed_assets_count;

		// Token: 0x0400004E RID: 78
		public int unchanged_assets_count;

		// Token: 0x0400004F RID: 79
		public string[] selected_asset_extensions;
	}
}
