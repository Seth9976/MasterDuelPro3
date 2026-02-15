using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	/// <summary>Represents a UTF-32 encoding of Unicode characters.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020002F8 RID: 760
	[Serializable]
	public sealed class UTF32Encoding : Encoding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.UTF32Encoding" /> class.</summary>
		// Token: 0x06001B19 RID: 6937 RVA: 0x0006741B File Offset: 0x0006561B
		public UTF32Encoding()
			: this(false, true, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.UTF32Encoding" /> class. Parameters specify whether to use the big endian byte order and whether to provide a Unicode byte order mark.</summary>
		/// <param name="bigEndian">true to use the big endian byte order (most significant byte first), or false to use the little endian byte order (least significant byte first). </param>
		/// <param name="byteOrderMark">true to specify that a Unicode byte order mark is provided; otherwise, false. </param>
		// Token: 0x06001B1A RID: 6938 RVA: 0x00067426 File Offset: 0x00065626
		public UTF32Encoding(bool bigEndian, bool byteOrderMark)
			: this(bigEndian, byteOrderMark, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.UTF32Encoding" /> class. Parameters specify whether to use the big endian byte order, whether to provide a Unicode byte order mark, and whether to throw an exception when an invalid encoding is detected.</summary>
		/// <param name="bigEndian">true to use the big endian byte order (most significant byte first), or false to use the little endian byte order (least significant byte first). </param>
		/// <param name="byteOrderMark">true to specify that a Unicode byte order mark is provided; otherwise, false. </param>
		/// <param name="throwOnInvalidCharacters">true to specify that an exception should be thrown when an invalid encoding is detected; otherwise, false. </param>
		// Token: 0x06001B1B RID: 6939 RVA: 0x00067431 File Offset: 0x00065631
		public UTF32Encoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidCharacters)
			: base(bigEndian ? 12001 : 12000)
		{
			this._bigEndian = bigEndian;
			this._emitUTF32ByteOrderMark = byteOrderMark;
			this._isThrowException = throwOnInvalidCharacters;
			if (this._isThrowException)
			{
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0006746C File Offset: 0x0006566C
		internal override void SetDefaultFallbacks()
		{
			if (this._isThrowException)
			{
				this.encoderFallback = EncoderFallback.ExceptionFallback;
				this.decoderFallback = DecoderFallback.ExceptionFallback;
				return;
			}
			this.encoderFallback = new EncoderReplacementFallback("\ufffd");
			this.decoderFallback = new DecoderReplacementFallback("\ufffd");
		}

		/// <summary>Calculates the number of bytes produced by encoding a set of characters from the specified character array.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="index">The index of the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />.-or- The resulting number of bytes is greater than the maximum number that can be returned as an integer. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="chars" /> contains an invalid sequence of characters. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B1D RID: 6941 RVA: 0x000674B8 File Offset: 0x000656B8
		public unsafe override int GetByteCount(char[] chars, int index, int count)
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
			if (count == 0)
			{
				return 0;
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			return this.GetByteCount(ptr + index, count, null);
		}

		/// <summary>Calculates the number of bytes produced by encoding the characters in the specified <see cref="T:System.String" />.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="s">The <see cref="T:System.String" /> containing the set of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting number of bytes is greater than the maximum number that can be returned as an integer. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="s" /> contains an invalid sequence of characters. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B1E RID: 6942 RVA: 0x00067540 File Offset: 0x00065740
		public unsafe override int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.GetByteCount(ptr, s.Length, null);
		}

		/// <summary>Calculates the number of bytes produced by encoding a set of characters starting at the specified character pointer.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">A pointer to the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.-or- The resulting number of bytes is greater than the maximum number that can be returned as an integer. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="chars" /> contains an invalid sequence of characters. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B1F RID: 6943 RVA: 0x000620AD File Offset: 0x000602AD
		[CLSCompliant(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			return this.GetByteCount(chars, count, null);
		}

		/// <summary>Encodes a set of characters from the specified <see cref="T:System.String" /> into the specified byte array.</summary>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <param name="s">The <see cref="T:System.String" /> containing the set of characters to encode. </param>
		/// <param name="charIndex">The index of the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes. </param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="s" /> contains an invalid sequence of characters.-or- <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B20 RID: 6944 RVA: 0x0006757C File Offset: 0x0006577C
		public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null || bytes == null)
			{
				throw new ArgumentNullException((s == null) ? "s" : "bytes", "Array cannot be null.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (s.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("s", "Index and count must refer to a location within the string.");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = bytes.Length - byteIndex;
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr2 = reference;
				return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, null);
			}
		}

		/// <summary>Encodes a set of characters from the specified character array into the specified byte array.</summary>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="charIndex">The index of the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes. </param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="chars" /> contains an invalid sequence of characters.-or- <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B21 RID: 6945 RVA: 0x00067644 File Offset: 0x00065844
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
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
			if (charCount == 0)
			{
				return 0;
			}
			int num = bytes.Length - byteIndex;
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr2 = reference;
				return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, null);
			}
		}

		/// <summary>Encodes a set of characters starting at the specified character pointer into a sequence of bytes that are stored starting at the specified byte pointer.</summary>
		/// <returns>The actual number of bytes written at the location indicated by the <paramref name="bytes" /> parameter.</returns>
		/// <param name="chars">A pointer to the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">A pointer to the location at which to start writing the resulting sequence of bytes. </param>
		/// <param name="byteCount">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="chars" /> contains an invalid sequence of characters.-or- <paramref name="byteCount" /> is less than the resulting number of bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B22 RID: 6946 RVA: 0x00067714 File Offset: 0x00065914
		[CLSCompliant(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", "Non-negative number required.");
			}
			return this.GetBytes(chars, charCount, bytes, byteCount, null);
		}

		/// <summary>Calculates the number of characters produced by decoding a sequence of bytes from the specified byte array.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />.-or- The resulting number of bytes is greater than the maximum number that can be returned as an integer. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B23 RID: 6947 RVA: 0x00067778 File Offset: 0x00065978
		public unsafe override int GetCharCount(byte[] bytes, int index, int count)
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
			if (count == 0)
			{
				return 0;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return this.GetCharCount(ptr + index, count, null);
		}

		/// <summary>Calculates the number of characters produced by decoding a sequence of bytes starting at the specified byte pointer.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">A pointer to the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.-or- The resulting number of bytes is greater than the maximum number that can be returned as an integer. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B24 RID: 6948 RVA: 0x00062363 File Offset: 0x00060563
		[CLSCompliant(false)]
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			return this.GetCharCount(bytes, count, null);
		}

		/// <summary>Decodes a sequence of bytes from the specified byte array into the specified character array.</summary>
		/// <returns>The actual number of characters written into <paramref name="chars" />.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="byteIndex">The index of the first byte to decode. </param>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <param name="chars">The character array to contain the resulting set of characters. </param>
		/// <param name="charIndex">The index at which to start writing the resulting set of characters. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null.-or- <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteIndex" /> or <paramref name="byteCount" /> or <paramref name="charIndex" /> is less than zero.-or- <paramref name="byteindex" /> and <paramref name="byteCount" /> do not denote a valid range in <paramref name="bytes" />.-or- <paramref name="charIndex" /> is not a valid index in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes.-or- <paramref name="chars" /> does not have enough capacity from <paramref name="charIndex" /> to the end of the array to accommodate the resulting characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B25 RID: 6949 RVA: 0x000677FC File Offset: 0x000659FC
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
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
			if (byteCount == 0)
			{
				return 0;
			}
			int num = chars.Length - charIndex;
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr2 = reference;
				return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, num, null);
			}
		}

		/// <summary>Decodes a sequence of bytes starting at the specified byte pointer into a set of characters that are stored starting at the specified character pointer.</summary>
		/// <returns>The actual number of characters written at the location indicated by <paramref name="chars" />.</returns>
		/// <param name="bytes">A pointer to the first byte to decode. </param>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <param name="chars">A pointer to the location at which to start writing the resulting set of characters. </param>
		/// <param name="charCount">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null.-or- <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> or <paramref name="charCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes.-or- <paramref name="charCount" /> is less than the resulting number of characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B26 RID: 6950 RVA: 0x000678CC File Offset: 0x00065ACC
		[CLSCompliant(false)]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", "Non-negative number required.");
			}
			return this.GetChars(bytes, byteCount, chars, charCount, null);
		}

		/// <summary>Decodes a range of bytes from a byte array into a string.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">Error detection is enabled, and <paramref name="bytes" /> contains an invalid sequence of bytes. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B27 RID: 6951 RVA: 0x00067930 File Offset: 0x00065B30
		public unsafe override string GetString(byte[] bytes, int index, int count)
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
			if (count == 0)
			{
				return string.Empty;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return string.CreateStringFromEncoding(ptr + index, count, this);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000679B8 File Offset: 0x00065BB8
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			char* ptr = chars + count;
			char* ptr2 = chars;
			int num = 0;
			char c = '\0';
			EncoderFallbackBuffer encoderFallbackBuffer;
			if (encoder != null)
			{
				c = encoder._charLeftOver;
				encoderFallbackBuffer = encoder.FallbackBuffer;
				if (encoderFallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException(SR.Format("Must complete Convert() operation or call Encoder.Reset() before calling GetBytes() or GetByteCount(). Encoder '{0}' fallback '{1}'.", this.EncodingName, encoder.Fallback.GetType()));
				}
			}
			else
			{
				encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
			}
			encoderFallbackBuffer.InternalInitialize(ptr2, ptr, encoder, false);
			for (;;)
			{
				char c2;
				if ((c2 = encoderFallbackBuffer.InternalGetNextChar()) == '\0' && chars >= ptr)
				{
					if ((encoder != null && !encoder.MustFlush) || c <= '\0')
					{
						break;
					}
					char* ptr3 = chars;
					encoderFallbackBuffer.InternalFallback(c, ref ptr3);
					chars = ptr3;
					c = '\0';
				}
				else
				{
					if (c2 == '\0')
					{
						c2 = *chars;
						chars++;
					}
					if (c != '\0')
					{
						if (char.IsLowSurrogate(c2))
						{
							c = '\0';
							num += 4;
						}
						else
						{
							chars--;
							char* ptr3 = chars;
							encoderFallbackBuffer.InternalFallback(c, ref ptr3);
							chars = ptr3;
							c = '\0';
						}
					}
					else if (char.IsHighSurrogate(c2))
					{
						c = c2;
					}
					else if (char.IsLowSurrogate(c2))
					{
						char* ptr3 = chars;
						encoderFallbackBuffer.InternalFallback(c2, ref ptr3);
						chars = ptr3;
					}
					else
					{
						num += 4;
					}
				}
			}
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Too many characters. The resulting number of bytes is larger than what can be returned as an int.");
			}
			return num;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00067AE4 File Offset: 0x00065CE4
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			char* ptr = chars;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			byte* ptr4 = bytes + byteCount;
			char c = '\0';
			EncoderFallbackBuffer encoderFallbackBuffer;
			if (encoder != null)
			{
				c = encoder._charLeftOver;
				encoderFallbackBuffer = encoder.FallbackBuffer;
				if (encoder._throwOnOverflow && encoderFallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException(SR.Format("Must complete Convert() operation or call Encoder.Reset() before calling GetBytes() or GetByteCount(). Encoder '{0}' fallback '{1}'.", this.EncodingName, encoder.Fallback.GetType()));
				}
			}
			else
			{
				encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
			}
			encoderFallbackBuffer.InternalInitialize(ptr, ptr2, encoder, true);
			for (;;)
			{
				char c2;
				char* ptr5;
				if ((c2 = encoderFallbackBuffer.InternalGetNextChar()) != '\0' || chars < ptr2)
				{
					if (c2 == '\0')
					{
						c2 = *chars;
						chars++;
					}
					if (c != '\0')
					{
						if (!char.IsLowSurrogate(c2))
						{
							chars--;
							ptr5 = chars;
							encoderFallbackBuffer.InternalFallback(c, ref ptr5);
							chars = ptr5;
							c = '\0';
							continue;
						}
						uint surrogate = this.GetSurrogate(c, c2);
						c = '\0';
						if (bytes + 3 >= ptr4)
						{
							if (encoderFallbackBuffer.bFallingBack)
							{
								encoderFallbackBuffer.MovePrevious();
								encoderFallbackBuffer.MovePrevious();
							}
							else
							{
								chars -= 2;
							}
							base.ThrowBytesOverflow(encoder, bytes == ptr3);
							c = '\0';
						}
						else
						{
							if (this._bigEndian)
							{
								*(bytes++) = 0;
								*(bytes++) = (byte)(surrogate >> 16);
								*(bytes++) = (byte)(surrogate >> 8);
								*(bytes++) = (byte)surrogate;
								continue;
							}
							*(bytes++) = (byte)surrogate;
							*(bytes++) = (byte)(surrogate >> 8);
							*(bytes++) = (byte)(surrogate >> 16);
							*(bytes++) = 0;
							continue;
						}
					}
					else
					{
						if (char.IsHighSurrogate(c2))
						{
							c = c2;
							continue;
						}
						if (char.IsLowSurrogate(c2))
						{
							ptr5 = chars;
							encoderFallbackBuffer.InternalFallback(c2, ref ptr5);
							chars = ptr5;
							continue;
						}
						if (bytes + 3 >= ptr4)
						{
							if (encoderFallbackBuffer.bFallingBack)
							{
								encoderFallbackBuffer.MovePrevious();
							}
							else
							{
								chars--;
							}
							base.ThrowBytesOverflow(encoder, bytes == ptr3);
						}
						else
						{
							if (this._bigEndian)
							{
								*(bytes++) = 0;
								*(bytes++) = 0;
								*(bytes++) = (byte)(c2 >> 8);
								*(bytes++) = (byte)c2;
								continue;
							}
							*(bytes++) = (byte)c2;
							*(bytes++) = (byte)(c2 >> 8);
							*(bytes++) = 0;
							*(bytes++) = 0;
							continue;
						}
					}
				}
				if ((encoder != null && !encoder.MustFlush) || c <= '\0')
				{
					break;
				}
				ptr5 = chars;
				encoderFallbackBuffer.InternalFallback(c, ref ptr5);
				chars = ptr5;
				c = '\0';
			}
			if (encoder != null)
			{
				encoder._charLeftOver = c;
				encoder._charsUsed = (int)((long)(chars - ptr));
			}
			return (int)((long)(bytes - ptr3));
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00067D7C File Offset: 0x00065F7C
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			UTF32Encoding.UTF32Decoder utf32Decoder = (UTF32Encoding.UTF32Decoder)baseDecoder;
			int num = 0;
			byte* ptr = bytes + count;
			byte* ptr2 = bytes;
			int i = 0;
			uint num2 = 0U;
			DecoderFallbackBuffer decoderFallbackBuffer;
			if (utf32Decoder != null)
			{
				i = utf32Decoder.readByteCount;
				num2 = (uint)utf32Decoder.iChar;
				decoderFallbackBuffer = utf32Decoder.FallbackBuffer;
			}
			else
			{
				decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
			}
			decoderFallbackBuffer.InternalInitialize(ptr2, null);
			while (bytes < ptr && num >= 0)
			{
				if (this._bigEndian)
				{
					num2 <<= 8;
					num2 += (uint)(*(bytes++));
				}
				else
				{
					num2 >>= 8;
					num2 += (uint)((uint)(*(bytes++)) << 24);
				}
				i++;
				if (i >= 4)
				{
					i = 0;
					if (num2 > 1114111U || (num2 >= 55296U && num2 <= 57343U))
					{
						byte[] array;
						if (this._bigEndian)
						{
							array = new byte[]
							{
								(byte)(num2 >> 24),
								(byte)(num2 >> 16),
								(byte)(num2 >> 8),
								(byte)num2
							};
						}
						else
						{
							array = new byte[]
							{
								(byte)num2,
								(byte)(num2 >> 8),
								(byte)(num2 >> 16),
								(byte)(num2 >> 24)
							};
						}
						num += decoderFallbackBuffer.InternalFallback(array, bytes);
						num2 = 0U;
					}
					else
					{
						if (num2 >= 65536U)
						{
							num++;
						}
						num++;
						num2 = 0U;
					}
				}
			}
			if (i > 0 && (utf32Decoder == null || utf32Decoder.MustFlush))
			{
				byte[] array2 = new byte[i];
				if (this._bigEndian)
				{
					while (i > 0)
					{
						array2[--i] = (byte)num2;
						num2 >>= 8;
					}
				}
				else
				{
					while (i > 0)
					{
						array2[--i] = (byte)(num2 >> 24);
						num2 <<= 8;
					}
				}
				num += decoderFallbackBuffer.InternalFallback(array2, bytes);
			}
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Too many characters. The resulting number of bytes is larger than what can be returned as an int.");
			}
			return num;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x00067F3C File Offset: 0x0006613C
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			UTF32Encoding.UTF32Decoder utf32Decoder = (UTF32Encoding.UTF32Decoder)baseDecoder;
			char* ptr = chars;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			byte* ptr4 = bytes + byteCount;
			int num = 0;
			uint num2 = 0U;
			DecoderFallbackBuffer decoderFallbackBuffer;
			if (utf32Decoder != null)
			{
				num = utf32Decoder.readByteCount;
				num2 = (uint)utf32Decoder.iChar;
				decoderFallbackBuffer = baseDecoder.FallbackBuffer;
			}
			else
			{
				decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
			}
			decoderFallbackBuffer.InternalInitialize(bytes, chars + charCount);
			while (bytes < ptr4)
			{
				if (this._bigEndian)
				{
					num2 <<= 8;
					num2 += (uint)(*(bytes++));
				}
				else
				{
					num2 >>= 8;
					num2 += (uint)((uint)(*(bytes++)) << 24);
				}
				num++;
				if (num >= 4)
				{
					num = 0;
					if (num2 > 1114111U || (num2 >= 55296U && num2 <= 57343U))
					{
						byte[] array;
						if (this._bigEndian)
						{
							array = new byte[]
							{
								(byte)(num2 >> 24),
								(byte)(num2 >> 16),
								(byte)(num2 >> 8),
								(byte)num2
							};
						}
						else
						{
							array = new byte[]
							{
								(byte)num2,
								(byte)(num2 >> 8),
								(byte)(num2 >> 16),
								(byte)(num2 >> 24)
							};
						}
						char* ptr5 = chars;
						bool flag = decoderFallbackBuffer.InternalFallback(array, bytes, ref ptr5);
						chars = ptr5;
						if (!flag)
						{
							bytes -= 4;
							num2 = 0U;
							decoderFallbackBuffer.InternalReset();
							base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
							break;
						}
						num2 = 0U;
					}
					else
					{
						if (num2 >= 65536U)
						{
							if (chars >= ptr2 - 1)
							{
								bytes -= 4;
								num2 = 0U;
								base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
								break;
							}
							*(chars++) = this.GetHighSurrogate(num2);
							num2 = (uint)this.GetLowSurrogate(num2);
						}
						else if (chars >= ptr2)
						{
							bytes -= 4;
							num2 = 0U;
							base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
							break;
						}
						*(chars++) = (char)num2;
						num2 = 0U;
					}
				}
			}
			if (num > 0 && (utf32Decoder == null || utf32Decoder.MustFlush))
			{
				byte[] array2 = new byte[num];
				int i = num;
				if (this._bigEndian)
				{
					while (i > 0)
					{
						array2[--i] = (byte)num2;
						num2 >>= 8;
					}
				}
				else
				{
					while (i > 0)
					{
						array2[--i] = (byte)(num2 >> 24);
						num2 <<= 8;
					}
				}
				char* ptr5 = chars;
				bool flag2 = decoderFallbackBuffer.InternalFallback(array2, bytes, ref ptr5);
				chars = ptr5;
				if (!flag2)
				{
					decoderFallbackBuffer.InternalReset();
					base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
				}
				else
				{
					num = 0;
					num2 = 0U;
				}
			}
			if (utf32Decoder != null)
			{
				utf32Decoder.iChar = (int)num2;
				utf32Decoder.readByteCount = num;
				utf32Decoder._bytesUsed = (int)((long)(bytes - ptr3));
			}
			return (int)((long)(chars - ptr));
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000681BF File Offset: 0x000663BF
		private uint GetSurrogate(char cHigh, char cLow)
		{
			return (uint)((cHigh - '\ud800') * 'Ѐ' + (cLow - '\udc00')) + 65536U;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000681DC File Offset: 0x000663DC
		private char GetHighSurrogate(uint iChar)
		{
			return (char)((iChar - 65536U) / 1024U + 55296U);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000681F2 File Offset: 0x000663F2
		private char GetLowSurrogate(uint iChar)
		{
			return (char)((iChar - 65536U) % 1024U + 56320U);
		}

		/// <summary>Obtains a decoder that converts a UTF-32 encoded sequence of bytes into a sequence of Unicode characters.</summary>
		/// <returns>A <see cref="T:System.Text.Decoder" /> that converts a UTF-32 encoded sequence of bytes into a sequence of Unicode characters.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B2F RID: 6959 RVA: 0x00068208 File Offset: 0x00066408
		public override Decoder GetDecoder()
		{
			return new UTF32Encoding.UTF32Decoder(this);
		}

		/// <summary>Obtains an encoder that converts a sequence of Unicode characters into a UTF-32 encoded sequence of bytes.</summary>
		/// <returns>A <see cref="T:System.Text.Encoder" /> that converts a sequence of Unicode characters into a UTF-32 encoded sequence of bytes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B30 RID: 6960 RVA: 0x00062BCC File Offset: 0x00060DCC
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}

		/// <summary>Calculates the maximum number of bytes produced by encoding the specified number of characters.</summary>
		/// <returns>The maximum number of bytes produced by encoding the specified number of characters.</returns>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero.-or- The resulting number of bytes is greater than the maximum number that can be returned as an integer. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B31 RID: 6961 RVA: 0x00068210 File Offset: 0x00066410
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", "Non-negative number required.");
			}
			long num = (long)charCount + 1L;
			if (base.EncoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.EncoderFallback.MaxCharCount;
			}
			num *= 4L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", "Too many characters. The resulting number of bytes is larger than what can be returned as an int.");
			}
			return (int)num;
		}

		/// <summary>Calculates the maximum number of characters produced by decoding the specified number of bytes.</summary>
		/// <returns>The maximum number of characters produced by decoding the specified number of bytes.</returns>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is less than zero.-or- The resulting number of bytes is greater than the maximum number that can be returned as an integer. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B32 RID: 6962 RVA: 0x00068274 File Offset: 0x00066474
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", "Non-negative number required.");
			}
			int num = byteCount / 2 + 2;
			if (base.DecoderFallback.MaxCharCount > 2)
			{
				num *= base.DecoderFallback.MaxCharCount;
				num /= 2;
			}
			if (num > 2147483647)
			{
				throw new ArgumentOutOfRangeException("byteCount", "Too many bytes. The resulting number of chars is larger than what can be returned as an int.");
			}
			return num;
		}

		/// <summary>Returns a Unicode byte order mark encoded in UTF-32 format, if the constructor for this instance requests a byte order mark.</summary>
		/// <returns>A byte array containing the Unicode byte order mark, if the constructor for this instance requests a byte order mark. Otherwise, this method returns a byte array of length zero.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B33 RID: 6963 RVA: 0x000682D4 File Offset: 0x000664D4
		public override byte[] GetPreamble()
		{
			if (!this._emitUTF32ByteOrderMark)
			{
				return Array.Empty<byte>();
			}
			if (this._bigEndian)
			{
				return new byte[] { 0, 0, 254, byte.MaxValue };
			}
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			return array;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x00068324 File Offset: 0x00066524
		public override ReadOnlySpan<byte> Preamble
		{
			get
			{
				return (base.GetType() != typeof(UTF32Encoding)) ? this.GetPreamble() : (this._emitUTF32ByteOrderMark ? (this._bigEndian ? UTF32Encoding.s_bigEndianPreamble : UTF32Encoding.s_littleEndianPreamble) : Array.Empty<byte>());
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Text.UTF32Encoding" /> object.</summary>
		/// <returns>true if <paramref name="value" /> is an instance of <see cref="T:System.Text.UTF32Encoding" /> and is equal to the current object; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to compare with the current object. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B35 RID: 6965 RVA: 0x00068378 File Offset: 0x00066578
		public override bool Equals(object value)
		{
			UTF32Encoding utf32Encoding = value as UTF32Encoding;
			return utf32Encoding != null && (this._emitUTF32ByteOrderMark == utf32Encoding._emitUTF32ByteOrderMark && this._bigEndian == utf32Encoding._bigEndian && base.EncoderFallback.Equals(utf32Encoding.EncoderFallback)) && base.DecoderFallback.Equals(utf32Encoding.DecoderFallback);
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>The hash code for the current <see cref="T:System.Text.UTF32Encoding" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B36 RID: 6966 RVA: 0x000683D3 File Offset: 0x000665D3
		public override int GetHashCode()
		{
			return base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode() + this.CodePage + (this._emitUTF32ByteOrderMark ? 4 : 0) + (this._bigEndian ? 8 : 0);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00068410 File Offset: 0x00066610
		// Note: this type is marked as 'beforefieldinit'.
		static UTF32Encoding()
		{
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			UTF32Encoding.s_littleEndianPreamble = array;
		}

		// Token: 0x04000C9E RID: 3230
		internal static readonly UTF32Encoding s_default = new UTF32Encoding(false, true);

		// Token: 0x04000C9F RID: 3231
		internal static readonly UTF32Encoding s_bigEndianDefault = new UTF32Encoding(true, true);

		// Token: 0x04000CA0 RID: 3232
		private static readonly byte[] s_bigEndianPreamble = new byte[] { 0, 0, 254, byte.MaxValue };

		// Token: 0x04000CA1 RID: 3233
		private static readonly byte[] s_littleEndianPreamble;

		// Token: 0x04000CA2 RID: 3234
		private bool _emitUTF32ByteOrderMark;

		// Token: 0x04000CA3 RID: 3235
		private bool _isThrowException;

		// Token: 0x04000CA4 RID: 3236
		private bool _bigEndian;

		// Token: 0x020002F9 RID: 761
		[Serializable]
		private sealed class UTF32Decoder : DecoderNLS
		{
			// Token: 0x06001B38 RID: 6968 RVA: 0x0006846B File Offset: 0x0006666B
			public UTF32Decoder(UTF32Encoding encoding)
				: base(encoding)
			{
			}

			// Token: 0x06001B39 RID: 6969 RVA: 0x00068474 File Offset: 0x00066674
			public override void Reset()
			{
				this.iChar = 0;
				this.readByteCount = 0;
				if (this._fallbackBuffer != null)
				{
					this._fallbackBuffer.Reset();
				}
			}

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06001B3A RID: 6970 RVA: 0x00068497 File Offset: 0x00066697
			internal override bool HasState
			{
				get
				{
					return this.readByteCount != 0;
				}
			}

			// Token: 0x04000CA5 RID: 3237
			internal int iChar;

			// Token: 0x04000CA6 RID: 3238
			internal int readByteCount;
		}
	}
}
