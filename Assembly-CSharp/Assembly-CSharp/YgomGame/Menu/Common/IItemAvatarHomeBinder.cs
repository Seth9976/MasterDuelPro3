using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B36 RID: 2870
	public interface IItemAvatarHomeBinder
	{
		// Token: 0x060053A0 RID: 21408
		Component BindItem(GameObject target, int itemID);

		// Token: 0x060053A1 RID: 21409
		Component BindItemLarge(GameObject target, int itemID);
	}
}
