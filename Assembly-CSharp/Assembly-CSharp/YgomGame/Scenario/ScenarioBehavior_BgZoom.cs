using System;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009AF RID: 2479
	public class ScenarioBehavior_BgZoom : ScenarioBehaviour
	{
		// Token: 0x0600484D RID: 18509 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehavior_BgZoom(object commandData)
			: base(null)
		{
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x04008678 RID: 34424
		private ScenarioBGActor m_BGActor;

		// Token: 0x04008679 RID: 34425
		private object m_RenderTarget;

		// Token: 0x0400867A RID: 34426
		private float m_TotalFadeSec;

		// Token: 0x0400867B RID: 34427
		private float m_FadeSec;

		// Token: 0x0400867C RID: 34428
		private float m_CurveDiff;

		// Token: 0x0400867D RID: 34429
		private AnimationCurve m_AnimationCurve;

		// Token: 0x0400867E RID: 34430
		private float m_SrcScale;

		// Token: 0x0400867F RID: 34431
		private float m_DstScale;
	}
}
