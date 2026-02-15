using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B96 RID: 2966
	[Serializable]
	public class MDMarkupContentH1 : MDMarkupContentBase, IMDMarkupContentGlobalText
	{
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06005523 RID: 21795 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06005524 RID: 21796 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x000F4C8C File Offset: 0x000F2E8C
		public MDMarkupContentH1()
		{
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x000F4C8C File Offset: 0x000F2E8C
		public MDMarkupContentH1(string rawText)
		{
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> GetTextGloups()
		{
			return null;
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x0400923C RID: 37436
		[SerializeField]
		public GlobalTextData text;
	}
}
