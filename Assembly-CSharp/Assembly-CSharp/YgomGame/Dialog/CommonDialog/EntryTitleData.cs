using System;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F82 RID: 3970
	public class EntryTitleData : IEntryData
	{
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06007491 RID: 29841 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x06007492 RID: 29842 RVA: 0x00002739 File Offset: 0x00000939
		public EntryTitleData(string text = null, CommonDialogTitleWidget.IconType iconType = CommonDialogTitleWidget.IconType.None)
		{
		}

		// Token: 0x0400ADA8 RID: 44456
		public string text;

		// Token: 0x0400ADA9 RID: 44457
		public CommonDialogTitleWidget.IconType iconType;
	}
}
