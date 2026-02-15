using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002A RID: 42
	public static class ComponentSingleton<TType> where TType : Component
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000066EC File Offset: 0x000048EC
		public static TType instance
		{
			get
			{
				if (ComponentSingleton<TType>.s_Instance == null)
				{
					GameObject gameObject = new GameObject("Default " + typeof(TType).Name);
					gameObject.hideFlags = HideFlags.HideAndDontSave;
					Object.DontDestroyOnLoad(gameObject);
					gameObject.SetActive(false);
					ComponentSingleton<TType>.s_Instance = gameObject.AddComponent<TType>();
				}
				return ComponentSingleton<TType>.s_Instance;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000674D File Offset: 0x0000494D
		public static void Release()
		{
			if (ComponentSingleton<TType>.s_Instance != null)
			{
				CoreUtils.Destroy(ComponentSingleton<TType>.s_Instance.gameObject);
				ComponentSingleton<TType>.s_Instance = default(TType);
			}
		}

		// Token: 0x040000A8 RID: 168
		private static TType s_Instance;
	}
}
