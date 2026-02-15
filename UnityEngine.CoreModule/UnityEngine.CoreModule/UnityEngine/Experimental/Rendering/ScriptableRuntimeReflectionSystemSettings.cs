using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003F6 RID: 1014
	[NativeHeader("Runtime/Camera/ScriptableRuntimeReflectionSystem.h")]
	[RequiredByNativeCode]
	public static class ScriptableRuntimeReflectionSystemSettings
	{
		// Token: 0x17000434 RID: 1076
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x0003C874 File Offset: 0x0003AA74
		private static IScriptableRuntimeReflectionSystem Internal_ScriptableRuntimeReflectionSystemSettings_system
		{
			[RequiredByNativeCode]
			set
			{
				bool flag = ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation != value;
				if (flag)
				{
					bool flag2 = ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation != null;
					if (flag2)
					{
						ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation.Dispose();
					}
				}
				ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0003C8C8 File Offset: 0x0003AAC8
		private static ScriptableRuntimeReflectionSystemWrapper Internal_ScriptableRuntimeReflectionSystemSettings_instance
		{
			[RequiredByNativeCode]
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.s_Instance;
			}
		}

		// Token: 0x06001B50 RID: 6992
		[StaticAccessor("ScriptableRuntimeReflectionSystem", StaticAccessorType.DoubleColon)]
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScriptingDirtyReflectionSystemInstance();

		// Token: 0x04000D84 RID: 3460
		private static ScriptableRuntimeReflectionSystemWrapper s_Instance = new ScriptableRuntimeReflectionSystemWrapper();
	}
}
