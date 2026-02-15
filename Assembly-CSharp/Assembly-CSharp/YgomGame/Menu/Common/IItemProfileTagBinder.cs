using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B40 RID: 2880
	public interface IItemProfileTagBinder
	{
		// Token: 0x060053B2 RID: 21426
		Component BindItem(GameObject target, int itemID);

		// Token: 0x060053B3 RID: 21427
		Component BindItemLarge(GameObject target, int itemID);
	}
}
