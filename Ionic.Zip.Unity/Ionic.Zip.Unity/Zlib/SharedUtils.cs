using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000066 RID: 102
	internal class SharedUtils
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x0001CCF5 File Offset: 0x0001AEF5
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001CD00 File Offset: 0x0001AF00
		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)array[i];
			}
			return num;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001CD42 File Offset: 0x0001AF42
		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001CD4F File Offset: 0x0001AF4F
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}
	}
}
