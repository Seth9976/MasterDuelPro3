using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x0200003D RID: 61
	[GenerateTestsForBurstCompatibility]
	public static class CollectionHelper
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00004DCA File Offset: 0x00002FCA
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckAllocator(AllocatorManager.AllocatorHandle allocator)
		{
			if (!CollectionHelper.ShouldDeallocate(allocator))
			{
				throw new ArgumentException(string.Format("Allocator {0} must not be None or Invalid", allocator));
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004DEA File Offset: 0x00002FEA
		public static int Log2Floor(int value)
		{
			return 31 - math.lzcnt((uint)value);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004DF5 File Offset: 0x00002FF5
		public static int Log2Ceil(int value)
		{
			return 32 - math.lzcnt((uint)(value - 1));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004E02 File Offset: 0x00003002
		public static int Align(int size, int alignmentPowerOfTwo)
		{
			if (alignmentPowerOfTwo == 0)
			{
				return size;
			}
			return (size + alignmentPowerOfTwo - 1) & ~(alignmentPowerOfTwo - 1);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004E13 File Offset: 0x00003013
		public static ulong Align(ulong size, ulong alignmentPowerOfTwo)
		{
			if (alignmentPowerOfTwo == 0UL)
			{
				return size;
			}
			return (size + alignmentPowerOfTwo - 1UL) & ~(alignmentPowerOfTwo - 1UL);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004E26 File Offset: 0x00003026
		public unsafe static bool IsAligned(void* p, int alignmentPowerOfTwo)
		{
			return ((byte*)p & ((byte*)((long)alignmentPowerOfTwo) - 1L)) == null;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004E34 File Offset: 0x00003034
		public static bool IsAligned(ulong offset, int alignmentPowerOfTwo)
		{
			return (offset & (ulong)((long)alignmentPowerOfTwo - 1L)) == 0UL;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004E41 File Offset: 0x00003041
		public static bool IsPowerOfTwo(int value)
		{
			return (value & (value - 1)) == 0;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004E4C File Offset: 0x0000304C
		public unsafe static uint Hash(void* ptr, int bytes)
		{
			ulong hash = 5381UL;
			while (bytes > 0)
			{
				int num = --bytes;
				ulong c = (ulong)((byte*)ptr)[num];
				hash = (hash << 5) + hash + c;
			}
			return (uint)hash;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004E80 File Offset: 0x00003080
		[ExcludeFromBurstCompatTesting("Used only for debugging, and uses managed strings")]
		internal static void WriteLayout(Type type)
		{
			Console.WriteLine(string.Format("   Offset | Bytes  | Name     Layout: {0}", 0), type.Name);
			foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Console.WriteLine("   {0, 6} | {1, 6} | {2}", Marshal.OffsetOf(type, field.Name), Marshal.SizeOf(field.FieldType), field.Name);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004EF4 File Offset: 0x000030F4
		internal static bool ShouldDeallocate(AllocatorManager.AllocatorHandle allocator)
		{
			return allocator.ToAllocator > Allocator.None;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004F00 File Offset: 0x00003100
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: AssumeRange(0L, 2147483647L)]
		internal static int AssumePositive(int value)
		{
			return value;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004F03 File Offset: 0x00003103
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "ENABLE_UNITY_COLLECTIONS_CHECKS", GenericTypeArguments = new Type[] { typeof(NativeArray<int>) })]
		internal static void CheckIsUnmanaged<T>()
		{
			if (!UnsafeUtility.IsUnmanaged<T>())
			{
				throw new ArgumentException(string.Format("{0} used in native collection is not blittable or not primitive", typeof(T)));
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004F26 File Offset: 0x00003126
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckIntPositivePowerOfTwo(int value)
		{
			if (value <= 0 || (value & (value - 1)) != 0)
			{
				throw new ArgumentException(string.Format("Alignment requested: {0} is not a non-zero, positive power of two.", value));
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004F4F File Offset: 0x0000314F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckUlongPositivePowerOfTwo(ulong value)
		{
			if (value <= 0UL || (value & (value - 1UL)) != 0UL)
			{
				throw new ArgumentException(string.Format("Alignment requested: {0} is not a non-zero, positive power of two.", value));
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004F7B File Offset: 0x0000317B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void CheckIndexInRange(int index, int length)
		{
			if (index >= length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in container of '{1}' Length.", index, length));
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004F9D File Offset: 0x0000319D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckCapacityInRange(int capacity, int length)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be positive.", capacity));
			}
			if (capacity < length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} is out of range in container of '{1}' Length.", capacity, length));
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004FDC File Offset: 0x000031DC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(AllocatorManager.AllocatorHandle)
		})]
		public static NativeArray<T> CreateNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(int length, ref U allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct, ValueType where U : struct, ValueType, AllocatorManager.IAllocator
		{
			NativeArray<T> nativeArray;
			if (!allocator.IsCustomAllocator)
			{
				nativeArray = new NativeArray<T>(length, allocator.ToAllocator, options);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(length, ref allocator, options);
			}
			return nativeArray;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005024 File Offset: 0x00003224
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static NativeArray<T> CreateNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int length, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct, ValueType
		{
			NativeArray<T> nativeArray;
			if (!AllocatorManager.IsCustomAllocator(allocator))
			{
				nativeArray = new NativeArray<T>(length, allocator.ToAllocator, options);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(length, allocator, options);
			}
			return nativeArray;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005060 File Offset: 0x00003260
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static NativeArray<T> CreateNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(NativeArray<T> array, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeArray<T> nativeArray;
			if (!AllocatorManager.IsCustomAllocator(allocator))
			{
				nativeArray = new NativeArray<T>(array, allocator.ToAllocator);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(array.Length, allocator, NativeArrayOptions.ClearMemory);
				nativeArray.CopyFrom(array);
			}
			return nativeArray;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000050A8 File Offset: 0x000032A8
		[ExcludeFromBurstCompatTesting("Managed array")]
		public static NativeArray<T> CreateNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T[] array, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeArray<T> nativeArray;
			if (!AllocatorManager.IsCustomAllocator(allocator))
			{
				nativeArray = new NativeArray<T>(array, allocator.ToAllocator);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(array.Length, allocator, NativeArrayOptions.ClearMemory);
				nativeArray.CopyFrom(array);
			}
			return nativeArray;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000050EC File Offset: 0x000032EC
		[ExcludeFromBurstCompatTesting("Managed array")]
		public static NativeArray<T> CreateNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(T[] array, ref U allocator) where T : struct, ValueType where U : struct, ValueType, AllocatorManager.IAllocator
		{
			NativeArray<T> nativeArray;
			if (!allocator.IsCustomAllocator)
			{
				nativeArray = new NativeArray<T>(array, allocator.ToAllocator);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(array.Length, ref allocator, NativeArrayOptions.ClearMemory);
				nativeArray.CopyFrom(array);
			}
			return nativeArray;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000513A File Offset: 0x0000333A
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static void DisposeNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(NativeArray<T> nativeArray, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			(ref nativeArray).DisposeCheckAllocator<T>();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000513A File Offset: 0x0000333A
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static void Dispose<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(NativeArray<T> nativeArray) where T : struct, ValueType
		{
			(ref nativeArray).DisposeCheckAllocator<T>();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005144 File Offset: 0x00003344
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckConvertArguments<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int length) where T : struct, ValueType
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
			}
			if (!UnsafeUtility.IsUnmanaged<T>())
			{
				throw new InvalidOperationException(string.Format("{0} used in NativeArray<{1}> must be unmanaged (contain no managed types).", typeof(T), typeof(T)));
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005190 File Offset: 0x00003390
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static NativeArray<T> ConvertExistingDataToNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(void* dataPointer, int length, AllocatorManager.AllocatorHandle allocator, bool setTempMemoryHandle = false) where T : struct, ValueType
		{
			NativeArray<T> nativeArray = default(NativeArray<T>);
			nativeArray.m_Buffer = dataPointer;
			nativeArray.m_Length = length;
			if (!allocator.IsCustomAllocator)
			{
				nativeArray.m_AllocatorLabel = allocator.ToAllocator;
			}
			else
			{
				nativeArray.m_AllocatorLabel = Allocator.None;
			}
			return nativeArray;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000051D7 File Offset: 0x000033D7
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static NativeArray<T> ConvertExistingNativeListToNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(ref NativeList<T> nativeList, int length, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			return CollectionHelper.ConvertExistingDataToNativeArray<T>((void*)nativeList.GetUnsafePtr<T>(), length, allocator, false);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000051EC File Offset: 0x000033EC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int),
			typeof(AllocatorManager.AllocatorHandle)
		})]
		public static NativeParallelMultiHashMap<TKey, TValue> CreateNativeParallelMultiHashMap<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(int length, ref U allocator) where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType where U : struct, ValueType, AllocatorManager.IAllocator
		{
			NativeParallelMultiHashMap<TKey, TValue> container = default(NativeParallelMultiHashMap<TKey, TValue>);
			container.Initialize<U>(length, ref allocator);
			return container;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00002C47 File Offset: 0x00000E47
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "ENABLE_UNITY_COLLECTIONS_CHECKS", GenericTypeArguments = new Type[] { typeof(CollectionHelper.DummyJob) })]
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		public static void CheckReflectionDataCorrect<T>(IntPtr reflectionData)
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000520B File Offset: 0x0000340B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[BurstDiscard]
		private static void CheckReflectionDataCorrectInternal<T>(IntPtr reflectionData, ref bool burstCompiled)
		{
			if (reflectionData == IntPtr.Zero)
			{
				throw new InvalidOperationException(string.Format("Reflection data was not set up by an Initialize() call. For generic job types, please include [assembly: RegisterGenericJobType(typeof({0}))] in your source file.", typeof(T)));
			}
			burstCompiled = false;
		}

		// Token: 0x04000086 RID: 134
		public const int CacheLineSize = 64;

		// Token: 0x0200003E RID: 62
		[StructLayout(LayoutKind.Explicit)]
		internal struct LongDoubleUnion
		{
			// Token: 0x04000087 RID: 135
			[FieldOffset(0)]
			internal long longValue;

			// Token: 0x04000088 RID: 136
			[FieldOffset(0)]
			internal double doubleValue;
		}

		// Token: 0x0200003F RID: 63
		[BurstCompile]
		public struct DummyJob : IJob
		{
			// Token: 0x06000159 RID: 345 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Execute()
			{
			}
		}
	}
}
