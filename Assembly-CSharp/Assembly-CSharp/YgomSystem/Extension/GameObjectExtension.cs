using System;
using UnityEngine;
using YgomSystem.UI;

namespace YgomSystem.Extension
{
	// Token: 0x0200076D RID: 1901
	public static class GameObjectExtension
	{
		// Token: 0x06003B45 RID: 15173 RVA: 0x000F390C File Offset: 0x000F1B0C
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			return default(T);
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x000F3924 File Offset: 0x000F1B24
		public static T AddOrReplaceComponent<T>(this GameObject gameObject) where T : Component
		{
			return default(T);
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveComponent<T>(this GameObject gameObject) where T : Component
		{
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetSurfaceActive(this GameObject gameObject, bool value, params Selector[] selectors)
		{
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetSurfaceActive(this GameObject gameObject)
		{
			return false;
		}
	}
}
