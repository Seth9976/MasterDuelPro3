using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A9A RID: 2714
	public abstract class InformDialogViewControllerBase<ARG1, ARG2, ARG3> : InformDialogViewControllerBase<ARG1, ARG2>
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x000F4B58 File Offset: 0x000F2D58
		protected virtual ARG3 arg3
		{
			get
			{
				return default(ARG3);
			}
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(string prefabPath, ARG1 arg1, ARG2 arg2, ARG3 arg3, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(GameObject prefab, ARG1 arg1, ARG2 arg2, ARG3 arg3, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x04008CD8 RID: 36056
		protected const string k_DefaultArgkey3 = "arg3";
	}
}
