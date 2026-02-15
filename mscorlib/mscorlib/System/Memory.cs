using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200011D RID: 285
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	public readonly struct Memory<T> : IEquatable<Memory<T>>
	{
		// Token: 0x06000978 RID: 2424 RVA: 0x000289A0 File Offset: 0x00026BA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array)
		{
			if (array == null)
			{
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000289FC File Offset: 0x00026BFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
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
			this._object = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00028A73 File Offset: 0x00026C73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00028A8A File Offset: 0x00026C8A
		public static implicit operator Memory<T>(T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00028A92 File Offset: 0x00026C92
		public static implicit operator Memory<T>(ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00028AAE File Offset: 0x00026CAE
		public unsafe static implicit operator ReadOnlyMemory<T>(Memory<T> memory)
		{
			return *Unsafe.As<Memory<T>, ReadOnlyMemory<T>>(ref memory);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00028ABC File Offset: 0x00026CBC
		public static Memory<T> Empty
		{
			get
			{
				return default(Memory<T>);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00028AD2 File Offset: 0x00026CD2
		public int Length
		{
			get
			{
				return this._length & int.MaxValue;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00028AE0 File Offset: 0x00026CE0
		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format("System.Memory<{0}>[{1}]", typeof(T).Name, this._length & int.MaxValue);
			}
			string text = this._object as string;
			if (text == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length & int.MaxValue);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00028B70 File Offset: 0x00026D70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start)
		{
			int length = this._length;
			int num = length & int.MaxValue;
			if (start > num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(this._object, this._index + start, length - start);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00028BB0 File Offset: 0x00026DB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start, int length)
		{
			int length2 = this._length;
			int num = length2 & int.MaxValue;
			if (start > num || length > num - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(this._object, this._index + start, length | (length2 & int.MinValue));
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00028BF8 File Offset: 0x00026DF8
		public Span<T> Span
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._index < 0)
				{
					return ((MemoryManager<T>)this._object).GetSpan().Slice(this._index & int.MaxValue, this._length);
				}
				if (typeof(T) == typeof(char))
				{
					string text = this._object as string;
					if (text != null)
					{
						return new Span<T>(Unsafe.As<char, T>(text.GetRawStringData()), text.Length).Slice(this._index, this._length);
					}
				}
				if (this._object != null)
				{
					return new Span<T>((T[])this._object, this._index, this._length & int.MaxValue);
				}
				return default(Span<T>);
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00028CC4 File Offset: 0x00026EC4
		public void CopyTo(Memory<T> destination)
		{
			this.Span.CopyTo(destination.Span);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00028CE8 File Offset: 0x00026EE8
		public MemoryHandle Pin()
		{
			if (this._index < 0)
			{
				return ((MemoryManager<T>)this._object).Pin(this._index & int.MaxValue);
			}
			if (typeof(T) == typeof(char))
			{
				string text = this._object as string;
				if (text != null)
				{
					GCHandle gchandle = GCHandle.Alloc(text, GCHandleType.Pinned);
					return new MemoryHandle(Unsafe.Add<T>(Unsafe.AsPointer<char>(text.GetRawStringData()), this._index), gchandle, null);
				}
			}
			T[] array = this._object as T[];
			if (array == null)
			{
				return default(MemoryHandle);
			}
			if (this._length < 0)
			{
				return new MemoryHandle(Unsafe.Add<T>(Unsafe.AsPointer<byte>(array.GetRawSzArrayData()), this._index), default(GCHandle), null);
			}
			GCHandle gchandle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			return new MemoryHandle(Unsafe.Add<T>(Unsafe.AsPointer<byte>(array.GetRawSzArrayData()), this._index), gchandle2, null);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00028DDC File Offset: 0x00026FDC
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00028DF8 File Offset: 0x00026FF8
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				return ((ReadOnlyMemory<T>)obj).Equals(this);
			}
			if (obj is Memory<T>)
			{
				Memory<T> memory = (Memory<T>)obj;
				return this.Equals(memory);
			}
			return false;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00028E3F File Offset: 0x0002703F
		public bool Equals(Memory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00028E6D File Offset: 0x0002706D
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return Memory<T>.CombineHashCodes(this._object.GetHashCode(), this._index.GetHashCode(), this._length.GetHashCode());
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00028E9F File Offset: 0x0002709F
		private static int CombineHashCodes(int left, int right)
		{
			return ((left << 5) + left) ^ right;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00028EA8 File Offset: 0x000270A8
		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Memory<T>.CombineHashCodes(Memory<T>.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x0400044A RID: 1098
		private readonly object _object;

		// Token: 0x0400044B RID: 1099
		private readonly int _index;

		// Token: 0x0400044C RID: 1100
		private readonly int _length;
	}
}
