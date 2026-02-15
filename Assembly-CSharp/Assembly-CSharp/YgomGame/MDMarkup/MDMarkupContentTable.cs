using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BA1 RID: 2977
	[Serializable]
	public class MDMarkupContentTable : MDMarkupContentBase, IMDMarkupContentGlobalText
	{
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06005561 RID: 21857 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06005562 RID: 21858 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06005563 RID: 21859 RVA: 0x0000216A File Offset: 0x0000036A
		public TableSizeSetting sizeSetting
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> GetTextGloups()
		{
			return null;
		}

		// Token: 0x06005565 RID: 21861 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009258 RID: 37464
		[SerializeField]
		[MDMarkupIndent]
		public int indent;

		// Token: 0x04009259 RID: 37465
		[SerializeField]
		public bool ignorePadding;

		// Token: 0x0400925A RID: 37466
		[SerializeField]
		private TableSizeSetting setting;

		// Token: 0x0400925B RID: 37467
		[SerializeField]
		public List<TableRow> rows;
	}
}
