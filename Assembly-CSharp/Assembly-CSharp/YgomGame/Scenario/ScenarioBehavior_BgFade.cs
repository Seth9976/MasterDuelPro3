using System;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009AD RID: 2477
	public class ScenarioBehavior_BgFade : ScenarioBehaviour
	{
		// Token: 0x06004847 RID: 18503 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehavior_BgFade(object commandData)
			: base(null)
		{
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x04008667 RID: 34407
		private object m_RenderTarget;

		// Token: 0x04008668 RID: 34408
		private ScenarioBGActor m_BGActor;

		// Token: 0x04008669 RID: 34409
		private float m_TotalFadeSec;

		// Token: 0x0400866A RID: 34410
		private float m_fadeSec;

		// Token: 0x0400866B RID: 34411
		private Vector3 m_SrcPos;

		// Token: 0x0400866C RID: 34412
		private Vector3 m_DstPos;

		// Token: 0x0400866D RID: 34413
		private Color m_FromColor;

		// Token: 0x0400866E RID: 34414
		private Color m_ToColor;
	}
}
