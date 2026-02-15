using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000626 RID: 1574
	public class TweenLayoutElementScalerTo : Tween
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x0000216A File Offset: 0x0000036A
		private LayoutElementScaler layoutElementScaler
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E4C RID: 11852
		[SerializeField]
		private float m_ToWidthScale;

		// Token: 0x04002E4D RID: 11853
		[SerializeField]
		private float m_ToHeightScale;

		// Token: 0x04002E4E RID: 11854
		private float m_FromWidthScale;

		// Token: 0x04002E4F RID: 11855
		private float m_FromHeightScale;

		// Token: 0x04002E50 RID: 11856
		private LayoutElementScaler m_LayoutElementScalerCache;
	}
}
