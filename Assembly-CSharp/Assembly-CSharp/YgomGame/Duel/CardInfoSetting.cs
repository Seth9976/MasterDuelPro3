using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Card;
using YgomGame.Settings;

namespace YgomGame.Duel
{
	// Token: 0x02000CD9 RID: 3289
	public class CardInfoSetting : ScriptableObject
	{
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06005E12 RID: 24082 RVA: 0x0000216A File Offset: 0x0000036A
		protected static CardInfoSetting Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FontSize_S
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06005E14 RID: 24084 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FontSize_M
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06005E15 RID: 24085 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FontSize_L
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06005E16 RID: 24086 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float ParaGraphSpace
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06005E17 RID: 24087 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float LineSpace
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetFrameColor(Material mat, Content.Frame frame)
		{
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x0000216A File Offset: 0x0000036A
		public static Material GetFrameMaterial()
		{
			return null;
		}

		// Token: 0x06005E1A RID: 24090 RVA: 0x000F50FC File Offset: 0x000F32FC
		public static Color GetFontColor(bool changed)
		{
			return default(Color);
		}

		// Token: 0x06005E1B RID: 24091 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFontSize(SettingsUtil.BasicParam.CARD_TEXT_SIZE size)
		{
			return 0;
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetLanguage()
		{
		}

		// Token: 0x06005E1D RID: 24093 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initializer()
		{
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFrameColorImpl(Material mat, Content.Frame frame)
		{
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x0000216A File Offset: 0x0000036A
		public Material GetFrameMaterialImpl()
		{
			return null;
		}

		// Token: 0x06005E20 RID: 24096 RVA: 0x000F5114 File Offset: 0x000F3314
		public Color GetFontColorImpl(bool changed)
		{
			return default(Color);
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetFontSizeImpl(SettingsUtil.BasicParam.CARD_TEXT_SIZE size)
		{
			return 0;
		}

		// Token: 0x04009995 RID: 39317
		private static CardInfoSetting m_Instance;

		// Token: 0x04009996 RID: 39318
		private const string PATH = "Duel/ScriptableObject/CardInfoSetting";

		// Token: 0x04009997 RID: 39319
		[SerializeField]
		private CardInfoSetting.SettingDataStringPair[] m_SettingDataTable;

		// Token: 0x04009998 RID: 39320
		private CardInfoSetting.SettingData m_CurrentSettingData;

		// Token: 0x04009999 RID: 39321
		private Dictionary<string, CardInfoSetting.LanguageScope> m_LanguageScopeTable;

		// Token: 0x0400999A RID: 39322
		private Dictionary<CardInfoSetting.LanguageScope, CardInfoSetting.SettingData> m_LanguageSettingTable;

		// Token: 0x0400999B RID: 39323
		[SerializeField]
		public CardInfoSetting.FrameColorPalette[] m_CardInfoframeColor;

		// Token: 0x0400999C RID: 39324
		[SerializeField]
		public Color m_FontColorNormal;

		// Token: 0x0400999D RID: 39325
		[SerializeField]
		public Color m_FontColorChanged;

		// Token: 0x0400999E RID: 39326
		[SerializeField]
		public Material m_FrameMaterialSrc;

		// Token: 0x02000CDA RID: 3290
		[Serializable]
		private struct SettingData
		{
			// Token: 0x0400999F RID: 39327
			public int FontSize_S;

			// Token: 0x040099A0 RID: 39328
			public int FontSize_M;

			// Token: 0x040099A1 RID: 39329
			public int FontSize_L;

			// Token: 0x040099A2 RID: 39330
			public int FontSizeMobile_S;

			// Token: 0x040099A3 RID: 39331
			public int FontSizeMobile_M;

			// Token: 0x040099A4 RID: 39332
			public int FontSizeMobile_L;

			// Token: 0x040099A5 RID: 39333
			public float LineSpacing;

			// Token: 0x040099A6 RID: 39334
			public float ParagraphSpace;
		}

		// Token: 0x02000CDB RID: 3291
		private enum LanguageScope
		{
			// Token: 0x040099A8 RID: 39336
			Generic,
			// Token: 0x040099A9 RID: 39337
			Japanese,
			// Token: 0x040099AA RID: 39338
			English,
			// Token: 0x040099AB RID: 39339
			French,
			// Token: 0x040099AC RID: 39340
			German,
			// Token: 0x040099AD RID: 39341
			Spanish,
			// Token: 0x040099AE RID: 39342
			Portuguese,
			// Token: 0x040099AF RID: 39343
			Korean,
			// Token: 0x040099B0 RID: 39344
			Italian,
			// Token: 0x040099B1 RID: 39345
			SCH,
			// Token: 0x040099B2 RID: 39346
			TCH
		}

		// Token: 0x02000CDC RID: 3292
		[Serializable]
		private struct SettingDataStringPair
		{
			// Token: 0x040099B3 RID: 39347
			public CardInfoSetting.LanguageScope language;

			// Token: 0x040099B4 RID: 39348
			public CardInfoSetting.SettingData setting;
		}

		// Token: 0x02000CDD RID: 3293
		[Serializable]
		public struct FrameColorPalette
		{
			// Token: 0x040099B5 RID: 39349
			public Content.Frame frame;

			// Token: 0x040099B6 RID: 39350
			public Color BgColor0;

			// Token: 0x040099B7 RID: 39351
			public Color BgColor1;
		}
	}
}
