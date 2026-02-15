using System;
using TMPro;
using YgomSystem.ElementSystem;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F70 RID: 3952
	public class CommonDialogMaintenanceImageWidget : ContentWidgetBase<CommonDialogMaintenanceImageWidget, EntryMaintenanceImageData>
	{
		// Token: 0x06007451 RID: 29777 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogMaintenanceImageWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007452 RID: 29778 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06007453 RID: 29779 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryMaintenanceImageData entryData)
		{
		}

		// Token: 0x06007454 RID: 29780 RVA: 0x0000216D File Offset: 0x0000036D
		public void AsyncBinding(IEntryData entryData, Action onComplete)
		{
		}

		// Token: 0x0400AD68 RID: 44392
		private TMP_Text m_TextTitle;

		// Token: 0x0400AD69 RID: 44393
		private TMP_Text m_TextDate;

		// Token: 0x0400AD6A RID: 44394
		private Action m_OnCompleteCallback;
	}
}
