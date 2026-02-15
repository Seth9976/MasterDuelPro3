using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B10 RID: 2832
	public class AvatarResourceBinder : ResourceBinderBase
	{
		// Token: 0x06005233 RID: 21043 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(AvatarResourceBinder.AvatarResourcePathData iconPath)
		{
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetIconPath(int itemId)
		{
			return null;
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemAvatarBinder_002EBindItem(GameObject target, int itemId)
		{
			return null;
		}

		// Token: 0x04009096 RID: 37014
		private AvatarResourceBinder.AvatarResourcePathData m_AvaterPath;

		// Token: 0x02000B11 RID: 2833
		[Serializable]
		public class AvatarResourcePathData
		{
			// Token: 0x04009097 RID: 37015
			public string m_AvatarResourcePath;
		}
	}
}
