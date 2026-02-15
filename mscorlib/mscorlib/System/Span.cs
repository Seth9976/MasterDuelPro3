using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000141 RID: 321
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[NonVersionable]
	public readonly ref struct Span<T>
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x000303C8 File Offset: 0x0002E5C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array)
		{
			if (array == null)
			{
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._pointer = new ByReference<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()));
			this._length = array.Length;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0003042C File Offset: 0x0002E62C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), start));
			this._length = length;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000304B1 File Offset: 0x0002E6B1
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Span(void* pointer, int length)
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.As<byte, T>(ref *(byte*)pointer));
			this._length = length;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000304EA File Offset: 0x0002E6EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Span(ref T ptr, int length)
		{
			this._pointer = new ByReference<T>(ref ptr);
			this._length = length;
		}

		// Token: 0x170000BB RID: 187
		public ref T this[int index]
		{
			[NonVersionable]
			[Intrinsic]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (index >= this._length)
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				return Unsafe.Add<T>(this._pointer.Value, index);
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00030530 File Offset: 0x0002E730
		public ref T GetPinnableReference()
		{
			if (this._length == 0)
			{
				return Unsafe.AsRef<T>(null);
			}
			return this._pointer.Value;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0003055C File Offset: 0x0002E75C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				SpanHelpers.ClearWithReferences(Unsafe.As<T, IntPtr>(this._pointer.Value), (ulong)((long)this._length * (long)(Unsafe.SizeOf<T>() / IntPtr.Size)));
				return;
			}
			SpanHelpers.ClearWithoutReferences(Unsafe.As<T, byte>(this._pointer.Value), (ulong)((long)this._length * (long)Unsafe.SizeOf<T>()));
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000305C4 File Offset: 0x0002E7C4
		public unsafe void Fill(T value)
		{
			if (Unsafe.SizeOf<T>() == 1)
			{
				uint length = (uint)this._length;
				if (length == 0U)
				{
					return;
				}
				T t = value;
				Unsafe.InitBlockUnaligned(Unsafe.As<T, byte>(this._pointer.Value), *Unsafe.As<T, byte>(ref t), length);
				return;
			}
			else
			{
				ulong num = (ulong)this._length;
				if (num == 0UL)
				{
					return;
				}
				ref T value2 = ref this._pointer.Value;
				ulong num2 = (ulong)Unsafe.SizeOf<T>();
				ulong num3;
				for (num3 = 0UL; num3 < (num & 18446744073709551608UL); num3 += 8UL)
				{
					*Unsafe.AddByteOffset<T>(ref value2, num3 * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 1UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 2UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 3UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 4UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 5UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 6UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 7UL) * num2) = value;
				}
				if (num3 < (num & 18446744073709551612UL))
				{
					*Unsafe.AddByteOffset<T>(ref value2, num3 * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 1UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 2UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 3UL) * num2) = value;
					num3 += 4UL;
				}
				while (num3 < num)
				{
					*Unsafe.AddByteOffset<T>(ref value2, num3 * num2) = value;
					num3 += 1UL;
				}
				return;
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00030770 File Offset: 0x0002E970
		public void CopyTo(Span<T> destination)
		{
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (ulong)((long)this._length));
				return;
			}
			ThrowHelper.ThrowArgumentException_DestinationTooShort();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000307BC File Offset: 0x0002E9BC
		public bool TryCopyTo(Span<T> destination)
		{
			bool flag = false;
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (ulong)((long)this._length));
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00030808 File Offset: 0x0002EA08
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._pointer.Value, span._length);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00030830 File Offset: 0x0002EA30
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				fixed (char* ptr = Unsafe.As<T, char>(this._pointer.Value))
				{
					return new string(ptr, 0, this._length);
				}
			}
			return string.Format("System.Span<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000308A0 File Offset: 0x0002EAA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(this._pointer.Value, start), this._length - start);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000308DC File Offset: 0x0002EADC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(this._pointer.Value, start), length);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0003091C File Offset: 0x0002EB1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T[] ToArray()
		{
			if (this._length == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._length];
			Buffer.Memmove<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), this._pointer.Value, (ulong)((long)this._length));
			return array;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00030967 File Offset: 0x0002EB67
		public int Length
		{
			[NonVersionable]
			get
			{
				return this._length;
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002FCEA File Offset: 0x0002DEEA
		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException("Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.");
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002FCF6 File Offset: 0x0002DEF6
		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		public override int GetHashCode()
		{
			throw new NotSupportedException("GetHashCode() on Span and ReadOnlySpan is not supported.");
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0003096F File Offset: 0x0002EB6F
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x04000480 RID: 1152
		internal readonly ByReference<T> _pointer;

		// Token: 0x04000481 RID: 1153
		private readonly int _length;
	}
}
