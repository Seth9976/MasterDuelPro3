using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Burst;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000074 RID: 116
	[NativeHeader("Runtime/Export/Unsafe/UnsafeUtility.bindings.h")]
	[StaticAccessor("UnsafeUtility", StaticAccessorType.DoubleColon)]
	public static class UnsafeUtility
	{
		// Token: 0x06000147 RID: 327 RVA: 0x000044E4 File Offset: 0x000026E4
		public static bool IsBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable(typeof(T));
		}

		// Token: 0x06000148 RID: 328
		[ThreadSafe(ThrowsException = false)]
		[BurstAuthorizedExternalMethod]
		[VisibleToOtherModules(new string[] { "UnityEngine.AIModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LeakRecord(IntPtr handle, LeakCategory category, int callstacksToSkip);

		// Token: 0x06000149 RID: 329
		[ThreadSafe(ThrowsException = false)]
		[BurstAuthorizedExternalMethod]
		[VisibleToOtherModules(new string[] { "UnityEngine.AIModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LeakErase(IntPtr handle, LeakCategory category);

		// Token: 0x0600014A RID: 330
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* MallocTracked(long size, int alignment, Allocator allocator, int callstacksToSkip);

		// Token: 0x0600014B RID: 331
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void FreeTracked(void* memory, Allocator allocator);

		// Token: 0x0600014C RID: 332
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* Malloc(long size, int alignment, Allocator allocator);

		// Token: 0x0600014D RID: 333
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Free(void* memory, Allocator allocator);

		// Token: 0x0600014E RID: 334
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpy(void* destination, void* source, long size);

		// Token: 0x0600014F RID: 335
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpyReplicate(void* destination, void* source, int size, int count);

		// Token: 0x06000150 RID: 336
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpyStride(void* destination, int destinationStride, void* source, int sourceStride, int elementSize, int count);

		// Token: 0x06000151 RID: 337
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemMove(void* destination, void* source, long size);

		// Token: 0x06000152 RID: 338
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemSet(void* destination, byte value, long size);

		// Token: 0x06000153 RID: 339 RVA: 0x00004505 File Offset: 0x00002705
		public unsafe static void MemClear(void* destination, long size)
		{
			UnsafeUtility.MemSet(destination, 0, size);
		}

		// Token: 0x06000154 RID: 340
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int MemCmp(void* ptr1, void* ptr2, long size);

		// Token: 0x06000155 RID: 341
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int SizeOf(Type type);

		// Token: 0x06000156 RID: 342
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBlittable(Type type);

		// Token: 0x06000157 RID: 343
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetScriptingTypeFlags(Type type);

		// Token: 0x06000158 RID: 344 RVA: 0x00004514 File Offset: 0x00002714
		private static bool IsBlittableValueType(Type t)
		{
			return t.IsValueType && UnsafeUtility.IsBlittable(t);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004538 File Offset: 0x00002738
		private static string GetReasonForTypeNonBlittableImpl(Type t, string name)
		{
			bool flag = !t.IsValueType;
			string text;
			if (flag)
			{
				text = string.Format("{0} is not blittable because it is not of value type ({1})\n", name, t);
			}
			else
			{
				bool isPrimitive = t.IsPrimitive;
				if (isPrimitive)
				{
					text = string.Format("{0} is not blittable ({1})\n", name, t);
				}
				else
				{
					string ret = "";
					foreach (FieldInfo f in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
					{
						bool flag2 = !UnsafeUtility.IsBlittableValueType(f.FieldType);
						if (flag2)
						{
							ret += UnsafeUtility.GetReasonForTypeNonBlittableImpl(f.FieldType, string.Format("{0}.{1}", name, f.Name));
						}
					}
					text = ret;
				}
			}
			return text;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000045EC File Offset: 0x000027EC
		internal static bool IsArrayBlittable(Array arr)
		{
			return UnsafeUtility.IsBlittableValueType(arr.GetType().GetElementType());
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004610 File Offset: 0x00002810
		internal static bool IsGenericListBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable<T>();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004628 File Offset: 0x00002828
		internal static string GetReasonForArrayNonBlittable(Array arr)
		{
			Type t = arr.GetType().GetElementType();
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(t, t.Name);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004654 File Offset: 0x00002854
		internal static string GetReasonForGenericListNonBlittable<T>() where T : struct
		{
			Type t = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(t, t.Name);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004680 File Offset: 0x00002880
		public static bool IsUnmanaged<T>()
		{
			return (UnsafeUtility.TypeFlagsCache<T>.flags & 1) == 0;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000469C File Offset: 0x0000289C
		public static int AlignOf<T>() where T : struct
		{
			return UnsafeUtility.SizeOf<UnsafeUtility.AlignOfHelper<T>>() - UnsafeUtility.SizeOf<T>();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000046B9 File Offset: 0x000028B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			UnsafeUtility.InternalCopyPtrToStructure<T>(ptr, out output);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000046C4 File Offset: 0x000028C4
		private unsafe static void InternalCopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			output = *(T*)ptr;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000046D2 File Offset: 0x000028D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			UnsafeUtility.InternalCopyStructureToPtr<T>(ref input, ptr);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000046C4 File Offset: 0x000028C4
		private unsafe static void InternalCopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			*(T*)ptr = input;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000046DD File Offset: 0x000028DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static T ReadArrayElement<T>(void* source, int index)
		{
			return *(T*)((byte*)source + (long)index * (long)sizeof(T));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000046F1 File Offset: 0x000028F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static T ReadArrayElementWithStride<T>(void* source, int index, int stride)
		{
			return *(T*)((byte*)source + (long)index * (long)stride);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004700 File Offset: 0x00002900
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteArrayElement<T>(void* destination, int index, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)sizeof(T)) = value;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004715 File Offset: 0x00002915
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteArrayElementWithStride<T>(void* destination, int index, int stride, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)stride) = value;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004725 File Offset: 0x00002925
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AddressOf<T>(ref T output) where T : struct
		{
			return (void*)(&output);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004728 File Offset: 0x00002928
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SizeOf<T>() where T : struct
		{
			return sizeof(T);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004725 File Offset: 0x00002925
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T As<U, T>(ref U from)
		{
			return ref from;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004725 File Offset: 0x00002925
		internal static T As<T>(object from) where T : class
		{
			return from;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004725 File Offset: 0x00002925
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ref T AsRef<T>(void* ptr) where T : struct
		{
			return ref *(T*)ptr;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004730 File Offset: 0x00002930
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ref T ArrayElementAsRef<T>(void* ptr, int index) where T : struct
		{
			return ref *(T*)((byte*)ptr + (long)index * (long)sizeof(T));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004740 File Offset: 0x00002940
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int EnumToInt<T>(T enumValue) where T : struct, IConvertible
		{
			int value = 0;
			UnsafeUtility.InternalEnumToInt<T>(ref enumValue, ref value);
			return value;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000475F File Offset: 0x0000295F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void InternalEnumToInt<T>(ref T enumValue, ref int intValue)
		{
			intValue = enumValue;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004765 File Offset: 0x00002965
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EnumEquals<T>(T lhs, T rhs) where T : struct, IConvertible
		{
			return lhs == rhs;
		}

		// Token: 0x02000075 RID: 117
		internal struct TypeFlagsCache<T>
		{
			// Token: 0x06000171 RID: 369 RVA: 0x0000476D File Offset: 0x0000296D
			static TypeFlagsCache()
			{
				UnsafeUtility.TypeFlagsCache<T>.Init(ref UnsafeUtility.TypeFlagsCache<T>.flags);
			}

			// Token: 0x06000172 RID: 370 RVA: 0x0000477B File Offset: 0x0000297B
			[BurstDiscard]
			private static void Init(ref int flags)
			{
				flags = UnsafeUtility.GetScriptingTypeFlags(typeof(T));
			}

			// Token: 0x04000111 RID: 273
			internal static readonly int flags;
		}

		// Token: 0x02000076 RID: 118
		private struct AlignOfHelper<T> where T : struct
		{
			// Token: 0x04000112 RID: 274
			public byte dummy;

			// Token: 0x04000113 RID: 275
			public T data;
		}
	}
}
