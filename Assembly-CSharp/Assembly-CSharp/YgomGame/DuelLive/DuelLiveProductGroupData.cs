using System;
using UnityEngine;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C54 RID: 3156
	[Serializable]
	public class DuelLiveProductGroupData : IDuelLiveProductGruopData
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06005A13 RID: 23059 RVA: 0x000029CC File Offset: 0x00000BCC
		public int groupId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06005A14 RID: 23060 RVA: 0x0000216A File Offset: 0x0000036A
		public string labelTextId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06005A15 RID: 23061 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual string labelText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06005A16 RID: 23062 RVA: 0x0000216A File Offset: 0x0000036A
		public string param
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005A17 RID: 23063 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGroupId(int id)
		{
		}

		// Token: 0x06005A18 RID: 23064 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabelTextId(string id)
		{
		}

		// Token: 0x06005A19 RID: 23065 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetParam(string para)
		{
		}

		// Token: 0x06005A1A RID: 23066 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool IsMatchProduct(IProductContext product)
		{
			return false;
		}

		// Token: 0x040095A7 RID: 38311
		[SerializeField]
		private int m_GroupId;

		// Token: 0x040095A8 RID: 38312
		[SerializeField]
		private string m_LabelTextId;

		// Token: 0x040095A9 RID: 38313
		[SerializeField]
		private string m_Param;
	}
}
