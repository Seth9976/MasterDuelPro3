using System;

namespace System.Data
{
	// Token: 0x02000075 RID: 117
	internal sealed class Operators
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x0001FF9C File Offset: 0x0001E19C
		internal static bool IsArithmetical(int op)
		{
			return op == 15 || op == 16 || op == 17 || op == 18 || op == 20;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001FFB9 File Offset: 0x0001E1B9
		internal static bool IsLogical(int op)
		{
			return op == 26 || op == 27 || op == 3 || op == 13 || op == 39;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001FFD5 File Offset: 0x0001E1D5
		internal static bool IsRelational(int op)
		{
			return 7 <= op && op <= 12;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001FFE5 File Offset: 0x0001E1E5
		internal static int Priority(int op)
		{
			if (op > Operators.s_priority.Length)
			{
				return 24;
			}
			return Operators.s_priority[op];
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001FFFC File Offset: 0x0001E1FC
		internal static string ToString(int op)
		{
			string text;
			if (op <= Operators.s_looks.Length)
			{
				text = Operators.s_looks[op];
			}
			else
			{
				text = "Unknown op";
			}
			return text;
		}

		// Token: 0x04000275 RID: 629
		private static readonly int[] s_priority = new int[]
		{
			0, 20, 20, 9, 12, 11, 11, 13, 13, 13,
			13, 13, 13, 10, 11, 16, 16, 19, 19, 18,
			17, 21, 8, 7, 6, 9, 8, 7, 2, 22,
			23, 23, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24
		};

		// Token: 0x04000276 RID: 630
		private static readonly string[] s_looks = new string[]
		{
			"", "-", "+", "Not", "BetweenAnd", "In", "Between", "=", ">", "<",
			">=", "<=", "<>", "Is", "Like", "+", "-", "*", "/", "\\",
			"Mod", "**", "&", "|", "^", "~", "And", "Or", "Proc", "Iff",
			".", ".", "Null", "True", "False", "Date", "GenUniqueId()", "GenGuid()", "Guid {..}", "Is Not"
		};
	}
}
