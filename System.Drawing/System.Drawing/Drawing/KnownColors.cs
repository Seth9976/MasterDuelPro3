using System;

namespace System.Drawing
{
	// Token: 0x02000055 RID: 85
	internal static class KnownColors
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000B96A File Offset: 0x00009B6A
		static KnownColors()
		{
			if (GDIPlus.RunningOnWindows())
			{
				KnownColors.RetrieveWindowsSystemColors();
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B994 File Offset: 0x00009B94
		private static uint GetSysColor(GetSysColorIndex index)
		{
			uint num = GDIPlus.Win32GetSysColor(index);
			return 4278190080U | ((num & 255U) << 16) | (num & 65280U) | (num >> 16);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		private static void RetrieveWindowsSystemColors()
		{
			KnownColors.ArgbValues[1] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_ACTIVEBORDER);
			KnownColors.ArgbValues[2] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_ACTIVECAPTION);
			KnownColors.ArgbValues[3] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_CAPTIONTEXT);
			KnownColors.ArgbValues[4] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_APPWORKSPACE);
			KnownColors.ArgbValues[5] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNFACE);
			KnownColors.ArgbValues[6] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNSHADOW);
			KnownColors.ArgbValues[7] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_3DDKSHADOW);
			KnownColors.ArgbValues[8] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_3DLIGHT);
			KnownColors.ArgbValues[9] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNHIGHLIGHT);
			KnownColors.ArgbValues[10] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNTEXT);
			KnownColors.ArgbValues[11] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BACKGROUND);
			KnownColors.ArgbValues[12] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_GRAYTEXT);
			KnownColors.ArgbValues[13] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_HIGHLIGHT);
			KnownColors.ArgbValues[14] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_HIGHLIGHTTEXT);
			KnownColors.ArgbValues[15] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_HOTLIGHT);
			KnownColors.ArgbValues[16] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INACTIVEBORDER);
			KnownColors.ArgbValues[17] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INACTIVECAPTION);
			KnownColors.ArgbValues[18] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INACTIVECAPTIONTEXT);
			KnownColors.ArgbValues[19] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INFOBK);
			KnownColors.ArgbValues[20] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INFOTEXT);
			KnownColors.ArgbValues[21] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_MENU);
			KnownColors.ArgbValues[22] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_MENUTEXT);
			KnownColors.ArgbValues[23] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_SCROLLBAR);
			KnownColors.ArgbValues[24] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_WINDOW);
			KnownColors.ArgbValues[25] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_WINDOWFRAME);
			KnownColors.ArgbValues[26] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_WINDOWTEXT);
			KnownColors.ArgbValues[168] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNFACE);
			KnownColors.ArgbValues[169] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNHIGHLIGHT);
			KnownColors.ArgbValues[170] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNSHADOW);
			KnownColors.ArgbValues[171] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_GRADIENTACTIVECAPTION);
			KnownColors.ArgbValues[172] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_GRADIENTINACTIVECAPTION);
			KnownColors.ArgbValues[173] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_MENUBAR);
			KnownColors.ArgbValues[174] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_MENUHIGHLIGHT);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		public static void Update(int knownColor, int color)
		{
			KnownColors.ArgbValues[knownColor] = (uint)color;
		}

		// Token: 0x04000186 RID: 390
		internal static uint[] ArgbValues = new uint[]
		{
			0U, 4292137160U, 4278211811U, uint.MaxValue, 4286611584U, 4293716440U, 4289505433U, 4285624164U, 4294045666U, uint.MaxValue,
			4278190080U, 4278210200U, 4289505433U, 4281428677U, uint.MaxValue, 4278190208U, 4292137160U, 4286224095U, 4292404472U, 4294967265U,
			4278190080U, uint.MaxValue, 4278190080U, 4292137160U, uint.MaxValue, 4278190080U, 4278190080U, 16777215U, 4293982463U, 4294634455U,
			4278255615U, 4286578644U, 4293984255U, 4294309340U, 4294960324U, 4278190080U, 4294962125U, 4278190335U, 4287245282U, 4289014314U,
			4292786311U, 4284456608U, 4286578432U, 4291979550U, 4294934352U, 4284782061U, 4294965468U, 4292613180U, 4278255615U, 4278190219U,
			4278225803U, 4290283019U, 4289309097U, 4278215680U, 4290623339U, 4287299723U, 4283788079U, 4294937600U, 4288230092U, 4287299584U,
			4293498490U, 4287609995U, 4282924427U, 4281290575U, 4278243025U, 4287889619U, 4294907027U, 4278239231U, 4285098345U, 4280193279U,
			4289864226U, 4294966000U, 4280453922U, 4294902015U, 4292664540U, 4294506751U, 4294956800U, 4292519200U, 4286611584U, 4278222848U,
			4289593135U, 4293984240U, 4294928820U, 4291648604U, 4283105410U, 4294967280U, 4293977740U, 4293322490U, 4294963445U, 4286381056U,
			4294965965U, 4289583334U, 4293951616U, 4292935679U, 4294638290U, 4292072403U, 4287688336U, 4294948545U, 4294942842U, 4280332970U,
			4287090426U, 4286023833U, 4289774814U, 4294967264U, 4278255360U, 4281519410U, 4294635750U, 4294902015U, 4286578688U, 4284927402U,
			4278190285U, 4290401747U, 4287852763U, 4282168177U, 4286277870U, 4278254234U, 4282962380U, 4291237253U, 4279834992U, 4294311930U,
			4294960353U, 4294960309U, 4294958765U, 4278190208U, 4294833638U, 4286611456U, 4285238819U, 4294944000U, 4294919424U, 4292505814U,
			4293847210U, 4288215960U, 4289720046U, 4292571283U, 4294963157U, 4294957753U, 4291659071U, 4294951115U, 4292714717U, 4289781990U,
			4286578816U, 4294901760U, 4290547599U, 4282477025U, 4287317267U, 4294606962U, 4294222944U, 4281240407U, 4294964718U, 4288696877U,
			4290822336U, 4287090411U, 4285160141U, 4285563024U, 4294966010U, 4278255487U, 4282811060U, 4291998860U, 4278222976U, 4292394968U,
			4294927175U, 4282441936U, 4293821166U, 4294303411U, uint.MaxValue, 4294309365U, 4294967040U, 4288335154U, 4293716440U, uint.MaxValue,
			4289505433U, 4282226175U, 4288526827U, 4293716440U, 4281428677U
		};
	}
}
