using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B27 RID: 2855
	public class ConsumeItemBinder : ResourceBinderBase
	{
		// Token: 0x0600533A RID: 21306 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ConsumeItemBinder.ConsumeItemPathData data)
		{
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetIconPath(int consumeId, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindIcon(Image target, int consumeId, bool async = true, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemConsumeBinder_002EBindItem(GameObject target, int consumeId)
		{
			return null;
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemConsumeBinder_002EBindItemLarge(GameObject target, int consumeId)
		{
			return null;
		}

		// Token: 0x04009115 RID: 37141
		private ConsumeItemBinder.ConsumeItemPathData m_Data;

		// Token: 0x02000B28 RID: 2856
		[Serializable]
		public class ConsumeItemPathData
		{
			// Token: 0x04009116 RID: 37142
			public ResourceBindingPathSetting.ItemPathData m_ConsumeItemPath;
		}
	}
}
