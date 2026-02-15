using System;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame.Menu
{
	// Token: 0x02000AF9 RID: 2809
	public class TitleDataLinkDialogViewController : BaseMenuViewController, IBokeSupported
	{
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060051B2 RID: 20914 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBsck()
		{
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x04009016 RID: 36886
		private readonly string content_LABEL;

		// Token: 0x04009017 RID: 36887
		private readonly string button1_LABEL;

		// Token: 0x04009018 RID: 36888
		private readonly string button0_LABEL;

		// Token: 0x04009019 RID: 36889
		private readonly string title_LABEL;

		// Token: 0x0400901A RID: 36890
		private ElementObject content;

		// Token: 0x0400901B RID: 36891
		private ElementObject buttonGrp0;

		// Token: 0x0400901C RID: 36892
		private ElementObject buttonGrp1;

		// Token: 0x0400901D RID: 36893
		private ElementObject titleGrp;

		// Token: 0x0400901E RID: 36894
		private ExtendedTextMeshProUGUI MainText;
	}
}
