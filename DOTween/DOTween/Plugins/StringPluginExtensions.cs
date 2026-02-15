using System;
using System.Text;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x02000083 RID: 131
	internal static class StringPluginExtensions
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		static StringPluginExtensions()
		{
			StringPluginExtensions.ScrambledCharsAll.ScrambleChars();
			StringPluginExtensions.ScrambledCharsUppercase.ScrambleChars();
			StringPluginExtensions.ScrambledCharsLowercase.ScrambleChars();
			StringPluginExtensions.ScrambledCharsNumerals.ScrambleChars();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000E378 File Offset: 0x0000C578
		internal static void ScrambleChars(this char[] chars)
		{
			int num = chars.Length;
			for (int i = 0; i < num; i++)
			{
				char c = chars[i];
				int num2 = Random.Range(i, num);
				chars[i] = chars[num2];
				chars[num2] = c;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		internal static StringBuilder AppendScrambledChars(this StringBuilder buffer, int length, char[] chars)
		{
			if (length <= 0)
			{
				return buffer;
			}
			int num = chars.Length;
			int num2;
			for (num2 = StringPluginExtensions._lastRndSeed; num2 == StringPluginExtensions._lastRndSeed; num2 = Random.Range(0, num))
			{
			}
			StringPluginExtensions._lastRndSeed = num2;
			for (int i = 0; i < length; i++)
			{
				if (num2 >= num)
				{
					num2 = 0;
				}
				buffer.Append(chars[num2]);
				num2++;
			}
			return buffer;
		}

		// Token: 0x04000162 RID: 354
		public static readonly char[] ScrambledCharsAll = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e',
			'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
			'p', 'q', 'r', 's', 't', 'u', 'v', 'x', 'y', 'z',
			'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
		};

		// Token: 0x04000163 RID: 355
		public static readonly char[] ScrambledCharsUppercase = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'X', 'Y', 'Z'
		};

		// Token: 0x04000164 RID: 356
		public static readonly char[] ScrambledCharsLowercase = new char[]
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
			'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
			'u', 'v', 'x', 'y', 'z'
		};

		// Token: 0x04000165 RID: 357
		public static readonly char[] ScrambledCharsNumerals = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

		// Token: 0x04000166 RID: 358
		private static int _lastRndSeed;
	}
}
