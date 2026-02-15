using System;

namespace System
{
	// Token: 0x020000EC RID: 236
	internal static class StringExtensions
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x000115C6 File Offset: 0x0000F7C6
		internal static string SubstringTrim(this string value, int startIndex)
		{
			return value.SubstringTrim(startIndex, value.Length - startIndex);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000115D8 File Offset: 0x0000F7D8
		internal static string SubstringTrim(this string value, int startIndex, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			int num = startIndex + length - 1;
			while (startIndex <= num)
			{
				if (!char.IsWhiteSpace(value[startIndex]))
				{
					break;
				}
				startIndex++;
			}
			while (num >= startIndex && char.IsWhiteSpace(value[num]))
			{
				num--;
			}
			int num2 = num - startIndex + 1;
			if (num2 == 0)
			{
				return string.Empty;
			}
			if (num2 != value.Length)
			{
				return value.Substring(startIndex, num2);
			}
			return value;
		}
	}
}
