using System;
using System.Globalization;

namespace System.Text
{
	/// <summary>Provides a buffer that allows a fallback handler to return an alternate string to a decoder when it cannot decode an input byte sequence. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E3 RID: 739
	public abstract class DecoderFallbackBuffer
	{
		/// <summary>When overridden in a derived class, prepares the fallback buffer to handle the specified input byte sequence.</summary>
		/// <returns>true if the fallback buffer can process <paramref name="bytesUnknown" />; false if the fallback buffer ignores <paramref name="bytesUnknown" />.</returns>
		/// <param name="bytesUnknown">An input array of bytes.</param>
		/// <param name="index">The index position of a byte in <paramref name="bytesUnknown" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A2D RID: 6701
		public abstract bool Fallback(byte[] bytesUnknown, int index);

		/// <summary>When overridden in a derived class, retrieves the next character in the fallback buffer.</summary>
		/// <returns>The next character in the fallback buffer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A2E RID: 6702
		public abstract char GetNextChar();

		/// <summary>When overridden in a derived class, gets the number of characters in the current <see cref="T:System.Text.DecoderFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The number of characters in the current fallback buffer that have not yet been processed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001A2F RID: 6703
		public abstract int Remaining { get; }

		/// <summary>Initializes all data and state information pertaining to this fallback buffer.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A30 RID: 6704 RVA: 0x0006335E File Offset: 0x0006155E
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00063368 File Offset: 0x00061568
		internal void InternalReset()
		{
			this.byteStart = null;
			this.Reset();
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00063378 File Offset: 0x00061578
		internal unsafe void InternalInitialize(byte* byteStart, char* charEnd)
		{
			this.byteStart = byteStart;
			this.charEnd = charEnd;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00063388 File Offset: 0x00061588
		internal unsafe virtual bool InternalFallback(byte[] bytes, byte* pBytes, ref char* chars)
		{
			if (this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				char* ptr = chars;
				bool flag = false;
				char nextChar;
				while ((nextChar = this.GetNextChar()) != '\0')
				{
					if (char.IsSurrogate(nextChar))
					{
						if (char.IsHighSurrogate(nextChar))
						{
							if (flag)
							{
								throw new ArgumentException("String contains invalid Unicode code points.");
							}
							flag = true;
						}
						else
						{
							if (!flag)
							{
								throw new ArgumentException("String contains invalid Unicode code points.");
							}
							flag = false;
						}
					}
					if (ptr >= this.charEnd)
					{
						return false;
					}
					*(ptr++) = nextChar;
				}
				if (flag)
				{
					throw new ArgumentException("String contains invalid Unicode code points.");
				}
				chars = ptr;
			}
			return true;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00063418 File Offset: 0x00061618
		internal unsafe virtual int InternalFallback(byte[] bytes, byte* pBytes)
		{
			if (!this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				return 0;
			}
			int num = 0;
			bool flag = false;
			char nextChar;
			while ((nextChar = this.GetNextChar()) != '\0')
			{
				if (char.IsSurrogate(nextChar))
				{
					if (char.IsHighSurrogate(nextChar))
					{
						if (flag)
						{
							throw new ArgumentException("String contains invalid Unicode code points.");
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							throw new ArgumentException("String contains invalid Unicode code points.");
						}
						flag = false;
					}
				}
				num++;
			}
			if (flag)
			{
				throw new ArgumentException("String contains invalid Unicode code points.");
			}
			return num;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00063498 File Offset: 0x00061698
		internal void ThrowLastBytesRecursive(byte[] bytesUnknown)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\\x{0:X2}", bytesUnknown[num]);
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new ArgumentException(SR.Format("Recursive fallback not allowed for bytes {0}.", stringBuilder.ToString()), "bytesUnknown");
		}

		// Token: 0x04000C66 RID: 3174
		internal unsafe byte* byteStart;

		// Token: 0x04000C67 RID: 3175
		internal unsafe char* charEnd;
	}
}
