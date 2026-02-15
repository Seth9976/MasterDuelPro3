using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x020007E3 RID: 2019
	internal class UnexceptionalStreamReader : StreamReader
	{
		// Token: 0x06004110 RID: 16656 RVA: 0x000FB330 File Offset: 0x000F9530
		static UnexceptionalStreamReader()
		{
			string newLine = Environment.NewLine;
			if (newLine.Length == 1)
			{
				UnexceptionalStreamReader.newlineChar = newLine[0];
			}
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x000FB36C File Offset: 0x000F956C
		public UnexceptionalStreamReader(Stream stream, Encoding encoding)
			: base(stream, encoding)
		{
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x000FB378 File Offset: 0x000F9578
		public override int Peek()
		{
			try
			{
				return base.Peek();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x000FB3A4 File Offset: 0x000F95A4
		public override int Read()
		{
			try
			{
				return base.Read();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x000FB3D0 File Offset: 0x000F95D0
		public override int Read([In] [Out] char[] dest_buffer, int index, int count)
		{
			if (dest_buffer == null)
			{
				throw new ArgumentNullException("dest_buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest_buffer.Length - count)
			{
				throw new ArgumentException("index + count > dest_buffer.Length");
			}
			int num = 0;
			char c = UnexceptionalStreamReader.newlineChar;
			try
			{
				while (count > 0)
				{
					int num2 = base.Read();
					if (num2 < 0)
					{
						break;
					}
					num++;
					count--;
					dest_buffer[index] = (char)num2;
					if (c != '\0')
					{
						if ((char)num2 == c)
						{
							return num;
						}
					}
					else if (this.CheckEOL((char)num2))
					{
						return num;
					}
					index++;
				}
			}
			catch (IOException)
			{
			}
			return num;
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x000FB484 File Offset: 0x000F9684
		private bool CheckEOL(char current)
		{
			int i = 0;
			while (i < UnexceptionalStreamReader.newline.Length)
			{
				if (!UnexceptionalStreamReader.newline[i])
				{
					if (current == Environment.NewLine[i])
					{
						UnexceptionalStreamReader.newline[i] = true;
						return i == UnexceptionalStreamReader.newline.Length - 1;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			for (int j = 0; j < UnexceptionalStreamReader.newline.Length; j++)
			{
				UnexceptionalStreamReader.newline[j] = false;
			}
			return false;
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x000FB4EC File Offset: 0x000F96EC
		public override string ReadLine()
		{
			try
			{
				return base.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000FB518 File Offset: 0x000F9718
		public override string ReadToEnd()
		{
			try
			{
				return base.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x04002121 RID: 8481
		private static bool[] newline = new bool[Environment.NewLine.Length];

		// Token: 0x04002122 RID: 8482
		private static char newlineChar;
	}
}
