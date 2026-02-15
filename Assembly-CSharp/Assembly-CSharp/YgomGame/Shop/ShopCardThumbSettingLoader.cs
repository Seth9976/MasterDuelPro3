using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Shop
{
	// Token: 0x02000948 RID: 2376
	public class ShopCardThumbSettingLoader : MonoBehaviour
	{
		// Token: 0x060045EC RID: 17900 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(GameObject owner, Action<ShopCardThumbSettings> callback)
		{
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Unload(GameObject owner)
		{
		}

		// Token: 0x0400840E RID: 33806
		private static ShopCardThumbSettingLoader s_Instance;

		// Token: 0x0400840F RID: 33807
		private ShopCardThumbSettings m_Setting;

		// Token: 0x04008410 RID: 33808
		private List<ValueTuple<GameObject, Action<ShopCardThumbSettings>>> m_Requests;

		// Token: 0x04008411 RID: 33809
		[SerializeField]
		private List<GameObject> m_Referer;
	}
}
