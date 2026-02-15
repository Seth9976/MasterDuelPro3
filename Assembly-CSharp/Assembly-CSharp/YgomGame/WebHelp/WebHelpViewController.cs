using System;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.WebHelp
{
	// Token: 0x020007EC RID: 2028
	public class WebHelpViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x06003EF7 RID: 16119 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open()
		{
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetUpView()
		{
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x0000216D File Offset: 0x0000036D
		private void JumpToLink()
		{
		}

		// Token: 0x040037EB RID: 14315
		private readonly string TEXT_TOP_LABEL;

		// Token: 0x040037EC RID: 14316
		private readonly string TEXT_DESC1_LABEL;

		// Token: 0x040037ED RID: 14317
		private readonly string TEXT_DESC2_LABEL;

		// Token: 0x040037EE RID: 14318
		private readonly string TEXT_BUTTON_LABEL;

		// Token: 0x040037EF RID: 14319
		private readonly string BUTTON_LABEL;

		// Token: 0x040037F0 RID: 14320
		private ExtendedTextMeshProUGUI m_topText;

		// Token: 0x040037F1 RID: 14321
		private ExtendedTextMeshProUGUI m_desc1Text;

		// Token: 0x040037F2 RID: 14322
		private ExtendedTextMeshProUGUI m_desc2Text;

		// Token: 0x040037F3 RID: 14323
		private ExtendedTextMeshProUGUI m_urlText;

		// Token: 0x040037F4 RID: 14324
		private ExtendedTextMeshProUGUI m_buttonText;

		// Token: 0x040037F5 RID: 14325
		private SelectionButton m_helpButton;

		// Token: 0x040037F6 RID: 14326
		private readonly string TEXT_URL_LABEL;

		// Token: 0x040037F7 RID: 14327
		private string m_url;
	}
}
