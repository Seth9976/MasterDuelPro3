using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A9B RID: 2715
	public abstract class InformDialogViewControllerBase<ARG1, ARG2, ARG3, ARG4> : InformDialogViewControllerBase<ARG1, ARG2, ARG3>
	{
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x000F4B78 File Offset: 0x000F2D78
		protected virtual ARG4 arg4
		{
			get
			{
				return default(ARG4);
			}
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(string prefabPath, ARG1 arg1, ARG2 arg2, ARG3 arg3, ARG4 arg4, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(GameObject prefab, ARG1 arg1, ARG2 arg2, ARG3 arg3, ARG4 arg4, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x04008CD9 RID: 36057
		protected const string k_DefaultArgkey4 = "arg4";
	}
}
