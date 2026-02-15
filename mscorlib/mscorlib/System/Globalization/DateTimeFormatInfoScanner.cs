using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000697 RID: 1687
	internal class DateTimeFormatInfoScanner
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000CB8C0 File Offset: 0x000C9AC0
		private static Dictionary<string, string> KnownWords
		{
			get
			{
				if (DateTimeFormatInfoScanner.s_knownWords == null)
				{
					DateTimeFormatInfoScanner.s_knownWords = new Dictionary<string, string>
					{
						{
							"/",
							string.Empty
						},
						{
							"-",
							string.Empty
						},
						{
							".",
							string.Empty
						},
						{
							"年",
							string.Empty
						},
						{
							"月",
							string.Empty
						},
						{
							"日",
							string.Empty
						},
						{
							"년",
							string.Empty
						},
						{
							"월",
							string.Empty
						},
						{
							"일",
							string.Empty
						},
						{
							"시",
							string.Empty
						},
						{
							"분",
							string.Empty
						},
						{
							"초",
							string.Empty
						},
						{
							"時",
							string.Empty
						},
						{
							"时",
							string.Empty
						},
						{
							"分",
							string.Empty
						},
						{
							"秒",
							string.Empty
						}
					};
				}
				return DateTimeFormatInfoScanner.s_knownWords;
			}
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000CB9EC File Offset: 0x000C9BEC
		internal static int SkipWhiteSpacesAndNonLetter(string pattern, int currentIndex)
		{
			while (currentIndex < pattern.Length)
			{
				char c = pattern[currentIndex];
				if (c == '\\')
				{
					currentIndex++;
					if (currentIndex >= pattern.Length)
					{
						break;
					}
					c = pattern[currentIndex];
					if (c == '\'')
					{
						continue;
					}
				}
				if (char.IsLetter(c) || c == '\'' || c == '.')
				{
					break;
				}
				currentIndex++;
			}
			return currentIndex;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000CBA44 File Offset: 0x000C9C44
		internal void AddDateWordOrPostfix(string formatPostfix, string str)
		{
			if (str.Length > 0)
			{
				if (str.Equals("."))
				{
					this.AddIgnorableSymbols(".");
					return;
				}
				string text;
				if (!DateTimeFormatInfoScanner.KnownWords.TryGetValue(str, out text))
				{
					if (this.m_dateWords == null)
					{
						this.m_dateWords = new List<string>();
					}
					if (formatPostfix == "MMMM")
					{
						string text2 = "\ue000" + str;
						if (!this.m_dateWords.Contains(text2))
						{
							this.m_dateWords.Add(text2);
							return;
						}
					}
					else
					{
						if (!this.m_dateWords.Contains(str))
						{
							this.m_dateWords.Add(str);
						}
						if (str[str.Length - 1] == '.')
						{
							string text3 = str.Substring(0, str.Length - 1);
							if (!this.m_dateWords.Contains(text3))
							{
								this.m_dateWords.Add(text3);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000CBB28 File Offset: 0x000C9D28
		internal int AddDateWords(string pattern, int index, string formatPostfix)
		{
			int num = DateTimeFormatInfoScanner.SkipWhiteSpacesAndNonLetter(pattern, index);
			if (num != index && formatPostfix != null)
			{
				formatPostfix = null;
			}
			index = num;
			StringBuilder stringBuilder = new StringBuilder();
			while (index < pattern.Length)
			{
				char c = pattern[index];
				if (c == '\'')
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					index++;
					break;
				}
				if (c == '\\')
				{
					index++;
					if (index < pattern.Length)
					{
						stringBuilder.Append(pattern[index]);
						index++;
					}
				}
				else if (char.IsWhiteSpace(c))
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					if (formatPostfix != null)
					{
						formatPostfix = null;
					}
					stringBuilder.Length = 0;
					index++;
				}
				else
				{
					stringBuilder.Append(c);
					index++;
				}
			}
			return index;
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000CBBDC File Offset: 0x000C9DDC
		internal static int ScanRepeatChar(string pattern, char ch, int index, out int count)
		{
			count = 1;
			while (++index < pattern.Length && pattern[index] == ch)
			{
				count++;
			}
			return index;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000CBC04 File Offset: 0x000C9E04
		internal void AddIgnorableSymbols(string text)
		{
			if (this.m_dateWords == null)
			{
				this.m_dateWords = new List<string>();
			}
			string text2 = "\ue001" + text;
			if (!this.m_dateWords.Contains(text2))
			{
				this.m_dateWords.Add(text2);
			}
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000CBC4C File Offset: 0x000C9E4C
		internal void ScanDateWord(string pattern)
		{
			this._ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
			for (int i = 0; i < pattern.Length; i++)
			{
				char c = pattern[i];
				if (c <= 'M')
				{
					if (c == '\'')
					{
						i = this.AddDateWords(pattern, i + 1, null);
						continue;
					}
					if (c == '.')
					{
						if (this._ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag)
						{
							this.AddIgnorableSymbols(".");
							this._ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
						}
						i++;
						continue;
					}
					if (c == 'M')
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'M', i, out num);
						if (num >= 4 && i < pattern.Length && pattern[i] == '\'')
						{
							i = this.AddDateWords(pattern, i + 1, "MMMM");
						}
						this._ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundMonthPatternFlag;
						continue;
					}
				}
				else
				{
					if (c == '\\')
					{
						i += 2;
						continue;
					}
					if (c != 'd')
					{
						if (c == 'y')
						{
							int num;
							i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'y', i, out num);
							this._ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundYearPatternFlag;
							continue;
						}
					}
					else
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'd', i, out num);
						if (num <= 2)
						{
							this._ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundDayPatternFlag;
							continue;
						}
						continue;
					}
				}
				if (this._ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag && !char.IsWhiteSpace(c))
				{
					this._ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
				}
			}
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000CBD84 File Offset: 0x000C9F84
		internal string[] GetDateWordsOfDTFI(DateTimeFormatInfo dtfi)
		{
			string[] array = dtfi.GetAllDateTimePatterns('D');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			array = dtfi.GetAllDateTimePatterns('d');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			array = dtfi.GetAllDateTimePatterns('y');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			this.ScanDateWord(dtfi.MonthDayPattern);
			array = dtfi.GetAllDateTimePatterns('T');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			array = dtfi.GetAllDateTimePatterns('t');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			string[] array2 = null;
			if (this.m_dateWords != null && this.m_dateWords.Count > 0)
			{
				array2 = new string[this.m_dateWords.Count];
				for (int i = 0; i < this.m_dateWords.Count; i++)
				{
					array2[i] = this.m_dateWords[i];
				}
			}
			return array2;
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000CBE8C File Offset: 0x000CA08C
		internal static FORMATFLAGS GetFormatFlagGenitiveMonth(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			if (DateTimeFormatInfoScanner.EqualStringArrays(monthNames, genitveMonthNames) && DateTimeFormatInfoScanner.EqualStringArrays(abbrevMonthNames, genetiveAbbrevMonthNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseGenitiveMonth;
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000CBEA4 File Offset: 0x000CA0A4
		internal static FORMATFLAGS GetFormatFlagUseSpaceInMonthNames(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			return FORMATFLAGS.None | ((DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(monthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseDigitPrefixInTokens : FORMATFLAGS.None) | ((DateTimeFormatInfoScanner.ArrayElementsHaveSpace(monthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseSpacesInMonthNames : FORMATFLAGS.None);
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000CBEFD File Offset: 0x000CA0FD
		internal static FORMATFLAGS GetFormatFlagUseSpaceInDayNames(string[] dayNames, string[] abbrevDayNames)
		{
			if (!DateTimeFormatInfoScanner.ArrayElementsHaveSpace(dayNames) && !DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevDayNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseSpacesInDayNames;
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000CBF13 File Offset: 0x000CA113
		internal static FORMATFLAGS GetFormatFlagUseHebrewCalendar(int calID)
		{
			if (calID != 8)
			{
				return FORMATFLAGS.None;
			}
			return (FORMATFLAGS)10;
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000CBF20 File Offset: 0x000CA120
		private static bool EqualStringArrays(string[] array1, string[] array2)
		{
			if (array1 == array2)
			{
				return true;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (!array1[i].Equals(array2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000CBF5C File Offset: 0x000CA15C
		private static bool ArrayElementsHaveSpace(string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array[i].Length; j++)
				{
					if (char.IsWhiteSpace(array[i][j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000CBFA0 File Offset: 0x000CA1A0
		private static bool ArrayElementsBeginWithDigit(string[] array)
		{
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].Length > 0 && array[i][0] >= '0' && array[i][0] <= '9')
				{
					int num = 1;
					while (num < array[i].Length && array[i][num] >= '0' && array[i][num] <= '9')
					{
						num++;
					}
					if (num == array[i].Length)
					{
						return false;
					}
					if (num == array[i].Length - 1)
					{
						char c = array[i][num];
						if (c == '月' || c == '월')
						{
							return false;
						}
					}
					return num != array[i].Length - 4 || array[i][num] != '\'' || array[i][num + 1] != ' ' || array[i][num + 2] != '月' || array[i][num + 3] != '\'';
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x04001C1D RID: 7197
		internal List<string> m_dateWords = new List<string>();

		// Token: 0x04001C1E RID: 7198
		private static volatile Dictionary<string, string> s_knownWords;

		// Token: 0x04001C1F RID: 7199
		private DateTimeFormatInfoScanner.FoundDatePattern _ymdFlags;

		// Token: 0x02000698 RID: 1688
		private enum FoundDatePattern
		{
			// Token: 0x04001C21 RID: 7201
			None,
			// Token: 0x04001C22 RID: 7202
			FoundYearPatternFlag,
			// Token: 0x04001C23 RID: 7203
			FoundMonthPatternFlag,
			// Token: 0x04001C24 RID: 7204
			FoundDayPatternFlag = 4,
			// Token: 0x04001C25 RID: 7205
			FoundYMDPatternFlag = 7
		}
	}
}
