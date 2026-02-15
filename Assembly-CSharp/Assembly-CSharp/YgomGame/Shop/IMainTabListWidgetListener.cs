using System;

namespace YgomGame.Shop
{
	// Token: 0x0200092C RID: 2348
	public interface IMainTabListWidgetListener
	{
		// Token: 0x06004483 RID: 17539
		void OnInputLeftMainTab();

		// Token: 0x06004484 RID: 17540
		void OnInputRightMainTab();

		// Token: 0x06004485 RID: 17541
		void OnInputUpMainTab();

		// Token: 0x06004486 RID: 17542
		void OnInputDownMainTab();

		// Token: 0x06004487 RID: 17543
		void OnClickMainTab(int idx);
	}
}
