using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine.SceneManagement
{
	// Token: 0x02000250 RID: 592
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h")]
	public class SceneManager
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060014C2 RID: 5314
		public static extern int sceneCount
		{
			[NativeHeader("Runtime/SceneManager/SceneManager.h")]
			[NativeMethod("GetSceneCount")]
			[StaticAccessor("GetSceneManager()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0002BD58 File Offset: 0x00029F58
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetActiveScene()
		{
			Scene scene;
			SceneManager.GetActiveScene_Injected(out scene);
			return scene;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0002BD70 File Offset: 0x00029F70
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneAt(int index)
		{
			Scene scene;
			SceneManager.GetSceneAt_Injected(index, out scene);
			return scene;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0002BD88 File Offset: 0x00029F88
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static AsyncOperation UnloadSceneAsyncInternal(Scene scene, UnloadSceneOptions options)
		{
			IntPtr intPtr = SceneManager.UnloadSceneAsyncInternal_Injected(ref scene, options);
			return (intPtr == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0002BDAC File Offset: 0x00029FAC
		private static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			bool flag = !SceneManager.s_AllowLoadScene;
			AsyncOperation asyncOperation;
			if (flag)
			{
				asyncOperation = null;
			}
			else
			{
				asyncOperation = SceneManagerAPI.ActiveAPI.LoadSceneAsyncByNameOrIndex(sceneName, sceneBuildIndex, parameters, mustCompleteNextFrame);
			}
			return asyncOperation;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0002BDDC File Offset: 0x00029FDC
		[RequiredByNativeCode]
		internal static AsyncOperation LoadFirstScene_Internal(bool async)
		{
			return SceneManagerAPI.ActiveAPI.LoadFirstScene(async);
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060014C8 RID: 5320 RVA: 0x0002BDFC File Offset: 0x00029FFC
		// (remove) Token: 0x060014C9 RID: 5321 RVA: 0x0002BE30 File Offset: 0x0002A030
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event UnityAction<Scene, LoadSceneMode> sceneLoaded;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060014CA RID: 5322 RVA: 0x0002BE64 File Offset: 0x0002A064
		// (remove) Token: 0x060014CB RID: 5323 RVA: 0x0002BE98 File Offset: 0x0002A098
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event UnityAction<Scene> sceneUnloaded;

		// Token: 0x060014CC RID: 5324 RVA: 0x0002BECC File Offset: 0x0002A0CC
		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, false);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0002BEE8 File Offset: 0x0002A0E8
		public static AsyncOperation UnloadSceneAsync(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, options);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0002BF04 File Offset: 0x0002A104
		[RequiredByNativeCode]
		private static void Internal_SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			bool flag = SceneManager.sceneLoaded != null;
			if (flag)
			{
				SceneManager.sceneLoaded(scene, mode);
			}
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0002BF30 File Offset: 0x0002A130
		[RequiredByNativeCode]
		private static void Internal_SceneUnloaded(Scene scene)
		{
			bool flag = SceneManager.sceneUnloaded != null;
			if (flag)
			{
				SceneManager.sceneUnloaded(scene);
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0002BF58 File Offset: 0x0002A158
		[RequiredByNativeCode]
		private static void Internal_ActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
		{
			bool flag = SceneManager.activeSceneChanged != null;
			if (flag)
			{
				SceneManager.activeSceneChanged(previousActiveScene, newActiveScene);
			}
		}

		// Token: 0x060014D2 RID: 5330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveScene_Injected(out Scene ret);

		// Token: 0x060014D3 RID: 5331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneAt_Injected(int index, out Scene ret);

		// Token: 0x060014D4 RID: 5332
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr UnloadSceneAsyncInternal_Injected([In] ref Scene scene, UnloadSceneOptions options);

		// Token: 0x040007B6 RID: 1974
		internal static bool s_AllowLoadScene = true;

		// Token: 0x040007B9 RID: 1977
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static UnityAction<Scene, Scene> activeSceneChanged;
	}
}
