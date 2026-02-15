using System;

namespace System.Text
{
	/// <summary>Represents a substitute input string that is used when the original input character cannot be encoded. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002F1 RID: 753
	public sealed class EncoderReplacementFallbackBuffer : EncoderFallbackBuffer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallbackBuffer" /> class using the value of a <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <param name="fallback">A <see cref="T:System.Text.EncoderReplacementFallback" /> object. </param>
		// Token: 0x06001AA1 RID: 6817 RVA: 0x00064A7F File Offset: 0x00062C7F
		public EncoderReplacementFallbackBuffer(EncoderReplacementFallback fallback)
		{
			this._strDefault = fallback.DefaultString + fallback.DefaultString;
		}

		/// <summary>Prepares the replacement fallback buffer to use the current replacement string.</summary>
		/// <returns>true if the replacement string is not empty; false if the replacement string is empty.</returns>
		/// <param name="charUnknown">An input character. This parameter is ignored in this operation unless an exception is thrown.</param>
		/// <param name="index">The index position of the character in the input buffer. This parameter is ignored in this operation.</param>
		/// <exception cref="T:System.ArgumentException">This method is called again before the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> method has read all the characters in the replacement fallback buffer.  </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AA2 RID: 6818 RVA: 0x00064AAC File Offset: 0x00062CAC
		public override bool Fallback(char charUnknown, int index)
		{
			if (this._fallbackCount >= 1)
			{
				if (char.IsHighSurrogate(charUnknown) && this._fallbackCount >= 0 && char.IsLowSurrogate(this._strDefault[this._fallbackIndex + 1]))
				{
					base.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknown, this._strDefault[this._fallbackIndex + 1]));
				}
				base.ThrowLastCharRecursive((int)charUnknown);
			}
			this._fallbackCount = this._strDefault.Length / 2;
			this._fallbackIndex = -1;
			return this._fallbackCount != 0;
		}

		/// <summary>Indicates whether a replacement string can be used when an input surrogate pair cannot be encoded, or whether the surrogate pair can be ignored. Parameters specify the surrogate pair and the index position of the pair in the input.</summary>
		/// <returns>true if the replacement string is not empty; false if the replacement string is empty.</returns>
		/// <param name="charUnknownHigh">The high surrogate of the input pair.</param>
		/// <param name="charUnknownLow">The low surrogate of the input pair.</param>
		/// <param name="index">The index position of the surrogate pair in the input buffer.</param>
		/// <exception cref="T:System.ArgumentException">This method is called again before the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> method has read all the replacement string characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="charUnknownHigh" /> is less than U+D800 or greater than U+D8FF.-or-The value of <paramref name="charUnknownLow" /> is less than U+DC00 or greater than U+DFFF.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AA3 RID: 6819 RVA: 0x00064B38 File Offset: 0x00062D38
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format("Valid values are between {0} and {1}, inclusive.", 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("charUnknownLow", SR.Format("Valid values are between {0} and {1}, inclusive.", 56320, 57343));
			}
			if (this._fallbackCount >= 1)
			{
				base.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknownHigh, charUnknownLow));
			}
			this._fallbackCount = this._strDefault.Length;
			this._fallbackIndex = -1;
			return this._fallbackCount != 0;
		}

		/// <summary>Retrieves the next character in the replacement fallback buffer.</summary>
		/// <returns>The next Unicode character in the replacement fallback buffer that the application can encode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AA4 RID: 6820 RVA: 0x00064BE0 File Offset: 0x00062DE0
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

		/// <summary>Causes the next call to the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> method to access the character position in the replacement fallback buffer prior to the current character position. </summary>
		/// <returns>true if the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.MovePrevious" /> operation was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AA5 RID: 6821 RVA: 0x00064C3B File Offset: 0x00062E3B
		public override bool MovePrevious()
		{
			if (this._fallbackCount >= -1 && this._fallbackIndex >= 0)
			{
				this._fallbackIndex--;
				this._fallbackCount++;
				return true;
			}
			return false;
		}

		/// <summary>Gets the number of characters in the replacement fallback buffer that remain to be processed.</summary>
		/// <returns>The number of characters in the replacement fallback buffer that have not yet been processed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x00064C6E File Offset: 0x00062E6E
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

		/// <summary>Initializes all internal state information and data in this instance of <see cref="T:System.Text.EncoderReplacementFallbackBuffer" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AA7 RID: 6823 RVA: 0x00064C81 File Offset: 0x00062E81
		public override void Reset()
		{
			this._fallbackCount = -1;
			this._fallbackIndex = 0;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x04000C8C RID: 3212
		private string _strDefault;

		// Token: 0x04000C8D RID: 3213
		private int _fallbackCount = -1;

		// Token: 0x04000C8E RID: 3214
		private int _fallbackIndex = -1;
	}
}
