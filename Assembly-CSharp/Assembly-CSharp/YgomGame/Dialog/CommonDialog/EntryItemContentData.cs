using System;
using System.Collections.Generic;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F7C RID: 3964
	public class EntryItemContentData : IEntryData
	{
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x0600747A RID: 29818 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x0600747B RID: 29819 RVA: 0x00002739 File Offset: 0x00000939
		public EntryItemContentData()
		{
		}

		// Token: 0x0600747C RID: 29820 RVA: 0x00002739 File Offset: 0x00000939
		public EntryItemContentData(bool isPeriod, int itemCategory, int itemId, string text = null, bool effect = false, Dictionary<string, object> itemArgs = null)
		{
		}

		// Token: 0x0400AD92 RID: 44434
		public bool isPeriod;

		// Token: 0x0400AD93 RID: 44435
		public int itemCategory;

		// Token: 0x0400AD94 RID: 44436
		public int itemId;

		// Token: 0x0400AD95 RID: 44437
		public string text;

		// Token: 0x0400AD96 RID: 44438
		public bool effect;

		// Token: 0x0400AD97 RID: 44439
		public string spItemProductLabel;

		// Token: 0x0400AD98 RID: 44440
		public Dictionary<string, object> itemArgs;
	}
}
