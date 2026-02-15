using System;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F79 RID: 3961
	public class EntryIconTextData : IEntryData
	{
		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06007472 RID: 29810 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x06007473 RID: 29811 RVA: 0x00002739 File Offset: 0x00000939
		public EntryIconTextData(CommonDialogIconTextWidget.IconType iconType, string text, string numText = null)
		{
		}

		// Token: 0x06007474 RID: 29812 RVA: 0x00002739 File Offset: 0x00000939
		public EntryIconTextData(string iconPath, string text, string numText = null)
		{
		}

		// Token: 0x06007475 RID: 29813 RVA: 0x00002739 File Offset: 0x00000939
		public EntryIconTextData(bool itemIsPeriod, int itemCategory, int itemId, string text, string numText = null)
		{
		}

		// Token: 0x0400AD89 RID: 44425
		public CommonDialogIconTextWidget.IconType iconType;

		// Token: 0x0400AD8A RID: 44426
		public string text;

		// Token: 0x0400AD8B RID: 44427
		public string iconPath;

		// Token: 0x0400AD8C RID: 44428
		public string numText;

		// Token: 0x0400AD8D RID: 44429
		public bool itemIsPeriod;

		// Token: 0x0400AD8E RID: 44430
		public int itemCategory;

		// Token: 0x0400AD8F RID: 44431
		public int itemId;
	}
}
