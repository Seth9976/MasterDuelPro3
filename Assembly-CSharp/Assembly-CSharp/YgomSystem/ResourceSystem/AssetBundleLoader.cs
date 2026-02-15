using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.LocalFileSystem;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006D3 RID: 1747
	public class AssetBundleLoader : BaseAssetBundleLoader
	{
		// Token: 0x0600367E RID: 13950 RVA: 0x0000216D File Offset: 0x0000036D
		private void loadDLCList()
		{
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x0000216A File Offset: 0x0000036A
		protected override List<string> GetWithDependenciesList(Resource res)
		{
			return null;
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Initialize()
		{
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x0000216A File Offset: 0x0000036A
		protected override IEnumerator yLoadRequestCache(string loadPath, Action<AssetBundle> callback)
		{
			return null;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void RemoveRequestCache(string loadPath)
		{
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ClearRequestCache()
		{
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ClearCache()
		{
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardIllustPathNotificator(object value)
		{
		}

		// Token: 0x04003133 RID: 12595
		private DependenciesList dlcList;

		// Token: 0x04003134 RID: 12596
		private DependenciesList dlcStreamingList;

		// Token: 0x04003135 RID: 12597
		private AssetBundleLoader.RequestCache reqCache;

		// Token: 0x020006D4 RID: 1748
		internal class RequestCache : AbstractReferenceCache<LocalFileAssetBundleLoadRequest>
		{
			// Token: 0x06003687 RID: 13959 RVA: 0x0000216A File Offset: 0x0000036A
			protected override LocalFileAssetBundleLoadRequest LoadRequest(string key, params object[] param)
			{
				return null;
			}

			// Token: 0x06003688 RID: 13960 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void RemoveCacheAction(LocalFileAssetBundleLoadRequest value)
			{
			}
		}
	}
}
