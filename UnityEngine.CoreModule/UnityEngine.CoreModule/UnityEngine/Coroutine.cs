using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000197 RID: 407
	[NativeHeader("Runtime/Mono/Coroutine.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Coroutine : YieldInstruction
	{
		// Token: 0x06001017 RID: 4119 RVA: 0x00021FA9 File Offset: 0x000201A9
		private Coroutine()
		{
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00021FB4 File Offset: 0x000201B4
		~Coroutine()
		{
			Coroutine.ReleaseCoroutine(this.m_Ptr);
		}

		// Token: 0x06001019 RID: 4121
		[FreeFunction("Coroutine::CleanupCoroutineGC", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseCoroutine(IntPtr ptr);

		// Token: 0x04000652 RID: 1618
		internal IntPtr m_Ptr;

		// Token: 0x02000198 RID: 408
		internal static class BindingsMarshaller
		{
			// Token: 0x0600101A RID: 4122 RVA: 0x00021FEC File Offset: 0x000201EC
			public static IntPtr ConvertToNative(Coroutine coroutine)
			{
				return coroutine.m_Ptr;
			}
		}
	}
}
