using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A51 RID: 2641
	public class ContentViewControllerManager : ViewControllerManager
	{
		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06004D17 RID: 19735 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004D18 RID: 19736 RVA: 0x0000216D File Offset: 0x0000036D
		public bool ConfirmAppQuit
		{
			[CompilerGenerated]
			private get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06004D19 RID: 19737 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004D1A RID: 19738 RVA: 0x0000216D File Offset: 0x0000036D
		public string AppQuitUrlScheme
		{
			[CompilerGenerated]
			private get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06004D1B RID: 19739 RVA: 0x000F4A80 File Offset: 0x000F2C80
		public Rect CanvasScreen
		{
			get
			{
				return default(Rect);
			}
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x0000216A File Offset: 0x0000036A
		public static ContentViewControllerManager GetManager()
		{
			return null;
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Awake()
		{
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Update()
		{
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x0000216D File Offset: 0x0000036D
		public void PrepareReboot()
		{
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x0000216D File Offset: 0x0000036D
		public void ExecuteReboot(string bootpath = null)
		{
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x0000216D File Offset: 0x0000036D
		public void Boot(string bootpath = null)
		{
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenAppQuitDialog()
		{
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool wantsToQuit()
		{
			return false;
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator WaitAbortNetwork()
		{
			return null;
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFadeType(SystemProgress.ProgressType tp, Color color)
		{
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void FadeIn(ViewController hideView, ViewController.TransitionType hideTrans, ViewController dispView, ViewController.TransitionType dispTrans)
		{
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void FadeOut(ViewController hideView, ViewController.TransitionType hideTrans, ViewController dispView, ViewController.TransitionType dispTrans)
		{
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override ViewControllerManager.FadeState GetFadeState()
		{
			return ViewControllerManager.FadeState.None;
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0000216D File Offset: 0x0000036D
		private void SendStackActionAction(ViewController.TransitionType type, ViewController vc, ViewController preVc)
		{
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBlurSetting(bool isBlur)
		{
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0000216A File Offset: 0x0000036A
		public Camera GetCamera()
		{
			return null;
		}

		// Token: 0x04008AC3 RID: 35523
		public static ContentViewControllerManager Instance;

		// Token: 0x04008AC4 RID: 35524
		[SerializeField]
		private bool m_launchDefaultVC;

		// Token: 0x04008AC5 RID: 35525
		private string m_wakeupBoot;

		// Token: 0x04008AC6 RID: 35526
		private bool AbortNetwork;

		// Token: 0x04008AC7 RID: 35527
		private IEnumerator m_WaitAbortNetworkCoroutine;

		// Token: 0x04008AC8 RID: 35528
		private float transitionTime;

		// Token: 0x04008AC9 RID: 35529
		private Canvas cv;

		// Token: 0x04008ACA RID: 35530
		private Color fadeColor;

		// Token: 0x04008ACB RID: 35531
		private SystemProgress.ProgressType fadeType;

		// Token: 0x04008ACC RID: 35532
		private bool manageFade;
	}
}
