using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x020002EF RID: 751
	internal class EncoderNLS : Encoder
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x0006452A File Offset: 0x0006272A
		internal EncoderNLS(Encoding encoding)
		{
			this._encoding = encoding;
			this._fallback = this._encoding.EncoderFallback;
			this.Reset();
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00064550 File Offset: 0x00062750
		public override void Reset()
		{
			this._charLeftOver = '\0';
			if (this._fallbackBuffer != null)
			{
				this._fallbackBuffer.Reset();
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0006456C File Offset: 0x0006276C
		public unsafe override int GetByteCount(char[] chars, int index, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "Array cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (chars.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			int byteCount;
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				byteCount = this.GetByteCount(ptr + index, count, flush);
			}
			return byteCount;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000645E8 File Offset: 0x000627E8
		public unsafe override int GetByteCount(char* chars, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetByteCount(chars, count, this);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0006463C File Offset: 0x0006283C
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = bytes.Length - byteIndex;
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* ptr2 = reference2;
					return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, flush);
				}
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00064700 File Offset: 0x00062900
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetBytes(chars, charCount, bytes, byteCount, this);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00064778 File Offset: 0x00062978
		public unsafe override void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", "Non-negative number required.");
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* ptr2 = reference2;
					this.Convert(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, flush, out charsUsed, out bytesUsed, out completed);
				}
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00064864 File Offset: 0x00062A64
		public unsafe override void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = false;
			this._charsUsed = 0;
			bytesUsed = this._encoding.GetBytes(chars, charCount, bytes, byteCount, this);
			charsUsed = this._charsUsed;
			completed = charsUsed == charCount && (!flush || !this.HasState) && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0);
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0006491F File Offset: 0x00062B1F
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00064927 File Offset: 0x00062B27
		public bool MustFlush
		{
			get
			{
				return this._mustFlush;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0006492F File Offset: 0x00062B2F
		internal virtual bool HasState
		{
			get
			{
				return this._charLeftOver > '\0';
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x0006493A File Offset: 0x00062B3A
		internal void ClearMustFlush()
		{
			this._mustFlush = false;
		}

		// Token: 0x04000C86 RID: 3206
		internal char _charLeftOver;

		// Token: 0x04000C87 RID: 3207
		private Encoding _encoding;

		// Token: 0x04000C88 RID: 3208
		private bool _mustFlush;

		// Token: 0x04000C89 RID: 3209
		internal bool _throwOnOverflow;

		// Token: 0x04000C8A RID: 3210
		internal int _charsUsed;
	}
}
