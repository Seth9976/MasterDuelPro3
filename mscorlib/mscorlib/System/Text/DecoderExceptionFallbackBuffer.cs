using System;
using System.Globalization;

namespace System.Text
{
	/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> when an encoded input byte sequence cannot be converted to a decoded output character. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E0 RID: 736
	public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
	{
		/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> when the input byte sequence cannot be decoded. The nominal return value is not used. </summary>
		/// <returns>None. No value is returned because the <see cref="M:System.Text.DecoderExceptionFallbackBuffer.Fallback(System.Byte[],System.Int32)" /> method always throws an exception. The nominal return value is true. A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <param name="bytesUnknown">An input array of bytes.</param>
		/// <param name="index">The index position of a byte in the input.</param>
		/// <exception cref="T:System.Text.DecoderFallbackException">This method always throws an exception that reports the value and index position of the input byte that cannot be decoded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A20 RID: 6688 RVA: 0x0006324E File Offset: 0x0006144E
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.Throw(bytesUnknown, index);
			return true;
		}

		/// <summary>Retrieves the next character in the exception data buffer.</summary>
		/// <returns>The return value is always the Unicode character NULL (U+0000). A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A21 RID: 6689 RVA: 0x00033991 File Offset: 0x00031B91
		public override char GetNextChar()
		{
			return '\0';
		}

		/// <summary>Gets the number of characters in the current <see cref="T:System.Text.DecoderExceptionFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The return value is always zero. A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00033991 File Offset: 0x00031B91
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0006325C File Offset: 0x0006145C
		private void Throw(byte[] bytesUnknown, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				stringBuilder.Append('[');
				stringBuilder.Append(bytesUnknown[num].ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append(']');
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new DecoderFallbackException(SR.Format("Unable to translate bytes {0} at index {1} from specified code page to Unicode.", stringBuilder, index), bytesUnknown, index);
		}
	}
}
