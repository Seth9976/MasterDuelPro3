using System;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Inquiry
{
	// Token: 0x02000BDB RID: 3035
	public class InquiryViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x06005674 RID: 22132 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open()
		{
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x0000216D File Offset: 0x0000036D
		private void JumpToLink()
		{
		}

		// Token: 0x06005678 RID: 22136 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetUpView()
		{
		}

		// Token: 0x0400935D RID: 37725
		private readonly string TEXT_TOP_LABEL;

		// Token: 0x0400935E RID: 37726
		private readonly string TEXT_DESC1_LABEL;

		// Token: 0x0400935F RID: 37727
		private readonly string TEXT_DESC2_LABEL;

		// Token: 0x04009360 RID: 37728
		private readonly string TEXT_BUTTON_LABEL;

		// Token: 0x04009361 RID: 37729
		private readonly string BUTTON_LABEL;

		// Token: 0x04009362 RID: 37730
		private ExtendedTextMeshProUGUI m_topText;

		// Token: 0x04009363 RID: 37731
		private ExtendedTextMeshProUGUI m_desc1Text;

		// Token: 0x04009364 RID: 37732
		private ExtendedTextMeshProUGUI m_desc2Text;

		// Token: 0x04009365 RID: 37733
		private ExtendedTextMeshProUGUI m_urlText;

		// Token: 0x04009366 RID: 37734
		private ExtendedTextMeshProUGUI m_buttonText;

		// Token: 0x04009367 RID: 37735
		private SelectionButton m_inquiryButton;

		// Token: 0x04009368 RID: 37736
		private readonly string TEXT_URL_LABEL;

		// Token: 0x04009369 RID: 37737
		private string m_url;
	}
}
