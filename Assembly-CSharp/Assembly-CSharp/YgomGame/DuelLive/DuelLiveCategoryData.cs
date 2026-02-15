using System;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C4D RID: 3149
	[Serializable]
	public class DuelLiveCategoryData : DuelLiveProductGroupTreeData<DuelLiveSubCategoryData>
	{
		// Token: 0x06005A07 RID: 23047 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsMatchProduct(IProductContext product)
		{
			return false;
		}
	}
}
