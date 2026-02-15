using System;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Colosseum;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B2E RID: 2862
	public class EventLogoResourceBinder : ResourceBinderBase
	{
		// Token: 0x06005377 RID: 21367 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(EventLogoResourceBinder.EventLogoPathData pathData)
		{
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetLogoPath(int id, bool isLarge)
		{
			return null;
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingEventLogo BindEventLogo(Image target, int id, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingEventLogo BindEventLogo(GameObject target, object json)
		{
			return null;
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingEventLogo BindEventLogo(GameObject target, BindingEventLogo.Context context)
		{
			return null;
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetLogoBGPath(int id)
		{
			return null;
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindEventLogoBG(Image target, int id, bool async = true)
		{
			return null;
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindEventLogoBG(Image target, string path, bool async = true)
		{
			return null;
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetBGPath(int id, int stage)
		{
			return null;
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindEventBG(Image target, int id, int stage = 0, bool async = true)
		{
			return null;
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTemplateBGPath(int id, int stage)
		{
			return null;
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindEventTemplateBG(Image target, int id, int stage = 0, bool async = true)
		{
			return null;
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetEventLogoMonsterPrefabPath()
		{
			return null;
		}

		// Token: 0x0400912C RID: 37164
		private EventLogoResourceBinder.EventLogoPathData m_PathData;

		// Token: 0x02000B2F RID: 2863
		[Serializable]
		public class EventLogoPathData
		{
			// Token: 0x0400912D RID: 37165
			public ResourceBindingPathSetting.ItemPathData m_EventLogoPath;

			// Token: 0x0400912E RID: 37166
			public string m_EventDeckSelectBGPath;

			// Token: 0x0400912F RID: 37167
			public string m_EventBGPath;

			// Token: 0x04009130 RID: 37168
			public string m_DCBGPath;

			// Token: 0x04009131 RID: 37169
			public string m_DCDeckSelectBGPath;

			// Token: 0x04009132 RID: 37170
			public string k_EventLogoMonsterPrefabPath;
		}
	}
}
