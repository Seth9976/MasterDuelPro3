using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000035 RID: 53
	public class DebugDisplaySettingsMaterial : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003C9E File Offset: 0x00001E9E
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset albedoValidationPreset
		{
			get
			{
				return this.m_AlbedoValidationPreset;
			}
			set
			{
				this.m_AlbedoValidationPreset = value;
				DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData presetData = this.m_AlbedoDebugValidationPresetData[(int)value];
				this.albedoMinLuminance = presetData.minLuminance;
				this.albedoMaxLuminance = presetData.maxLuminance;
				this.albedoCompareColor = presetData.color;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003CED File Offset: 0x00001EED
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00003CF5 File Offset: 0x00001EF5
		public float albedoMinLuminance { get; set; } = 0.01f;

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003CFE File Offset: 0x00001EFE
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00003D06 File Offset: 0x00001F06
		public float albedoMaxLuminance { get; set; } = 0.9f;

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003D0F File Offset: 0x00001F0F
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00003D25 File Offset: 0x00001F25
		public float albedoHueTolerance
		{
			get
			{
				if (this.m_AlbedoValidationPreset != DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance)
				{
					return this.m_AlbedoHueTolerance;
				}
				return 1f;
			}
			set
			{
				this.m_AlbedoHueTolerance = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00003D2E File Offset: 0x00001F2E
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00003D44 File Offset: 0x00001F44
		public float albedoSaturationTolerance
		{
			get
			{
				if (this.m_AlbedoValidationPreset != DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance)
				{
					return this.m_AlbedoSaturationTolerance;
				}
				return 1f;
			}
			set
			{
				this.m_AlbedoSaturationTolerance = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00003D4D File Offset: 0x00001F4D
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00003D55 File Offset: 0x00001F55
		public Color albedoCompareColor { get; set; } = new Color(0.49803922f, 0.49803922f, 0.49803922f, 1f);

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003D5E File Offset: 0x00001F5E
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00003D66 File Offset: 0x00001F66
		public float metallicMinValue { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003D6F File Offset: 0x00001F6F
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00003D77 File Offset: 0x00001F77
		public float metallicMaxValue { get; set; } = 0.9f;

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003D80 File Offset: 0x00001F80
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00003D88 File Offset: 0x00001F88
		public bool renderingLayersSelectedLight { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00003D91 File Offset: 0x00001F91
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00003D99 File Offset: 0x00001F99
		public bool selectedLightShadowLayerMask { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003DA2 File Offset: 0x00001FA2
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00003DAA File Offset: 0x00001FAA
		public uint renderingLayerMask { get; set; }

		// Token: 0x06000113 RID: 275 RVA: 0x00003DB3 File Offset: 0x00001FB3
		public uint GetDebugLightLayersMask()
		{
			return 65535U;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003DBA File Offset: 0x00001FBA
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00003DC2 File Offset: 0x00001FC2
		public DebugMaterialValidationMode materialValidationMode { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003DCB File Offset: 0x00001FCB
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00003DD3 File Offset: 0x00001FD3
		public DebugMaterialMode materialDebugMode { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003DDC File Offset: 0x00001FDC
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public DebugVertexAttributeMode vertexAttributeDebugMode { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003DED File Offset: 0x00001FED
		public bool AreAnySettingsActive
		{
			get
			{
				return this.materialDebugMode != DebugMaterialMode.None || this.vertexAttributeDebugMode != DebugVertexAttributeMode.None || this.materialValidationMode > DebugMaterialValidationMode.None;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00003E0A File Offset: 0x0000200A
		public bool IsPostProcessingAllowed
		{
			get
			{
				return !this.AreAnySettingsActive;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003E0A File Offset: 0x0000200A
		public bool IsLightingActive
		{
			get
			{
				return !this.AreAnySettingsActive;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003E15 File Offset: 0x00002015
		IDebugDisplaySettingsPanelDisposable IDebugDisplaySettingsData.CreatePanel()
		{
			return new DebugDisplaySettingsMaterial.SettingsPanel(this);
		}

		// Token: 0x0400010B RID: 267
		private DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData[] m_AlbedoDebugValidationPresetData = new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData[]
		{
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Default Luminance",
				color = new Color(0.49803922f, 0.49803922f, 0.49803922f),
				minLuminance = 0.01f,
				maxLuminance = 0.9f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Black Acrylic Paint",
				color = new Color(0.21960784f, 0.21960784f, 0.21960784f),
				minLuminance = 0.03f,
				maxLuminance = 0.07f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Dark Soil",
				color = new Color(0.33333334f, 0.23921569f, 0.19215687f),
				minLuminance = 0.05f,
				maxLuminance = 0.14f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Worn Asphalt",
				color = new Color(0.35686275f, 0.35686275f, 0.35686275f),
				minLuminance = 0.1f,
				maxLuminance = 0.15f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Dry Clay Soil",
				color = new Color(0.5372549f, 0.47058824f, 0.4f),
				minLuminance = 0.15f,
				maxLuminance = 0.35f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Green Grass",
				color = new Color(0.48235294f, 0.5137255f, 0.2901961f),
				minLuminance = 0.16f,
				maxLuminance = 0.26f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Old Concrete",
				color = new Color(0.5294118f, 0.53333336f, 0.5137255f),
				minLuminance = 0.17f,
				maxLuminance = 0.3f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Red Clay Tile",
				color = new Color(0.77254903f, 0.49019608f, 0.39215687f),
				minLuminance = 0.23f,
				maxLuminance = 0.33f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Dry Sand",
				color = new Color(0.69411767f, 0.654902f, 0.5176471f),
				minLuminance = 0.2f,
				maxLuminance = 0.45f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "New Concrete",
				color = new Color(0.7254902f, 0.7137255f, 0.6862745f),
				minLuminance = 0.32f,
				maxLuminance = 0.55f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "White Acrylic Paint",
				color = new Color(0.8901961f, 0.8901961f, 0.8901961f),
				minLuminance = 0.75f,
				maxLuminance = 0.85f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Fresh Snow",
				color = new Color(0.9529412f, 0.9529412f, 0.9529412f),
				minLuminance = 0.85f,
				maxLuminance = 0.95f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Blue Sky",
				color = new Color(0.3647059f, 0.48235294f, 0.6156863f),
				minLuminance = new Color(0.3647059f, 0.48235294f, 0.6156863f).linear.maxColorComponent - 0.05f,
				maxLuminance = new Color(0.3647059f, 0.48235294f, 0.6156863f).linear.maxColorComponent + 0.05f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Foliage",
				color = new Color(0.35686275f, 0.42352942f, 0.25490198f),
				minLuminance = new Color(0.35686275f, 0.42352942f, 0.25490198f).linear.maxColorComponent - 0.05f,
				maxLuminance = new Color(0.35686275f, 0.42352942f, 0.25490198f).linear.maxColorComponent + 0.05f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Custom",
				color = new Color(0.49803922f, 0.49803922f, 0.49803922f),
				minLuminance = 0.01f,
				maxLuminance = 0.9f
			}
		};

		// Token: 0x0400010C RID: 268
		private DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset m_AlbedoValidationPreset;

		// Token: 0x0400010F RID: 271
		private float m_AlbedoHueTolerance = 0.104f;

		// Token: 0x04000110 RID: 272
		private float m_AlbedoSaturationTolerance = 0.214f;

		// Token: 0x04000117 RID: 279
		public Vector4[] debugRenderingLayersColors = new Vector4[]
		{
			new Vector4(230f, 159f, 0f) / 255f,
			new Vector4(86f, 180f, 233f) / 255f,
			new Vector4(255f, 182f, 291f) / 255f,
			new Vector4(0f, 158f, 115f) / 255f,
			new Vector4(240f, 228f, 66f) / 255f,
			new Vector4(0f, 114f, 178f) / 255f,
			new Vector4(213f, 94f, 0f) / 255f,
			new Vector4(170f, 68f, 170f) / 255f,
			new Vector4(1f, 0.5f, 0.5f),
			new Vector4(0.5f, 1f, 0.5f),
			new Vector4(0.5f, 0.5f, 1f),
			new Vector4(0.5f, 1f, 1f),
			new Vector4(0.75f, 0.25f, 1f),
			new Vector4(0.25f, 1f, 0.75f),
			new Vector4(0.25f, 0.25f, 0.75f),
			new Vector4(0.75f, 0.25f, 0.25f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0f)
		};

		// Token: 0x02000036 RID: 54
		public enum AlbedoDebugValidationPreset
		{
			// Token: 0x0400011C RID: 284
			DefaultLuminance,
			// Token: 0x0400011D RID: 285
			BlackAcrylicPaint,
			// Token: 0x0400011E RID: 286
			DarkSoil,
			// Token: 0x0400011F RID: 287
			WornAsphalt,
			// Token: 0x04000120 RID: 288
			DryClaySoil,
			// Token: 0x04000121 RID: 289
			GreenGrass,
			// Token: 0x04000122 RID: 290
			OldConcrete,
			// Token: 0x04000123 RID: 291
			RedClayTile,
			// Token: 0x04000124 RID: 292
			DrySand,
			// Token: 0x04000125 RID: 293
			NewConcrete,
			// Token: 0x04000126 RID: 294
			WhiteAcrylicPaint,
			// Token: 0x04000127 RID: 295
			FreshSnow,
			// Token: 0x04000128 RID: 296
			BlueSky,
			// Token: 0x04000129 RID: 297
			Foliage,
			// Token: 0x0400012A RID: 298
			Custom
		}

		// Token: 0x02000037 RID: 55
		private struct AlbedoDebugValidationPresetData
		{
			// Token: 0x0400012B RID: 299
			public string name;

			// Token: 0x0400012C RID: 300
			public Color color;

			// Token: 0x0400012D RID: 301
			public float minLuminance;

			// Token: 0x0400012E RID: 302
			public float maxLuminance;
		}

		// Token: 0x02000038 RID: 56
		private static class Strings
		{
			// Token: 0x0400012F RID: 303
			public const string AlbedoSettingsContainerName = "Albedo Settings";

			// Token: 0x04000130 RID: 304
			public const string MetallicSettingsContainerName = "Metallic Settings";

			// Token: 0x04000131 RID: 305
			public const string RenderingLayerMasksSettingsContainerName = "Rendering Layer Masks Settings";

			// Token: 0x04000132 RID: 306
			public static readonly DebugUI.Widget.NameAndTooltip MaterialOverride = new DebugUI.Widget.NameAndTooltip
			{
				name = "Material Override",
				tooltip = "Use the drop-down to select a Material property to visualize on every GameObject on screen."
			};

			// Token: 0x04000133 RID: 307
			public static readonly DebugUI.Widget.NameAndTooltip VertexAttribute = new DebugUI.Widget.NameAndTooltip
			{
				name = "Vertex Attribute",
				tooltip = "Use the drop-down to select a 3D GameObject attribute, like Texture Coordinates or Vertex Color, to visualize on screen."
			};

			// Token: 0x04000134 RID: 308
			public static readonly DebugUI.Widget.NameAndTooltip MaterialValidationMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "Material Validation Mode",
				tooltip = "Debug and validate material properties."
			};

			// Token: 0x04000135 RID: 309
			public static readonly DebugUI.Widget.NameAndTooltip RenderingLayersSelectedLight = new DebugUI.Widget.NameAndTooltip
			{
				name = "Filter Rendering Layers by Light",
				tooltip = "Highlight Renderers affected by Selected Light"
			};

			// Token: 0x04000136 RID: 310
			public static readonly DebugUI.Widget.NameAndTooltip SelectedLightShadowLayerMask = new DebugUI.Widget.NameAndTooltip
			{
				name = "Use Light's Shadow Layer Mask",
				tooltip = "Highlight Renderers that cast shadows for the Selected Light"
			};

			// Token: 0x04000137 RID: 311
			public static readonly DebugUI.Widget.NameAndTooltip RenderingLayerColors = new DebugUI.Widget.NameAndTooltip
			{
				name = "Layers Color",
				tooltip = "Select the display color for each Rendering Layer"
			};

			// Token: 0x04000138 RID: 312
			public static readonly DebugUI.Widget.NameAndTooltip FilterRenderingLayerMask = new DebugUI.Widget.NameAndTooltip
			{
				name = "Filter Layers",
				tooltip = "Use the dropdown to filter Rendering Layers that you want to visualize"
			};

			// Token: 0x04000139 RID: 313
			public static readonly DebugUI.Widget.NameAndTooltip ValidationPreset = new DebugUI.Widget.NameAndTooltip
			{
				name = "Validation Preset",
				tooltip = "Validate using a list of preset surfaces and inputs based on real-world surfaces."
			};

			// Token: 0x0400013A RID: 314
			public static readonly DebugUI.Widget.NameAndTooltip AlbedoCustomColor = new DebugUI.Widget.NameAndTooltip
			{
				name = "Target Color",
				tooltip = "Custom target color for albedo validation."
			};

			// Token: 0x0400013B RID: 315
			public static readonly DebugUI.Widget.NameAndTooltip AlbedoMinLuminance = new DebugUI.Widget.NameAndTooltip
			{
				name = "Min Luminance",
				tooltip = "Any values set below this field are invalid and appear red on screen."
			};

			// Token: 0x0400013C RID: 316
			public static readonly DebugUI.Widget.NameAndTooltip AlbedoMaxLuminance = new DebugUI.Widget.NameAndTooltip
			{
				name = "Max Luminance",
				tooltip = "Any values set above this field are invalid and appear blue on screen."
			};

			// Token: 0x0400013D RID: 317
			public static readonly DebugUI.Widget.NameAndTooltip AlbedoHueTolerance = new DebugUI.Widget.NameAndTooltip
			{
				name = "Hue Tolerance",
				tooltip = "Validate a material based on a specific hue."
			};

			// Token: 0x0400013E RID: 318
			public static readonly DebugUI.Widget.NameAndTooltip AlbedoSaturationTolerance = new DebugUI.Widget.NameAndTooltip
			{
				name = "Saturation Tolerance",
				tooltip = "Validate a material based on a specific Saturation."
			};

			// Token: 0x0400013F RID: 319
			public static readonly DebugUI.Widget.NameAndTooltip MetallicMinValue = new DebugUI.Widget.NameAndTooltip
			{
				name = "Min Value",
				tooltip = "Any values set below this field are invalid and appear red on screen."
			};

			// Token: 0x04000140 RID: 320
			public static readonly DebugUI.Widget.NameAndTooltip MetallicMaxValue = new DebugUI.Widget.NameAndTooltip
			{
				name = "Max Value",
				tooltip = "Any values set above this field are invalid and appear blue on screen."
			};
		}

		// Token: 0x02000039 RID: 57
		internal static class WidgetFactory
		{
			// Token: 0x06000120 RID: 288 RVA: 0x000049F0 File Offset: 0x00002BF0
			internal static DebugUI.Widget CreateMaterialOverride(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.MaterialOverride,
					autoEnum = typeof(DebugMaterialMode),
					getter = () => (int)panel.data.materialDebugMode,
					setter = delegate(int value)
					{
						panel.data.materialDebugMode = (DebugMaterialMode)value;
					},
					getIndex = () => (int)panel.data.materialDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.materialDebugMode = (DebugMaterialMode)value;
					}
				};
			}

			// Token: 0x06000121 RID: 289 RVA: 0x00004A74 File Offset: 0x00002C74
			internal static DebugUI.Widget CreateVertexAttribute(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.VertexAttribute,
					autoEnum = typeof(DebugVertexAttributeMode),
					getter = () => (int)panel.data.vertexAttributeDebugMode,
					setter = delegate(int value)
					{
						panel.data.vertexAttributeDebugMode = (DebugVertexAttributeMode)value;
					},
					getIndex = () => (int)panel.data.vertexAttributeDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.vertexAttributeDebugMode = (DebugVertexAttributeMode)value;
					}
				};
			}

			// Token: 0x06000122 RID: 290 RVA: 0x00004AF8 File Offset: 0x00002CF8
			internal static DebugUI.Widget CreateMaterialValidationMode(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsMaterial.Strings.MaterialValidationMode;
				enumField.autoEnum = typeof(DebugMaterialValidationMode);
				enumField.getter = () => (int)panel.data.materialValidationMode;
				enumField.setter = delegate(int value)
				{
					panel.data.materialValidationMode = (DebugMaterialValidationMode)value;
				};
				enumField.getIndex = () => (int)panel.data.materialValidationMode;
				enumField.setIndex = delegate(int value)
				{
					panel.data.materialValidationMode = (DebugMaterialValidationMode)value;
				};
				enumField.onValueChanged = delegate(DebugUI.Field<int> _, int _)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				};
				return enumField;
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00004BA0 File Offset: 0x00002DA0
			internal static DebugUI.Widget CreateRenderingLayersSelectedLight(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.RenderingLayersSelectedLight,
					getter = () => panel.data.renderingLayersSelectedLight,
					setter = delegate(bool value)
					{
						panel.data.renderingLayersSelectedLight = value;
					},
					flags = DebugUI.Flags.EditorOnly
				};
			}

			// Token: 0x06000124 RID: 292 RVA: 0x00004BF8 File Offset: 0x00002DF8
			internal static DebugUI.Widget CreateSelectedLightShadowLayerMask(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.SelectedLightShadowLayerMask,
					getter = () => panel.data.selectedLightShadowLayerMask,
					setter = delegate(bool value)
					{
						panel.data.selectedLightShadowLayerMask = value;
					},
					flags = DebugUI.Flags.EditorOnly,
					isHiddenCallback = () => !panel.data.renderingLayersSelectedLight
				};
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00004C60 File Offset: 0x00002E60
			internal static DebugUI.Widget CreateFilterRenderingLayerMasks(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.MaskField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.FilterRenderingLayerMask,
					getter = () => panel.data.renderingLayerMask,
					setter = delegate(uint value)
					{
						panel.data.renderingLayerMask = value;
					},
					isHiddenCallback = () => panel.data.renderingLayersSelectedLight
				};
			}

			// Token: 0x06000126 RID: 294 RVA: 0x00004CC0 File Offset: 0x00002EC0
			internal static DebugUI.Widget CreateAlbedoPreset(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsMaterial.Strings.ValidationPreset;
				enumField.autoEnum = typeof(DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset);
				enumField.getter = () => (int)panel.data.albedoValidationPreset;
				enumField.setter = delegate(int value)
				{
					panel.data.albedoValidationPreset = (DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset)value;
				};
				enumField.getIndex = () => (int)panel.data.albedoValidationPreset;
				enumField.setIndex = delegate(int value)
				{
					panel.data.albedoValidationPreset = (DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset)value;
				};
				enumField.onValueChanged = delegate(DebugUI.Field<int> _, int _)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				};
				return enumField;
			}

			// Token: 0x06000127 RID: 295 RVA: 0x00004D68 File Offset: 0x00002F68
			internal static DebugUI.Widget CreateAlbedoCustomColor(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.ColorField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.AlbedoCustomColor,
					getter = () => panel.data.albedoCompareColor,
					setter = delegate(Color value)
					{
						panel.data.albedoCompareColor = value;
					},
					isHiddenCallback = () => panel.data.albedoValidationPreset != DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.Custom
				};
			}

			// Token: 0x06000128 RID: 296 RVA: 0x00004DC8 File Offset: 0x00002FC8
			internal static DebugUI.Widget CreateAlbedoMinLuminance(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.AlbedoMinLuminance,
					getter = () => panel.data.albedoMinLuminance,
					setter = delegate(float value)
					{
						panel.data.albedoMinLuminance = value;
					},
					incStep = 0.01f
				};
			}

			// Token: 0x06000129 RID: 297 RVA: 0x00004E24 File Offset: 0x00003024
			internal static DebugUI.Widget CreateAlbedoMaxLuminance(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.AlbedoMaxLuminance,
					getter = () => panel.data.albedoMaxLuminance,
					setter = delegate(float value)
					{
						panel.data.albedoMaxLuminance = value;
					},
					incStep = 0.01f
				};
			}

			// Token: 0x0600012A RID: 298 RVA: 0x00004E80 File Offset: 0x00003080
			internal static DebugUI.Widget CreateAlbedoHueTolerance(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.AlbedoHueTolerance,
					getter = () => panel.data.albedoHueTolerance,
					setter = delegate(float value)
					{
						panel.data.albedoHueTolerance = value;
					},
					incStep = 0.01f,
					isHiddenCallback = () => panel.data.albedoValidationPreset == DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance
				};
			}

			// Token: 0x0600012B RID: 299 RVA: 0x00004EEC File Offset: 0x000030EC
			internal static DebugUI.Widget CreateAlbedoSaturationTolerance(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.AlbedoSaturationTolerance,
					getter = () => panel.data.albedoSaturationTolerance,
					setter = delegate(float value)
					{
						panel.data.albedoSaturationTolerance = value;
					},
					incStep = 0.01f,
					isHiddenCallback = () => panel.data.albedoValidationPreset == DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance
				};
			}

			// Token: 0x0600012C RID: 300 RVA: 0x00004F58 File Offset: 0x00003158
			internal static DebugUI.Widget CreateMetallicMinValue(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.MetallicMinValue,
					getter = () => panel.data.metallicMinValue,
					setter = delegate(float value)
					{
						panel.data.metallicMinValue = value;
					},
					incStep = 0.01f
				};
			}

			// Token: 0x0600012D RID: 301 RVA: 0x00004FB4 File Offset: 0x000031B4
			internal static DebugUI.Widget CreateMetallicMaxValue(DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.MetallicMaxValue,
					getter = () => panel.data.metallicMaxValue,
					setter = delegate(float value)
					{
						panel.data.metallicMaxValue = value;
					},
					incStep = 0.01f
				};
			}
		}

		// Token: 0x02000049 RID: 73
		[DisplayInfo(name = "Material", order = 2)]
		internal class SettingsPanel : DebugDisplaySettingsPanel<DebugDisplaySettingsMaterial>
		{
			// Token: 0x06000169 RID: 361 RVA: 0x00005298 File Offset: 0x00003498
			public SettingsPanel(DebugDisplaySettingsMaterial data)
				: base(data)
			{
				DebugDisplaySettingsMaterial.SettingsPanel <>4__this = this;
				base.AddWidget(new DebugUI.RuntimeDebugShadersMessageBox());
				DebugUI.MaskField filterRenderingLayerWidget = (DebugUI.MaskField)DebugDisplaySettingsMaterial.WidgetFactory.CreateFilterRenderingLayerMasks(this);
				List<string> renderingLayers = new List<string>();
				for (int i = 0; i < 32; i++)
				{
					renderingLayers.Add(string.Format("Unused Rendering Layer {0}", i));
				}
				string[] names = RenderingLayerMask.GetDefinedRenderingLayerNames();
				for (int j = 0; j < names.Length; j++)
				{
					int index2 = RenderingLayerMask.NameToRenderingLayer(names[j]);
					renderingLayers[index2] = names[j];
				}
				filterRenderingLayerWidget.Fill(renderingLayers.ToArray());
				DebugUI.Foldout layersColor = new DebugUI.Foldout
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.RenderingLayerColors,
					flags = DebugUI.Flags.EditorOnly
				};
				for (int k = 0; k < renderingLayers.Count; k++)
				{
					int index = k;
					layersColor.children.Add(new DebugUI.ColorField
					{
						displayName = renderingLayers[k],
						flags = DebugUI.Flags.EditorOnly,
						getter = () => <>4__this.data.debugRenderingLayersColors[index],
						setter = delegate(Color value)
						{
							<>4__this.data.debugRenderingLayersColors[index] = value;
						}
					});
				}
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Material Filters",
					flags = DebugUI.Flags.FrequentlyUsed,
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsMaterial.WidgetFactory.CreateMaterialOverride(this),
						new DebugUI.Container
						{
							displayName = "Rendering Layer Masks Settings",
							isHiddenCallback = () => data.materialDebugMode != DebugMaterialMode.RenderingLayerMasks,
							children = 
							{
								DebugDisplaySettingsMaterial.WidgetFactory.CreateRenderingLayersSelectedLight(this),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateSelectedLightShadowLayerMask(this),
								filterRenderingLayerWidget,
								layersColor
							}
						},
						DebugDisplaySettingsMaterial.WidgetFactory.CreateVertexAttribute(this)
					}
				});
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Material Validation",
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsMaterial.WidgetFactory.CreateMaterialValidationMode(this),
						new DebugUI.Container
						{
							displayName = "Albedo Settings",
							isHiddenCallback = () => data.materialValidationMode != DebugMaterialValidationMode.Albedo,
							children = 
							{
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoPreset(this),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoCustomColor(this),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoMinLuminance(this),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoMaxLuminance(this),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoHueTolerance(this),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoSaturationTolerance(this)
							}
						},
						new DebugUI.Container
						{
							displayName = "Metallic Settings",
							isHiddenCallback = () => data.materialValidationMode != DebugMaterialValidationMode.Metallic,
							children = 
							{
								DebugDisplaySettingsMaterial.WidgetFactory.CreateMetallicMinValue(this),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateMetallicMaxValue(this)
							}
						}
					}
				});
			}
		}
	}
}
