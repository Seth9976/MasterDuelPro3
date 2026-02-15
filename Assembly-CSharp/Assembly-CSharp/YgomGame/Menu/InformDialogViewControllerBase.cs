using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A97 RID: 2711
	public abstract class InformDialogViewControllerBase : DialogViewControllerBase
	{
		// Token: 0x06004F11 RID: 20241 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(string prefabPath, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(GameObject prefab, Dictionary<string, object> args = null)
		{
		}
	}
}
