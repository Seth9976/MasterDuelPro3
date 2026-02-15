using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x020004F8 RID: 1272
	public class AssetContainer : ScriptableObject
	{
		// Token: 0x0600281F RID: 10271 RVA: 0x0000216A File Offset: 0x0000036A
		public AssetContainer.Container GetContainer(string label)
		{
			return null;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x0000216A File Offset: 0x0000036A
		public global::UnityEngine.Object GetAsset(string label)
		{
			return null;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000F1A14 File Offset: 0x000EFC14
		public T GetAsset<T>(string label) where T : global::UnityEngine.Object
		{
			return default(T);
		}

		// Token: 0x040028E8 RID: 10472
		public List<AssetContainer.Container> containers;

		// Token: 0x020004F9 RID: 1273
		[Serializable]
		public class Container
		{
			// Token: 0x040028E9 RID: 10473
			public string label;

			// Token: 0x040028EA RID: 10474
			public global::UnityEngine.Object asset;
		}
	}
}
