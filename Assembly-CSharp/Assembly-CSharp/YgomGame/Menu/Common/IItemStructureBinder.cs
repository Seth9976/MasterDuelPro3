using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B42 RID: 2882
	public interface IItemStructureBinder
	{
		// Token: 0x060053B5 RID: 21429
		Component BindItem(GameObject target, int itemID);
	}
}
