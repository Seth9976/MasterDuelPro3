using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace YgomSystem.YGomTMPro
{
	// Token: 0x020004F0 RID: 1264
	public class TextMeshProFontCacheManager : MonoBehaviour
	{
		// Token: 0x06002801 RID: 10241 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0000216A File Offset: 0x0000036A
		public static TMP_FontAsset GetCache(GameObject owner, TMP_FontAsset fontAssetRef)
		{
			return null;
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ReleaseCache(GameObject owner, TMP_FontAsset fontAssetRef)
		{
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ReleaseAllCache()
		{
		}

		// Token: 0x040028C1 RID: 10433
		private static TextMeshProFontCacheManager s_Instance;

		// Token: 0x040028C2 RID: 10434
		private List<TMP_FontAsset> m_SearchList;

		// Token: 0x040028C3 RID: 10435
		private Dictionary<TMP_FontAsset, List<GameObject>> m_FontReferenceMap;
	}
}
