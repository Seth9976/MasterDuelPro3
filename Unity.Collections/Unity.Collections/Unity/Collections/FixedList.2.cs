using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x0200004D RID: 77
	[GenerateTestsForBurstCompatibility]
	internal struct FixedList
	{
		// Token: 0x06000212 RID: 530 RVA: 0x00006E9A File Offset: 0x0000509A
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int PaddingBytes<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
		{
			return math.max(0, math.min(6, (1 << math.tzcnt(UnsafeUtility.SizeOf<T>())) - 2));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006EB9 File Offset: 0x000050B9
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int StorageBytes<[global::System.Runtime.CompilerServices.IsUnmanaged] BUFFER, [global::System.Runtime.CompilerServices.IsUnmanaged] T>() where BUFFER : struct, ValueType where T : struct, ValueType
		{
			return UnsafeUtility.SizeOf<BUFFER>() - UnsafeUtility.SizeOf<ushort>() - FixedList.PaddingBytes<T>();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006ECC File Offset: 0x000050CC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int Capacity<[global::System.Runtime.CompilerServices.IsUnmanaged] BUFFER, [global::System.Runtime.CompilerServices.IsUnmanaged] T>() where BUFFER : struct, ValueType where T : struct, ValueType
		{
			return FixedList.StorageBytes<BUFFER, T>() / UnsafeUtility.SizeOf<T>();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006EDC File Offset: 0x000050DC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckResize<[global::System.Runtime.CompilerServices.IsUnmanaged] BUFFER, [global::System.Runtime.CompilerServices.IsUnmanaged] T>(int newLength) where BUFFER : struct, ValueType where T : struct, ValueType
		{
			int Capacity = FixedList.Capacity<BUFFER, T>();
			if (newLength < 0 || newLength > Capacity)
			{
				throw new IndexOutOfRangeException(string.Format("NewLength {0} is out of range of '{1}' Capacity.", newLength, Capacity));
			}
		}
	}
}
