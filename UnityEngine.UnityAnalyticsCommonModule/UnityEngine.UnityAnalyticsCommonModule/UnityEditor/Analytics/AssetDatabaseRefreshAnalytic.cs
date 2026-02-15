using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200000B RID: 11
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetDatabaseRefreshAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000215B File Offset: 0x0000035B
		public AssetDatabaseRefreshAnalytic()
			: base("assetDatabaseInitRefresh", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002174 File Offset: 0x00000374
		[RequiredByNativeCode]
		internal static AssetDatabaseRefreshAnalytic CreateAssetDatabaseRefreshAnalytic()
		{
			return new AssetDatabaseRefreshAnalytic();
		}

		// Token: 0x0400000A RID: 10
		[SerializeField]
		public bool isV2;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		public long Imports_Imported;

		// Token: 0x0400000C RID: 12
		[SerializeField]
		public long Imports_ImportedInProcess;

		// Token: 0x0400000D RID: 13
		[SerializeField]
		public long Imports_ImportedOutOfProcess;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		public long Imports_Refresh;

		// Token: 0x0400000F RID: 15
		[SerializeField]
		public long Imports_DomainReload;

		// Token: 0x04000010 RID: 16
		[SerializeField]
		public long CacheServer_MetadataRequested;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		public long CacheServer_MetadataDownloaded;

		// Token: 0x04000012 RID: 18
		[SerializeField]
		public long CacheServer_MetadataFailedToDownload;

		// Token: 0x04000013 RID: 19
		[SerializeField]
		public long CacheServer_MetadataUploaded;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		public long CacheServer_ArtifactsFailedToUpload;

		// Token: 0x04000015 RID: 21
		[SerializeField]
		public long CacheServer_MetadataVersionsDownloaded;

		// Token: 0x04000016 RID: 22
		[SerializeField]
		public long CacheServer_MetadataMatched;

		// Token: 0x04000017 RID: 23
		[SerializeField]
		public long CacheServer_ArtifactsDownloaded;

		// Token: 0x04000018 RID: 24
		[SerializeField]
		public long CacheServer_ArtifactFilesDownloaded;

		// Token: 0x04000019 RID: 25
		[SerializeField]
		public long CacheServer_ArtifactFilesFailedToDownload;

		// Token: 0x0400001A RID: 26
		[SerializeField]
		public long CacheServer_ArtifactsUploaded;

		// Token: 0x0400001B RID: 27
		[SerializeField]
		public long CacheServer_ArtifactFilesUploaded;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		public long CacheServer_ArtifactFilesFailedToUpload;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		public long CacheServer_Connects;

		// Token: 0x0400001E RID: 30
		[SerializeField]
		public long CacheServer_Disconnects;
	}
}
