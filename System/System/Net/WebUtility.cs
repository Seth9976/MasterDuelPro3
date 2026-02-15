using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net
{
	/// <summary>Provides methods for encoding and decoding URLs when processing Web requests. </summary>
	// Token: 0x020003C6 RID: 966
	public static class WebUtility
	{
		/// <summary>Converts a string to an HTML-encoded string.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="value">The string to encode.</param>
		// Token: 0x0600181D RID: 6173 RVA: 0x00066394 File Offset: 0x00064594
		public static string HtmlEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			if (WebUtility.IndexOfHtmlEncodingChars(value, 0) == -1)
			{
				return value;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			WebUtility.HtmlEncode(value, stringWriter);
			return stringWriter.ToString();
		}

		/// <summary>Converts a string into an HTML-encoded string, and returns the output as a <see cref="T:System.IO.TextWriter" /> stream of output.</summary>
		/// <param name="value">The string to encode.</param>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> output stream.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="output" /> parameter cannot be null if the <paramref name="value" /> parameter is not null.  </exception>
		// Token: 0x0600181E RID: 6174 RVA: 0x000663D0 File Offset: 0x000645D0
		public unsafe static void HtmlEncode(string value, TextWriter output)
		{
			if (value == null)
			{
				return;
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			int num = WebUtility.IndexOfHtmlEncodingChars(value, 0);
			if (num == -1)
			{
				output.Write(value);
				return;
			}
			UnicodeEncodingConformance htmlEncodeConformance = WebUtility.HtmlEncodeConformance;
			int i = value.Length - num;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				while (num-- > 0)
				{
					output.Write(*(ptr2++));
				}
				while (i > 0)
				{
					char c = *ptr2;
					if (c <= '>')
					{
						if (c <= '&')
						{
							if (c == '"')
							{
								output.Write("&quot;");
								goto IL_0172;
							}
							if (c == '&')
							{
								output.Write("&amp;");
								goto IL_0172;
							}
						}
						else
						{
							if (c == '\'')
							{
								output.Write("&#39;");
								goto IL_0172;
							}
							if (c == '<')
							{
								output.Write("&lt;");
								goto IL_0172;
							}
							if (c == '>')
							{
								output.Write("&gt;");
								goto IL_0172;
							}
						}
						output.Write(c);
					}
					else
					{
						int num2 = -1;
						if (c >= '\u00a0' && !char.IsSurrogate(c))
						{
							num2 = (int)c;
						}
						else if (htmlEncodeConformance == UnicodeEncodingConformance.Strict && char.IsSurrogate(c))
						{
							int nextUnicodeScalarValueFromUtf16Surrogate = WebUtility.GetNextUnicodeScalarValueFromUtf16Surrogate(ref ptr2, ref i);
							if (nextUnicodeScalarValueFromUtf16Surrogate >= 65536)
							{
								num2 = nextUnicodeScalarValueFromUtf16Surrogate;
							}
							else
							{
								c = (char)nextUnicodeScalarValueFromUtf16Surrogate;
							}
						}
						if (num2 >= 0)
						{
							output.Write("&#");
							output.Write(num2.ToString(NumberFormatInfo.InvariantInfo));
							output.Write(';');
						}
						else
						{
							output.Write(c);
						}
					}
					IL_0172:
					i--;
					ptr2++;
				}
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00066564 File Offset: 0x00064764
		private unsafe static int IndexOfHtmlEncodingChars(string s, int startPos)
		{
			UnicodeEncodingConformance htmlEncodeConformance = WebUtility.HtmlEncodeConformance;
			int i = s.Length - startPos;
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + startPos;
				while (i > 0)
				{
					char c = *ptr2;
					if (c <= '>')
					{
						if (c <= '&')
						{
							if (c != '"' && c != '&')
							{
								goto IL_008C;
							}
						}
						else if (c != '\'' && c != '<' && c != '>')
						{
							goto IL_008C;
						}
						return s.Length - i;
					}
					if (c >= '\u00a0')
					{
						return s.Length - i;
					}
					if (htmlEncodeConformance == UnicodeEncodingConformance.Strict && char.IsSurrogate(c))
					{
						return s.Length - i;
					}
					IL_008C:
					ptr2++;
					i--;
				}
			}
			return -1;
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x00066610 File Offset: 0x00064810
		private static UnicodeEncodingConformance HtmlEncodeConformance
		{
			get
			{
				if (WebUtility._htmlEncodeConformance != UnicodeEncodingConformance.Auto)
				{
					return WebUtility._htmlEncodeConformance;
				}
				UnicodeEncodingConformance unicodeEncodingConformance = UnicodeEncodingConformance.Strict;
				UnicodeEncodingConformance unicodeEncodingConformance2 = unicodeEncodingConformance;
				try
				{
					unicodeEncodingConformance2 = SettingsSectionInternal.Section.WebUtilityUnicodeEncodingConformance;
					if (unicodeEncodingConformance2 <= UnicodeEncodingConformance.Auto || unicodeEncodingConformance2 > UnicodeEncodingConformance.Compat)
					{
						unicodeEncodingConformance2 = unicodeEncodingConformance;
					}
				}
				catch (ConfigurationException)
				{
					unicodeEncodingConformance2 = unicodeEncodingConformance;
				}
				catch
				{
					return unicodeEncodingConformance;
				}
				WebUtility._htmlEncodeConformance = unicodeEncodingConformance2;
				return WebUtility._htmlEncodeConformance;
			}
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00066680 File Offset: 0x00064880
		private static string UrlDecodeInternal(string value, Encoding encoding)
		{
			if (value == null)
			{
				return null;
			}
			int length = value.Length;
			WebUtility.UrlDecoder urlDecoder = new WebUtility.UrlDecoder(length, encoding);
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				if (c == '+')
				{
					c = ' ';
					goto IL_0077;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_0077;
				}
				int num = WebUtility.HexToInt(value[i + 1]);
				int num2 = WebUtility.HexToInt(value[i + 2]);
				if (num < 0 || num2 < 0)
				{
					goto IL_0077;
				}
				byte b = (byte)((num << 4) | num2);
				i += 2;
				urlDecoder.AddByte(b);
				IL_0091:
				i++;
				continue;
				IL_0077:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_0091;
				}
				urlDecoder.AddChar(c);
				goto IL_0091;
			}
			return urlDecoder.GetString();
		}

		/// <summary>Converts a string that has been encoded for transmission in a URL into a decoded string.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A decoded string.</returns>
		/// <param name="encodedValue">A URL-encoded string to decode.</param>
		// Token: 0x06001822 RID: 6178 RVA: 0x0006672F File Offset: 0x0006492F
		public static string UrlDecode(string encodedValue)
		{
			if (encodedValue == null)
			{
				return null;
			}
			return WebUtility.UrlDecodeInternal(encodedValue, Encoding.UTF8);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00066744 File Offset: 0x00064944
		private unsafe static int GetNextUnicodeScalarValueFromUtf16Surrogate(ref char* pch, ref int charsRemaining)
		{
			if (charsRemaining <= 1)
			{
				return 65533;
			}
			char c = (char)(*pch);
			char c2 = (char)(*(pch + 2));
			if (char.IsSurrogatePair(c, c2))
			{
				pch += 2;
				charsRemaining--;
				return (int)((c - '\ud800') * 'Ѐ' + (c2 - '\udc00')) + 65536;
			}
			return 65533;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0006679C File Offset: 0x0006499C
		private static int HexToInt(char h)
		{
			if (h >= '0' && h <= '9')
			{
				return (int)(h - '0');
			}
			if (h >= 'a' && h <= 'f')
			{
				return (int)(h - 'a' + '\n');
			}
			if (h < 'A' || h > 'F')
			{
				return -1;
			}
			return (int)(h - 'A' + '\n');
		}

		// Token: 0x04000F41 RID: 3905
		private static readonly char[] _htmlEntityEndingChars = new char[] { ';', '&' };

		// Token: 0x04000F42 RID: 3906
		private static volatile UnicodeDecodingConformance _htmlDecodeConformance = UnicodeDecodingConformance.Auto;

		// Token: 0x04000F43 RID: 3907
		private static volatile UnicodeEncodingConformance _htmlEncodeConformance = UnicodeEncodingConformance.Auto;

		// Token: 0x020003C7 RID: 967
		private class UrlDecoder
		{
			// Token: 0x06001826 RID: 6182 RVA: 0x000667FC File Offset: 0x000649FC
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x06001827 RID: 6183 RVA: 0x0006684A File Offset: 0x00064A4A
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x06001828 RID: 6184 RVA: 0x0006686C File Offset: 0x00064A6C
			internal void AddChar(char ch)
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				char[] charBuffer = this._charBuffer;
				int numChars = this._numChars;
				this._numChars = numChars + 1;
				charBuffer[numChars] = ch;
			}

			// Token: 0x06001829 RID: 6185 RVA: 0x000668A4 File Offset: 0x00064AA4
			internal void AddByte(byte b)
			{
				if (this._byteBuffer == null)
				{
					this._byteBuffer = new byte[this._bufferSize];
				}
				byte[] byteBuffer = this._byteBuffer;
				int numBytes = this._numBytes;
				this._numBytes = numBytes + 1;
				byteBuffer[numBytes] = b;
			}

			// Token: 0x0600182A RID: 6186 RVA: 0x000668E3 File Offset: 0x00064AE3
			internal string GetString()
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				if (this._numChars > 0)
				{
					return new string(this._charBuffer, 0, this._numChars);
				}
				return string.Empty;
			}

			// Token: 0x04000F44 RID: 3908
			private int _bufferSize;

			// Token: 0x04000F45 RID: 3909
			private int _numChars;

			// Token: 0x04000F46 RID: 3910
			private char[] _charBuffer;

			// Token: 0x04000F47 RID: 3911
			private int _numBytes;

			// Token: 0x04000F48 RID: 3912
			private byte[] _byteBuffer;

			// Token: 0x04000F49 RID: 3913
			private Encoding _encoding;
		}
	}
}
