using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000FA RID: 250
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Export/Graphics/Light.bindings.h")]
	[NativeHeader("Runtime/Camera/Light.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class Light : Behaviour
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00011E50 File Offset: 0x00010050
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00011E74 File Offset: 0x00010074
		[NativeProperty("LightType")]
		public LightType type
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_type_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_type_Injected(intPtr, value);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00011E97 File Offset: 0x00010097
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00011E9F File Offset: 0x0001009F
		[Obsolete("This property has been deprecated. Use Light.type instead.")]
		public LightShape shape { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00011EA8 File Offset: 0x000100A8
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00011ECC File Offset: 0x000100CC
		public float spotAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_spotAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_spotAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00011EF0 File Offset: 0x000100F0
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00011F14 File Offset: 0x00010114
		public float innerSpotAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_innerSpotAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_innerSpotAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00011F38 File Offset: 0x00010138
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00011F60 File Offset: 0x00010160
		public Color color
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				Light.get_color_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_color_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00011F84 File Offset: 0x00010184
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00011FA8 File Offset: 0x000101A8
		public float colorTemperature
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_colorTemperature_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_colorTemperature_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00011FCC File Offset: 0x000101CC
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x00011FF0 File Offset: 0x000101F0
		public bool useColorTemperature
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_useColorTemperature_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_useColorTemperature_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00012014 File Offset: 0x00010214
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x00012038 File Offset: 0x00010238
		public float intensity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_intensity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_intensity_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0001205C File Offset: 0x0001025C
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x00012080 File Offset: 0x00010280
		public float bounceIntensity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_bounceIntensity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_bounceIntensity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x000120A4 File Offset: 0x000102A4
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x000120C8 File Offset: 0x000102C8
		public LightUnit lightUnit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_lightUnit_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_lightUnit_Injected(intPtr, value);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x000120EC File Offset: 0x000102EC
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x00012110 File Offset: 0x00010310
		public float luxAtDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_luxAtDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_luxAtDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00012134 File Offset: 0x00010334
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00012158 File Offset: 0x00010358
		public bool enableSpotReflector
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_enableSpotReflector_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_enableSpotReflector_Injected(intPtr, value);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001217C File Offset: 0x0001037C
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x000121A0 File Offset: 0x000103A0
		public bool useBoundingSphereOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_useBoundingSphereOverride_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_useBoundingSphereOverride_Injected(intPtr, value);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x000121C4 File Offset: 0x000103C4
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x000121EC File Offset: 0x000103EC
		public Vector4 boundingSphereOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				Light.get_boundingSphereOverride_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_boundingSphereOverride_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00012210 File Offset: 0x00010410
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00012234 File Offset: 0x00010434
		public bool useViewFrustumForShadowCasterCull
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_useViewFrustumForShadowCasterCull_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_useViewFrustumForShadowCasterCull_Injected(intPtr, value);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00012258 File Offset: 0x00010458
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0001227C File Offset: 0x0001047C
		public bool forceVisible
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_forceVisible_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_forceVisible_Injected(intPtr, value);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000122A0 File Offset: 0x000104A0
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x000122C4 File Offset: 0x000104C4
		public int shadowCustomResolution
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowCustomResolution_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowCustomResolution_Injected(intPtr, value);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000122E8 File Offset: 0x000104E8
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0001230C File Offset: 0x0001050C
		public float shadowBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowBias_Injected(intPtr, value);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00012330 File Offset: 0x00010530
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x00012354 File Offset: 0x00010554
		public float shadowNormalBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowNormalBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowNormalBias_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00012378 File Offset: 0x00010578
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0001239C File Offset: 0x0001059C
		public float shadowNearPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowNearPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowNearPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x000123C0 File Offset: 0x000105C0
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x000123E4 File Offset: 0x000105E4
		public bool useShadowMatrixOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_useShadowMatrixOverride_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_useShadowMatrixOverride_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00012408 File Offset: 0x00010608
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x00012430 File Offset: 0x00010630
		public Matrix4x4 shadowMatrixOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Light.get_shadowMatrixOverride_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowMatrixOverride_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00012454 File Offset: 0x00010654
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x00012478 File Offset: 0x00010678
		public float range
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_range_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_range_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001249C File Offset: 0x0001069C
		public float dilatedRange
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_dilatedRange_Injected(intPtr);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x000124C0 File Offset: 0x000106C0
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x000124E8 File Offset: 0x000106E8
		public Flare flare
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Flare>(Light.get_flare_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_flare_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Flare>(value));
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00012510 File Offset: 0x00010710
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x00012538 File Offset: 0x00010738
		public LightBakingOutput bakingOutput
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightBakingOutput lightBakingOutput;
				Light.get_bakingOutput_Injected(intPtr, out lightBakingOutput);
				return lightBakingOutput;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_bakingOutput_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001255C File Offset: 0x0001075C
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00012580 File Offset: 0x00010780
		public int cullingMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_cullingMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_cullingMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x000125A4 File Offset: 0x000107A4
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x000125C8 File Offset: 0x000107C8
		public int renderingLayerMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_renderingLayerMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_renderingLayerMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x000125EC File Offset: 0x000107EC
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00012610 File Offset: 0x00010810
		public LightShadowCasterMode lightShadowCasterMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_lightShadowCasterMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_lightShadowCasterMode_Injected(intPtr, value);
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00012634 File Offset: 0x00010834
		public void Reset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.Reset_Injected(intPtr);
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00012658 File Offset: 0x00010858
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0001267C File Offset: 0x0001087C
		public LightShadows shadows
		{
			[NativeMethod("GetShadowType")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadows_Injected(intPtr);
			}
			[FreeFunction("Light_Bindings::SetShadowType", HasExplicitThis = true, ThrowsException = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadows_Injected(intPtr, value);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000126A0 File Offset: 0x000108A0
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x000126C4 File Offset: 0x000108C4
		public float shadowStrength
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowStrength_Injected(intPtr);
			}
			[FreeFunction("Light_Bindings::SetShadowStrength", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowStrength_Injected(intPtr, value);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000126E8 File Offset: 0x000108E8
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x0001270C File Offset: 0x0001090C
		public LightShadowResolution shadowResolution
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowResolution_Injected(intPtr);
			}
			[FreeFunction("Light_Bindings::SetShadowResolution", HasExplicitThis = true, ThrowsException = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowResolution_Injected(intPtr, value);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00012730 File Offset: 0x00010930
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00003D56 File Offset: 0x00001F56
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		public float shadowSoftness
		{
			get
			{
				return 4f;
			}
			set
			{
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00012748 File Offset: 0x00010948
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x00003D56 File Offset: 0x00001F56
		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float shadowSoftnessFade
		{
			get
			{
				return 1f;
			}
			set
			{
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00012760 File Offset: 0x00010960
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x000127A4 File Offset: 0x000109A4
		public unsafe float[] layerShadowCullDistances
		{
			[FreeFunction("Light_Bindings::GetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = false)]
			get
			{
				float[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Light.get_layerShadowCullDistances_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					float[] array;
					blittableArrayWrapper.Unmarshal<float>(ref array);
					array2 = array;
				}
				return array2;
			}
			[FreeFunction("Light_Bindings::SetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<float> span = new Span<float>(value);
				fixed (float* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Light.set_layerShadowCullDistances_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x000127EC File Offset: 0x000109EC
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x00012810 File Offset: 0x00010A10
		public float cookieSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_cookieSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_cookieSize_Injected(intPtr, value);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00012834 File Offset: 0x00010A34
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0001285C File Offset: 0x00010A5C
		public Texture cookie
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture>(Light.get_cookie_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_cookie_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(value));
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00012884 File Offset: 0x00010A84
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x000128A8 File Offset: 0x00010AA8
		public LightRenderMode renderMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_renderMode_Injected(intPtr);
			}
			[FreeFunction("Light_Bindings::SetRenderMode", HasExplicitThis = true, ThrowsException = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_renderMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x000128CC File Offset: 0x00010ACC
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x000128E4 File Offset: 0x00010AE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("warning bakedIndex has been removed please use bakingOutput.isBaked instead.", true)]
		public int bakedIndex
		{
			get
			{
				return this.m_BakedIndex;
			}
			set
			{
				this.m_BakedIndex = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x000128F0 File Offset: 0x00010AF0
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x00012918 File Offset: 0x00010B18
		public Vector2 areaSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Light.get_areaSize_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_areaSize_Injected(intPtr, ref value);
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001293C File Offset: 0x00010B3C
		public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer)
		{
			this.AddCommandBuffer(evt, buffer, ShadowMapPass.All);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00012950 File Offset: 0x00010B50
		public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.AddCommandBuffer only with the built-in renderer.");
			}
			this.AddCommandBufferInternal(evt, buffer, shadowPassMask);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00012984 File Offset: 0x00010B84
		[FreeFunction("Light_Bindings::AddCommandBuffer", HasExplicitThis = true)]
		internal void AddCommandBufferInternal(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.AddCommandBufferInternal_Injected(intPtr, evt, (buffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(buffer), shadowPassMask);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000129B7 File Offset: 0x00010BB7
		public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			this.AddCommandBufferAsync(evt, buffer, ShadowMapPass.All, queueType);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000129CC File Offset: 0x00010BCC
		public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.AddCommandBufferAsync only with the built-in renderer.");
			}
			this.AddCommandBufferAsyncInternal(evt, buffer, shadowPassMask, queueType);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00012A00 File Offset: 0x00010C00
		[FreeFunction("Light_Bindings::AddCommandBufferAsync", HasExplicitThis = true)]
		internal void AddCommandBufferAsyncInternal(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.AddCommandBufferAsyncInternal_Injected(intPtr, evt, (buffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(buffer), shadowPassMask, queueType);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00012A38 File Offset: 0x00010C38
		public void RemoveCommandBuffer(LightEvent evt, CommandBuffer buffer)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.RemoveCommandBuffer only with the built-in renderer.");
			}
			this.RemoveCommandBufferInternal(evt, buffer);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00012A68 File Offset: 0x00010C68
		[NativeMethod("RemoveCommandBuffer")]
		internal void RemoveCommandBufferInternal(LightEvent evt, CommandBuffer buffer)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.RemoveCommandBufferInternal_Injected(intPtr, evt, (buffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00012A9C File Offset: 0x00010C9C
		public void RemoveCommandBuffers(LightEvent evt)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.RemoveCommandBuffer only with the built-in renderer.");
			}
			this.RemoveCommandBuffersInternal(evt);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00012ACC File Offset: 0x00010CCC
		[NativeMethod("RemoveCommandBuffers")]
		internal void RemoveCommandBuffersInternal(LightEvent evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.RemoveCommandBuffersInternal_Injected(intPtr, evt);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00012AF0 File Offset: 0x00010CF0
		public void RemoveAllCommandBuffers()
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.RemoveAllCommandBuffers only with the built-in renderer.");
			}
			this.RemoveAllCommandBuffersInternal();
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00012B20 File Offset: 0x00010D20
		[NativeMethod("RemoveAllCommandBuffers")]
		internal void RemoveAllCommandBuffersInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.RemoveAllCommandBuffersInternal_Injected(intPtr);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00012B44 File Offset: 0x00010D44
		public CommandBuffer[] GetCommandBuffers(LightEvent evt)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.GetCommandBuffers only with the built-in renderer.");
			}
			return this.GetCommandBuffersInternal(evt);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00012B78 File Offset: 0x00010D78
		[FreeFunction("Light_Bindings::GetCommandBuffers", HasExplicitThis = true)]
		internal CommandBuffer[] GetCommandBuffersInternal(LightEvent evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Light.GetCommandBuffersInternal_Injected(intPtr, evt);
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00012B9C File Offset: 0x00010D9C
		public int commandBufferCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_commandBufferCount_Injected(intPtr);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00012BC0 File Offset: 0x00010DC0
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x00012BD7 File Offset: 0x00010DD7
		[Obsolete("Use QualitySettings.pixelLightCount instead.")]
		public static int pixelLightCount
		{
			get
			{
				return QualitySettings.pixelLightCount;
			}
			set
			{
				QualitySettings.pixelLightCount = value;
			}
		}

		// Token: 0x060009B0 RID: 2480
		[FreeFunction("Light_Bindings::GetLights")]
		[Obsolete("Light.GetLights has been deprecated, use FindObjectsOfType in combination with light.cullingmask/light.type", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Light[] GetLights(LightType type, int layer);

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00012BE4 File Offset: 0x00010DE4
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x00003D56 File Offset: 0x00001F56
		[Obsolete("light.shadowConstantBias was removed, use light.shadowBias", true)]
		public float shadowConstantBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00012BFC File Offset: 0x00010DFC
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00003D56 File Offset: 0x00001F56
		[Obsolete("light.shadowObjectSizeBias was removed, use light.shadowBias", true)]
		public float shadowObjectSizeBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00012C14 File Offset: 0x00010E14
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00003D56 File Offset: 0x00001F56
		[Obsolete("light.attenuate was removed; all lights always attenuate now", true)]
		public bool attenuate
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x060009B8 RID: 2488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightType get_type_Injected(IntPtr _unity_self);

		// Token: 0x060009B9 RID: 2489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_type_Injected(IntPtr _unity_self, LightType value);

		// Token: 0x060009BA RID: 2490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spotAngle_Injected(IntPtr _unity_self);

		// Token: 0x060009BB RID: 2491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spotAngle_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009BC RID: 2492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_innerSpotAngle_Injected(IntPtr _unity_self);

		// Token: 0x060009BD RID: 2493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_innerSpotAngle_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009BE RID: 2494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x060009BF RID: 2495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060009C0 RID: 2496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_colorTemperature_Injected(IntPtr _unity_self);

		// Token: 0x060009C1 RID: 2497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colorTemperature_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009C2 RID: 2498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useColorTemperature_Injected(IntPtr _unity_self);

		// Token: 0x060009C3 RID: 2499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useColorTemperature_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060009C4 RID: 2500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_intensity_Injected(IntPtr _unity_self);

		// Token: 0x060009C5 RID: 2501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_intensity_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009C6 RID: 2502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_bounceIntensity_Injected(IntPtr _unity_self);

		// Token: 0x060009C7 RID: 2503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bounceIntensity_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009C8 RID: 2504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightUnit get_lightUnit_Injected(IntPtr _unity_self);

		// Token: 0x060009C9 RID: 2505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lightUnit_Injected(IntPtr _unity_self, LightUnit value);

		// Token: 0x060009CA RID: 2506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_luxAtDistance_Injected(IntPtr _unity_self);

		// Token: 0x060009CB RID: 2507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_luxAtDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009CC RID: 2508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableSpotReflector_Injected(IntPtr _unity_self);

		// Token: 0x060009CD RID: 2509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableSpotReflector_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060009CE RID: 2510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useBoundingSphereOverride_Injected(IntPtr _unity_self);

		// Token: 0x060009CF RID: 2511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useBoundingSphereOverride_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060009D0 RID: 2512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_boundingSphereOverride_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x060009D1 RID: 2513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_boundingSphereOverride_Injected(IntPtr _unity_self, [In] ref Vector4 value);

		// Token: 0x060009D2 RID: 2514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useViewFrustumForShadowCasterCull_Injected(IntPtr _unity_self);

		// Token: 0x060009D3 RID: 2515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useViewFrustumForShadowCasterCull_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060009D4 RID: 2516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_forceVisible_Injected(IntPtr _unity_self);

		// Token: 0x060009D5 RID: 2517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceVisible_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060009D6 RID: 2518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_shadowCustomResolution_Injected(IntPtr _unity_self);

		// Token: 0x060009D7 RID: 2519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowCustomResolution_Injected(IntPtr _unity_self, int value);

		// Token: 0x060009D8 RID: 2520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowBias_Injected(IntPtr _unity_self);

		// Token: 0x060009D9 RID: 2521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009DA RID: 2522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowNormalBias_Injected(IntPtr _unity_self);

		// Token: 0x060009DB RID: 2523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowNormalBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009DC RID: 2524
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowNearPlane_Injected(IntPtr _unity_self);

		// Token: 0x060009DD RID: 2525
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowNearPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009DE RID: 2526
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useShadowMatrixOverride_Injected(IntPtr _unity_self);

		// Token: 0x060009DF RID: 2527
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useShadowMatrixOverride_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060009E0 RID: 2528
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_shadowMatrixOverride_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060009E1 RID: 2529
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowMatrixOverride_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x060009E2 RID: 2530
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_range_Injected(IntPtr _unity_self);

		// Token: 0x060009E3 RID: 2531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_range_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009E4 RID: 2532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dilatedRange_Injected(IntPtr _unity_self);

		// Token: 0x060009E5 RID: 2533
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_flare_Injected(IntPtr _unity_self);

		// Token: 0x060009E6 RID: 2534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flare_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060009E7 RID: 2535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bakingOutput_Injected(IntPtr _unity_self, out LightBakingOutput ret);

		// Token: 0x060009E8 RID: 2536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bakingOutput_Injected(IntPtr _unity_self, [In] ref LightBakingOutput value);

		// Token: 0x060009E9 RID: 2537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_cullingMask_Injected(IntPtr _unity_self);

		// Token: 0x060009EA RID: 2538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x060009EB RID: 2539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_renderingLayerMask_Injected(IntPtr _unity_self);

		// Token: 0x060009EC RID: 2540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderingLayerMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x060009ED RID: 2541
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightShadowCasterMode get_lightShadowCasterMode_Injected(IntPtr _unity_self);

		// Token: 0x060009EE RID: 2542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lightShadowCasterMode_Injected(IntPtr _unity_self, LightShadowCasterMode value);

		// Token: 0x060009EF RID: 2543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Reset_Injected(IntPtr _unity_self);

		// Token: 0x060009F0 RID: 2544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightShadows get_shadows_Injected(IntPtr _unity_self);

		// Token: 0x060009F1 RID: 2545
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadows_Injected(IntPtr _unity_self, LightShadows value);

		// Token: 0x060009F2 RID: 2546
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowStrength_Injected(IntPtr _unity_self);

		// Token: 0x060009F3 RID: 2547
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowStrength_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009F4 RID: 2548
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightShadowResolution get_shadowResolution_Injected(IntPtr _unity_self);

		// Token: 0x060009F5 RID: 2549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowResolution_Injected(IntPtr _unity_self, LightShadowResolution value);

		// Token: 0x060009F6 RID: 2550
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_layerShadowCullDistances_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060009F7 RID: 2551
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layerShadowCullDistances_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060009F8 RID: 2552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_cookieSize_Injected(IntPtr _unity_self);

		// Token: 0x060009F9 RID: 2553
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cookieSize_Injected(IntPtr _unity_self, float value);

		// Token: 0x060009FA RID: 2554
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_cookie_Injected(IntPtr _unity_self);

		// Token: 0x060009FB RID: 2555
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cookie_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060009FC RID: 2556
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightRenderMode get_renderMode_Injected(IntPtr _unity_self);

		// Token: 0x060009FD RID: 2557
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderMode_Injected(IntPtr _unity_self, LightRenderMode value);

		// Token: 0x060009FE RID: 2558
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_areaSize_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060009FF RID: 2559
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_areaSize_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000A00 RID: 2560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCommandBufferInternal_Injected(IntPtr _unity_self, LightEvent evt, IntPtr buffer, ShadowMapPass shadowPassMask);

		// Token: 0x06000A01 RID: 2561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCommandBufferAsyncInternal_Injected(IntPtr _unity_self, LightEvent evt, IntPtr buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType);

		// Token: 0x06000A02 RID: 2562
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveCommandBufferInternal_Injected(IntPtr _unity_self, LightEvent evt, IntPtr buffer);

		// Token: 0x06000A03 RID: 2563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveCommandBuffersInternal_Injected(IntPtr _unity_self, LightEvent evt);

		// Token: 0x06000A04 RID: 2564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveAllCommandBuffersInternal_Injected(IntPtr _unity_self);

		// Token: 0x06000A05 RID: 2565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CommandBuffer[] GetCommandBuffersInternal_Injected(IntPtr _unity_self, LightEvent evt);

		// Token: 0x06000A06 RID: 2566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_commandBufferCount_Injected(IntPtr _unity_self);

		// Token: 0x040002D3 RID: 723
		private int m_BakedIndex;
	}
}
