using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B3E RID: 2878
	public interface IItemIconBinder
	{
		// Token: 0x060053AE RID: 21422
		Component BindItem(GameObject target, int itemID);

		// Token: 0x060053AF RID: 21423
		Component BindItemLarge(GameObject target, int itemID);
	}
}
