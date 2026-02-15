using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200011F RID: 287
	public static class MemoryExtensions
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x00028EB7 File Offset: 0x000270B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool EqualsOrdinal(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length == value.Length && (value.Length == 0 || span.SequenceEqual(value));
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00028EDD File Offset: 0x000270DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool EqualsOrdinalIgnoreCase(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length == value.Length && (value.Length == 0 || CompareInfo.CompareOrdinalIgnoreCase(span, value) == 0);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00028F08 File Offset: 0x00027108
		internal unsafe static bool Contains(this ReadOnlySpan<char> source, char value)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (*source[i] == (ushort)value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00028F38 File Offset: 0x00027138
		public static int ToUpperInvariant(this ReadOnlySpan<char> source, Span<char> destination)
		{
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				CultureInfo.InvariantCulture.TextInfo.ToUpperAsciiInvariant(source, destination);
			}
			else
			{
				CultureInfo.InvariantCulture.TextInfo.ChangeCase(source, destination, true);
			}
			return source.Length;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00028F8C File Offset: 0x0002718C
		public static bool EndsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			if (value.Length == 0)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return SpanHelpers.EndsWithCultureHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.CurrentCultureIgnoreCase:
				return SpanHelpers.EndsWithCultureIgnoreCaseHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.InvariantCulture:
				return SpanHelpers.EndsWithCultureHelper(span, value, CompareInfo.Invariant);
			case StringComparison.InvariantCultureIgnoreCase:
				return SpanHelpers.EndsWithCultureIgnoreCaseHelper(span, value, CompareInfo.Invariant);
			case StringComparison.Ordinal:
				return span.EndsWith(value);
			case StringComparison.OrdinalIgnoreCase:
				return SpanHelpers.EndsWithOrdinalIgnoreCaseHelper(span, value);
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00029028 File Offset: 0x00027228
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), start), array.Length - start);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002909C File Offset: 0x0002729C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text)
		{
			if (text == null)
			{
				return default(ReadOnlySpan<char>);
			}
			return new ReadOnlySpan<char>(text.GetRawStringData(), text.Length);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000290C8 File Offset: 0x000272C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text, int start)
		{
			if (text == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if (start > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.Add<char>(text.GetRawStringData(), start), text.Length - start);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00029118 File Offset: 0x00027318
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text, int start, int length)
		{
			if (text == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if (start > text.Length || length > text.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.Add<char>(text.GetRawStringData(), start), length);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002916C File Offset: 0x0002736C
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span)
		{
			return span.TrimStart().TrimEnd();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002917C File Offset: 0x0002737C
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span)
		{
			int num = 0;
			while (num < span.Length && char.IsWhiteSpace((char)(*span[num])))
			{
				num++;
			}
			return span.Slice(num);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000291B4 File Offset: 0x000273B4
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span)
		{
			int num = span.Length - 1;
			while (num >= 0 && char.IsWhiteSpace((char)(*span[num])))
			{
				num--;
			}
			return span.Slice(0, num + 1);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000291F0 File Offset: 0x000273F0
		public unsafe static bool IsWhiteSpace(this ReadOnlySpan<char> span)
		{
			for (int i = 0; i < span.Length; i++)
			{
				if (!char.IsWhiteSpace((char)(*span[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00029224 File Offset: 0x00027424
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<T>(this Span<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000292BC File Offset: 0x000274BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00029330 File Offset: 0x00027530
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000293C8 File Offset: 0x000275C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002943C File Offset: 0x0002763C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>
		{
			int length = span.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length == other.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), (ulong)((long)length * (long)num));
			}
			return length == other.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other), length);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000294B4 File Offset: 0x000276B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length <= span.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length * (long)num));
			}
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(value), length);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002952C File Offset: 0x0002772C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWith<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = span.Length;
			int length2 = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length2 <= length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length2 * (long)num));
			}
			return length2 <= length && SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2), MemoryMarshal.GetReference<T>(value), length2);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000295B0 File Offset: 0x000277B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000295B8 File Offset: 0x000277B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, int start, int length)
		{
			return new Span<T>(array, start, length);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000295C2 File Offset: 0x000277C2
		public static Memory<T> AsMemory<T>(this T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000295CC File Offset: 0x000277CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this T[] source, Span<T> destination)
		{
			new ReadOnlySpan<T>(source).CopyTo(destination);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000295E8 File Offset: 0x000277E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsTypeComparableAsBytes<T>(out ulong size)
		{
			if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte))
			{
				size = 1UL;
				return true;
			}
			if (typeof(T) == typeof(char) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
			{
				size = 2UL;
				return true;
			}
			if (typeof(T) == typeof(int) || typeof(T) == typeof(uint))
			{
				size = 4UL;
				return true;
			}
			if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
			{
				size = 8UL;
				return true;
			}
			size = 0UL;
			return false;
		}
	}
}
