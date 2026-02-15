using System;
using UnityEngine;

namespace YgomGame.Utility
{
	// Token: 0x0200081D RID: 2077
	public class CommonGameParent
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x0000216A File Offset: 0x0000036A
		public static Transform root
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x0000216A File Offset: 0x0000036A
		public static GameObject Create(string name, params Type[] c)
		{
			return null;
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DestroyGameRoot()
		{
		}

		// Token: 0x0400393D RID: 14653
		private static Transform _root;
	}
}
