using System;
using System.Collections.Generic;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AB5 RID: 2741
	public class PlaceHolderDialogViewController : BaseMenuViewController, IBokeSupported
	{
		// Token: 0x06004FE0 RID: 20448 RVA: 0x0000216A File Offset: 0x0000036A
		public static PlaceHolderDialogViewController PushOpen(ViewController owner, Dictionary<string, object> args = null)
		{
			return null;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnContentTransition(ViewController.TransitionType transitionType, ViewController hideVc, ViewController showVc)
		{
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x04008DD4 RID: 36308
		private ViewController m_Owner;
	}
}
