using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Unity;

namespace System.Globalization
{
	/// <summary>Defines text properties and behaviors, such as casing, that are specific to a writing system. </summary>
	// Token: 0x020006BC RID: 1724
	[ComVisible(true)]
	[Serializable]
	public class TextInfo : ICloneable, IDeserializationCallback
	{
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000D14C0 File Offset: 0x000CF6C0
		internal static TextInfo Invariant
		{
			get
			{
				if (TextInfo.s_Invariant == null)
				{
					TextInfo.s_Invariant = new TextInfo(CultureData.Invariant);
				}
				return TextInfo.s_Invariant;
			}
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000D14E3 File Offset: 0x000CF6E3
		internal TextInfo(CultureData cultureData)
		{
			this.m_cultureData = cultureData;
			this.m_cultureName = this.m_cultureData.CultureName;
			this.m_textInfoName = this.m_cultureData.STEXTINFO;
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000D1514 File Offset: 0x000CF714
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_cultureData = null;
			this.m_cultureName = null;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000D1524 File Offset: 0x000CF724
		private void OnDeserialized()
		{
			if (this.m_cultureData == null)
			{
				if (this.m_cultureName == null)
				{
					if (this.customCultureName != null)
					{
						this.m_cultureName = this.customCultureName;
					}
					else if (this.m_win32LangID == 0)
					{
						this.m_cultureName = "ar-SA";
					}
					else
					{
						this.m_cultureName = CultureInfo.GetCultureInfo(this.m_win32LangID).m_cultureData.CultureName;
					}
				}
				this.m_cultureData = CultureInfo.GetCultureInfo(this.m_cultureName).m_cultureData;
				this.m_textInfoName = this.m_cultureData.STEXTINFO;
			}
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000D15AE File Offset: 0x000CF7AE
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000D15B6 File Offset: 0x000CF7B6
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.m_useUserOverride = false;
			this.customCultureName = this.m_cultureName;
			this.m_win32LangID = CultureInfo.GetCultureInfo(this.m_cultureName).LCID;
		}

		/// <summary>Gets the American National Standards Institute (ANSI) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The ANSI code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x000D15E1 File Offset: 0x000CF7E1
		public virtual int ANSICodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTANSICODEPAGE;
			}
		}

		/// <summary>Gets the name of the culture associated with the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>The name of a culture. </returns>
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06003652 RID: 13906 RVA: 0x000D15EE File Offset: 0x000CF7EE
		[ComVisible(false)]
		public string CultureName
		{
			get
			{
				return this.m_textInfoName;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.TextInfo" /> object is read-only; otherwise, false.</returns>
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x000D15F6 File Offset: 0x000CF7F6
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>A new instance of <see cref="T:System.Object" /> that is the memberwise clone of the current <see cref="T:System.Globalization.TextInfo" /> object.</returns>
		// Token: 0x06003654 RID: 13908 RVA: 0x000D15FE File Offset: 0x000CF7FE
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((TextInfo)obj).SetReadOnlyState(false);
			return obj;
		}

		/// <summary>Returns a read-only version of the specified <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> object specified by the <paramref name="textInfo" /> parameter, if <paramref name="textInfo" /> is read-only.-or-A read-only memberwise clone of the <see cref="T:System.Globalization.TextInfo" /> object specified by <paramref name="textInfo" />, if <paramref name="textInfo" /> is not read-only.</returns>
		/// <param name="textInfo">A <see cref="T:System.Globalization.TextInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textInfo" /> is null.</exception>
		// Token: 0x06003655 RID: 13909 RVA: 0x000D1612 File Offset: 0x000CF812
		[ComVisible(false)]
		public static TextInfo ReadOnly(TextInfo textInfo)
		{
			if (textInfo == null)
			{
				throw new ArgumentNullException("textInfo");
			}
			if (textInfo.IsReadOnly)
			{
				return textInfo;
			}
			TextInfo textInfo2 = (TextInfo)textInfo.MemberwiseClone();
			textInfo2.SetReadOnlyState(true);
			return textInfo2;
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000D163E File Offset: 0x000CF83E
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		/// <summary>Gets or sets the string that separates items in a list.</summary>
		/// <returns>The string that separates items in a list.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</exception>
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06003657 RID: 13911 RVA: 0x000D1647 File Offset: 0x000CF847
		public virtual string ListSeparator
		{
			get
			{
				if (this.m_listSeparator == null)
				{
					this.m_listSeparator = this.m_cultureData.SLIST;
				}
				return this.m_listSeparator;
			}
		}

		/// <summary>Converts the specified character to lowercase.</summary>
		/// <returns>The specified character converted to lowercase.</returns>
		/// <param name="c">The character to convert to lowercase. </param>
		// Token: 0x06003658 RID: 13912 RVA: 0x000D1668 File Offset: 0x000CF868
		public virtual char ToLower(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToLowerAsciiInvariant(c);
			}
			return this.ToLowerInternal(c);
		}

		/// <summary>Converts the specified string to lowercase.</summary>
		/// <returns>The specified string converted to lowercase.</returns>
		/// <param name="str">The string to convert to lowercase. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x06003659 RID: 13913 RVA: 0x000D1688 File Offset: 0x000CF888
		public virtual string ToLower(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return this.ToLowerInternal(str);
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000D169F File Offset: 0x000CF89F
		private static char ToLowerAsciiInvariant(char c)
		{
			if ('A' <= c && c <= 'Z')
			{
				c |= ' ';
			}
			return c;
		}

		/// <summary>Converts the specified character to uppercase.</summary>
		/// <returns>The specified character converted to uppercase.</returns>
		/// <param name="c">The character to convert to uppercase. </param>
		// Token: 0x0600365B RID: 13915 RVA: 0x000D16B3 File Offset: 0x000CF8B3
		public virtual char ToUpper(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToUpperAsciiInvariant(c);
			}
			return this.ToUpperInternal(c);
		}

		/// <summary>Converts the specified string to uppercase.</summary>
		/// <returns>The specified string converted to uppercase.</returns>
		/// <param name="str">The string to convert to uppercase. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x0600365C RID: 13916 RVA: 0x000D16D3 File Offset: 0x000CF8D3
		public virtual string ToUpper(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return this.ToUpperInternal(str);
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000D16EA File Offset: 0x000CF8EA
		internal static char ToUpperAsciiInvariant(char c)
		{
			if ('a' <= c && c <= 'z')
			{
				c = (char)((int)c & -33);
			}
			return c;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000D16FE File Offset: 0x000CF8FE
		private static bool IsAscii(char c)
		{
			return c < '\u0080';
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000D1708 File Offset: 0x000CF908
		private bool IsAsciiCasingSameAsInvariant
		{
			get
			{
				if (this.m_IsAsciiCasingSameAsInvariant == null)
				{
					this.m_IsAsciiCasingSameAsInvariant = new bool?(!(this.m_cultureData.SISO639LANGNAME == "az") && !(this.m_cultureData.SISO639LANGNAME == "tr"));
				}
				return this.m_IsAsciiCasingSameAsInvariant.Value;
			}
		}

		/// <summary>Determines whether the specified object represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Globalization.TextInfo" />. </param>
		// Token: 0x06003660 RID: 13920 RVA: 0x000D176C File Offset: 0x000CF96C
		public override bool Equals(object obj)
		{
			TextInfo textInfo = obj as TextInfo;
			return textInfo != null && this.CultureName.Equals(textInfo.CultureName);
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.TextInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x06003661 RID: 13921 RVA: 0x000D1796 File Offset: 0x000CF996
		public override int GetHashCode()
		{
			return this.CultureName.GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x06003662 RID: 13922 RVA: 0x000D17A3 File Offset: 0x000CF9A3
		public override string ToString()
		{
			return "TextInfo - " + this.m_cultureData.CultureName;
		}

		/// <summary>Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms)..</summary>
		/// <returns>The specified string converted to title case.</returns>
		/// <param name="str">The string to convert to title case. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x06003663 RID: 13923 RVA: 0x000D17BC File Offset: 0x000CF9BC
		public string ToTitleCase(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			for (int i = 0; i < str.Length; i++)
			{
				int num;
				UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
				if (char.CheckLetter(unicodeCategory))
				{
					i = this.AddTitlecaseLetter(ref stringBuilder, ref str, i, num) + 1;
					int num2 = i;
					bool flag = unicodeCategory == UnicodeCategory.LowercaseLetter;
					while (i < str.Length)
					{
						unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
						if (TextInfo.IsLetterCategory(unicodeCategory))
						{
							if (unicodeCategory == UnicodeCategory.LowercaseLetter)
							{
								flag = true;
							}
							i += num;
						}
						else if (str[i] == '\'')
						{
							i++;
							if (flag)
							{
								if (text == null)
								{
									text = this.ToLower(str);
								}
								stringBuilder.Append(text, num2, i - num2);
							}
							else
							{
								stringBuilder.Append(str, num2, i - num2);
							}
							num2 = i;
							flag = true;
						}
						else
						{
							if (TextInfo.IsWordSeparator(unicodeCategory))
							{
								break;
							}
							i += num;
						}
					}
					int num3 = i - num2;
					if (num3 > 0)
					{
						if (flag)
						{
							if (text == null)
							{
								text = this.ToLower(str);
							}
							stringBuilder.Append(text, num2, num3);
						}
						else
						{
							stringBuilder.Append(str, num2, num3);
						}
					}
					if (i < str.Length)
					{
						i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
					}
				}
				else
				{
					i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000D1909 File Offset: 0x000CFB09
		private static int AddNonLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(input[inputIndex++]);
				result.Append(input[inputIndex]);
			}
			else
			{
				result.Append(input[inputIndex]);
			}
			return inputIndex;
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000D1948 File Offset: 0x000CFB48
		private int AddTitlecaseLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(this.ToUpper(input.Substring(inputIndex, charLen)));
				inputIndex++;
			}
			else
			{
				char c = input[inputIndex];
				switch (c)
				{
				case 'Ǆ':
				case 'ǅ':
				case 'ǆ':
					result.Append('ǅ');
					break;
				case 'Ǉ':
				case 'ǈ':
				case 'ǉ':
					result.Append('ǈ');
					break;
				case 'Ǌ':
				case 'ǋ':
				case 'ǌ':
					result.Append('ǋ');
					break;
				default:
					switch (c)
					{
					case 'Ǳ':
					case 'ǲ':
					case 'ǳ':
						result.Append('ǲ');
						break;
					default:
						result.Append(this.ToUpper(input[inputIndex]));
						break;
					}
					break;
				}
			}
			return inputIndex;
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000D1A22 File Offset: 0x000CFC22
		private static bool IsWordSeparator(UnicodeCategory category)
		{
			return (536672256 & (1 << (int)category)) != 0;
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000D1A33 File Offset: 0x000CFC33
		private static bool IsLetterCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.UppercaseLetter || uc == UnicodeCategory.LowercaseLetter || uc == UnicodeCategory.TitlecaseLetter || uc == UnicodeCategory.ModifierLetter || uc == UnicodeCategory.OtherLetter;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object represents a writing system where text flows from right to left.</summary>
		/// <returns>true if text flows from right to left; otherwise, false.</returns>
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x000D1A4A File Offset: 0x000CFC4A
		[ComVisible(false)]
		public bool IsRightToLeft
		{
			get
			{
				return this.m_cultureData.IsRightToLeft;
			}
		}

		/// <summary>Raises the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		// Token: 0x06003669 RID: 13929 RVA: 0x000D15AE File Offset: 0x000CF7AE
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000D1A58 File Offset: 0x000CFC58
		private unsafe string ToUpperInternal(string str)
		{
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(str.Length);
			fixed (string text2 = str)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr2;
					char* ptr4 = ptr;
					for (int i = 0; i < str.Length; i++)
					{
						*ptr3 = this.ToUpper(*ptr4);
						ptr4++;
						ptr3++;
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000D1ADC File Offset: 0x000CFCDC
		private unsafe string ToLowerInternal(string str)
		{
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(str.Length);
			fixed (string text2 = str)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr2;
					char* ptr4 = ptr;
					for (int i = 0; i < str.Length; i++)
					{
						*ptr3 = this.ToLower(*ptr4);
						ptr4++;
						ptr3++;
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000D1B60 File Offset: 0x000CFD60
		private char ToUpperInternal(char c)
		{
			if (!this.m_cultureData.IsInvariantCulture)
			{
				if (c <= 'ǲ')
				{
					if (c > 'ſ')
					{
						if (c <= 'ǈ')
						{
							if (c != 'ǅ' && c != 'ǈ')
							{
								goto IL_015A;
							}
						}
						else if (c != 'ǋ' && c != 'ǲ')
						{
							goto IL_015A;
						}
						return c - '\u0001';
					}
					if (c == 'µ')
					{
						return 'Μ';
					}
					if (c == 'ı')
					{
						return 'I';
					}
					if (c == 'ſ')
					{
						return 'S';
					}
				}
				else if (c <= 'ϰ')
				{
					if (c <= 'ς')
					{
						if (c == '\u0345')
						{
							return 'Ι';
						}
						if (c == 'ς')
						{
							return 'Σ';
						}
					}
					else
					{
						switch (c)
						{
						case 'ϐ':
							return 'Β';
						case 'ϑ':
							return 'Θ';
						case 'ϒ':
						case 'ϓ':
						case 'ϔ':
							break;
						case 'ϕ':
							return 'Φ';
						case 'ϖ':
							return 'Π';
						default:
							if (c == 'ϰ')
							{
								return 'Κ';
							}
							break;
						}
					}
				}
				else if (c <= 'ϵ')
				{
					if (c == 'ϱ')
					{
						return 'Ρ';
					}
					if (c == 'ϵ')
					{
						return 'Ε';
					}
				}
				else
				{
					if (c == 'ẛ')
					{
						return 'Ṡ';
					}
					if (c == 'ι')
					{
						return 'Ι';
					}
				}
				IL_015A:
				if (!this.IsAsciiCasingSameAsInvariant)
				{
					if (c == 'i')
					{
						return 'İ';
					}
					if (TextInfo.IsAscii(c))
					{
						return TextInfo.ToUpperAsciiInvariant(c);
					}
				}
			}
			if (c >= 'à' && c <= 'ֆ')
			{
				return TextInfoToUpperData.range_00e0_0586[(int)(c - 'à')];
			}
			if (c >= 'ḁ' && c <= 'ῳ')
			{
				return TextInfoToUpperData.range_1e01_1ff3[(int)(c - 'ḁ')];
			}
			if (c >= 'ⅰ' && c <= 'ↄ')
			{
				return TextInfoToUpperData.range_2170_2184[(int)(c - 'ⅰ')];
			}
			if (c >= 'ⓐ' && c <= 'ⓩ')
			{
				return TextInfoToUpperData.range_24d0_24e9[(int)(c - 'ⓐ')];
			}
			if (c >= 'ⰰ' && c <= 'ⳣ')
			{
				return TextInfoToUpperData.range_2c30_2ce3[(int)(c - 'ⰰ')];
			}
			if (c >= 'ⴀ' && c <= 'ⴥ')
			{
				return TextInfoToUpperData.range_2d00_2d25[(int)(c - 'ⴀ')];
			}
			if (c >= 'ꙁ' && c <= 'ꚗ')
			{
				return TextInfoToUpperData.range_a641_a697[(int)(c - 'ꙁ')];
			}
			if (c >= 'ꜣ' && c <= 'ꞌ')
			{
				return TextInfoToUpperData.range_a723_a78c[(int)(c - 'ꜣ')];
			}
			if ('ａ' <= c && c <= 'ｚ')
			{
				return c - ' ';
			}
			if (c == 'ᵹ')
			{
				return 'Ᵹ';
			}
			if (c == 'ᵽ')
			{
				return 'Ᵽ';
			}
			if (c != 'ⅎ')
			{
				return c;
			}
			return 'Ⅎ';
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000D1E1C File Offset: 0x000D001C
		private char ToLowerInternal(char c)
		{
			if (!this.m_cultureData.IsInvariantCulture)
			{
				if (c <= 'ǲ')
				{
					if (c <= 'ǅ')
					{
						if (c == 'İ')
						{
							return 'i';
						}
						if (c != 'ǅ')
						{
							goto IL_00D3;
						}
					}
					else if (c != 'ǈ' && c != 'ǋ' && c != 'ǲ')
					{
						goto IL_00D3;
					}
					return c + '\u0001';
				}
				if (c <= 'ẞ')
				{
					switch (c)
					{
					case 'ϒ':
						return 'υ';
					case 'ϓ':
						return 'ύ';
					case 'ϔ':
						return 'ϋ';
					default:
						if (c == 'ϴ')
						{
							return 'θ';
						}
						if (c == 'ẞ')
						{
							return 'ß';
						}
						break;
					}
				}
				else
				{
					if (c == 'Ω')
					{
						return 'ω';
					}
					if (c == 'K')
					{
						return 'k';
					}
					if (c == 'Å')
					{
						return 'å';
					}
				}
				IL_00D3:
				if (!this.IsAsciiCasingSameAsInvariant)
				{
					if (c == 'I')
					{
						return 'ı';
					}
					if (TextInfo.IsAscii(c))
					{
						return TextInfo.ToLowerAsciiInvariant(c);
					}
				}
			}
			if (c >= 'À' && c <= 'Ֆ')
			{
				return TextInfoToLowerData.range_00c0_0556[(int)(c - 'À')];
			}
			if (c >= 'Ⴀ' && c <= 'Ⴥ')
			{
				return TextInfoToLowerData.range_10a0_10c5[(int)(c - 'Ⴀ')];
			}
			if (c >= 'Ḁ' && c <= 'ῼ')
			{
				return TextInfoToLowerData.range_1e00_1ffc[(int)(c - 'Ḁ')];
			}
			if (c >= 'Ⅰ' && c <= 'Ⅿ')
			{
				return TextInfoToLowerData.range_2160_216f[(int)(c - 'Ⅰ')];
			}
			if (c >= 'Ⓐ' && c <= 'Ⓩ')
			{
				return TextInfoToLowerData.range_24b6_24cf[(int)(c - 'Ⓐ')];
			}
			if (c >= 'Ⰰ' && c <= 'Ⱞ')
			{
				return TextInfoToLowerData.range_2c00_2c2e[(int)(c - 'Ⰰ')];
			}
			if (c >= 'Ⱡ' && c <= 'Ⳣ')
			{
				return TextInfoToLowerData.range_2c60_2ce2[(int)(c - 'Ⱡ')];
			}
			if (c >= 'Ꙁ' && c <= 'Ꚗ')
			{
				return TextInfoToLowerData.range_a640_a696[(int)(c - 'Ꙁ')];
			}
			if (c >= 'Ꜣ' && c <= 'Ꞌ')
			{
				return TextInfoToLowerData.range_a722_a78b[(int)(c - 'Ꜣ')];
			}
			if ('Ａ' <= c && c <= 'Ｚ')
			{
				return c + ' ';
			}
			if (c == 'Ⅎ')
			{
				return 'ⅎ';
			}
			if (c != 'Ↄ')
			{
				return c;
			}
			return 'ↄ';
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000D2064 File Offset: 0x000D0264
		internal unsafe void ToUpperAsciiInvariant(ReadOnlySpan<char> source, Span<char> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = TextInfo.ToUpperAsciiInvariant((char)(*source[i]));
			}
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000D209C File Offset: 0x000D029C
		internal unsafe void ChangeCase(ReadOnlySpan<char> source, Span<char> destination, bool toUpper)
		{
			if (source.IsEmpty)
			{
				return;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(destination))
				{
					char* ptr2 = reference2;
					int i = 0;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					if (toUpper)
					{
						while (i < source.Length)
						{
							*(ptr4++) = this.ToUpper(*(ptr3++));
							i++;
						}
					}
					else
					{
						while (i < source.Length)
						{
							*(ptr4++) = this.ToLower(*(ptr3++));
							i++;
						}
					}
				}
			}
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000176B9 File Offset: 0x000158B9
		internal TextInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001D39 RID: 7481
		[OptionalField(VersionAdded = 2)]
		private string m_listSeparator;

		// Token: 0x04001D3A RID: 7482
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04001D3B RID: 7483
		[OptionalField(VersionAdded = 3)]
		private string m_cultureName;

		// Token: 0x04001D3C RID: 7484
		[NonSerialized]
		private CultureData m_cultureData;

		// Token: 0x04001D3D RID: 7485
		[NonSerialized]
		private string m_textInfoName;

		// Token: 0x04001D3E RID: 7486
		[NonSerialized]
		private bool? m_IsAsciiCasingSameAsInvariant;

		// Token: 0x04001D3F RID: 7487
		internal static volatile TextInfo s_Invariant;

		// Token: 0x04001D40 RID: 7488
		[OptionalField(VersionAdded = 2)]
		private string customCultureName;

		// Token: 0x04001D41 RID: 7489
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x04001D42 RID: 7490
		[OptionalField(VersionAdded = 1)]
		internal int m_win32LangID;
	}
}
