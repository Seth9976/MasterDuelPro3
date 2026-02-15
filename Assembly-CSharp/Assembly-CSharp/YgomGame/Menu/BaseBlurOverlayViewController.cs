using System;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A4B RID: 2635
	public class BaseBlurOverlayViewController : BaseMenuViewController
	{
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06004CC2 RID: 19650 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool defaultBlurOverlay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06004CC3 RID: 19651 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GameObject m_TweenTarget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04008A99 RID: 35481
		public const string k_ArgKeyBlurOverlay = "blurOverlay";

		// Token: 0x04008A9A RID: 35482
		protected bool m_IsBlurOverlay;
	}
}
