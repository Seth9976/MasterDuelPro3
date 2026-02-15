using System;
using UnityEngine.Rendering.Universal;

namespace Willow
{
	// Token: 0x02001550 RID: 5456
	public class PostProcessVolumeMotionBlur : BasePostProcessVolumeControl
	{
		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06009E40 RID: 40512 RVA: 0x0000216A File Offset: 0x0000036A
		private MotionBlur motionBlur
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06009E41 RID: 40513 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CheckPostProcess()
		{
		}

		// Token: 0x06009E42 RID: 40514 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void StartPostProcess()
		{
		}

		// Token: 0x06009E43 RID: 40515 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdatePostProcess()
		{
		}

		// Token: 0x0400DDDE RID: 56798
		private MotionBlur m_motionBlur;

		// Token: 0x0400DDDF RID: 56799
		public float m_intensity;

		// Token: 0x0400DDE0 RID: 56800
		public float m_clamp;

		// Token: 0x0400DDE1 RID: 56801
		private bool m_usedIntensity;

		// Token: 0x0400DDE2 RID: 56802
		private bool m_usedClamp;

		// Token: 0x0400DDE3 RID: 56803
		private float m_prevIntensity;

		// Token: 0x0400DDE4 RID: 56804
		private float m_prevClamp;
	}
}
