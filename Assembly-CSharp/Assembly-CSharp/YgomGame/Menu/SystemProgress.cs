using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem;
using YgomSystem.ElementSystem;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.Menu
{
	// Token: 0x02000AF6 RID: 2806
	public class SystemProgress : MonoBehaviour
	{
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06005191 RID: 20881 RVA: 0x0000216A File Offset: 0x0000036A
		[Obsolete]
		public static SystemProgress Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06005192 RID: 20882 RVA: 0x000F4BF4 File Offset: 0x000F2DF4
		public Color fadeColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0000216A File Offset: 0x0000036A
		public static string get_driverURL()
		{
			return null;
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFatalErrorReboot()
		{
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnAbortRebootRequest(string bootpage, AppInfo.BootType boottype = AppInfo.BootType.ExitReboot)
		{
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnAbortReboot(string bootpage, AppInfo.BootType boottype)
		{
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDirty()
		{
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetFadeAlpha()
		{
			return 0f;
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsNeedChangeFadeAlpha(Color fadeColor)
		{
			return false;
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x0000216D File Offset: 0x0000036D
		public void DispProgress(SystemProgress.ProgressType type, Color fadeColor, float delay = 0f, bool immediate = false, ResourceManager.UnloadCheckLevel unloadCheckLevel = ResourceManager.UnloadCheckLevel.Low)
		{
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ShowNormalProgress(bool isShow)
		{
			return null;
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x0000216A File Offset: 0x0000036A
		private static string RandomSelectTipsNumberString(Dictionary<string, object> tipsSettingDict)
		{
			return null;
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadTipsSetting()
		{
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideProgress()
		{
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x000029CC File Offset: 0x00000BCC
		public ViewControllerManager.FadeState GetFadeState()
		{
			return ViewControllerManager.FadeState.None;
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetNormalProgress()
		{
			return null;
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnReboot()
		{
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x0000216D File Offset: 0x0000036D
		[Obsolete]
		public void OpenFatalErrorDialog(string message, string label1 = null, Action action1 = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x0000216D File Offset: 0x0000036D
		[Obsolete]
		public static void Disp(SystemProgress.ProgressType type, Color fadeColor, float delay = 0f, bool immediate = false)
		{
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x0000216D File Offset: 0x0000036D
		[Obsolete]
		public static void Hide()
		{
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x0000216D File Offset: 0x0000036D
		[Obsolete]
		public static void OpenNetworkErrorDialog(Handle handle)
		{
		}

		// Token: 0x04008FF0 RID: 36848
		public CanvasGroup fillScreen;

		// Token: 0x04008FF1 RID: 36849
		public Image fadeScreen;

		// Token: 0x04008FF2 RID: 36850
		public GameObject normalProgress;

		// Token: 0x04008FF3 RID: 36851
		public GameObject tipsProgress;

		// Token: 0x04008FF4 RID: 36852
		public GameObject connectingProgress;

		// Token: 0x04008FF5 RID: 36853
		public ElementObjectManager fatalErrorDialogPref;

		// Token: 0x04008FF6 RID: 36854
		public float delayTime;

		// Token: 0x04008FF7 RID: 36855
		public float guaranteeTime;

		// Token: 0x04008FF8 RID: 36856
		public float fadeSpeed;

		// Token: 0x04008FF9 RID: 36857
		public float fadeAlpha;

		// Token: 0x04008FFA RID: 36858
		private int count;

		// Token: 0x04008FFB RID: 36859
		private float crntTime;

		// Token: 0x04008FFC RID: 36860
		private bool rebooting;

		// Token: 0x04008FFD RID: 36861
		private bool unloadCalled;

		// Token: 0x04008FFE RID: 36862
		private ResourceManager.UnloadCheckLevel unloadCheckLevel;

		// Token: 0x04008FFF RID: 36863
		private string abortRequest;

		// Token: 0x04009000 RID: 36864
		private AppInfo.BootType abortRequestType;

		// Token: 0x04009001 RID: 36865
		private SystemProgress.ProgressType crntType;

		// Token: 0x04009002 RID: 36866
		private bool crntScreenCenter;

		// Token: 0x04009003 RID: 36867
		private Dictionary<string, object> tipsSettingDict;

		// Token: 0x04009004 RID: 36868
		private Image centerFadeScreen;

		// Token: 0x04009005 RID: 36869
		public bool CenterFade;

		// Token: 0x02000AF7 RID: 2807
		public enum ProgressType
		{
			// Token: 0x04009007 RID: 36871
			None,
			// Token: 0x04009008 RID: 36872
			Normal,
			// Token: 0x04009009 RID: 36873
			Tips,
			// Token: 0x0400900A RID: 36874
			Blank
		}
	}
}
