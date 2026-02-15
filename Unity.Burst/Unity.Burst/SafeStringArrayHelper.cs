using System;
using System.Collections.Generic;
using System.Text;

namespace Unity.Burst
{
	// Token: 0x0200002B RID: 43
	internal static class SafeStringArrayHelper
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00005858 File Offset: 0x00003A58
		public static string SerialiseStringArraySafe(string[] array)
		{
			StringBuilder s = new StringBuilder();
			foreach (string entry in array)
			{
				s.Append(string.Format("{0}]", Encoding.UTF8.GetByteCount(entry)));
				s.Append(entry);
			}
			return s.ToString();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000058B0 File Offset: 0x00003AB0
		public static string[] DeserialiseStringArraySafe(string input)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			List<string> listFolders = new List<string>();
			int index = 0;
			int length = bytes.Length;
			IL_0097:
			while (index < length)
			{
				int len = 0;
				while (index < length)
				{
					byte d = bytes[index];
					if (d == 93)
					{
						index++;
						listFolders.Add(Encoding.UTF8.GetString(bytes, index, len));
						index += len;
						goto IL_0097;
					}
					if (d < 48 || d > 57)
					{
						throw new FormatException(string.Format("Invalid input `{0}` at {1}: Got non-digit character while reading length", input, index));
					}
					len = len * 10 + (int)(d - 48);
					index++;
				}
				throw new FormatException("Invalid input `" + input + "`: reached end while reading length");
			}
			return listFolders.ToArray();
		}
	}
}
