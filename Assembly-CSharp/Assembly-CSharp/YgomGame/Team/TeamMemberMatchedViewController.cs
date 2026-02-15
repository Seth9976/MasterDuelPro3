using System;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;

namespace YgomGame.Team
{
	// Token: 0x020008C5 RID: 2245
	public class TeamMemberMatchedViewController : BaseMenuViewController, IBokeSupported
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsLoadingEffect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Action callback = null)
		{
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x0000216D File Offset: 0x0000036D
		private void Play()
		{
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayEffect(string effectPath)
		{
		}

		// Token: 0x04007FF3 RID: 32755
		private const string k_ArgKeyCallback = "callback";

		// Token: 0x04007FF4 RID: 32756
		private readonly string E_BG3D;

		// Token: 0x04007FF5 RID: 32757
		private readonly string E_BackShortcutButton;

		// Token: 0x04007FF6 RID: 32758
		private readonly string E_ButtonSkip;

		// Token: 0x04007FF7 RID: 32759
		private readonly string E_RootMatched;

		// Token: 0x04007FF8 RID: 32760
		private readonly string E_Root;

		// Token: 0x04007FF9 RID: 32761
		private readonly string E_Text;

		// Token: 0x04007FFA RID: 32762
		private GameObject m_View3D;

		// Token: 0x04007FFB RID: 32763
		private ElementObjectManager m_TargetEom;

		// Token: 0x04007FFC RID: 32764
		private bool b_IsFinish;

		// Token: 0x04007FFD RID: 32765
		private bool b_IsSkip;

		// Token: 0x04007FFE RID: 32766
		private int m_LoadingEffectCount;
	}
}
