using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004D RID: 77
	public class DebugDisplaySettingsRendering : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000562D File Offset: 0x0000382D
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00005635 File Offset: 0x00003835
		public DebugWireframeMode wireframeMode
		{
			get
			{
				return this.m_WireframeMode;
			}
			set
			{
				this.m_WireframeMode = value;
				this.UpdateDebugSceneOverrideMode();
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005644 File Offset: 0x00003844
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000564C File Offset: 0x0000384C
		[Obsolete("overdraw has been deprecated. Use overdrawMode instead.", true)]
		public bool overdraw
		{
			get
			{
				return this.m_Overdraw;
			}
			set
			{
				this.m_Overdraw = value;
				this.UpdateDebugSceneOverrideMode();
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000565B File Offset: 0x0000385B
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00005663 File Offset: 0x00003863
		public DebugOverdrawMode overdrawMode
		{
			get
			{
				return this.m_OverdrawMode;
			}
			set
			{
				this.m_OverdrawMode = value;
				this.UpdateDebugSceneOverrideMode();
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005672 File Offset: 0x00003872
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000567A File Offset: 0x0000387A
		public int maxOverdrawCount { get; set; } = 10;

		// Token: 0x06000179 RID: 377 RVA: 0x00005684 File Offset: 0x00003884
		private void UpdateDebugSceneOverrideMode()
		{
			switch (this.wireframeMode)
			{
			case DebugWireframeMode.Wireframe:
				this.sceneOverrideMode = DebugSceneOverrideMode.Wireframe;
				return;
			case DebugWireframeMode.SolidWireframe:
				this.sceneOverrideMode = DebugSceneOverrideMode.SolidWireframe;
				return;
			case DebugWireframeMode.ShadedWireframe:
				this.sceneOverrideMode = DebugSceneOverrideMode.ShadedWireframe;
				return;
			default:
				this.sceneOverrideMode = ((this.overdrawMode != DebugOverdrawMode.None) ? DebugSceneOverrideMode.Overdraw : DebugSceneOverrideMode.None);
				return;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000056D8 File Offset: 0x000038D8
		// (set) Token: 0x0600017B RID: 379 RVA: 0x000056E0 File Offset: 0x000038E0
		public DebugFullScreenMode fullScreenDebugMode { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000056E9 File Offset: 0x000038E9
		// (set) Token: 0x0600017D RID: 381 RVA: 0x000056F1 File Offset: 0x000038F1
		internal int stpDebugViewIndex { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000056FA File Offset: 0x000038FA
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005702 File Offset: 0x00003902
		public int fullScreenDebugModeOutputSizeScreenPercent { get; set; } = 50;

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000570B File Offset: 0x0000390B
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00005713 File Offset: 0x00003913
		internal DebugSceneOverrideMode sceneOverrideMode { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000571C File Offset: 0x0000391C
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00005724 File Offset: 0x00003924
		public DebugMipInfoMode mipInfoMode { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000572D File Offset: 0x0000392D
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00005735 File Offset: 0x00003935
		public bool mipDebugStatusShowCode { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000573E File Offset: 0x0000393E
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00005746 File Offset: 0x00003946
		public DebugMipMapStatusMode mipDebugStatusMode { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000574F File Offset: 0x0000394F
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00005757 File Offset: 0x00003957
		public float mipDebugOpacity { get; set; } = 1f;

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005760 File Offset: 0x00003960
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00005768 File Offset: 0x00003968
		public float mipDebugRecentUpdateCooldown { get; set; } = 3f;

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005771 File Offset: 0x00003971
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00005779 File Offset: 0x00003979
		public int mipDebugMaterialTextureSlot { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005782 File Offset: 0x00003982
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000578A File Offset: 0x0000398A
		public bool showInfoForAllSlots { get; set; } = true;

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005793 File Offset: 0x00003993
		internal bool canAggregateData
		{
			get
			{
				return this.mipInfoMode == DebugMipInfoMode.MipStreamingStatus || this.mipInfoMode == DebugMipInfoMode.MipStreamingActivity;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000057A9 File Offset: 0x000039A9
		// (set) Token: 0x06000192 RID: 402 RVA: 0x000057B1 File Offset: 0x000039B1
		public DebugMipMapModeTerrainTexture mipDebugTerrainTexture { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000057BA File Offset: 0x000039BA
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000057C2 File Offset: 0x000039C2
		public DebugPostProcessingMode postProcessingDebugMode { get; set; } = DebugPostProcessingMode.Auto;

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000057CB File Offset: 0x000039CB
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000057D3 File Offset: 0x000039D3
		public bool enableMsaa { get; set; } = true;

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000057DC File Offset: 0x000039DC
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000057E4 File Offset: 0x000039E4
		public bool enableHDR { get; set; } = true;

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000057ED File Offset: 0x000039ED
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000057F5 File Offset: 0x000039F5
		public DebugDisplaySettingsRendering.TaaDebugMode taaDebugMode { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000057FE File Offset: 0x000039FE
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00005806 File Offset: 0x00003A06
		public DebugValidationMode validationMode { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000580F File Offset: 0x00003A0F
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00005817 File Offset: 0x00003A17
		public PixelValidationChannels validationChannels { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005820 File Offset: 0x00003A20
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00005828 File Offset: 0x00003A28
		public float validationRangeMin { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00005831 File Offset: 0x00003A31
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00005839 File Offset: 0x00003A39
		public float validationRangeMax { get; set; } = 1f;

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00005844 File Offset: 0x00003A44
		public bool AreAnySettingsActive
		{
			get
			{
				return this.postProcessingDebugMode != DebugPostProcessingMode.Auto || this.fullScreenDebugMode != DebugFullScreenMode.None || this.sceneOverrideMode != DebugSceneOverrideMode.None || this.mipInfoMode != DebugMipInfoMode.None || this.validationMode != DebugValidationMode.None || !this.enableMsaa || !this.enableHDR || this.taaDebugMode > DebugDisplaySettingsRendering.TaaDebugMode.None;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005895 File Offset: 0x00003A95
		public bool IsPostProcessingAllowed
		{
			get
			{
				return this.postProcessingDebugMode != DebugPostProcessingMode.Disabled && this.sceneOverrideMode == DebugSceneOverrideMode.None && this.mipInfoMode == DebugMipInfoMode.None;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000058B2 File Offset: 0x00003AB2
		public bool IsLightingActive
		{
			get
			{
				return this.sceneOverrideMode == DebugSceneOverrideMode.None && this.mipInfoMode == DebugMipInfoMode.None;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000058C8 File Offset: 0x00003AC8
		public bool TryGetScreenClearColor(ref Color color)
		{
			if (this.mipInfoMode != DebugMipInfoMode.None)
			{
				color = Color.black;
				return true;
			}
			switch (this.sceneOverrideMode)
			{
			case DebugSceneOverrideMode.None:
			case DebugSceneOverrideMode.ShadedWireframe:
				return false;
			case DebugSceneOverrideMode.Overdraw:
				color = Color.black;
				return true;
			case DebugSceneOverrideMode.Wireframe:
			case DebugSceneOverrideMode.SolidWireframe:
				color = new Color(0.1f, 0.1f, 0.1f, 1f);
				return true;
			default:
				throw new ArgumentOutOfRangeException("color");
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005947 File Offset: 0x00003B47
		IDebugDisplaySettingsPanelDisposable IDebugDisplaySettingsData.CreatePanel()
		{
			return new DebugDisplaySettingsRendering.SettingsPanel(this);
		}

		// Token: 0x0400015B RID: 347
		private DebugWireframeMode m_WireframeMode;

		// Token: 0x0400015C RID: 348
		private bool m_Overdraw;

		// Token: 0x0400015D RID: 349
		private DebugOverdrawMode m_OverdrawMode;

		// Token: 0x0200004E RID: 78
		public enum TaaDebugMode
		{
			// Token: 0x04000174 RID: 372
			None,
			// Token: 0x04000175 RID: 373
			ShowRawFrame,
			// Token: 0x04000176 RID: 374
			ShowRawFrameNoJitter,
			// Token: 0x04000177 RID: 375
			ShowClampedHistory
		}

		// Token: 0x0200004F RID: 79
		private static class Strings
		{
			// Token: 0x04000178 RID: 376
			public const string RangeValidationSettingsContainerName = "Pixel Range Settings";

			// Token: 0x04000179 RID: 377
			public static readonly DebugUI.Widget.NameAndTooltip MapOverlays = new DebugUI.Widget.NameAndTooltip
			{
				name = "Map Overlays",
				tooltip = "Overlays render pipeline textures to validate the scene."
			};

			// Token: 0x0400017A RID: 378
			public static readonly DebugUI.Widget.NameAndTooltip StpDebugViews = new DebugUI.Widget.NameAndTooltip
			{
				name = "STP Debug Views",
				tooltip = "Debug visualizations provided by STP."
			};

			// Token: 0x0400017B RID: 379
			public static readonly DebugUI.Widget.NameAndTooltip MapSize = new DebugUI.Widget.NameAndTooltip
			{
				name = "Map Size",
				tooltip = "Set the size of the render pipeline texture in the scene."
			};

			// Token: 0x0400017C RID: 380
			public static readonly DebugUI.Widget.NameAndTooltip AdditionalWireframeModes = new DebugUI.Widget.NameAndTooltip
			{
				name = "Additional Wireframe Modes",
				tooltip = "Debug the scene with additional wireframe shader views that are different from those in the scene view."
			};

			// Token: 0x0400017D RID: 381
			public static readonly DebugUI.Widget.NameAndTooltip WireframeNotSupportedWarning = new DebugUI.Widget.NameAndTooltip
			{
				name = "Warning: This platform might not support wireframe rendering.",
				tooltip = "Some platforms, for example, mobile platforms using OpenGL ES and Vulkan, might not support wireframe rendering."
			};

			// Token: 0x0400017E RID: 382
			public static readonly DebugUI.Widget.NameAndTooltip OverdrawMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "Overdraw Mode",
				tooltip = "Debug anywhere materials that overdrawn pixels top of each other."
			};

			// Token: 0x0400017F RID: 383
			public static readonly DebugUI.Widget.NameAndTooltip MaxOverdrawCount = new DebugUI.Widget.NameAndTooltip
			{
				name = "Max Overdraw Count",
				tooltip = "Maximum overdraw count allowed for a single pixel."
			};

			// Token: 0x04000180 RID: 384
			public static readonly DebugUI.Widget.NameAndTooltip MipMapDisableMipCaching = new DebugUI.Widget.NameAndTooltip
			{
				name = "Disable Mip Caching",
				tooltip = "By disabling mip caching, the data on GPU accurately reflects what the TextureStreamer calculates. While this can significantly increase CPU-to-GPU traffic, it can be an invaluable tool to validate that the Streamer behaves as expected."
			};

			// Token: 0x04000181 RID: 385
			public static readonly DebugUI.Widget.NameAndTooltip MipMapDebugView = new DebugUI.Widget.NameAndTooltip
			{
				name = "Debug View",
				tooltip = "Use the drop-down to select a mipmap property to debug."
			};

			// Token: 0x04000182 RID: 386
			public static readonly DebugUI.Widget.NameAndTooltip MipMapDebugOpacity = new DebugUI.Widget.NameAndTooltip
			{
				name = "Debug Opacity",
				tooltip = "Opacity of texture mipmap streaming debug colors."
			};

			// Token: 0x04000183 RID: 387
			public static readonly DebugUI.Widget.NameAndTooltip MipMapMaterialTextureSlot = new DebugUI.Widget.NameAndTooltip
			{
				name = "Material Texture Slot",
				tooltip = "Use the drop-down to select the material texture slot to debug (does not affect terrain).\n\nThe slot indices follow the default order by which texture properties appear in the Material Inspector.\nThe default order is itself defined by the order in which (non-hidden) texture properties appear in the shader's \"Properties\" block."
			};

			// Token: 0x04000184 RID: 388
			public static readonly DebugUI.Widget.NameAndTooltip MipMapTerrainTexture = new DebugUI.Widget.NameAndTooltip
			{
				name = "Terrain Texture",
				tooltip = "Use the drop-down to select the terrain Texture to debug the mipmap for."
			};

			// Token: 0x04000185 RID: 389
			public static readonly DebugUI.Widget.NameAndTooltip MipMapDisplayStatusCodes = new DebugUI.Widget.NameAndTooltip
			{
				name = "Display Status Codes",
				tooltip = "Show detailed status codes indicating why textures are not streaming or highlighting points of attention."
			};

			// Token: 0x04000186 RID: 390
			public static readonly DebugUI.Widget.NameAndTooltip MipMapActivityTimespan = new DebugUI.Widget.NameAndTooltip
			{
				name = "Activity Timespan",
				tooltip = "How long a texture should be shown as \"recently updated\"."
			};

			// Token: 0x04000187 RID: 391
			public static readonly DebugUI.Widget.NameAndTooltip MipMapCombinePerMaterial = new DebugUI.Widget.NameAndTooltip
			{
				name = "Combined per Material",
				tooltip = "Combine the information over all slots per material."
			};

			// Token: 0x04000188 RID: 392
			public static readonly DebugUI.Widget.NameAndTooltip PostProcessing = new DebugUI.Widget.NameAndTooltip
			{
				name = "Post-processing",
				tooltip = "Override the controls for Post Processing in the scene."
			};

			// Token: 0x04000189 RID: 393
			public static readonly DebugUI.Widget.NameAndTooltip MSAA = new DebugUI.Widget.NameAndTooltip
			{
				name = "MSAA",
				tooltip = "Use the checkbox to disable MSAA in the scene."
			};

			// Token: 0x0400018A RID: 394
			public static readonly DebugUI.Widget.NameAndTooltip HDR = new DebugUI.Widget.NameAndTooltip
			{
				name = "HDR",
				tooltip = "Use the checkbox to disable High Dynamic Range in the scene."
			};

			// Token: 0x0400018B RID: 395
			public static readonly DebugUI.Widget.NameAndTooltip TaaDebugMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "TAA Debug Mode",
				tooltip = "Choose whether to force TAA to output the raw jittered frame or clamped reprojected history."
			};

			// Token: 0x0400018C RID: 396
			public static readonly DebugUI.Widget.NameAndTooltip PixelValidationMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "Pixel Validation Mode",
				tooltip = "Choose between modes that validate pixel on screen."
			};

			// Token: 0x0400018D RID: 397
			public static readonly DebugUI.Widget.NameAndTooltip Channels = new DebugUI.Widget.NameAndTooltip
			{
				name = "Channels",
				tooltip = "Choose the texture channel used to validate the scene."
			};

			// Token: 0x0400018E RID: 398
			public static readonly DebugUI.Widget.NameAndTooltip ValueRangeMin = new DebugUI.Widget.NameAndTooltip
			{
				name = "Value Range Min",
				tooltip = "Any values set below this field will be considered invalid and will appear red on screen."
			};

			// Token: 0x0400018F RID: 399
			public static readonly DebugUI.Widget.NameAndTooltip ValueRangeMax = new DebugUI.Widget.NameAndTooltip
			{
				name = "Value Range Max",
				tooltip = "Any values set above this field will be considered invalid and will appear blue on screen."
			};
		}

		// Token: 0x02000050 RID: 80
		internal static class WidgetFactory
		{
			// Token: 0x060001AA RID: 426 RVA: 0x00005D28 File Offset: 0x00003F28
			internal static DebugUI.Widget CreateMapOverlays(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.MapOverlays,
					autoEnum = typeof(DebugFullScreenMode),
					getter = () => (int)panel.data.fullScreenDebugMode,
					setter = delegate(int value)
					{
						panel.data.fullScreenDebugMode = (DebugFullScreenMode)value;
					},
					getIndex = () => (int)panel.data.fullScreenDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.fullScreenDebugMode = (DebugFullScreenMode)value;
					}
				};
			}

			// Token: 0x060001AB RID: 427 RVA: 0x00005DAC File Offset: 0x00003FAC
			internal static DebugUI.Widget CreateStpDebugViews(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.StpDebugViews,
					isHiddenCallback = () => panel.data.fullScreenDebugMode != DebugFullScreenMode.STP,
					enumNames = STP.debugViewDescriptions,
					enumValues = STP.debugViewIndices,
					getter = () => panel.data.stpDebugViewIndex,
					setter = delegate(int value)
					{
						panel.data.stpDebugViewIndex = value;
					},
					getIndex = () => panel.data.stpDebugViewIndex,
					setIndex = delegate(int value)
					{
						panel.data.stpDebugViewIndex = value;
					}
				};
			}

			// Token: 0x060001AC RID: 428 RVA: 0x00005E48 File Offset: 0x00004048
			internal static DebugUI.Widget CreateMapOverlaySize(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				DebugUI.Container container = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children = container.children;
				DebugUI.IntField intField = new DebugUI.IntField();
				intField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.MapSize;
				intField.getter = () => panel.data.fullScreenDebugModeOutputSizeScreenPercent;
				intField.setter = delegate(int value)
				{
					panel.data.fullScreenDebugModeOutputSizeScreenPercent = value;
				};
				intField.incStep = 10;
				intField.min = () => 0;
				intField.max = () => 100;
				children.Add(intField);
				return container;
			}

			// Token: 0x060001AD RID: 429 RVA: 0x00005EF8 File Offset: 0x000040F8
			internal static DebugUI.Widget CreateAdditionalWireframeShaderViews(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.AdditionalWireframeModes;
				enumField.autoEnum = typeof(DebugWireframeMode);
				enumField.getter = () => (int)panel.data.wireframeMode;
				enumField.setter = delegate(int value)
				{
					panel.data.wireframeMode = (DebugWireframeMode)value;
				};
				enumField.getIndex = () => (int)panel.data.wireframeMode;
				enumField.setIndex = delegate(int value)
				{
					panel.data.wireframeMode = (DebugWireframeMode)value;
				};
				enumField.onValueChanged = delegate(DebugUI.Field<int> _, int _)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				};
				return enumField;
			}

			// Token: 0x060001AE RID: 430 RVA: 0x00005FA0 File Offset: 0x000041A0
			internal static DebugUI.Widget CreateWireframeNotSupportedWarning(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.MessageBox
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.WireframeNotSupportedWarning,
					style = DebugUI.MessageBox.Style.Warning,
					isHiddenCallback = delegate
					{
						GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
						return (graphicsDeviceType != GraphicsDeviceType.OpenGLES3 && graphicsDeviceType != GraphicsDeviceType.Vulkan) || panel.data.wireframeMode == DebugWireframeMode.None;
					}
				};
			}

			// Token: 0x060001AF RID: 431 RVA: 0x00005FE4 File Offset: 0x000041E4
			internal static DebugUI.Widget CreateOverdrawMode(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.OverdrawMode,
					autoEnum = typeof(DebugOverdrawMode),
					getter = () => (int)panel.data.overdrawMode,
					setter = delegate(int value)
					{
						panel.data.overdrawMode = (DebugOverdrawMode)value;
					},
					getIndex = () => (int)panel.data.overdrawMode,
					setIndex = delegate(int value)
					{
						panel.data.overdrawMode = (DebugOverdrawMode)value;
					}
				};
			}

			// Token: 0x060001B0 RID: 432 RVA: 0x00006068 File Offset: 0x00004268
			internal static DebugUI.Widget CreateMaxOverdrawCount(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				DebugUI.Container container = new DebugUI.Container();
				container.isHiddenCallback = () => panel.data.overdrawMode == DebugOverdrawMode.None;
				ObservableList<DebugUI.Widget> children = container.children;
				DebugUI.IntField intField = new DebugUI.IntField();
				intField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.MaxOverdrawCount;
				intField.getter = () => panel.data.maxOverdrawCount;
				intField.setter = delegate(int value)
				{
					panel.data.maxOverdrawCount = value;
				};
				intField.incStep = 10;
				intField.min = () => 1;
				intField.max = () => 500;
				children.Add(intField);
				return container;
			}

			// Token: 0x060001B1 RID: 433 RVA: 0x0000612C File Offset: 0x0000432C
			internal static DebugUI.Widget CreateMipMapDebugWidget(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				DebugUI.Container container = new DebugUI.Container();
				container.displayName = "Mipmap Streaming";
				ObservableList<DebugUI.Widget> children = container.children;
				DebugUI.BoolField boolField = new DebugUI.BoolField();
				boolField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.MipMapDisableMipCaching;
				boolField.getter = () => Texture.streamingTextureDiscardUnusedMips;
				boolField.setter = delegate(bool value)
				{
					Texture.streamingTextureDiscardUnusedMips = value;
				};
				children.Add(boolField);
				container.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateMipMapMode(panel));
				container.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateMipMapDebugSettings(panel));
				return container;
			}

			// Token: 0x060001B2 RID: 434 RVA: 0x000061D0 File Offset: 0x000043D0
			internal static DebugUI.Widget CreateMipMapMode(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.MipMapDebugView,
					autoEnum = typeof(DebugMipInfoMode),
					getter = () => (int)panel.data.mipInfoMode,
					setter = delegate(int value)
					{
						panel.data.mipInfoMode = (DebugMipInfoMode)value;
					},
					getIndex = () => (int)panel.data.mipInfoMode,
					setIndex = delegate(int value)
					{
						panel.data.mipInfoMode = (DebugMipInfoMode)value;
					}
				};
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x00006254 File Offset: 0x00004454
			internal static DebugUI.Widget CreateMipMapDebugSettings(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				GUIContent[] texSlotStrings = new GUIContent[64];
				int[] texSlotValues = new int[64];
				for (int i = 0; i < 64; i++)
				{
					texSlotStrings[i] = new GUIContent(string.Format("Slot {0}", i));
					texSlotValues[i] = i;
				}
				DebugUI.Container container = new DebugUI.Container();
				container.isHiddenCallback = () => panel.data.mipInfoMode == DebugMipInfoMode.None;
				ObservableList<DebugUI.Widget> children = container.children;
				DebugUI.FloatField floatField = new DebugUI.FloatField();
				floatField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.MipMapDebugOpacity;
				floatField.getter = () => panel.data.mipDebugOpacity;
				floatField.setter = delegate(float value)
				{
					panel.data.mipDebugOpacity = value;
				};
				floatField.min = () => 0f;
				floatField.max = () => 1f;
				children.Add(floatField);
				container.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateMipMapDebugSlotSelector(panel, () => panel.data.canAggregateData, texSlotStrings, texSlotValues));
				container.children.Add(new DebugUI.BoolField
				{
					isHiddenCallback = () => !panel.data.canAggregateData,
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.MipMapCombinePerMaterial,
					getter = () => panel.data.showInfoForAllSlots,
					setter = delegate(bool value)
					{
						panel.data.showInfoForAllSlots = value;
						panel.data.mipDebugStatusMode = (value ? DebugMipMapStatusMode.Material : DebugMipMapStatusMode.Texture);
					}
				});
				ObservableList<DebugUI.Widget> children2 = container.children;
				DebugUI.Container container2 = new DebugUI.Container();
				container2.isHiddenCallback = () => !panel.data.canAggregateData || panel.data.showInfoForAllSlots;
				container2.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateMipMapDebugSlotSelector(panel, () => false, texSlotStrings, texSlotValues));
				container2.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateMipMapShowStatusCodeToggle(panel));
				children2.Add(container2);
				container.children.Add(new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.MipMapTerrainTexture,
					getter = () => (int)panel.data.mipDebugTerrainTexture,
					setter = delegate(int value)
					{
						panel.data.mipDebugTerrainTexture = (DebugMipMapModeTerrainTexture)value;
					},
					autoEnum = typeof(DebugMipMapModeTerrainTexture),
					getIndex = () => (int)panel.data.mipDebugTerrainTexture,
					setIndex = delegate(int value)
					{
						panel.data.mipDebugTerrainTexture = (DebugMipMapModeTerrainTexture)value;
					}
				});
				container.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateMipMapDebugCooldownSlider(panel));
				return container;
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x000064B4 File Offset: 0x000046B4
			internal static DebugUI.Widget CreateMipMapDebugSlotSelector(DebugDisplaySettingsRendering.SettingsPanel panel, Func<bool> hiddenCB, GUIContent[] texSlotStrings, int[] texSlotValues)
			{
				return new DebugUI.EnumField
				{
					isHiddenCallback = hiddenCB,
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.MipMapMaterialTextureSlot,
					getter = () => panel.data.mipDebugMaterialTextureSlot,
					setter = delegate(int value)
					{
						panel.data.mipDebugMaterialTextureSlot = value;
					},
					getIndex = () => panel.data.mipDebugMaterialTextureSlot,
					setIndex = delegate(int value)
					{
						panel.data.mipDebugMaterialTextureSlot = value;
					},
					enumNames = texSlotStrings,
					enumValues = texSlotValues
				};
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x0000653C File Offset: 0x0000473C
			internal static DebugUI.Widget CreateMipMapDebugCooldownSlider(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				DebugUI.FloatField floatField = new DebugUI.FloatField();
				floatField.isHiddenCallback = () => panel.data.mipInfoMode != DebugMipInfoMode.MipStreamingActivity;
				floatField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.MipMapActivityTimespan;
				floatField.getter = () => panel.data.mipDebugRecentUpdateCooldown;
				floatField.setter = delegate(float value)
				{
					panel.data.mipDebugRecentUpdateCooldown = value;
				};
				floatField.min = () => 0f;
				floatField.max = () => 60f;
				return floatField;
			}

			// Token: 0x060001B6 RID: 438 RVA: 0x000065E8 File Offset: 0x000047E8
			internal static DebugUI.Widget CreateMipMapShowStatusCodeToggle(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.BoolField
				{
					isHiddenCallback = () => panel.data.mipInfoMode != DebugMipInfoMode.MipStreamingStatus,
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.MipMapDisplayStatusCodes,
					getter = () => panel.data.mipDebugStatusShowCode,
					setter = delegate(bool value)
					{
						panel.data.mipDebugStatusShowCode = value;
					}
				};
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x00006648 File Offset: 0x00004848
			internal static DebugUI.Widget CreatePostProcessing(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.PostProcessing,
					autoEnum = typeof(DebugPostProcessingMode),
					getter = () => (int)panel.data.postProcessingDebugMode,
					setter = delegate(int value)
					{
						panel.data.postProcessingDebugMode = (DebugPostProcessingMode)value;
					},
					getIndex = () => (int)panel.data.postProcessingDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.postProcessingDebugMode = (DebugPostProcessingMode)value;
					}
				};
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x000066CC File Offset: 0x000048CC
			internal static DebugUI.Widget CreateMSAA(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.MSAA,
					getter = () => panel.data.enableMsaa,
					setter = delegate(bool value)
					{
						panel.data.enableMsaa = value;
					}
				};
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x0000671C File Offset: 0x0000491C
			internal static DebugUI.Widget CreateHDR(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.HDR,
					getter = () => panel.data.enableHDR,
					setter = delegate(bool value)
					{
						panel.data.enableHDR = value;
					}
				};
			}

			// Token: 0x060001BA RID: 442 RVA: 0x0000676C File Offset: 0x0000496C
			internal static DebugUI.Widget CreateTaaDebugMode(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.TaaDebugMode;
				enumField.autoEnum = typeof(DebugDisplaySettingsRendering.TaaDebugMode);
				enumField.getter = () => (int)panel.data.taaDebugMode;
				enumField.setter = delegate(int value)
				{
					panel.data.taaDebugMode = (DebugDisplaySettingsRendering.TaaDebugMode)value;
				};
				enumField.getIndex = () => (int)panel.data.taaDebugMode;
				enumField.setIndex = delegate(int value)
				{
					panel.data.taaDebugMode = (DebugDisplaySettingsRendering.TaaDebugMode)value;
				};
				enumField.onValueChanged = delegate(DebugUI.Field<int> _, int _)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				};
				return enumField;
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00006814 File Offset: 0x00004A14
			internal static DebugUI.Widget CreatePixelValidationMode(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.PixelValidationMode;
				enumField.autoEnum = typeof(DebugValidationMode);
				enumField.getter = () => (int)panel.data.validationMode;
				enumField.setter = delegate(int value)
				{
					panel.data.validationMode = (DebugValidationMode)value;
				};
				enumField.getIndex = () => (int)panel.data.validationMode;
				enumField.setIndex = delegate(int value)
				{
					panel.data.validationMode = (DebugValidationMode)value;
				};
				enumField.onValueChanged = delegate(DebugUI.Field<int> _, int _)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				};
				return enumField;
			}

			// Token: 0x060001BC RID: 444 RVA: 0x000068BC File Offset: 0x00004ABC
			internal static DebugUI.Widget CreatePixelValidationChannels(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.Channels,
					autoEnum = typeof(PixelValidationChannels),
					getter = () => (int)panel.data.validationChannels,
					setter = delegate(int value)
					{
						panel.data.validationChannels = (PixelValidationChannels)value;
					},
					getIndex = () => (int)panel.data.validationChannels,
					setIndex = delegate(int value)
					{
						panel.data.validationChannels = (PixelValidationChannels)value;
					}
				};
			}

			// Token: 0x060001BD RID: 445 RVA: 0x00006940 File Offset: 0x00004B40
			internal static DebugUI.Widget CreatePixelValueRangeMin(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.ValueRangeMin,
					getter = () => panel.data.validationRangeMin,
					setter = delegate(float value)
					{
						panel.data.validationRangeMin = value;
					},
					incStep = 0.01f
				};
			}

			// Token: 0x060001BE RID: 446 RVA: 0x0000699C File Offset: 0x00004B9C
			internal static DebugUI.Widget CreatePixelValueRangeMax(DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.ValueRangeMax,
					getter = () => panel.data.validationRangeMax,
					setter = delegate(float value)
					{
						panel.data.validationRangeMax = value;
					},
					incStep = 0.01f
				};
			}
		}

		// Token: 0x02000066 RID: 102
		[DisplayInfo(name = "Rendering", order = 1)]
		internal class SettingsPanel : DebugDisplaySettingsPanel<DebugDisplaySettingsRendering>
		{
			// Token: 0x0600022C RID: 556 RVA: 0x00006E44 File Offset: 0x00005044
			public SettingsPanel(DebugDisplaySettingsRendering data)
				: base(data)
			{
				base.AddWidget(new DebugUI.RuntimeDebugShadersMessageBox());
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Rendering Debug",
					flags = DebugUI.Flags.FrequentlyUsed,
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsRendering.WidgetFactory.CreateMapOverlays(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateStpDebugViews(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateMapOverlaySize(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateHDR(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateMSAA(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateTaaDebugMode(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreatePostProcessing(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateAdditionalWireframeShaderViews(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateWireframeNotSupportedWarning(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateOverdrawMode(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateMaxOverdrawCount(this),
						DebugDisplaySettingsRendering.WidgetFactory.CreateMipMapDebugWidget(this)
					}
				});
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Pixel Validation",
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValidationMode(this),
						new DebugUI.Container
						{
							displayName = "Pixel Range Settings",
							isHiddenCallback = () => data.validationMode != DebugValidationMode.HighlightOutsideOfRange,
							children = 
							{
								DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValidationChannels(this),
								DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValueRangeMin(this),
								DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValueRangeMax(this)
							}
						}
					}
				});
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "HDR Output",
					isHeader = true,
					opened = true,
					children = 
					{
						new DebugUI.MessageBox
						{
							displayName = "The values on the Rendering Debugger editor window might not be accurate. Please use the playmode debug UI (Ctrl+Backspace).",
							style = DebugUI.MessageBox.Style.Warning
						},
						DebugDisplaySettingsHDROutput.CreateHDROuputDisplayTable()
					}
				});
			}
		}
	}
}
