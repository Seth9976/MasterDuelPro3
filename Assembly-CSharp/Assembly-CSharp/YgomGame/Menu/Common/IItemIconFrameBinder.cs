using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B3F RID: 2879
	public interface IItemIconFrameBinder
	{
		// Token: 0x060053B0 RID: 21424
		Component BindItem(GameObject target, int itemID);

		// Token: 0x060053B1 RID: 21425
		Component BindItemLarge(GameObject target, int itemID);
	}
}
