using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Shop
{
	// Token: 0x0200095F RID: 2399
	public class ShopInformButtonSettings : ScriptableObject
	{
		// Token: 0x06004617 RID: 17943 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopInformButtonData[] Find(string label)
		{
			return null;
		}

		// Token: 0x04008488 RID: 33928
		internal const string k_Path = "Definition/Shop/ShopSettings";

		// Token: 0x04008489 RID: 33929
		[SerializeField]
		private ShopInformButtonSettings.Data[] m_Datas;

		// Token: 0x0400848A RID: 33930
		private Dictionary<string, ShopInformButtonSettings.Data> m_DataMap;

		// Token: 0x02000960 RID: 2400
		[Serializable]
		public class Data
		{
			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06004619 RID: 17945 RVA: 0x0000216A File Offset: 0x0000036A
			public string label
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x0600461A RID: 17946 RVA: 0x0000216A File Offset: 0x0000036A
			public ShopInformButtonData[] infoButtonDatas
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x0600461B RID: 17947 RVA: 0x0000216A File Offset: 0x0000036A
			public ShopInformButtonData[] infoButtonMobileDatas
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600461C RID: 17948 RVA: 0x00002739 File Offset: 0x00000939
			public Data(string label)
			{
			}

			// Token: 0x0400848B RID: 33931
			[SerializeField]
			private string m_Label;

			// Token: 0x0400848C RID: 33932
			[SerializeField]
			private ShopInformButtonData[] m_InfoButtonDatas;

			// Token: 0x0400848D RID: 33933
			[SerializeField]
			private ShopInformButtonData[] m_InfoButtonMobileDatas;
		}
	}
}
