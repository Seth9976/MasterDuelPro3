using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000140 RID: 320
	[GenerateTestsForBurstCompatibility]
	public struct UnsafeScratchAllocator
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x00029B86 File Offset: 0x00027D86
		public unsafe UnsafeScratchAllocator(void* ptr, int capacityInBytes)
		{
			this.m_Pointer = ptr;
			this.m_LengthInBytes = 0;
			this.m_CapacityInBytes = capacityInBytes;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00029B9D File Offset: 0x00027D9D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckAllocationDoesNotExceedCapacity(ulong requestedSize)
		{
			if (requestedSize > (ulong)((long)this.m_CapacityInBytes))
			{
				throw new ArgumentException(string.Format("Cannot allocate more than provided size in UnsafeScratchAllocator. Requested: {0} Size: {1} Capacity: {2}", requestedSize, this.m_LengthInBytes, this.m_CapacityInBytes));
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00029BD8 File Offset: 0x00027DD8
		public unsafe void* Allocate(int sizeInBytes, int alignmentInBytes)
		{
			if (sizeInBytes == 0)
			{
				return null;
			}
			ulong alignmentMask = (ulong)((long)(alignmentInBytes - 1));
			long num = ((long)((IntPtr)this.m_Pointer) + (long)this.m_LengthInBytes + (long)alignmentMask) & (long)(~(long)alignmentMask);
			long lengthInBytes = (long)((byte*)(void*)((IntPtr)num) - (byte*)this.m_Pointer);
			lengthInBytes += (long)sizeInBytes;
			this.m_LengthInBytes = (int)lengthInBytes;
			return (void*)((IntPtr)num);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00029C3A File Offset: 0x00027E3A
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe void* Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int count = 1) where T : struct, ValueType
		{
			return this.Allocate(UnsafeUtility.SizeOf<T>() * count, UnsafeUtility.AlignOf<T>());
		}

		// Token: 0x04000525 RID: 1317
		private unsafe void* m_Pointer;

		// Token: 0x04000526 RID: 1318
		private int m_LengthInBytes;

		// Token: 0x04000527 RID: 1319
		private readonly int m_CapacityInBytes;
	}
}
