using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Profiling
{
	// Token: 0x020001EF RID: 495
	[NativeHeader("Runtime/ScriptingBackend/ScriptingApi.h")]
	[UsedByNativeCode]
	[MovedFrom("UnityEngine")]
	[NativeHeader("Runtime/Profiler/Profiler.h")]
	[NativeHeader("Runtime/Profiler/MemoryProfiler.h")]
	[NativeHeader("Runtime/Allocator/MemoryManager.h")]
	[NativeHeader("Runtime/Utilities/MemoryUtilities.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Profiler.bindings.h")]
	public sealed class Profiler
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001384 RID: 4996
		public static extern bool enabled
		{
			[NativeConditional("ENABLE_PROFILER")]
			[NativeMethod(Name = "profiler_is_enabled", IsFreeFunction = true, IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000291D0 File Offset: 0x000273D0
		[NativeMethod(Name = "ProfilerBindings::GetRuntimeMemorySizeLong", IsFreeFunction = true)]
		public static long GetRuntimeMemorySizeLong([NotNull] Object o)
		{
			if (o == null)
			{
				ThrowHelper.ThrowArgumentNullException(o, "o");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(o);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(o, "o");
			}
			return Profiler.GetRuntimeMemorySizeLong_Injected(intPtr);
		}

		// Token: 0x06001386 RID: 4998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetRuntimeMemorySizeLong_Injected(IntPtr o);
	}
}
