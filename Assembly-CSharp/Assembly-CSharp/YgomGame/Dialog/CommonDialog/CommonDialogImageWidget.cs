using System;
using YgomSystem.ElementSystem;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F6B RID: 3947
	public class CommonDialogImageWidget : ContentWidgetBase<CommonDialogImageWidget, EntryImageData>, IContentWidgetAsyncLoader
	{
		// Token: 0x0600742C RID: 29740 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogImageWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x0600742D RID: 29741 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600742E RID: 29742 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryImageData entryData)
		{
		}

		// Token: 0x0600742F RID: 29743 RVA: 0x0000216D File Offset: 0x0000036D
		public void AsyncBinding(IEntryData entryData, Action onComplete)
		{
		}

		// Token: 0x06007430 RID: 29744 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshLayoutElement()
		{
		}

		// Token: 0x0400AD44 RID: 44356
		private Action m_OnCompleteCallback;
	}
}
