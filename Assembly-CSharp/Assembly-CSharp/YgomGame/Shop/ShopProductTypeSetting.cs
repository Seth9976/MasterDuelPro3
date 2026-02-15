using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Shop
{
	// Token: 0x02000964 RID: 2404
	public class ShopProductTypeSetting : ScriptableObject
	{
		// Token: 0x06004632 RID: 17970 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopProductTypeSetting.Data FindProductTypeData(ProductContext productContext)
		{
			return null;
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FindBoolData(ProductContext productContext, string key, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x000029CC File Offset: 0x00000BCC
		public int FindIntData(ProductContext productContext, string key, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x0000216A File Offset: 0x0000036A
		public string FindStringData(ProductContext productContext, string key, string defaultValue = null)
		{
			return null;
		}

		// Token: 0x040084A2 RID: 33954
		[SerializeField]
		private List<ShopProductTypeSetting.Data> m_Datas;

		// Token: 0x040084A3 RID: 33955
		private Dictionary<int, Dictionary<int, Dictionary<bool, ShopProductTypeSetting.Data>>> m_DataMap;

		// Token: 0x02000965 RID: 2405
		[Serializable]
		public class Data
		{
			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06004637 RID: 17975 RVA: 0x000029CC File Offset: 0x00000BCC
			public int productTypeId
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x06004638 RID: 17976 RVA: 0x000029CC File Offset: 0x00000BCC
			public int targetCategoryId
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x06004639 RID: 17977 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool targetIsPeriod
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x0600463A RID: 17978 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool hasSetItems
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x0600463B RID: 17979 RVA: 0x000029CC File Offset: 0x00000BCC
			public int limitAlertSec
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x0600463C RID: 17980 RVA: 0x0000216A File Offset: 0x0000036A
			public string informButtonLabel
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x0600463D RID: 17981 RVA: 0x000029CC File Offset: 0x00000BCC
			public ShopDef.HighlightType highlightType
			{
				get
				{
					return (ShopDef.HighlightType)0;
				}
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x0600463E RID: 17982 RVA: 0x000029CC File Offset: 0x00000BCC
			public ShopDef.ViewerLoopType viewerLoopType
			{
				get
				{
					return ShopDef.ViewerLoopType.Default;
				}
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x0600463F RID: 17983 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool hideSummonPlay
			{
				get
				{
					return false;
				}
			}

			// Token: 0x040084A4 RID: 33956
			[SerializeField]
			private int m_ProductTypeId;

			// Token: 0x040084A5 RID: 33957
			[SerializeField]
			private int m_TargetCategoryId;

			// Token: 0x040084A6 RID: 33958
			[SerializeField]
			private bool m_TargetIsPeriod;

			// Token: 0x040084A7 RID: 33959
			[SerializeField]
			private bool m_HasSetItems;

			// Token: 0x040084A8 RID: 33960
			[SerializeField]
			private int m_LimitAlertSec;

			// Token: 0x040084A9 RID: 33961
			[SerializeField]
			private string m_InformButtonLabel;

			// Token: 0x040084AA RID: 33962
			[SerializeField]
			private ShopDef.HighlightType m_HighlightType;

			// Token: 0x040084AB RID: 33963
			[SerializeField]
			private ShopDef.ViewerLoopType m_ViewerLoopType;

			// Token: 0x040084AC RID: 33964
			[SerializeField]
			private bool m_HideSummonPlay;
		}
	}
}
