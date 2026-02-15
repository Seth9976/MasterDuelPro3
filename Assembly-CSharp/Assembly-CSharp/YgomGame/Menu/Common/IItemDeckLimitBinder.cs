using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B3B RID: 2875
	public interface IItemDeckLimitBinder
	{
		// Token: 0x060053A8 RID: 21416
		Component BindItem(GameObject target, int itemID);

		// Token: 0x060053A9 RID: 21417
		Component BindItemLarge(GameObject target, int itemID);
	}
}
