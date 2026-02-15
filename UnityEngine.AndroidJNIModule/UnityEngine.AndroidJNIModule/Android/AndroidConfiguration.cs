using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Android
{
	// Token: 0x02000014 RID: 20
	[NativeAsStruct]
	[NativeType(Header = "Modules/AndroidJNI/Public/AndroidConfiguration.bindings.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AndroidConfiguration
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006BF9 File Offset: 0x00004DF9
		private int colorMode { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006C01 File Offset: 0x00004E01
		public int densityDpi { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006C09 File Offset: 0x00004E09
		public float fontScale { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006C11 File Offset: 0x00004E11
		public int fontWeightAdjustment { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006C19 File Offset: 0x00004E19
		public AndroidKeyboard keyboard { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006C21 File Offset: 0x00004E21
		public AndroidHardwareKeyboardHidden hardKeyboardHidden { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006C29 File Offset: 0x00004E29
		public AndroidKeyboardHidden keyboardHidden { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006C31 File Offset: 0x00004E31
		public int mobileCountryCode { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006C39 File Offset: 0x00004E39
		public int mobileNetworkCode { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006C41 File Offset: 0x00004E41
		public AndroidNavigation navigation { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006C49 File Offset: 0x00004E49
		public AndroidNavigationHidden navigationHidden { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006C51 File Offset: 0x00004E51
		public AndroidOrientation orientation { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006C59 File Offset: 0x00004E59
		public int screenHeightDp { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006C61 File Offset: 0x00004E61
		public int screenWidthDp { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006C69 File Offset: 0x00004E69
		public int smallestScreenWidthDp { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006C71 File Offset: 0x00004E71
		private int screenLayout { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006C79 File Offset: 0x00004E79
		public AndroidTouchScreen touchScreen { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006C81 File Offset: 0x00004E81
		private int uiMode { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006C89 File Offset: 0x00004E89
		private string primaryLocaleCountry { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006C91 File Offset: 0x00004E91
		private string primaryLocaleLanguage { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006C9C File Offset: 0x00004E9C
		public AndroidLocale[] locales
		{
			get
			{
				bool flag = this.primaryLocaleCountry == null && this.primaryLocaleLanguage == null;
				AndroidLocale[] array;
				if (flag)
				{
					array = new AndroidLocale[0];
				}
				else
				{
					array = new AndroidLocale[]
					{
						new AndroidLocale(this.primaryLocaleCountry, this.primaryLocaleLanguage)
					};
				}
				return array;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006CE9 File Offset: 0x00004EE9
		public AndroidColorModeHdr colorModeHdr
		{
			get
			{
				return (AndroidColorModeHdr)(this.colorMode & 12);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006CF4 File Offset: 0x00004EF4
		public AndroidColorModeWideColorGamut colorModeWideColorGamut
		{
			get
			{
				return (AndroidColorModeWideColorGamut)(this.colorMode & 3);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006CFE File Offset: 0x00004EFE
		public AndroidScreenLayoutDirection screenLayoutDirection
		{
			get
			{
				return (AndroidScreenLayoutDirection)(this.screenLayout & 192);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006D0C File Offset: 0x00004F0C
		public AndroidScreenLayoutLong screenLayoutLong
		{
			get
			{
				return (AndroidScreenLayoutLong)(this.screenLayout & 48);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006D17 File Offset: 0x00004F17
		public AndroidScreenLayoutRound screenLayoutRound
		{
			get
			{
				return (AndroidScreenLayoutRound)(this.screenLayout & 768);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006D25 File Offset: 0x00004F25
		public AndroidScreenLayoutSize screenLayoutSize
		{
			get
			{
				return (AndroidScreenLayoutSize)(this.screenLayout & 15);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006D30 File Offset: 0x00004F30
		public AndroidUIModeNight uiModeNight
		{
			get
			{
				return (AndroidUIModeNight)(this.uiMode & 48);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006D3B File Offset: 0x00004F3B
		public AndroidUIModeType uiModeType
		{
			get
			{
				return (AndroidUIModeType)(this.uiMode & 15);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006D48 File Offset: 0x00004F48
		[Preserve]
		public override string ToString()
		{
			StringBuilder contents = new StringBuilder();
			contents.AppendLine(string.Format("* ColorMode, Hdr: {0}", this.colorModeHdr));
			contents.AppendLine(string.Format("* ColorMode, Gamut: {0}", this.colorModeWideColorGamut));
			contents.AppendLine(string.Format("* DensityDpi: {0}", this.densityDpi));
			contents.AppendLine(string.Format("* FontScale: {0}", this.fontScale));
			contents.AppendLine(string.Format("* FontWeightAdj: {0}", this.fontWeightAdjustment));
			contents.AppendLine(string.Format("* Keyboard: {0}", this.keyboard));
			contents.AppendLine(string.Format("* Keyboard Hidden, Hard: {0}", this.hardKeyboardHidden));
			contents.AppendLine(string.Format("* Keyboard Hidden, Normal: {0}", this.keyboardHidden));
			contents.AppendLine(string.Format("* Mcc: {0}", this.mobileCountryCode));
			contents.AppendLine(string.Format("* Mnc: {0}", this.mobileNetworkCode));
			contents.AppendLine(string.Format("* Navigation: {0}", this.navigation));
			contents.AppendLine(string.Format("* NavigationHidden: {0}", this.navigationHidden));
			contents.AppendLine(string.Format("* Orientation: {0}", this.orientation));
			contents.AppendLine(string.Format("* ScreenHeightDp: {0}", this.screenHeightDp));
			contents.AppendLine(string.Format("* ScreenWidthDp: {0}", this.screenWidthDp));
			contents.AppendLine(string.Format("* SmallestScreenWidthDp: {0}", this.smallestScreenWidthDp));
			contents.AppendLine(string.Format("* ScreenLayout, Direction: {0}", this.screenLayoutDirection));
			contents.AppendLine(string.Format("* ScreenLayout, Size: {0}", this.screenLayoutSize));
			contents.AppendLine(string.Format("* ScreenLayout, Long: {0}", this.screenLayoutLong));
			contents.AppendLine(string.Format("* ScreenLayout, Round: {0}", this.screenLayoutRound));
			contents.AppendLine(string.Format("* TouchScreen: {0}", this.touchScreen));
			contents.AppendLine(string.Format("* UiMode, Night: {0}", this.uiModeNight));
			contents.AppendLine(string.Format("* UiMode, Type: {0}", this.uiModeType));
			contents.AppendLine(string.Format("* Locales ({0}):", this.locales.Length));
			for (int i = 0; i < this.locales.Length; i++)
			{
				AndroidLocale j = this.locales[i];
				contents.AppendLine(string.Format("* Locale[{0}] {1}-{2}", i, j.country, j.language));
			}
			return contents.ToString();
		}
	}
}
