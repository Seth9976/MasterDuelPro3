using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Notification
{
	// Token: 0x02000A21 RID: 2593
	[Serializable]
	public class TopicsCardUVSetting : ScriptableObject
	{
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x0000216A File Offset: 0x0000036A
		public TopicsCardUVSetting.SettingMap BannerMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x0000216A File Offset: 0x0000036A
		public TopicsCardUVSetting.ThumbSetting GetSetting(int mrk)
		{
			return null;
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsExitsSetting(int mrk)
		{
			return false;
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportData(int mrk, RawImage rawImage)
		{
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportData(int mrk)
		{
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveData(int mrk)
		{
		}

		// Token: 0x04008951 RID: 35153
		[SerializeField]
		private TopicsCardUVSetting.SettingMap m_BannerMap;

		// Token: 0x02000A22 RID: 2594
		[Serializable]
		public class ThumbSetting
		{
			// Token: 0x06004B4A RID: 19274 RVA: 0x00002739 File Offset: 0x00000939
			public ThumbSetting(int mrk)
			{
			}

			// Token: 0x06004B4B RID: 19275 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportRawImage(RawImage rawImage)
			{
			}

			// Token: 0x06004B4C RID: 19276 RVA: 0x0000216D File Offset: 0x0000036D
			public void ExportRawImage(RawImage rawImage)
			{
			}

			// Token: 0x04008952 RID: 35154
			public int mrk;

			// Token: 0x04008953 RID: 35155
			public Vector2 uvRectPos;

			// Token: 0x04008954 RID: 35156
			public Vector2 uvRectSize;
		}

		// Token: 0x02000A23 RID: 2595
		[Serializable]
		public class SettingMap
		{
			// Token: 0x170006E1 RID: 1761
			// (get) Token: 0x06004B4D RID: 19277 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004B4E RID: 19278 RVA: 0x0000216D File Offset: 0x0000036D
			public List<TopicsCardUVSetting.ThumbSetting> Settings
			{
				get
				{
					return null;
				}
				private set
				{
				}
			}

			// Token: 0x06004B4F RID: 19279 RVA: 0x0000216A File Offset: 0x0000036A
			public TopicsCardUVSetting.ThumbSetting GetSetting(int mrk)
			{
				return null;
			}

			// Token: 0x06004B50 RID: 19280 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsExists(int mrk)
			{
				return false;
			}

			// Token: 0x06004B51 RID: 19281 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(int mrk, RawImage rawImage)
			{
			}

			// Token: 0x06004B52 RID: 19282 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(int mrk)
			{
			}

			// Token: 0x06004B53 RID: 19283 RVA: 0x0000216D File Offset: 0x0000036D
			public void Remove(int mrk)
			{
			}

			// Token: 0x04008955 RID: 35157
			[SerializeField]
			private List<TopicsCardUVSetting.ThumbSetting> m_Settings;

			// Token: 0x04008956 RID: 35158
			private Dictionary<int, TopicsCardUVSetting.ThumbSetting> m_SettingsMap;
		}
	}
}
