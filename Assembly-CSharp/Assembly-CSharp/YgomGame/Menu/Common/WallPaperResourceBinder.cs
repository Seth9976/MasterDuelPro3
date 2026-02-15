using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B64 RID: 2916
	public class WallPaperResourceBinder : ResourceBinderBase
	{
		// Token: 0x06005445 RID: 21573 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(WallPaperResourceBinder.WallPaperResourcePathData iconPath)
		{
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetIconPath(int itemId)
		{
			return null;
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTopicsThumbPath(int itemId)
		{
			return null;
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindIcon(Image target, int itemId, bool async = true)
		{
			return null;
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindThumb(Image target, int itemId, bool async = true)
		{
			return null;
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindTopicsThumb(Image target, int itemId, bool async = true)
		{
			return null;
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemWallpaperBinder_002EBindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x040091CA RID: 37322
		private WallPaperResourceBinder.WallPaperResourcePathData m_WallPaperPath;

		// Token: 0x02000B65 RID: 2917
		[Serializable]
		public class WallPaperResourcePathData
		{
			// Token: 0x040091CB RID: 37323
			public string m_WallPaperResourcePath;

			// Token: 0x040091CC RID: 37324
			public string topicsThumbPath;
		}
	}
}
