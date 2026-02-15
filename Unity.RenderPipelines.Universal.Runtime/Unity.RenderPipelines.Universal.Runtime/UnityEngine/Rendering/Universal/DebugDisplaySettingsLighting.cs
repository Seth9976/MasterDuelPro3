using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200002E RID: 46
	public class DebugDisplaySettingsLighting : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000394B File Offset: 0x00001B4B
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003953 File Offset: 0x00001B53
		public DebugLightingMode lightingDebugMode { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000395C File Offset: 0x00001B5C
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003964 File Offset: 0x00001B64
		public DebugLightingFeatureFlags lightingFeatureFlags { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000396D File Offset: 0x00001B6D
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003975 File Offset: 0x00001B75
		public HDRDebugMode hdrDebugMode { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000397E File Offset: 0x00001B7E
		public bool AreAnySettingsActive
		{
			get
			{
				return this.lightingDebugMode != DebugLightingMode.None || this.lightingFeatureFlags != DebugLightingFeatureFlags.None || this.hdrDebugMode > HDRDebugMode.None;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000399B File Offset: 0x00001B9B
		public bool IsPostProcessingAllowed
		{
			get
			{
				return this.lightingDebugMode != DebugLightingMode.Reflections && this.lightingDebugMode != DebugLightingMode.ReflectionsWithSmoothness;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000039B4 File Offset: 0x00001BB4
		public bool IsLightingActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000039B7 File Offset: 0x00001BB7
		IDebugDisplaySettingsPanelDisposable IDebugDisplaySettingsData.CreatePanel()
		{
			return new DebugDisplaySettingsLighting.SettingsPanel(this);
		}

		// Token: 0x0200002F RID: 47
		internal static class Strings
		{
			// Token: 0x04000105 RID: 261
			public static readonly DebugUI.Widget.NameAndTooltip LightingDebugMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "Lighting Debug Mode",
				tooltip = "Use the drop-down to select which lighting and shadow debug information to overlay on the screen."
			};

			// Token: 0x04000106 RID: 262
			public static readonly DebugUI.Widget.NameAndTooltip LightingFeatures = new DebugUI.Widget.NameAndTooltip
			{
				name = "Lighting Features",
				tooltip = "Filter and debug selected lighting features in the system."
			};

			// Token: 0x04000107 RID: 263
			public static readonly DebugUI.Widget.NameAndTooltip HDRDebugMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "HDR Debug Mode",
				tooltip = "Select which HDR brightness debug information to overlay on the screen."
			};
		}

		// Token: 0x02000030 RID: 48
		internal static class WidgetFactory
		{
			// Token: 0x060000EC RID: 236 RVA: 0x00003A40 File Offset: 0x00001C40
			internal static DebugUI.Widget CreateLightingDebugMode(DebugDisplaySettingsLighting.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsLighting.Strings.LightingDebugMode,
					autoEnum = typeof(DebugLightingMode),
					getter = () => (int)panel.data.lightingDebugMode,
					setter = delegate(int value)
					{
						panel.data.lightingDebugMode = (DebugLightingMode)value;
					},
					getIndex = () => (int)panel.data.lightingDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.lightingDebugMode = (DebugLightingMode)value;
					}
				};
			}

			// Token: 0x060000ED RID: 237 RVA: 0x00003AC4 File Offset: 0x00001CC4
			internal static DebugUI.Widget CreateLightingFeatures(DebugDisplaySettingsLighting.SettingsPanel panel)
			{
				return new DebugUI.BitField
				{
					nameAndTooltip = DebugDisplaySettingsLighting.Strings.LightingFeatures,
					getter = () => panel.data.lightingFeatureFlags,
					setter = delegate(Enum value)
					{
						panel.data.lightingFeatureFlags = (DebugLightingFeatureFlags)value;
					},
					enumType = typeof(DebugLightingFeatureFlags)
				};
			}

			// Token: 0x060000EE RID: 238 RVA: 0x00003B24 File Offset: 0x00001D24
			internal static DebugUI.Widget CreateHDRDebugMode(DebugDisplaySettingsLighting.SettingsPanel panel)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsLighting.Strings.HDRDebugMode,
					autoEnum = typeof(HDRDebugMode),
					getter = () => (int)panel.data.hdrDebugMode,
					setter = delegate(int value)
					{
						panel.data.hdrDebugMode = (HDRDebugMode)value;
					},
					getIndex = () => (int)panel.data.hdrDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.hdrDebugMode = (HDRDebugMode)value;
					}
				};
			}
		}

		// Token: 0x02000034 RID: 52
		[DisplayInfo(name = "Lighting", order = 3)]
		internal class SettingsPanel : DebugDisplaySettingsPanel<DebugDisplaySettingsLighting>
		{
			// Token: 0x060000FC RID: 252 RVA: 0x00003C20 File Offset: 0x00001E20
			public SettingsPanel(DebugDisplaySettingsLighting data)
				: base(data)
			{
				base.AddWidget(new DebugUI.RuntimeDebugShadersMessageBox());
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Lighting Debug Modes",
					flags = DebugUI.Flags.FrequentlyUsed,
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsLighting.WidgetFactory.CreateLightingDebugMode(this),
						DebugDisplaySettingsLighting.WidgetFactory.CreateHDRDebugMode(this),
						DebugDisplaySettingsLighting.WidgetFactory.CreateLightingFeatures(this)
					}
				});
			}
		}
	}
}
