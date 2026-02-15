using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020005CC RID: 1484
	internal class StyleSyntaxTokenizer
	{
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x000A61E4 File Offset: 0x000A43E4
		public StyleSyntaxToken current
		{
			get
			{
				bool flag = this.m_CurrentTokenIndex < 0 || this.m_CurrentTokenIndex >= this.m_Tokens.Count;
				StyleSyntaxToken styleSyntaxToken;
				if (flag)
				{
					styleSyntaxToken = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
				}
				else
				{
					styleSyntaxToken = this.m_Tokens[this.m_CurrentTokenIndex];
				}
				return styleSyntaxToken;
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000A6238 File Offset: 0x000A4438
		public StyleSyntaxToken MoveNext()
		{
			StyleSyntaxToken token = this.current;
			bool flag = token.type == StyleSyntaxTokenType.Unknown;
			StyleSyntaxToken styleSyntaxToken;
			if (flag)
			{
				styleSyntaxToken = token;
			}
			else
			{
				this.m_CurrentTokenIndex++;
				token = this.current;
				bool flag2 = this.m_CurrentTokenIndex == this.m_Tokens.Count;
				if (flag2)
				{
					this.m_CurrentTokenIndex = -1;
				}
				styleSyntaxToken = token;
			}
			return styleSyntaxToken;
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000A6298 File Offset: 0x000A4498
		public StyleSyntaxToken PeekNext()
		{
			int nextIndex = this.m_CurrentTokenIndex + 1;
			bool flag = this.m_CurrentTokenIndex < 0 || nextIndex >= this.m_Tokens.Count;
			StyleSyntaxToken styleSyntaxToken;
			if (flag)
			{
				styleSyntaxToken = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
			}
			else
			{
				styleSyntaxToken = this.m_Tokens[nextIndex];
			}
			return styleSyntaxToken;
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x000A62EC File Offset: 0x000A44EC
		public void Tokenize(string syntax)
		{
			this.m_Tokens.Clear();
			this.m_CurrentTokenIndex = 0;
			syntax = syntax.Trim(' ').ToLowerInvariant();
			int i = 0;
			while (i < syntax.Length)
			{
				char c = syntax[i];
				char c2 = c;
				char c3 = c2;
				if (c3 <= '?')
				{
					switch (c3)
					{
					case ' ':
						i = StyleSyntaxTokenizer.GlobCharacter(syntax, i, ' ');
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Space));
						break;
					case '!':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.ExclamationPoint));
						break;
					case '"':
					case '$':
					case '%':
					case '(':
					case ')':
						goto IL_02E1;
					case '#':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.HashMark));
						break;
					case '&':
					{
						bool flag = !StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '&');
						if (flag)
						{
							string nextChar = ((i + 1 < syntax.Length) ? syntax[i + 1].ToString() : "EOF");
							Debug.LogAssertionFormat("Expected '&' got '{0}'", new object[] { nextChar });
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
						}
						else
						{
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleAmpersand));
							i++;
						}
						break;
					}
					case '\'':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleQuote));
						break;
					case '*':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Asterisk));
						break;
					case '+':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Plus));
						break;
					case ',':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Comma));
						break;
					default:
						switch (c3)
						{
						case '<':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.LessThan));
							break;
						case '=':
							goto IL_02E1;
						case '>':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.GreaterThan));
							break;
						case '?':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.QuestionMark));
							break;
						default:
							goto IL_02E1;
						}
						break;
					}
				}
				else if (c3 != '[')
				{
					if (c3 != ']')
					{
						switch (c3)
						{
						case '{':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBrace));
							break;
						case '|':
						{
							bool flag2 = StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '|');
							if (flag2)
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleBar));
								i++;
							}
							else
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleBar));
							}
							break;
						}
						case '}':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBrace));
							break;
						default:
							goto IL_02E1;
						}
					}
					else
					{
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBracket));
					}
				}
				else
				{
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBracket));
				}
				IL_03BC:
				i++;
				continue;
				IL_02E1:
				bool flag3 = char.IsNumber(c);
				if (flag3)
				{
					int subStrStart = i;
					int subStrLength = 1;
					while (StyleSyntaxTokenizer.IsNextNumber(syntax, i))
					{
						i++;
						subStrLength++;
					}
					string tokenText = syntax.Substring(subStrStart, subStrLength);
					int tokenNumber = int.Parse(tokenText);
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Number, tokenNumber));
				}
				else
				{
					bool flag4 = char.IsLetter(c);
					if (flag4)
					{
						int subStrStart2 = i;
						int subStrLength2 = 1;
						while (StyleSyntaxTokenizer.IsNextLetterOrDash(syntax, i))
						{
							i++;
							subStrLength2++;
						}
						string tokenText2 = syntax.Substring(subStrStart2, subStrLength2);
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.String, tokenText2));
					}
					else
					{
						Debug.LogAssertionFormat("Expected letter or number got '{0}'", new object[] { c });
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
					}
				}
				goto IL_03BC;
			}
			this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.End));
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000A66E0 File Offset: 0x000A48E0
		private static bool IsNextCharacter(string s, int index, char c)
		{
			return index + 1 < s.Length && s[index + 1] == c;
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000A670C File Offset: 0x000A490C
		private static bool IsNextLetterOrDash(string s, int index)
		{
			return index + 1 < s.Length && (char.IsLetter(s[index + 1]) || s[index + 1] == '-');
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000A674C File Offset: 0x000A494C
		private static bool IsNextNumber(string s, int index)
		{
			return index + 1 < s.Length && char.IsNumber(s[index + 1]);
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000A677C File Offset: 0x000A497C
		private static int GlobCharacter(string s, int index, char c)
		{
			while (StyleSyntaxTokenizer.IsNextCharacter(s, index, c))
			{
				index++;
			}
			return index;
		}

		// Token: 0x04001567 RID: 5479
		private List<StyleSyntaxToken> m_Tokens = new List<StyleSyntaxToken>();

		// Token: 0x04001568 RID: 5480
		private int m_CurrentTokenIndex = -1;
	}
}
