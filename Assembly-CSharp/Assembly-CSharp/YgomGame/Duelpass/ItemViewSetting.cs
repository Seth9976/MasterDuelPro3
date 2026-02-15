using System;
using UnityEngine;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C49 RID: 3145
	[Serializable]
	public class ItemViewSetting
	{
		// Token: 0x060059F6 RID: 23030 RVA: 0x00002739 File Offset: 0x00000939
		public ItemViewSetting(int itemId, Transform transform)
		{
		}

		// Token: 0x04009569 RID: 38249
		public int itemId;

		// Token: 0x0400956A RID: 38250
		public Vector3 localPosition;

		// Token: 0x0400956B RID: 38251
		public Vector3 eulerAngles;

		// Token: 0x0400956C RID: 38252
		public Vector3 localScale;
	}
}
