using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A99 RID: 2713
	public abstract class InformDialogViewControllerBase<ARG1, ARG2> : InformDialogViewControllerBase<ARG1>
	{
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x000F4B38 File Offset: 0x000F2D38
		protected virtual ARG2 arg2
		{
			get
			{
				return default(ARG2);
			}
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(string prefabPath, ARG1 arg1, ARG2 arg2, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(GameObject prefab, ARG1 arg1, ARG2 arg2, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x04008CD7 RID: 36055
		protected const string k_DefaultArgkey2 = "arg2";
	}
}
