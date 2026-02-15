using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D0 RID: 208
	internal static class MemoryUtilities
	{
		// Token: 0x06000321 RID: 801 RVA: 0x00013C2C File Offset: 0x00011E2C
		public unsafe static T* Malloc<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int count, Allocator allocator) where T : struct, ValueType
		{
			return (T*)UnsafeUtility.Malloc((long)(UnsafeUtility.SizeOf<T>() * count), UnsafeUtility.AlignOf<T>(), allocator);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00013C41 File Offset: 0x00011E41
		public unsafe static void Free<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* p, Allocator allocator) where T : struct, ValueType
		{
			UnsafeUtility.Free((void*)p, allocator);
		}
	}
}
