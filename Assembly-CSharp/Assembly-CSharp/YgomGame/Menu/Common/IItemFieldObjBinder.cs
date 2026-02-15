using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B3D RID: 2877
	public interface IItemFieldObjBinder
	{
		// Token: 0x060053AC RID: 21420
		Component BindItem(GameObject target, int itemID);

		// Token: 0x060053AD RID: 21421
		Component BindItemLarge(GameObject target, int itemID);
	}
}
