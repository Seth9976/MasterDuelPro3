using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B3C RID: 2876
	public interface IItemFieldBinder
	{
		// Token: 0x060053AA RID: 21418
		Component BindItem(GameObject target, int itemID);

		// Token: 0x060053AB RID: 21419
		Component BindItemLarge(GameObject target, int itemID);
	}
}
