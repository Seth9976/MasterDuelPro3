using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000072 RID: 114
	public static class NativeArrayUnsafeUtility
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000439C File Offset: 0x0000259C
		public unsafe static NativeArray<T> ConvertExistingDataToNativeArray<T>(void* dataPointer, int length, Allocator allocator) where T : struct
		{
			return new NativeArray<T>
			{
				m_Buffer = dataPointer,
				m_Length = length,
				m_AllocatorLabel = allocator
			};
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000043D4 File Offset: 0x000025D4
		public unsafe static void* GetUnsafePtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000043EC File Offset: 0x000025EC
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004404 File Offset: 0x00002604
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T>.ReadOnly nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000441C File Offset: 0x0000261C
		public unsafe static void* GetUnsafeBufferPointerWithoutChecks<T>(NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}
	}
}
