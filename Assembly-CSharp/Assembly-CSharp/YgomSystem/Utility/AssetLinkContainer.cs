using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x020004FA RID: 1274
	public class AssetLinkContainer : ScriptableObject
	{
		// Token: 0x06002824 RID: 10276 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual AssetLinkContainer.Container GetContainer(string label)
		{
			return null;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0000216D File Offset: 0x0000036D
		public void GetAsset(string label, Action<global::UnityEngine.Object> onFinished, Type systemTypeInstance = null)
		{
		}

		// Token: 0x040028EB RID: 10475
		public List<AssetLinkContainer.Container> containers;

		// Token: 0x020004FB RID: 1275
		[Serializable]
		public class Container
		{
			// Token: 0x06002827 RID: 10279 RVA: 0x0000216A File Offset: 0x0000036A
			public AssetLinkContainer.Container Copy()
			{
				return null;
			}

			// Token: 0x040028EC RID: 10476
			public string label;

			// Token: 0x040028ED RID: 10477
			public string assetPath;
		}
	}
}
