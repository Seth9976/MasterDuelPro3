using System;
using System.Collections;
using YgomSystem;

namespace YgomGame.Menu
{
	// Token: 0x02000A9F RID: 2719
	public class KonamiLogoViewController : BaseMenuViewController
	{
		// Token: 0x06004F40 RID: 20288 RVA: 0x0000216D File Offset: 0x0000036D
		private void debugLog(string msg)
		{
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepInit(StepSequencer seq)
		{
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepTweenPlay(StepSequencer seq, string tweenLabel, KonamiLogoViewController.Step nextStep)
		{
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepLogoStay(StepSequencer seq)
		{
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepFinish(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x04008D2E RID: 36142
		private const float KonamiLogoDelayTime = 2f;

		// Token: 0x04008D2F RID: 36143
		private StepSequencer m_sequencer;

		// Token: 0x04008D30 RID: 36144
		private float m_KonamiLogoDispTime;

		// Token: 0x04008D31 RID: 36145
		private bool m_KonamiLogoSkip;

		// Token: 0x02000AA0 RID: 2720
		private enum Step
		{
			// Token: 0x04008D33 RID: 36147
			Init,
			// Token: 0x04008D34 RID: 36148
			StartFade,
			// Token: 0x04008D35 RID: 36149
			LogoIn,
			// Token: 0x04008D36 RID: 36150
			LogoStay,
			// Token: 0x04008D37 RID: 36151
			LogoOut,
			// Token: 0x04008D38 RID: 36152
			EndFade,
			// Token: 0x04008D39 RID: 36153
			Finish
		}
	}
}
