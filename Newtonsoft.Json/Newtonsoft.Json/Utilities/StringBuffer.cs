using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000ED RID: 237
	[NullableContext(2)]
	[Nullable(0)]
	internal struct StringBuffer
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00023907 File Offset: 0x00021B07
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0002390F File Offset: 0x00021B0F
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00023918 File Offset: 0x00021B18
		public bool IsEmpty
		{
			get
			{
				return this._buffer == null;
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00023923 File Offset: 0x00021B23
		public StringBuffer(IArrayPool<char> bufferPool, int initalSize)
		{
			this = new StringBuffer(BufferUtils.RentBuffer(bufferPool, initalSize));
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00023932 File Offset: 0x00021B32
		[NullableContext(1)]
		private StringBuffer(char[] buffer)
		{
			this._buffer = buffer;
			this._position = 0;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00023944 File Offset: 0x00021B44
		public void Append(IArrayPool<char> bufferPool, char value)
		{
			if (this._position == this._buffer.Length)
			{
				this.EnsureSize(bufferPool, 1);
			}
			char[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00023984 File Offset: 0x00021B84
		[NullableContext(1)]
		public void Append([Nullable(2)] IArrayPool<char> bufferPool, char[] buffer, int startIndex, int count)
		{
			if (this._position + count >= this._buffer.Length)
			{
				this.EnsureSize(bufferPool, count);
			}
			Array.Copy(buffer, startIndex, this._buffer, this._position, count);
			this._position += count;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000239D1 File Offset: 0x00021BD1
		public void Clear(IArrayPool<char> bufferPool)
		{
			if (this._buffer != null)
			{
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
				this._buffer = null;
			}
			this._position = 0;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000239F8 File Offset: 0x00021BF8
		private void EnsureSize(IArrayPool<char> bufferPool, int appendLength)
		{
			char[] array = BufferUtils.RentBuffer(bufferPool, (this._position + appendLength) * 2);
			if (this._buffer != null)
			{
				Array.Copy(this._buffer, array, this._position);
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
			}
			this._buffer = array;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00023A43 File Offset: 0x00021C43
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00023A52 File Offset: 0x00021C52
		[NullableContext(1)]
		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00023A61 File Offset: 0x00021C61
		public char[] InternalBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x040004C5 RID: 1221
		private char[] _buffer;

		// Token: 0x040004C6 RID: 1222
		private int _position;
	}
}
