using System;
using UnityEngine;
using YgomGame.Solo;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B61 RID: 2913
	public class SoloResourceBinder : ResourceBinderBase
	{
		// Token: 0x0600543A RID: 21562 RVA: 0x000F4C2A File Offset: 0x000F2E2A
		public SoloResourceBinder(string cardThumbSettingPath)
		{
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadCardThumbSettings()
		{
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnloadCardThumbSettings()
		{
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnloadForce()
		{
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetSoloCardThumbDataPath()
		{
			return null;
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSoloMrk(SoloCardThumbSettings.Format format, int id, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsExistSetting(SoloCardThumbSettings.Format format, int id)
		{
			return false;
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingSoloCardThumb BindCardThumb(RectTransform target, int id, SoloCardThumbSettings.Format thumbFormat)
		{
			return null;
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingSoloCardThumb BindCardThumbOther(RectTransform target, int id, SoloCardThumbSettings.Format thumbFormat)
		{
			return null;
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingSoloCardThumb BindCardThumbLootSource(RectTransform target, int id)
		{
			return null;
		}

		// Token: 0x040091B8 RID: 37304
		private readonly string m_CardThumbSettingPath;

		// Token: 0x040091B9 RID: 37305
		private SoloCardThumbSettings m_CardThumbSettings;

		// Token: 0x040091BA RID: 37306
		private int m_CardThumbRefCount;
	}
}
