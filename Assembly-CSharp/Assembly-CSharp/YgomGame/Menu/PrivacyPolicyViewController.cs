using System;
using System.Collections;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000ABD RID: 2749
	public class PrivacyPolicyViewController : CommonScreenViewController
	{
		// Token: 0x06005000 RID: 20480 RVA: 0x0000216D File Offset: 0x0000036D
		private void setupButton(string element, string text, Action callback)
		{
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0000216D File Offset: 0x0000036D
		private void closeButton(string element)
		{
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator openGDPRPrivacyNotice(string url)
		{
			return null;
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0000216D File Offset: 0x0000036D
		private static void debugLog(string str)
		{
		}

		// Token: 0x04008DF4 RID: 36340
		private UserAgreementType m_type;

		// Token: 0x04008DF5 RID: 36341
		private SelectionButton m_agreeButton;

		// Token: 0x04008DF6 RID: 36342
		private IEnumerator m_openGDPRPrivacyNotice;

		// Token: 0x04008DF7 RID: 36343
		private Action<int> m_resultCallback;
	}
}
