using System;
using System.Collections.Generic;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F77 RID: 3959
	public class EntryCheckBoxListData : IEntryData
	{
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x0600746F RID: 29807 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x06007470 RID: 29808 RVA: 0x00002739 File Offset: 0x00000939
		public EntryCheckBoxListData(List<EntryCheckBoxListData.EntryCheckBoxData> checkBoxList, Action<List<bool>> callback, bool isEnableMulti = true)
		{
		}

		// Token: 0x0400AD83 RID: 44419
		public List<EntryCheckBoxListData.EntryCheckBoxData> checkBoxList;

		// Token: 0x0400AD84 RID: 44420
		public Action<List<bool>> callback;

		// Token: 0x0400AD85 RID: 44421
		public bool isEnableMulti;

		// Token: 0x02000F78 RID: 3960
		public class EntryCheckBoxData
		{
			// Token: 0x06007471 RID: 29809 RVA: 0x00002739 File Offset: 0x00000939
			public EntryCheckBoxData(string text, bool isOn = false, bool interactive = true)
			{
			}

			// Token: 0x0400AD86 RID: 44422
			public string text;

			// Token: 0x0400AD87 RID: 44423
			public bool isOn;

			// Token: 0x0400AD88 RID: 44424
			public bool interactive;
		}
	}
}
