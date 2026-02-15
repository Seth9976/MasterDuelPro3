using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BA2 RID: 2978
	[Serializable]
	public class MDMarkupContentText : MDMarkupContentBase, IMDMarkupContentGlobalText
	{
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06005568 RID: 21864 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06005569 RID: 21865 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x000F4C8C File Offset: 0x000F2E8C
		public MDMarkupContentText()
		{
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x000F4C8C File Offset: 0x000F2E8C
		public MDMarkupContentText(string rawText)
		{
		}

		// Token: 0x0600556C RID: 21868 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> GetTextGloups()
		{
			return null;
		}

		// Token: 0x0600556D RID: 21869 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x0400925C RID: 37468
		[SerializeField]
		[MDMarkupIndent]
		public int indent;

		// Token: 0x0400925D RID: 37469
		[SerializeField]
		public TextAlignmentOptions alignment;

		// Token: 0x0400925E RID: 37470
		[SerializeField]
		public GlobalTextData text;

		// Token: 0x0400925F RID: 37471
		[SerializeField]
		public bool ignorePadding;
	}
}
