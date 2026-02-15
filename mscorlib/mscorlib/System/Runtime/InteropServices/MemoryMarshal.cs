using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000514 RID: 1300
	public static class MemoryMarshal
	{
		// Token: 0x060028E0 RID: 10464 RVA: 0x000A7A33 File Offset: 0x000A5C33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<byte> AsBytes<T>(Span<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new Span<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000A7A68 File Offset: 0x000A5C68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<byte> AsBytes<T>(ReadOnlySpan<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new ReadOnlySpan<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000A7A9D File Offset: 0x000A5C9D
		public unsafe static Memory<T> AsMemory<T>(ReadOnlyMemory<T> memory)
		{
			return *Unsafe.As<ReadOnlyMemory<T>, Memory<T>>(ref memory);
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000A7AAC File Offset: 0x000A5CAC
		public static ref T GetReference<T>(Span<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000A7AC8 File Offset: 0x000A5CC8
		public static ref T GetReference<T>(ReadOnlySpan<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000A7AE4 File Offset: 0x000A5CE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(Span<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000A7B10 File Offset: 0x000A5D10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(ReadOnlySpan<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000A7B3C File Offset: 0x000A5D3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<T> CreateReadOnlySpan<T>(ref T reference, int length)
		{
			return new ReadOnlySpan<T>(ref reference, length);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000A7B48 File Offset: 0x000A5D48
		public static bool TryGetArray<T>(ReadOnlyMemory<T> memory, out ArraySegment<T> segment)
		{
			int num;
			int num2;
			object objectStartLength = memory.GetObjectStartLength(out num, out num2);
			if (num < 0)
			{
				ArraySegment<T> arraySegment;
				if (((MemoryManager<T>)objectStartLength).TryGetArray(out arraySegment))
				{
					segment = new ArraySegment<T>(arraySegment.Array, arraySegment.Offset + (num & int.MaxValue), num2);
					return true;
				}
			}
			else
			{
				T[] array = objectStartLength as T[];
				if (array != null)
				{
					segment = new ArraySegment<T>(array, num, num2 & int.MaxValue);
					return true;
				}
			}
			if ((num2 & 2147483647) == 0)
			{
				segment = ArraySegment<T>.Empty;
				return true;
			}
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000A7BD9 File Offset: 0x000A5DD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Read<T>(ReadOnlySpan<byte> source) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > source.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
		}
	}
}
