using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000087 RID: 135
	[GenerateTestsForBurstCompatibility]
	public static class NativeArrayExtensions
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x00016ECE File Offset: 0x000150CE
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeArray<T> array, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(array.GetUnsafeReadOnlyPtr<T>(), array.Length, value) != -1;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00016EE9 File Offset: 0x000150E9
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeArray<T> array, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(array.GetUnsafeReadOnlyPtr<T>(), array.Length, value);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00016EFE File Offset: 0x000150FE
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeArray<T>.ReadOnly array, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(array.GetUnsafeReadOnlyPtr<T>(), array.m_Length, value) != -1;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00016F18 File Offset: 0x00015118
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeArray<T>.ReadOnly array, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(array.GetUnsafeReadOnlyPtr<T>(), array.m_Length, value);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00016F2C File Offset: 0x0001512C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static bool Contains<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* ptr, int length, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(ptr, length, value) != -1;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00016F3C File Offset: 0x0001513C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* ptr, int length, U value) where T : struct, ValueType, IEquatable<U>
		{
			for (int i = 0; i != length; i++)
			{
				T t = UnsafeUtility.ReadArrayElement<T>(ptr, i);
				if (t.Equals(value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00016F70 File Offset: 0x00015170
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static void CopyFrom<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			container.CopyFrom(other.AsArray());
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00016F80 File Offset: 0x00015180
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static void CopyFrom<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> container, in NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			NativeHashSet<T> nativeHashSet = other;
			using (NativeArray<T> array = nativeHashSet.ToNativeArray(Allocator.TempJob))
			{
				container.CopyFrom(array);
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00016FCC File Offset: 0x000151CC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static void CopyFrom<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> container, in UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeHashSet<T> unsafeHashSet = other;
			using (NativeArray<T> array = unsafeHashSet.ToNativeArray(Allocator.TempJob))
			{
				container.CopyFrom(array);
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00017018 File Offset: 0x00015218
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static NativeArray<U> Reinterpret<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(this NativeArray<T> array) where T : struct, ValueType where U : struct, ValueType
		{
			int tSize = UnsafeUtility.SizeOf<T>();
			int uSize = UnsafeUtility.SizeOf<U>();
			long uLen = (long)array.Length * (long)tSize / (long)uSize;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<U>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array), (int)uLen, Allocator.None);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00017050 File Offset: 0x00015250
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static bool ArraysEqual<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			if (container.Length != other.Length)
			{
				return false;
			}
			for (int i = 0; i != container.Length; i++)
			{
				T t = container[i];
				if (!t.Equals(other[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000170A4 File Offset: 0x000152A4
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckReinterpretSize<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref NativeArray<T> array) where T : struct, ValueType where U : struct, ValueType
		{
			int tSize = UnsafeUtility.SizeOf<T>();
			int uSize = UnsafeUtility.SizeOf<U>();
			long byteLen = (long)array.Length * (long)tSize;
			if (byteLen / (long)uSize * (long)uSize != byteLen)
			{
				throw new InvalidOperationException(string.Format("Types {0} (array length {1}) and {2} cannot be aliased due to size constraints. The size of the types and lengths involved must line up.", typeof(T), array.Length, typeof(U)));
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00017104 File Offset: 0x00015304
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		internal static void Initialize<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> array, int length, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct, ValueType
		{
			AllocatorManager.AllocatorHandle handle = allocator;
			array = default(NativeArray<T>);
			array.m_Buffer = (ref handle).AllocateStruct(default(T), length);
			array.m_Length = length;
			array.m_AllocatorLabel = (allocator.IsAutoDispose ? Allocator.None : allocator.ToAllocator);
			if (options == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear(array.m_Buffer, (long)(array.m_Length * UnsafeUtility.SizeOf<T>()));
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00017170 File Offset: 0x00015370
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal static void Initialize<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(this NativeArray<T> array, int length, ref U allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct, ValueType where U : struct, ValueType, AllocatorManager.IAllocator
		{
			array = default(NativeArray<T>);
			array.m_Buffer = (ref allocator).AllocateStruct(default(T), length);
			array.m_Length = length;
			array.m_AllocatorLabel = (allocator.IsAutoDispose ? Allocator.None : allocator.ToAllocator);
			if (options == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear(array.m_Buffer, (long)(array.m_Length * UnsafeUtility.SizeOf<T>()));
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000171E0 File Offset: 0x000153E0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		internal static void DisposeCheckAllocator<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> array) where T : struct, ValueType
		{
			if (array.m_Buffer == null)
			{
				throw new ObjectDisposedException("The NativeArray is already disposed.");
			}
			if (!AllocatorManager.IsCustomAllocator(array.m_AllocatorLabel))
			{
				array.Dispose();
				return;
			}
			AllocatorManager.Free(array.m_AllocatorLabel, array.m_Buffer);
			array.m_AllocatorLabel = Allocator.Invalid;
			array.m_Buffer = null;
		}

		// Token: 0x02000088 RID: 136
		public struct NativeArrayStaticId<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
		{
			// Token: 0x040003AB RID: 939
			internal static readonly SharedStatic<int> s_staticSafetyId = SharedStatic<int>.GetOrCreate<NativeArray<T>>(0U);
		}
	}
}
