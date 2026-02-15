using System;

namespace System.Text
{
	/// <summary>Provides a buffer that allows a fallback handler to return an alternate string to an encoder when it cannot encode an input character. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002EE RID: 750
	public abstract class EncoderFallbackBuffer
	{
		/// <summary>When overridden in a derived class, prepares the fallback buffer to handle the specified input character. </summary>
		/// <returns>true if the fallback buffer can process <paramref name="charUnknown" />; false if the fallback buffer ignores <paramref name="charUnknown" />.</returns>
		/// <param name="charUnknown">An input character.</param>
		/// <param name="index">The index position of the character in the input buffer.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A80 RID: 6784
		public abstract bool Fallback(char charUnknown, int index);

		/// <summary>When overridden in a derived class, prepares the fallback buffer to handle the specified surrogate pair.</summary>
		/// <returns>true if the fallback buffer can process <paramref name="charUnknownHigh" /> and <paramref name="charUnknownLow" />; false if the fallback buffer ignores the surrogate pair.</returns>
		/// <param name="charUnknownHigh">The high surrogate of the input pair.</param>
		/// <param name="charUnknownLow">The low surrogate of the input pair.</param>
		/// <param name="index">The index position of the surrogate pair in the input buffer.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A81 RID: 6785
		public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

		/// <summary>When overridden in a derived class, retrieves the next character in the fallback buffer.</summary>
		/// <returns>The next character in the fallback buffer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A82 RID: 6786
		public abstract char GetNextChar();

		/// <summary>When overridden in a derived class, causes the next call to the <see cref="M:System.Text.EncoderFallbackBuffer.GetNextChar" /> method to access the data buffer character position that is prior to the current character position. </summary>
		/// <returns>true if the <see cref="M:System.Text.EncoderFallbackBuffer.MovePrevious" /> operation was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A83 RID: 6787
		public abstract bool MovePrevious();

		/// <summary>When overridden in a derived class, gets the number of characters in the current <see cref="T:System.Text.EncoderFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The number of characters in the current fallback buffer that have not yet been processed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001A84 RID: 6788
		public abstract int Remaining { get; }

		/// <summary>Initializes all data and state information pertaining to this fallback buffer.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A85 RID: 6789 RVA: 0x00064386 File Offset: 0x00062586
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00064390 File Offset: 0x00062590
		internal void InternalReset()
		{
			this.charStart = null;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
			this.Reset();
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000643AE File Offset: 0x000625AE
		internal unsafe void InternalInitialize(char* charStart, char* charEnd, EncoderNLS encoder, bool setEncoder)
		{
			this.charStart = charStart;
			this.charEnd = charEnd;
			this.encoder = encoder;
			this.setEncoder = setEncoder;
			this.bUsedEncoder = false;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000643E4 File Offset: 0x000625E4
		internal char InternalGetNextChar()
		{
			char nextChar = this.GetNextChar();
			this.bFallingBack = nextChar > '\0';
			if (nextChar == '\0')
			{
				this.iRecursionCount = 0;
			}
			return nextChar;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00064410 File Offset: 0x00062610
		internal unsafe virtual bool InternalFallback(char ch, ref char* chars)
		{
			int num = (chars - this.charStart) / 2 - 1;
			if (char.IsHighSurrogate(ch))
			{
				if (chars >= this.charEnd)
				{
					if (this.encoder != null && !this.encoder.MustFlush)
					{
						if (this.setEncoder)
						{
							this.bUsedEncoder = true;
							this.encoder._charLeftOver = ch;
						}
						this.bFallingBack = false;
						return false;
					}
				}
				else
				{
					char c = (char)(*chars);
					if (char.IsLowSurrogate(c))
					{
						if (this.bFallingBack)
						{
							int num2 = this.iRecursionCount;
							this.iRecursionCount = num2 + 1;
							if (num2 > 250)
							{
								this.ThrowLastCharRecursive(char.ConvertToUtf32(ch, c));
							}
						}
						chars += 2;
						this.bFallingBack = this.Fallback(ch, c, num);
						return this.bFallingBack;
					}
				}
			}
			if (this.bFallingBack)
			{
				int num2 = this.iRecursionCount;
				this.iRecursionCount = num2 + 1;
				if (num2 > 250)
				{
					this.ThrowLastCharRecursive((int)ch);
				}
			}
			this.bFallingBack = this.Fallback(ch, num);
			return this.bFallingBack;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0006450E File Offset: 0x0006270E
		internal void ThrowLastCharRecursive(int charRecursive)
		{
			throw new ArgumentException(SR.Format("Recursive fallback not allowed for character \\\\u{0:X4}.", charRecursive), "chars");
		}

		// Token: 0x04000C7F RID: 3199
		internal unsafe char* charStart;

		// Token: 0x04000C80 RID: 3200
		internal unsafe char* charEnd;

		// Token: 0x04000C81 RID: 3201
		internal EncoderNLS encoder;

		// Token: 0x04000C82 RID: 3202
		internal bool setEncoder;

		// Token: 0x04000C83 RID: 3203
		internal bool bUsedEncoder;

		// Token: 0x04000C84 RID: 3204
		internal bool bFallingBack;

		// Token: 0x04000C85 RID: 3205
		internal int iRecursionCount;
	}
}
