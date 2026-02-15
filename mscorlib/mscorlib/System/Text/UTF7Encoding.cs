using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	/// <summary>Represents a UTF-7 encoding of Unicode characters.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020002FA RID: 762
	[Serializable]
	public class UTF7Encoding : Encoding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.UTF7Encoding" /> class.</summary>
		// Token: 0x06001B3B RID: 6971 RVA: 0x000684A2 File Offset: 0x000666A2
		public UTF7Encoding()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.UTF7Encoding" /> class. A parameter specifies whether to allow optional characters.</summary>
		/// <param name="allowOptionals">true to specify that optional characters are allowed; otherwise, false. </param>
		// Token: 0x06001B3C RID: 6972 RVA: 0x000684AB File Offset: 0x000666AB
		public UTF7Encoding(bool allowOptionals)
			: base(65000)
		{
			this._allowOptionals = allowOptionals;
			this.MakeTables();
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000684C8 File Offset: 0x000666C8
		private void MakeTables()
		{
			this._base64Bytes = new byte[64];
			for (int i = 0; i < 64; i++)
			{
				this._base64Bytes[i] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[i];
			}
			this._base64Values = new sbyte[128];
			for (int j = 0; j < 128; j++)
			{
				this._base64Values[j] = -1;
			}
			for (int k = 0; k < 64; k++)
			{
				this._base64Values[(int)this._base64Bytes[k]] = (sbyte)k;
			}
			this._directEncode = new bool[128];
			int num = "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Length;
			for (int l = 0; l < num; l++)
			{
				this._directEncode[(int)"\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[l]] = true;
			}
			if (this._allowOptionals)
			{
				num = "!\"#$%&*;<=>@[]^_`{|}".Length;
				for (int m = 0; m < num; m++)
				{
					this._directEncode[(int)"!\"#$%&*;<=>@[]^_`{|}"[m]] = true;
				}
			}
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000685C0 File Offset: 0x000667C0
		internal override void SetDefaultFallbacks()
		{
			this.encoderFallback = new EncoderReplacementFallback(string.Empty);
			this.decoderFallback = new UTF7Encoding.DecoderUTF7Fallback();
		}

		/// <summary>Gets a value indicating whether the specified object is equal to the current <see cref="T:System.Text.UTF7Encoding" /> object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Text.UTF7Encoding" /> object and is equal to the current <see cref="T:System.Text.UTF7Encoding" /> object; otherwise, false.</returns>
		/// <param name="value">An object to compare to the current <see cref="T:System.Text.UTF7Encoding" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B3F RID: 6975 RVA: 0x000685E0 File Offset: 0x000667E0
		public override bool Equals(object value)
		{
			UTF7Encoding utf7Encoding = value as UTF7Encoding;
			return utf7Encoding != null && (this._allowOptionals == utf7Encoding._allowOptionals && base.EncoderFallback.Equals(utf7Encoding.EncoderFallback)) && base.DecoderFallback.Equals(utf7Encoding.DecoderFallback);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Text.UTF7Encoding" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B40 RID: 6976 RVA: 0x0006862D File Offset: 0x0006682D
		public override int GetHashCode()
		{
			return this.CodePage + base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode();
		}

		/// <summary>Calculates the number of bytes produced by encoding a set of characters from the specified character array.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="index">The index of the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />.-or- The resulting number of bytes is greater than the maximum number that can be returned as an int. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B41 RID: 6977 RVA: 0x00068650 File Offset: 0x00066850
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

		/// <summary>Calculates the number of bytes produced by encoding the characters in the specified <see cref="T:System.String" /> object.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="s">The <see cref="T:System.String" /> object containing the set of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting number of bytes is greater than the maximum number that can be returned as an int. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B42 RID: 6978 RVA: 0x000686D8 File Offset: 0x000668D8
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
		///   <paramref name="chars" /> is null (Nothing in Visual Basic .NET). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.-or- The resulting number of bytes is greater than the maximum number that can be returned as an int. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B43 RID: 6979 RVA: 0x000620AD File Offset: 0x000602AD
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
		///   <paramref name="s" /> is null (Nothing).-or- <paramref name="bytes" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B44 RID: 6980 RVA: 0x00068714 File Offset: 0x00066914
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
		///   <paramref name="chars" /> is null (Nothing).-or- <paramref name="bytes" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B45 RID: 6981 RVA: 0x000687DC File Offset: 0x000669DC
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
		/// <returns>The actual number of bytes written at the location indicated by <paramref name="bytes" />.</returns>
		/// <param name="chars">A pointer to the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">A pointer to the location at which to start writing the resulting sequence of bytes. </param>
		/// <param name="byteCount">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null (Nothing).-or- <paramref name="bytes" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="byteCount" /> is less than the resulting number of bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B46 RID: 6982 RVA: 0x000688AC File Offset: 0x00066AAC
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
		///   <paramref name="bytes" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />.-or- The resulting number of characters is greater than the maximum number that can be returned as an int. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B47 RID: 6983 RVA: 0x00068910 File Offset: 0x00066B10
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
		///   <paramref name="bytes" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.-or- The resulting number of characters is greater than the maximum number that can be returned as an int. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B48 RID: 6984 RVA: 0x00062363 File Offset: 0x00060563
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
		///   <paramref name="bytes" /> is null (Nothing).-or- <paramref name="chars" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteIndex" /> or <paramref name="byteCount" /> or <paramref name="charIndex" /> is less than zero.-or- <paramref name="byteindex" /> and <paramref name="byteCount" /> do not denote a valid range in <paramref name="bytes" />.-or- <paramref name="charIndex" /> is not a valid index in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="chars" /> does not have enough capacity from <paramref name="charIndex" /> to the end of the array to accommodate the resulting characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B49 RID: 6985 RVA: 0x00068994 File Offset: 0x00066B94
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
		///   <paramref name="bytes" /> is null (Nothing).-or- <paramref name="chars" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> or <paramref name="charCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="charCount" /> is less than the resulting number of characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B4A RID: 6986 RVA: 0x00068A64 File Offset: 0x00066C64
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
		///   <paramref name="bytes" /> is null (Nothing). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for fuller explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B4B RID: 6987 RVA: 0x00068AC8 File Offset: 0x00066CC8
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

		// Token: 0x06001B4C RID: 6988 RVA: 0x00068B4E File Offset: 0x00066D4E
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
		{
			return this.GetBytes(chars, count, null, 0, baseEncoder);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00068B5C File Offset: 0x00066D5C
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
		{
			UTF7Encoding.Encoder encoder = (UTF7Encoding.Encoder)baseEncoder;
			int num = 0;
			int i = -1;
			Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer(this, encoder, bytes, byteCount, chars, charCount);
			if (encoder != null)
			{
				num = encoder.bits;
				i = encoder.bitCount;
				while (i >= 6)
				{
					i -= 6;
					if (!encodingByteBuffer.AddByte(this._base64Bytes[(num >> i) & 63]))
					{
						base.ThrowBytesOverflow(encoder, encodingByteBuffer.Count == 0);
					}
				}
			}
			while (encodingByteBuffer.MoreData)
			{
				char c = encodingByteBuffer.GetNextChar();
				if (c < '\u0080' && this._directEncode[(int)c])
				{
					if (i >= 0)
					{
						if (i > 0)
						{
							if (!encodingByteBuffer.AddByte(this._base64Bytes[(num << 6 - i) & 63]))
							{
								break;
							}
							i = 0;
						}
						if (!encodingByteBuffer.AddByte(45))
						{
							break;
						}
						i = -1;
					}
					if (!encodingByteBuffer.AddByte((byte)c))
					{
						break;
					}
				}
				else if (i < 0 && c == '+')
				{
					if (!encodingByteBuffer.AddByte(43, 45))
					{
						break;
					}
				}
				else
				{
					if (i < 0)
					{
						if (!encodingByteBuffer.AddByte(43))
						{
							break;
						}
						i = 0;
					}
					num = (num << 16) | (int)c;
					i += 16;
					while (i >= 6)
					{
						i -= 6;
						if (!encodingByteBuffer.AddByte(this._base64Bytes[(num >> i) & 63]))
						{
							i += 6;
							c = encodingByteBuffer.GetNextChar();
							break;
						}
					}
					if (i >= 6)
					{
						break;
					}
				}
			}
			if (i >= 0 && (encoder == null || encoder.MustFlush))
			{
				if (i > 0 && encodingByteBuffer.AddByte(this._base64Bytes[(num << 6 - i) & 63]))
				{
					i = 0;
				}
				if (encodingByteBuffer.AddByte(45))
				{
					num = 0;
					i = -1;
				}
				else
				{
					encodingByteBuffer.GetNextChar();
				}
			}
			if (bytes != null && encoder != null)
			{
				encoder.bits = num;
				encoder.bitCount = i;
				encoder._charsUsed = encodingByteBuffer.CharsUsed;
			}
			return encodingByteBuffer.Count;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00068D0E File Offset: 0x00066F0E
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			return this.GetChars(bytes, count, null, 0, baseDecoder);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00068D1C File Offset: 0x00066F1C
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			UTF7Encoding.Decoder decoder = (UTF7Encoding.Decoder)baseDecoder;
			Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer(this, decoder, chars, charCount, bytes, byteCount);
			int num = 0;
			int num2 = -1;
			bool flag = false;
			if (decoder != null)
			{
				num = decoder.bits;
				num2 = decoder.bitCount;
				flag = decoder.firstByte;
			}
			if (num2 >= 16)
			{
				if (!encodingCharBuffer.AddChar((char)((num >> num2 - 16) & 65535)))
				{
					base.ThrowCharsOverflow(decoder, true);
				}
				num2 -= 16;
			}
			while (encodingCharBuffer.MoreData)
			{
				byte nextByte = encodingCharBuffer.GetNextByte();
				int num3;
				if (num2 >= 0)
				{
					sbyte b;
					if (nextByte < 128 && (b = this._base64Values[(int)nextByte]) >= 0)
					{
						flag = false;
						num = (num << 6) | (int)((byte)b);
						num2 += 6;
						if (num2 < 16)
						{
							continue;
						}
						num3 = (num >> num2 - 16) & 65535;
						num2 -= 16;
					}
					else
					{
						num2 = -1;
						if (nextByte != 45)
						{
							if (!encodingCharBuffer.Fallback(nextByte))
							{
								break;
							}
							continue;
						}
						else
						{
							if (!flag)
							{
								continue;
							}
							num3 = 43;
						}
					}
				}
				else
				{
					if (nextByte == 43)
					{
						num2 = 0;
						flag = true;
						continue;
					}
					if (nextByte >= 128)
					{
						if (!encodingCharBuffer.Fallback(nextByte))
						{
							break;
						}
						continue;
					}
					else
					{
						num3 = (int)nextByte;
					}
				}
				if (num3 >= 0 && !encodingCharBuffer.AddChar((char)num3))
				{
					if (num2 >= 0)
					{
						encodingCharBuffer.AdjustBytes(1);
						num2 += 16;
						break;
					}
					break;
				}
			}
			if (chars != null && decoder != null)
			{
				if (decoder.MustFlush)
				{
					decoder.bits = 0;
					decoder.bitCount = -1;
					decoder.firstByte = false;
				}
				else
				{
					decoder.bits = num;
					decoder.bitCount = num2;
					decoder.firstByte = flag;
				}
				decoder._bytesUsed = encodingCharBuffer.BytesUsed;
			}
			return encodingCharBuffer.Count;
		}

		/// <summary>Obtains a decoder that converts a UTF-7 encoded sequence of bytes into a sequence of Unicode characters.</summary>
		/// <returns>A <see cref="T:System.Text.Decoder" /> that converts a UTF-7 encoded sequence of bytes into a sequence of Unicode characters.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B50 RID: 6992 RVA: 0x00068EA0 File Offset: 0x000670A0
		public override global::System.Text.Decoder GetDecoder()
		{
			return new UTF7Encoding.Decoder(this);
		}

		/// <summary>Obtains an encoder that converts a sequence of Unicode characters into a UTF-7 encoded sequence of bytes.</summary>
		/// <returns>A <see cref="T:System.Text.Encoder" /> that converts a sequence of Unicode characters into a UTF-7 encoded sequence of bytes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B51 RID: 6993 RVA: 0x00068EA8 File Offset: 0x000670A8
		public override global::System.Text.Encoder GetEncoder()
		{
			return new UTF7Encoding.Encoder(this);
		}

		/// <summary>Calculates the maximum number of bytes produced by encoding the specified number of characters.</summary>
		/// <returns>The maximum number of bytes produced by encoding the specified number of characters.</returns>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero.-or- The resulting number of bytes is greater than the maximum number that can be returned as an int. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B52 RID: 6994 RVA: 0x00068EB0 File Offset: 0x000670B0
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", "Non-negative number required.");
			}
			long num = (long)charCount * 3L + 2L;
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
		///   <paramref name="byteCount" /> is less than zero.-or- The resulting number of characters is greater than the maximum number that can be returned as an int. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B53 RID: 6995 RVA: 0x00068EE8 File Offset: 0x000670E8
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", "Non-negative number required.");
			}
			int num = byteCount;
			if (num == 0)
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x04000CA7 RID: 3239
		internal static readonly UTF7Encoding s_default = new UTF7Encoding();

		// Token: 0x04000CA8 RID: 3240
		private byte[] _base64Bytes;

		// Token: 0x04000CA9 RID: 3241
		private sbyte[] _base64Values;

		// Token: 0x04000CAA RID: 3242
		private bool[] _directEncode;

		// Token: 0x04000CAB RID: 3243
		private bool _allowOptionals;

		// Token: 0x020002FB RID: 763
		[Serializable]
		private sealed class Decoder : DecoderNLS
		{
			// Token: 0x06001B55 RID: 6997 RVA: 0x0006846B File Offset: 0x0006666B
			public Decoder(UTF7Encoding encoding)
				: base(encoding)
			{
			}

			// Token: 0x06001B56 RID: 6998 RVA: 0x00068F1D File Offset: 0x0006711D
			public override void Reset()
			{
				this.bits = 0;
				this.bitCount = -1;
				this.firstByte = false;
				if (this._fallbackBuffer != null)
				{
					this._fallbackBuffer.Reset();
				}
			}

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06001B57 RID: 6999 RVA: 0x00068F47 File Offset: 0x00067147
			internal override bool HasState
			{
				get
				{
					return this.bitCount != -1;
				}
			}

			// Token: 0x04000CAC RID: 3244
			internal int bits;

			// Token: 0x04000CAD RID: 3245
			internal int bitCount;

			// Token: 0x04000CAE RID: 3246
			internal bool firstByte;
		}

		// Token: 0x020002FC RID: 764
		[Serializable]
		private sealed class Encoder : EncoderNLS
		{
			// Token: 0x06001B58 RID: 7000 RVA: 0x00068F55 File Offset: 0x00067155
			public Encoder(UTF7Encoding encoding)
				: base(encoding)
			{
			}

			// Token: 0x06001B59 RID: 7001 RVA: 0x00068F5E File Offset: 0x0006715E
			public override void Reset()
			{
				this.bitCount = -1;
				this.bits = 0;
				if (this._fallbackBuffer != null)
				{
					this._fallbackBuffer.Reset();
				}
			}

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06001B5A RID: 7002 RVA: 0x00068F81 File Offset: 0x00067181
			internal override bool HasState
			{
				get
				{
					return this.bits != 0 || this.bitCount != -1;
				}
			}

			// Token: 0x04000CAF RID: 3247
			internal int bits;

			// Token: 0x04000CB0 RID: 3248
			internal int bitCount;
		}

		// Token: 0x020002FD RID: 765
		[Serializable]
		private sealed class DecoderUTF7Fallback : DecoderFallback
		{
			// Token: 0x06001B5C RID: 7004 RVA: 0x00068F99 File Offset: 0x00067199
			public override DecoderFallbackBuffer CreateFallbackBuffer()
			{
				return new UTF7Encoding.DecoderUTF7FallbackBuffer(this);
			}

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06001B5D RID: 7005 RVA: 0x0000C091 File Offset: 0x0000A291
			public override int MaxCharCount
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x06001B5E RID: 7006 RVA: 0x00068FA1 File Offset: 0x000671A1
			public override bool Equals(object value)
			{
				return value is UTF7Encoding.DecoderUTF7Fallback;
			}

			// Token: 0x06001B5F RID: 7007 RVA: 0x00068FAE File Offset: 0x000671AE
			public override int GetHashCode()
			{
				return 984;
			}
		}

		// Token: 0x020002FE RID: 766
		private sealed class DecoderUTF7FallbackBuffer : DecoderFallbackBuffer
		{
			// Token: 0x06001B60 RID: 7008 RVA: 0x00068FB5 File Offset: 0x000671B5
			public DecoderUTF7FallbackBuffer(UTF7Encoding.DecoderUTF7Fallback fallback)
			{
			}

			// Token: 0x06001B61 RID: 7009 RVA: 0x00068FC4 File Offset: 0x000671C4
			public override bool Fallback(byte[] bytesUnknown, int index)
			{
				this.cFallback = (char)bytesUnknown[0];
				if (this.cFallback == '\0')
				{
					return false;
				}
				this.iCount = (this.iSize = 1);
				return true;
			}

			// Token: 0x06001B62 RID: 7010 RVA: 0x00068FF8 File Offset: 0x000671F8
			public override char GetNextChar()
			{
				int num = this.iCount;
				this.iCount = num - 1;
				if (num > 0)
				{
					return this.cFallback;
				}
				return '\0';
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06001B63 RID: 7011 RVA: 0x00069021 File Offset: 0x00067221
			public override int Remaining
			{
				get
				{
					if (this.iCount <= 0)
					{
						return 0;
					}
					return this.iCount;
				}
			}

			// Token: 0x06001B64 RID: 7012 RVA: 0x00069034 File Offset: 0x00067234
			public override void Reset()
			{
				this.iCount = -1;
				this.byteStart = null;
			}

			// Token: 0x06001B65 RID: 7013 RVA: 0x00069045 File Offset: 0x00067245
			internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
			{
				if (bytes.Length != 1)
				{
					throw new ArgumentException("String contains invalid Unicode code points.");
				}
				if (bytes[0] != 0)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x04000CB1 RID: 3249
			private char cFallback;

			// Token: 0x04000CB2 RID: 3250
			private int iCount = -1;

			// Token: 0x04000CB3 RID: 3251
			private int iSize;
		}
	}
}
