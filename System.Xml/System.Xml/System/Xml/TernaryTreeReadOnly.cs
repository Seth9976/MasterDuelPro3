using System;

namespace System.Xml
{
	// Token: 0x0200003E RID: 62
	internal class TernaryTreeReadOnly
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000CE89 File Offset: 0x0000B089
		public TernaryTreeReadOnly(byte[] nodeBuffer)
		{
			this.nodeBuffer = nodeBuffer;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000CE98 File Offset: 0x0000B098
		public byte FindCaseInsensitiveString(string stringToFind)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.nodeBuffer;
			int num3 = (int)stringToFind[num];
			if (num3 > 122)
			{
				return 0;
			}
			if (num3 >= 97)
			{
				num3 -= 32;
			}
			int num4;
			for (;;)
			{
				num4 = num2 * 4;
				int num5 = (int)array[num4];
				if (num3 < num5)
				{
					if (array[num4 + 1] == 0)
					{
						return 0;
					}
					num2 += (int)array[num4 + 1];
				}
				else if (num3 > num5)
				{
					if (array[num4 + 2] == 0)
					{
						return 0;
					}
					num2 += (int)array[num4 + 2];
				}
				else
				{
					if (num3 == 0)
					{
						break;
					}
					num2++;
					num++;
					if (num == stringToFind.Length)
					{
						num3 = 0;
					}
					else
					{
						num3 = (int)stringToFind[num];
						if (num3 > 122)
						{
							return 0;
						}
						if (num3 >= 97)
						{
							num3 -= 32;
						}
					}
				}
			}
			return array[num4 + 3];
		}

		// Token: 0x04000154 RID: 340
		private byte[] nodeBuffer;
	}
}
