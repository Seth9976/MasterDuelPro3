using System;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F7B RID: 3963
	public class EntryInsertWidgetData : IEntryData
	{
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06007478 RID: 29816 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x06007479 RID: 29817 RVA: 0x00002739 File Offset: 0x00000939
		public EntryInsertWidgetData(IContentWidget insertWidget)
		{
		}

		// Token: 0x0400AD91 RID: 44433
		public IContentWidget insertWidget;
	}
}
