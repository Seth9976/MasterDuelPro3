using System;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace UnityEngine.Rendering
{
	// Token: 0x0200011B RID: 283
	internal static class SceneExtensions
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0001E318 File Offset: 0x0001C518
		public static string GetGUID(this Scene scene)
		{
			return (string)SceneExtensions.s_SceneGUID.GetValue(scene);
		}

		// Token: 0x04000500 RID: 1280
		private static PropertyInfo s_SceneGUID = typeof(Scene).GetProperty("guid", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
