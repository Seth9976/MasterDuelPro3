using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Solo
{
	// Token: 0x020008EF RID: 2287
	public class SoloCardThumbSettings : ScriptableObject
	{
		// Token: 0x060042F4 RID: 17140 RVA: 0x0000216A File Offset: 0x0000036A
		public SoloCardThumbSettings.ThumbSetting GetSetting(SoloCardThumbSettings.Format format, int id)
		{
			return null;
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsExitsSetting(SoloCardThumbSettings.Format format, int id)
		{
			return false;
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportData(SoloCardThumbSettings.Format format, int id, int mrk, RawImage rawImage, RawImage rawImageOther)
		{
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveData(SoloCardThumbSettings.Format format, int id)
		{
		}

		// Token: 0x0400815B RID: 33115
		[SerializeField]
		private SoloCardThumbSettings.SettingMap m_GateMap;

		// Token: 0x0400815C RID: 33116
		[SerializeField]
		private SoloCardThumbSettings.SettingMap m_ChapterMap;

		// Token: 0x0400815D RID: 33117
		[SerializeField]
		private SoloCardThumbSettings.SettingMap m_LootSourceMap;

		// Token: 0x020008F0 RID: 2288
		[Serializable]
		public class ThumbSetting
		{
			// Token: 0x060042F9 RID: 17145 RVA: 0x00002739 File Offset: 0x00000939
			public ThumbSetting(int id)
			{
			}

			// Token: 0x060042FA RID: 17146 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportRawImage(RawImage rawImage)
			{
			}

			// Token: 0x060042FB RID: 17147 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportRawImageOther(RawImage rawImage)
			{
			}

			// Token: 0x060042FC RID: 17148 RVA: 0x0000216D File Offset: 0x0000036D
			public void ExportRawImage(RawImage rawImage)
			{
			}

			// Token: 0x060042FD RID: 17149 RVA: 0x0000216D File Offset: 0x0000036D
			public void ExportRawImageOther(RawImage rawImage)
			{
			}

			// Token: 0x0400815E RID: 33118
			public int id;

			// Token: 0x0400815F RID: 33119
			public int mrk;

			// Token: 0x04008160 RID: 33120
			public Vector2 uvRectPos;

			// Token: 0x04008161 RID: 33121
			public Vector2 uvRectSize;

			// Token: 0x04008162 RID: 33122
			public Vector2 uvRectPosOther;

			// Token: 0x04008163 RID: 33123
			public Vector2 uvRectSizeOther;
		}

		// Token: 0x020008F1 RID: 2289
		[Serializable]
		private class SettingMap
		{
			// Token: 0x060042FE RID: 17150 RVA: 0x0000216A File Offset: 0x0000036A
			public SoloCardThumbSettings.ThumbSetting GetSetting(int id)
			{
				return null;
			}

			// Token: 0x060042FF RID: 17151 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsExists(int id)
			{
				return false;
			}

			// Token: 0x06004300 RID: 17152 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(int id, int mrk, RawImage rawImage, RawImage rawImageOther)
			{
			}

			// Token: 0x06004301 RID: 17153 RVA: 0x0000216D File Offset: 0x0000036D
			public void RemoveData(int id)
			{
			}

			// Token: 0x04008164 RID: 33124
			[SerializeField]
			private List<SoloCardThumbSettings.ThumbSetting> m_Settings;

			// Token: 0x04008165 RID: 33125
			private Dictionary<int, SoloCardThumbSettings.ThumbSetting> m_SettingsMap;
		}

		// Token: 0x020008F2 RID: 2290
		public enum Format
		{
			// Token: 0x04008167 RID: 33127
			Gate,
			// Token: 0x04008168 RID: 33128
			Chapter,
			// Token: 0x04008169 RID: 33129
			LootSource
		}
	}
}
