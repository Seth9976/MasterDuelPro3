using System;
using UnityEngine;
using YgomGame.Menu;

namespace YgomGame.WCS.Portal
{
	// Token: 0x02000818 RID: 2072
	public class WatchMenuViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x06004015 RID: 16405 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x0000216D File Offset: 0x0000036D
		private void setupWatchSection(GameObject root, string url, string titleText, string buttonText)
		{
		}

		// Token: 0x0400392D RID: 14637
		private const string LABEL_BTN_JUMP = "ButtonJump";

		// Token: 0x0400392E RID: 14638
		private bool m_isPC;

		// Token: 0x0400392F RID: 14639
		private bool m_isMobile;

		// Token: 0x04003930 RID: 14640
		private bool m_isIOS;
	}
}
