using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000376 RID: 886
	internal sealed class XPathScanner
	{
		// Token: 0x06002720 RID: 10016 RVA: 0x000D995C File Offset: 0x000D7B5C
		public XPathScanner(string xpathExpr)
		{
			if (xpathExpr == null)
			{
				throw XPathException.Create("'{0}' is an invalid expression.", string.Empty);
			}
			this._xpathExpr = xpathExpr;
			this.NextChar();
			this.NextLex();
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x000D99B1 File Offset: 0x000D7BB1
		public string SourceText
		{
			get
			{
				return this._xpathExpr;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x000D99B9 File Offset: 0x000D7BB9
		private char CurrentChar
		{
			get
			{
				return this._currentChar;
			}
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000D99C4 File Offset: 0x000D7BC4
		private bool NextChar()
		{
			if (this._xpathExprIndex < this._xpathExpr.Length)
			{
				string xpathExpr = this._xpathExpr;
				int xpathExprIndex = this._xpathExprIndex;
				this._xpathExprIndex = xpathExprIndex + 1;
				this._currentChar = xpathExpr[xpathExprIndex];
				return true;
			}
			this._currentChar = '\0';
			return false;
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x000D9A10 File Offset: 0x000D7C10
		public XPathScanner.LexKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x000D9A18 File Offset: 0x000D7C18
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x000D9A20 File Offset: 0x000D7C20
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x000D9A28 File Offset: 0x000D7C28
		public string StringValue
		{
			get
			{
				return this._stringValue;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x000D9A30 File Offset: 0x000D7C30
		public double NumberValue
		{
			get
			{
				return this._numberValue;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x000D9A38 File Offset: 0x000D7C38
		public bool CanBeFunction
		{
			get
			{
				return this._canBeFunction;
			}
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000D9A40 File Offset: 0x000D7C40
		private void SkipSpace()
		{
			while (this._xmlCharType.IsWhiteSpace(this.CurrentChar) && this.NextChar())
			{
			}
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000D9A60 File Offset: 0x000D7C60
		public bool NextLex()
		{
			this.SkipSpace();
			char currentChar = this.CurrentChar;
			if (currentChar <= '@')
			{
				if (currentChar == '\0')
				{
					this._kind = XPathScanner.LexKind.Eof;
					return false;
				}
				switch (currentChar)
				{
				case '!':
					this._kind = XPathScanner.LexKind.Bang;
					this.NextChar();
					if (this.CurrentChar == '=')
					{
						this._kind = XPathScanner.LexKind.Ne;
						this.NextChar();
						return true;
					}
					return true;
				case '"':
				case '\'':
					this._kind = XPathScanner.LexKind.String;
					this._stringValue = this.ScanString();
					return true;
				case '#':
				case '$':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '=':
				case '@':
					break;
				case '%':
				case '&':
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
				case ':':
				case ';':
				case '?':
					goto IL_021D;
				case '.':
					this._kind = XPathScanner.LexKind.Dot;
					this.NextChar();
					if (this.CurrentChar == '.')
					{
						this._kind = XPathScanner.LexKind.DotDot;
						this.NextChar();
						return true;
					}
					if (XmlCharType.IsDigit(this.CurrentChar))
					{
						this._kind = XPathScanner.LexKind.Number;
						this._numberValue = this.ScanFraction();
						return true;
					}
					return true;
				case '/':
					this._kind = XPathScanner.LexKind.Slash;
					this.NextChar();
					if (this.CurrentChar == '/')
					{
						this._kind = XPathScanner.LexKind.SlashSlash;
						this.NextChar();
						return true;
					}
					return true;
				case '<':
					this._kind = XPathScanner.LexKind.Lt;
					this.NextChar();
					if (this.CurrentChar == '=')
					{
						this._kind = XPathScanner.LexKind.Le;
						this.NextChar();
						return true;
					}
					return true;
				case '>':
					this._kind = XPathScanner.LexKind.Gt;
					this.NextChar();
					if (this.CurrentChar == '=')
					{
						this._kind = XPathScanner.LexKind.Ge;
						this.NextChar();
						return true;
					}
					return true;
				default:
					goto IL_021D;
				}
			}
			else if (currentChar != '[' && currentChar != ']' && currentChar != '|')
			{
				goto IL_021D;
			}
			this._kind = (XPathScanner.LexKind)Convert.ToInt32(this.CurrentChar, CultureInfo.InvariantCulture);
			this.NextChar();
			return true;
			IL_021D:
			if (XmlCharType.IsDigit(this.CurrentChar))
			{
				this._kind = XPathScanner.LexKind.Number;
				this._numberValue = this.ScanNumber();
			}
			else
			{
				if (!this._xmlCharType.IsStartNCNameSingleChar(this.CurrentChar))
				{
					throw XPathException.Create("'{0}' has an invalid token.", this.SourceText);
				}
				this._kind = XPathScanner.LexKind.Name;
				this._name = this.ScanName();
				this._prefix = string.Empty;
				if (this.CurrentChar == ':')
				{
					this.NextChar();
					if (this.CurrentChar == ':')
					{
						this.NextChar();
						this._kind = XPathScanner.LexKind.Axe;
					}
					else
					{
						this._prefix = this._name;
						if (this.CurrentChar == '*')
						{
							this.NextChar();
							this._name = "*";
						}
						else
						{
							if (!this._xmlCharType.IsStartNCNameSingleChar(this.CurrentChar))
							{
								throw XPathException.Create("'{0}' has an invalid qualified name.", this.SourceText);
							}
							this._name = this.ScanName();
						}
					}
				}
				else
				{
					this.SkipSpace();
					if (this.CurrentChar == ':')
					{
						this.NextChar();
						if (this.CurrentChar != ':')
						{
							throw XPathException.Create("'{0}' has an invalid qualified name.", this.SourceText);
						}
						this.NextChar();
						this._kind = XPathScanner.LexKind.Axe;
					}
				}
				this.SkipSpace();
				this._canBeFunction = this.CurrentChar == '(';
			}
			return true;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000D9DE0 File Offset: 0x000D7FE0
		private double ScanNumber()
		{
			int num = this._xpathExprIndex - 1;
			int num2 = 0;
			while (XmlCharType.IsDigit(this.CurrentChar))
			{
				this.NextChar();
				num2++;
			}
			if (this.CurrentChar == '.')
			{
				this.NextChar();
				num2++;
				while (XmlCharType.IsDigit(this.CurrentChar))
				{
					this.NextChar();
					num2++;
				}
			}
			return XmlConvert.ToXPathDouble(this._xpathExpr.Substring(num, num2));
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000D9E54 File Offset: 0x000D8054
		private double ScanFraction()
		{
			int num = this._xpathExprIndex - 2;
			int num2 = 1;
			while (XmlCharType.IsDigit(this.CurrentChar))
			{
				this.NextChar();
				num2++;
			}
			return XmlConvert.ToXPathDouble(this._xpathExpr.Substring(num, num2));
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000D9E98 File Offset: 0x000D8098
		private string ScanString()
		{
			char currentChar = this.CurrentChar;
			this.NextChar();
			int num = this._xpathExprIndex - 1;
			int num2 = 0;
			while (this.CurrentChar != currentChar)
			{
				if (!this.NextChar())
				{
					throw XPathException.Create("This is an unclosed string.");
				}
				num2++;
			}
			this.NextChar();
			return this._xpathExpr.Substring(num, num2);
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000D9EF4 File Offset: 0x000D80F4
		private string ScanName()
		{
			int num = this._xpathExprIndex - 1;
			int num2 = 0;
			while (this._xmlCharType.IsNCNameSingleChar(this.CurrentChar))
			{
				this.NextChar();
				num2++;
			}
			return this._xpathExpr.Substring(num, num2);
		}

		// Token: 0x040012AD RID: 4781
		private string _xpathExpr;

		// Token: 0x040012AE RID: 4782
		private int _xpathExprIndex;

		// Token: 0x040012AF RID: 4783
		private XPathScanner.LexKind _kind;

		// Token: 0x040012B0 RID: 4784
		private char _currentChar;

		// Token: 0x040012B1 RID: 4785
		private string _name;

		// Token: 0x040012B2 RID: 4786
		private string _prefix;

		// Token: 0x040012B3 RID: 4787
		private string _stringValue;

		// Token: 0x040012B4 RID: 4788
		private double _numberValue = double.NaN;

		// Token: 0x040012B5 RID: 4789
		private bool _canBeFunction;

		// Token: 0x040012B6 RID: 4790
		private XmlCharType _xmlCharType = XmlCharType.Instance;

		// Token: 0x02000377 RID: 887
		public enum LexKind
		{
			// Token: 0x040012B8 RID: 4792
			Comma = 44,
			// Token: 0x040012B9 RID: 4793
			Slash = 47,
			// Token: 0x040012BA RID: 4794
			At = 64,
			// Token: 0x040012BB RID: 4795
			Dot = 46,
			// Token: 0x040012BC RID: 4796
			LParens = 40,
			// Token: 0x040012BD RID: 4797
			RParens,
			// Token: 0x040012BE RID: 4798
			LBracket = 91,
			// Token: 0x040012BF RID: 4799
			RBracket = 93,
			// Token: 0x040012C0 RID: 4800
			Star = 42,
			// Token: 0x040012C1 RID: 4801
			Plus,
			// Token: 0x040012C2 RID: 4802
			Minus = 45,
			// Token: 0x040012C3 RID: 4803
			Eq = 61,
			// Token: 0x040012C4 RID: 4804
			Lt = 60,
			// Token: 0x040012C5 RID: 4805
			Gt = 62,
			// Token: 0x040012C6 RID: 4806
			Bang = 33,
			// Token: 0x040012C7 RID: 4807
			Dollar = 36,
			// Token: 0x040012C8 RID: 4808
			Apos = 39,
			// Token: 0x040012C9 RID: 4809
			Quote = 34,
			// Token: 0x040012CA RID: 4810
			Union = 124,
			// Token: 0x040012CB RID: 4811
			Ne = 78,
			// Token: 0x040012CC RID: 4812
			Le = 76,
			// Token: 0x040012CD RID: 4813
			Ge = 71,
			// Token: 0x040012CE RID: 4814
			And = 65,
			// Token: 0x040012CF RID: 4815
			Or = 79,
			// Token: 0x040012D0 RID: 4816
			DotDot = 68,
			// Token: 0x040012D1 RID: 4817
			SlashSlash = 83,
			// Token: 0x040012D2 RID: 4818
			Name = 110,
			// Token: 0x040012D3 RID: 4819
			String = 115,
			// Token: 0x040012D4 RID: 4820
			Number = 100,
			// Token: 0x040012D5 RID: 4821
			Axe = 97,
			// Token: 0x040012D6 RID: 4822
			Eof = 69
		}
	}
}
