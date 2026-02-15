using System;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B9A RID: 2970
	[Serializable]
	public class MDMarkupContentHalfImageTextPage : MDMarkupContentFullTextPage, IMDMarkupContentGlobalText
	{
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600553C RID: 21820 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009247 RID: 37447
		[SerializeField]
		public string resourcePath;
	}
}
