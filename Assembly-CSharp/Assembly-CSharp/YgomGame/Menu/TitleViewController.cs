using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AFC RID: 2812
	public class TitleViewController : BaseMenuViewController, IFadeSupported
	{
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060051BF RID: 20927 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060051C0 RID: 20928 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isEnableAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060051C1 RID: 20929 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool enablePlatformIcon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060051C2 RID: 20930 RVA: 0x0000216A File Offset: 0x0000036A
		public static TitleViewController Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x0000216D File Offset: 0x0000036D
		private void clickAction(Action action)
		{
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool isMobile()
		{
			return false;
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickStart()
		{
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSettingMenu()
		{
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickDataLink()
		{
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCompany()
		{
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBackKey()
		{
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x0000216D File Offset: 0x0000036D
		private static void openMainteDialog()
		{
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x000F4C0C File Offset: 0x000F2E0C
		public Color FadeColor(ViewController.TransitionType type)
		{
			return default(Color);
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x000029CC File Offset: 0x00000BCC
		public SystemProgress.ProgressType FadeType(ViewController.TransitionType type)
		{
			return SystemProgress.ProgressType.None;
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepTitle(StepSequencer seq)
		{
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepDemoStart(StepSequencer seq)
		{
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepGameStart(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x0000216D File Offset: 0x0000036D
		private void PrepareLayout(TitleViewController.LayoutType type)
		{
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool SetTitleBGMonster()
		{
			return false;
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x0000216D File Offset: 0x0000036D
		private void PrepareOverlayBG()
		{
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickPlatformIcon()
		{
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenDataLink()
		{
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartGame()
		{
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetDemoStartTime()
		{
			return 0f;
		}

		// Token: 0x04009021 RID: 36897
		private StepSequencer m_sequencer;

		// Token: 0x04009022 RID: 36898
		private bool m_autoStart;

		// Token: 0x04009023 RID: 36899
		private float m_autoStartWaitTimer;

		// Token: 0x04009024 RID: 36900
		private readonly string BTN_START_LABEL;

		// Token: 0x04009025 RID: 36901
		private readonly string BTN_SETTING_LABEL;

		// Token: 0x04009026 RID: 36902
		private readonly string BTN_DATALINK_LABEL;

		// Token: 0x04009027 RID: 36903
		private readonly string BTN_KONAMI_LABEL;

		// Token: 0x04009028 RID: 36904
		private readonly string BTN_BACKKEYSHORTCUT_LABEL;

		// Token: 0x04009029 RID: 36905
		private readonly string MONSTER_WALLPAPER_LABEL;

		// Token: 0x0400902A RID: 36906
		private readonly string ICON_PLATFORM_LABEL;

		// Token: 0x0400902B RID: 36907
		private readonly string ICON_START_LABEL;

		// Token: 0x0400902C RID: 36908
		private readonly string TITLELOGO_LABEL;

		// Token: 0x0400902D RID: 36909
		private readonly string PRESSMSGTEXT_LABEL;

		// Token: 0x0400902E RID: 36910
		private readonly string CODEVER_LABEL;

		// Token: 0x0400902F RID: 36911
		private readonly string PLAYERID_LABEL;

		// Token: 0x04009030 RID: 36912
		private readonly string TEXT_GAMESTART_LABEL;

		// Token: 0x04009031 RID: 36913
		private GameObject m_currentWallpaperMonster;

		// Token: 0x04009032 RID: 36914
		private TitleOverlayBGController m_overlayBG;

		// Token: 0x04009033 RID: 36915
		private PlayableDirector titleLogoDirector;

		// Token: 0x04009034 RID: 36916
		[SerializeField]
		private PlayableAsset beginTimeline;

		// Token: 0x04009035 RID: 36917
		[SerializeField]
		private PlayableAsset fadeInTimeline;

		// Token: 0x04009036 RID: 36918
		[SerializeField]
		private PlayableAsset fadeOutTimeline;

		// Token: 0x04009037 RID: 36919
		private bool m_demoDataReady;

		// Token: 0x04009038 RID: 36920
		private const float DefaultDemoStartTime = 45f;

		// Token: 0x04009039 RID: 36921
		private float m_demoTimer;

		// Token: 0x0400903A RID: 36922
		private bool m_demoTimerActive;

		// Token: 0x0400903B RID: 36923
		private TitleViewController.LayoutType m_layout;

		// Token: 0x0400903C RID: 36924
		private static TitleViewController s_instance;

		// Token: 0x02000AFD RID: 2813
		private enum Step
		{
			// Token: 0x0400903E RID: 36926
			Title,
			// Token: 0x0400903F RID: 36927
			DemoStart,
			// Token: 0x04009040 RID: 36928
			GameStart
		}

		// Token: 0x02000AFE RID: 2814
		private enum LayoutType
		{
			// Token: 0x04009042 RID: 36930
			None,
			// Token: 0x04009043 RID: 36931
			LogoOnly,
			// Token: 0x04009044 RID: 36932
			LogoAndMonster,
			// Token: 0x04009045 RID: 36933
			AutoStart
		}
	}
}
