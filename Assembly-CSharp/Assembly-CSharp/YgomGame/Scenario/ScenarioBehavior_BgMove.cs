using System;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009AE RID: 2478
	public class ScenarioBehavior_BgMove : ScenarioBehaviour
	{
		// Token: 0x0600484A RID: 18506 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehavior_BgMove(object commandData)
			: base(null)
		{
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x0400866F RID: 34415
		private object m_RenderTarget;

		// Token: 0x04008670 RID: 34416
		private ScenarioBGActor m_BGActor;

		// Token: 0x04008671 RID: 34417
		private int m_Direction;

		// Token: 0x04008672 RID: 34418
		private float m_TotalFadeSec;

		// Token: 0x04008673 RID: 34419
		private float m_FadeSec;

		// Token: 0x04008674 RID: 34420
		private float m_CurveDiff;

		// Token: 0x04008675 RID: 34421
		private AnimationCurve m_AnimationCurve;

		// Token: 0x04008676 RID: 34422
		private Vector3 m_SrcPos;

		// Token: 0x04008677 RID: 34423
		private Vector3 m_DstPos;
	}
}
