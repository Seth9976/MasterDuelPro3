using System;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A4F RID: 2639
	public abstract class CommonScreenViewController : BaseMenuViewController
	{
		// Token: 0x06004D02 RID: 19714 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTitleText(string text)
		{
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDecisionText(string text)
		{
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDecisionCallback(Action callback)
		{
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetBackCallback(Action callback)
		{
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetEnableDecisionButton(bool enable)
		{
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ShowDecisionButton(bool enable)
		{
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ShowBackButton(bool enable)
		{
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x04008AB8 RID: 35512
		private const string DecisionButtonLabel = "ButtonNext";

		// Token: 0x04008AB9 RID: 35513
		private const string BackButtonLabel = "ButtonBack";

		// Token: 0x04008ABA RID: 35514
		private SelectionButton m_decisionButton;

		// Token: 0x04008ABB RID: 35515
		private SelectionButton m_backButton;

		// Token: 0x04008ABC RID: 35516
		private Action m_decisionCallback;

		// Token: 0x04008ABD RID: 35517
		private Action m_backCallback;

		// Token: 0x04008ABE RID: 35518
		private MDText m_decisionButtonText;
	}
}
