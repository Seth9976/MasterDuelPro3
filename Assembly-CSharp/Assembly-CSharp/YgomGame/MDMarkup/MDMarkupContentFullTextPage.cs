using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B95 RID: 2965
	[Serializable]
	public class MDMarkupContentFullTextPage : MDMarkupContentPageBase, IMDMarkupContentGlobalText
	{
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600551E RID: 21790 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IReadOnlyList<string> GetTextGloups()
		{
			return null;
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009239 RID: 37433
		[SerializeField]
		public GlobalTextData caption;

		// Token: 0x0400923A RID: 37434
		[SerializeField]
		public GlobalTextData text;

		// Token: 0x0400923B RID: 37435
		[SerializeField]
		public List<URLSchemeButton> buttons;
	}
}
