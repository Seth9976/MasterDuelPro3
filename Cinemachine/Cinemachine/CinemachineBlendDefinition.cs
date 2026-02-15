using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000069 RID: 105
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[Serializable]
	public struct CinemachineBlendDefinition
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00011EDF File Offset: 0x000100DF
		public float BlendTime
		{
			get
			{
				if (this.m_Style != CinemachineBlendDefinition.Style.Cut)
				{
					return this.m_Time;
				}
				return 0f;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00011EF5 File Offset: 0x000100F5
		public CinemachineBlendDefinition(CinemachineBlendDefinition.Style style, float time)
		{
			this.m_Style = style;
			this.m_Time = time;
			this.m_CustomCurve = null;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00011F0C File Offset: 0x0001010C
		private void CreateStandardCurves()
		{
			CinemachineBlendDefinition.sStandardCurves = new AnimationCurve[7];
			CinemachineBlendDefinition.sStandardCurves[0] = null;
			CinemachineBlendDefinition.sStandardCurves[1] = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
			CinemachineBlendDefinition.sStandardCurves[2] = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			Keyframe[] keys = CinemachineBlendDefinition.sStandardCurves[2].keys;
			keys[0].outTangent = 1.4f;
			keys[1].inTangent = 0f;
			CinemachineBlendDefinition.sStandardCurves[2].keys = keys;
			CinemachineBlendDefinition.sStandardCurves[3] = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			keys = CinemachineBlendDefinition.sStandardCurves[3].keys;
			keys[0].outTangent = 0f;
			keys[1].inTangent = 1.4f;
			CinemachineBlendDefinition.sStandardCurves[3].keys = keys;
			CinemachineBlendDefinition.sStandardCurves[4] = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			keys = CinemachineBlendDefinition.sStandardCurves[4].keys;
			keys[0].outTangent = 0f;
			keys[1].inTangent = 3f;
			CinemachineBlendDefinition.sStandardCurves[4].keys = keys;
			CinemachineBlendDefinition.sStandardCurves[5] = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			keys = CinemachineBlendDefinition.sStandardCurves[5].keys;
			keys[0].outTangent = 3f;
			keys[1].inTangent = 0f;
			CinemachineBlendDefinition.sStandardCurves[5].keys = keys;
			CinemachineBlendDefinition.sStandardCurves[6] = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000120DC File Offset: 0x000102DC
		public AnimationCurve BlendCurve
		{
			get
			{
				if (this.m_Style == CinemachineBlendDefinition.Style.Custom)
				{
					if (this.m_CustomCurve == null)
					{
						this.m_CustomCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
					}
					return this.m_CustomCurve;
				}
				if (CinemachineBlendDefinition.sStandardCurves == null)
				{
					this.CreateStandardCurves();
				}
				return CinemachineBlendDefinition.sStandardCurves[(int)this.m_Style];
			}
		}

		// Token: 0x0400027A RID: 634
		[Tooltip("Shape of the blend curve")]
		public CinemachineBlendDefinition.Style m_Style;

		// Token: 0x0400027B RID: 635
		[Tooltip("Duration of the blend, in seconds")]
		public float m_Time;

		// Token: 0x0400027C RID: 636
		public AnimationCurve m_CustomCurve;

		// Token: 0x0400027D RID: 637
		private static AnimationCurve[] sStandardCurves;

		// Token: 0x0200006A RID: 106
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		public enum Style
		{
			// Token: 0x0400027F RID: 639
			Cut,
			// Token: 0x04000280 RID: 640
			EaseInOut,
			// Token: 0x04000281 RID: 641
			EaseIn,
			// Token: 0x04000282 RID: 642
			EaseOut,
			// Token: 0x04000283 RID: 643
			HardIn,
			// Token: 0x04000284 RID: 644
			HardOut,
			// Token: 0x04000285 RID: 645
			Linear,
			// Token: 0x04000286 RID: 646
			Custom
		}
	}
}
