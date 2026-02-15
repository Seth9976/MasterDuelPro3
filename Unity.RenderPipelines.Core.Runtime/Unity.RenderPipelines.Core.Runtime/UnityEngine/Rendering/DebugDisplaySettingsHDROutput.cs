using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006C RID: 108
	public class DebugDisplaySettingsHDROutput
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x000097D8 File Offset: 0x000079D8
		public static DebugUI.Table CreateHDROuputDisplayTable()
		{
			DebugUI.Table table = new DebugUI.Table
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.hdrOutputAPI,
				isReadOnly = true
			};
			DebugUI.Table.Row row_hdrActive = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.hdrActive,
				opened = true
			};
			DebugUI.Table.Row row_hdrAvailable = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.hdrAvailable,
				opened = true
			};
			DebugUI.Table.Row row_gamut = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.gamut,
				opened = false
			};
			DebugUI.Table.Row row_format = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.format,
				opened = false
			};
			DebugUI.Table.Row row_autoHdrTonemapping = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.autoHdrTonemapping,
				opened = false
			};
			DebugUI.Table.Row row_paperWhite = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.paperWhite,
				opened = false
			};
			DebugUI.Table.Row row_minLuminance = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.minLuminance,
				opened = false
			};
			DebugUI.Table.Row row_maxLuminance = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.maxLuminance,
				opened = false
			};
			DebugUI.Table.Row row_maxFullFrameLuminance = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.maxFullFrameLuminance,
				opened = false
			};
			DebugUI.Table.Row row_modeChangeRequested = new DebugUI.Table.Row
			{
				displayName = DebugDisplaySettingsHDROutput.Strings.modeChangeRequested,
				opened = false
			};
			HDROutputSettings[] displays = HDROutputSettings.displays;
			for (int i = 0; i < displays.Length; i++)
			{
				HDROutputSettings d = displays[i];
				int idName = i + 1;
				string name = DebugDisplaySettingsHDROutput.Strings.displayName + idName.ToString();
				if (HDROutputSettings.main == d)
				{
					name += DebugDisplaySettingsHDROutput.Strings.displayMain;
				}
				row_hdrActive.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = () => d.active
				});
				row_hdrAvailable.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = () => d.available
				});
				row_gamut.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = delegate
					{
						if (d.available)
						{
							return d.displayColorGamut;
						}
						return DebugDisplaySettingsHDROutput.Strings.notAvailable;
					}
				});
				row_format.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = delegate
					{
						if (d.available)
						{
							return d.graphicsFormat;
						}
						return DebugDisplaySettingsHDROutput.Strings.notAvailable;
					}
				});
				row_autoHdrTonemapping.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = delegate
					{
						if (d.available)
						{
							return d.automaticHDRTonemapping;
						}
						return DebugDisplaySettingsHDROutput.Strings.notAvailable;
					}
				});
				row_paperWhite.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = delegate
					{
						if (d.available)
						{
							return d.paperWhiteNits;
						}
						return DebugDisplaySettingsHDROutput.Strings.notAvailable;
					}
				});
				row_minLuminance.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = delegate
					{
						if (d.available)
						{
							return d.minToneMapLuminance;
						}
						return DebugDisplaySettingsHDROutput.Strings.notAvailable;
					}
				});
				row_maxLuminance.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = delegate
					{
						if (d.available)
						{
							return d.maxToneMapLuminance;
						}
						return DebugDisplaySettingsHDROutput.Strings.notAvailable;
					}
				});
				row_maxFullFrameLuminance.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = delegate
					{
						if (d.available)
						{
							return d.maxFullFrameToneMapLuminance;
						}
						return DebugDisplaySettingsHDROutput.Strings.notAvailable;
					}
				});
				row_modeChangeRequested.children.Add(new DebugUI.Value
				{
					displayName = name,
					getter = delegate
					{
						if (d.available)
						{
							return d.HDRModeChangeRequested;
						}
						return DebugDisplaySettingsHDROutput.Strings.notAvailable;
					}
				});
			}
			table.children.Add(row_hdrActive);
			table.children.Add(row_hdrAvailable);
			table.children.Add(row_gamut);
			table.children.Add(row_format);
			table.children.Add(row_autoHdrTonemapping);
			table.children.Add(row_paperWhite);
			table.children.Add(row_minLuminance);
			table.children.Add(row_maxLuminance);
			table.children.Add(row_maxFullFrameLuminance);
			table.children.Add(row_modeChangeRequested);
			return table;
		}

		// Token: 0x0200006D RID: 109
		private static class Strings
		{
			// Token: 0x04000148 RID: 328
			public static readonly string hdrOutputAPI = "HDROutputSettings";

			// Token: 0x04000149 RID: 329
			public static readonly string displayName = "Display ";

			// Token: 0x0400014A RID: 330
			public static readonly string displayMain = " (main)";

			// Token: 0x0400014B RID: 331
			public static readonly string hdrActive = "HDR Output Active";

			// Token: 0x0400014C RID: 332
			public static readonly string hdrAvailable = "HDR Output Available";

			// Token: 0x0400014D RID: 333
			public static readonly string gamut = "Display Color Gamut";

			// Token: 0x0400014E RID: 334
			public static readonly string format = "Display Buffer Graphics Format";

			// Token: 0x0400014F RID: 335
			public static readonly string autoHdrTonemapping = "Automatic HDR Tonemapping";

			// Token: 0x04000150 RID: 336
			public static readonly string paperWhite = "Paper White Nits";

			// Token: 0x04000151 RID: 337
			public static readonly string minLuminance = "Min Tone Map Luminance";

			// Token: 0x04000152 RID: 338
			public static readonly string maxLuminance = "Max Tone Map Luminance";

			// Token: 0x04000153 RID: 339
			public static readonly string maxFullFrameLuminance = "Max Full Frame Tone Map Luminance";

			// Token: 0x04000154 RID: 340
			public static readonly string modeChangeRequested = "HDR Mode Change Requested";

			// Token: 0x04000155 RID: 341
			public static readonly string notAvailable = "N/A";
		}
	}
}
