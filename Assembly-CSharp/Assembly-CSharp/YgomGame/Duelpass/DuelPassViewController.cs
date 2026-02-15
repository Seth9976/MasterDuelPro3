using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C36 RID: 3126
	public class DuelPassViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x0600591C RID: 22812 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600591E RID: 22814 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelpassRecommendItemWidget recommendItemView
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateView()
		{
		}

		// Token: 0x04009501 RID: 38145
		private DuelpassProgressBarWidget progressBar;

		// Token: 0x04009502 RID: 38146
		private DuelpassRewardPanelWidget rewardPanel;

		// Token: 0x04009503 RID: 38147
		private DuelpassBulkRecieveButtonWidget bulkRecieveButton;

		// Token: 0x04009504 RID: 38148
		private DuelpassPeriodDateWidget dateView;

		// Token: 0x04009505 RID: 38149
		private GameObject goldFog;

		// Token: 0x04009506 RID: 38150
		private GameObject goldFirefly;

		// Token: 0x04009507 RID: 38151
		private GameObject normalFirefly;

		// Token: 0x04009508 RID: 38152
		private TMP_Text textMessage;

		// Token: 0x04009509 RID: 38153
		private SelectionButton toShopButton;

		// Token: 0x0400950A RID: 38154
		private SelectionButton whatDuelpassButton;

		// Token: 0x0400950B RID: 38155
		private bool isFirst;
	}
}
