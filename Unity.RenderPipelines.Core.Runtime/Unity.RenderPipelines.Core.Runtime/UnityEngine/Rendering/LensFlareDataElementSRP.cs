using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	public sealed class LensFlareDataElementSRP
	{
		// Token: 0x06000A6B RID: 2667 RVA: 0x000244E8 File Offset: 0x000226E8
		public LensFlareDataElementSRP()
		{
			this.lensFlareDataSRP = null;
			this.visible = true;
			this.localIntensity = 1f;
			this.position = 0f;
			this.positionOffset = new Vector2(0f, 0f);
			this.angularOffset = 0f;
			this.translationScale = new Vector2(1f, 1f);
			this.lensFlareTexture = null;
			this.uniformScale = 1f;
			this.sizeXY = Vector2.one;
			this.allowMultipleElement = false;
			this.count = 5;
			this.rotation = 0f;
			this.preserveAspectRatio = false;
			this.ringThickness = 0.25f;
			this.hoopFactor = 1f;
			this.noiseAmplitude = 1f;
			this.noiseFrequency = 1;
			this.noiseSpeed = 0f;
			this.shapeCutOffSpeed = 0f;
			this.shapeCutOffRadius = 10f;
			this.tintColorType = SRPLensFlareColorType.Constant;
			this.tint = new Color(1f, 1f, 1f, 0.5f);
			this.tintGradient = new TextureGradient(new GradientColorKey[]
			{
				new GradientColorKey(Color.black, 0f),
				new GradientColorKey(Color.white, 1f)
			}, new GradientAlphaKey[]
			{
				new GradientAlphaKey(0f, 0f),
				new GradientAlphaKey(1f, 1f)
			}, GradientMode.PerceptualBlend, ColorSpace.Uninitialized, -1, false);
			this.blendMode = SRPLensFlareBlendMode.Additive;
			this.autoRotate = false;
			this.isFoldOpened = true;
			this.flareType = SRPLensFlareType.Circle;
			this.distribution = SRPLensFlareDistribution.Uniform;
			this.lengthSpread = 1f;
			this.colorGradient = new Gradient();
			this.colorGradient.SetKeys(new GradientColorKey[]
			{
				new GradientColorKey(Color.white, 0f),
				new GradientColorKey(Color.white, 1f)
			}, new GradientAlphaKey[]
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			});
			this.positionCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, -1f)
			});
			this.scaleCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
			this.uniformAngle = 0f;
			this.uniformAngleCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f),
				new Keyframe(1f, 0f)
			});
			this.seed = 0;
			this.intensityVariation = 0.75f;
			this.positionVariation = new Vector2(1f, 0f);
			this.scaleVariation = 1f;
			this.rotationVariation = 180f;
			this.enableRadialDistortion = false;
			this.targetSizeDistortion = Vector2.one;
			this.distortionCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, -1f)
			});
			this.distortionRelativeToCenter = false;
			this.fallOff = 1f;
			this.edgeOffset = 0.1f;
			this.sdfRoundness = 0f;
			this.sideCount = 6;
			this.inverseSDF = false;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000248C8 File Offset: 0x00022AC8
		public LensFlareDataElementSRP Clone()
		{
			LensFlareDataElementSRP lensFlareDataElementSRP = new LensFlareDataElementSRP();
			lensFlareDataElementSRP.lensFlareDataSRP = this.lensFlareDataSRP;
			lensFlareDataElementSRP.visible = this.visible;
			lensFlareDataElementSRP.localIntensity = this.localIntensity;
			lensFlareDataElementSRP.position = this.position;
			lensFlareDataElementSRP.positionOffset = this.positionOffset;
			lensFlareDataElementSRP.angularOffset = this.angularOffset;
			lensFlareDataElementSRP.translationScale = this.translationScale;
			lensFlareDataElementSRP.lensFlareTexture = this.lensFlareTexture;
			lensFlareDataElementSRP.uniformScale = this.uniformScale;
			lensFlareDataElementSRP.sizeXY = this.sizeXY;
			lensFlareDataElementSRP.allowMultipleElement = this.allowMultipleElement;
			lensFlareDataElementSRP.count = this.count;
			lensFlareDataElementSRP.rotation = this.rotation;
			lensFlareDataElementSRP.preserveAspectRatio = this.preserveAspectRatio;
			lensFlareDataElementSRP.ringThickness = this.ringThickness;
			lensFlareDataElementSRP.hoopFactor = this.hoopFactor;
			lensFlareDataElementSRP.noiseAmplitude = this.noiseAmplitude;
			lensFlareDataElementSRP.noiseFrequency = this.noiseFrequency;
			lensFlareDataElementSRP.noiseSpeed = this.noiseSpeed;
			lensFlareDataElementSRP.shapeCutOffSpeed = this.shapeCutOffSpeed;
			lensFlareDataElementSRP.shapeCutOffRadius = this.shapeCutOffRadius;
			lensFlareDataElementSRP.tintColorType = this.tintColorType;
			lensFlareDataElementSRP.tint = this.tint;
			lensFlareDataElementSRP.tintGradient = new TextureGradient(this.tintGradient.colorKeys, this.tintGradient.alphaKeys, this.tintGradient.mode, this.tintGradient.colorSpace, this.tintGradient.textureSize, false);
			lensFlareDataElementSRP.tintGradient = new TextureGradient(this.tintGradient.colorKeys, this.tintGradient.alphaKeys, GradientMode.PerceptualBlend, ColorSpace.Uninitialized, -1, false);
			lensFlareDataElementSRP.blendMode = this.blendMode;
			lensFlareDataElementSRP.autoRotate = this.autoRotate;
			lensFlareDataElementSRP.isFoldOpened = this.isFoldOpened;
			lensFlareDataElementSRP.flareType = this.flareType;
			lensFlareDataElementSRP.distribution = this.distribution;
			lensFlareDataElementSRP.lengthSpread = this.lengthSpread;
			lensFlareDataElementSRP.colorGradient = new Gradient();
			lensFlareDataElementSRP.colorGradient.SetKeys(this.colorGradient.colorKeys, this.colorGradient.alphaKeys);
			lensFlareDataElementSRP.colorGradient.mode = this.colorGradient.mode;
			lensFlareDataElementSRP.colorGradient.colorSpace = this.colorGradient.colorSpace;
			lensFlareDataElementSRP.positionCurve = new AnimationCurve(this.positionCurve.keys);
			lensFlareDataElementSRP.scaleCurve = new AnimationCurve(this.scaleCurve.keys);
			lensFlareDataElementSRP.uniformAngle = this.uniformAngle;
			lensFlareDataElementSRP.uniformAngleCurve = new AnimationCurve(this.uniformAngleCurve.keys);
			lensFlareDataElementSRP.seed = this.seed;
			lensFlareDataElementSRP.intensityVariation = this.intensityVariation;
			lensFlareDataElementSRP.positionVariation = this.positionVariation;
			lensFlareDataElementSRP.scaleVariation = this.scaleVariation;
			lensFlareDataElementSRP.rotationVariation = this.rotationVariation;
			lensFlareDataElementSRP.enableRadialDistortion = this.enableRadialDistortion;
			lensFlareDataElementSRP.targetSizeDistortion = this.targetSizeDistortion;
			lensFlareDataElementSRP.distortionCurve = new AnimationCurve(this.distortionCurve.keys);
			lensFlareDataElementSRP.distortionRelativeToCenter = this.distortionRelativeToCenter;
			lensFlareDataElementSRP.fallOff = this.fallOff;
			lensFlareDataElementSRP.edgeOffset = this.edgeOffset;
			lensFlareDataElementSRP.sdfRoundness = this.sdfRoundness;
			lensFlareDataElementSRP.sideCount = this.sideCount;
			lensFlareDataElementSRP.inverseSDF = this.inverseSDF;
			return lensFlareDataElementSRP;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00024BF6 File Offset: 0x00022DF6
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x00024BFE File Offset: 0x00022DFE
		public float localIntensity
		{
			get
			{
				return this.m_LocalIntensity;
			}
			set
			{
				this.m_LocalIntensity = Mathf.Max(0f, value);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00024C11 File Offset: 0x00022E11
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x00024C19 File Offset: 0x00022E19
		public int count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = Mathf.Max(1, value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00024C28 File Offset: 0x00022E28
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x00024C30 File Offset: 0x00022E30
		public float intensityVariation
		{
			get
			{
				return this.m_IntensityVariation;
			}
			set
			{
				this.m_IntensityVariation = Mathf.Max(0f, value);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00024C43 File Offset: 0x00022E43
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x00024C4B File Offset: 0x00022E4B
		public float fallOff
		{
			get
			{
				return this.m_FallOff;
			}
			set
			{
				this.m_FallOff = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00024C59 File Offset: 0x00022E59
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x00024C61 File Offset: 0x00022E61
		public float edgeOffset
		{
			get
			{
				return this.m_EdgeOffset;
			}
			set
			{
				this.m_EdgeOffset = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00024C6F File Offset: 0x00022E6F
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x00024C77 File Offset: 0x00022E77
		public int sideCount
		{
			get
			{
				return this.m_SideCount;
			}
			set
			{
				this.m_SideCount = Mathf.Max(3, value);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00024C86 File Offset: 0x00022E86
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x00024C8E File Offset: 0x00022E8E
		public float sdfRoundness
		{
			get
			{
				return this.m_SdfRoundness;
			}
			set
			{
				this.m_SdfRoundness = Mathf.Clamp01(value);
			}
		}

		// Token: 0x04000685 RID: 1669
		public LensFlareDataSRP lensFlareDataSRP;

		// Token: 0x04000686 RID: 1670
		public bool visible;

		// Token: 0x04000687 RID: 1671
		public float position;

		// Token: 0x04000688 RID: 1672
		public Vector2 positionOffset;

		// Token: 0x04000689 RID: 1673
		public float angularOffset;

		// Token: 0x0400068A RID: 1674
		public Vector2 translationScale;

		// Token: 0x0400068B RID: 1675
		[Range(0f, 1f)]
		public float ringThickness;

		// Token: 0x0400068C RID: 1676
		[Range(-1f, 1f)]
		public float hoopFactor;

		// Token: 0x0400068D RID: 1677
		public float noiseAmplitude;

		// Token: 0x0400068E RID: 1678
		public int noiseFrequency;

		// Token: 0x0400068F RID: 1679
		public float noiseSpeed;

		// Token: 0x04000690 RID: 1680
		public float shapeCutOffSpeed;

		// Token: 0x04000691 RID: 1681
		public float shapeCutOffRadius;

		// Token: 0x04000692 RID: 1682
		[Min(0f)]
		[SerializeField]
		[FormerlySerializedAs("localIntensity")]
		private float m_LocalIntensity;

		// Token: 0x04000693 RID: 1683
		public Texture lensFlareTexture;

		// Token: 0x04000694 RID: 1684
		public float uniformScale;

		// Token: 0x04000695 RID: 1685
		public Vector2 sizeXY;

		// Token: 0x04000696 RID: 1686
		public bool allowMultipleElement;

		// Token: 0x04000697 RID: 1687
		[Min(1f)]
		[SerializeField]
		[FormerlySerializedAs("count")]
		private int m_Count;

		// Token: 0x04000698 RID: 1688
		public bool preserveAspectRatio;

		// Token: 0x04000699 RID: 1689
		public float rotation;

		// Token: 0x0400069A RID: 1690
		public SRPLensFlareColorType tintColorType;

		// Token: 0x0400069B RID: 1691
		public Color tint;

		// Token: 0x0400069C RID: 1692
		public TextureGradient tintGradient;

		// Token: 0x0400069D RID: 1693
		public SRPLensFlareBlendMode blendMode;

		// Token: 0x0400069E RID: 1694
		public bool autoRotate;

		// Token: 0x0400069F RID: 1695
		public SRPLensFlareType flareType;

		// Token: 0x040006A0 RID: 1696
		public bool modulateByLightColor;

		// Token: 0x040006A1 RID: 1697
		[SerializeField]
		private bool isFoldOpened;

		// Token: 0x040006A2 RID: 1698
		public SRPLensFlareDistribution distribution;

		// Token: 0x040006A3 RID: 1699
		public float lengthSpread;

		// Token: 0x040006A4 RID: 1700
		public AnimationCurve positionCurve;

		// Token: 0x040006A5 RID: 1701
		public AnimationCurve scaleCurve;

		// Token: 0x040006A6 RID: 1702
		public int seed;

		// Token: 0x040006A7 RID: 1703
		public Gradient colorGradient;

		// Token: 0x040006A8 RID: 1704
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("intensityVariation")]
		private float m_IntensityVariation;

		// Token: 0x040006A9 RID: 1705
		public Vector2 positionVariation;

		// Token: 0x040006AA RID: 1706
		public float scaleVariation;

		// Token: 0x040006AB RID: 1707
		public float rotationVariation;

		// Token: 0x040006AC RID: 1708
		public bool enableRadialDistortion;

		// Token: 0x040006AD RID: 1709
		public Vector2 targetSizeDistortion;

		// Token: 0x040006AE RID: 1710
		public AnimationCurve distortionCurve;

		// Token: 0x040006AF RID: 1711
		public bool distortionRelativeToCenter;

		// Token: 0x040006B0 RID: 1712
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("fallOff")]
		private float m_FallOff;

		// Token: 0x040006B1 RID: 1713
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("edgeOffset")]
		private float m_EdgeOffset;

		// Token: 0x040006B2 RID: 1714
		[Min(3f)]
		[SerializeField]
		[FormerlySerializedAs("sideCount")]
		private int m_SideCount;

		// Token: 0x040006B3 RID: 1715
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("sdfRoundness")]
		private float m_SdfRoundness;

		// Token: 0x040006B4 RID: 1716
		public bool inverseSDF;

		// Token: 0x040006B5 RID: 1717
		public float uniformAngle;

		// Token: 0x040006B6 RID: 1718
		public AnimationCurve uniformAngleCurve;
	}
}
