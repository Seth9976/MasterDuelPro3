using System;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C5A RID: 3162
	[Serializable]
	public class DuelLiveSubCategoryData : DuelLiveProductGroupData
	{
		// Token: 0x06005A46 RID: 23110 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsMatchProduct(IProductContext product)
		{
			return false;
		}
	}
}
