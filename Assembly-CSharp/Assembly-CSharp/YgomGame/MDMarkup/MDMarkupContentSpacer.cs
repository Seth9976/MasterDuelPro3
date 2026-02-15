using System;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BA0 RID: 2976
	[Serializable]
	public class MDMarkupContentSpacer : MDMarkupContentBase, IMDMarkupPrefabContent
	{
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600555B RID: 21851 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600555C RID: 21852 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPrefabLabel()
		{
			return null;
		}

		// Token: 0x04009256 RID: 37462
		[SerializeField]
		[MDMarkupIndent]
		public int indent;

		// Token: 0x04009257 RID: 37463
		[SerializeField]
		public MDMarkupDef.SpacerSize size;
	}
}
