using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000501 RID: 1281
	public class AssetReferer : MonoBehaviour
	{
		// Token: 0x06002836 RID: 10294 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject Instantiate(string label, Transform parent)
		{
			return null;
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0000216A File Offset: 0x0000036A
		public global::UnityEngine.Object GetAsset(string label)
		{
			return null;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000F1A3C File Offset: 0x000EFC3C
		public T GetAsset<T>(string label) where T : global::UnityEngine.Object
		{
			return default(T);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x0000216A File Offset: 0x0000036A
		private AssetReferer.ReferenceInfo FindReferenceInfo(string label)
		{
			return null;
		}

		// Token: 0x040028FB RID: 10491
		public List<AssetReferer.ReferenceInfo> infoList;

		// Token: 0x02000502 RID: 1282
		[Serializable]
		public class ReferenceInfo
		{
			// Token: 0x0600283B RID: 10299 RVA: 0x0000216A File Offset: 0x0000036A
			public AssetReferer.ReferenceInfo Copy()
			{
				return null;
			}

			// Token: 0x040028FC RID: 10492
			public string label;

			// Token: 0x040028FD RID: 10493
			public global::UnityEngine.Object assetRef;
		}
	}
}
