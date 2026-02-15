using System;

namespace YgomGame.Scenario
{
	// Token: 0x020009B5 RID: 2485
	public class ScenarioBehaviour_BlurLayer : ScenarioBehaviour
	{
		// Token: 0x06004875 RID: 18549 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_BlurLayer(object commandData)
			: base(null)
		{
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x06004877 RID: 18551 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x04008697 RID: 34455
		private ScenarioBlurLayerActor m_BlurLayerActor;

		// Token: 0x04008698 RID: 34456
		private float m_TotalFadeSec;

		// Token: 0x04008699 RID: 34457
		private float m_FadeSec;

		// Token: 0x0400869A RID: 34458
		private float m_FromEffectVal;

		// Token: 0x0400869B RID: 34459
		private float m_ToEffectVal;
	}
}
