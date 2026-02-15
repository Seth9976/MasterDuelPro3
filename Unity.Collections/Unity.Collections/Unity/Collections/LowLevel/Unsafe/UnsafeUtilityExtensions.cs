using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200014D RID: 333
	[GenerateTestsForBurstCompatibility]
	public static class UnsafeUtilityExtensions
	{
		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002A8C8 File Offset: 0x00028AC8
		internal unsafe static void MemSwap(void* ptr, void* otherPtr, long size)
		{
			byte* dst = (byte*)ptr;
			byte* src = (byte*)otherPtr;
			byte* tmp = stackalloc byte[(UIntPtr)1024];
			while (size > 0L)
			{
				long numBytes = math.min(size, 1024L);
				UnsafeUtility.MemCpy((void*)tmp, (void*)dst, numBytes);
				UnsafeUtility.MemCpy((void*)dst, (void*)src, numBytes);
				UnsafeUtility.MemCpy((void*)src, (void*)tmp, numBytes);
				size -= numBytes;
				src += numBytes;
				dst += numBytes;
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002A91D File Offset: 0x00028B1D
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static T ReadArrayElementBoundsChecked<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(void* source, int index, int capacity) where T : struct, ValueType
		{
			return UnsafeUtility.ReadArrayElement<T>(source, index);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002A926 File Offset: 0x00028B26
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void WriteArrayElementBoundsChecked<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(void* destination, int index, T value, int capacity) where T : struct, ValueType
		{
			UnsafeUtility.WriteArrayElement<T>(destination, index, value);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0002A930 File Offset: 0x00028B30
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AddressOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(in T value) where T : struct, ValueType
		{
			return ILSupport.AddressOf<T>(in value);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002A938 File Offset: 0x00028B38
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T AsRef<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(in T value) where T : struct, ValueType
		{
			return ILSupport.AsRef<T>(in value);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0002A940 File Offset: 0x00028B40
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private unsafe static void CheckMemSwapOverlap(byte* dst, byte* src, long size)
		{
			if (dst + size != src && src + size != dst)
			{
				throw new InvalidOperationException("MemSwap memory blocks are overlapped.");
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0002A95B File Offset: 0x00028B5B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckIndexRange(int index, int capacity)
		{
			if (index > capacity - 1 || index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Attempt to read or write from array index {0}, which is out of bounds. Array capacity is {1}. ", index, capacity) + "This may lead to a crash, data corruption, or reading invalid data.");
			}
		}
	}
}
