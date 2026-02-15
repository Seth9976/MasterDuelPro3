using System;
using System.Text;

namespace System
{
	// Token: 0x020000EB RID: 235
	internal static class PasteArguments
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x00011494 File Offset: 0x0000F694
		internal static void AppendArgument(StringBuilder stringBuilder, string argument)
		{
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Append(' ');
			}
			if (argument.Length != 0 && PasteArguments.ContainsNoWhitespaceOrQuotes(argument))
			{
				stringBuilder.Append(argument);
				return;
			}
			stringBuilder.Append('"');
			int i = 0;
			while (i < argument.Length)
			{
				char c = argument[i++];
				if (c == '\\')
				{
					int num = 1;
					while (i < argument.Length && argument[i] == '\\')
					{
						i++;
						num++;
					}
					if (i == argument.Length)
					{
						stringBuilder.Append('\\', num * 2);
					}
					else if (argument[i] == '"')
					{
						stringBuilder.Append('\\', num * 2 + 1);
						stringBuilder.Append('"');
						i++;
					}
					else
					{
						stringBuilder.Append('\\', num);
					}
				}
				else if (c == '"')
				{
					stringBuilder.Append('\\');
					stringBuilder.Append('"');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			stringBuilder.Append('"');
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00011590 File Offset: 0x0000F790
		private static bool ContainsNoWhitespaceOrQuotes(string s)
		{
			foreach (char c in s)
			{
				if (char.IsWhiteSpace(c) || c == '"')
				{
					return false;
				}
			}
			return true;
		}
	}
}
