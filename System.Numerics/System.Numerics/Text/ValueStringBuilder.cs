using System;
using System.Buffers;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200000D RID: 13
	[DefaultMember("Item")]
	internal ref struct ValueStringBuilder
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00006CA2 File Offset: 0x00004EA2
		public ValueStringBuilder(Span<char> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._chars = initialBuffer;
			this._pos = 0;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00006CB9 File Offset: 0x00004EB9
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006CC1 File Offset: 0x00004EC1
		public override string ToString()
		{
			string text = new string(this._chars.Slice(0, this._pos));
			this.Dispose();
			return text;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006CE8 File Offset: 0x00004EE8
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

		// Token: 0x06000092 RID: 146 RVA: 0x00006D2C File Offset: 0x00004F2C
		public void Insert(int index, char value, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			int num = this._pos - index;
			this._chars.Slice(index, num).CopyTo(this._chars.Slice(index + count));
			this._chars.Slice(index, count).Fill(value);
			this._pos += count;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006DA8 File Offset: 0x00004FA8
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

		// Token: 0x06000094 RID: 148 RVA: 0x00006DEC File Offset: 0x00004FEC
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

		// Token: 0x06000095 RID: 149 RVA: 0x00006E3C File Offset: 0x0000503C
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

		// Token: 0x06000096 RID: 150 RVA: 0x00006EA0 File Offset: 0x000050A0
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

		// Token: 0x06000097 RID: 151 RVA: 0x00006F08 File Offset: 0x00005108
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

		// Token: 0x06000098 RID: 152 RVA: 0x00006F74 File Offset: 0x00005174
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

		// Token: 0x06000099 RID: 153 RVA: 0x00006FB5 File Offset: 0x000051B5
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GrowAndAppend(char c)
		{
			this.Grow(1);
			this.Append(c);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006FC8 File Offset: 0x000051C8
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

		// Token: 0x0600009B RID: 155 RVA: 0x00007038 File Offset: 0x00005238
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

		// Token: 0x04000020 RID: 32
		private char[] _arrayToReturnToPool;

		// Token: 0x04000021 RID: 33
		private Span<char> _chars;

		// Token: 0x04000022 RID: 34
		private int _pos;
	}
}
