using System;

namespace Ionic.Zlib
{
	// Token: 0x02000069 RID: 105
	public sealed class Adler
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0001CE8C File Offset: 0x0001B08C
		public static uint Adler32(uint adler, byte[] buf, int index, int len)
		{
			if (buf == null)
			{
				return 1U;
			}
			uint num = adler & 65535U;
			uint num2 = (adler >> 16) & 65535U;
			while (len > 0)
			{
				int i = ((len < Adler.NMAX) ? len : Adler.NMAX);
				len -= i;
				while (i >= 16)
				{
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					i -= 16;
				}
				if (i != 0)
				{
					do
					{
						num += (uint)buf[index++];
						num2 += num;
					}
					while (--i != 0);
				}
				num %= Adler.BASE;
				num2 %= Adler.BASE;
			}
			return (num2 << 16) | num;
		}

		// Token: 0x0400039F RID: 927
		private static readonly uint BASE = 65521U;

		// Token: 0x040003A0 RID: 928
		private static readonly int NMAX = 5552;
	}
}
