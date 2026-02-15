using System;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B9E RID: 2974
	[Serializable]
	public class MDMarkupContentSeparator : MDMarkupContentBase, IMDMarkupPrefabContent
	{
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06005552 RID: 21842 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06005553 RID: 21843 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPrefabLabel()
		{
			return null;
		}

		// Token: 0x04009252 RID: 37458
		public const string k_PrefKey = "separator";

		// Token: 0x04009253 RID: 37459
		[SerializeField]
		[MDMarkupIndent]
		public int indent;
	}
}
