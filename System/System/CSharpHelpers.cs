using System;
using System.Collections.Generic;
using System.Globalization;

namespace System
{
	// Token: 0x020000E7 RID: 231
	internal abstract class CSharpHelpers
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		static CSharpHelpers()
		{
			for (int i = 0; i < CSharpHelpers.s_keywords.Length; i++)
			{
				string[] array = CSharpHelpers.s_keywords[i];
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						CSharpHelpers.s_fixedStringLookup.Add(array[j], null);
					}
				}
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00010352 File Offset: 0x0000E552
		public static string CreateEscapedIdentifier(string name)
		{
			if (CSharpHelpers.IsKeyword(name) || CSharpHelpers.IsPrefixTwoUnderscore(name))
			{
				return "@" + name;
			}
			return name;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00010371 File Offset: 0x0000E571
		internal static bool IsKeyword(string value)
		{
			return CSharpHelpers.s_fixedStringLookup.ContainsKey(value);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001037E File Offset: 0x0000E57E
		internal static bool IsPrefixTwoUnderscore(string value)
		{
			return value.Length >= 3 && (value[0] == '_' && value[1] == '_') && value[2] != '_';
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000103B4 File Offset: 0x0000E5B4
		internal static bool IsValidTypeNameOrIdentifier(string value, bool isTypeName)
		{
			bool flag = true;
			if (value.Length == 0)
			{
				return false;
			}
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				switch (CharUnicodeInfo.GetUnicodeCategory(c))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.LetterNumber:
					flag = false;
					break;
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.ConnectorPunctuation:
					if (flag && c != '_')
					{
						return false;
					}
					flag = false;
					break;
				case UnicodeCategory.EnclosingMark:
				case UnicodeCategory.OtherNumber:
				case UnicodeCategory.SpaceSeparator:
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Control:
				case UnicodeCategory.Format:
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_0088;
				default:
					goto IL_0088;
				}
				IL_0097:
				i++;
				continue;
				IL_0088:
				if (!isTypeName || !CSharpHelpers.IsSpecialTypeChar(c, ref flag))
				{
					return false;
				}
				goto IL_0097;
			}
			return true;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001046C File Offset: 0x0000E66C
		internal static bool IsSpecialTypeChar(char ch, ref bool nextMustBeStartChar)
		{
			if (ch <= '>')
			{
				switch (ch)
				{
				case '$':
				case '&':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
					break;
				case '%':
				case '\'':
				case '(':
				case ')':
					return false;
				default:
					switch (ch)
					{
					case ':':
					case '<':
					case '>':
						break;
					case ';':
					case '=':
						return false;
					default:
						return false;
					}
					break;
				}
			}
			else if (ch != '[' && ch != ']')
			{
				if (ch != '`')
				{
					return false;
				}
				return true;
			}
			nextMustBeStartChar = true;
			return true;
		}

		// Token: 0x04000381 RID: 897
		private static Dictionary<string, object> s_fixedStringLookup = new Dictionary<string, object>();

		// Token: 0x04000382 RID: 898
		private static readonly string[][] s_keywords = new string[][]
		{
			null,
			new string[] { "as", "do", "if", "in", "is" },
			new string[] { "for", "int", "new", "out", "ref", "try" },
			new string[]
			{
				"base", "bool", "byte", "case", "char", "else", "enum", "goto", "lock", "long",
				"null", "this", "true", "uint", "void"
			},
			new string[]
			{
				"break", "catch", "class", "const", "event", "false", "fixed", "float", "sbyte", "short",
				"throw", "ulong", "using", "where", "while", "yield"
			},
			new string[]
			{
				"double", "extern", "object", "params", "public", "return", "sealed", "sizeof", "static", "string",
				"struct", "switch", "typeof", "unsafe", "ushort"
			},
			new string[] { "checked", "decimal", "default", "finally", "foreach", "partial", "private", "virtual" },
			new string[] { "abstract", "continue", "delegate", "explicit", "implicit", "internal", "operator", "override", "readonly", "volatile" },
			new string[] { "__arglist", "__makeref", "__reftype", "interface", "namespace", "protected", "unchecked" },
			new string[] { "__refvalue", "stackalloc" }
		};
	}
}
