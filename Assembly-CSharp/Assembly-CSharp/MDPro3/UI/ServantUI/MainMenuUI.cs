using System;
using System.Collections.Generic;
using MDPro3.Servant;
using UnityEngine;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x0200146D RID: 5229
	public class MainMenuUI : ServantUI
	{
		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x060097ED RID: 38893 RVA: 0x00164A38 File Offset: 0x00162C38
		public NewsManager News
		{
			get
			{
				return this.m_News = ((this.m_News != null) ? this.m_News : base.Manager.GetElement<NewsManager>("News"));
			}
		}

		// Token: 0x060097EE RID: 38894 RVA: 0x00164A74 File Offset: 0x00162C74
		public override void Initialize(Servant servant)
		{
			base.Initialize(servant);
			base.Title.text = "MDPro3 v" + Application.version;
		}

		// Token: 0x060097EF RID: 38895 RVA: 0x00164A97 File Offset: 0x00162C97
		public void OnSolo()
		{
			if (Program.exitOnReturn)
			{
				return;
			}
			Program.instance.solo.SwitchCondition(SoloSelector.Condition.ForSolo);
			Program.instance.ShiftToServant(Program.instance.solo);
		}

		// Token: 0x060097F0 RID: 38896 RVA: 0x00164AC5 File Offset: 0x00162CC5
		public void OnOnline()
		{
			if (Program.exitOnReturn)
			{
				return;
			}
			Program.instance.ShiftToServant(Program.instance.online);
		}

		// Token: 0x060097F1 RID: 38897 RVA: 0x00164AE3 File Offset: 0x00162CE3
		public void OnPuzzle()
		{
			if (Program.exitOnReturn)
			{
				return;
			}
			Program.instance.ShiftToServant(Program.instance.puzzle);
		}

		// Token: 0x060097F2 RID: 38898 RVA: 0x00164B01 File Offset: 0x00162D01
		public void OnReplay()
		{
			if (Program.exitOnReturn)
			{
				return;
			}
			Program.instance.ShiftToServant(Program.instance.replay);
		}

		// Token: 0x060097F3 RID: 38899 RVA: 0x00164B1F File Offset: 0x00162D1F
		public void OnCutin()
		{
			if (Program.exitOnReturn)
			{
				return;
			}
			Program.instance.ShiftToServant(Program.instance.cutin);
		}

		// Token: 0x060097F4 RID: 38900 RVA: 0x00164B3D File Offset: 0x00162D3D
		public void OnMate()
		{
			if (Program.exitOnReturn)
			{
				return;
			}
			Program.instance.ShiftToServant(Program.instance.mate);
		}

		// Token: 0x060097F5 RID: 38901 RVA: 0x00164B5B File Offset: 0x00162D5B
		public void OnDeck()
		{
			if (Program.exitOnReturn)
			{
				return;
			}
			Program.instance.deckSelector.SwitchCondition(DeckSelector.Condition.ForEdit);
			Program.instance.ShiftToServant(Program.instance.deckSelector);
		}

		// Token: 0x060097F6 RID: 38902 RVA: 0x00164B89 File Offset: 0x00162D89
		public void OnSetting()
		{
			if (Program.exitOnReturn)
			{
				return;
			}
			Program.instance.ShiftToServant(Program.instance.setting);
		}

		// Token: 0x060097F7 RID: 38903 RVA: 0x00164BA8 File Offset: 0x00162DA8
		public void OnExit()
		{
			if (Program.exitOnReturn)
			{
				return;
			}
			UIManager.ShowPopupYesOrNo(new List<string>
			{
				InterString.Get("确认退出", 0),
				InterString.Get("即将退出应用程序，@n是否确认？", 0),
				InterString.Get("确认", 0),
				InterString.Get("取消", 0),
				"1"
			}, new Action(Program.GameQuit), null);
		}

		// Token: 0x0400D613 RID: 54803
		private const string LABEL_MYCARD_NEWS = "News";

		// Token: 0x0400D614 RID: 54804
		private NewsManager m_News;
	}
}
