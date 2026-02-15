using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x020002E4 RID: 740
	internal class DecoderNLS : Decoder
	{
		// Token: 0x06001A37 RID: 6711 RVA: 0x00063519 File Offset: 0x00061719
		internal DecoderNLS(Encoding encoding)
		{
			this._encoding = encoding;
			this._fallback = this._encoding.DecoderFallback;
			this.Reset();
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0006353F File Offset: 0x0006173F
		public override void Reset()
		{
			DecoderFallbackBuffer fallbackBuffer = this._fallbackBuffer;
			if (fallbackBuffer == null)
			{
				return;
			}
			fallbackBuffer.Reset();
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00063551 File Offset: 0x00061751
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00063560 File Offset: 0x00061760
		public unsafe override int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "Array cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				return this.GetCharCount(ptr + index, count, flush);
			}
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000635D8 File Offset: 0x000617D8
		public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetCharCount(bytes, count, this);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0006362A File Offset: 0x0006182A
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0006363C File Offset: 0x0006183C
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", "Non-negative number required.");
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = chars.Length - charIndex;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr2 = reference2;
					return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, num, flush);
				}
			}
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00063700 File Offset: 0x00061900
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
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
			return this._encoding.GetChars(bytes, byteCount, chars, charCount, this);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00063778 File Offset: 0x00061978
		public unsafe override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", "Non-negative number required.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr2 = reference2;
					this.Convert(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
				}
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00063864 File Offset: 0x00061A64
		public unsafe override void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
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
			this._throwOnOverflow = false;
			this._bytesUsed = 0;
			charsUsed = this._encoding.GetChars(bytes, byteCount, chars, charCount, this);
			bytesUsed = this._bytesUsed;
			completed = bytesUsed == byteCount && (!flush || !this.HasState) && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0);
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0006391F File Offset: 0x00061B1F
		public bool MustFlush
		{
			get
			{
				return this._mustFlush;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00033991 File Offset: 0x00031B91
		internal virtual bool HasState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00063927 File Offset: 0x00061B27
		internal void ClearMustFlush()
		{
			this._mustFlush = false;
		}

		// Token: 0x04000C68 RID: 3176
		private Encoding _encoding;

		// Token: 0x04000C69 RID: 3177
		private bool _mustFlush;

		// Token: 0x04000C6A RID: 3178
		internal bool _throwOnOverflow;

		// Token: 0x04000C6B RID: 3179
		internal int _bytesUsed;
	}
}
