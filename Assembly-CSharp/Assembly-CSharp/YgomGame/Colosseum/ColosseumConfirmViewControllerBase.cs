using System;
using YgomGame.Menu;

namespace YgomGame.Colosseum
{
	// Token: 0x02001026 RID: 4134
	public abstract class ColosseumConfirmViewControllerBase : BaseMenuViewController, IDynamicHeaderCustomSupported, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x06007C21 RID: 31777
		protected abstract void UpdateView();

		// Token: 0x06007C22 RID: 31778 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007C23 RID: 31779 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007C24 RID: 31780 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateLogo()
		{
		}

		// Token: 0x06007C25 RID: 31781 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06007C26 RID: 31782 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckText()
		{
		}

		// Token: 0x06007C27 RID: 31783 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckSetActiveText(string label)
		{
		}

		// Token: 0x06007C28 RID: 31784 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x0400B3E9 RID: 46057
		protected const string E_Image = "Image";

		// Token: 0x0400B3EA RID: 46058
		protected const string E_TextTitle = "TextTitle";

		// Token: 0x0400B3EB RID: 46059
		protected const string E_TextExplain = "TextExplain";

		// Token: 0x0400B3EC RID: 46060
		protected const string E_TextAlert = "TextAlert";

		// Token: 0x0400B3ED RID: 46061
		protected const string E_DescGroup = "DescGroup";

		// Token: 0x0400B3EE RID: 46062
		protected const string E_DescScrollRect = "DescScrollRect";

		// Token: 0x0400B3EF RID: 46063
		protected const string E_DescText = "DescText";

		// Token: 0x0400B3F0 RID: 46064
		protected const string E_TemplateButtonInfo = "TemplateButtonInfo";

		// Token: 0x0400B3F1 RID: 46065
		protected const string E_AgreementGroup = "AgreementGroup";

		// Token: 0x0400B3F2 RID: 46066
		protected const string E_TemplateButtonGroup = "TemplateButtonGroup";

		// Token: 0x0400B3F3 RID: 46067
		protected const string E_ConfirmGroup = "ConfirmGroup";

		// Token: 0x0400B3F4 RID: 46068
		protected const string E_TemplateButtonBottom = "TemplateButtonBottom";

		// Token: 0x0400B3F5 RID: 46069
		protected const string E_Button = "Button";

		// Token: 0x0400B3F6 RID: 46070
		protected const string E_Text = "Text";

		// Token: 0x0400B3F7 RID: 46071
		protected const string E_TextSelecting = "TextSelecting";

		// Token: 0x0400B3F8 RID: 46072
		protected const string ARGKEY_LOGO = "logoId";

		// Token: 0x0400B3F9 RID: 46073
		protected const string ARGKEY_ID = "identifier";

		// Token: 0x0400B3FA RID: 46074
		protected const string ARGKEY_ONSUCCESS = "onSuccess";

		// Token: 0x0400B3FB RID: 46075
		protected int logoId;

		// Token: 0x0400B3FC RID: 46076
		protected int identifier;

		// Token: 0x0400B3FD RID: 46077
		protected Action onSuccess;
	}
}
