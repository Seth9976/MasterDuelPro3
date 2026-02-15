using System;
using YgomGame.Dialog.CommonDialog;
using YgomSystem.ElementSystem;

namespace YgomGame.Shop
{
	// Token: 0x02000922 RID: 2338
	public class ConfirmRegDialogProductWidget : ContentWidgetBase<ConfirmRegDialogProductWidget, EntryInsertWidgetData>
	{
		// Token: 0x06004427 RID: 17447 RVA: 0x0000216A File Offset: 0x0000036A
		public static ConfirmRegDialogProductWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryInsertWidgetData entryData)
		{
		}

		// Token: 0x040082E9 RID: 33513
		public string headerText;

		// Token: 0x040082EA RID: 33514
		public string hasText;

		// Token: 0x040082EB RID: 33515
		public string numText;
	}
}
