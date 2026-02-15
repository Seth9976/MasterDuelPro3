using System;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AB6 RID: 2742
	public class PreHomeViewController : ViewController
	{
		// Token: 0x06004FE7 RID: 20455 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float Progress()
		{
			return 0f;
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ProgressUpdate()
		{
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsWaitFinish()
		{
			return false;
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x04008DD5 RID: 36309
		public const string PREFAB_PATH = "Home/PreHome";

		// Token: 0x04008DD6 RID: 36310
		private PreHomeViewController.Step step;

		// Token: 0x04008DD7 RID: 36311
		private bool m_soundLoadFinish;

		// Token: 0x04008DD8 RID: 36312
		private bool m_shouldDownload;

		// Token: 0x02000AB7 RID: 2743
		private enum Step
		{
			// Token: 0x04008DDA RID: 36314
			WaitDownload,
			// Token: 0x04008DDB RID: 36315
			Idle,
			// Token: 0x04008DDC RID: 36316
			Wait1,
			// Token: 0x04008DDD RID: 36317
			Wait2,
			// Token: 0x04008DDE RID: 36318
			Wait3,
			// Token: 0x04008DDF RID: 36319
			GoHome
		}
	}
}
