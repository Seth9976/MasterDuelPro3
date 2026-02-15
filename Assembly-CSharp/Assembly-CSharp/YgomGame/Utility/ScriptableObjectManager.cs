using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Utility
{
	// Token: 0x02000830 RID: 2096
	public class ScriptableObjectManager<T> where T : ScriptableObject
	{
		// Token: 0x060040A0 RID: 16544 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x000F4744 File Offset: 0x000F2944
		public static T Load(string path)
		{
			return default(T);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAsync(string path, Action<T> on_finished)
		{
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadAll()
		{
		}

		// Token: 0x0400399F RID: 14751
		private static Dictionary<string, UnityEvent<T>> loadFinishedCallbackList;
	}
}
