using System;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B9F RID: 2975
	[Serializable]
	public class MDMarkupContentSerializeData
	{
		// Token: 0x06005558 RID: 21848 RVA: 0x00002739 File Offset: 0x00000939
		public MDMarkupContentSerializeData(IMDMarkupContent content)
		{
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x000029CC File Offset: 0x00000BCC
		public MDMarkupDef.MarkupType GetMarkupType()
		{
			return MDMarkupDef.MarkupType.None;
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetData()
		{
			return null;
		}

		// Token: 0x04009254 RID: 37460
		[SerializeField]
		protected string tp;

		// Token: 0x04009255 RID: 37461
		[SerializeField]
		protected string data;
	}
}
