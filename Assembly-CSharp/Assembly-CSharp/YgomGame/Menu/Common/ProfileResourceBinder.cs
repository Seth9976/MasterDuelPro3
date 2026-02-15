using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B50 RID: 2896
	public class ProfileResourceBinder : ResourceBinderBase
	{
		// Token: 0x060053F4 RID: 21492 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ProfileResourceBinder.ProfileResource path)
		{
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetProfileTagIconPath(bool isLarge = false)
		{
			return null;
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindIcon(Image target, bool async = true, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemProfileTagBinder_002EBindItem(GameObject target, int consumeId)
		{
			return null;
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemProfileTagBinder_002EBindItemLarge(GameObject target, int consumeId)
		{
			return null;
		}

		// Token: 0x04009165 RID: 37221
		private ProfileResourceBinder.ProfileResource m_PathData;

		// Token: 0x02000B51 RID: 2897
		[Serializable]
		public class ProfileResource
		{
			// Token: 0x04009166 RID: 37222
			public ResourceBindingPathSetting.ItemPathData m_ProfileTagIconPath;
		}
	}
}
