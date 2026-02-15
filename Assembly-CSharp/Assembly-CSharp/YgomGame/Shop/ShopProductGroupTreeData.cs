using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Shop
{
	// Token: 0x02000963 RID: 2403
	[Serializable]
	public class ShopProductGroupTreeData<T> : ShopProductGroupData where T : class, IShopProductGruopData
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600462F RID: 17967 RVA: 0x0000216A File Offset: 0x0000036A
		public T[] children
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x000F48C0 File Offset: 0x000F2AC0
		public T GetGroupData(int groupId)
		{
			return default(T);
		}

		// Token: 0x040084A0 RID: 33952
		[SerializeField]
		private T[] m_Children;

		// Token: 0x040084A1 RID: 33953
		private Dictionary<int, T> m_ChildrenMap;
	}
}
