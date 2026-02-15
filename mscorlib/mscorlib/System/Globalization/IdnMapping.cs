using System;
using System.Text;

namespace System.Globalization
{
	/// <summary>Supports the use of non-ASCII characters for Internet domain names. This class cannot be inherited.</summary>
	// Token: 0x020006CD RID: 1741
	public sealed class IdnMapping
	{
		/// <summary>Indicates whether a specified object and the current <see cref="T:System.Globalization.IdnMapping" /> object are equal.</summary>
		/// <returns>true if the object specified by the <paramref name="obj" /> parameter is derived from <see cref="T:System.Globalization.IdnMapping" /> and its <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties are equal; otherwise, false. </returns>
		/// <param name="obj">The object to compare to the current object.</param>
		// Token: 0x0600374F RID: 14159 RVA: 0x000D9EA8 File Offset: 0x000D80A8
		public override bool Equals(object obj)
		{
			IdnMapping idnMapping = obj as IdnMapping;
			return idnMapping != null && this.allow_unassigned == idnMapping.allow_unassigned && this.use_std3 == idnMapping.use_std3;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>One of four 32-bit signed constants derived from the properties of an <see cref="T:System.Globalization.IdnMapping" /> object.  The return value has no special meaning and is not suitable for use in a hash code algorithm.</returns>
		// Token: 0x06003750 RID: 14160 RVA: 0x000D9EDD File Offset: 0x000D80DD
		public override int GetHashCode()
		{
			return (this.allow_unassigned ? 2 : 0) + (this.use_std3 ? 1 : 0);
		}

		/// <summary>Encodes a string of domain name labels that consist of Unicode characters to a string of displayable Unicode characters in the US-ASCII character range. The string is formatted according to the IDNA standard.</summary>
		/// <returns>The equivalent of the string specified by the <paramref name="unicode" /> parameter, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06003751 RID: 14161 RVA: 0x000D9EF8 File Offset: 0x000D80F8
		public string GetAscii(string unicode)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, 0, unicode.Length);
		}

		/// <summary>Encodes the specified number of characters in a  substring of domain name labels that include Unicode characters outside the US-ASCII character range. The substring is converted to a string of displayable Unicode characters in the US-ASCII character range and is formatted according to the IDNA standard. </summary>
		/// <returns>The equivalent of the substring specified by the <paramref name="unicode" />, <paramref name="index" />, and <paramref name="count" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring.</param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by  <paramref name="index" /> in the <paramref name="unicode" /> string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" />.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06003752 RID: 14162 RVA: 0x000D9F18 File Offset: 0x000D8118
		public string GetAscii(string unicode, int index, int count)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > unicode.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument unicode string");
			}
			return this.Convert(unicode, index, count, true);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000D9F68 File Offset: 0x000D8168
		private string Convert(string input, int index, int count, bool toAscii)
		{
			string text = input.Substring(index, count);
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] >= '\u0080')
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					break;
				}
			}
			string[] array = text.Split(new char[] { '.', '。', '．', '｡' });
			int num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Length != 0 || j + 1 != array.Length)
				{
					if (toAscii)
					{
						array[j] = this.ToAscii(array[j], num);
					}
					else
					{
						array[j] = this.ToUnicode(array[j], num);
					}
				}
				num += array[j].Length;
			}
			return string.Join(".", array);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000DA028 File Offset: 0x000D8228
		private string ToAscii(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < ' ' || s[i] == '\u007f')
				{
					throw new ArgumentException(string.Format("Not allowed character was found, at {0}", offset + i));
				}
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (this.use_std3)
			{
				this.VerifyStd3AsciiRules(s, offset);
			}
			int j = 0;
			while (j < s.Length)
			{
				if (s[j] >= '\u0080')
				{
					if (s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(string.Format("The input string must not start with ACE (xn--), at {0}", offset + j));
					}
					s = this.puny.Encode(s, offset);
					s = "xn--" + s;
					break;
				}
				else
				{
					j++;
				}
			}
			this.VerifyLength(s, offset);
			return s;
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000DA10A File Offset: 0x000D830A
		private void VerifyLength(string s, int offset)
		{
			if (s.Length == 0)
			{
				throw new ArgumentException(string.Format("A label in the input string resulted in an invalid zero-length string, at {0}", offset));
			}
			if (s.Length > 63)
			{
				throw new ArgumentException(string.Format("A label in the input string exceeded the length in ASCII representation, at {0}", offset));
			}
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000DA14C File Offset: 0x000D834C
		private string NamePrep(string s, int offset)
		{
			s = s.Normalize(NormalizationForm.FormKC);
			this.VerifyProhibitedCharacters(s, offset);
			if (!this.allow_unassigned)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (char.GetUnicodeCategory(s, i) == UnicodeCategory.OtherNotAssigned)
					{
						throw new ArgumentException(string.Format("Use of unassigned Unicode characer is prohibited in this IdnMapping, at {0}", offset + i));
					}
				}
			}
			return s;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000DA1A8 File Offset: 0x000D83A8
		private void VerifyProhibitedCharacters(string s, int offset)
		{
			int i = 0;
			while (i < s.Length)
			{
				switch (char.GetUnicodeCategory(s, i))
				{
				case UnicodeCategory.SpaceSeparator:
					if (s[i] >= '\u0080')
					{
						goto IL_0111;
					}
					break;
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Format:
					goto IL_006E;
				case UnicodeCategory.Control:
					if (s[i] == '\0' || s[i] >= '\u0080')
					{
						goto IL_0111;
					}
					break;
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_0111;
				default:
					goto IL_006E;
				}
				IL_0129:
				i++;
				continue;
				IL_0111:
				throw new ArgumentException(string.Format("Not allowed character was in the input string, at {0}", offset + i));
				IL_006E:
				char c = s[i];
				if (('\ufddf' <= c && c <= '\ufdef') || (c & '\uffff') == '\ufffe' || ('\ufff9' <= c && c <= '\ufffd') || ('⿰' <= c && c <= '⿻') || ('\u202a' <= c && c <= '\u202e') || ('\u206a' <= c && c <= '\u206f'))
				{
					goto IL_0111;
				}
				if (c <= '\u200e')
				{
					if (c != '\u0340' && c != '\u0341' && c != '\u200e')
					{
						goto IL_0129;
					}
					goto IL_0111;
				}
				else
				{
					if (c == '\u200f' || c == '\u2028' || c == '\u2029')
					{
						goto IL_0111;
					}
					goto IL_0129;
				}
			}
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000DA2F0 File Offset: 0x000D84F0
		private void VerifyStd3AsciiRules(string s, int offset)
		{
			if (s.Length > 0 && s[0] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at head of a sequence in STD3 mode, found at {0}", offset));
			}
			if (s.Length > 0 && s[s.Length - 1] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at tail of a sequence in STD3 mode, found at {0}", offset + s.Length - 1));
			}
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c != '-' && (c <= '/' || (':' <= c && c <= '@') || ('[' <= c && c <= '`') || ('{' <= c && c <= '\u007f')))
				{
					throw new ArgumentException(string.Format("Not allowed character in STD3 mode, found at {0}", offset + i));
				}
			}
		}

		/// <summary>Decodes a string of one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters. </summary>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> parameter.</returns>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06003759 RID: 14169 RVA: 0x000DA3BA File Offset: 0x000D85BA
		public string GetUnicode(string ascii)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, 0, ascii.Length);
		}

		/// <summary>Decodes a substring of a specified length that contains one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters. </summary>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" />, <paramref name="index" />, and <paramref name="count" /> parameters.</returns>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
		/// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring. </param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by <paramref name="index" /> in the <paramref name="ascii" /> string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" />.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600375A RID: 14170 RVA: 0x000DA3D8 File Offset: 0x000D85D8
		public string GetUnicode(string ascii, int index, int count)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > ascii.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument ascii string");
			}
			return this.Convert(ascii, index, count, false);
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000DA428 File Offset: 0x000D8628
		private string ToUnicode(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (!s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
			{
				return s;
			}
			s = s.ToLower(CultureInfo.InvariantCulture);
			string text = s;
			s = s.Substring(4);
			s = this.puny.Decode(s, offset);
			string text2 = s;
			s = this.ToAscii(s, offset);
			if (string.Compare(text, s, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(string.Format("ToUnicode() failed at verifying the result, at label part from {0}", offset));
			}
			return text2;
		}

		// Token: 0x04001DC9 RID: 7625
		private bool allow_unassigned;

		// Token: 0x04001DCA RID: 7626
		private bool use_std3;

		// Token: 0x04001DCB RID: 7627
		private Punycode puny = new Punycode();
	}
}
