using System;
using UnityEngine;
using YgomSystem;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000B01 RID: 2817
	public class UnityLogoViewController : TweenViewController
	{
		// Token: 0x060051EC RID: 20972 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepInit(StepSequencer seq)
		{
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepTweenPlay(StepSequencer seq, string tweenLabel, UnityLogoViewController.Step nextStep)
		{
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepLogoStay(StepSequencer seq)
		{
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepFinish(StepSequencer seq)
		{
		}

		// Token: 0x04009057 RID: 36951
		private static bool s_enableUnityLogoSkip;

		// Token: 0x04009058 RID: 36952
		private const float UnityLogoDelayTime = 2f;

		// Token: 0x04009059 RID: 36953
		[SerializeField]
		private ElementObjectManager prefabUI;

		// Token: 0x0400905A RID: 36954
		private ElementObjectManager m_ui;

		// Token: 0x0400905B RID: 36955
		private StepSequencer m_sequencer;

		// Token: 0x0400905C RID: 36956
		private float m_UnityLogoDispTime;

		// Token: 0x0400905D RID: 36957
		private bool m_UnityLogoSkip;

		// Token: 0x02000B02 RID: 2818
		private enum Step
		{
			// Token: 0x0400905F RID: 36959
			Init,
			// Token: 0x04009060 RID: 36960
			StartFade,
			// Token: 0x04009061 RID: 36961
			LogoIn,
			// Token: 0x04009062 RID: 36962
			LogoStay,
			// Token: 0x04009063 RID: 36963
			LogoOut,
			// Token: 0x04009064 RID: 36964
			EndFade,
			// Token: 0x04009065 RID: 36965
			Finish
		}

		// Token: 0x02000B03 RID: 2819
		private enum ConnectionStatus
		{
			// Token: 0x04009067 RID: 36967
			None,
			// Token: 0x04009068 RID: 36968
			Connecting,
			// Token: 0x04009069 RID: 36969
			Success,
			// Token: 0x0400906A RID: 36970
			Failed
		}
	}
}
