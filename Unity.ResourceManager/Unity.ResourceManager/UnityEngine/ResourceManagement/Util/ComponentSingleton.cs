using System;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000029 RID: 41
	[ExecuteInEditMode]
	public abstract class ComponentSingleton<T> : MonoBehaviour where T : ComponentSingleton<T>
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005BB2 File Offset: 0x00003DB2
		public static bool Exists
		{
			get
			{
				return ComponentSingleton<T>.s_Instance != null;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public static T Instance
		{
			get
			{
				if (ComponentSingleton<T>.s_Instance == null)
				{
					T t;
					if ((t = ComponentSingleton<T>.FindInstance()) == null)
					{
						t = ComponentSingleton<T>.CreateNewSingleton();
					}
					ComponentSingleton<T>.s_Instance = t;
				}
				return ComponentSingleton<T>.s_Instance;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005BF5 File Offset: 0x00003DF5
		private static T FindInstance()
		{
			return Object.FindObjectOfType<T>();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005BFC File Offset: 0x00003DFC
		protected virtual string GetGameObjectName()
		{
			return typeof(T).Name;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005C10 File Offset: 0x00003E10
		private static T CreateNewSingleton()
		{
			GameObject go = new GameObject();
			if (Application.isPlaying)
			{
				Object.DontDestroyOnLoad(go);
				go.hideFlags = HideFlags.DontSave;
			}
			else
			{
				go.hideFlags = HideFlags.HideAndDontSave;
			}
			T instance = go.AddComponent<T>();
			go.name = instance.GetGameObjectName();
			return instance;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005C5C File Offset: 0x00003E5C
		private void Awake()
		{
			if (ComponentSingleton<T>.s_Instance != null && ComponentSingleton<T>.s_Instance != this)
			{
				Object.DestroyImmediate(base.gameObject);
				return;
			}
			ComponentSingleton<T>.s_Instance = this as T;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005CA9 File Offset: 0x00003EA9
		public static void DestroySingleton()
		{
			if (ComponentSingleton<T>.Exists)
			{
				Object.DestroyImmediate(ComponentSingleton<T>.Instance.gameObject);
				ComponentSingleton<T>.s_Instance = default(T);
			}
		}

		// Token: 0x04000073 RID: 115
		private static T s_Instance;
	}
}
