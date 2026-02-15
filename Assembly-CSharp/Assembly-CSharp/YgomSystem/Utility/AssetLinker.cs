using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x020004FD RID: 1277
	public class AssetLinker : MonoBehaviour
	{
		// Token: 0x0600282B RID: 10283 RVA: 0x0000216D File Offset: 0x0000036D
		public void Instantiate(string label, Transform parent, Action<GameObject> onFinished)
		{
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetAssetPath(string label)
		{
			return null;
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0000216D File Offset: 0x0000036D
		public void GetAsset(string label, Action<global::UnityEngine.Object> onFinished, Type systemTypeInstance = null)
		{
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0000216A File Offset: 0x0000036A
		private AssetLinker.LinkInfo FindLinkInfo(string label)
		{
			return null;
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetResourcePath(string fullPath)
		{
			return null;
		}

		// Token: 0x040028EF RID: 10479
		public List<AssetLinker.LinkInfo> infoList;

		// Token: 0x040028F0 RID: 10480
		private const string dirNameResources = "/Resources/";

		// Token: 0x040028F1 RID: 10481
		private const int dirNameResourcesLength = 11;

		// Token: 0x040028F2 RID: 10482
		private const string dirNameResourcesAB = "/ResourcesAssetBundle/";

		// Token: 0x040028F3 RID: 10483
		private const int dirNameResourcesABLength = 22;

		// Token: 0x020004FE RID: 1278
		[Serializable]
		public class LinkInfo
		{
			// Token: 0x06002831 RID: 10289 RVA: 0x0000216A File Offset: 0x0000036A
			public AssetLinker.LinkInfo Copy()
			{
				return null;
			}

			// Token: 0x040028F4 RID: 10484
			public string label;

			// Token: 0x040028F5 RID: 10485
			public string assetPath;
		}
	}
}
