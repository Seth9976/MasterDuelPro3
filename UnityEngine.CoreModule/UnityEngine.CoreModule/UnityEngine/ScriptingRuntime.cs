using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001B2 RID: 434
	[VisibleToOtherModules]
	[NativeHeader("Runtime/Export/Scripting/ScriptingRuntime.h")]
	internal class ScriptingRuntime
	{
		// Token: 0x06001110 RID: 4368
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetAllUserAssemblies();
	}
}
