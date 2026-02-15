using System;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B4C RID: 2892
	public class OutGameBGResourceBinder : ResourceBinderBase
	{
		// Token: 0x060053D7 RID: 21463 RVA: 0x000F4C2A File Offset: 0x000F2E2A
		public OutGameBGResourceBinder(string frontBGPath, string backBGPath)
		{
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingGameObjectEx BindWallpaper(GameObject target, int id, bool async = true, UnityAction<GameObject> onCreated = null)
		{
			return null;
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetFrontBGPath(int id)
		{
			return null;
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetBackBGPath(int id)
		{
			return null;
		}

		// Token: 0x04009155 RID: 37205
		public readonly string m_FrontBGPath;

		// Token: 0x04009156 RID: 37206
		public readonly string m_BackBGPath;

		// Token: 0x04009157 RID: 37207
		public const string k_EmptyWallpaperPath = "EmptyWallpaper";

		// Token: 0x04009158 RID: 37208
		private OutGameBGManager m_BGManager;
	}
}
