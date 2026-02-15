using System;
using System.Collections;
using YgomSystem;

namespace YgomGame.Menu
{
	// Token: 0x02000AB0 RID: 2736
	public class OnDemandLogoViewController : BaseMenuViewController
	{
		// Token: 0x06004FA2 RID: 20386 RVA: 0x0000216D File Offset: 0x0000036D
		private static void debugLog(string msg)
		{
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x0000216D File Offset: 0x0000036D
		private static void timeLog(string msg)
		{
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepLoading(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepTweenPlay(StepSequencer seq, string tweenLabel, OnDemandLogoViewController.Step nextStep)
		{
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepLogoStay(StepSequencer seq)
		{
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepFinish(StepSequencer seq)
		{
		}

		// Token: 0x04008DB0 RID: 36272
		private const float LogoDelayTime = 1.5f;

		// Token: 0x04008DB1 RID: 36273
		private StepSequencer m_sequencer;

		// Token: 0x04008DB2 RID: 36274
		private OnDemandLogoData m_logoData;

		// Token: 0x04008DB3 RID: 36275
		private float m_logoDispTime;

		// Token: 0x04008DB4 RID: 36276
		private bool m_skipLogo;

		// Token: 0x04008DB5 RID: 36277
		private Action m_resultCallback;

		// Token: 0x02000AB1 RID: 2737
		private enum Step
		{
			// Token: 0x04008DB7 RID: 36279
			Loading,
			// Token: 0x04008DB8 RID: 36280
			StartFade,
			// Token: 0x04008DB9 RID: 36281
			LogoIn,
			// Token: 0x04008DBA RID: 36282
			LogoStay,
			// Token: 0x04008DBB RID: 36283
			LogoOut,
			// Token: 0x04008DBC RID: 36284
			EndFade,
			// Token: 0x04008DBD RID: 36285
			Finish
		}
	}
}
