using System;

namespace System.Net
{
	// Token: 0x020003DE RID: 990
	internal class CookieTokenizer
	{
		// Token: 0x0600189A RID: 6298 RVA: 0x00068A74 File Offset: 0x00066C74
		internal CookieTokenizer(string tokenStream)
		{
			this.m_length = tokenStream.Length;
			this.m_tokenStream = tokenStream;
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x00068A8F File Offset: 0x00066C8F
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x00068A97 File Offset: 0x00066C97
		internal bool EndOfCookie
		{
			get
			{
				return this.m_eofCookie;
			}
			set
			{
				this.m_eofCookie = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00068AA0 File Offset: 0x00066CA0
		internal bool Eof
		{
			get
			{
				return this.m_index >= this.m_length;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00068AB3 File Offset: 0x00066CB3
		// (set) Token: 0x0600189F RID: 6303 RVA: 0x00068ABB File Offset: 0x00066CBB
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x00068AC4 File Offset: 0x00066CC4
		// (set) Token: 0x060018A1 RID: 6305 RVA: 0x00068ACC File Offset: 0x00066CCC
		internal bool Quoted
		{
			get
			{
				return this.m_quoted;
			}
			set
			{
				this.m_quoted = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x00068AD5 File Offset: 0x00066CD5
		// (set) Token: 0x060018A3 RID: 6307 RVA: 0x00068ADD File Offset: 0x00066CDD
		internal CookieToken Token
		{
			get
			{
				return this.m_token;
			}
			set
			{
				this.m_token = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x00068AE6 File Offset: 0x00066CE6
		// (set) Token: 0x060018A5 RID: 6309 RVA: 0x00068AEE File Offset: 0x00066CEE
		internal string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00068AF8 File Offset: 0x00066CF8
		internal string Extract()
		{
			string text = string.Empty;
			if (this.m_tokenLength != 0)
			{
				text = this.m_tokenStream.Substring(this.m_start, this.m_tokenLength);
				if (!this.Quoted)
				{
					text = text.Trim();
				}
			}
			return text;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00068B3C File Offset: 0x00066D3C
		internal CookieToken FindNext(bool ignoreComma, bool ignoreEquals)
		{
			this.m_tokenLength = 0;
			this.m_start = this.m_index;
			while (this.m_index < this.m_length && char.IsWhiteSpace(this.m_tokenStream[this.m_index]))
			{
				this.m_index++;
				this.m_start++;
			}
			CookieToken cookieToken = CookieToken.End;
			int num = 1;
			if (!this.Eof)
			{
				if (this.m_tokenStream[this.m_index] == '"')
				{
					this.Quoted = true;
					this.m_index++;
					bool flag = false;
					while (this.m_index < this.m_length)
					{
						char c = this.m_tokenStream[this.m_index];
						if (!flag && c == '"')
						{
							break;
						}
						if (flag)
						{
							flag = false;
						}
						else if (c == '\\')
						{
							flag = true;
						}
						this.m_index++;
					}
					if (this.m_index < this.m_length)
					{
						this.m_index++;
					}
					this.m_tokenLength = this.m_index - this.m_start;
					num = 0;
					ignoreComma = false;
				}
				while (this.m_index < this.m_length && this.m_tokenStream[this.m_index] != ';' && (ignoreEquals || this.m_tokenStream[this.m_index] != '=') && (ignoreComma || this.m_tokenStream[this.m_index] != ','))
				{
					if (this.m_tokenStream[this.m_index] == ',')
					{
						this.m_start = this.m_index + 1;
						this.m_tokenLength = -1;
						ignoreComma = false;
					}
					this.m_index++;
					this.m_tokenLength += num;
				}
				if (!this.Eof)
				{
					char c2 = this.m_tokenStream[this.m_index];
					if (c2 != ';')
					{
						if (c2 != '=')
						{
							cookieToken = CookieToken.EndCookie;
						}
						else
						{
							cookieToken = CookieToken.Equals;
						}
					}
					else
					{
						cookieToken = CookieToken.EndToken;
					}
					this.m_index++;
				}
			}
			return cookieToken;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00068D40 File Offset: 0x00066F40
		internal CookieToken Next(bool first, bool parseResponseCookies)
		{
			this.Reset();
			CookieToken cookieToken = this.FindNext(false, false);
			if (cookieToken == CookieToken.EndCookie)
			{
				this.EndOfCookie = true;
			}
			if (cookieToken == CookieToken.End || cookieToken == CookieToken.EndCookie)
			{
				if ((this.Name = this.Extract()).Length != 0)
				{
					this.Token = this.TokenFromName(parseResponseCookies);
					return CookieToken.Attribute;
				}
				return cookieToken;
			}
			else
			{
				this.Name = this.Extract();
				if (first)
				{
					this.Token = CookieToken.CookieName;
				}
				else
				{
					this.Token = this.TokenFromName(parseResponseCookies);
				}
				if (cookieToken == CookieToken.Equals)
				{
					cookieToken = this.FindNext(!first && this.Token == CookieToken.Expires, true);
					if (cookieToken == CookieToken.EndCookie)
					{
						this.EndOfCookie = true;
					}
					this.Value = this.Extract();
					return CookieToken.NameValuePair;
				}
				return CookieToken.Attribute;
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00068DF2 File Offset: 0x00066FF2
		internal void Reset()
		{
			this.m_eofCookie = false;
			this.m_name = string.Empty;
			this.m_quoted = false;
			this.m_start = this.m_index;
			this.m_token = CookieToken.Nothing;
			this.m_tokenLength = 0;
			this.m_value = string.Empty;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00068E34 File Offset: 0x00067034
		internal CookieToken TokenFromName(bool parseResponseCookies)
		{
			if (!parseResponseCookies)
			{
				for (int i = 0; i < CookieTokenizer.RecognizedServerAttributes.Length; i++)
				{
					if (CookieTokenizer.RecognizedServerAttributes[i].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedServerAttributes[i].Token;
					}
				}
			}
			else
			{
				for (int j = 0; j < CookieTokenizer.RecognizedAttributes.Length; j++)
				{
					if (CookieTokenizer.RecognizedAttributes[j].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedAttributes[j].Token;
					}
				}
			}
			return CookieToken.Unknown;
		}

		// Token: 0x04000FB3 RID: 4019
		private bool m_eofCookie;

		// Token: 0x04000FB4 RID: 4020
		private int m_index;

		// Token: 0x04000FB5 RID: 4021
		private int m_length;

		// Token: 0x04000FB6 RID: 4022
		private string m_name;

		// Token: 0x04000FB7 RID: 4023
		private bool m_quoted;

		// Token: 0x04000FB8 RID: 4024
		private int m_start;

		// Token: 0x04000FB9 RID: 4025
		private CookieToken m_token;

		// Token: 0x04000FBA RID: 4026
		private int m_tokenLength;

		// Token: 0x04000FBB RID: 4027
		private string m_tokenStream;

		// Token: 0x04000FBC RID: 4028
		private string m_value;

		// Token: 0x04000FBD RID: 4029
		private static CookieTokenizer.RecognizedAttribute[] RecognizedAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("Max-Age", CookieToken.MaxAge),
			new CookieTokenizer.RecognizedAttribute("Expires", CookieToken.Expires),
			new CookieTokenizer.RecognizedAttribute("Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("Secure", CookieToken.Secure),
			new CookieTokenizer.RecognizedAttribute("Discard", CookieToken.Discard),
			new CookieTokenizer.RecognizedAttribute("Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("Comment", CookieToken.Comment),
			new CookieTokenizer.RecognizedAttribute("CommentURL", CookieToken.CommentUrl),
			new CookieTokenizer.RecognizedAttribute("HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x04000FBE RID: 4030
		private static CookieTokenizer.RecognizedAttribute[] RecognizedServerAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("$Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("$Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("$Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("$Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("$HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x020003DF RID: 991
		private struct RecognizedAttribute
		{
			// Token: 0x060018AC RID: 6316 RVA: 0x00069014 File Offset: 0x00067214
			internal RecognizedAttribute(string name, CookieToken token)
			{
				this.m_name = name;
				this.m_token = token;
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x060018AD RID: 6317 RVA: 0x00069024 File Offset: 0x00067224
			internal CookieToken Token
			{
				get
				{
					return this.m_token;
				}
			}

			// Token: 0x060018AE RID: 6318 RVA: 0x0006902C File Offset: 0x0006722C
			internal bool IsEqualTo(string value)
			{
				return string.Compare(this.m_name, value, StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x04000FBF RID: 4031
			private string m_name;

			// Token: 0x04000FC0 RID: 4032
			private CookieToken m_token;
		}
	}
}
