using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000135 RID: 309
	internal sealed class RegexCharClass
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x0001E820 File Offset: 0x0001CA20
		public RegexCharClass()
		{
			this._rangelist = new List<RegexCharClass.SingleRange>(6);
			this._canonical = true;
			this._categories = new StringBuilder();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001E846 File Offset: 0x0001CA46
		private RegexCharClass(bool negate, List<RegexCharClass.SingleRange> ranges, StringBuilder categories, RegexCharClass subtraction)
		{
			this._rangelist = ranges;
			this._categories = categories;
			this._canonical = true;
			this._negate = negate;
			this._subtractor = subtraction;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001E872 File Offset: 0x0001CA72
		public bool CanMerge
		{
			get
			{
				return !this._negate && this._subtractor == null;
			}
		}

		// Token: 0x17000117 RID: 279
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0001E887 File Offset: 0x0001CA87
		public bool Negate
		{
			set
			{
				this._negate = value;
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001E890 File Offset: 0x0001CA90
		public void AddChar(char c)
		{
			this.AddRange(c, c);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001E89C File Offset: 0x0001CA9C
		public void AddCharClass(RegexCharClass cc)
		{
			if (!cc._canonical)
			{
				this._canonical = false;
			}
			else if (this._canonical && this.RangeCount() > 0 && cc.RangeCount() > 0 && cc.GetRangeAt(0).First <= this.GetRangeAt(this.RangeCount() - 1).Last)
			{
				this._canonical = false;
			}
			for (int i = 0; i < cc.RangeCount(); i++)
			{
				this._rangelist.Add(cc.GetRangeAt(i));
			}
			this._categories.Append(cc._categories.ToString());
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001E938 File Offset: 0x0001CB38
		private void AddSet(string set)
		{
			if (this._canonical && this.RangeCount() > 0 && set.Length > 0 && set[0] <= this.GetRangeAt(this.RangeCount() - 1).Last)
			{
				this._canonical = false;
			}
			int i;
			for (i = 0; i < set.Length - 1; i += 2)
			{
				this._rangelist.Add(new RegexCharClass.SingleRange(set[i], set[i + 1] - '\u0001'));
			}
			if (i < set.Length)
			{
				this._rangelist.Add(new RegexCharClass.SingleRange(set[i], char.MaxValue));
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001E9DD File Offset: 0x0001CBDD
		public void AddSubtraction(RegexCharClass sub)
		{
			this._subtractor = sub;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001E9E8 File Offset: 0x0001CBE8
		public void AddRange(char first, char last)
		{
			this._rangelist.Add(new RegexCharClass.SingleRange(first, last));
			if (this._canonical && this._rangelist.Count > 0 && first <= this._rangelist[this._rangelist.Count - 1].Last)
			{
				this._canonical = false;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001EA44 File Offset: 0x0001CC44
		public void AddCategoryFromName(string categoryName, bool invert, bool caseInsensitive, string pattern)
		{
			string text;
			if (RegexCharClass.s_definedCategories.TryGetValue(categoryName, out text) && !categoryName.Equals(RegexCharClass.s_internalRegexIgnoreCase))
			{
				if (caseInsensitive && (categoryName.Equals("Ll") || categoryName.Equals("Lu") || categoryName.Equals("Lt")))
				{
					text = RegexCharClass.s_definedCategories[RegexCharClass.s_internalRegexIgnoreCase];
				}
				if (invert)
				{
					text = RegexCharClass.NegateCategory(text);
				}
				this._categories.Append(text);
				return;
			}
			this.AddSet(RegexCharClass.SetFromProperty(categoryName, invert, pattern));
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001EACE File Offset: 0x0001CCCE
		private void AddCategory(string category)
		{
			this._categories.Append(category);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		public void AddLowercase(CultureInfo culture)
		{
			this._canonical = false;
			int count = this._rangelist.Count;
			for (int i = 0; i < count; i++)
			{
				RegexCharClass.SingleRange singleRange = this._rangelist[i];
				if (singleRange.First == singleRange.Last)
				{
					char c = culture.TextInfo.ToLower(singleRange.First);
					this._rangelist[i] = new RegexCharClass.SingleRange(c, c);
				}
				else
				{
					this.AddLowercaseRange(singleRange.First, singleRange.Last, culture);
				}
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001EB64 File Offset: 0x0001CD64
		private void AddLowercaseRange(char chMin, char chMax, CultureInfo culture)
		{
			int i = 0;
			int num = RegexCharClass.s_lcTable.Length;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				if (RegexCharClass.s_lcTable[num2].ChMax < chMin)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2;
				}
			}
			if (i >= RegexCharClass.s_lcTable.Length)
			{
				return;
			}
			RegexCharClass.LowerCaseMapping lowerCaseMapping;
			while (i < RegexCharClass.s_lcTable.Length && (lowerCaseMapping = RegexCharClass.s_lcTable[i]).ChMin <= chMax)
			{
				char c;
				if ((c = lowerCaseMapping.ChMin) < chMin)
				{
					c = chMin;
				}
				char c2;
				if ((c2 = lowerCaseMapping.ChMax) > chMax)
				{
					c2 = chMax;
				}
				switch (lowerCaseMapping.LcOp)
				{
				case 0:
					c = (char)lowerCaseMapping.Data;
					c2 = (char)lowerCaseMapping.Data;
					break;
				case 1:
					c += (char)lowerCaseMapping.Data;
					c2 += (char)lowerCaseMapping.Data;
					break;
				case 2:
					c |= '\u0001';
					c2 |= '\u0001';
					break;
				case 3:
					c += c & '\u0001';
					c2 += c2 & '\u0001';
					break;
				}
				if (c < chMin || c2 > chMax)
				{
					this.AddRange(c, c2);
				}
				i++;
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001EC7B File Offset: 0x0001CE7B
		public void AddWord(bool ecma, bool negate)
		{
			if (negate)
			{
				if (ecma)
				{
					this.AddSet("\00:A[_`a{İı");
					return;
				}
				this.AddCategory(RegexCharClass.s_notWord);
				return;
			}
			else
			{
				if (ecma)
				{
					this.AddSet("0:A[_`a{İı");
					return;
				}
				this.AddCategory(RegexCharClass.s_word);
				return;
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001ECB5 File Offset: 0x0001CEB5
		public void AddSpace(bool ecma, bool negate)
		{
			if (negate)
			{
				if (ecma)
				{
					this.AddSet("\0\t\u000e !");
					return;
				}
				this.AddCategory(RegexCharClass.s_notSpace);
				return;
			}
			else
			{
				if (ecma)
				{
					this.AddSet("\t\u000e !");
					return;
				}
				this.AddCategory(RegexCharClass.s_space);
				return;
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001ECEF File Offset: 0x0001CEEF
		public void AddDigit(bool ecma, bool negate, string pattern)
		{
			if (!ecma)
			{
				this.AddCategoryFromName("Nd", negate, false, pattern);
				return;
			}
			if (negate)
			{
				this.AddSet("\00:");
				return;
			}
			this.AddSet("0:");
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001ED20 File Offset: 0x0001CF20
		public static string ConvertOldStringsToClass(string set, string category)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(set.Length + category.Length + 3);
			if (set.Length >= 2 && set[0] == '\0' && set[1] == '\0')
			{
				stringBuilder.Append('\u0001');
				stringBuilder.Append((char)(set.Length - 2));
				stringBuilder.Append((char)category.Length);
				stringBuilder.Append(set.Substring(2));
			}
			else
			{
				stringBuilder.Append('\0');
				stringBuilder.Append((char)set.Length);
				stringBuilder.Append((char)category.Length);
				stringBuilder.Append(set);
			}
			stringBuilder.Append(category);
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001EDCD File Offset: 0x0001CFCD
		public static char SingletonChar(string set)
		{
			return set[3];
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001EDD6 File Offset: 0x0001CFD6
		public static bool IsMergeable(string charClass)
		{
			return !RegexCharClass.IsNegated(charClass) && !RegexCharClass.IsSubtraction(charClass);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001EDEB File Offset: 0x0001CFEB
		public static bool IsEmpty(string charClass)
		{
			return charClass[2] == '\0' && charClass[0] == '\0' && charClass[1] == '\0' && !RegexCharClass.IsSubtraction(charClass);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001EE14 File Offset: 0x0001D014
		public static bool IsSingleton(string set)
		{
			return set[0] == '\0' && set[2] == '\0' && set[1] == '\u0002' && !RegexCharClass.IsSubtraction(set) && (set[3] == char.MaxValue || set[3] + '\u0001' == set[4]);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001EE68 File Offset: 0x0001D068
		public static bool IsSingletonInverse(string set)
		{
			return set[0] == '\u0001' && set[2] == '\0' && set[1] == '\u0002' && !RegexCharClass.IsSubtraction(set) && (set[3] == char.MaxValue || set[3] + '\u0001' == set[4]);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001EEBD File Offset: 0x0001D0BD
		private static bool IsSubtraction(string charClass)
		{
			return charClass.Length > (int)('\u0003' + charClass[1] + charClass[2]);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001EED8 File Offset: 0x0001D0D8
		private static bool IsNegated(string set)
		{
			return set != null && set[0] == '\u0001';
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001EEE9 File Offset: 0x0001D0E9
		public static bool IsECMAWordChar(char ch)
		{
			return RegexCharClass.CharInClass(ch, "\0\n\00:A[_`a{İı");
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001EEF6 File Offset: 0x0001D0F6
		public static bool IsWordChar(char ch)
		{
			return RegexCharClass.CharInClass(ch, RegexCharClass.WordClass) || ch == '\u200d' || ch == '\u200c';
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001EF17 File Offset: 0x0001D117
		public static bool CharInClass(char ch, string set)
		{
			return RegexCharClass.CharInClassRecursive(ch, set, 0);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001EF24 File Offset: 0x0001D124
		private static bool CharInClassRecursive(char ch, string set, int start)
		{
			int num = (int)set[start + 1];
			int num2 = (int)set[start + 2];
			int num3 = start + 3 + num + num2;
			bool flag = false;
			if (set.Length > num3)
			{
				flag = RegexCharClass.CharInClassRecursive(ch, set, num3);
			}
			bool flag2 = RegexCharClass.CharInClassInternal(ch, set, start, num, num2);
			if (set[start] == '\u0001')
			{
				flag2 = !flag2;
			}
			return flag2 && !flag;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001EF88 File Offset: 0x0001D188
		private static bool CharInClassInternal(char ch, string set, int start, int mySetLength, int myCategoryLength)
		{
			int num = start + 3;
			int num2 = num + mySetLength;
			while (num != num2)
			{
				int num3 = (num + num2) / 2;
				if (ch < set[num3])
				{
					num2 = num3;
				}
				else
				{
					num = num3 + 1;
				}
			}
			return (num & 1) == (start & 1) || (myCategoryLength != 0 && RegexCharClass.CharInCategory(ch, set, start, mySetLength, myCategoryLength));
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		private static bool CharInCategory(char ch, string set, int start, int mySetLength, int myCategoryLength)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(ch);
			int i = start + 3 + mySetLength;
			int num = i + myCategoryLength;
			while (i < num)
			{
				int num2 = (int)((short)set[i]);
				if (num2 == 0)
				{
					if (RegexCharClass.CharInCategoryGroup(ch, unicodeCategory, set, ref i))
					{
						return true;
					}
				}
				else if (num2 > 0)
				{
					if (num2 == 100)
					{
						if (char.IsWhiteSpace(ch))
						{
							return true;
						}
						i++;
						continue;
					}
					else
					{
						num2--;
						if (unicodeCategory == (UnicodeCategory)num2)
						{
							return true;
						}
					}
				}
				else if (num2 == -100)
				{
					if (!char.IsWhiteSpace(ch))
					{
						return true;
					}
					i++;
					continue;
				}
				else
				{
					num2 = -1 - num2;
					if (unicodeCategory != (UnicodeCategory)num2)
					{
						return true;
					}
				}
				i++;
			}
			return false;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001F060 File Offset: 0x0001D260
		private static bool CharInCategoryGroup(char ch, UnicodeCategory chcategory, string category, ref int i)
		{
			i++;
			int num = (int)((short)category[i]);
			if (num > 0)
			{
				bool flag = false;
				while (num != 0)
				{
					if (!flag)
					{
						num--;
						if (chcategory == (UnicodeCategory)num)
						{
							flag = true;
						}
					}
					i++;
					num = (int)((short)category[i]);
				}
				return flag;
			}
			bool flag2 = true;
			while (num != 0)
			{
				if (flag2)
				{
					num = -1 - num;
					if (chcategory == (UnicodeCategory)num)
					{
						flag2 = false;
					}
				}
				i++;
				num = (int)((short)category[i]);
			}
			return flag2;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001F0CC File Offset: 0x0001D2CC
		private static string NegateCategory(string category)
		{
			if (category == null)
			{
				return null;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(category.Length);
			foreach (short num in category)
			{
				stringBuilder.Append((char)(-(char)num));
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001F114 File Offset: 0x0001D314
		public static RegexCharClass Parse(string charClass)
		{
			return RegexCharClass.ParseRecursive(charClass, 0);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001F120 File Offset: 0x0001D320
		private static RegexCharClass ParseRecursive(string charClass, int start)
		{
			int num = (int)charClass[start + 1];
			int num2 = (int)charClass[start + 2];
			int num3 = start + 3 + num + num2;
			List<RegexCharClass.SingleRange> list = new List<RegexCharClass.SingleRange>(num);
			int i = start + 3;
			int num4 = i + num;
			while (i < num4)
			{
				char c = charClass[i];
				i++;
				char c2;
				if (i < num4)
				{
					c2 = charClass[i] - '\u0001';
				}
				else
				{
					c2 = char.MaxValue;
				}
				i++;
				list.Add(new RegexCharClass.SingleRange(c, c2));
			}
			RegexCharClass regexCharClass = null;
			if (charClass.Length > num3)
			{
				regexCharClass = RegexCharClass.ParseRecursive(charClass, num3);
			}
			return new RegexCharClass(charClass[start] == '\u0001', list, new StringBuilder(charClass.Substring(num4, num2)), regexCharClass);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001F1D9 File Offset: 0x0001D3D9
		private int RangeCount()
		{
			return this._rangelist.Count;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001F1E8 File Offset: 0x0001D3E8
		public string ToStringClass()
		{
			if (!this._canonical)
			{
				this.Canonicalize();
			}
			int num = this._rangelist.Count * 2;
			StringBuilder stringBuilder = StringBuilderCache.Acquire(num + this._categories.Length + 3);
			int num2;
			if (this._negate)
			{
				num2 = 1;
			}
			else
			{
				num2 = 0;
			}
			stringBuilder.Append((char)num2);
			stringBuilder.Append((char)num);
			stringBuilder.Append((char)this._categories.Length);
			for (int i = 0; i < this._rangelist.Count; i++)
			{
				RegexCharClass.SingleRange singleRange = this._rangelist[i];
				stringBuilder.Append(singleRange.First);
				if (singleRange.Last != '\uffff')
				{
					stringBuilder.Append(singleRange.Last + '\u0001');
				}
			}
			stringBuilder[1] = (char)(stringBuilder.Length - 3);
			stringBuilder.Append(this._categories);
			if (this._subtractor != null)
			{
				stringBuilder.Append(this._subtractor.ToStringClass());
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001F2E7 File Offset: 0x0001D4E7
		private RegexCharClass.SingleRange GetRangeAt(int i)
		{
			return this._rangelist[i];
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001F2F8 File Offset: 0x0001D4F8
		private void Canonicalize()
		{
			this._canonical = true;
			this._rangelist.Sort(RegexCharClass.SingleRangeComparer.Instance);
			if (this._rangelist.Count > 1)
			{
				bool flag = false;
				int num = 1;
				int num2 = 0;
				for (;;)
				{
					IL_002F:
					char c = this._rangelist[num2].Last;
					while (num != this._rangelist.Count && c != '\uffff')
					{
						RegexCharClass.SingleRange singleRange;
						if ((singleRange = this._rangelist[num]).First <= c + '\u0001')
						{
							if (c < singleRange.Last)
							{
								c = singleRange.Last;
							}
							num++;
						}
						else
						{
							IL_008A:
							this._rangelist[num2] = new RegexCharClass.SingleRange(this._rangelist[num2].First, c);
							num2++;
							if (!flag)
							{
								if (num2 < num)
								{
									this._rangelist[num2] = this._rangelist[num];
								}
								num++;
								goto IL_002F;
							}
							goto IL_00DA;
						}
					}
					flag = true;
					goto IL_008A;
				}
				IL_00DA:
				this._rangelist.RemoveRange(num2, this._rangelist.Count - num2);
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		private static string SetFromProperty(string capname, bool invert, string pattern)
		{
			int num = 0;
			int num2 = RegexCharClass.s_propTable.Length;
			while (num != num2)
			{
				int num3 = (num + num2) / 2;
				int num4 = string.Compare(capname, RegexCharClass.s_propTable[num3][0], StringComparison.Ordinal);
				if (num4 < 0)
				{
					num2 = num3;
				}
				else if (num4 > 0)
				{
					num = num3 + 1;
				}
				else
				{
					string text = RegexCharClass.s_propTable[num3][1];
					if (!invert)
					{
						return text;
					}
					if (text[0] == '\0')
					{
						return text.Substring(1);
					}
					return "\0" + text;
				}
			}
			throw new ArgumentException(SR.Format("parsing \"{0}\" - {1}", pattern, SR.Format("Unknown property '{0}'.", capname)));
		}

		// Token: 0x04000514 RID: 1300
		private static readonly string s_internalRegexIgnoreCase = "__InternalRegexIgnoreCase__";

		// Token: 0x04000515 RID: 1301
		private static readonly string s_space = "d";

		// Token: 0x04000516 RID: 1302
		private static readonly string s_notSpace = "ﾜ";

		// Token: 0x04000517 RID: 1303
		private static readonly string s_word = "\0\u0002\u0004\u0005\u0003\u0001\u0006\t\u0013\0";

		// Token: 0x04000518 RID: 1304
		private static readonly string s_notWord = "\0\ufffe￼\ufffb\ufffd\uffff\ufffa\ufff7￭\0";

		// Token: 0x04000519 RID: 1305
		public static readonly string SpaceClass = "\0\0\u0001d";

		// Token: 0x0400051A RID: 1306
		public static readonly string NotSpaceClass = "\u0001\0\u0001d";

		// Token: 0x0400051B RID: 1307
		public static readonly string WordClass = "\0\0\n\0\u0002\u0004\u0005\u0003\u0001\u0006\t\u0013\0";

		// Token: 0x0400051C RID: 1308
		public static readonly string NotWordClass = "\u0001\0\n\0\u0002\u0004\u0005\u0003\u0001\u0006\t\u0013\0";

		// Token: 0x0400051D RID: 1309
		public static readonly string DigitClass = "\0\0\u0001\t";

		// Token: 0x0400051E RID: 1310
		public static readonly string NotDigitClass = "\0\0\u0001\ufff7";

		// Token: 0x0400051F RID: 1311
		private static readonly Dictionary<string, string> s_definedCategories = new Dictionary<string, string>(38)
		{
			{ "Cc", "\u000f" },
			{ "Cf", "\u0010" },
			{ "Cn", "\u001e" },
			{ "Co", "\u0012" },
			{ "Cs", "\u0011" },
			{ "C", "\0\u000f\u0010\u001e\u0012\u0011\0" },
			{ "Ll", "\u0002" },
			{ "Lm", "\u0004" },
			{ "Lo", "\u0005" },
			{ "Lt", "\u0003" },
			{ "Lu", "\u0001" },
			{ "L", "\0\u0002\u0004\u0005\u0003\u0001\0" },
			{ "__InternalRegexIgnoreCase__", "\0\u0002\u0003\u0001\0" },
			{ "Mc", "\a" },
			{ "Me", "\b" },
			{ "Mn", "\u0006" },
			{ "M", "\0\a\b\u0006\0" },
			{ "Nd", "\t" },
			{ "Nl", "\n" },
			{ "No", "\v" },
			{ "N", "\0\t\n\v\0" },
			{ "Pc", "\u0013" },
			{ "Pd", "\u0014" },
			{ "Pe", "\u0016" },
			{ "Po", "\u0019" },
			{ "Ps", "\u0015" },
			{ "Pf", "\u0018" },
			{ "Pi", "\u0017" },
			{ "P", "\0\u0013\u0014\u0016\u0019\u0015\u0018\u0017\0" },
			{ "Sc", "\u001b" },
			{ "Sk", "\u001c" },
			{ "Sm", "\u001a" },
			{ "So", "\u001d" },
			{ "S", "\0\u001b\u001c\u001a\u001d\0" },
			{ "Zl", "\r" },
			{ "Zp", "\u000e" },
			{ "Zs", "\f" },
			{ "Z", "\0\r\u000e\f\0" }
		};

		// Token: 0x04000520 RID: 1312
		private static readonly string[][] s_propTable = new string[][]
		{
			new string[] { "IsAlphabeticPresentationForms", "ﬀﭐ" },
			new string[] { "IsArabic", "\u0600܀" },
			new string[] { "IsArabicPresentationForms-A", "ﭐ\ufe00" },
			new string[] { "IsArabicPresentationForms-B", "ﹰ\uff00" },
			new string[] { "IsArmenian", "\u0530\u0590" },
			new string[] { "IsArrows", "←∀" },
			new string[] { "IsBasicLatin", "\0\u0080" },
			new string[] { "IsBengali", "ঀ\u0a00" },
			new string[] { "IsBlockElements", "▀■" },
			new string[] { "IsBopomofo", "\u3100\u3130" },
			new string[] { "IsBopomofoExtended", "ㆠ㇀" },
			new string[] { "IsBoxDrawing", "─▀" },
			new string[] { "IsBraillePatterns", "⠀⤀" },
			new string[] { "IsBuhid", "ᝀᝠ" },
			new string[] { "IsCJKCompatibility", "㌀㐀" },
			new string[] { "IsCJKCompatibilityForms", "︰﹐" },
			new string[] { "IsCJKCompatibilityIdeographs", "豈ﬀ" },
			new string[] { "IsCJKRadicalsSupplement", "⺀⼀" },
			new string[] { "IsCJKSymbolsandPunctuation", "\u3000\u3040" },
			new string[] { "IsCJKUnifiedIdeographs", "一ꀀ" },
			new string[] { "IsCJKUnifiedIdeographsExtensionA", "㐀䷀" },
			new string[] { "IsCherokee", "Ꭰ᐀" },
			new string[] { "IsCombiningDiacriticalMarks", "\u0300Ͱ" },
			new string[] { "IsCombiningDiacriticalMarksforSymbols", "\u20d0℀" },
			new string[] { "IsCombiningHalfMarks", "\ufe20︰" },
			new string[] { "IsCombiningMarksforSymbols", "\u20d0℀" },
			new string[] { "IsControlPictures", "␀⑀" },
			new string[] { "IsCurrencySymbols", "₠\u20d0" },
			new string[] { "IsCyrillic", "ЀԀ" },
			new string[] { "IsCyrillicSupplement", "Ԁ\u0530" },
			new string[] { "IsDevanagari", "\u0900ঀ" },
			new string[] { "IsDingbats", "✀⟀" },
			new string[] { "IsEnclosedAlphanumerics", "①─" },
			new string[] { "IsEnclosedCJKLettersandMonths", "㈀㌀" },
			new string[] { "IsEthiopic", "ሀᎀ" },
			new string[] { "IsGeneralPunctuation", "\u2000⁰" },
			new string[] { "IsGeometricShapes", "■☀" },
			new string[] { "IsGeorgian", "Ⴀᄀ" },
			new string[] { "IsGreek", "ͰЀ" },
			new string[] { "IsGreekExtended", "ἀ\u2000" },
			new string[] { "IsGreekandCoptic", "ͰЀ" },
			new string[] { "IsGujarati", "\u0a80\u0b00" },
			new string[] { "IsGurmukhi", "\u0a00\u0a80" },
			new string[] { "IsHalfwidthandFullwidthForms", "\uff00\ufff0" },
			new string[] { "IsHangulCompatibilityJamo", "\u3130㆐" },
			new string[] { "IsHangulJamo", "ᄀሀ" },
			new string[] { "IsHangulSyllables", "가ힰ" },
			new string[] { "IsHanunoo", "ᜠᝀ" },
			new string[] { "IsHebrew", "\u0590\u0600" },
			new string[] { "IsHighPrivateUseSurrogates", "\udb80\udc00" },
			new string[] { "IsHighSurrogates", "\ud800\udb80" },
			new string[] { "IsHiragana", "\u3040゠" },
			new string[] { "IsIPAExtensions", "ɐʰ" },
			new string[] { "IsIdeographicDescriptionCharacters", "⿰\u3000" },
			new string[] { "IsKanbun", "㆐ㆠ" },
			new string[] { "IsKangxiRadicals", "⼀\u2fe0" },
			new string[] { "IsKannada", "ಀ\u0d00" },
			new string[] { "IsKatakana", "゠\u3100" },
			new string[] { "IsKatakanaPhoneticExtensions", "ㇰ㈀" },
			new string[] { "IsKhmer", "ក᠀" },
			new string[] { "IsKhmerSymbols", "᧠ᨀ" },
			new string[] { "IsLao", "\u0e80ༀ" },
			new string[] { "IsLatin-1Supplement", "\u0080Ā" },
			new string[] { "IsLatinExtended-A", "Āƀ" },
			new string[] { "IsLatinExtended-B", "ƀɐ" },
			new string[] { "IsLatinExtendedAdditional", "Ḁἀ" },
			new string[] { "IsLetterlikeSymbols", "℀⅐" },
			new string[] { "IsLimbu", "ᤀᥐ" },
			new string[] { "IsLowSurrogates", "\udc00\ue000" },
			new string[] { "IsMalayalam", "\u0d00\u0d80" },
			new string[] { "IsMathematicalOperators", "∀⌀" },
			new string[] { "IsMiscellaneousMathematicalSymbols-A", "⟀⟰" },
			new string[] { "IsMiscellaneousMathematicalSymbols-B", "⦀⨀" },
			new string[] { "IsMiscellaneousSymbols", "☀✀" },
			new string[] { "IsMiscellaneousSymbolsandArrows", "⬀Ⰰ" },
			new string[] { "IsMiscellaneousTechnical", "⌀␀" },
			new string[] { "IsMongolian", "᠀ᢰ" },
			new string[] { "IsMyanmar", "ကႠ" },
			new string[] { "IsNumberForms", "⅐←" },
			new string[] { "IsOgham", "\u1680ᚠ" },
			new string[] { "IsOpticalCharacterRecognition", "⑀①" },
			new string[] { "IsOriya", "\u0b00\u0b80" },
			new string[] { "IsPhoneticExtensions", "ᴀᶀ" },
			new string[] { "IsPrivateUse", "\ue000豈" },
			new string[] { "IsPrivateUseArea", "\ue000豈" },
			new string[] { "IsRunic", "ᚠᜀ" },
			new string[] { "IsSinhala", "\u0d80\u0e00" },
			new string[] { "IsSmallFormVariants", "﹐ﹰ" },
			new string[] { "IsSpacingModifierLetters", "ʰ\u0300" },
			new string[] { "IsSpecials", "\ufff0" },
			new string[] { "IsSuperscriptsandSubscripts", "⁰₠" },
			new string[] { "IsSupplementalArrows-A", "⟰⠀" },
			new string[] { "IsSupplementalArrows-B", "⤀⦀" },
			new string[] { "IsSupplementalMathematicalOperators", "⨀⬀" },
			new string[] { "IsSyriac", "܀ݐ" },
			new string[] { "IsTagalog", "ᜀᜠ" },
			new string[] { "IsTagbanwa", "ᝠក" },
			new string[] { "IsTaiLe", "ᥐᦀ" },
			new string[] { "IsTamil", "\u0b80\u0c00" },
			new string[] { "IsTelugu", "\u0c00ಀ" },
			new string[] { "IsThaana", "ހ߀" },
			new string[] { "IsThai", "\u0e00\u0e80" },
			new string[] { "IsTibetan", "ༀက" },
			new string[] { "IsUnifiedCanadianAboriginalSyllabics", "᐀\u1680" },
			new string[] { "IsVariationSelectors", "\ufe00︐" },
			new string[] { "IsYiRadicals", "꒐ꓐ" },
			new string[] { "IsYiSyllables", "ꀀ꒐" },
			new string[] { "IsYijingHexagramSymbols", "䷀一" },
			new string[] { "_xmlC", "-/0;A[_`a{·\u00b8À×Ø÷øĲĴĿŁŉŊſƀǄǍǱǴǶǺȘɐʩʻ\u02c2ː\u02d2\u0300\u0346\u0360\u0362Ά\u038bΌ\u038dΎ\u03a2ΣϏϐϗϚϛϜϝϞϟϠϡϢϴЁЍЎѐёѝў҂\u0483\u0487ҐӅӇӉӋӍӐӬӮӶӸӺԱ\u0557ՙ՚աև\u0591\u05a2\u05a3\u05ba\u05bb־\u05bf׀\u05c1׃\u05c4\u05c5א\u05ebװ׳ءػـ\u0653٠٪\u0670ڸںڿۀۏې۔ە۩\u06eaۮ۰ۺ\u0901ऄअ\u093a\u093c\u094e\u0951\u0955क़।०॰\u0981\u0984অ\u098dএ\u0991ও\u09a9প\u09b1ল\u09b3শ\u09ba\u09bcঽ\u09be\u09c5\u09c7\u09c9\u09cbৎ\u09d7\u09d8ড়\u09deয়\u09e4০৲\u0a02\u0a03ਅ\u0a0bਏ\u0a11ਓ\u0a29ਪ\u0a31ਲ\u0a34ਵ\u0a37ਸ\u0a3a\u0a3c\u0a3d\u0a3e\u0a43\u0a47\u0a49\u0a4b\u0a4eਖ਼\u0a5dਫ਼\u0a5f੦\u0a75\u0a81\u0a84અઌઍ\u0a8eએ\u0a92ઓ\u0aa9પ\u0ab1લ\u0ab4વ\u0aba\u0abc\u0ac6\u0ac7\u0aca\u0acb\u0aceૠૡ૦૰\u0b01\u0b04ଅ\u0b0dଏ\u0b11ଓ\u0b29ପ\u0b31ଲ\u0b34ଶ\u0b3a\u0b3c\u0b44\u0b47\u0b49\u0b4b\u0b4e\u0b56\u0b58ଡ଼\u0b5eୟ\u0b62୦୰\u0b82\u0b84அ\u0b8bஎ\u0b91ஒ\u0b96ங\u0b9bஜ\u0b9dஞ\u0ba0ண\u0ba5ந\u0babமஶஷ\u0bba\u0bbe\u0bc3\u0bc6\u0bc9\u0bca\u0bce\u0bd7\u0bd8௧௰\u0c01\u0c04అ\u0c0dఎ\u0c11ఒ\u0c29పఴవ\u0c3a\u0c3e\u0c45\u0c46\u0c49\u0c4a\u0c4e\u0c55\u0c57ౠ\u0c62౦\u0c70\u0c82಄ಅ\u0c8dಎ\u0c91ಒ\u0ca9ಪ\u0cb4ವ\u0cba\u0cbe\u0cc5\u0cc6\u0cc9\u0cca\u0cce\u0cd5\u0cd7ೞ\u0cdfೠ\u0ce2೦\u0cf0\u0d02ഄഅ\u0d0dഎ\u0d11ഒഩപഺ\u0d3e\u0d44\u0d46\u0d49\u0d4aൎ\u0d57൘ൠ\u0d62൦൰กฯะ\u0e3bเ๏๐๚ກ\u0e83ຄ\u0e85ງຉຊ\u0e8bຍຎດຘນຠມ\u0ea4ລ\u0ea6ວຨສຬອຯະ\u0eba\u0ebb\u0ebeເ\u0ec5ໆ\u0ec7\u0ec8\u0ece໐\u0eda\u0f18༚༠༪\u0f35༶\u0f37༸\u0f39༺\u0f3e\u0f48ཉཪ\u0f71྅\u0f86ྌ\u0f90\u0f96\u0f97\u0f98\u0f99\u0fae\u0fb1\u0fb8\u0fb9\u0fbaႠ\u10c6აჷᄀᄁᄂᄄᄅᄈᄉᄊᄋᄍᄎᄓᄼᄽᄾᄿᅀᅁᅌᅍᅎᅏᅐᅑᅔᅖᅙᅚᅟᅢᅣᅤᅥᅦᅧᅨᅩᅪᅭᅯᅲᅴᅵᅶᆞᆟᆨᆩᆫᆬᆮᆰᆷᆹᆺᆻᆼᇃᇫᇬᇰᇱᇹᇺḀẜẠỺἀ\u1f16Ἐ\u1f1eἠ\u1f46Ὀ\u1f4eὐ\u1f58Ὑ\u1f5aὛ\u1f5cὝ\u1f5eὟ\u1f7eᾀ\u1fb5ᾶ\u1fbdι\u1fbfῂ\u1fc5ῆ\u1fcdῐ\u1fd4ῖ\u1fdcῠ\u1fedῲ\u1ff5ῶ\u1ffd\u20d0\u20dd\u20e1\u20e2Ω℧Kℬ℮ℯↀↃ々〆〇〈〡〰〱〶ぁゕ\u3099\u309bゝゟァ・ーヿㄅㄭ一龦가\ud7a4" },
			new string[] { "_xmlD", "0:٠٪۰ۺ०॰০ৰ੦\u0a70૦૰୦୰௧௰౦\u0c70೦\u0cf0൦൰๐๚໐\u0eda༠༪၀၊፩፲០\u17ea᠐\u181a０：" },
			new string[] { "_xmlI", ":;A[_`a{À×Ø÷øĲĴĿŁŉŊſƀǄǍǱǴǶǺȘɐʩʻ\u02c2Ά·Έ\u038bΌ\u038dΎ\u03a2ΣϏϐϗϚϛϜϝϞϟϠϡϢϴЁЍЎѐёѝў҂ҐӅӇӉӋӍӐӬӮӶӸӺԱ\u0557ՙ՚աևא\u05ebװ׳ءػف\u064bٱڸںڿۀۏې۔ە\u06d6ۥ\u06e7अ\u093aऽ\u093eक़\u0962অ\u098dএ\u0991ও\u09a9প\u09b1ল\u09b3শ\u09baড়\u09deয়\u09e2ৰ৲ਅ\u0a0bਏ\u0a11ਓ\u0a29ਪ\u0a31ਲ\u0a34ਵ\u0a37ਸ\u0a3aਖ਼\u0a5dਫ਼\u0a5fੲ\u0a75અઌઍ\u0a8eએ\u0a92ઓ\u0aa9પ\u0ab1લ\u0ab4વ\u0abaઽ\u0abeૠૡଅ\u0b0dଏ\u0b11ଓ\u0b29ପ\u0b31ଲ\u0b34ଶ\u0b3aଽ\u0b3eଡ଼\u0b5eୟ\u0b62அ\u0b8bஎ\u0b91ஒ\u0b96ங\u0b9bஜ\u0b9dஞ\u0ba0ண\u0ba5ந\u0babமஶஷ\u0bbaఅ\u0c0dఎ\u0c11ఒ\u0c29పఴవ\u0c3aౠ\u0c62ಅ\u0c8dಎ\u0c91ಒ\u0ca9ಪ\u0cb4ವ\u0cbaೞ\u0cdfೠ\u0ce2അ\u0d0dഎ\u0d11ഒഩപഺൠ\u0d62กฯะ\u0e31า\u0e34เๆກ\u0e83ຄ\u0e85ງຉຊ\u0e8bຍຎດຘນຠມ\u0ea4ລ\u0ea6ວຨສຬອຯະ\u0eb1າ\u0eb4ຽ\u0ebeເ\u0ec5ཀ\u0f48ཉཪႠ\u10c6აჷᄀᄁᄂᄄᄅᄈᄉᄊᄋᄍᄎᄓᄼᄽᄾᄿᅀᅁᅌᅍᅎᅏᅐᅑᅔᅖᅙᅚᅟᅢᅣᅤᅥᅦᅧᅨᅩᅪᅭᅯᅲᅴᅵᅶᆞᆟᆨᆩᆫᆬᆮᆰᆷᆹᆺᆻᆼᇃᇫᇬᇰᇱᇹᇺḀẜẠỺἀ\u1f16Ἐ\u1f1eἠ\u1f46Ὀ\u1f4eὐ\u1f58Ὑ\u1f5aὛ\u1f5cὝ\u1f5eὟ\u1f7eᾀ\u1fb5ᾶ\u1fbdι\u1fbfῂ\u1fc5ῆ\u1fcdῐ\u1fd4ῖ\u1fdcῠ\u1fedῲ\u1ff5ῶ\u1ffdΩ℧Kℬ℮ℯↀↃ〇〈〡\u302aぁゕァ・ㄅㄭ一龦가\ud7a4" },
			new string[] { "_xmlW", "$%+,0:<?A[^_`{|}~\u007f¢«¬\u00ad®·\u00b8»¼¿ÀȡȢȴɐʮʰ\u02ef\u0300\u0350\u0360ͰʹͶͺͻ\u0384·Έ\u038bΌ\u038dΎ\u03a2ΣϏϐϷЀ\u0487\u0488ӏӐӶӸӺԀԐԱ\u0557ՙ՚աֈ\u0591\u05a2\u05a3\u05ba\u05bb־\u05bf׀\u05c1׃\u05c4\u05c5א\u05ebװ׳ءػـ\u0656٠٪ٮ۔ە\u06dd۞ۮ۰ۿܐܭ\u0730\u074bހ\u07b2\u0901ऄअ\u093a\u093c\u094eॐ\u0955क़।०॰\u0981\u0984অ\u098dএ\u0991ও\u09a9প\u09b1ল\u09b3শ\u09ba\u09bcঽ\u09be\u09c5\u09c7\u09c9\u09cbৎ\u09d7\u09d8ড়\u09deয়\u09e4০৻\u0a02\u0a03ਅ\u0a0bਏ\u0a11ਓ\u0a29ਪ\u0a31ਲ\u0a34ਵ\u0a37ਸ\u0a3a\u0a3c\u0a3d\u0a3e\u0a43\u0a47\u0a49\u0a4b\u0a4eਖ਼\u0a5dਫ਼\u0a5f੦\u0a75\u0a81\u0a84અઌઍ\u0a8eએ\u0a92ઓ\u0aa9પ\u0ab1લ\u0ab4વ\u0aba\u0abc\u0ac6\u0ac7\u0aca\u0acb\u0aceૐ\u0ad1ૠૡ૦૰\u0b01\u0b04ଅ\u0b0dଏ\u0b11ଓ\u0b29ପ\u0b31ଲ\u0b34ଶ\u0b3a\u0b3c\u0b44\u0b47\u0b49\u0b4b\u0b4e\u0b56\u0b58ଡ଼\u0b5eୟ\u0b62୦ୱ\u0b82\u0b84அ\u0b8bஎ\u0b91ஒ\u0b96ங\u0b9bஜ\u0b9dஞ\u0ba0ண\u0ba5ந\u0babமஶஷ\u0bba\u0bbe\u0bc3\u0bc6\u0bc9\u0bca\u0bce\u0bd7\u0bd8௧௳\u0c01\u0c04అ\u0c0dఎ\u0c11ఒ\u0c29పఴవ\u0c3a\u0c3e\u0c45\u0c46\u0c49\u0c4a\u0c4e\u0c55\u0c57ౠ\u0c62౦\u0c70\u0c82಄ಅ\u0c8dಎ\u0c91ಒ\u0ca9ಪ\u0cb4ವ\u0cba\u0cbe\u0cc5\u0cc6\u0cc9\u0cca\u0cce\u0cd5\u0cd7ೞ\u0cdfೠ\u0ce2೦\u0cf0\u0d02ഄഅ\u0d0dഎ\u0d11ഒഩപഺ\u0d3e\u0d44\u0d46\u0d49\u0d4aൎ\u0d57൘ൠ\u0d62൦൰\u0d82\u0d84අ\u0d97ක\u0db2ඳ\u0dbcල\u0dbeව\u0dc7\u0dca\u0dcb\u0dcf\u0dd5\u0dd6\u0dd7\u0dd8\u0de0\u0df2෴ก\u0e3b฿๏๐๚ກ\u0e83ຄ\u0e85ງຉຊ\u0e8bຍຎດຘນຠມ\u0ea4ລ\u0ea6ວຨສຬອ\u0eba\u0ebb\u0ebeເ\u0ec5ໆ\u0ec7\u0ec8\u0ece໐\u0edaໜໞༀ༄༓༺\u0f3e\u0f48ཉཫ\u0f71྅\u0f86ྌ\u0f90\u0f98\u0f99\u0fbd྾\u0fcd࿏࿐ကဢဣဨဩ\u102b\u102c\u1033\u1036\u103a၀၊ၐၚႠ\u10c6აჹᄀᅚᅟᆣᆨᇺሀሇለቇቈ\u1249ቊ\u124eቐ\u1257ቘ\u1259ቚ\u125eበኇኈ\u1289ኊ\u128eነኯኰ\u12b1ኲ\u12b6ኸ\u12bfዀ\u12c1ዂ\u12c6ወዏዐ\u12d7ዘዯደጏጐ\u1311ጒ\u1316ጘጟጠፇፈ\u135b፩\u137dᎠᏵᐁ᙭ᙯᙷᚁ᚛ᚠ᛫ᛮᛱᜀᜍᜎ\u1715ᜠ᜵ᝀ\u1754ᝠ\u176dᝮ\u1771\u1772\u1774ក។ៗ៘៛\u17dd០\u17ea\u180b\u180e᠐\u181aᠠᡸᢀᢪḀẜẠỺἀ\u1f16Ἐ\u1f1eἠ\u1f46Ὀ\u1f4eὐ\u1f58Ὑ\u1f5aὛ\u1f5cὝ\u1f5eὟ\u1f7eᾀ\u1fb5ᾶ\u1fc5ῆ\u1fd4ῖ\u1fdc\u1fdd\u1ff0ῲ\u1ff5ῶ\u1fff⁄⁅⁒⁓⁰\u2072⁴⁽ⁿ₍₠₲\u20d0\u20eb℀℻ℽ⅌⅓ↄ←〈⌫⎴⎷⏏␀\u2427⑀\u244b①⓿─☔☖☘☙♾⚀⚊✁✅✆✊✌✨✩❌❍❎❏❓❖❗❘❟❡❨❶➕➘➰➱➿⟐⟦⟰⦃⦙⧘⧜⧼⧾⬀⺀\u2e9a⺛\u2ef4⼀\u2fd6⿰\u2ffc〄〈〒〔〠〰〱〽〾\u3040ぁ\u3097\u3099゠ァ・ー\u3100ㄅㄭㄱ\u318f㆐ㆸㇰ㈝㈠㉄㉑㉼㉿㋌㋐㋿㌀㍷㍻㏞㏠㏿㐀䶶一龦ꀀ\ua48d꒐\ua4c7가\ud7a4豈郞侮恵ﬀ\ufb07ﬓ\ufb18יִ\ufb37טּ\ufb3dמּ\ufb3fנּ\ufb42ףּ\ufb45צּ\ufbb2ﯓ﴾ﵐ\ufd90ﶒ\ufdc8ﷰ﷽\ufe00︐\ufe20\ufe24﹢﹣﹤\ufe67﹩﹪ﹰ\ufe75ﹶ\ufefd＄％＋，０：＜？Ａ［\uff3e\uff3f\uff40｛｜｝～｟ｦ\uffbfￂ\uffc8ￊ\uffd0ￒ\uffd8ￚ\uffdd￠\uffe7￨\uffef￼\ufffe" }
		};

		// Token: 0x04000521 RID: 1313
		private static readonly RegexCharClass.LowerCaseMapping[] s_lcTable = new RegexCharClass.LowerCaseMapping[]
		{
			new RegexCharClass.LowerCaseMapping('A', 'Z', 1, 32),
			new RegexCharClass.LowerCaseMapping('À', 'Þ', 1, 32),
			new RegexCharClass.LowerCaseMapping('Ā', 'Į', 2, 0),
			new RegexCharClass.LowerCaseMapping('İ', 'İ', 0, 105),
			new RegexCharClass.LowerCaseMapping('Ĳ', 'Ķ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ĺ', 'Ň', 3, 0),
			new RegexCharClass.LowerCaseMapping('Ŋ', 'Ŷ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ÿ', 'Ÿ', 0, 255),
			new RegexCharClass.LowerCaseMapping('Ź', 'Ž', 3, 0),
			new RegexCharClass.LowerCaseMapping('Ɓ', 'Ɓ', 0, 595),
			new RegexCharClass.LowerCaseMapping('Ƃ', 'Ƅ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ɔ', 'Ɔ', 0, 596),
			new RegexCharClass.LowerCaseMapping('Ƈ', 'Ƈ', 0, 392),
			new RegexCharClass.LowerCaseMapping('Ɖ', 'Ɗ', 1, 205),
			new RegexCharClass.LowerCaseMapping('Ƌ', 'Ƌ', 0, 396),
			new RegexCharClass.LowerCaseMapping('Ǝ', 'Ǝ', 0, 477),
			new RegexCharClass.LowerCaseMapping('Ə', 'Ə', 0, 601),
			new RegexCharClass.LowerCaseMapping('Ɛ', 'Ɛ', 0, 603),
			new RegexCharClass.LowerCaseMapping('Ƒ', 'Ƒ', 0, 402),
			new RegexCharClass.LowerCaseMapping('Ɠ', 'Ɠ', 0, 608),
			new RegexCharClass.LowerCaseMapping('Ɣ', 'Ɣ', 0, 611),
			new RegexCharClass.LowerCaseMapping('Ɩ', 'Ɩ', 0, 617),
			new RegexCharClass.LowerCaseMapping('Ɨ', 'Ɨ', 0, 616),
			new RegexCharClass.LowerCaseMapping('Ƙ', 'Ƙ', 0, 409),
			new RegexCharClass.LowerCaseMapping('Ɯ', 'Ɯ', 0, 623),
			new RegexCharClass.LowerCaseMapping('Ɲ', 'Ɲ', 0, 626),
			new RegexCharClass.LowerCaseMapping('Ɵ', 'Ɵ', 0, 629),
			new RegexCharClass.LowerCaseMapping('Ơ', 'Ƥ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ƨ', 'Ƨ', 0, 424),
			new RegexCharClass.LowerCaseMapping('Ʃ', 'Ʃ', 0, 643),
			new RegexCharClass.LowerCaseMapping('Ƭ', 'Ƭ', 0, 429),
			new RegexCharClass.LowerCaseMapping('Ʈ', 'Ʈ', 0, 648),
			new RegexCharClass.LowerCaseMapping('Ư', 'Ư', 0, 432),
			new RegexCharClass.LowerCaseMapping('Ʊ', 'Ʋ', 1, 217),
			new RegexCharClass.LowerCaseMapping('Ƴ', 'Ƶ', 3, 0),
			new RegexCharClass.LowerCaseMapping('Ʒ', 'Ʒ', 0, 658),
			new RegexCharClass.LowerCaseMapping('Ƹ', 'Ƹ', 0, 441),
			new RegexCharClass.LowerCaseMapping('Ƽ', 'Ƽ', 0, 445),
			new RegexCharClass.LowerCaseMapping('Ǆ', 'ǅ', 0, 454),
			new RegexCharClass.LowerCaseMapping('Ǉ', 'ǈ', 0, 457),
			new RegexCharClass.LowerCaseMapping('Ǌ', 'ǋ', 0, 460),
			new RegexCharClass.LowerCaseMapping('Ǎ', 'Ǜ', 3, 0),
			new RegexCharClass.LowerCaseMapping('Ǟ', 'Ǯ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ǳ', 'ǲ', 0, 499),
			new RegexCharClass.LowerCaseMapping('Ǵ', 'Ǵ', 0, 501),
			new RegexCharClass.LowerCaseMapping('Ǻ', 'Ȗ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ά', 'Ά', 0, 940),
			new RegexCharClass.LowerCaseMapping('Έ', 'Ί', 1, 37),
			new RegexCharClass.LowerCaseMapping('Ό', 'Ό', 0, 972),
			new RegexCharClass.LowerCaseMapping('Ύ', 'Ώ', 1, 63),
			new RegexCharClass.LowerCaseMapping('Α', 'Ϋ', 1, 32),
			new RegexCharClass.LowerCaseMapping('Ϣ', 'Ϯ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ё', 'Џ', 1, 80),
			new RegexCharClass.LowerCaseMapping('А', 'Я', 1, 32),
			new RegexCharClass.LowerCaseMapping('Ѡ', 'Ҁ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ґ', 'Ҿ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ӂ', 'Ӄ', 3, 0),
			new RegexCharClass.LowerCaseMapping('Ӈ', 'Ӈ', 0, 1224),
			new RegexCharClass.LowerCaseMapping('Ӌ', 'Ӌ', 0, 1228),
			new RegexCharClass.LowerCaseMapping('Ӑ', 'Ӫ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ӯ', 'Ӵ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ӹ', 'Ӹ', 0, 1273),
			new RegexCharClass.LowerCaseMapping('Ա', 'Ֆ', 1, 48),
			new RegexCharClass.LowerCaseMapping('Ⴀ', 'Ⴥ', 1, 48),
			new RegexCharClass.LowerCaseMapping('Ḁ', 'Ỹ', 2, 0),
			new RegexCharClass.LowerCaseMapping('Ἀ', 'Ἇ', 1, -8),
			new RegexCharClass.LowerCaseMapping('Ἐ', '\u1f1f', 1, -8),
			new RegexCharClass.LowerCaseMapping('Ἠ', 'Ἧ', 1, -8),
			new RegexCharClass.LowerCaseMapping('Ἰ', 'Ἷ', 1, -8),
			new RegexCharClass.LowerCaseMapping('Ὀ', 'Ὅ', 1, -8),
			new RegexCharClass.LowerCaseMapping('Ὑ', 'Ὑ', 0, 8017),
			new RegexCharClass.LowerCaseMapping('Ὓ', 'Ὓ', 0, 8019),
			new RegexCharClass.LowerCaseMapping('Ὕ', 'Ὕ', 0, 8021),
			new RegexCharClass.LowerCaseMapping('Ὗ', 'Ὗ', 0, 8023),
			new RegexCharClass.LowerCaseMapping('Ὠ', 'Ὧ', 1, -8),
			new RegexCharClass.LowerCaseMapping('ᾈ', 'ᾏ', 1, -8),
			new RegexCharClass.LowerCaseMapping('ᾘ', 'ᾟ', 1, -8),
			new RegexCharClass.LowerCaseMapping('ᾨ', 'ᾯ', 1, -8),
			new RegexCharClass.LowerCaseMapping('Ᾰ', 'Ᾱ', 1, -8),
			new RegexCharClass.LowerCaseMapping('Ὰ', 'Ά', 1, -74),
			new RegexCharClass.LowerCaseMapping('ᾼ', 'ᾼ', 0, 8115),
			new RegexCharClass.LowerCaseMapping('Ὲ', 'Ή', 1, -86),
			new RegexCharClass.LowerCaseMapping('ῌ', 'ῌ', 0, 8131),
			new RegexCharClass.LowerCaseMapping('Ῐ', 'Ῑ', 1, -8),
			new RegexCharClass.LowerCaseMapping('Ὶ', 'Ί', 1, -100),
			new RegexCharClass.LowerCaseMapping('Ῠ', 'Ῡ', 1, -8),
			new RegexCharClass.LowerCaseMapping('Ὺ', 'Ύ', 1, -112),
			new RegexCharClass.LowerCaseMapping('Ῥ', 'Ῥ', 0, 8165),
			new RegexCharClass.LowerCaseMapping('Ὸ', 'Ό', 1, -128),
			new RegexCharClass.LowerCaseMapping('Ὼ', 'Ώ', 1, -126),
			new RegexCharClass.LowerCaseMapping('ῼ', 'ῼ', 0, 8179),
			new RegexCharClass.LowerCaseMapping('Ⅰ', 'Ⅿ', 1, 16),
			new RegexCharClass.LowerCaseMapping('Ⓐ', 'ⓐ', 1, 26),
			new RegexCharClass.LowerCaseMapping('Ａ', 'Ｚ', 1, 32)
		};

		// Token: 0x04000522 RID: 1314
		private List<RegexCharClass.SingleRange> _rangelist;

		// Token: 0x04000523 RID: 1315
		private StringBuilder _categories;

		// Token: 0x04000524 RID: 1316
		private bool _canonical;

		// Token: 0x04000525 RID: 1317
		private bool _negate;

		// Token: 0x04000526 RID: 1318
		private RegexCharClass _subtractor;

		// Token: 0x02000136 RID: 310
		private readonly struct LowerCaseMapping
		{
			// Token: 0x0600066D RID: 1645 RVA: 0x00020CD4 File Offset: 0x0001EED4
			internal LowerCaseMapping(char chMin, char chMax, int lcOp, int data)
			{
				this.ChMin = chMin;
				this.ChMax = chMax;
				this.LcOp = lcOp;
				this.Data = data;
			}

			// Token: 0x04000527 RID: 1319
			public readonly char ChMin;

			// Token: 0x04000528 RID: 1320
			public readonly char ChMax;

			// Token: 0x04000529 RID: 1321
			public readonly int LcOp;

			// Token: 0x0400052A RID: 1322
			public readonly int Data;
		}

		// Token: 0x02000137 RID: 311
		private sealed class SingleRangeComparer : IComparer<RegexCharClass.SingleRange>
		{
			// Token: 0x0600066E RID: 1646 RVA: 0x000026E5 File Offset: 0x000008E5
			private SingleRangeComparer()
			{
			}

			// Token: 0x0600066F RID: 1647 RVA: 0x00020CF3 File Offset: 0x0001EEF3
			public int Compare(RegexCharClass.SingleRange x, RegexCharClass.SingleRange y)
			{
				return x.First.CompareTo(y.First);
			}

			// Token: 0x0400052B RID: 1323
			public static readonly RegexCharClass.SingleRangeComparer Instance = new RegexCharClass.SingleRangeComparer();
		}

		// Token: 0x02000138 RID: 312
		private readonly struct SingleRange
		{
			// Token: 0x06000671 RID: 1649 RVA: 0x00020D13 File Offset: 0x0001EF13
			internal SingleRange(char first, char last)
			{
				this.First = first;
				this.Last = last;
			}

			// Token: 0x0400052C RID: 1324
			public readonly char First;

			// Token: 0x0400052D RID: 1325
			public readonly char Last;
		}
	}
}
