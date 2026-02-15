using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x0200013B RID: 315
	[NonVersionable]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	public readonly ref struct ReadOnlySpan<T>
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x0002F998 File Offset: 0x0002DB98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array)
		{
			if (array == null)
			{
				this = default(ReadOnlySpan<T>);
				return;
			}
			this._pointer = new ByReference<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()));
			this._length = array.Length;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002F9C4 File Offset: 0x0002DBC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(ReadOnlySpan<T>);
				return;
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), start));
			this._length = length;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002FA1D File Offset: 0x0002DC1D
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ReadOnlySpan(void* pointer, int length)
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

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002FA56 File Offset: 0x0002DC56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlySpan(ref T ptr, int length)
		{
			this._pointer = new ByReference<T>(ref ptr);
			this._length = length;
		}

		// Token: 0x170000B6 RID: 182
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

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002FA9C File Offset: 0x0002DC9C
		public readonly ref T GetPinnableReference()
		{
			if (this._length == 0)
			{
				return Unsafe.AsRef<T>(null);
			}
			return this._pointer.Value;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002FAC8 File Offset: 0x0002DCC8
		public void CopyTo(Span<T> destination)
		{
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (ulong)((long)this._length));
				return;
			}
			ThrowHelper.ThrowArgumentException_DestinationTooShort();
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002FB14 File Offset: 0x0002DD14
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

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002FB60 File Offset: 0x0002DD60
		public static bool operator ==(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left._pointer.Value, right._pointer.Value);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002FBA0 File Offset: 0x0002DDA0
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				fixed (char* ptr = Unsafe.As<T, char>(this._pointer.Value))
				{
					return new string(ptr, 0, this._length);
				}
			}
			return string.Format("System.ReadOnlySpan<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002FC10 File Offset: 0x0002DE10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<T>(Unsafe.Add<T>(this._pointer.Value, start), this._length - start);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002FC4C File Offset: 0x0002DE4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<T>(Unsafe.Add<T>(this._pointer.Value, start), length);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002FC8C File Offset: 0x0002DE8C
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0002FCD7 File Offset: 0x0002DED7
		public int Length
		{
			[NonVersionable]
			get
			{
				return this._length;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002FCDF File Offset: 0x0002DEDF
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002FCEA File Offset: 0x0002DEEA
		[Obsolete("Equals() on ReadOnlySpan will always throw an exception. Use == instead.")]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException("Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.");
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002FCF6 File Offset: 0x0002DEF6
		[Obsolete("GetHashCode() on ReadOnlySpan will always throw an exception.")]
		public override int GetHashCode()
		{
			throw new NotSupportedException("GetHashCode() on Span and ReadOnlySpan is not supported.");
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002FD02 File Offset: 0x0002DF02
		public static implicit operator ReadOnlySpan<T>(T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0002FD0C File Offset: 0x0002DF0C
		public static ReadOnlySpan<T> Empty
		{
			get
			{
				return default(ReadOnlySpan<T>);
			}
		}

		// Token: 0x0400047A RID: 1146
		internal readonly ByReference<T> _pointer;

		// Token: 0x0400047B RID: 1147
		private readonly int _length;
	}
}
