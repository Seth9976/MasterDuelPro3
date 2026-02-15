using System;
using TMPro;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F80 RID: 3968
	public class EntryScrollTextData : IEntryData
	{
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x0600748D RID: 29837 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x0600748E RID: 29838 RVA: 0x00002739 File Offset: 0x00000939
		public EntryScrollTextData(string text = null, int maxHeight = 32, TextAlignmentOptions alignment = TextAlignmentOptions.Midline)
		{
		}

		// Token: 0x0400ADA2 RID: 44450
		public string text;

		// Token: 0x0400ADA3 RID: 44451
		public int maxHeight;

		// Token: 0x0400ADA4 RID: 44452
		public TextAlignmentOptions alignment;
	}
}
