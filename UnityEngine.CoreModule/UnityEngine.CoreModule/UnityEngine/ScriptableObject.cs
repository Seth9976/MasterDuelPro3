using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B1 RID: 433
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[NativeClass(null)]
	[ExtensionOfNativeClass]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ScriptableObject : Object
	{
		// Token: 0x0600110A RID: 4362 RVA: 0x0002442A File Offset: 0x0002262A
		public ScriptableObject()
		{
			ScriptableObject.CreateScriptableObject(this);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0002443C File Offset: 0x0002263C
		public static ScriptableObject CreateInstance(Type type)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromType(type, true);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00024458 File Offset: 0x00022658
		public static T CreateInstance<T>() where T : ScriptableObject
		{
			return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
		}

		// Token: 0x0600110D RID: 4365
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateScriptableObject([Writable] ScriptableObject self);

		// Token: 0x0600110E RID: 4366 RVA: 0x00024480 File Offset: 0x00022680
		[NativeMethod(Name = "Scripting::CreateScriptableObjectWithType", IsFreeFunction = true, ThrowsException = true)]
		internal static ScriptableObject CreateScriptableObjectInstanceFromType(Type type, bool applyDefaultsAndReset)
		{
			return Unmarshal.UnmarshalUnityObject<ScriptableObject>(ScriptableObject.CreateScriptableObjectInstanceFromType_Injected(type, applyDefaultsAndReset));
		}

		// Token: 0x0600110F RID: 4367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateScriptableObjectInstanceFromType_Injected(Type type, bool applyDefaultsAndReset);
	}
}
