using System;
using UnityEngine;

namespace YgomSystem.Extension
{
	// Token: 0x0200076B RID: 1899
	public static class ComponentExtension
	{
		// Token: 0x06003B2A RID: 15146 RVA: 0x000F3850 File Offset: 0x000F1A50
		public static T GetOrAddComponent<T>(this Component component) where T : Component
		{
			return default(T);
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000F3868 File Offset: 0x000F1A68
		public static T AddOrReplaceComponent<T>(this Component component) where T : Component
		{
			return default(T);
		}
	}
}
