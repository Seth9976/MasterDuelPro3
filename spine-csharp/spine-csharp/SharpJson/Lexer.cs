using System;
using System.Globalization;
using System.Text;

namespace SharpJson
{
	// Token: 0x02000004 RID: 4
	internal class Lexer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public bool hasError
		{
			get
			{
				return !this.success;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CB File Offset: 0x000002CB
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020D3 File Offset: 0x000002D3
		public int lineNumber { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020E4 File Offset: 0x000002E4
		public bool parseNumbersAsFloat { get; set; }

		// Token: 0x06000008 RID: 8 RVA: 0x000020ED File Offset: 0x000002ED
		public Lexer(string text)
		{
			this.Reset();
			this.json = text.ToCharArray();
			this.parseNumbersAsFloat = false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002125 File Offset: 0x00000325
		public void Reset()
		{
			this.index = 0;
			this.lineNumber = 1;
			this.success = true;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213C File Offset: 0x0000033C
		public string ParseString()
		{
			int idx = 0;
			StringBuilder builder = null;
			this.SkipWhiteSpaces();
			char[] array = this.json;
			int num = this.index;
			this.index = num + 1;
			char c = array[num];
			bool failed = false;
			bool complete = false;
			while (!complete && !failed && this.index != this.json.Length)
			{
				char[] array2 = this.json;
				num = this.index;
				this.index = num + 1;
				c = array2[num];
				if (c == '"')
				{
					complete = true;
					break;
				}
				if (c == '\\')
				{
					if (this.index == this.json.Length)
					{
						break;
					}
					char[] array3 = this.json;
					num = this.index;
					this.index = num + 1;
					c = array3[num];
					if (c <= '\\')
					{
						if (c != '"')
						{
							if (c != '/')
							{
								if (c == '\\')
								{
									this.stringBuffer[idx++] = '\\';
								}
							}
							else
							{
								this.stringBuffer[idx++] = '/';
							}
						}
						else
						{
							this.stringBuffer[idx++] = '"';
						}
					}
					else if (c <= 'f')
					{
						if (c != 'b')
						{
							if (c == 'f')
							{
								this.stringBuffer[idx++] = '\f';
							}
						}
						else
						{
							this.stringBuffer[idx++] = '\b';
						}
					}
					else if (c != 'n')
					{
						switch (c)
						{
						case 'r':
							this.stringBuffer[idx++] = '\r';
							break;
						case 't':
							this.stringBuffer[idx++] = '\t';
							break;
						case 'u':
							if (this.json.Length - this.index >= 4)
							{
								string hex = new string(this.json, this.index, 4);
								this.stringBuffer[idx++] = (char)Convert.ToInt32(hex, 16);
								this.index += 4;
							}
							else
							{
								failed = true;
							}
							break;
						}
					}
					else
					{
						this.stringBuffer[idx++] = '\n';
					}
				}
				else
				{
					this.stringBuffer[idx++] = c;
				}
				if (idx >= this.stringBuffer.Length)
				{
					if (builder == null)
					{
						builder = new StringBuilder();
					}
					builder.Append(this.stringBuffer, 0, idx);
					idx = 0;
				}
			}
			if (!complete)
			{
				this.success = false;
				return null;
			}
			if (builder != null)
			{
				return builder.ToString();
			}
			return new string(this.stringBuffer, 0, idx);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000237C File Offset: 0x0000057C
		private string GetNumberString()
		{
			this.SkipWhiteSpaces();
			int lastIndex = this.GetLastIndexOfNumber(this.index);
			int charLength = lastIndex - this.index + 1;
			string text = new string(this.json, this.index, charLength);
			this.index = lastIndex + 1;
			return text;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023C4 File Offset: 0x000005C4
		public float ParseFloatNumber()
		{
			float number;
			if (!float.TryParse(this.GetNumberString(), NumberStyles.Float, CultureInfo.InvariantCulture, out number))
			{
				return 0f;
			}
			return number;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023F4 File Offset: 0x000005F4
		public double ParseDoubleNumber()
		{
			double number;
			if (!double.TryParse(this.GetNumberString(), NumberStyles.Any, CultureInfo.InvariantCulture, out number))
			{
				return 0.0;
			}
			return number;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002428 File Offset: 0x00000628
		private int GetLastIndexOfNumber(int index)
		{
			int lastIndex;
			for (lastIndex = index; lastIndex < this.json.Length; lastIndex++)
			{
				char ch = this.json[lastIndex];
				if ((ch < '0' || ch > '9') && ch != '+' && ch != '-' && ch != '.' && ch != 'e' && ch != 'E')
				{
					break;
				}
			}
			return lastIndex - 1;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002478 File Offset: 0x00000678
		private void SkipWhiteSpaces()
		{
			while (this.index < this.json.Length)
			{
				if (this.json[this.index] == '\n')
				{
					int lineNumber = this.lineNumber;
					this.lineNumber = lineNumber + 1;
				}
				if (!char.IsWhiteSpace(this.json[this.index]))
				{
					break;
				}
				this.index++;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024DC File Offset: 0x000006DC
		public Lexer.Token LookAhead()
		{
			this.SkipWhiteSpaces();
			int savedIndex = this.index;
			return Lexer.NextToken(this.json, ref savedIndex);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002503 File Offset: 0x00000703
		public Lexer.Token NextToken()
		{
			this.SkipWhiteSpaces();
			return Lexer.NextToken(this.json, ref this.index);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000251C File Offset: 0x0000071C
		private static Lexer.Token NextToken(char[] json, ref int index)
		{
			if (index == json.Length)
			{
				return Lexer.Token.None;
			}
			int num = index;
			index = num + 1;
			char c = json[num];
			if (c <= '[')
			{
				switch (c)
				{
				case '"':
					return Lexer.Token.String;
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case '.':
				case '/':
					break;
				case ',':
					return Lexer.Token.Comma;
				case '-':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return Lexer.Token.Number;
				case ':':
					return Lexer.Token.Colon;
				default:
					if (c == '[')
					{
						return Lexer.Token.SquaredOpen;
					}
					break;
				}
			}
			else
			{
				if (c == ']')
				{
					return Lexer.Token.SquaredClose;
				}
				if (c == '{')
				{
					return Lexer.Token.CurlyOpen;
				}
				if (c == '}')
				{
					return Lexer.Token.CurlyClose;
				}
			}
			index--;
			int remainingLength = json.Length - index;
			if (remainingLength >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
			{
				index += 5;
				return Lexer.Token.False;
			}
			if (remainingLength >= 4 && json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
			{
				index += 4;
				return Lexer.Token.True;
			}
			if (remainingLength >= 4 && json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
			{
				index += 4;
				return Lexer.Token.Null;
			}
			return Lexer.Token.None;
		}

		// Token: 0x04000008 RID: 8
		private char[] json;

		// Token: 0x04000009 RID: 9
		private int index;

		// Token: 0x0400000A RID: 10
		private bool success = true;

		// Token: 0x0400000B RID: 11
		private char[] stringBuffer = new char[4096];

		// Token: 0x02000005 RID: 5
		public enum Token
		{
			// Token: 0x0400000D RID: 13
			None,
			// Token: 0x0400000E RID: 14
			Null,
			// Token: 0x0400000F RID: 15
			True,
			// Token: 0x04000010 RID: 16
			False,
			// Token: 0x04000011 RID: 17
			Colon,
			// Token: 0x04000012 RID: 18
			Comma,
			// Token: 0x04000013 RID: 19
			String,
			// Token: 0x04000014 RID: 20
			Number,
			// Token: 0x04000015 RID: 21
			CurlyOpen,
			// Token: 0x04000016 RID: 22
			CurlyClose,
			// Token: 0x04000017 RID: 23
			SquaredOpen,
			// Token: 0x04000018 RID: 24
			SquaredClose
		}
	}
}
