using System;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;

namespace MDPro3.Servant
{
	// Token: 0x020012FD RID: 4861
	public class ReplaySelector : Servant
	{
		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06008E31 RID: 36401 RVA: 0x0000763C File Offset: 0x0000583C
		public override int Depth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06008E32 RID: 36402 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008E33 RID: 36403 RVA: 0x0012F988 File Offset: 0x0012DB88
		public override void Initialize()
		{
			this.returnServant = Program.instance.menu;
			base.Initialize();
		}

		// Token: 0x06008E34 RID: 36404 RVA: 0x0012FA77 File Offset: 0x0012DC77
		protected override void ApplyShowArrangement(int preDepth)
		{
			base.ApplyShowArrangement(preDepth);
			this.GetUI<ReplaySelectorUI>().Print();
		}

		// Token: 0x06008E35 RID: 36405 RVA: 0x0012FA8B File Offset: 0x0012DC8B
		protected override void AfterHidingEvent()
		{
			base.AfterHidingEvent();
			this.GetUI<ReplaySelectorUI>().superScrollView.Clear();
		}

		// Token: 0x06008E36 RID: 36406 RVA: 0x001289B0 File Offset: 0x00126BB0
		public override void OnExit()
		{
			if (Program.exitOnReturn)
			{
				Program.GameQuit();
				return;
			}
			Program.instance.ShiftToServant(this.returnServant);
		}

		// Token: 0x06008E37 RID: 36407 RVA: 0x0012FAA3 File Offset: 0x0012DCA3
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			this.lastSelectedReplayItem.GetSelectable().Select();
		}

		// Token: 0x06008E38 RID: 36408 RVA: 0x00127555 File Offset: 0x00125755
		public void SelecLastReplayItem()
		{
			UserInput.NextSelectionIsAxis = true;
			this.Select(false);
		}

		// Token: 0x06008E39 RID: 36409 RVA: 0x0012FAC0 File Offset: 0x0012DCC0
		protected override void FirstLoadEvent()
		{
			base.FirstLoadEvent();
			if (Program.exitOnReturn)
			{
				this.GetUI<ReplaySelectorUI>().KF_Replay(this.replayName, false);
			}
		}

		// Token: 0x06008E3A RID: 36410 RVA: 0x0012FAE1 File Offset: 0x0012DCE1
		public void PlayReplay(string replayName)
		{
			if (this.servantUI == null)
			{
				this.replayName = replayName;
				Program.instance.ShiftToServant(this);
				return;
			}
			this.GetUI<ReplaySelectorUI>().KF_Replay(replayName, false);
		}

		// Token: 0x0400CC30 RID: 52272
		[HideInInspector]
		public SelectionToggle_Replay lastSelectedReplayItem;

		// Token: 0x0400CC31 RID: 52273
		private string replayName;
	}
}
