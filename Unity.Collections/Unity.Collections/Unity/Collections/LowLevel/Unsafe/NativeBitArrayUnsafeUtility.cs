using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000101 RID: 257
	[GenerateTestsForBurstCompatibility]
	public static class NativeBitArrayUnsafeUtility
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x000215DC File Offset: 0x0001F7DC
		public unsafe static NativeBitArray ConvertExistingDataToNativeBitArray(void* ptr, int sizeInBytes, AllocatorManager.AllocatorHandle allocator)
		{
			UnsafeBitArray* bitArray = UnsafeBitArray.Alloc(Allocator.Persistent);
			*bitArray = new UnsafeBitArray(ptr, sizeInBytes, allocator);
			return new NativeBitArray
			{
				m_BitArray = bitArray,
				m_Allocator = Allocator.Persistent
			};
		}
	}
}
