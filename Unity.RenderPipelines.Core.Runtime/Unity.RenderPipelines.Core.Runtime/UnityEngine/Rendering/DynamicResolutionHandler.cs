using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000046 RID: 70
	public class DynamicResolutionHandler
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x00007F58 File Offset: 0x00006158
		private void Reset()
		{
			this.m_Enabled = false;
			this.m_UseMipBias = false;
			this.m_MinScreenFraction = 1f;
			this.m_MaxScreenFraction = 1f;
			this.m_CurrentFraction = 1f;
			this.m_ForcingRes = false;
			this.m_CurrentCameraRequest = true;
			this.m_PrevFraction = -1f;
			this.m_ForceSoftwareFallback = false;
			this.m_RunUpscalerFilterOnFullResolution = false;
			this.m_PrevHWScaleWidth = 1f;
			this.m_PrevHWScaleHeight = 1f;
			this.m_LastScaledSize = new Vector2Int(0, 0);
			this.filter = DynamicResUpscaleFilter.CatmullRom;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00007FE5 File Offset: 0x000061E5
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00007FED File Offset: 0x000061ED
		public DynamicResUpscaleFilter filter { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00007FF6 File Offset: 0x000061F6
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00007FFE File Offset: 0x000061FE
		public Vector2Int finalViewport { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00008010 File Offset: 0x00006210
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00008007 File Offset: 0x00006207
		public bool runUpscalerFilterOnFullResolution
		{
			get
			{
				return this.m_RunUpscalerFilterOnFullResolution || this.filter == DynamicResUpscaleFilter.EdgeAdaptiveScalingUpres;
			}
			set
			{
				this.m_RunUpscalerFilterOnFullResolution = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00008025 File Offset: 0x00006225
		public bool forcingResolution
		{
			get
			{
				return this.m_ForcingRes;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00008030 File Offset: 0x00006230
		private bool FlushScalableBufferManagerState()
		{
			if (DynamicResolutionHandler.s_GlobalHwUpresActive == this.HardwareDynamicResIsEnabled() && DynamicResolutionHandler.s_GlobalHwFraction == this.m_CurrentFraction)
			{
				return false;
			}
			DynamicResolutionHandler.s_GlobalHwUpresActive = this.HardwareDynamicResIsEnabled();
			DynamicResolutionHandler.s_GlobalHwFraction = this.m_CurrentFraction;
			float num = (DynamicResolutionHandler.s_GlobalHwUpresActive ? DynamicResolutionHandler.s_GlobalHwFraction : 1f);
			ScalableBufferManager.ResizeBuffers(num, num);
			return true;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000808C File Offset: 0x0000628C
		private static DynamicResolutionHandler GetOrCreateDrsInstanceHandler(Camera camera)
		{
			if (camera == null)
			{
				return null;
			}
			DynamicResolutionHandler instance = null;
			int key = camera.GetInstanceID();
			if (!DynamicResolutionHandler.s_CameraInstances.TryGetValue(key, out instance))
			{
				if (DynamicResolutionHandler.s_CameraInstances.Count >= 32)
				{
					int recycledInstanceKey = 0;
					DynamicResolutionHandler recycledInstance = null;
					foreach (KeyValuePair<int, DynamicResolutionHandler> kv in DynamicResolutionHandler.s_CameraInstances)
					{
						if (kv.Value.m_OwnerCameraWeakRef == null || !kv.Value.m_OwnerCameraWeakRef.IsAlive)
						{
							recycledInstance = kv.Value;
							recycledInstanceKey = kv.Key;
							break;
						}
					}
					if (recycledInstance != null)
					{
						instance = recycledInstance;
						DynamicResolutionHandler.s_CameraInstances.Remove(recycledInstanceKey);
						DynamicResolutionHandler.s_CameraUpscaleFilters.Remove(recycledInstanceKey);
					}
				}
				if (instance == null)
				{
					instance = new DynamicResolutionHandler();
					instance.m_OwnerCameraWeakRef = new WeakReference(camera);
				}
				else
				{
					instance.Reset();
					instance.m_OwnerCameraWeakRef.Target = camera;
				}
				DynamicResolutionHandler.s_CameraInstances.Add(key, instance);
			}
			return instance;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000081A5 File Offset: 0x000063A5
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000819C File Offset: 0x0000639C
		public DynamicResolutionHandler.UpsamplerScheduleType upsamplerSchedule
		{
			get
			{
				return this.m_UpsamplerSchedule;
			}
			set
			{
				this.m_UpsamplerSchedule = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000081AD File Offset: 0x000063AD
		public static DynamicResolutionHandler instance
		{
			get
			{
				return DynamicResolutionHandler.s_ActiveInstance;
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000081B4 File Offset: 0x000063B4
		private DynamicResolutionHandler()
		{
			this.Reset();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000081D4 File Offset: 0x000063D4
		private static float DefaultDynamicResMethod()
		{
			return 1f;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000081DC File Offset: 0x000063DC
		private void ProcessSettings(GlobalDynamicResolutionSettings settings)
		{
			this.m_Enabled = settings.enabled && (Application.isPlaying || settings.forceResolution);
			if (!this.m_Enabled)
			{
				this.m_CurrentFraction = 1f;
			}
			else
			{
				this.type = settings.dynResType;
				this.m_UseMipBias = settings.useMipBias;
				float minScreenFrac = Mathf.Clamp(settings.minPercentage / 100f, 0.1f, 1f);
				this.m_MinScreenFraction = minScreenFrac;
				float maxScreenFrac = Mathf.Clamp(settings.maxPercentage / 100f, this.m_MinScreenFraction, 3f);
				this.m_MaxScreenFraction = maxScreenFrac;
				DynamicResUpscaleFilter requestedFilter;
				this.filter = (DynamicResolutionHandler.s_CameraUpscaleFilters.TryGetValue(DynamicResolutionHandler.s_ActiveCameraId, out requestedFilter) ? requestedFilter : settings.upsampleFilter);
				this.m_ForcingRes = settings.forceResolution;
				if (this.m_ForcingRes)
				{
					float fraction = Mathf.Clamp(settings.forcedPercentage / 100f, 0.1f, 1.5f);
					this.m_CurrentFraction = fraction;
				}
			}
			this.m_CachedSettings = settings;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000082E8 File Offset: 0x000064E8
		public Vector2 GetResolvedScale()
		{
			if (!this.m_Enabled || !this.m_CurrentCameraRequest)
			{
				return new Vector2(1f, 1f);
			}
			float scaleFractionX = this.m_CurrentFraction;
			float scaleFractionY = this.m_CurrentFraction;
			if (!this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware)
			{
				scaleFractionX = ScalableBufferManager.widthScaleFactor;
				scaleFractionY = ScalableBufferManager.heightScaleFactor;
			}
			return new Vector2(scaleFractionX, scaleFractionY);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00008347 File Offset: 0x00006547
		public float CalculateMipBias(Vector2Int inputResolution, Vector2Int outputResolution, bool forceApply = false)
		{
			if (!this.m_UseMipBias && !forceApply)
			{
				return 0f;
			}
			return (float)Math.Log((double)inputResolution.x / (double)outputResolution.x, 2.0);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000837C File Offset: 0x0000657C
		public static void SetDynamicResScaler(PerformDynamicRes scaler, DynamicResScalePolicyType scalerType = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
		{
			DynamicResolutionHandler.s_ScalerContainers[0] = new DynamicResolutionHandler.ScalerContainer
			{
				type = scalerType,
				method = scaler
			};
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000083B0 File Offset: 0x000065B0
		public static void SetSystemDynamicResScaler(PerformDynamicRes scaler, DynamicResScalePolicyType scalerType = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
		{
			DynamicResolutionHandler.s_ScalerContainers[1] = new DynamicResolutionHandler.ScalerContainer
			{
				type = scalerType,
				method = scaler
			};
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000083E1 File Offset: 0x000065E1
		public static void SetActiveDynamicScalerSlot(DynamicResScalerSlot slot)
		{
			DynamicResolutionHandler.s_ActiveScalerSlot = slot;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000083E9 File Offset: 0x000065E9
		public static void ClearSelectedCamera()
		{
			DynamicResolutionHandler.s_ActiveInstance = DynamicResolutionHandler.s_DefaultInstance;
			DynamicResolutionHandler.s_ActiveCameraId = 0;
			DynamicResolutionHandler.s_ActiveInstanceDirty = true;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00008404 File Offset: 0x00006604
		public static void SetUpscaleFilter(Camera camera, DynamicResUpscaleFilter filter)
		{
			int cameraID = camera.GetInstanceID();
			if (DynamicResolutionHandler.s_CameraUpscaleFilters.ContainsKey(cameraID))
			{
				DynamicResolutionHandler.s_CameraUpscaleFilters[cameraID] = filter;
				return;
			}
			DynamicResolutionHandler.s_CameraUpscaleFilters.Add(cameraID, filter);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000843E File Offset: 0x0000663E
		public void SetCurrentCameraRequest(bool cameraRequest)
		{
			this.m_CurrentCameraRequest = cameraRequest;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00008448 File Offset: 0x00006648
		public static void UpdateAndUseCamera(Camera camera, GlobalDynamicResolutionSettings? settings = null, Action OnResolutionChange = null)
		{
			int newCameraId;
			if (camera == null)
			{
				DynamicResolutionHandler.s_ActiveInstance = DynamicResolutionHandler.s_DefaultInstance;
				newCameraId = 0;
			}
			else
			{
				DynamicResolutionHandler.s_ActiveInstance = DynamicResolutionHandler.GetOrCreateDrsInstanceHandler(camera);
				newCameraId = camera.GetInstanceID();
			}
			DynamicResolutionHandler.s_ActiveInstanceDirty = newCameraId != DynamicResolutionHandler.s_ActiveCameraId;
			DynamicResolutionHandler.s_ActiveCameraId = newCameraId;
			DynamicResolutionHandler.s_ActiveInstance.Update((settings != null) ? settings.Value : DynamicResolutionHandler.s_ActiveInstance.m_CachedSettings, OnResolutionChange);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000084BC File Offset: 0x000066BC
		public void Update(GlobalDynamicResolutionSettings settings, Action OnResolutionChange = null)
		{
			this.ProcessSettings(settings);
			if (!this.m_Enabled || !DynamicResolutionHandler.s_ActiveInstanceDirty)
			{
				this.FlushScalableBufferManagerState();
				DynamicResolutionHandler.s_ActiveInstanceDirty = false;
				return;
			}
			if (!this.m_ForcingRes)
			{
				ref DynamicResolutionHandler.ScalerContainer scaler = ref DynamicResolutionHandler.s_ScalerContainers[(int)DynamicResolutionHandler.s_ActiveScalerSlot];
				if (scaler.type == DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
				{
					float lerpFactor = Mathf.Clamp(scaler.method(), 0f, 1f);
					this.m_CurrentFraction = Mathf.Lerp(this.m_MinScreenFraction, this.m_MaxScreenFraction, lerpFactor);
				}
				else if (scaler.type == DynamicResScalePolicyType.ReturnsPercentage)
				{
					float percentageRequested = Mathf.Max(scaler.method(), 5f);
					this.m_CurrentFraction = Mathf.Clamp(percentageRequested / 100f, this.m_MinScreenFraction, this.m_MaxScreenFraction);
				}
			}
			bool hardwareResolutionChanged = false;
			bool flag = this.m_CurrentFraction != this.m_PrevFraction;
			this.m_PrevFraction = this.m_CurrentFraction;
			if (!this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware)
			{
				hardwareResolutionChanged = this.FlushScalableBufferManagerState();
				if (ScalableBufferManager.widthScaleFactor != this.m_PrevHWScaleWidth || ScalableBufferManager.heightScaleFactor != this.m_PrevHWScaleHeight)
				{
					hardwareResolutionChanged = true;
				}
			}
			if ((flag || hardwareResolutionChanged) && OnResolutionChange != null)
			{
				OnResolutionChange();
			}
			DynamicResolutionHandler.s_ActiveInstanceDirty = false;
			this.m_PrevHWScaleWidth = ScalableBufferManager.widthScaleFactor;
			this.m_PrevHWScaleHeight = ScalableBufferManager.heightScaleFactor;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000085FE File Offset: 0x000067FE
		public bool SoftwareDynamicResIsEnabled()
		{
			return this.m_CurrentCameraRequest && this.m_Enabled && (this.m_CurrentFraction != 1f || this.runUpscalerFilterOnFullResolution) && (this.m_ForceSoftwareFallback || this.type == DynamicResolutionType.Software);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000863A File Offset: 0x0000683A
		public bool HardwareDynamicResIsEnabled()
		{
			return !this.m_ForceSoftwareFallback && this.m_CurrentCameraRequest && this.m_Enabled && this.type == DynamicResolutionType.Hardware;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000865F File Offset: 0x0000685F
		public bool RequestsHardwareDynamicResolution()
		{
			return !this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00008674 File Offset: 0x00006874
		public bool DynamicResolutionEnabled()
		{
			return this.m_CurrentCameraRequest && this.m_Enabled && (this.m_CurrentFraction != 1f || this.runUpscalerFilterOnFullResolution);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000869D File Offset: 0x0000689D
		public void ForceSoftwareFallback()
		{
			this.m_ForceSoftwareFallback = true;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000086A8 File Offset: 0x000068A8
		public Vector2Int GetScaledSize(Vector2Int size)
		{
			this.cachedOriginalSize = size;
			if (!this.m_Enabled || !this.m_CurrentCameraRequest)
			{
				return size;
			}
			Vector2Int scaledSize = this.ApplyScalesOnSize(size);
			this.m_LastScaledSize = scaledSize;
			return scaledSize;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000086DE File Offset: 0x000068DE
		public Vector2Int ApplyScalesOnSize(Vector2Int size)
		{
			return this.ApplyScalesOnSize(size, this.GetResolvedScale());
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000086F0 File Offset: 0x000068F0
		internal Vector2Int ApplyScalesOnSize(Vector2Int size, Vector2 scales)
		{
			Vector2Int scaledSize = new Vector2Int(Mathf.CeilToInt((float)size.x * scales.x), Mathf.CeilToInt((float)size.y * scales.y));
			if (this.m_ForceSoftwareFallback || this.type != DynamicResolutionType.Hardware)
			{
				scaledSize.x += 1 & scaledSize.x;
				scaledSize.y += 1 & scaledSize.y;
			}
			scaledSize.x = Math.Min(scaledSize.x, size.x);
			scaledSize.y = Math.Min(scaledSize.y, size.y);
			return scaledSize;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000087A0 File Offset: 0x000069A0
		public float GetCurrentScale()
		{
			if (!this.m_Enabled || !this.m_CurrentCameraRequest)
			{
				return 1f;
			}
			return this.m_CurrentFraction;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000087BE File Offset: 0x000069BE
		public Vector2Int GetLastScaledSize()
		{
			return this.m_LastScaledSize;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000087C6 File Offset: 0x000069C6
		public float GetLowResMultiplier(float targetLowRes)
		{
			return this.GetLowResMultiplier(targetLowRes, this.m_CachedSettings.lowResTransparencyMinimumThreshold);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000087DC File Offset: 0x000069DC
		public float GetLowResMultiplier(float targetLowRes, float minimumThreshold)
		{
			if (!this.m_Enabled)
			{
				return targetLowRes;
			}
			float thresholdPercentage = Math.Min(minimumThreshold / 100f, targetLowRes);
			if (targetLowRes * this.m_CurrentFraction >= thresholdPercentage)
			{
				return targetLowRes;
			}
			return Mathf.Clamp(thresholdPercentage / this.m_CurrentFraction, 0f, 1f);
		}

		// Token: 0x040000D4 RID: 212
		private bool m_Enabled;

		// Token: 0x040000D5 RID: 213
		private bool m_UseMipBias;

		// Token: 0x040000D6 RID: 214
		private float m_MinScreenFraction;

		// Token: 0x040000D7 RID: 215
		private float m_MaxScreenFraction;

		// Token: 0x040000D8 RID: 216
		private float m_CurrentFraction;

		// Token: 0x040000D9 RID: 217
		private bool m_ForcingRes;

		// Token: 0x040000DA RID: 218
		private bool m_CurrentCameraRequest;

		// Token: 0x040000DB RID: 219
		private float m_PrevFraction;

		// Token: 0x040000DC RID: 220
		private bool m_ForceSoftwareFallback;

		// Token: 0x040000DD RID: 221
		private bool m_RunUpscalerFilterOnFullResolution;

		// Token: 0x040000DE RID: 222
		private float m_PrevHWScaleWidth;

		// Token: 0x040000DF RID: 223
		private float m_PrevHWScaleHeight;

		// Token: 0x040000E0 RID: 224
		private Vector2Int m_LastScaledSize;

		// Token: 0x040000E1 RID: 225
		private static DynamicResScalerSlot s_ActiveScalerSlot = DynamicResScalerSlot.User;

		// Token: 0x040000E2 RID: 226
		private static DynamicResolutionHandler.ScalerContainer[] s_ScalerContainers = new DynamicResolutionHandler.ScalerContainer[]
		{
			new DynamicResolutionHandler.ScalerContainer
			{
				type = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor,
				method = new PerformDynamicRes(DynamicResolutionHandler.DefaultDynamicResMethod)
			},
			new DynamicResolutionHandler.ScalerContainer
			{
				type = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor,
				method = new PerformDynamicRes(DynamicResolutionHandler.DefaultDynamicResMethod)
			}
		};

		// Token: 0x040000E3 RID: 227
		private Vector2Int cachedOriginalSize;

		// Token: 0x040000E5 RID: 229
		private static Dictionary<int, DynamicResUpscaleFilter> s_CameraUpscaleFilters = new Dictionary<int, DynamicResUpscaleFilter>();

		// Token: 0x040000E7 RID: 231
		private DynamicResolutionType type;

		// Token: 0x040000E8 RID: 232
		private GlobalDynamicResolutionSettings m_CachedSettings = GlobalDynamicResolutionSettings.NewDefault();

		// Token: 0x040000E9 RID: 233
		private const int CameraDictionaryMaxcCapacity = 32;

		// Token: 0x040000EA RID: 234
		private WeakReference m_OwnerCameraWeakRef;

		// Token: 0x040000EB RID: 235
		private static Dictionary<int, DynamicResolutionHandler> s_CameraInstances = new Dictionary<int, DynamicResolutionHandler>(32);

		// Token: 0x040000EC RID: 236
		private static DynamicResolutionHandler s_DefaultInstance = new DynamicResolutionHandler();

		// Token: 0x040000ED RID: 237
		private static int s_ActiveCameraId = 0;

		// Token: 0x040000EE RID: 238
		private static DynamicResolutionHandler s_ActiveInstance = DynamicResolutionHandler.s_DefaultInstance;

		// Token: 0x040000EF RID: 239
		private static bool s_ActiveInstanceDirty = true;

		// Token: 0x040000F0 RID: 240
		private static float s_GlobalHwFraction = 1f;

		// Token: 0x040000F1 RID: 241
		private static bool s_GlobalHwUpresActive = false;

		// Token: 0x040000F2 RID: 242
		private DynamicResolutionHandler.UpsamplerScheduleType m_UpsamplerSchedule = DynamicResolutionHandler.UpsamplerScheduleType.AfterPost;

		// Token: 0x02000047 RID: 71
		private struct ScalerContainer
		{
			// Token: 0x040000F3 RID: 243
			public DynamicResScalePolicyType type;

			// Token: 0x040000F4 RID: 244
			public PerformDynamicRes method;
		}

		// Token: 0x02000048 RID: 72
		public enum UpsamplerScheduleType
		{
			// Token: 0x040000F6 RID: 246
			BeforePost,
			// Token: 0x040000F7 RID: 247
			AfterDepthOfField,
			// Token: 0x040000F8 RID: 248
			AfterPost
		}
	}
}
