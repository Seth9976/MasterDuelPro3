using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000068 RID: 104
	internal static class TextUtilities
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0002FF0C File Offset: 0x0002E10C
		internal static char ToUpperFast(char c)
		{
			bool flag = (int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1;
			char c2;
			if (flag)
			{
				c2 = c;
			}
			else
			{
				c2 = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
			}
			return c2;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0002FF40 File Offset: 0x0002E140
		public static int GetHashCodeCaseInSensitive(string s)
		{
			int hashCode = 0;
			for (int i = 0; i < s.Length; i++)
			{
				hashCode = ((hashCode << 5) + hashCode) ^ (int)TextUtilities.ToUpperFast(s[i]);
			}
			return hashCode;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0002FF80 File Offset: 0x0002E180
		internal static int GetTextFontWeightIndex(TextFontWeight fontWeight)
		{
			if (fontWeight <= TextFontWeight.Regular)
			{
				if (fontWeight <= TextFontWeight.ExtraLight)
				{
					if (fontWeight == TextFontWeight.Thin)
					{
						return 1;
					}
					if (fontWeight == TextFontWeight.ExtraLight)
					{
						return 2;
					}
				}
				else
				{
					if (fontWeight == TextFontWeight.Light)
					{
						return 3;
					}
					if (fontWeight == TextFontWeight.Regular)
					{
						return 4;
					}
				}
			}
			else if (fontWeight <= TextFontWeight.SemiBold)
			{
				if (fontWeight == TextFontWeight.Medium)
				{
					return 5;
				}
				if (fontWeight == TextFontWeight.SemiBold)
				{
					return 6;
				}
			}
			else
			{
				if (fontWeight == TextFontWeight.Bold)
				{
					return 7;
				}
				if (fontWeight == TextFontWeight.Heavy)
				{
					return 8;
				}
				if (fontWeight == TextFontWeight.Black)
				{
					return 9;
				}
			}
			return 4;
		}
	}
}
