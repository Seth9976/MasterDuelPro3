using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000ED RID: 237
	internal ref struct __DTString
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00024726 File Offset: 0x00022926
		internal int Length
		{
			get
			{
				return this.Value.Length;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00024733 File Offset: 0x00022933
		internal __DTString(ReadOnlySpan<char> str, DateTimeFormatInfo dtfi, bool checkDigitToken)
		{
			this = new __DTString(str, dtfi);
			this.m_checkDigitToken = checkDigitToken;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00024744 File Offset: 0x00022944
		internal __DTString(ReadOnlySpan<char> str, DateTimeFormatInfo dtfi)
		{
			this.Index = -1;
			this.Value = str;
			this.m_current = '\0';
			if (dtfi != null)
			{
				this.m_info = dtfi.CompareInfo;
				this.m_checkDigitToken = (dtfi.FormatFlags & DateTimeFormatFlags.UseDigitPrefixInTokens) > DateTimeFormatFlags.None;
				return;
			}
			this.m_info = CultureInfo.CurrentCulture.CompareInfo;
			this.m_checkDigitToken = false;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0002479F File Offset: 0x0002299F
		internal CompareInfo CompareInfo
		{
			get
			{
				return this.m_info;
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000247A7 File Offset: 0x000229A7
		internal unsafe bool GetNext()
		{
			this.Index++;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
				return true;
			}
			return false;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000247E0 File Offset: 0x000229E0
		internal bool AtEnd()
		{
			return this.Index >= this.Length;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000247F3 File Offset: 0x000229F3
		internal unsafe bool Advance(int count)
		{
			this.Index += count;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
				return true;
			}
			return false;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002482C File Offset: 0x00022A2C
		internal unsafe void GetRegularToken(out TokenType tokenType, out int tokenValue, DateTimeFormatInfo dtfi)
		{
			tokenValue = 0;
			if (this.Index >= this.Length)
			{
				tokenType = TokenType.EndOfString;
				return;
			}
			tokenType = TokenType.UnknownToken;
			IL_0019:
			while (!DateTimeParse.IsDigit(this.m_current))
			{
				if (char.IsWhiteSpace(this.m_current))
				{
					for (;;)
					{
						int num = this.Index + 1;
						this.Index = num;
						if (num >= this.Length)
						{
							break;
						}
						this.m_current = (char)(*this.Value[this.Index]);
						if (!char.IsWhiteSpace(this.m_current))
						{
							goto IL_0019;
						}
					}
					tokenType = TokenType.EndOfString;
					return;
				}
				dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType, out tokenValue, ref this);
				return;
			}
			tokenValue = (int)(this.m_current - '0');
			int index = this.Index;
			for (;;)
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.Length)
				{
					break;
				}
				this.m_current = (char)(*this.Value[this.Index]);
				int num2 = (int)(this.m_current - '0');
				if (num2 < 0 || num2 > 9)
				{
					break;
				}
				tokenValue = tokenValue * 10 + num2;
			}
			if (this.Index - index > 8)
			{
				tokenType = TokenType.NumberToken;
				tokenValue = -1;
			}
			else if (this.Index - index < 3)
			{
				tokenType = TokenType.NumberToken;
			}
			else
			{
				tokenType = TokenType.YearNumberToken;
			}
			if (!this.m_checkDigitToken)
			{
				return;
			}
			int index2 = this.Index;
			char current = this.m_current;
			this.Index = index;
			this.m_current = (char)(*this.Value[this.Index]);
			TokenType tokenType2;
			int num3;
			if (dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType2, out num3, ref this))
			{
				tokenType = tokenType2;
				tokenValue = num3;
				return;
			}
			this.Index = index2;
			this.m_current = current;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000249B0 File Offset: 0x00022BB0
		internal TokenType GetSeparatorToken(DateTimeFormatInfo dtfi, out int indexBeforeSeparator, out char charBeforeSeparator)
		{
			indexBeforeSeparator = this.Index;
			charBeforeSeparator = this.m_current;
			if (!this.SkipWhiteSpaceCurrent())
			{
				return TokenType.SEP_End;
			}
			TokenType tokenType;
			if (!DateTimeParse.IsDigit(this.m_current))
			{
				int num;
				if (!dtfi.Tokenize(TokenType.SeparatorTokenMask, out tokenType, out num, ref this))
				{
					tokenType = TokenType.SEP_Space;
				}
			}
			else
			{
				tokenType = TokenType.SEP_Space;
			}
			return tokenType;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00024A09 File Offset: 0x00022C09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool MatchSpecifiedWord(string target)
		{
			return this.Index + target.Length <= this.Length && this.m_info.Compare(this.Value.Slice(this.Index, target.Length), target, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00024A4C File Offset: 0x00022C4C
		internal unsafe bool MatchSpecifiedWords(string target, bool checkWordBoundary, ref int matchLength)
		{
			int num = this.Value.Length - this.Index;
			matchLength = target.Length;
			if (matchLength > num || this.m_info.Compare(this.Value.Slice(this.Index, matchLength), target, CompareOptions.IgnoreCase) != 0)
			{
				int num2 = 0;
				int num3 = this.Index;
				int num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2);
				if (num4 == -1)
				{
					return false;
				}
				for (;;)
				{
					int num5 = num4 - num2;
					if (num3 >= this.Value.Length - num5)
					{
						break;
					}
					if (num5 == 0)
					{
						matchLength--;
					}
					else
					{
						if (!char.IsWhiteSpace((char)(*this.Value[num3 + num5])))
						{
							return false;
						}
						if (this.m_info.CompareOptionIgnoreCase(this.Value.Slice(num3, num5), target.AsSpan(num2, num5)) != 0)
						{
							return false;
						}
						num3 = num3 + num5 + 1;
					}
					num2 = num4 + 1;
					while (num3 < this.Value.Length && char.IsWhiteSpace((char)(*this.Value[num3])))
					{
						num3++;
						matchLength++;
					}
					if ((num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2)) < 0)
					{
						goto Block_8;
					}
				}
				return false;
				Block_8:
				if (num2 < target.Length)
				{
					int num6 = target.Length - num2;
					if (num3 > this.Value.Length - num6)
					{
						return false;
					}
					if (this.m_info.CompareOptionIgnoreCase(this.Value.Slice(num3, num6), target.AsSpan(num2, num6)) != 0)
					{
						return false;
					}
				}
			}
			if (checkWordBoundary)
			{
				int num7 = this.Index + matchLength;
				if (num7 < this.Value.Length && char.IsLetter((char)(*this.Value[num7])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00024BEC File Offset: 0x00022DEC
		internal bool Match(string str)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.Length)
			{
				return false;
			}
			if (str.Length > this.Value.Length - this.Index)
			{
				return false;
			}
			if (this.m_info.Compare(this.Value.Slice(this.Index, str.Length), str, CompareOptions.Ordinal) == 0)
			{
				this.Index += str.Length - 1;
				return true;
			}
			return false;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00024C74 File Offset: 0x00022E74
		internal unsafe bool Match(char ch)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.Length)
			{
				return false;
			}
			if (*this.Value[this.Index] == (ushort)ch)
			{
				this.m_current = ch;
				return true;
			}
			this.Index--;
			return false;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00024CCC File Offset: 0x00022ECC
		internal int MatchLongestWords(string[] words, ref int maxMatchStrLen)
		{
			int num = -1;
			for (int i = 0; i < words.Length; i++)
			{
				string text = words[i];
				int length = text.Length;
				if (this.MatchSpecifiedWords(text, false, ref length) && length > maxMatchStrLen)
				{
					maxMatchStrLen = length;
					num = i;
				}
			}
			return num;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00024D0C File Offset: 0x00022F0C
		internal unsafe int GetRepeatCount()
		{
			char c = (char)(*this.Value[this.Index]);
			int num = this.Index + 1;
			while (num < this.Length && *this.Value[num] == (ushort)c)
			{
				num++;
			}
			int num2 = num - this.Index;
			this.Index = num - 1;
			return num2;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00024D68 File Offset: 0x00022F68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe bool GetNextDigit()
		{
			int num = this.Index + 1;
			this.Index = num;
			return num < this.Length && DateTimeParse.IsDigit((char)(*this.Value[this.Index]));
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00024DA7 File Offset: 0x00022FA7
		internal unsafe char GetChar()
		{
			return (char)(*this.Value[this.Index]);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00024DBB File Offset: 0x00022FBB
		internal unsafe int GetDigit()
		{
			return (int)(*this.Value[this.Index] - 48);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00024DD2 File Offset: 0x00022FD2
		internal unsafe void SkipWhiteSpaces()
		{
			while (this.Index + 1 < this.Length)
			{
				if (!char.IsWhiteSpace((char)(*this.Value[this.Index + 1])))
				{
					return;
				}
				this.Index++;
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00024E10 File Offset: 0x00023010
		internal unsafe bool SkipWhiteSpaceCurrent()
		{
			if (this.Index >= this.Length)
			{
				return false;
			}
			if (!char.IsWhiteSpace(this.m_current))
			{
				return true;
			}
			do
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.Length)
				{
					return false;
				}
				this.m_current = (char)(*this.Value[this.Index]);
			}
			while (char.IsWhiteSpace(this.m_current));
			return true;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00024E80 File Offset: 0x00023080
		internal unsafe void TrimTail()
		{
			int num = this.Length - 1;
			while (num >= 0 && char.IsWhiteSpace((char)(*this.Value[num])))
			{
				num--;
			}
			this.Value = this.Value.Slice(0, num + 1);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00024ECC File Offset: 0x000230CC
		internal unsafe void RemoveTrailingInQuoteSpaces()
		{
			int num = this.Length - 1;
			if (num <= 1)
			{
				return;
			}
			char c = (char)(*this.Value[num]);
			if ((c == '\'' || c == '"') && char.IsWhiteSpace((char)(*this.Value[num - 1])))
			{
				num--;
				while (num >= 1 && char.IsWhiteSpace((char)(*this.Value[num - 1])))
				{
					num--;
				}
				Span<char> span = new char[num + 1];
				*span[num] = c;
				this.Value.Slice(0, num).CopyTo(span);
				this.Value = span;
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00024F74 File Offset: 0x00023174
		internal unsafe void RemoveLeadingInQuoteSpaces()
		{
			if (this.Length <= 2)
			{
				return;
			}
			int num = 0;
			char c = (char)(*this.Value[num]);
			if (c != '\'')
			{
				if (c != '"')
				{
					return;
				}
			}
			while (num + 1 < this.Length && char.IsWhiteSpace((char)(*this.Value[num + 1])))
			{
				num++;
			}
			if (num != 0)
			{
				Span<char> span = new char[this.Value.Length - num];
				*span[0] = c;
				this.Value.Slice(num + 1).CopyTo(span.Slice(1));
				this.Value = span;
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002501C File Offset: 0x0002321C
		internal unsafe DTSubString GetSubString()
		{
			DTSubString dtsubString = default(DTSubString);
			dtsubString.index = this.Index;
			dtsubString.s = this.Value;
			while (this.Index + dtsubString.length < this.Length)
			{
				char c = (char)(*this.Value[this.Index + dtsubString.length]);
				DTSubStringType dtsubStringType;
				if (c >= '0' && c <= '9')
				{
					dtsubStringType = DTSubStringType.Number;
				}
				else
				{
					dtsubStringType = DTSubStringType.Other;
				}
				if (dtsubString.length == 0)
				{
					dtsubString.type = dtsubStringType;
				}
				else if (dtsubString.type != dtsubStringType)
				{
					break;
				}
				dtsubString.length++;
				if (dtsubStringType != DTSubStringType.Number)
				{
					break;
				}
				if (dtsubString.length > 8)
				{
					dtsubString.type = DTSubStringType.Invalid;
					return dtsubString;
				}
				int num = (int)(c - '0');
				dtsubString.value = dtsubString.value * 10 + num;
			}
			if (dtsubString.length == 0)
			{
				dtsubString.type = DTSubStringType.End;
				return dtsubString;
			}
			return dtsubString;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000250F7 File Offset: 0x000232F7
		internal unsafe void ConsumeSubString(DTSubString sub)
		{
			this.Index = sub.index + sub.length;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
			}
		}

		// Token: 0x04000384 RID: 900
		internal ReadOnlySpan<char> Value;

		// Token: 0x04000385 RID: 901
		internal int Index;

		// Token: 0x04000386 RID: 902
		internal char m_current;

		// Token: 0x04000387 RID: 903
		private CompareInfo m_info;

		// Token: 0x04000388 RID: 904
		private bool m_checkDigitToken;

		// Token: 0x04000389 RID: 905
		private static readonly char[] WhiteSpaceChecks = new char[] { ' ', '\u00a0' };
	}
}
