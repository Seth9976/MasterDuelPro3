using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001E RID: 30
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class AssetImportAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000248B File Offset: 0x0000068B
		public AssetImportAnalytic()
			: base("assetImport", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024A4 File Offset: 0x000006A4
		[RequiredByNativeCode]
		public static AssetImportAnalytic CreateAssetImportAnalytic()
		{
			return new AssetImportAnalytic();
		}

		// Token: 0x04000050 RID: 80
		public string package_name;

		// Token: 0x04000051 RID: 81
		public int package_import_choice;
	}
}
