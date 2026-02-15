using System;
using TMPro;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F7F RID: 3967
	public class EntryMaintenanceImageData : IEntryData
	{
		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x0600748B RID: 29835 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x0600748C RID: 29836 RVA: 0x00002739 File Offset: 0x00000939
		public EntryMaintenanceImageData(string textTitle = null, string textDate = null, string imagePath = null, TextAlignmentOptions alignment = TextAlignmentOptions.Midline)
		{
		}

		// Token: 0x0400AD9E RID: 44446
		public string textTitle;

		// Token: 0x0400AD9F RID: 44447
		public string textDate;

		// Token: 0x0400ADA0 RID: 44448
		public string imagePath;

		// Token: 0x0400ADA1 RID: 44449
		public TextAlignmentOptions alignment;
	}
}
