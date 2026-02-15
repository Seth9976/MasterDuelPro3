using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B93 RID: 2963
	[Serializable]
	public class MDMarkupContentEmpty : MDMarkupContentBase, IMDMarkupContent
	{
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06005510 RID: 21776 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06005511 RID: 21777 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}
	}
}
