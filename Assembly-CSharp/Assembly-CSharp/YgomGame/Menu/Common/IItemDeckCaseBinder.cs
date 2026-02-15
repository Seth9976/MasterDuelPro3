using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B3A RID: 2874
	public interface IItemDeckCaseBinder
	{
		// Token: 0x060053A6 RID: 21414
		Component BindItem(GameObject target, int itemID);

		// Token: 0x060053A7 RID: 21415
		Component BindItemLarge(GameObject target, int itemID);
	}
}
