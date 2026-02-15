using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000143 RID: 323
	internal sealed class RegexParser
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x00027290 File Offset: 0x00025490
		public static RegexTree Parse(string re, RegexOptions op)
		{
			RegexParser regexParser = new RegexParser(((op & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
			regexParser._options = op;
			regexParser.SetPattern(re);
			regexParser.CountCaptures();
			regexParser.Reset(op);
			RegexNode regexNode = regexParser.ScanRegex();
			string[] array;
			if (regexParser._capnamelist == null)
			{
				array = null;
			}
			else
			{
				array = regexParser._capnamelist.ToArray();
			}
			return new RegexTree(regexNode, regexParser._caps, regexParser._capnumlist, regexParser._captop, regexParser._capnames, array, op);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00027310 File Offset: 0x00025510
		public static RegexReplacement ParseReplacement(string rep, Hashtable caps, int capsize, Hashtable capnames, RegexOptions op)
		{
			RegexParser regexParser = new RegexParser(((op & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
			regexParser._options = op;
			regexParser.NoteCaptures(caps, capsize, capnames);
			regexParser.SetPattern(rep);
			RegexNode regexNode = regexParser.ScanReplacement();
			return new RegexReplacement(rep, regexNode, caps);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00027360 File Offset: 0x00025560
		public static string Escape(string input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (RegexParser.IsMetachar(input[i]))
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					char c = input[i];
					stringBuilder.Append(input, 0, i);
					do
					{
						stringBuilder.Append('\\');
						switch (c)
						{
						case '\t':
							c = 't';
							break;
						case '\n':
							c = 'n';
							break;
						case '\f':
							c = 'f';
							break;
						case '\r':
							c = 'r';
							break;
						}
						stringBuilder.Append(c);
						i++;
						int num = i;
						while (i < input.Length)
						{
							c = input[i];
							if (RegexParser.IsMetachar(c))
							{
								break;
							}
							i++;
						}
						stringBuilder.Append(input, num, i - num);
					}
					while (i < input.Length);
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
			}
			return input;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00027433 File Offset: 0x00025633
		private RegexParser(CultureInfo culture)
		{
			this._culture = culture;
			this._optionsStack = new List<RegexOptions>();
			this._caps = new Hashtable();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00027458 File Offset: 0x00025658
		private void SetPattern(string Re)
		{
			if (Re == null)
			{
				Re = string.Empty;
			}
			this._pattern = Re;
			this._currentPos = 0;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00027474 File Offset: 0x00025674
		private void Reset(RegexOptions topopts)
		{
			this._currentPos = 0;
			this._autocap = 1;
			this._ignoreNextParen = false;
			if (this._optionsStack.Count > 0)
			{
				this._optionsStack.RemoveRange(0, this._optionsStack.Count - 1);
			}
			this._options = topopts;
			this._stack = null;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000274CC File Offset: 0x000256CC
		private RegexNode ScanRegex()
		{
			bool flag = false;
			this.StartGroup(new RegexNode(28, this._options, 0, -1));
			while (this.CharsRight() > 0)
			{
				bool flag2 = flag;
				flag = false;
				this.ScanBlank();
				int num = this.Textpos();
				char c;
				if (this.UseOptionX())
				{
					while (this.CharsRight() > 0)
					{
						if (RegexParser.IsStopperX(c = this.RightChar()))
						{
							if (c != '{')
							{
								break;
							}
							if (this.IsTrueQuantifier())
							{
								break;
							}
						}
						this.MoveRight();
					}
				}
				else
				{
					while (this.CharsRight() > 0 && (!RegexParser.IsSpecial(c = this.RightChar()) || (c == '{' && !this.IsTrueQuantifier())))
					{
						this.MoveRight();
					}
				}
				int num2 = this.Textpos();
				this.ScanBlank();
				if (this.CharsRight() == 0)
				{
					c = '!';
				}
				else if (RegexParser.IsSpecial(c = this.RightChar()))
				{
					flag = RegexParser.IsQuantifier(c);
					this.MoveRight();
				}
				else
				{
					c = ' ';
				}
				if (num < num2)
				{
					int num3 = num2 - num - (flag ? 1 : 0);
					flag2 = false;
					if (num3 > 0)
					{
						this.AddConcatenate(num, num3, false);
					}
					if (flag)
					{
						this.AddUnitOne(this.CharAt(num2 - 1));
					}
				}
				if (c <= '?')
				{
					switch (c)
					{
					case ' ':
						continue;
					case '!':
						goto IL_0414;
					case '"':
					case '#':
					case '%':
					case '&':
					case '\'':
					case ',':
					case '-':
						goto IL_02A3;
					case '$':
						this.AddUnitType(this.UseOptionM() ? 15 : 20);
						break;
					case '(':
					{
						this.PushOptions();
						RegexNode regexNode;
						if ((regexNode = this.ScanGroupOpen()) == null)
						{
							this.PopKeepOptions();
							continue;
						}
						this.PushGroup();
						this.StartGroup(regexNode);
						continue;
					}
					case ')':
						if (this.EmptyStack())
						{
							throw this.MakeException("Too many )'s.");
						}
						this.AddGroup();
						this.PopGroup();
						this.PopOptions();
						if (this.Unit() == null)
						{
							continue;
						}
						break;
					case '*':
					case '+':
						goto IL_0271;
					case '.':
						if (this.UseOptionS())
						{
							this.AddUnitSet("\0\u0001\0\0");
						}
						else
						{
							this.AddUnitNotone('\n');
						}
						break;
					default:
						if (c != '?')
						{
							goto IL_02A3;
						}
						goto IL_0271;
					}
				}
				else
				{
					switch (c)
					{
					case '[':
						this.AddUnitSet(this.ScanCharClass(this.UseOptionI(), false).ToStringClass());
						break;
					case '\\':
						this.AddUnitNode(this.ScanBackslash(false));
						break;
					case ']':
						goto IL_02A3;
					case '^':
						this.AddUnitType(this.UseOptionM() ? 14 : 18);
						break;
					default:
						if (c == '{')
						{
							goto IL_0271;
						}
						if (c != '|')
						{
							goto IL_02A3;
						}
						this.AddAlternate();
						continue;
					}
				}
				IL_02AF:
				this.ScanBlank();
				if (this.CharsRight() == 0 || !(flag = this.IsTrueQuantifier()))
				{
					this.AddConcatenate();
					continue;
				}
				c = this.RightCharMoveRight();
				while (this.Unit() != null)
				{
					int num4;
					int num5;
					if (c <= '+')
					{
						if (c != '*')
						{
							if (c != '+')
							{
								goto IL_03AD;
							}
							num4 = 1;
							num5 = int.MaxValue;
						}
						else
						{
							num4 = 0;
							num5 = int.MaxValue;
						}
					}
					else if (c != '?')
					{
						if (c != '{')
						{
							goto IL_03AD;
						}
						num = this.Textpos();
						num4 = (num5 = this.ScanDecimal());
						if (num < this.Textpos() && this.CharsRight() > 0 && this.RightChar() == ',')
						{
							this.MoveRight();
							if (this.CharsRight() == 0 || this.RightChar() == '}')
							{
								num5 = int.MaxValue;
							}
							else
							{
								num5 = this.ScanDecimal();
							}
						}
						if (num == this.Textpos() || this.CharsRight() == 0 || this.RightCharMoveRight() != '}')
						{
							this.AddConcatenate();
							this.Textto(num - 1);
							break;
						}
					}
					else
					{
						num4 = 0;
						num5 = 1;
					}
					this.ScanBlank();
					bool flag3;
					if (this.CharsRight() == 0 || this.RightChar() != '?')
					{
						flag3 = false;
					}
					else
					{
						this.MoveRight();
						flag3 = true;
					}
					if (num4 > num5)
					{
						throw this.MakeException("Illegal {x,y} with x > y.");
					}
					this.AddConcatenate(flag3, num4, num5);
					continue;
					IL_03AD:
					throw this.MakeException("Internal error in ScanRegex.");
				}
				continue;
				IL_0271:
				if (this.Unit() == null)
				{
					throw this.MakeException(flag2 ? SR.Format("Nested quantifier {0}.", c.ToString()) : "Quantifier {x,y} following nothing.");
				}
				this.MoveLeft();
				goto IL_02AF;
				IL_02A3:
				throw this.MakeException("Internal error in ScanRegex.");
			}
			IL_0414:
			if (!this.EmptyStack())
			{
				throw this.MakeException("Not enough )'s.");
			}
			this.AddGroup();
			return this.Unit();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00027910 File Offset: 0x00025B10
		private RegexNode ScanReplacement()
		{
			this._concatenation = new RegexNode(25, this._options);
			for (;;)
			{
				int num = this.CharsRight();
				if (num == 0)
				{
					break;
				}
				int num2 = this.Textpos();
				while (num > 0 && this.RightChar() != '$')
				{
					this.MoveRight();
					num--;
				}
				this.AddConcatenate(num2, this.Textpos() - num2, true);
				if (num > 0)
				{
					if (this.RightCharMoveRight() == '$')
					{
						this.AddUnitNode(this.ScanDollar());
					}
					this.AddConcatenate();
				}
			}
			return this._concatenation;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00027994 File Offset: 0x00025B94
		private RegexCharClass ScanCharClass(bool caseInsensitive, bool scanOnly)
		{
			char c = '\0';
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			RegexCharClass regexCharClass = (scanOnly ? null : new RegexCharClass());
			if (this.CharsRight() > 0 && this.RightChar() == '^')
			{
				this.MoveRight();
				if (!scanOnly)
				{
					regexCharClass.Negate = true;
				}
			}
			while (this.CharsRight() > 0)
			{
				bool flag4 = false;
				char c2 = this.RightCharMoveRight();
				if (c2 == ']')
				{
					if (!flag2)
					{
						flag3 = true;
						break;
					}
					goto IL_0261;
				}
				else
				{
					if (c2 == '\\' && this.CharsRight() > 0)
					{
						char c3;
						c2 = (c3 = this.RightCharMoveRight());
						if (c3 <= 'S')
						{
							if (c3 <= 'D')
							{
								if (c3 != '-')
								{
									if (c3 != 'D')
									{
										goto IL_01FA;
									}
								}
								else
								{
									if (!scanOnly)
									{
										regexCharClass.AddRange(c2, c2);
										goto IL_0371;
									}
									goto IL_0371;
								}
							}
							else
							{
								if (c3 == 'P')
								{
									goto IL_019B;
								}
								if (c3 != 'S')
								{
									goto IL_01FA;
								}
								goto IL_012B;
							}
						}
						else
						{
							if (c3 <= 'd')
							{
								if (c3 != 'W')
								{
									if (c3 != 'd')
									{
										goto IL_01FA;
									}
									goto IL_00ED;
								}
							}
							else
							{
								if (c3 == 'p')
								{
									goto IL_019B;
								}
								if (c3 == 's')
								{
									goto IL_012B;
								}
								if (c3 != 'w')
								{
									goto IL_01FA;
								}
							}
							if (scanOnly)
							{
								goto IL_0371;
							}
							if (flag)
							{
								throw this.MakeException(SR.Format("Cannot include class \\{0} in character range.", c2.ToString()));
							}
							regexCharClass.AddWord(this.UseOptionE(), c2 == 'W');
							goto IL_0371;
						}
						IL_00ED:
						if (scanOnly)
						{
							goto IL_0371;
						}
						if (flag)
						{
							throw this.MakeException(SR.Format("Cannot include class \\{0} in character range.", c2.ToString()));
						}
						regexCharClass.AddDigit(this.UseOptionE(), c2 == 'D', this._pattern);
						goto IL_0371;
						IL_012B:
						if (scanOnly)
						{
							goto IL_0371;
						}
						if (flag)
						{
							throw this.MakeException(SR.Format("Cannot include class \\{0} in character range.", c2.ToString()));
						}
						regexCharClass.AddSpace(this.UseOptionE(), c2 == 'S');
						goto IL_0371;
						IL_019B:
						if (scanOnly)
						{
							this.ParseProperty();
							goto IL_0371;
						}
						if (flag)
						{
							throw this.MakeException(SR.Format("Cannot include class \\{0} in character range.", c2.ToString()));
						}
						regexCharClass.AddCategoryFromName(this.ParseProperty(), c2 != 'p', caseInsensitive, this._pattern);
						goto IL_0371;
						IL_01FA:
						this.MoveLeft();
						c2 = this.ScanCharEscape();
						flag4 = true;
						goto IL_0261;
					}
					if (c2 != '[' || this.CharsRight() <= 0 || this.RightChar() != ':' || flag)
					{
						goto IL_0261;
					}
					int num = this.Textpos();
					this.MoveRight();
					this.ScanCapname();
					if (this.CharsRight() < 2 || this.RightCharMoveRight() != ':' || this.RightCharMoveRight() != ']')
					{
						this.Textto(num);
						goto IL_0261;
					}
					goto IL_0261;
				}
				IL_0371:
				flag2 = false;
				continue;
				IL_0261:
				if (flag)
				{
					flag = false;
					if (scanOnly)
					{
						goto IL_0371;
					}
					if (c2 == '[' && !flag4 && !flag2)
					{
						regexCharClass.AddChar(c);
						regexCharClass.AddSubtraction(this.ScanCharClass(caseInsensitive, scanOnly));
						if (this.CharsRight() > 0 && this.RightChar() != ']')
						{
							throw this.MakeException("A subtraction must be the last element in a character class.");
						}
						goto IL_0371;
					}
					else
					{
						if (c > c2)
						{
							throw this.MakeException("[x-y] range in reverse order.");
						}
						regexCharClass.AddRange(c, c2);
						goto IL_0371;
					}
				}
				else
				{
					if (this.CharsRight() >= 2 && this.RightChar() == '-' && this.RightChar(1) != ']')
					{
						c = c2;
						flag = true;
						this.MoveRight();
						goto IL_0371;
					}
					if (this.CharsRight() >= 1 && c2 == '-' && !flag4 && this.RightChar() == '[' && !flag2)
					{
						if (scanOnly)
						{
							this.MoveRight(1);
							this.ScanCharClass(caseInsensitive, scanOnly);
							goto IL_0371;
						}
						this.MoveRight(1);
						regexCharClass.AddSubtraction(this.ScanCharClass(caseInsensitive, scanOnly));
						if (this.CharsRight() > 0 && this.RightChar() != ']')
						{
							throw this.MakeException("A subtraction must be the last element in a character class.");
						}
						goto IL_0371;
					}
					else
					{
						if (!scanOnly)
						{
							regexCharClass.AddRange(c2, c2);
							goto IL_0371;
						}
						goto IL_0371;
					}
				}
			}
			if (!flag3)
			{
				throw this.MakeException("Unterminated [] set.");
			}
			if (!scanOnly && caseInsensitive)
			{
				regexCharClass.AddLowercase(this._culture);
			}
			return regexCharClass;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00027D48 File Offset: 0x00025F48
		private RegexNode ScanGroupOpen()
		{
			char c = '>';
			if (this.CharsRight() != 0 && this.RightChar() == '?' && (this.RightChar() != '?' || this.CharsRight() <= 1 || this.RightChar(1) != ')'))
			{
				this.MoveRight();
				if (this.CharsRight() != 0)
				{
					char c2 = this.RightCharMoveRight();
					int num;
					char c3;
					if (c2 <= '\'')
					{
						if (c2 == '!')
						{
							this._options &= ~RegexOptions.RightToLeft;
							num = 31;
							goto IL_04FA;
						}
						if (c2 != '\'')
						{
							goto IL_04C1;
						}
						c = '\'';
					}
					else if (c2 != '(')
					{
						switch (c2)
						{
						case ':':
							num = 29;
							goto IL_04FA;
						case ';':
							goto IL_04C1;
						case '<':
							break;
						case '=':
							this._options &= ~RegexOptions.RightToLeft;
							num = 30;
							goto IL_04FA;
						case '>':
							num = 32;
							goto IL_04FA;
						default:
							goto IL_04C1;
						}
					}
					else
					{
						int num2 = this.Textpos();
						if (this.CharsRight() > 0)
						{
							c3 = this.RightChar();
							if (c3 >= '0' && c3 <= '9')
							{
								int num3 = this.ScanDecimal();
								if (this.CharsRight() <= 0 || this.RightCharMoveRight() != ')')
								{
									throw this.MakeException(SR.Format("(?({0}) ) malformed.", num3.ToString(CultureInfo.CurrentCulture)));
								}
								if (this.IsCaptureSlot(num3))
								{
									return new RegexNode(33, this._options, num3);
								}
								throw this.MakeException(SR.Format("(?({0}) ) reference to undefined group.", num3.ToString(CultureInfo.CurrentCulture)));
							}
							else if (RegexCharClass.IsWordChar(c3))
							{
								string text = this.ScanCapname();
								if (this.IsCaptureName(text) && this.CharsRight() > 0 && this.RightCharMoveRight() == ')')
								{
									return new RegexNode(33, this._options, this.CaptureSlotFromName(text));
								}
							}
						}
						num = 34;
						this.Textto(num2 - 1);
						this._ignoreNextParen = true;
						int num4 = this.CharsRight();
						if (num4 < 3 || this.RightChar(1) != '?')
						{
							goto IL_04FA;
						}
						char c4 = this.RightChar(2);
						if (c4 == '#')
						{
							throw this.MakeException("Alternation conditions cannot be comments.");
						}
						if (c4 == '\'')
						{
							throw this.MakeException("Alternation conditions do not capture and cannot be named.");
						}
						if (num4 >= 4 && c4 == '<' && this.RightChar(3) != '!' && this.RightChar(3) != '=')
						{
							throw this.MakeException("Alternation conditions do not capture and cannot be named.");
						}
						goto IL_04FA;
					}
					if (this.CharsRight() == 0)
					{
						goto IL_0507;
					}
					char c5;
					c3 = (c5 = this.RightCharMoveRight());
					if (c5 != '!')
					{
						if (c5 == '=')
						{
							if (c != '\'')
							{
								this._options |= RegexOptions.RightToLeft;
								num = 30;
								goto IL_04FA;
							}
							goto IL_0507;
						}
						else
						{
							this.MoveLeft();
							int num5 = -1;
							int num6 = -1;
							bool flag = false;
							if (c3 >= '0' && c3 <= '9')
							{
								num5 = this.ScanDecimal();
								if (!this.IsCaptureSlot(num5))
								{
									num5 = -1;
								}
								if (this.CharsRight() > 0 && this.RightChar() != c && this.RightChar() != '-')
								{
									throw this.MakeException("Invalid group name: Group names must begin with a word character.");
								}
								if (num5 == 0)
								{
									throw this.MakeException("Capture number cannot be zero.");
								}
							}
							else if (RegexCharClass.IsWordChar(c3))
							{
								string text2 = this.ScanCapname();
								if (this.IsCaptureName(text2))
								{
									num5 = this.CaptureSlotFromName(text2);
								}
								if (this.CharsRight() > 0 && this.RightChar() != c && this.RightChar() != '-')
								{
									throw this.MakeException("Invalid group name: Group names must begin with a word character.");
								}
							}
							else
							{
								if (c3 != '-')
								{
									throw this.MakeException("Invalid group name: Group names must begin with a word character.");
								}
								flag = true;
							}
							if ((num5 != -1 || flag) && this.CharsRight() > 1 && this.RightChar() == '-')
							{
								this.MoveRight();
								c3 = this.RightChar();
								if (c3 >= '0' && c3 <= '9')
								{
									num6 = this.ScanDecimal();
									if (!this.IsCaptureSlot(num6))
									{
										throw this.MakeException(SR.Format("Reference to undefined group number {0}.", num6));
									}
									if (this.CharsRight() > 0 && this.RightChar() != c)
									{
										throw this.MakeException("Invalid group name: Group names must begin with a word character.");
									}
								}
								else
								{
									if (!RegexCharClass.IsWordChar(c3))
									{
										throw this.MakeException("Invalid group name: Group names must begin with a word character.");
									}
									string text3 = this.ScanCapname();
									if (!this.IsCaptureName(text3))
									{
										throw this.MakeException(SR.Format("Reference to undefined group name {0}.", text3));
									}
									num6 = this.CaptureSlotFromName(text3);
									if (this.CharsRight() > 0 && this.RightChar() != c)
									{
										throw this.MakeException("Invalid group name: Group names must begin with a word character.");
									}
								}
							}
							if ((num5 != -1 || num6 != -1) && this.CharsRight() > 0 && this.RightCharMoveRight() == c)
							{
								return new RegexNode(28, this._options, num5, num6);
							}
							goto IL_0507;
						}
					}
					else
					{
						if (c != '\'')
						{
							this._options |= RegexOptions.RightToLeft;
							num = 31;
							goto IL_04FA;
						}
						goto IL_0507;
					}
					IL_04C1:
					this.MoveLeft();
					num = 29;
					if (this._group.NType != 34)
					{
						this.ScanOptions();
					}
					if (this.CharsRight() == 0)
					{
						goto IL_0507;
					}
					if ((c3 = this.RightCharMoveRight()) == ')')
					{
						return null;
					}
					if (c3 != ':')
					{
						goto IL_0507;
					}
					IL_04FA:
					return new RegexNode(num, this._options);
				}
				IL_0507:
				throw this.MakeException("Unrecognized grouping construct.");
			}
			if (this.UseOptionN() || this._ignoreNextParen)
			{
				this._ignoreNextParen = false;
				return new RegexNode(29, this._options);
			}
			int num7 = 28;
			RegexOptions options = this._options;
			int autocap = this._autocap;
			this._autocap = autocap + 1;
			return new RegexNode(num7, options, autocap, -1);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00028268 File Offset: 0x00026468
		private void ScanBlank()
		{
			if (this.UseOptionX())
			{
				for (;;)
				{
					if (this.CharsRight() <= 0 || !RegexParser.IsSpace(this.RightChar()))
					{
						if (this.CharsRight() == 0)
						{
							return;
						}
						if (this.RightChar() == '#')
						{
							while (this.CharsRight() > 0)
							{
								if (this.RightChar() == '\n')
								{
									break;
								}
								this.MoveRight();
							}
						}
						else
						{
							if (this.CharsRight() < 3 || this.RightChar(2) != '#' || this.RightChar(1) != '?' || this.RightChar() != '(')
							{
								return;
							}
							while (this.CharsRight() > 0 && this.RightChar() != ')')
							{
								this.MoveRight();
							}
							if (this.CharsRight() == 0)
							{
								break;
							}
							this.MoveRight();
						}
					}
					else
					{
						this.MoveRight();
					}
				}
				throw this.MakeException("Unterminated (?#...) comment.");
			}
			while (this.CharsRight() >= 3 && this.RightChar(2) == '#' && this.RightChar(1) == '?' && this.RightChar() == '(')
			{
				while (this.CharsRight() > 0 && this.RightChar() != ')')
				{
					this.MoveRight();
				}
				if (this.CharsRight() == 0)
				{
					throw this.MakeException("Unterminated (?#...) comment.");
				}
				this.MoveRight();
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000283A0 File Offset: 0x000265A0
		private RegexNode ScanBackslash(bool scanOnly)
		{
			if (this.CharsRight() == 0)
			{
				throw this.MakeException("Illegal \\ at end of pattern.");
			}
			char c2;
			char c = (c2 = this.RightChar());
			if (c2 <= 'Z')
			{
				if (c2 <= 'P')
				{
					switch (c2)
					{
					case 'A':
					case 'B':
					case 'G':
						break;
					case 'C':
					case 'E':
					case 'F':
						goto IL_0274;
					case 'D':
						this.MoveRight();
						if (scanOnly)
						{
							return null;
						}
						if (this.UseOptionE())
						{
							return new RegexNode(11, this._options, "\u0001\u0002\00:");
						}
						return new RegexNode(11, this._options, RegexCharClass.NotDigitClass);
					default:
						if (c2 != 'P')
						{
							goto IL_0274;
						}
						goto IL_021B;
					}
				}
				else if (c2 != 'S')
				{
					if (c2 != 'W')
					{
						if (c2 != 'Z')
						{
							goto IL_0274;
						}
					}
					else
					{
						this.MoveRight();
						if (scanOnly)
						{
							return null;
						}
						if (this.UseOptionE())
						{
							return new RegexNode(11, this._options, "\u0001\n\00:A[_`a{İı");
						}
						return new RegexNode(11, this._options, RegexCharClass.NotWordClass);
					}
				}
				else
				{
					this.MoveRight();
					if (scanOnly)
					{
						return null;
					}
					if (this.UseOptionE())
					{
						return new RegexNode(11, this._options, "\u0001\u0004\0\t\u000e !");
					}
					return new RegexNode(11, this._options, RegexCharClass.NotSpaceClass);
				}
			}
			else if (c2 <= 'p')
			{
				if (c2 != 'b')
				{
					if (c2 != 'd')
					{
						if (c2 != 'p')
						{
							goto IL_0274;
						}
						goto IL_021B;
					}
					else
					{
						this.MoveRight();
						if (scanOnly)
						{
							return null;
						}
						if (this.UseOptionE())
						{
							return new RegexNode(11, this._options, "\0\u0002\00:");
						}
						return new RegexNode(11, this._options, RegexCharClass.DigitClass);
					}
				}
			}
			else if (c2 != 's')
			{
				if (c2 != 'w')
				{
					if (c2 != 'z')
					{
						goto IL_0274;
					}
				}
				else
				{
					this.MoveRight();
					if (scanOnly)
					{
						return null;
					}
					if (this.UseOptionE())
					{
						return new RegexNode(11, this._options, "\0\n\00:A[_`a{İı");
					}
					return new RegexNode(11, this._options, RegexCharClass.WordClass);
				}
			}
			else
			{
				this.MoveRight();
				if (scanOnly)
				{
					return null;
				}
				if (this.UseOptionE())
				{
					return new RegexNode(11, this._options, "\0\u0004\0\t\u000e !");
				}
				return new RegexNode(11, this._options, RegexCharClass.SpaceClass);
			}
			this.MoveRight();
			if (scanOnly)
			{
				return null;
			}
			return new RegexNode(this.TypeFromCode(c), this._options);
			IL_021B:
			this.MoveRight();
			if (scanOnly)
			{
				return null;
			}
			RegexCharClass regexCharClass = new RegexCharClass();
			regexCharClass.AddCategoryFromName(this.ParseProperty(), c != 'p', this.UseOptionI(), this._pattern);
			if (this.UseOptionI())
			{
				regexCharClass.AddLowercase(this._culture);
			}
			return new RegexNode(11, this._options, regexCharClass.ToStringClass());
			IL_0274:
			return this.ScanBasicBackslash(scanOnly);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00028628 File Offset: 0x00026828
		private RegexNode ScanBasicBackslash(bool scanOnly)
		{
			if (this.CharsRight() == 0)
			{
				throw this.MakeException("Illegal \\ at end of pattern.");
			}
			bool flag = false;
			char c = '\0';
			int num = this.Textpos();
			char c2 = this.RightChar();
			if (c2 == 'k')
			{
				if (this.CharsRight() >= 2)
				{
					this.MoveRight();
					c2 = this.RightCharMoveRight();
					if (c2 == '<' || c2 == '\'')
					{
						flag = true;
						c = ((c2 == '\'') ? '\'' : '>');
					}
				}
				if (!flag || this.CharsRight() <= 0)
				{
					throw this.MakeException("Malformed \\k<...> named back reference.");
				}
				c2 = this.RightChar();
			}
			else if ((c2 == '<' || c2 == '\'') && this.CharsRight() > 1)
			{
				flag = true;
				c = ((c2 == '\'') ? '\'' : '>');
				this.MoveRight();
				c2 = this.RightChar();
			}
			if (flag && c2 >= '0' && c2 <= '9')
			{
				int num2 = this.ScanDecimal();
				if (this.CharsRight() > 0 && this.RightCharMoveRight() == c)
				{
					if (scanOnly)
					{
						return null;
					}
					if (this.IsCaptureSlot(num2))
					{
						return new RegexNode(13, this._options, num2);
					}
					throw this.MakeException(SR.Format("Reference to undefined group number {0}.", num2.ToString(CultureInfo.CurrentCulture)));
				}
			}
			else if (!flag && c2 >= '1' && c2 <= '9')
			{
				if (this.UseOptionE())
				{
					int num3 = -1;
					int i = (int)(c2 - '0');
					int num4 = this.Textpos() - 1;
					while (i <= this._captop)
					{
						if (this.IsCaptureSlot(i) && (this._caps == null || (int)this._caps[i] < num4))
						{
							num3 = i;
						}
						this.MoveRight();
						if (this.CharsRight() == 0 || (c2 = this.RightChar()) < '0' || c2 > '9')
						{
							break;
						}
						i = i * 10 + (int)(c2 - '0');
					}
					if (num3 >= 0)
					{
						if (!scanOnly)
						{
							return new RegexNode(13, this._options, num3);
						}
						return null;
					}
				}
				else
				{
					int num5 = this.ScanDecimal();
					if (scanOnly)
					{
						return null;
					}
					if (this.IsCaptureSlot(num5))
					{
						return new RegexNode(13, this._options, num5);
					}
					if (num5 <= 9)
					{
						throw this.MakeException(SR.Format("Reference to undefined group number {0}.", num5.ToString(CultureInfo.CurrentCulture)));
					}
				}
			}
			else if (flag && RegexCharClass.IsWordChar(c2))
			{
				string text = this.ScanCapname();
				if (this.CharsRight() > 0 && this.RightCharMoveRight() == c)
				{
					if (scanOnly)
					{
						return null;
					}
					if (this.IsCaptureName(text))
					{
						return new RegexNode(13, this._options, this.CaptureSlotFromName(text));
					}
					throw this.MakeException(SR.Format("Reference to undefined group name {0}.", text));
				}
			}
			this.Textto(num);
			c2 = this.ScanCharEscape();
			if (this.UseOptionI())
			{
				c2 = this._culture.TextInfo.ToLower(c2);
			}
			if (!scanOnly)
			{
				return new RegexNode(9, this._options, c2);
			}
			return null;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000288E4 File Offset: 0x00026AE4
		private RegexNode ScanDollar()
		{
			if (this.CharsRight() == 0)
			{
				return new RegexNode(9, this._options, '$');
			}
			char c = this.RightChar();
			int num = this.Textpos();
			int num2 = num;
			bool flag;
			if (c == '{' && this.CharsRight() > 1)
			{
				flag = true;
				this.MoveRight();
				c = this.RightChar();
			}
			else
			{
				flag = false;
			}
			if (c >= '0' && c <= '9')
			{
				if (!flag && this.UseOptionE())
				{
					int num3 = -1;
					int num4 = (int)(c - '0');
					this.MoveRight();
					if (this.IsCaptureSlot(num4))
					{
						num3 = num4;
						num2 = this.Textpos();
					}
					while (this.CharsRight() > 0 && (c = this.RightChar()) >= '0' && c <= '9')
					{
						int num5 = (int)(c - '0');
						if (num4 > 214748364 || (num4 == 214748364 && num5 > 7))
						{
							throw this.MakeException("Capture group numbers must be less than or equal to Int32.MaxValue.");
						}
						num4 = num4 * 10 + num5;
						this.MoveRight();
						if (this.IsCaptureSlot(num4))
						{
							num3 = num4;
							num2 = this.Textpos();
						}
					}
					this.Textto(num2);
					if (num3 >= 0)
					{
						return new RegexNode(13, this._options, num3);
					}
				}
				else
				{
					int num6 = this.ScanDecimal();
					if ((!flag || (this.CharsRight() > 0 && this.RightCharMoveRight() == '}')) && this.IsCaptureSlot(num6))
					{
						return new RegexNode(13, this._options, num6);
					}
				}
			}
			else if (flag && RegexCharClass.IsWordChar(c))
			{
				string text = this.ScanCapname();
				if (this.CharsRight() > 0 && this.RightCharMoveRight() == '}' && this.IsCaptureName(text))
				{
					return new RegexNode(13, this._options, this.CaptureSlotFromName(text));
				}
			}
			else if (!flag)
			{
				int num7 = 1;
				if (c <= '+')
				{
					switch (c)
					{
					case '$':
						this.MoveRight();
						return new RegexNode(9, this._options, '$');
					case '%':
						break;
					case '&':
						num7 = 0;
						break;
					case '\'':
						num7 = -2;
						break;
					default:
						if (c == '+')
						{
							num7 = -3;
						}
						break;
					}
				}
				else if (c != '_')
				{
					if (c == '`')
					{
						num7 = -1;
					}
				}
				else
				{
					num7 = -4;
				}
				if (num7 != 1)
				{
					this.MoveRight();
					return new RegexNode(13, this._options, num7);
				}
			}
			this.Textto(num);
			return new RegexNode(9, this._options, '$');
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00028B30 File Offset: 0x00026D30
		private string ScanCapname()
		{
			int num = this.Textpos();
			while (this.CharsRight() > 0)
			{
				if (!RegexCharClass.IsWordChar(this.RightCharMoveRight()))
				{
					this.MoveLeft();
					break;
				}
			}
			return this._pattern.Substring(num, this.Textpos() - num);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00028B78 File Offset: 0x00026D78
		private char ScanOctal()
		{
			int num = 3;
			if (num > this.CharsRight())
			{
				num = this.CharsRight();
			}
			int num2 = 0;
			int num3;
			while (num > 0 && (num3 = (int)(this.RightChar() - '0')) <= 7)
			{
				this.MoveRight();
				num2 *= 8;
				num2 += num3;
				if (this.UseOptionE() && num2 >= 32)
				{
					break;
				}
				num--;
			}
			num2 &= 255;
			return (char)num2;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00028BD8 File Offset: 0x00026DD8
		private int ScanDecimal()
		{
			int num = 0;
			int num2;
			while (this.CharsRight() > 0 && (num2 = (int)((ushort)(this.RightChar() - '0'))) <= 9)
			{
				this.MoveRight();
				if (num > 214748364 || (num == 214748364 && num2 > 7))
				{
					throw this.MakeException("Capture group numbers must be less than or equal to Int32.MaxValue.");
				}
				num *= 10;
				num += num2;
			}
			return num;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00028C34 File Offset: 0x00026E34
		private char ScanHex(int c)
		{
			int num = 0;
			if (this.CharsRight() >= c)
			{
				int num2;
				while (c > 0 && (num2 = RegexParser.HexDigit(this.RightCharMoveRight())) >= 0)
				{
					num *= 16;
					num += num2;
					c--;
				}
			}
			if (c > 0)
			{
				throw this.MakeException("Insufficient hexadecimal digits.");
			}
			return (char)num;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00028C84 File Offset: 0x00026E84
		private static int HexDigit(char ch)
		{
			int num;
			if ((num = (int)(ch - '0')) <= 9)
			{
				return num;
			}
			if ((num = (int)(ch - 'a')) <= 5)
			{
				return num + 10;
			}
			if ((num = (int)(ch - 'A')) <= 5)
			{
				return num + 10;
			}
			return -1;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00028CBC File Offset: 0x00026EBC
		private char ScanControl()
		{
			if (this.CharsRight() <= 0)
			{
				throw this.MakeException("Missing control character.");
			}
			char c = this.RightCharMoveRight();
			if (c >= 'a' && c <= 'z')
			{
				c -= ' ';
			}
			if ((c -= '@') < ' ')
			{
				return c;
			}
			throw this.MakeException("Unrecognized control character.");
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00028D0D File Offset: 0x00026F0D
		private bool IsOnlyTopOption(RegexOptions option)
		{
			return option == RegexOptions.RightToLeft || option == RegexOptions.CultureInvariant || option == RegexOptions.ECMAScript;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00028D28 File Offset: 0x00026F28
		private void ScanOptions()
		{
			bool flag = false;
			while (this.CharsRight() > 0)
			{
				char c = this.RightChar();
				if (c == '-')
				{
					flag = true;
				}
				else if (c == '+')
				{
					flag = false;
				}
				else
				{
					RegexOptions regexOptions = RegexParser.OptionFromCode(c);
					if (regexOptions == RegexOptions.None || this.IsOnlyTopOption(regexOptions))
					{
						return;
					}
					if (flag)
					{
						this._options &= ~regexOptions;
					}
					else
					{
						this._options |= regexOptions;
					}
				}
				this.MoveRight();
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00028D98 File Offset: 0x00026F98
		private char ScanCharEscape()
		{
			char c = this.RightCharMoveRight();
			if (c >= '0' && c <= '7')
			{
				this.MoveLeft();
				return this.ScanOctal();
			}
			switch (c)
			{
			case 'a':
				return '\a';
			case 'b':
				return '\b';
			case 'c':
				return this.ScanControl();
			case 'd':
				break;
			case 'e':
				return '\u001b';
			case 'f':
				return '\f';
			default:
				switch (c)
				{
				case 'n':
					return '\n';
				case 'r':
					return '\r';
				case 't':
					return '\t';
				case 'u':
					return this.ScanHex(4);
				case 'v':
					return '\v';
				case 'x':
					return this.ScanHex(2);
				}
				break;
			}
			if (!this.UseOptionE() && RegexCharClass.IsWordChar(c))
			{
				throw this.MakeException(SR.Format("Unrecognized escape sequence \\{0}.", c.ToString()));
			}
			return c;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00028E74 File Offset: 0x00027074
		private string ParseProperty()
		{
			if (this.CharsRight() < 3)
			{
				throw this.MakeException("Incomplete \\p{X} character escape.");
			}
			char c = this.RightCharMoveRight();
			if (c != '{')
			{
				throw this.MakeException("Malformed \\p{X} character escape.");
			}
			int num = this.Textpos();
			while (this.CharsRight() > 0)
			{
				c = this.RightCharMoveRight();
				if (!RegexCharClass.IsWordChar(c) && c != '-')
				{
					this.MoveLeft();
					break;
				}
			}
			string text = this._pattern.Substring(num, this.Textpos() - num);
			if (this.CharsRight() == 0 || this.RightCharMoveRight() != '}')
			{
				throw this.MakeException("Incomplete \\p{X} character escape.");
			}
			return text;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00028F10 File Offset: 0x00027110
		private int TypeFromCode(char ch)
		{
			if (ch <= 'G')
			{
				if (ch == 'A')
				{
					return 18;
				}
				if (ch != 'B')
				{
					if (ch == 'G')
					{
						return 19;
					}
				}
				else
				{
					if (!this.UseOptionE())
					{
						return 17;
					}
					return 42;
				}
			}
			else
			{
				if (ch == 'Z')
				{
					return 20;
				}
				if (ch != 'b')
				{
					if (ch == 'z')
					{
						return 21;
					}
				}
				else
				{
					if (!this.UseOptionE())
					{
						return 16;
					}
					return 41;
				}
			}
			return 22;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00028F70 File Offset: 0x00027170
		private static RegexOptions OptionFromCode(char ch)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				ch += ' ';
			}
			if (ch <= 'i')
			{
				if (ch == 'e')
				{
					return RegexOptions.ECMAScript;
				}
				if (ch == 'i')
				{
					return RegexOptions.IgnoreCase;
				}
			}
			else
			{
				switch (ch)
				{
				case 'm':
					return RegexOptions.Multiline;
				case 'n':
					return RegexOptions.ExplicitCapture;
				case 'o':
				case 'p':
				case 'q':
					break;
				case 'r':
					return RegexOptions.RightToLeft;
				case 's':
					return RegexOptions.Singleline;
				default:
					if (ch == 'x')
					{
						return RegexOptions.IgnorePatternWhitespace;
					}
					break;
				}
			}
			return RegexOptions.None;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00028FE4 File Offset: 0x000271E4
		private void CountCaptures()
		{
			this.NoteCaptureSlot(0, 0);
			this._autocap = 1;
			while (this.CharsRight() > 0)
			{
				int num = this.Textpos();
				char c = this.RightCharMoveRight();
				if (c <= '(')
				{
					if (c != '#')
					{
						if (c == '(')
						{
							if (this.CharsRight() >= 2 && this.RightChar(1) == '#' && this.RightChar() == '?')
							{
								this.MoveLeft();
								this.ScanBlank();
							}
							else
							{
								this.PushOptions();
								if (this.CharsRight() > 0 && this.RightChar() == '?')
								{
									this.MoveRight();
									if (this.CharsRight() > 1 && (this.RightChar() == '<' || this.RightChar() == '\''))
									{
										this.MoveRight();
										c = this.RightChar();
										if (c != '0' && RegexCharClass.IsWordChar(c))
										{
											if (c >= '1' && c <= '9')
											{
												this.NoteCaptureSlot(this.ScanDecimal(), num);
											}
											else
											{
												this.NoteCaptureName(this.ScanCapname(), num);
											}
										}
									}
									else
									{
										this.ScanOptions();
										if (this.CharsRight() > 0)
										{
											if (this.RightChar() == ')')
											{
												this.MoveRight();
												this.PopKeepOptions();
											}
											else if (this.RightChar() == '(')
											{
												this._ignoreNextParen = true;
												continue;
											}
										}
									}
								}
								else if (!this.UseOptionN() && !this._ignoreNextParen)
								{
									int autocap = this._autocap;
									this._autocap = autocap + 1;
									this.NoteCaptureSlot(autocap, num);
								}
							}
							this._ignoreNextParen = false;
						}
					}
					else if (this.UseOptionX())
					{
						this.MoveLeft();
						this.ScanBlank();
					}
				}
				else if (c != ')')
				{
					if (c != '[')
					{
						if (c == '\\' && this.CharsRight() > 0)
						{
							this.ScanBackslash(true);
						}
					}
					else
					{
						this.ScanCharClass(false, true);
					}
				}
				else if (!this.EmptyOptionsStack())
				{
					this.PopOptions();
				}
			}
			this.AssignNameSlots();
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000291C8 File Offset: 0x000273C8
		private void NoteCaptureSlot(int i, int pos)
		{
			if (!this._caps.ContainsKey(i))
			{
				this._caps.Add(i, pos);
				this._capcount++;
				if (this._captop <= i)
				{
					if (i == 2147483647)
					{
						this._captop = i;
						return;
					}
					this._captop = i + 1;
				}
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00029230 File Offset: 0x00027430
		private void NoteCaptureName(string name, int pos)
		{
			if (this._capnames == null)
			{
				this._capnames = new Hashtable();
				this._capnamelist = new List<string>();
			}
			if (!this._capnames.ContainsKey(name))
			{
				this._capnames.Add(name, pos);
				this._capnamelist.Add(name);
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00029287 File Offset: 0x00027487
		private void NoteCaptures(Hashtable caps, int capsize, Hashtable capnames)
		{
			this._caps = caps;
			this._capsize = capsize;
			this._capnames = capnames;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000292A0 File Offset: 0x000274A0
		private void AssignNameSlots()
		{
			if (this._capnames != null)
			{
				for (int i = 0; i < this._capnamelist.Count; i++)
				{
					while (this.IsCaptureSlot(this._autocap))
					{
						this._autocap++;
					}
					string text = this._capnamelist[i];
					int num = (int)this._capnames[text];
					this._capnames[text] = this._autocap;
					this.NoteCaptureSlot(this._autocap, num);
					this._autocap++;
				}
			}
			if (this._capcount < this._captop)
			{
				this._capnumlist = new int[this._capcount];
				int num2 = 0;
				IDictionaryEnumerator enumerator = this._caps.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this._capnumlist[num2++] = (int)enumerator.Key;
				}
				Array.Sort<int>(this._capnumlist, Comparer<int>.Default);
			}
			if (this._capnames != null || this._capnumlist != null)
			{
				int num3 = 0;
				List<string> list;
				int num4;
				if (this._capnames == null)
				{
					list = null;
					this._capnames = new Hashtable();
					this._capnamelist = new List<string>();
					num4 = -1;
				}
				else
				{
					list = this._capnamelist;
					this._capnamelist = new List<string>();
					num4 = (int)this._capnames[list[0]];
				}
				for (int j = 0; j < this._capcount; j++)
				{
					int num5 = ((this._capnumlist == null) ? j : this._capnumlist[j]);
					if (num4 == num5)
					{
						this._capnamelist.Add(list[num3++]);
						num4 = ((num3 == list.Count) ? (-1) : ((int)this._capnames[list[num3]]));
					}
					else
					{
						string text2 = Convert.ToString(num5, this._culture);
						this._capnamelist.Add(text2);
						this._capnames[text2] = num5;
					}
				}
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000294B1 File Offset: 0x000276B1
		private int CaptureSlotFromName(string capname)
		{
			return (int)this._capnames[capname];
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000294C4 File Offset: 0x000276C4
		private bool IsCaptureSlot(int i)
		{
			if (this._caps != null)
			{
				return this._caps.ContainsKey(i);
			}
			return i >= 0 && i < this._capsize;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000294EF File Offset: 0x000276EF
		private bool IsCaptureName(string capname)
		{
			return this._capnames != null && this._capnames.ContainsKey(capname);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00029507 File Offset: 0x00027707
		private bool UseOptionN()
		{
			return (this._options & RegexOptions.ExplicitCapture) > RegexOptions.None;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00029514 File Offset: 0x00027714
		private bool UseOptionI()
		{
			return (this._options & RegexOptions.IgnoreCase) > RegexOptions.None;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00029521 File Offset: 0x00027721
		private bool UseOptionM()
		{
			return (this._options & RegexOptions.Multiline) > RegexOptions.None;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0002952E File Offset: 0x0002772E
		private bool UseOptionS()
		{
			return (this._options & RegexOptions.Singleline) > RegexOptions.None;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002953C File Offset: 0x0002773C
		private bool UseOptionX()
		{
			return (this._options & RegexOptions.IgnorePatternWhitespace) > RegexOptions.None;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0002954A File Offset: 0x0002774A
		private bool UseOptionE()
		{
			return (this._options & RegexOptions.ECMAScript) > RegexOptions.None;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002955B File Offset: 0x0002775B
		private static bool IsSpecial(char ch)
		{
			return ch <= '|' && RegexParser.s_category[(int)ch] >= 4;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00029571 File Offset: 0x00027771
		private static bool IsStopperX(char ch)
		{
			return ch <= '|' && RegexParser.s_category[(int)ch] >= 2;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00029587 File Offset: 0x00027787
		private static bool IsQuantifier(char ch)
		{
			return ch <= '{' && RegexParser.s_category[(int)ch] >= 5;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000295A0 File Offset: 0x000277A0
		private bool IsTrueQuantifier()
		{
			int num = this.CharsRight();
			if (num == 0)
			{
				return false;
			}
			int num2 = this.Textpos();
			char c = this.CharAt(num2);
			if (c != '{')
			{
				return c <= '{' && RegexParser.s_category[(int)c] >= 5;
			}
			int num3 = num2;
			while (--num > 0 && (c = this.CharAt(++num3)) >= '0' && c <= '9')
			{
			}
			if (num == 0 || num3 - num2 == 1)
			{
				return false;
			}
			if (c == '}')
			{
				return true;
			}
			if (c != ',')
			{
				return false;
			}
			while (--num > 0 && (c = this.CharAt(++num3)) >= '0' && c <= '9')
			{
			}
			return num > 0 && c == '}';
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00029644 File Offset: 0x00027844
		private static bool IsSpace(char ch)
		{
			return ch <= ' ' && RegexParser.s_category[(int)ch] == 2;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00029657 File Offset: 0x00027857
		private static bool IsMetachar(char ch)
		{
			return ch <= '|' && RegexParser.s_category[(int)ch] >= 1;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00029670 File Offset: 0x00027870
		private void AddConcatenate(int pos, int cch, bool isReplacement)
		{
			if (cch == 0)
			{
				return;
			}
			RegexNode regexNode;
			if (cch > 1)
			{
				string text = this._pattern.Substring(pos, cch);
				if (this.UseOptionI() && !isReplacement)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(text.Length);
					for (int i = 0; i < text.Length; i++)
					{
						stringBuilder.Append(this._culture.TextInfo.ToLower(text[i]));
					}
					text = StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
				regexNode = new RegexNode(12, this._options, text);
			}
			else
			{
				char c = this._pattern[pos];
				if (this.UseOptionI() && !isReplacement)
				{
					c = this._culture.TextInfo.ToLower(c);
				}
				regexNode = new RegexNode(9, this._options, c);
			}
			this._concatenation.AddChild(regexNode);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002973C File Offset: 0x0002793C
		private void PushGroup()
		{
			this._group.Next = this._stack;
			this._alternation.Next = this._group;
			this._concatenation.Next = this._alternation;
			this._stack = this._concatenation;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00029788 File Offset: 0x00027988
		private void PopGroup()
		{
			this._concatenation = this._stack;
			this._alternation = this._concatenation.Next;
			this._group = this._alternation.Next;
			this._stack = this._group.Next;
			if (this._group.Type() == 34 && this._group.ChildCount() == 0)
			{
				if (this._unit == null)
				{
					throw this.MakeException("Illegal conditional (?(...)) expression.");
				}
				this._group.AddChild(this._unit);
				this._unit = null;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002981C File Offset: 0x00027A1C
		private bool EmptyStack()
		{
			return this._stack == null;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00029827 File Offset: 0x00027A27
		private void StartGroup(RegexNode openGroup)
		{
			this._group = openGroup;
			this._alternation = new RegexNode(24, this._options);
			this._concatenation = new RegexNode(25, this._options);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00029858 File Offset: 0x00027A58
		private void AddAlternate()
		{
			if (this._group.Type() == 34 || this._group.Type() == 33)
			{
				this._group.AddChild(this._concatenation.ReverseLeft());
			}
			else
			{
				this._alternation.AddChild(this._concatenation.ReverseLeft());
			}
			this._concatenation = new RegexNode(25, this._options);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000298C4 File Offset: 0x00027AC4
		private void AddConcatenate()
		{
			this._concatenation.AddChild(this._unit);
			this._unit = null;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000298DE File Offset: 0x00027ADE
		private void AddConcatenate(bool lazy, int min, int max)
		{
			this._concatenation.AddChild(this._unit.MakeQuantifier(lazy, min, max));
			this._unit = null;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00029900 File Offset: 0x00027B00
		private RegexNode Unit()
		{
			return this._unit;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00029908 File Offset: 0x00027B08
		private void AddUnitOne(char ch)
		{
			if (this.UseOptionI())
			{
				ch = this._culture.TextInfo.ToLower(ch);
			}
			this._unit = new RegexNode(9, this._options, ch);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00029939 File Offset: 0x00027B39
		private void AddUnitNotone(char ch)
		{
			if (this.UseOptionI())
			{
				ch = this._culture.TextInfo.ToLower(ch);
			}
			this._unit = new RegexNode(10, this._options, ch);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0002996A File Offset: 0x00027B6A
		private void AddUnitSet(string cc)
		{
			this._unit = new RegexNode(11, this._options, cc);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00029980 File Offset: 0x00027B80
		private void AddUnitNode(RegexNode node)
		{
			this._unit = node;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00029989 File Offset: 0x00027B89
		private void AddUnitType(int type)
		{
			this._unit = new RegexNode(type, this._options);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000299A0 File Offset: 0x00027BA0
		private void AddGroup()
		{
			if (this._group.Type() == 34 || this._group.Type() == 33)
			{
				this._group.AddChild(this._concatenation.ReverseLeft());
				if ((this._group.Type() == 33 && this._group.ChildCount() > 2) || this._group.ChildCount() > 3)
				{
					throw this.MakeException("Too many | in (?()|).");
				}
			}
			else
			{
				this._alternation.AddChild(this._concatenation.ReverseLeft());
				this._group.AddChild(this._alternation);
			}
			this._unit = this._group;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00029A4B File Offset: 0x00027C4B
		private void PushOptions()
		{
			this._optionsStack.Add(this._options);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00029A5E File Offset: 0x00027C5E
		private void PopOptions()
		{
			this._options = this._optionsStack[this._optionsStack.Count - 1];
			this._optionsStack.RemoveAt(this._optionsStack.Count - 1);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00029A96 File Offset: 0x00027C96
		private bool EmptyOptionsStack()
		{
			return this._optionsStack.Count == 0;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00029AA6 File Offset: 0x00027CA6
		private void PopKeepOptions()
		{
			this._optionsStack.RemoveAt(this._optionsStack.Count - 1);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00029AC0 File Offset: 0x00027CC0
		private ArgumentException MakeException(string message)
		{
			return new ArgumentException(SR.Format("parsing \"{0}\" - {1}", this._pattern, message));
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00029AD8 File Offset: 0x00027CD8
		private int Textpos()
		{
			return this._currentPos;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00029AE0 File Offset: 0x00027CE0
		private void Textto(int pos)
		{
			this._currentPos = pos;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00029AEC File Offset: 0x00027CEC
		private char RightCharMoveRight()
		{
			string pattern = this._pattern;
			int currentPos = this._currentPos;
			this._currentPos = currentPos + 1;
			return pattern[currentPos];
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00029B15 File Offset: 0x00027D15
		private void MoveRight()
		{
			this.MoveRight(1);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00029B1E File Offset: 0x00027D1E
		private void MoveRight(int i)
		{
			this._currentPos += i;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00029B2E File Offset: 0x00027D2E
		private void MoveLeft()
		{
			this._currentPos--;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00029B3E File Offset: 0x00027D3E
		private char CharAt(int i)
		{
			return this._pattern[i];
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00029B4C File Offset: 0x00027D4C
		internal char RightChar()
		{
			return this._pattern[this._currentPos];
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00029B5F File Offset: 0x00027D5F
		private char RightChar(int i)
		{
			return this._pattern[this._currentPos + i];
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00029B74 File Offset: 0x00027D74
		private int CharsRight()
		{
			return this._pattern.Length - this._currentPos;
		}

		// Token: 0x040005C8 RID: 1480
		private RegexNode _stack;

		// Token: 0x040005C9 RID: 1481
		private RegexNode _group;

		// Token: 0x040005CA RID: 1482
		private RegexNode _alternation;

		// Token: 0x040005CB RID: 1483
		private RegexNode _concatenation;

		// Token: 0x040005CC RID: 1484
		private RegexNode _unit;

		// Token: 0x040005CD RID: 1485
		private string _pattern;

		// Token: 0x040005CE RID: 1486
		private int _currentPos;

		// Token: 0x040005CF RID: 1487
		private CultureInfo _culture;

		// Token: 0x040005D0 RID: 1488
		private int _autocap;

		// Token: 0x040005D1 RID: 1489
		private int _capcount;

		// Token: 0x040005D2 RID: 1490
		private int _captop;

		// Token: 0x040005D3 RID: 1491
		private int _capsize;

		// Token: 0x040005D4 RID: 1492
		private Hashtable _caps;

		// Token: 0x040005D5 RID: 1493
		private Hashtable _capnames;

		// Token: 0x040005D6 RID: 1494
		private int[] _capnumlist;

		// Token: 0x040005D7 RID: 1495
		private List<string> _capnamelist;

		// Token: 0x040005D8 RID: 1496
		private RegexOptions _options;

		// Token: 0x040005D9 RID: 1497
		private List<RegexOptions> _optionsStack;

		// Token: 0x040005DA RID: 1498
		private bool _ignoreNextParen;

		// Token: 0x040005DB RID: 1499
		private static readonly byte[] s_category = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 2,
			2, 0, 2, 2, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 2, 0, 0, 3, 4, 0, 0, 0,
			4, 4, 5, 5, 0, 0, 4, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 5, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 4, 4, 0, 4, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 5, 4, 0, 0, 0
		};
	}
}
