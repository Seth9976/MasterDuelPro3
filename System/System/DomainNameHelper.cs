using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000103 RID: 259
	internal class DomainNameHelper
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0001B510 File Offset: 0x00019710
		internal static string ParseCanonicalName(string str, int start, int end, ref bool loopback)
		{
			string text = null;
			for (int i = end - 1; i >= start; i--)
			{
				if (str[i] >= 'A' && str[i] <= 'Z')
				{
					text = str.Substring(start, end - start).ToLower(CultureInfo.InvariantCulture);
					break;
				}
				if (str[i] == ':')
				{
					end = i;
				}
			}
			if (text == null)
			{
				text = str.Substring(start, end - start);
			}
			if (text == "localhost" || text == "loopback")
			{
				loopback = true;
				return "localhost";
			}
			return text;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001B59C File Offset: 0x0001979C
		internal unsafe static bool IsValid(char* name, ushort pos, ref int returnedEnd, ref bool notCanonical, bool notImplicitFile)
		{
			char* ptr = name + pos;
			char* ptr2 = ptr;
			char* ptr3 = name + returnedEnd;
			while (ptr2 < ptr3)
			{
				char c = *ptr2;
				if (c > '\u007f')
				{
					return false;
				}
				if (c == '/' || c == '\\' || (notImplicitFile && (c == ':' || c == '?' || c == '#')))
				{
					ptr3 = ptr2;
					break;
				}
				ptr2++;
			}
			if (ptr3 == ptr)
			{
				return false;
			}
			for (;;)
			{
				ptr2 = ptr;
				while (ptr2 < ptr3 && *ptr2 != '.')
				{
					ptr2++;
				}
				if (ptr == ptr2 || (long)(ptr2 - ptr) > 63L || !DomainNameHelper.IsASCIILetterOrDigit(*(ptr++), ref notCanonical))
				{
					break;
				}
				while (ptr < ptr2)
				{
					if (!DomainNameHelper.IsValidDomainLabelCharacter(*(ptr++), ref notCanonical))
					{
						return false;
					}
				}
				ptr++;
				if (ptr >= ptr3)
				{
					goto Block_13;
				}
			}
			return false;
			Block_13:
			returnedEnd = (int)((ushort)((long)(ptr3 - name)));
			return true;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001B64C File Offset: 0x0001984C
		internal unsafe static bool IsValidByIri(char* name, ushort pos, ref int returnedEnd, ref bool notCanonical, bool notImplicitFile)
		{
			char* ptr = name + pos;
			char* ptr2 = ptr;
			char* ptr3 = name + returnedEnd;
			while (ptr2 < ptr3)
			{
				char c = *ptr2;
				if (c == '/' || c == '\\' || (notImplicitFile && (c == ':' || c == '?' || c == '#')))
				{
					ptr3 = ptr2;
					break;
				}
				ptr2++;
			}
			if (ptr3 == ptr)
			{
				return false;
			}
			for (;;)
			{
				ptr2 = ptr;
				int num = 0;
				bool flag = false;
				while (ptr2 < ptr3 && *ptr2 != '.' && *ptr2 != '。' && *ptr2 != '．' && *ptr2 != '｡')
				{
					num++;
					if (*ptr2 > 'ÿ')
					{
						num++;
					}
					if (*ptr2 >= '\u00a0')
					{
						flag = true;
					}
					ptr2++;
				}
				if (ptr == ptr2 || (flag ? (num + 4) : num) > 63 || (*(ptr++) < '\u00a0' && !DomainNameHelper.IsASCIILetterOrDigit(*(ptr - 1), ref notCanonical)))
				{
					break;
				}
				while (ptr < ptr2)
				{
					if (*(ptr++) < '\u00a0' && !DomainNameHelper.IsValidDomainLabelCharacter(*(ptr - 1), ref notCanonical))
					{
						return false;
					}
				}
				ptr++;
				if (ptr >= ptr3)
				{
					goto Block_20;
				}
			}
			return false;
			Block_20:
			returnedEnd = (int)((ushort)((long)(ptr3 - name)));
			return true;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001B758 File Offset: 0x00019958
		internal unsafe static string IdnEquivalent(string hostname)
		{
			bool flag = true;
			bool flag2 = false;
			char* ptr = hostname;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return DomainNameHelper.IdnEquivalent(ptr, 0, hostname.Length, ref flag, ref flag2);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001B78C File Offset: 0x0001998C
		internal unsafe static string IdnEquivalent(char* hostname, int start, int end, ref bool allAscii, ref bool atLeastOneValidIdn)
		{
			string text = null;
			string text2 = DomainNameHelper.IdnEquivalent(hostname, start, end, ref allAscii, ref text);
			if (text2 != null)
			{
				string text4;
				string text3 = (text4 = (allAscii ? text2 : text));
				char* ptr = text4;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int length = text3.Length;
				int i = 0;
				int num = 0;
				bool flag = false;
				do
				{
					bool flag2 = false;
					bool flag3 = false;
					flag = false;
					for (i = num; i < length; i++)
					{
						char c = ptr[i];
						if (!flag3)
						{
							flag3 = true;
							if (i + 3 < length && DomainNameHelper.IsIdnAce(ptr, i))
							{
								i += 4;
								flag2 = true;
								continue;
							}
						}
						if (c == '.' || c == '。' || c == '．' || c == '｡')
						{
							flag = true;
							break;
						}
					}
					if (flag2)
					{
						try
						{
							new IdnMapping().GetUnicode(new string(ptr, num, i - num));
							atLeastOneValidIdn = true;
							break;
						}
						catch (ArgumentException)
						{
						}
					}
					num = i + (flag ? 1 : 0);
				}
				while (num < length);
				text4 = null;
			}
			else
			{
				atLeastOneValidIdn = false;
			}
			return text2;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001B8A0 File Offset: 0x00019AA0
		internal unsafe static string IdnEquivalent(char* hostname, int start, int end, ref bool allAscii, ref string bidiStrippedHost)
		{
			string text = null;
			if (end <= start)
			{
				return text;
			}
			int i = start;
			allAscii = true;
			while (i < end)
			{
				if (hostname[i] > '\u007f')
				{
					allAscii = false;
					break;
				}
				i++;
			}
			if (!allAscii)
			{
				IdnMapping idnMapping = new IdnMapping();
				bidiStrippedHost = Uri.StripBidiControlCharacter(hostname, start, end - start);
				string ascii;
				try
				{
					ascii = idnMapping.GetAscii(bidiStrippedHost);
				}
				catch (ArgumentException)
				{
					throw new UriFormatException(SR.GetString("An invalid Unicode character by IDN standards was specified in the host."));
				}
				return ascii;
			}
			string text2 = new string(hostname, start, end - start);
			if (text2 == null)
			{
				return null;
			}
			return text2.ToLowerInvariant();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001B934 File Offset: 0x00019B34
		private static bool IsIdnAce(string input, int index)
		{
			return input[index] == 'x' && input[index + 1] == 'n' && input[index + 2] == '-' && input[index + 3] == '-';
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001B96B File Offset: 0x00019B6B
		private unsafe static bool IsIdnAce(char* input, int index)
		{
			return input[index] == 'x' && input[index + 1] == 'n' && input[index + 2] == '-' && input[index + 3] == '-';
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001B9A4 File Offset: 0x00019BA4
		internal unsafe static string UnicodeEquivalent(string idnHost, char* hostname, int start, int end)
		{
			IdnMapping idnMapping = new IdnMapping();
			try
			{
				return idnMapping.GetUnicode(idnHost);
			}
			catch (ArgumentException)
			{
			}
			bool flag = true;
			return DomainNameHelper.UnicodeEquivalent(hostname, start, end, ref flag, ref flag);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		internal unsafe static string UnicodeEquivalent(char* hostname, int start, int end, ref bool allAscii, ref bool atLeastOneValidIdn)
		{
			IdnMapping idnMapping = new IdnMapping();
			allAscii = true;
			atLeastOneValidIdn = false;
			string text = null;
			if (end <= start)
			{
				return text;
			}
			string text2 = Uri.StripBidiControlCharacter(hostname, start, end - start);
			string text3 = null;
			int num = 0;
			int i = 0;
			int length = text2.Length;
			bool flag = false;
			do
			{
				bool flag2 = true;
				bool flag3 = false;
				bool flag4 = false;
				flag = false;
				for (i = num; i < length; i++)
				{
					char c = text2[i];
					if (!flag4)
					{
						flag4 = true;
						if (i + 3 < length && c == 'x' && DomainNameHelper.IsIdnAce(text2, i))
						{
							flag3 = true;
						}
					}
					if (flag2 && c > '\u007f')
					{
						flag2 = false;
						allAscii = false;
					}
					if (c == '.' || c == '。' || c == '．' || c == '｡')
					{
						flag = true;
						break;
					}
				}
				if (!flag2)
				{
					string text4 = text2.Substring(num, i - num);
					try
					{
						text4 = idnMapping.GetAscii(text4);
					}
					catch (ArgumentException)
					{
						throw new UriFormatException(SR.GetString("An invalid Unicode character by IDN standards was specified in the host."));
					}
					text3 += idnMapping.GetUnicode(text4);
					if (flag)
					{
						text3 += ".";
					}
				}
				else
				{
					bool flag5 = false;
					if (flag3)
					{
						try
						{
							text3 += idnMapping.GetUnicode(text2.Substring(num, i - num));
							if (flag)
							{
								text3 += ".";
							}
							flag5 = true;
							atLeastOneValidIdn = true;
						}
						catch (ArgumentException)
						{
						}
					}
					if (!flag5)
					{
						text3 += text2.Substring(num, i - num).ToLowerInvariant();
						if (flag)
						{
							text3 += ".";
						}
					}
				}
				num = i + (flag ? 1 : 0);
			}
			while (num < length);
			return text3;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001BB9C File Offset: 0x00019D9C
		private static bool IsASCIILetterOrDigit(char character, ref bool notCanonical)
		{
			if ((character >= 'a' && character <= 'z') || (character >= '0' && character <= '9'))
			{
				return true;
			}
			if (character >= 'A' && character <= 'Z')
			{
				notCanonical = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001BBC4 File Offset: 0x00019DC4
		private static bool IsValidDomainLabelCharacter(char character, ref bool notCanonical)
		{
			if ((character >= 'a' && character <= 'z') || (character >= '0' && character <= '9') || character == '-' || character == '_')
			{
				return true;
			}
			if (character >= 'A' && character <= 'Z')
			{
				notCanonical = true;
				return true;
			}
			return false;
		}
	}
}
