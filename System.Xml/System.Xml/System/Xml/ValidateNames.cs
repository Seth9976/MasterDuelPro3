using System;

namespace System.Xml
{
	// Token: 0x02000110 RID: 272
	internal static class ValidateNames
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x0004866C File Offset: 0x0004686C
		internal static int ParseNmtoken(string s, int offset)
		{
			int num = offset;
			while (num < s.Length && (ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0)
			{
				num++;
			}
			return num - offset;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x000486A4 File Offset: 0x000468A4
		internal static int ParseNmtokenNoNamespaces(string s, int offset)
		{
			int num = offset;
			while (num < s.Length && ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0 || s[num] == ':'))
			{
				num++;
			}
			return num - offset;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x000486E8 File Offset: 0x000468E8
		internal static bool IsNmtokenNoNamespaces(string s)
		{
			int num = ValidateNames.ParseNmtokenNoNamespaces(s, 0);
			return num > 0 && num == s.Length;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0004870C File Offset: 0x0004690C
		internal static int ParseNameNoNamespaces(string s, int offset)
		{
			int num = offset;
			if (num < s.Length)
			{
				if ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 4) == 0 && s[num] != ':')
				{
					return 0;
				}
				num++;
				while (num < s.Length && ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0 || s[num] == ':'))
				{
					num++;
				}
			}
			return num - offset;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00048780 File Offset: 0x00046980
		internal static bool IsNameNoNamespaces(string s)
		{
			int num = ValidateNames.ParseNameNoNamespaces(s, 0);
			return num > 0 && num == s.Length;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000487A4 File Offset: 0x000469A4
		internal static int ParseNCName(string s, int offset)
		{
			int num = offset;
			if (num < s.Length)
			{
				if ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 4) == 0)
				{
					return 0;
				}
				num++;
				while (num < s.Length && (ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0)
				{
					num++;
				}
			}
			return num - offset;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00048800 File Offset: 0x00046A00
		internal static int ParseNCName(string s)
		{
			return ValidateNames.ParseNCName(s, 0);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0004880C File Offset: 0x00046A0C
		internal static int ParseQName(string s, int offset, out int colonOffset)
		{
			colonOffset = 0;
			int num = ValidateNames.ParseNCName(s, offset);
			if (num != 0)
			{
				offset += num;
				if (offset < s.Length && s[offset] == ':')
				{
					int num2 = ValidateNames.ParseNCName(s, offset + 1);
					if (num2 != 0)
					{
						colonOffset = offset;
						num += num2 + 1;
					}
				}
			}
			return num;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00048858 File Offset: 0x00046A58
		internal static void ParseQNameThrow(string s, out string prefix, out string localName)
		{
			int num2;
			int num = ValidateNames.ParseQName(s, 0, out num2);
			if (num == 0 || num != s.Length)
			{
				ValidateNames.ThrowInvalidName(s, 0, num);
			}
			if (num2 != 0)
			{
				prefix = s.Substring(0, num2);
				localName = s.Substring(num2 + 1);
				return;
			}
			prefix = "";
			localName = s;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x000488A8 File Offset: 0x00046AA8
		internal static void ThrowInvalidName(string s, int offsetStartChar, int offsetBadChar)
		{
			if (offsetStartChar >= s.Length)
			{
				throw new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			if (ValidateNames.xmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !XmlCharType.Instance.IsStartNCNameSingleChar(s[offsetBadChar]))
			{
				throw new XmlException("Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
			}
			throw new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0004891C File Offset: 0x00046B1C
		internal static Exception GetInvalidNameException(string s, int offsetStartChar, int offsetBadChar)
		{
			if (offsetStartChar >= s.Length)
			{
				return new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			if (ValidateNames.xmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !ValidateNames.xmlCharType.IsStartNCNameSingleChar(s[offsetBadChar]))
			{
				return new XmlException("Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
			}
			return new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0004898C File Offset: 0x00046B8C
		internal static void SplitQName(string name, out string prefix, out string lname)
		{
			int num = name.IndexOf(':');
			if (-1 == num)
			{
				prefix = string.Empty;
				lname = name;
				return;
			}
			if (num == 0 || name.Length - 1 == num)
			{
				string text = "The '{0}' character, hexadecimal value {1}, cannot be included in a name.";
				object[] array = XmlException.BuildCharExceptionArgs(':', '\0');
				throw new ArgumentException(Res.GetString(text, array), "name");
			}
			prefix = name.Substring(0, num);
			num++;
			lname = name.Substring(num, name.Length - num);
		}

		// Token: 0x04000721 RID: 1825
		private static XmlCharType xmlCharType = XmlCharType.Instance;
	}
}
