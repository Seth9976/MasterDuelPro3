using System;
using TMPro;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F81 RID: 3969
	public class EntryTextData : IEntryData
	{
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x0600748F RID: 29839 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x06007490 RID: 29840 RVA: 0x00002739 File Offset: 0x00000939
		public EntryTextData(string text = null, TextAlignmentOptions alignment = TextAlignmentOptions.Midline, bool baseVisible = true)
		{
		}

		// Token: 0x0400ADA5 RID: 44453
		public string text;

		// Token: 0x0400ADA6 RID: 44454
		public TextAlignmentOptions alignment;

		// Token: 0x0400ADA7 RID: 44455
		public bool baseVisible;
	}
}
