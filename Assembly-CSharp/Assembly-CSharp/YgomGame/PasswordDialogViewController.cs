using System;
using YgomGame.Menu;
using YgomSystem.UI.ElementWidget;

namespace YgomGame
{
	// Token: 0x020007CF RID: 1999
	public class PasswordDialogViewController : DialogViewControllerBase
	{
		// Token: 0x06003E62 RID: 15970 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x04003747 RID: 14151
		public static readonly string argKeyCallback;

		// Token: 0x04003748 RID: 14152
		private readonly string INPUT_LABEL;

		// Token: 0x04003749 RID: 14153
		private readonly string BTN_OK_LABEL;

		// Token: 0x0400374A RID: 14154
		private readonly string BTN_CANCEL_LABEL;

		// Token: 0x0400374B RID: 14155
		private InputFieldWidget m_inputFieldWidget;

		// Token: 0x0400374C RID: 14156
		private string m_inputText;

		// Token: 0x0400374D RID: 14157
		private Action<string> m_callback;
	}
}
