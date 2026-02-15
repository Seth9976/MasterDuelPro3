using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MDPro3.Net;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;

namespace MDPro3.Servant
{
	// Token: 0x020012DB RID: 4827
	public class MainMenu : Servant
	{
		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06008D0B RID: 36107 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06008D0C RID: 36108 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06008D0D RID: 36109 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool NeedExitButton
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008D0E RID: 36110 RVA: 0x00128B66 File Offset: 0x00126D66
		public override void Initialize()
		{
			base.Initialize();
			this.showing = true;
			UIManager.HideExitButton(0f, Ease.Linear);
			UIManager.HideLine(0f);
			Program.instance.currentServant = this;
			Program.instance.depth = 0;
			base.LoadUI();
		}

		// Token: 0x06008D0F RID: 36111 RVA: 0x00128BA8 File Offset: 0x00126DA8
		public override void OnExit()
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

		// Token: 0x06008D10 RID: 36112 RVA: 0x00128C23 File Offset: 0x00126E23
		protected override void FirstLoadEvent()
		{
			base.FirstLoadEvent();
			this.servantUI.ResetUI();
			base.StartCoroutine(this.LoadMyCardNewsAsync());
			Program.instance.ReadParams();
		}

		// Token: 0x06008D11 RID: 36113 RVA: 0x00128C4D File Offset: 0x00126E4D
		protected override void ApplyShowArrangement(int preDepth)
		{
			base.ApplyShowArrangement(preDepth);
			UIManager.ShowWallpaper(this.TransitionTime);
		}

		// Token: 0x06008D12 RID: 36114 RVA: 0x00128C61 File Offset: 0x00126E61
		protected override void ApplyHideArrangement(int preDepth)
		{
			base.ApplyHideArrangement(preDepth);
			UIManager.HideWallpaper(this.TransitionTime);
		}

		// Token: 0x06008D13 RID: 36115 RVA: 0x00128C78 File Offset: 0x00126E78
		public override void PerFrameFunction()
		{
			if (this.NeedResponseInput())
			{
				if (UserInput.WasCancelPressed)
				{
					this.OnReturn();
				}
				if (this.GetUI<MainMenuUI>().News.showing)
				{
					if (UserInput.WasLeftPressed)
					{
						this.GetUI<MainMenuUI>().News.OnLeft(0.1f);
					}
					else if (UserInput.WasRightPressed)
					{
						this.GetUI<MainMenuUI>().News.OnRight(0.1f);
					}
					if (UserInput.WasGamepadButtonWestPressed)
					{
						this.GetUI<MainMenuUI>().News.OnNewsClick();
					}
					if (UserInput.WasGamepadButtonNorthPressed)
					{
						this.GetUI<MainMenuUI>().News.OnClose();
					}
				}
			}
		}

		// Token: 0x06008D14 RID: 36116 RVA: 0x00128D17 File Offset: 0x00126F17
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (this.lastSelectedButton != null)
			{
				this.lastSelectedButton.GetSelectable().Select();
				return;
			}
			this.GetUI<MainMenuUI>().SelectDefaultSelectable();
		}

		// Token: 0x06008D15 RID: 36117 RVA: 0x00128D4E File Offset: 0x00126F4E
		private IEnumerator CheckUpdateAsync()
		{
			return new MainMenu.<CheckUpdateAsync>d__14(0);
		}

		// Token: 0x06008D16 RID: 36118 RVA: 0x00128D56 File Offset: 0x00126F56
		private IEnumerator LoadMyCardNewsAsync()
		{
			while (OnlineService.myCardNews == null)
			{
				yield return null;
			}
			MyCardNews news = OnlineService.myCardNews;
			this.GetUI<MainMenuUI>().News.news = news;
			this.GetUI<MainMenuUI>().News.LoadNews();
			yield break;
		}

		// Token: 0x0400CB19 RID: 51993
		[HideInInspector]
		public SelectionButton_MainMenu lastSelectedButton;
	}
}
