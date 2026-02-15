using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001B5 RID: 437
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Light))]
	public class UniversalAdditionalLightData : MonoBehaviour, ISerializationCallbackReceiver, IAdditionalData
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0002E4F6 File Offset: 0x0002C6F6
		internal int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0002E4FE File Offset: 0x0002C6FE
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0002E506 File Offset: 0x0002C706
		public bool usePipelineSettings
		{
			get
			{
				return this.m_UsePipelineSettings;
			}
			set
			{
				this.m_UsePipelineSettings = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0002E50F File Offset: 0x0002C70F
		internal Light light
		{
			get
			{
				if (!this.m_Light)
				{
					base.TryGetComponent<Light>(out this.m_Light);
				}
				return this.m_Light;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0002E531 File Offset: 0x0002C731
		public int additionalLightsShadowResolutionTier
		{
			get
			{
				return this.m_AdditionalLightsShadowResolutionTier;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0002E539 File Offset: 0x0002C739
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0002E541 File Offset: 0x0002C741
		[Obsolete("This is obsolete, please use renderingLayerMask instead.", true)]
		public LightLayerEnum lightLayerMask
		{
			get
			{
				return this.m_LightLayerMask;
			}
			set
			{
				this.m_LightLayerMask = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0002E54A File Offset: 0x0002C74A
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x0002E552 File Offset: 0x0002C752
		public uint renderingLayers
		{
			get
			{
				return this.m_RenderingLayers;
			}
			set
			{
				if (this.m_RenderingLayers != value)
				{
					this.m_RenderingLayers = value;
					this.SyncLightAndShadowLayers();
				}
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0002E56A File Offset: 0x0002C76A
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x0002E572 File Offset: 0x0002C772
		public bool customShadowLayers
		{
			get
			{
				return this.m_CustomShadowLayers;
			}
			set
			{
				if (this.m_CustomShadowLayers != value)
				{
					this.m_CustomShadowLayers = value;
					this.SyncLightAndShadowLayers();
				}
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0002E58A File Offset: 0x0002C78A
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0002E592 File Offset: 0x0002C792
		[Obsolete("This is obsolete, please use shadowRenderingLayerMask instead.", true)]
		public LightLayerEnum shadowLayerMask
		{
			get
			{
				return this.m_ShadowLayerMask;
			}
			set
			{
				this.m_ShadowLayerMask = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0002E59B File Offset: 0x0002C79B
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0002E5A3 File Offset: 0x0002C7A3
		public uint shadowRenderingLayers
		{
			get
			{
				return this.m_ShadowRenderingLayers;
			}
			set
			{
				if (value != this.m_ShadowRenderingLayers)
				{
					this.m_ShadowRenderingLayers = value;
					this.SyncLightAndShadowLayers();
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002E5BB File Offset: 0x0002C7BB
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0002E5C3 File Offset: 0x0002C7C3
		[Tooltip("Controls the size of the cookie mask currently assigned to the light.")]
		public Vector2 lightCookieSize
		{
			get
			{
				return this.m_LightCookieSize;
			}
			set
			{
				this.m_LightCookieSize = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0002E5CC File Offset: 0x0002C7CC
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x0002E5D4 File Offset: 0x0002C7D4
		[Tooltip("Controls the offset of the cookie mask currently assigned to the light.")]
		public Vector2 lightCookieOffset
		{
			get
			{
				return this.m_LightCookieOffset;
			}
			set
			{
				this.m_LightCookieOffset = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0002E5DD File Offset: 0x0002C7DD
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x0002E5E5 File Offset: 0x0002C7E5
		[Tooltip("Controls the filtering quality of soft shadows. Higher quality has lower performance.")]
		public SoftShadowQuality softShadowQuality
		{
			get
			{
				return this.m_SoftShadowQuality;
			}
			set
			{
				this.m_SoftShadowQuality = value;
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0000217F File Offset: 0x0000037F
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002E5F0 File Offset: 0x0002C7F0
		public void OnAfterDeserialize()
		{
			if (this.m_Version < 2)
			{
				this.m_RenderingLayers = (uint)this.m_LightLayerMask;
				this.m_ShadowRenderingLayers = (uint)this.m_ShadowLayerMask;
				this.m_Version = 2;
			}
			if (this.m_Version < 3)
			{
				this.m_SoftShadowQuality = (SoftShadowQuality)Math.Clamp((int)(this.m_SoftShadowQuality + 1), 0, 3);
				this.m_Version = 3;
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002E64A File Offset: 0x0002C84A
		private void SyncLightAndShadowLayers()
		{
			if (this.light)
			{
				this.light.renderingLayerMask = (int)(this.m_CustomShadowLayers ? this.m_ShadowRenderingLayers : this.m_RenderingLayers);
			}
		}

		// Token: 0x040009A1 RID: 2465
		[SerializeField]
		private int m_Version = 3;

		// Token: 0x040009A2 RID: 2466
		[Tooltip("Controls if light Shadow Bias parameters use pipeline settings.")]
		[SerializeField]
		private bool m_UsePipelineSettings = true;

		// Token: 0x040009A3 RID: 2467
		public static readonly int AdditionalLightsShadowResolutionTierCustom = -1;

		// Token: 0x040009A4 RID: 2468
		public static readonly int AdditionalLightsShadowResolutionTierLow = 0;

		// Token: 0x040009A5 RID: 2469
		public static readonly int AdditionalLightsShadowResolutionTierMedium = 1;

		// Token: 0x040009A6 RID: 2470
		public static readonly int AdditionalLightsShadowResolutionTierHigh = 2;

		// Token: 0x040009A7 RID: 2471
		public static readonly int AdditionalLightsShadowDefaultResolutionTier = UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierHigh;

		// Token: 0x040009A8 RID: 2472
		public static readonly int AdditionalLightsShadowDefaultCustomResolution = 128;

		// Token: 0x040009A9 RID: 2473
		[NonSerialized]
		private Light m_Light;

		// Token: 0x040009AA RID: 2474
		public static readonly int AdditionalLightsShadowMinimumResolution = 128;

		// Token: 0x040009AB RID: 2475
		[Tooltip("Controls if light shadow resolution uses pipeline settings.")]
		[SerializeField]
		private int m_AdditionalLightsShadowResolutionTier = UniversalAdditionalLightData.AdditionalLightsShadowDefaultResolutionTier;

		// Token: 0x040009AC RID: 2476
		[Obsolete("This is obsolete, please use m_RenderingLayerMask instead.", false)]
		[SerializeField]
		private LightLayerEnum m_LightLayerMask = LightLayerEnum.LightLayerDefault;

		// Token: 0x040009AD RID: 2477
		[SerializeField]
		private uint m_RenderingLayers = 1U;

		// Token: 0x040009AE RID: 2478
		[SerializeField]
		private bool m_CustomShadowLayers;

		// Token: 0x040009AF RID: 2479
		[SerializeField]
		private LightLayerEnum m_ShadowLayerMask = LightLayerEnum.LightLayerDefault;

		// Token: 0x040009B0 RID: 2480
		[SerializeField]
		private uint m_ShadowRenderingLayers = 1U;

		// Token: 0x040009B1 RID: 2481
		[SerializeField]
		private Vector2 m_LightCookieSize = Vector2.one;

		// Token: 0x040009B2 RID: 2482
		[SerializeField]
		private Vector2 m_LightCookieOffset = Vector2.zero;

		// Token: 0x040009B3 RID: 2483
		[SerializeField]
		private SoftShadowQuality m_SoftShadowQuality;
	}
}
