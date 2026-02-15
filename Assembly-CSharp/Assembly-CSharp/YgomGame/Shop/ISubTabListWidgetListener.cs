using System;
using YgomSystem.UI;

namespace YgomGame.Shop
{
	// Token: 0x02000933 RID: 2355
	public interface ISubTabListWidgetListener
	{
		// Token: 0x060044A0 RID: 17568
		bool OnInputDirectionSubCategory(PadInputDirection direction);

		// Token: 0x060044A1 RID: 17569
		void OnClickSubCategory(int dataIdx);

		// Token: 0x060044A2 RID: 17570
		void OnClickSubCategoryGroup(int dataIdx);

		// Token: 0x060044A3 RID: 17571
		void OnClickSubCategorySection(int dataIdx, int sectionIdx);
	}
}
