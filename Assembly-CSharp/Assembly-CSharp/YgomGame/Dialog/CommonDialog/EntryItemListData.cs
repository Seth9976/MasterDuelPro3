using System;
using System.Collections.Generic;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F7D RID: 3965
	public class EntryItemListData : IEntryData
	{
		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x0600747D RID: 29821 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x0600747E RID: 29822 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600747F RID: 29823 RVA: 0x00002739 File Offset: 0x00000939
		public EntryItemListData()
		{
		}

		// Token: 0x06007480 RID: 29824 RVA: 0x00002739 File Offset: 0x00000939
		public EntryItemListData(List<EntryItemListData.Context> contexts)
		{
		}

		// Token: 0x06007481 RID: 29825 RVA: 0x00002739 File Offset: 0x00000939
		public EntryItemListData(IReadOnlyList<EntryItemListData.Context> contexts)
		{
		}

		// Token: 0x06007482 RID: 29826 RVA: 0x00002739 File Offset: 0x00000939
		public EntryItemListData(EntryItemListData.Context context)
		{
		}

		// Token: 0x06007483 RID: 29827 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(IReadOnlyList<object> list)
		{
		}

		// Token: 0x06007484 RID: 29828 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddContext(EntryItemListData.Context context)
		{
		}

		// Token: 0x0400AD99 RID: 44441
		public List<EntryItemListData.Context> contexts;

		// Token: 0x02000F7E RID: 3966
		public class Context
		{
			// Token: 0x06007485 RID: 29829 RVA: 0x00002739 File Offset: 0x00000939
			public Context()
			{
			}

			// Token: 0x06007486 RID: 29830 RVA: 0x00002739 File Offset: 0x00000939
			public Context(bool isPeriod, int itemCategory, int itemId, int num)
			{
			}

			// Token: 0x06007487 RID: 29831 RVA: 0x00002739 File Offset: 0x00000939
			public Context(object data)
			{
			}

			// Token: 0x06007488 RID: 29832 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(object data)
			{
			}

			// Token: 0x06007489 RID: 29833 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(Dictionary<string, object> data)
			{
			}

			// Token: 0x0600748A RID: 29834 RVA: 0x0000216A File Offset: 0x0000036A
			public static EntryItemListData.Context CreateFromData(object data)
			{
				return null;
			}

			// Token: 0x0400AD9A RID: 44442
			public bool isPeriod;

			// Token: 0x0400AD9B RID: 44443
			public int itemCategory;

			// Token: 0x0400AD9C RID: 44444
			public int itemId;

			// Token: 0x0400AD9D RID: 44445
			public int num;
		}
	}
}
