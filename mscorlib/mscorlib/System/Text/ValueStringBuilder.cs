using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000305 RID: 773
	internal ref struct ValueStringBuilder
	{
		// Token: 0x06001BAF RID: 7087 RVA: 0x0006C7C6 File Offset: 0x0006A9C6
		public ValueStringBuilder(Span<char> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._chars = initialBuffer;
			this._pos = 0;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0006C7DD File Offset: 0x0006A9DD
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x170002F7 RID: 759
		public ref char this[int index]
		{
			get
			{
				return this._chars[index];
			}
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0006C7F3 File Offset: 0x0006A9F3
		public override string ToString()
		{
			string text = new string(this._chars.Slice(0, this._pos));
			this.Dispose();
			return text;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0006C818 File Offset: 0x0006AA18
		public bool TryCopyTo(Span<char> destination, out int charsWritten)
		{
			if (this._chars.Slice(0, this._pos).TryCopyTo(destination))
			{
				charsWritten = this._pos;
				this.Dispose();
				return true;
			}
			charsWritten = 0;
			this.Dispose();
			return false;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0006C85C File Offset: 0x0006AA5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(char c)
		{
			int pos = this._pos;
			if (pos < this._chars.Length)
			{
				*this._chars[pos] = c;
				this._pos = pos + 1;
				return;
			}
			this.GrowAndAppend(c);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0006C8A0 File Offset: 0x0006AAA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(string s)
		{
			int pos = this._pos;
			if (s.Length == 1 && pos < this._chars.Length)
			{
				*this._chars[pos] = s[0];
				this._pos = pos + 1;
				return;
			}
			this.AppendSlow(s);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0006C8F0 File Offset: 0x0006AAF0
		private void AppendSlow(string s)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - s.Length)
			{
				this.Grow(s.Length);
			}
			s.AsSpan().CopyTo(this._chars.Slice(pos));
			this._pos += s.Length;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0006C954 File Offset: 0x0006AB54
		public unsafe void Append(char c, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			Span<char> span = this._chars.Slice(this._pos, count);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = c;
			}
			this._pos += count;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0006C9BC File Offset: 0x0006ABBC
		public unsafe void Append(char* value, int length)
		{
			if (this._pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			Span<char> span = this._chars.Slice(this._pos, length);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = *(value++);
			}
			this._pos += length;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0006CA28 File Offset: 0x0006AC28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AppendSpan(int length)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			this._pos = pos + length;
			return this._chars.Slice(pos, length);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0006CA69 File Offset: 0x0006AC69
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GrowAndAppend(char c)
		{
			this.Grow(1);
			this.Append(c);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0006CA7C File Offset: 0x0006AC7C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int requiredAdditionalCapacity)
		{
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(this._pos + requiredAdditionalCapacity, this._chars.Length * 2));
			this._chars.CopyTo(array);
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this._chars = (this._arrayToReturnToPool = array);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0006CAEC File Offset: 0x0006ACEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this = default(ValueStringBuilder);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x04000CC4 RID: 3268
		private char[] _arrayToReturnToPool;

		// Token: 0x04000CC5 RID: 3269
		private Span<char> _chars;

		// Token: 0x04000CC6 RID: 3270
		private int _pos;
	}
}
