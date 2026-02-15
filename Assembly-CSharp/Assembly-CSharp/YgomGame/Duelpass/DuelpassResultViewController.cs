using System;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C42 RID: 3138
	public class DuelpassResultViewController : BaseMenuViewController
	{
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600597C RID: 22908 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResultView()
		{
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnStartProgressBarAnimation()
		{
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEndProgressBarAnimation()
		{
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnGradeUp(int grade)
		{
		}

		// Token: 0x0400953E RID: 38206
		private DuelpassResultProgressBarWidget resultProgressBar;

		// Token: 0x0400953F RID: 38207
		private DuelpassRewardPanelWidget rewardPanel;

		// Token: 0x04009540 RID: 38208
		private SelectionButton toDuelResultButton;

		// Token: 0x04009541 RID: 38209
		private GameObject goldFirefly;

		// Token: 0x04009542 RID: 38210
		private GameObject normalFirefly;
	}
}
