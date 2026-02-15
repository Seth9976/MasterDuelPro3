using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000151 RID: 337
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Lens Flare (SRP)")]
	public sealed class LensFlareComponentSRP : MonoBehaviour
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x000242D3 File Offset: 0x000224D3
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x000242DB File Offset: 0x000224DB
		public LensFlareDataSRP lensFlareData
		{
			get
			{
				return this.m_LensFlareData;
			}
			set
			{
				this.m_LensFlareData = value;
				this.OnValidate();
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000242EC File Offset: 0x000224EC
		public float celestialProjectedOcclusionRadius(Camera mainCam)
		{
			float projectedRadius = (float)Math.Tan((double)LensFlareComponentSRP.sCelestialAngularRadius) * mainCam.farClipPlane;
			return this.occlusionRadius * projectedRadius;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00005704 File Offset: 0x00003904
		private void Awake()
		{
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00024315 File Offset: 0x00022515
		private void OnEnable()
		{
			if (this.lensFlareData)
			{
				LensFlareCommonSRP.Instance.AddData(this);
				return;
			}
			LensFlareCommonSRP.Instance.RemoveData(this);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002433B File Offset: 0x0002253B
		private void OnDisable()
		{
			LensFlareCommonSRP.Instance.RemoveData(this);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00024348 File Offset: 0x00022548
		private void OnValidate()
		{
			if (base.isActiveAndEnabled && this.lensFlareData != null)
			{
				LensFlareCommonSRP.Instance.AddData(this);
				return;
			}
			LensFlareCommonSRP.Instance.RemoveData(this);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00024377 File Offset: 0x00022577
		private void OnDestroy()
		{
			this.occlusionRemapCurve.Release();
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00024384 File Offset: 0x00022584
		public LensFlareComponentSRP()
		{
			AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			float num = 1f;
			bool flag = false;
			Vector2 vector = new Vector2(0f, 1f);
			this.occlusionRemapCurve = new TextureCurve(animationCurve, num, flag, in vector);
			base..ctor();
		}

		// Token: 0x0400065A RID: 1626
		[SerializeField]
		private LensFlareDataSRP m_LensFlareData;

		// Token: 0x0400065B RID: 1627
		[SerializeField]
		private LensFlareComponentSRP.Version version;

		// Token: 0x0400065C RID: 1628
		[Min(0f)]
		public float intensity = 1f;

		// Token: 0x0400065D RID: 1629
		[Min(1E-05f)]
		public float maxAttenuationDistance = 100f;

		// Token: 0x0400065E RID: 1630
		[Min(1E-05f)]
		public float maxAttenuationScale = 100f;

		// Token: 0x0400065F RID: 1631
		public AnimationCurve distanceAttenuationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04000660 RID: 1632
		public AnimationCurve scaleByDistanceCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04000661 RID: 1633
		public bool attenuationByLightShape = true;

		// Token: 0x04000662 RID: 1634
		public AnimationCurve radialScreenAttenuationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04000663 RID: 1635
		public bool useOcclusion;

		// Token: 0x04000664 RID: 1636
		[Obsolete("Replaced by environmentOcclusion.")]
		public bool useBackgroundCloudOcclusion;

		// Token: 0x04000665 RID: 1637
		[FormerlySerializedAs("volumetricCloudOcclusion")]
		[FormerlySerializedAs("useFogOpacityOcclusion")]
		public bool environmentOcclusion;

		// Token: 0x04000666 RID: 1638
		[Obsolete("Replaced by environmentOcclusion.")]
		public bool useWaterOcclusion;

		// Token: 0x04000667 RID: 1639
		[Min(0f)]
		public float occlusionRadius = 0.1f;

		// Token: 0x04000668 RID: 1640
		[Range(1f, 64f)]
		public uint sampleCount = 32U;

		// Token: 0x04000669 RID: 1641
		public float occlusionOffset = 0.05f;

		// Token: 0x0400066A RID: 1642
		[Min(0f)]
		public float scale = 1f;

		// Token: 0x0400066B RID: 1643
		public bool allowOffScreen;

		// Token: 0x0400066C RID: 1644
		[Obsolete("Please use environmentOcclusion instead.")]
		public bool volumetricCloudOcclusion;

		// Token: 0x0400066D RID: 1645
		private static float sCelestialAngularRadius = 0.057595868f;

		// Token: 0x0400066E RID: 1646
		public TextureCurve occlusionRemapCurve;

		// Token: 0x0400066F RID: 1647
		public Light lightOverride;

		// Token: 0x02000152 RID: 338
		private enum Version
		{
			// Token: 0x04000671 RID: 1649
			Initial
		}
	}
}
