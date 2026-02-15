using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000320 RID: 800
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h")]
	[UsedByNativeCode]
	internal struct AsyncRequestNativeArrayData
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x0002E910 File Offset: 0x0002CB10
		public static AsyncRequestNativeArrayData CreateAndCheckAccess<T>(NativeArray<T> array) where T : struct
		{
			bool flag = array.m_AllocatorLabel == Allocator.Temp || array.m_AllocatorLabel == Allocator.TempJob;
			if (flag)
			{
				throw new ArgumentException("AsyncGPUReadback cannot use Temp memory as input since the result may only become available at an unspecified point in the future.");
			}
			return new AsyncRequestNativeArrayData
			{
				nativeArrayBuffer = array.GetUnsafePtr<T>(),
				lengthInBytes = (long)array.Length * (long)UnsafeUtility.SizeOf<T>()
			};
		}

		// Token: 0x04000857 RID: 2135
		public unsafe void* nativeArrayBuffer;

		// Token: 0x04000858 RID: 2136
		public long lengthInBytes;
	}
}
