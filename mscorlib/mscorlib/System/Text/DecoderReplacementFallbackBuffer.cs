using System;

namespace System.Text
{
	/// <summary>Represents a substitute output string that is emitted when the original input byte sequence cannot be decoded. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E6 RID: 742
	public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> class using the value of a <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <param name="fallback">A <see cref="T:System.Text.DecoderReplacementFallback" /> object that contains a replacement string. </param>
		// Token: 0x06001A4D RID: 6733 RVA: 0x00063A6F File Offset: 0x00061C6F
		public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
		{
			this._strDefault = fallback.DefaultString;
		}

		/// <summary>Prepares the replacement fallback buffer to use the current replacement string.</summary>
		/// <returns>true if the replacement string is not empty; false if the replacement string is empty.</returns>
		/// <param name="bytesUnknown">An input byte sequence. This parameter is ignored unless an exception is thrown.</param>
		/// <param name="index">The index position of the byte in <paramref name="bytesUnknown" />. This parameter is ignored in this operation.</param>
		/// <exception cref="T:System.ArgumentException">This method is called again before the <see cref="M:System.Text.DecoderReplacementFallbackBuffer.GetNextChar" /> method has read all the characters in the replacement fallback buffer.  </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A4E RID: 6734 RVA: 0x00063A91 File Offset: 0x00061C91
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			if (this._fallbackCount >= 1)
			{
				base.ThrowLastBytesRecursive(bytesUnknown);
			}
			if (this._strDefault.Length == 0)
			{
				return false;
			}
			this._fallbackCount = this._strDefault.Length;
			this._fallbackIndex = -1;
			return true;
		}

		/// <summary>Retrieves the next character in the replacement fallback buffer.</summary>
		/// <returns>The next character in the replacement fallback buffer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A4F RID: 6735 RVA: 0x00063ACC File Offset: 0x00061CCC
		public override char GetNextChar()
		{
			this._fallbackCount--;
			this._fallbackIndex++;
			if (this._fallbackCount < 0)
			{
				return '\0';
			}
			if (this._fallbackCount == 2147483647)
			{
				this._fallbackCount = -1;
				return '\0';
			}
			return this._strDefault[this._fallbackIndex];
		}

		/// <summary>Gets the number of characters in the replacement fallback buffer that remain to be processed.</summary>
		/// <returns>The number of characters in the replacement fallback buffer that have not yet been processed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00063B27 File Offset: 0x00061D27
		public override int Remaining
		{
			get
			{
				if (this._fallbackCount >= 0)
				{
					return this._fallbackCount;
				}
				return 0;
			}
		}

		/// <summary>Initializes all internal state information and data in the <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A51 RID: 6737 RVA: 0x00063B3A File Offset: 0x00061D3A
		public override void Reset()
		{
			this._fallbackCount = -1;
			this._fallbackIndex = -1;
			this.byteStart = null;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00063B52 File Offset: 0x00061D52
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return this._strDefault.Length;
		}

		// Token: 0x04000C6D RID: 3181
		private string _strDefault;

		// Token: 0x04000C6E RID: 3182
		private int _fallbackCount = -1;

		// Token: 0x04000C6F RID: 3183
		private int _fallbackIndex = -1;
	}
}
