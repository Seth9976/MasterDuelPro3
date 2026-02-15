using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007C RID: 124
	public class RfcFilter : Asn1Choice
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x00011DFE File Offset: 0x0000FFFE
		public RfcFilter(string filter)
			: base(null)
		{
			this.ChoiceValue = this.parse(filter);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00011E14 File Offset: 0x00010014
		public RfcFilter()
			: base(null)
		{
			this.filterStack = new Stack();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00011E28 File Offset: 0x00010028
		private Asn1Tagged parse(string filterExpr)
		{
			if (filterExpr == null || filterExpr.Equals(""))
			{
				filterExpr = new StringBuilder("(objectclass=*)").ToString();
			}
			int num;
			if ((num = filterExpr.IndexOf('\\')) != -1)
			{
				StringBuilder stringBuilder = new StringBuilder(filterExpr);
				int i = num;
				while (i < stringBuilder.Length - 1)
				{
					char c = stringBuilder[i++];
					if (c == '\\')
					{
						c = stringBuilder[i];
						if (c == '*' || c == '(' || c == ')' || c == '\\')
						{
							stringBuilder.Remove(i, i + 1 - i);
							stringBuilder.Insert(i, Convert.ToString((int)c, 16));
							i += 2;
						}
					}
				}
				filterExpr = stringBuilder.ToString();
			}
			if (filterExpr[0] != '(' && filterExpr[filterExpr.Length - 1] != ')')
			{
				filterExpr = "(" + filterExpr + ")";
			}
			int num2 = (int)filterExpr[0];
			int length = filterExpr.Length;
			if (num2 != 40)
			{
				throw new LdapLocalException("MISSING_LEFT_PAREN", 87);
			}
			if (filterExpr[length - 1] != ')')
			{
				throw new LdapLocalException("MISSING_RIGHT_PAREN", 87);
			}
			int num3 = 0;
			for (int j = 0; j < length; j++)
			{
				if (filterExpr[j] == '(')
				{
					num3++;
				}
				if (filterExpr[j] == ')')
				{
					num3--;
				}
			}
			if (num3 > 0)
			{
				throw new LdapLocalException("MISSING_RIGHT_PAREN", 87);
			}
			if (num3 < 0)
			{
				throw new LdapLocalException("MISSING_LEFT_PAREN", 87);
			}
			this.ft = new RfcFilter.FilterTokenizer(this, filterExpr);
			return this.parseFilter();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00011FB1 File Offset: 0x000101B1
		private Asn1Tagged parseFilter()
		{
			this.ft.getLeftParen();
			Asn1Tagged asn1Tagged = this.parseFilterComp();
			this.ft.getRightParen();
			return asn1Tagged;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00011FD0 File Offset: 0x000101D0
		private Asn1Tagged parseFilterComp()
		{
			Asn1Tagged asn1Tagged = null;
			int opOrAttr = this.ft.OpOrAttr;
			if (opOrAttr > 1)
			{
				if (opOrAttr != 2)
				{
					int filterType = this.ft.FilterType;
					string value = this.ft.Value;
					switch (filterType)
					{
					case 3:
						if (value.Equals("*"))
						{
							asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, false, 7), new RfcAttributeDescription(this.ft.Attr), false);
						}
						else if (value.IndexOf('*') != -1)
						{
							SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(value, "*", true);
							Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf(5);
							int count = tokenizer.Count;
							int num = 0;
							string text = new StringBuilder("").ToString();
							while (tokenizer.HasMoreTokens())
							{
								string text2 = tokenizer.NextToken();
								num++;
								if (text2.Equals("*"))
								{
									if (text.Equals(text2))
									{
										asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), new RfcLdapString(this.unescapeString("")), false));
									}
								}
								else if (num == 1)
								{
									asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 0), new RfcLdapString(this.unescapeString(text2)), false));
								}
								else if (num < count)
								{
									asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), new RfcLdapString(this.unescapeString(text2)), false));
								}
								else
								{
									asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 2), new RfcLdapString(this.unescapeString(text2)), false));
								}
								text = text2;
							}
							asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, 4), new RfcSubstringFilter(new RfcAttributeDescription(this.ft.Attr), asn1SequenceOf), false);
						}
						else
						{
							asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, 3), new RfcAttributeValueAssertion(new RfcAttributeDescription(this.ft.Attr), new RfcAssertionValue(this.unescapeString(value))), false);
						}
						break;
					case 5:
					case 6:
					case 8:
						asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, filterType), new RfcAttributeValueAssertion(new RfcAttributeDescription(this.ft.Attr), new RfcAssertionValue(this.unescapeString(value))), false);
						break;
					case 9:
					{
						string text3 = null;
						string text4 = null;
						bool flag = false;
						SupportClass.Tokenizer tokenizer2 = new SupportClass.Tokenizer(this.ft.Attr, ":");
						bool flag2 = true;
						while (tokenizer2.HasMoreTokens())
						{
							string text5 = tokenizer2.NextToken().Trim();
							if (flag2 && !text5.Equals(":"))
							{
								text3 = text5;
							}
							else if (text5.Equals("dn"))
							{
								flag = true;
							}
							else if (!text5.Equals(":"))
							{
								text4 = text5;
							}
							flag2 = false;
						}
						asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, 9), new RfcMatchingRuleAssertion((text4 == null) ? null : new RfcMatchingRuleId(text4), (text3 == null) ? null : new RfcAttributeDescription(text3), new RfcAssertionValue(this.unescapeString(value)), (!flag) ? null : new Asn1Boolean(true)), false);
						break;
					}
					}
				}
				else
				{
					asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, opOrAttr), this.parseFilter(), true);
				}
			}
			else
			{
				asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, opOrAttr), this.parseFilterList(), false);
			}
			return asn1Tagged;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00012314 File Offset: 0x00010514
		private Asn1SetOf parseFilterList()
		{
			Asn1SetOf asn1SetOf = new Asn1SetOf();
			asn1SetOf.add(this.parseFilter());
			while (this.ft.peekChar() == '(')
			{
				asn1SetOf.add(this.parseFilter());
			}
			return asn1SetOf;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012351 File Offset: 0x00010551
		internal static int hex2int(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			if (c < 'a' || c > 'f')
			{
				return -1;
			}
			return (int)(c - 'a' + '\n');
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00012388 File Offset: 0x00010588
		private sbyte[] unescapeString(string string_Renamed)
		{
			sbyte[] array = new sbyte[string_Renamed.Length * 3];
			bool flag = false;
			bool flag2 = false;
			int length = string_Renamed.Length;
			char[] array2 = new char[1];
			char c = '\0';
			int i = 0;
			int num = 0;
			while (i < length)
			{
				char c2 = string_Renamed[i];
				if (flag)
				{
					int num2;
					if ((num2 = RfcFilter.hex2int(c2)) < 0)
					{
						throw new LdapLocalException("INVALID_ESCAPE", new object[] { c2 }, 87);
					}
					if (flag2)
					{
						c = (char)(num2 << 4);
						flag2 = false;
					}
					else
					{
						c |= (char)num2;
						array[num++] = (sbyte)c;
						flag = (flag2 = false);
					}
				}
				else if (c2 == '\\')
				{
					flag = (flag2 = true);
				}
				else
				{
					try
					{
						if ((c2 < '\u0001' || c2 > '\'') && (c2 < '+' || c2 > '[') && c2 < ']')
						{
							string text = "";
							array2[0] = c2;
							foreach (sbyte b in SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(new string(array2))))
							{
								if (b >= 0 && b < 16)
								{
									text = text + "\\0" + Convert.ToString((int)b & 255, 16);
								}
								else
								{
									text = text + "\\" + Convert.ToString((int)b & 255, 16);
								}
							}
							throw new LdapLocalException("INVALID_CHAR_IN_FILTER", new object[] { c2, text }, 87);
						}
						if (c2 <= '\u007f')
						{
							array[num++] = (sbyte)c2;
						}
						else
						{
							array2[0] = c2;
							sbyte[] array3 = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(new string(array2)));
							Array.Copy(array3, 0, array, num, array3.Length);
							num += array3.Length;
						}
						flag = false;
					}
					catch (IOException)
					{
						throw new SystemException("UTF-8 String encoding not supported by JVM");
					}
				}
				i++;
			}
			if (flag2 || flag)
			{
				throw new LdapLocalException("SHORT_ESCAPE", 87);
			}
			sbyte[] array4 = new sbyte[num];
			Array.Copy(array, 0, array4, 0, num);
			array = null;
			return array4;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000125B8 File Offset: 0x000107B8
		private void addObject(Asn1Object current)
		{
			if (this.filterStack == null)
			{
				this.filterStack = new Stack();
			}
			if (base.choiceValue() == null)
			{
				this.ChoiceValue = current;
			}
			else
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)this.filterStack.Peek();
				Asn1Object asn1Object = asn1Tagged.taggedValue();
				if (asn1Object == null)
				{
					asn1Tagged.TaggedValue = current;
					this.filterStack.Push(current);
				}
				else if (asn1Object is Asn1SetOf)
				{
					((Asn1SetOf)asn1Object).add(current);
				}
				else if (asn1Object is Asn1Set)
				{
					((Asn1Set)asn1Object).add(current);
				}
				else if (asn1Object.getIdentifier().Tag == 2)
				{
					throw new LdapLocalException("Attemp to create more than one 'not' sub-filter", 87);
				}
			}
			int tag = current.getIdentifier().Tag;
			if (tag == 0 || tag == 1 || tag == 2)
			{
				this.filterStack.Push(current);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00012684 File Offset: 0x00010884
		public virtual void startSubstrings(string attrName)
		{
			this.finalFound = false;
			Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf(5);
			Asn1Object asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, 4), new RfcSubstringFilter(new RfcAttributeDescription(attrName), asn1SequenceOf), false);
			this.addObject(asn1Object);
			SupportClass.StackPush(this.filterStack, asn1SequenceOf);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000126D0 File Offset: 0x000108D0
		[CLSCompliant(false)]
		public virtual void addSubstring(int type, sbyte[] value_Renamed)
		{
			try
			{
				Asn1SequenceOf asn1SequenceOf = (Asn1SequenceOf)this.filterStack.Peek();
				if (type != 0 && type != 1 && type != 2)
				{
					throw new LdapLocalException("Attempt to add an invalid substring type", 87);
				}
				if (type == 0 && asn1SequenceOf.size() != 0)
				{
					throw new LdapLocalException("Attempt to add an initial substring match after the first substring", 87);
				}
				if (this.finalFound)
				{
					throw new LdapLocalException("Attempt to add a substring match after a final substring match", 87);
				}
				if (type == 2)
				{
					this.finalFound = true;
				}
				asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, type), new RfcLdapString(value_Renamed), false));
			}
			catch (InvalidCastException)
			{
				throw new LdapLocalException("A call to addSubstring occured without calling startSubstring", 87);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00012778 File Offset: 0x00010978
		public virtual void endSubstrings()
		{
			try
			{
				this.finalFound = false;
				if (((Asn1SequenceOf)this.filterStack.Peek()).size() == 0)
				{
					throw new LdapLocalException("Empty substring filter", 87);
				}
			}
			catch (InvalidCastException)
			{
				throw new LdapLocalException("Missmatched ending of substrings", 87);
			}
			this.filterStack.Pop();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000127DC File Offset: 0x000109DC
		[CLSCompliant(false)]
		public virtual void addAttributeValueAssertion(int rfcType, string attrName, sbyte[] value_Renamed)
		{
			if (this.filterStack != null && this.filterStack.Count != 0 && this.filterStack.Peek() is Asn1SequenceOf)
			{
				throw new LdapLocalException("Cannot insert an attribute assertion in a substring", 87);
			}
			if (rfcType != 3 && rfcType != 5 && rfcType != 6 && rfcType != 8)
			{
				throw new LdapLocalException("Invalid filter type for AttributeValueAssertion", 87);
			}
			Asn1Object asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, rfcType), new RfcAttributeValueAssertion(new RfcAttributeDescription(attrName), new RfcAssertionValue(value_Renamed)), false);
			this.addObject(asn1Object);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00012864 File Offset: 0x00010A64
		public virtual void addPresent(string attrName)
		{
			Asn1Object asn1Object = new Asn1Tagged(new Asn1Identifier(2, false, 7), new RfcAttributeDescription(attrName), false);
			this.addObject(asn1Object);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00012890 File Offset: 0x00010A90
		[CLSCompliant(false)]
		public virtual void addExtensibleMatch(string matchingRule, string attrName, sbyte[] value_Renamed, bool useDNMatching)
		{
			Asn1Object asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, 9), new RfcMatchingRuleAssertion((matchingRule == null) ? null : new RfcMatchingRuleId(matchingRule), (attrName == null) ? null : new RfcAttributeDescription(attrName), new RfcAssertionValue(value_Renamed), (!useDNMatching) ? null : new Asn1Boolean(true)), false);
			this.addObject(asn1Object);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000128E4 File Offset: 0x00010AE4
		public virtual void startNestedFilter(int rfcType)
		{
			Asn1Object asn1Object;
			if (rfcType == 0 || rfcType == 1)
			{
				asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, rfcType), new Asn1SetOf(), false);
			}
			else
			{
				if (rfcType != 2)
				{
					throw new LdapLocalException("Attempt to create a nested filter other than AND, OR or NOT", 87);
				}
				asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, rfcType), null, true);
			}
			this.addObject(asn1Object);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00012938 File Offset: 0x00010B38
		public virtual void endNestedFilter(int rfcType)
		{
			if (rfcType == 2)
			{
				this.filterStack.Pop();
			}
			if (((Asn1Object)this.filterStack.Peek()).getIdentifier().Tag != rfcType)
			{
				throw new LdapLocalException("Missmatched ending of nested filter", 87);
			}
			this.filterStack.Pop();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001298B File Offset: 0x00010B8B
		public virtual IEnumerator getFilterIterator()
		{
			return new RfcFilter.FilterIterator(this, (Asn1Tagged)base.choiceValue());
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000129A0 File Offset: 0x00010BA0
		public virtual string filterToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			RfcFilter.stringFilter(this.getFilterIterator(), stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000129C8 File Offset: 0x00010BC8
		private static void stringFilter(IEnumerator itr, StringBuilder filter)
		{
			filter.Append('(');
			while (itr.MoveNext())
			{
				object obj = itr.Current;
				if (obj is int)
				{
					switch ((int)obj)
					{
					case 0:
						filter.Append('&');
						break;
					case 1:
						filter.Append('|');
						break;
					case 2:
						filter.Append('!');
						break;
					case 3:
					{
						filter.Append((string)itr.Current);
						filter.Append('=');
						sbyte[] array = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(array));
						break;
					}
					case 4:
					{
						filter.Append((string)itr.Current);
						filter.Append('=');
						bool flag = false;
						while (itr.MoveNext())
						{
							switch ((int)itr.Current)
							{
							case 0:
								filter.Append((string)itr.Current);
								filter.Append('*');
								flag = false;
								break;
							case 1:
								if (flag)
								{
									filter.Append('*');
								}
								filter.Append((string)itr.Current);
								filter.Append('*');
								flag = false;
								break;
							case 2:
								if (flag)
								{
									filter.Append('*');
								}
								filter.Append((string)itr.Current);
								break;
							}
						}
						break;
					}
					case 5:
					{
						filter.Append((string)itr.Current);
						filter.Append(">=");
						sbyte[] array2 = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(array2));
						break;
					}
					case 6:
					{
						filter.Append((string)itr.Current);
						filter.Append("<=");
						sbyte[] array3 = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(array3));
						break;
					}
					case 7:
						filter.Append((string)itr.Current);
						filter.Append("=*");
						break;
					case 8:
					{
						filter.Append((string)itr.Current);
						filter.Append("~=");
						sbyte[] array4 = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(array4));
						break;
					}
					case 9:
					{
						string text = (string)itr.Current;
						filter.Append((string)itr.Current);
						filter.Append(':');
						filter.Append(text);
						filter.Append(":=");
						filter.Append((string)itr.Current);
						break;
					}
					}
				}
				else if (obj is IEnumerator)
				{
					RfcFilter.stringFilter((IEnumerator)obj, filter);
				}
			}
			filter.Append(')');
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00012CA8 File Offset: 0x00010EA8
		private static string byteString(sbyte[] value_Renamed)
		{
			string text = null;
			if (Base64.isValidUTF8(value_Renamed, true))
			{
				try
				{
					return new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray(value_Renamed)));
				}
				catch (IOException ex)
				{
					string text2 = "Default JVM does not support UTF-8 encoding";
					IOException ex2 = ex;
					throw new SystemException(text2 + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value_Renamed.Length; i++)
			{
				if (value_Renamed[i] >= 0)
				{
					stringBuilder.Append("\\0");
					stringBuilder.Append(Convert.ToString((short)value_Renamed[i], 16));
				}
				else
				{
					stringBuilder.Append("\\" + Convert.ToString((short)value_Renamed[i], 16).Substring(6));
				}
			}
			text = stringBuilder.ToString();
			return text;
		}

		// Token: 0x0400025E RID: 606
		public const int AND = 0;

		// Token: 0x0400025F RID: 607
		public const int OR = 1;

		// Token: 0x04000260 RID: 608
		public const int NOT = 2;

		// Token: 0x04000261 RID: 609
		public const int EQUALITY_MATCH = 3;

		// Token: 0x04000262 RID: 610
		public const int SUBSTRINGS = 4;

		// Token: 0x04000263 RID: 611
		public const int GREATER_OR_EQUAL = 5;

		// Token: 0x04000264 RID: 612
		public const int LESS_OR_EQUAL = 6;

		// Token: 0x04000265 RID: 613
		public const int PRESENT = 7;

		// Token: 0x04000266 RID: 614
		public const int APPROX_MATCH = 8;

		// Token: 0x04000267 RID: 615
		public const int EXTENSIBLE_MATCH = 9;

		// Token: 0x04000268 RID: 616
		public const int INITIAL = 0;

		// Token: 0x04000269 RID: 617
		public const int ANY = 1;

		// Token: 0x0400026A RID: 618
		public const int FINAL = 2;

		// Token: 0x0400026B RID: 619
		private RfcFilter.FilterTokenizer ft;

		// Token: 0x0400026C RID: 620
		private Stack filterStack;

		// Token: 0x0400026D RID: 621
		private bool finalFound;

		// Token: 0x0200007D RID: 125
		private class FilterIterator : IEnumerator
		{
			// Token: 0x06000428 RID: 1064 RVA: 0x00002A00 File Offset: 0x00000C00
			public void Reset()
			{
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x00012D70 File Offset: 0x00010F70
			private void InitBlock(RfcFilter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x0600042A RID: 1066 RVA: 0x00012D7C File Offset: 0x00010F7C
			public virtual object Current
			{
				get
				{
					object obj = null;
					if (!this.tagReturned)
					{
						this.tagReturned = true;
						obj = this.root.getIdentifier().Tag;
					}
					else
					{
						Asn1Object asn1Object = this.root.taggedValue();
						if (asn1Object is RfcLdapString)
						{
							this.hasMore = false;
							obj = ((RfcLdapString)asn1Object).stringValue();
						}
						else if (asn1Object is RfcSubstringFilter)
						{
							RfcSubstringFilter rfcSubstringFilter = (RfcSubstringFilter)asn1Object;
							if (this.index == -1)
							{
								this.index = 0;
								obj = ((RfcAttributeDescription)rfcSubstringFilter.get_Renamed(0)).stringValue();
							}
							else if (this.index % 2 == 0)
							{
								obj = ((Asn1Tagged)((Asn1SequenceOf)rfcSubstringFilter.get_Renamed(1)).get_Renamed(this.index / 2)).getIdentifier().Tag;
								this.index++;
							}
							else
							{
								obj = ((RfcLdapString)((Asn1Tagged)((Asn1SequenceOf)rfcSubstringFilter.get_Renamed(1)).get_Renamed(this.index / 2)).taggedValue()).stringValue();
								this.index++;
							}
							if (this.index / 2 >= ((Asn1SequenceOf)rfcSubstringFilter.get_Renamed(1)).size())
							{
								this.hasMore = false;
							}
						}
						else if (asn1Object is RfcAttributeValueAssertion)
						{
							RfcAttributeValueAssertion rfcAttributeValueAssertion = (RfcAttributeValueAssertion)asn1Object;
							if (this.index == -1)
							{
								obj = rfcAttributeValueAssertion.AttributeDescription;
								this.index = 1;
							}
							else if (this.index == 1)
							{
								obj = rfcAttributeValueAssertion.AssertionValue;
								this.index = 2;
								this.hasMore = false;
							}
						}
						else if (asn1Object is RfcMatchingRuleAssertion)
						{
							Asn1Structured asn1Structured = (RfcMatchingRuleAssertion)asn1Object;
							if (this.index == -1)
							{
								this.index = 0;
							}
							int num = this.index;
							this.index = num + 1;
							obj = ((Asn1OctetString)((Asn1Tagged)asn1Structured.get_Renamed(num)).taggedValue()).stringValue();
							if (this.index > 2)
							{
								this.hasMore = false;
							}
						}
						else if (asn1Object is Asn1SetOf)
						{
							Asn1SetOf asn1SetOf = (Asn1SetOf)asn1Object;
							if (this.index == -1)
							{
								this.index = 0;
							}
							RfcFilter rfcFilter = this.enclosingInstance;
							Asn1Structured asn1Structured2 = asn1SetOf;
							int num = this.index;
							this.index = num + 1;
							obj = new RfcFilter.FilterIterator(rfcFilter, (Asn1Tagged)asn1Structured2.get_Renamed(num));
							if (this.index >= asn1SetOf.size())
							{
								this.hasMore = false;
							}
						}
						else if (asn1Object is Asn1Tagged)
						{
							obj = new RfcFilter.FilterIterator(this.enclosingInstance, (Asn1Tagged)asn1Object);
							this.hasMore = false;
						}
					}
					return obj;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x00013003 File Offset: 0x00011203
			public RfcFilter Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x0001300B File Offset: 0x0001120B
			public FilterIterator(RfcFilter enclosingInstance, Asn1Tagged root)
			{
				this.InitBlock(enclosingInstance);
				this.root = root;
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x0001302F File Offset: 0x0001122F
			public virtual bool MoveNext()
			{
				return this.hasMore;
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x00013037 File Offset: 0x00011237
			public void remove()
			{
				throw new NotSupportedException("Remove is not supported on a filter iterator");
			}

			// Token: 0x0400026E RID: 622
			private RfcFilter enclosingInstance;

			// Token: 0x0400026F RID: 623
			internal Asn1Tagged root;

			// Token: 0x04000270 RID: 624
			internal bool tagReturned;

			// Token: 0x04000271 RID: 625
			internal int index = -1;

			// Token: 0x04000272 RID: 626
			private bool hasMore = true;
		}

		// Token: 0x0200007E RID: 126
		internal class FilterTokenizer
		{
			// Token: 0x0600042F RID: 1071 RVA: 0x00013043 File Offset: 0x00011243
			private void InitBlock(RfcFilter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001304C File Offset: 0x0001124C
			public virtual int OpOrAttr
			{
				get
				{
					if (this.offset >= this.filterLength)
					{
						throw new LdapLocalException("UNEXPECTED_END", 87);
					}
					int num = (int)this.filter[this.offset];
					int num2;
					if (num == 38)
					{
						this.offset++;
						num2 = 0;
					}
					else if (num == 124)
					{
						this.offset++;
						num2 = 1;
					}
					else if (num == 33)
					{
						this.offset++;
						num2 = 2;
					}
					else
					{
						if (this.filter.Substring(this.offset).StartsWith(":="))
						{
							throw new LdapLocalException("NO_MATCHING_RULE", 87);
						}
						if (this.filter.Substring(this.offset).StartsWith("::=") || this.filter.Substring(this.offset).StartsWith(":::="))
						{
							throw new LdapLocalException("NO_DN_NOR_MATCHING_RULE", 87);
						}
						string text = "=~<>()";
						StringBuilder stringBuilder = new StringBuilder();
						while (text.IndexOf(this.filter[this.offset]) == -1 && !this.filter.Substring(this.offset).StartsWith(":="))
						{
							StringBuilder stringBuilder2 = stringBuilder;
							string text2 = this.filter;
							int num3 = this.offset;
							this.offset = num3 + 1;
							stringBuilder2.Append(text2[num3]);
						}
						this.attr = stringBuilder.ToString().Trim();
						if (this.attr.Length == 0 || this.attr[0] == ';')
						{
							throw new LdapLocalException("NO_ATTRIBUTE_NAME", 87);
						}
						int i = 0;
						while (i < this.attr.Length)
						{
							char c = this.attr[i];
							if (!char.IsLetterOrDigit(c) && c != '-' && c != '.' && c != ';' && c != ':')
							{
								if (c == '\\')
								{
									throw new LdapLocalException("INVALID_ESC_IN_DESCR", 87);
								}
								throw new LdapLocalException("INVALID_CHAR_IN_DESCR", new object[] { c }, 87);
							}
							else
							{
								i++;
							}
						}
						i = this.attr.IndexOf(';');
						if (i != -1 && i == this.attr.Length - 1)
						{
							throw new LdapLocalException("NO_OPTION", 87);
						}
						num2 = -1;
					}
					return num2;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x00013298 File Offset: 0x00011498
			public virtual int FilterType
			{
				get
				{
					if (this.offset >= this.filterLength)
					{
						throw new LdapLocalException("UNEXPECTED_END", 87);
					}
					int num;
					if (this.filter.Substring(this.offset).StartsWith(">="))
					{
						this.offset += 2;
						num = 5;
					}
					else if (this.filter.Substring(this.offset).StartsWith("<="))
					{
						this.offset += 2;
						num = 6;
					}
					else if (this.filter.Substring(this.offset).StartsWith("~="))
					{
						this.offset += 2;
						num = 8;
					}
					else if (this.filter.Substring(this.offset).StartsWith(":="))
					{
						this.offset += 2;
						num = 9;
					}
					else
					{
						if (this.filter[this.offset] != '=')
						{
							throw new LdapLocalException("INVALID_FILTER_COMPARISON", 87);
						}
						this.offset++;
						num = 3;
					}
					return num;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x000133B8 File Offset: 0x000115B8
			public virtual string Value
			{
				get
				{
					if (this.offset >= this.filterLength)
					{
						throw new LdapLocalException("UNEXPECTED_END", 87);
					}
					int num = this.filter.IndexOf(')', this.offset);
					if (num == -1)
					{
						num = this.filterLength;
					}
					string text = this.filter.Substring(this.offset, num - this.offset);
					this.offset = num;
					return text;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001341F File Offset: 0x0001161F
			public virtual string Attr
			{
				get
				{
					return this.attr;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x00013427 File Offset: 0x00011627
			public RfcFilter Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x0001342F File Offset: 0x0001162F
			public FilterTokenizer(RfcFilter enclosingInstance, string filter)
			{
				this.InitBlock(enclosingInstance);
				this.filter = filter;
				this.offset = 0;
				this.filterLength = filter.Length;
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x00013458 File Offset: 0x00011658
			public void getLeftParen()
			{
				if (this.offset >= this.filterLength)
				{
					throw new LdapLocalException("UNEXPECTED_END", 87);
				}
				string text = this.filter;
				int num = this.offset;
				this.offset = num + 1;
				if (text[num] != '(')
				{
					throw new LdapLocalException("EXPECTING_LEFT_PAREN", new object[] { this.filter[--this.offset] }, 87);
				}
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x000134D8 File Offset: 0x000116D8
			public void getRightParen()
			{
				if (this.offset >= this.filterLength)
				{
					throw new LdapLocalException("UNEXPECTED_END", 87);
				}
				string text = this.filter;
				int num = this.offset;
				this.offset = num + 1;
				if (text[num] != ')')
				{
					throw new LdapLocalException("EXPECTING_RIGHT_PAREN", new object[] { this.filter[this.offset - 1] }, 87);
				}
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x0001354E File Offset: 0x0001174E
			public char peekChar()
			{
				if (this.offset >= this.filterLength)
				{
					throw new LdapLocalException("UNEXPECTED_END", 87);
				}
				return this.filter[this.offset];
			}

			// Token: 0x04000273 RID: 627
			private RfcFilter enclosingInstance;

			// Token: 0x04000274 RID: 628
			private string filter;

			// Token: 0x04000275 RID: 629
			private string attr;

			// Token: 0x04000276 RID: 630
			private int offset;

			// Token: 0x04000277 RID: 631
			private int filterLength;
		}
	}
}
