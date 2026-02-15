using System;
using UnityEngine;

namespace YgomGame.Shop
{
	// Token: 0x02000962 RID: 2402
	[Serializable]
	public class ShopProductGroupData : IShopProductGruopData
	{
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06004626 RID: 17958 RVA: 0x000029CC File Offset: 0x00000BCC
		public int groupId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x0000216A File Offset: 0x0000036A
		public string labelTextId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual string labelText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06004629 RID: 17961 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool constant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool IsMatchProduct(ProductContext product)
		{
			return false;
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FindBoolData(string key, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x000029CC File Offset: 0x00000BCC
		public int FindIntData(string key, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x0000216A File Offset: 0x0000036A
		public string FindStringData(string key, string defaultValue = null)
		{
			return null;
		}

		// Token: 0x04008496 RID: 33942
		private readonly string k_NobrFormat;

		// Token: 0x04008497 RID: 33943
		[SerializeField]
		private int m_GroupId;

		// Token: 0x04008498 RID: 33944
		[SerializeField]
		private string m_LabelTextId;

		// Token: 0x04008499 RID: 33945
		[SerializeField]
		private bool m_LabelNobr;

		// Token: 0x0400849A RID: 33946
		[SerializeField]
		private bool m_Constant;

		// Token: 0x0400849B RID: 33947
		[SerializeField]
		private string m_ProductWidgetLabel;

		// Token: 0x0400849C RID: 33948
		[SerializeField]
		private int m_BgId;

		// Token: 0x0400849D RID: 33949
		[SerializeField]
		private bool m_SkipSoldoutSort;

		// Token: 0x0400849E RID: 33950
		[SerializeField]
		private bool m_IsShortPayAmountSort;

		// Token: 0x0400849F RID: 33951
		[SerializeField]
		private bool m_IgnoreTurnoffBadge;
	}
}
