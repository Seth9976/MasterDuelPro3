using System;
using UnityEngine.Rendering.Universal;

namespace Willow
{
	// Token: 0x0200154F RID: 5455
	public class PostProcessVolumeDepthOfField : BasePostProcessVolumeControl
	{
		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x06009E39 RID: 40505 RVA: 0x0000216A File Offset: 0x0000036A
		private DepthOfField depthOfField
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06009E3A RID: 40506 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CheckPostProcess()
		{
		}

		// Token: 0x06009E3B RID: 40507 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void StartPostProcess()
		{
		}

		// Token: 0x06009E3C RID: 40508 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdatePostProcess()
		{
		}

		// Token: 0x06009E3D RID: 40509 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateBokeh()
		{
		}

		// Token: 0x06009E3E RID: 40510 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateGaussian()
		{
		}

		// Token: 0x0400DDC7 RID: 56775
		private DepthOfField m_depthOfField;

		// Token: 0x0400DDC8 RID: 56776
		public float m_focusDistance;

		// Token: 0x0400DDC9 RID: 56777
		public float m_focalLength;

		// Token: 0x0400DDCA RID: 56778
		public float m_aperture;

		// Token: 0x0400DDCB RID: 56779
		public int m_bladeCount;

		// Token: 0x0400DDCC RID: 56780
		public float m_bladeCurvature;

		// Token: 0x0400DDCD RID: 56781
		public float m_bladeRotation;

		// Token: 0x0400DDCE RID: 56782
		public float m_gaussianMaxRadius;

		// Token: 0x0400DDCF RID: 56783
		private DepthOfFieldMode m_mode;

		// Token: 0x0400DDD0 RID: 56784
		private bool m_usedFocusDistance;

		// Token: 0x0400DDD1 RID: 56785
		private bool m_usedAperture;

		// Token: 0x0400DDD2 RID: 56786
		private bool m_usedFocalLength;

		// Token: 0x0400DDD3 RID: 56787
		private bool m_usedBladeCount;

		// Token: 0x0400DDD4 RID: 56788
		private bool m_usedBladeCurvature;

		// Token: 0x0400DDD5 RID: 56789
		private bool m_usedBladeRotation;

		// Token: 0x0400DDD6 RID: 56790
		private bool m_usedGaussianMaxRadius;

		// Token: 0x0400DDD7 RID: 56791
		private float m_prevFocusDistance;

		// Token: 0x0400DDD8 RID: 56792
		private float m_prevAperture;

		// Token: 0x0400DDD9 RID: 56793
		private float m_prevFocalLength;

		// Token: 0x0400DDDA RID: 56794
		private int m_prevBladeCount;

		// Token: 0x0400DDDB RID: 56795
		private float m_prevBladeCurvature;

		// Token: 0x0400DDDC RID: 56796
		private float m_prevBladeRotation;

		// Token: 0x0400DDDD RID: 56797
		private float m_prevGaussianMaxRadius;
	}
}
