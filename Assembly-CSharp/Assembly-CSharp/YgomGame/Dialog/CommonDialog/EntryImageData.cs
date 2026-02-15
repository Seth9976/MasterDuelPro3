using System;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F7A RID: 3962
	public class EntryImageData : IEntryData
	{
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06007476 RID: 29814 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x06007477 RID: 29815 RVA: 0x00002739 File Offset: 0x00000939
		public EntryImageData(string path = null)
		{
		}

		// Token: 0x0400AD90 RID: 44432
		public string path;
	}
}
