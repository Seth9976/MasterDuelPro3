using System;
using System.Collections.Generic;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006F0 RID: 1776
	public class ResourceLoader
	{
		// Token: 0x06003755 RID: 14165 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ResourceManager.ProgressHandler progressHandler, ResourceManager.RetryHandler retryHandler, float HttpTimeOut)
		{
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x0000216A File Offset: 0x0000036A
		public IResourceLoader GetLoader(Resource.Type resType)
		{
			return null;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x0000216D File Offset: 0x0000036D
		public void LateUpdate()
		{
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearCache()
		{
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x0000216D File Offset: 0x0000036D
		public void Destroy()
		{
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPathFromSpriteTagName(string tag)
		{
			return null;
		}

		// Token: 0x04003163 RID: 12643
		private Dictionary<int, IResourceLoader> loaderDic;

		// Token: 0x04003164 RID: 12644
		private Dictionary<Resource.Type, ResourceLoader.LoadType> resourceTypeToLoaderDic;

		// Token: 0x020006F1 RID: 1777
		public enum LoadType
		{
			// Token: 0x04003166 RID: 12646
			None,
			// Token: 0x04003167 RID: 12647
			BuiltIn,
			// Token: 0x04003168 RID: 12648
			AssetBundle,
			// Token: 0x04003169 RID: 12649
			Binary,
			// Token: 0x0400316A RID: 12650
			Network,
			// Token: 0x0400316B RID: 12651
			StreamingBinary,
			// Token: 0x0400316C RID: 12652
			PlayAssetDelivery
		}
	}
}
