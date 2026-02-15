using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B39 RID: 2873
	public interface IItemConsumeBinder
	{
		// Token: 0x060053A4 RID: 21412
		Component BindItem(GameObject target, int itemID);

		// Token: 0x060053A5 RID: 21413
		Component BindItemLarge(GameObject target, int itemID);
	}
}
