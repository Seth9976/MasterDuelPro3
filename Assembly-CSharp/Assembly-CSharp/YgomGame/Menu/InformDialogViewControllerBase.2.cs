using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A98 RID: 2712
	public abstract class InformDialogViewControllerBase<ARG> : InformDialogViewControllerBase
	{
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06004F14 RID: 20244 RVA: 0x000F4B18 File Offset: 0x000F2D18
		protected virtual ARG arg1
		{
			get
			{
				return default(ARG);
			}
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(string prefabPath, ARG arg, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void InnerOpen(GameObject prefab, ARG arg, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x04008CD6 RID: 36054
		protected const string k_DefaultArgkey1 = "arg1";
	}
}
