using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200013A RID: 314
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	public readonly struct ReadOnlyMemory<T> : IEquatable<ReadOnlyMemory<T>>
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x0002F688 File Offset: 0x0002D888
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory(T[] array)
		{
			if (array == null)
			{
				this = default(ReadOnlyMemory<T>);
				return;
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002F6AC File Offset: 0x0002D8AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(ReadOnlyMemory<T>);
				return;
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002F6EC File Offset: 0x0002D8EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlyMemory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002F703 File Offset: 0x0002D903
		public static implicit operator ReadOnlyMemory<T>(ArraySegment<T> segment)
		{
			return new ReadOnlyMemory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0002F71F File Offset: 0x0002D91F
		public int Length
		{
			get
			{
				return this._length & int.MaxValue;
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002F730 File Offset: 0x0002D930
		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format("System.ReadOnlyMemory<{0}>[{1}]", typeof(T).Name, this._length & int.MaxValue);
			}
			string text = this._object as string;
			if (text == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length & int.MaxValue);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002F7C0 File Offset: 0x0002D9C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory<T> Slice(int start)
		{
			int length = this._length;
			int num = length & int.MaxValue;
			if (start > num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<T>(this._object, this._index + start, length - start);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002F800 File Offset: 0x0002DA00
		public ReadOnlySpan<T> Span
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
						return new ReadOnlySpan<T>(Unsafe.As<char, T>(text.GetRawStringData()), text.Length).Slice(this._index, this._length);
					}
				}
				if (this._object != null)
				{
					return new ReadOnlySpan<T>((T[])this._object, this._index, this._length & int.MaxValue);
				}
				return default(ReadOnlySpan<T>);
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002F8D0 File Offset: 0x0002DAD0
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				ReadOnlyMemory<T> readOnlyMemory = (ReadOnlyMemory<T>)obj;
				return this.Equals(readOnlyMemory);
			}
			if (obj is Memory<T>)
			{
				Memory<T> memory = (Memory<T>)obj;
				return this.Equals(memory);
			}
			return false;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002F911 File Offset: 0x0002DB11
		public bool Equals(ReadOnlyMemory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002F93F File Offset: 0x0002DB3F
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return ReadOnlyMemory<T>.CombineHashCodes(this._object.GetHashCode(), this._index.GetHashCode(), this._length.GetHashCode());
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00028E9F File Offset: 0x0002709F
		private static int CombineHashCodes(int left, int right)
		{
			return ((left << 5) + left) ^ right;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002F971 File Offset: 0x0002DB71
		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return ReadOnlyMemory<T>.CombineHashCodes(ReadOnlyMemory<T>.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002F980 File Offset: 0x0002DB80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal object GetObjectStartLength(out int start, out int length)
		{
			start = this._index;
			length = this._length;
			return this._object;
		}

		// Token: 0x04000477 RID: 1143
		private readonly object _object;

		// Token: 0x04000478 RID: 1144
		private readonly int _index;

		// Token: 0x04000479 RID: 1145
		private readonly int _length;
	}
}
