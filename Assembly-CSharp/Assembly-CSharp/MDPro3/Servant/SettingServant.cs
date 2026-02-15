using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MDPro3.Net;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace MDPro3.Servant
{
	// Token: 0x02001304 RID: 4868
	public class SettingServant : Servant
	{
		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06008E8A RID: 36490 RVA: 0x0000763C File Offset: 0x0000583C
		public override int Depth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06008E8B RID: 36491 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06008E8C RID: 36492 RVA: 0x0012843E File Offset: 0x0012663E
		protected override float BlackAlpha
		{
			get
			{
				return 0.6f;
			}
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06008E8D RID: 36493 RVA: 0x00124D11 File Offset: 0x00122F11
		protected override float SubBlackAlpha
		{
			get
			{
				return 0.9f;
			}
		}

		// Token: 0x06008E8E RID: 36494 RVA: 0x00130D31 File Offset: 0x0012EF31
		public override void Initialize()
		{
			this.returnServant = Program.instance.menu;
			base.Initialize();
			this.InitializeSettings();
		}

		// Token: 0x06008E8F RID: 36495 RVA: 0x00130D50 File Offset: 0x0012EF50
		protected override void ApplyShowArrangement(int preDepth)
		{
			base.ApplyShowArrangement(preDepth);
			this.RefreshCharacterName();
			if (preDepth <= this.Depth)
			{
				this.servantUI.SelectDefaultSelectable();
			}
			if (Program.instance.currentServant == Program.instance.ocgcore)
			{
				Program.instance.currentSubServant = this;
				UIManager.ShowFPSRight();
			}
		}

		// Token: 0x06008E90 RID: 36496 RVA: 0x00130DAC File Offset: 0x0012EFAC
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (!(this.lastSelectable != null))
			{
				this.servantUI.SelectDefaultSelectable();
				return;
			}
			SelectionButton_Setting selectionButton_Setting;
			if (this.lastSelectable.TryGetComponent<SelectionButton_Setting>(out selectionButton_Setting))
			{
				EventSystem.current.SetSelectedGameObject(this.lastSelectable.gameObject);
				return;
			}
			SelectionToggle_Setting selectionToggle_Setting;
			if (this.lastSelectable.TryGetComponent<SelectionToggle_Setting>(out selectionToggle_Setting))
			{
				EventSystem.current.SetSelectedGameObject(this.lastSelectable.gameObject);
				return;
			}
			this.servantUI.SelectDefaultSelectable();
		}

		// Token: 0x06008E91 RID: 36497 RVA: 0x00130E34 File Offset: 0x0012F034
		public override void OnReturn()
		{
			if (this.inTransition)
			{
				return;
			}
			if (this.returnAction != null)
			{
				this.returnAction();
				return;
			}
			AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
			GameObject selected = EventSystem.current.currentSelectedGameObject;
			if (selected == null)
			{
				this.OnExit();
				return;
			}
			if (Cursor.lockState == CursorLockMode.None)
			{
				this.OnExit();
				return;
			}
			SelectionButton_Setting selectionButton_Setting;
			if (!selected.TryGetComponent<SelectionButton_Setting>(out selectionButton_Setting))
			{
				this.OnExit();
				return;
			}
			if (this.lastSelectedToggle != null)
			{
				this.lastSelectedToggle.GetSelectable().Select();
				return;
			}
			this.servantUI.SelectDefaultSelectable();
		}

		// Token: 0x06008E92 RID: 36498 RVA: 0x00130ED4 File Offset: 0x0012F0D4
		private void InitializeSettings()
		{
			QualitySettings.vSyncCount = 0;
			AudioManager.SetBGMVol(Config.GetFloat("BgmVol", 0.7f));
			AudioManager.SetSeVol(Config.GetFloat("SEVol", 0.7f));
			AudioManager.SetVoiceVol(Config.GetFloat("VoiceVol", 0.7f));
			Program.instance.camera_.urpAsset.renderScale = SettingServantUI.GetScale();
			SettingServantUI.ChangeAAA(Config.GetFloat("AAA", 0f));
			SettingServantUI.ChangeShadow(Config.GetFloat("Shadow", 0f));
			SettingServant.SetFpsToConfig();
			SettingServantUI.ChangeShowFPS();
			Program.instance.background_.Change(int.Parse(Config.Get("Background", "0")));
		}

		// Token: 0x06008E93 RID: 36499 RVA: 0x00130F93 File Offset: 0x0012F193
		public static void SetFpsToConfig()
		{
			SettingServantUI.ChangeFPS((float)SettingServantUI.GetFPS());
		}

		// Token: 0x06008E94 RID: 36500 RVA: 0x00130FA1 File Offset: 0x0012F1A1
		public float GetBGMVolum()
		{
			if (this.servantUI == null)
			{
				return Config.GetFloat("BgmVol", 0.7f);
			}
			return this.GetUI<SettingServantUI>().GetBGMVolum();
		}

		// Token: 0x06008E95 RID: 36501 RVA: 0x00130FCC File Offset: 0x0012F1CC
		public void RefreshCharacterName()
		{
			if (this.servantUI == null)
			{
				return;
			}
			this.GetUI<SettingServantUI>().RefreshCharacterName();
		}

		// Token: 0x06008E96 RID: 36502 RVA: 0x00130FE8 File Offset: 0x0012F1E8
		public void UpdatePrerelease()
		{
			if (!this.checkingPrereleaseUpdate)
			{
				this.checkingPrereleaseUpdate = true;
				base.StartCoroutine(this.UpdatePrereleaseAsync());
			}
		}

		// Token: 0x06008E97 RID: 36503 RVA: 0x00131006 File Offset: 0x0012F206
		private IEnumerator UpdatePrereleaseAsync()
		{
			string filePath = Path.Combine("Expansions/", Path.GetFileName(Settings.Data.PrereleasePackUrl));
			if (!File.Exists(filePath) || Language.GetConfig() != Language.GetPrereleaseConfig())
			{
				Config.Set("Prerelease", "0");
				Config.Save();
			}
			UnityWebRequest www = UnityWebRequest.Get(Settings.GetPrereleasePackVersionUrl());
			www.SendWebRequest();
			while (!www.isDone)
			{
				yield return null;
				this.GetUI<SettingServantUI>().ButtonUpdatePrerelease.SetModeText(InterString.Get("检查更新中", 0));
			}
			if (www.result == UnityWebRequest.Result.Success)
			{
				string result = www.downloadHandler.text;
				string[] lines = result.Replace("\r", string.Empty).Split('\n', StringSplitOptions.None);
				if (Config.Get("Prerelease", "0") != lines[0])
				{
					if (!Directory.Exists("Expansions/"))
					{
						Directory.CreateDirectory("Expansions/");
					}
					UnityWebRequest download = UnityWebRequest.Get(Settings.GetPrereleasePackUrl());
					download.SendWebRequest();
					MessageManager.Cast(InterString.Get("正在更新，请耐心等待更待更新完成再进行其他操作。", 0));
					while (!download.isDone)
					{
						yield return null;
						this.GetUI<SettingServantUI>().ButtonUpdatePrerelease.SetModeText((download.downloadProgress * 100f).ToString("0.##") + "%");
					}
					if (download.result == UnityWebRequest.Result.Success)
					{
						File.WriteAllBytes(filePath, download.downloadHandler.data);
						MessageManager.Cast(InterString.Get("先行卡更新成功。", 0));
						Config.Set("Prerelease", lines[0]);
						Language.SetPrereleaseConfig(Language.GetConfig());
						Config.Save();
						Program.instance.InitializeForDataChange();
					}
					else
					{
						MessageManager.Cast(InterString.Get("先行卡更新失败。", 0) + download.error);
					}
					download = null;
				}
				else
				{
					MessageManager.Cast(InterString.Get("先行卡已是最新版。", 0));
				}
				lines = null;
			}
			else
			{
				MessageManager.Cast(InterString.Get("检查更新失败！", 0));
			}
			this.GetUI<SettingServantUI>().ButtonUpdatePrerelease.SetModeText(string.Empty);
			this.checkingPrereleaseUpdate = false;
			yield break;
		}

		// Token: 0x06008E98 RID: 36504 RVA: 0x00131018 File Offset: 0x0012F218
		public void DownloadYPK()
		{
			if (!this.downloadingYPK)
			{
				UIManager.ShowPopupInput(new List<string>
				{
					InterString.Get("下载卡包@n请输入卡包的下载地址", 0),
					string.Empty
				}, new Action<string>(this.DownloadYPK), null, TmpInputValidation.ValidationType.None);
				return;
			}
			MessageManager.Toast(InterString.Get("正在下载中，请稍后再试。", 0));
		}

		// Token: 0x06008E99 RID: 36505 RVA: 0x00131074 File Offset: 0x0012F274
		private void DownloadYPK(string url)
		{
			string[] supportedExts = new string[] { ".zip", ".ypk" };
			if (!NetUtil.IsValidDownloadUrl(url, supportedExts))
			{
				MessageManager.Toast(InterString.Get("无效的下载地址。", 0));
				return;
			}
			base.StartCoroutine(this.DownloadYpkAsync(url));
		}

		// Token: 0x06008E9A RID: 36506 RVA: 0x001310C0 File Offset: 0x0012F2C0
		private IEnumerator DownloadYpkAsync(string url)
		{
			SettingServant.<DownloadYpkAsync>d__25 <DownloadYpkAsync>d__ = new SettingServant.<DownloadYpkAsync>d__25(0);
			<DownloadYpkAsync>d__.<>4__this = this;
			<DownloadYpkAsync>d__.url = url;
			return <DownloadYpkAsync>d__;
		}

		// Token: 0x06008E9B RID: 36507 RVA: 0x001310D6 File Offset: 0x0012F2D6
		private void DownloadYpkUnsafe()
		{
			base.StartCoroutine(this.DownloadYpkUnsafeAsync(this.unsafeDownloadUrl));
		}

		// Token: 0x06008E9C RID: 36508 RVA: 0x001310EB File Offset: 0x0012F2EB
		private IEnumerator DownloadYpkUnsafeAsync(string url)
		{
			SettingServant.<DownloadYpkUnsafeAsync>d__27 <DownloadYpkUnsafeAsync>d__ = new SettingServant.<DownloadYpkUnsafeAsync>d__27(0);
			<DownloadYpkUnsafeAsync>d__.<>4__this = this;
			<DownloadYpkUnsafeAsync>d__.url = url;
			return <DownloadYpkUnsafeAsync>d__;
		}

		// Token: 0x0400CC59 RID: 52313
		[Header("Setting Servant")]
		[HideInInspector]
		public SelectionToggle_Setting lastSelectedToggle;

		// Token: 0x0400CC5A RID: 52314
		[HideInInspector]
		public SelectionButton_Setting lastSelectedButton;

		// Token: 0x0400CC5B RID: 52315
		private string unsafeDownloadUrl;

		// Token: 0x0400CC5C RID: 52316
		private bool checkingPrereleaseUpdate;

		// Token: 0x0400CC5D RID: 52317
		private bool downloadingYPK;
	}
}
