using System;

namespace YgomGame.Scenario
{
	// Token: 0x020009B4 RID: 2484
	public class ScenarioBehaviour_BgWaitLabeledTimeline : ScenarioBehaviour
	{
		// Token: 0x06004871 RID: 18545 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_BgWaitLabeledTimeline(object commandData)
			: base(null)
		{
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPointerClick()
		{
		}

		// Token: 0x04008694 RID: 34452
		private object m_RenderTarget;

		// Token: 0x04008695 RID: 34453
		private ScenarioBGActor m_BGActor;

		// Token: 0x04008696 RID: 34454
		private bool m_IsEnableSkip;
	}
}
