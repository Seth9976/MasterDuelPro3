using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003F4 RID: 1012
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	internal class BuiltinRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		// Token: 0x06001B47 RID: 6983 RVA: 0x0003C838 File Offset: 0x0003AA38
		public bool TickRealtimeProbes()
		{
			return BuiltinRuntimeReflectionSystem.BuiltinUpdate();
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0003C84F File Offset: 0x0003AA4F
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00003D56 File Offset: 0x00001F56
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x06001B4A RID: 6986
		[StaticAccessor("GetReflectionProbes()", Type = StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool BuiltinUpdate();

		// Token: 0x06001B4B RID: 6987 RVA: 0x0003C85C File Offset: 0x0003AA5C
		[RequiredByNativeCode]
		private static BuiltinRuntimeReflectionSystem Internal_BuiltinRuntimeReflectionSystem_New()
		{
			return new BuiltinRuntimeReflectionSystem();
		}
	}
}
