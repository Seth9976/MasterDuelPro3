using System;
using System.Collections.Generic;

namespace YgomGame.Scenario
{
	// Token: 0x020009AC RID: 2476
	public class ScenarioBehavior_Bg : ScenarioBehaviour, IScenarioFadeInTransitionBehaviour, IScenarioLoadGroupHandleBehaviour
	{
		// Token: 0x06004841 RID: 18497 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehavior_Bg(object commandData)
			: base(null)
		{
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFadeInTransitionCompleted()
		{
			return false;
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x0000216D File Offset: 0x0000036D
		public void CollectLoadPath(List<ValueTuple<string, Type>> res)
		{
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x0000216D File Offset: 0x0000036D
		public void CollectLoadMrk(List<int> res)
		{
		}

		// Token: 0x04008660 RID: 34400
		private readonly float k_SrcFadeAlpha;

		// Token: 0x04008661 RID: 34401
		private readonly float k_DstFadeAlpha;

		// Token: 0x04008662 RID: 34402
		private bool m_IsLoaded;

		// Token: 0x04008663 RID: 34403
		private float m_TotalFadeSec;

		// Token: 0x04008664 RID: 34404
		private float m_FadeSec;

		// Token: 0x04008665 RID: 34405
		private object m_RenderTarget;

		// Token: 0x04008666 RID: 34406
		private object m_SubRenderTarget;
	}
}
