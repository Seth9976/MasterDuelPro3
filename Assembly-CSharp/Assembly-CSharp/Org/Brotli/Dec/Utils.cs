using System;

namespace Org.Brotli.Dec
{
	// Token: 0x02000085 RID: 133
	internal sealed class Utils
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000A050 File Offset: 0x00008250
		internal static void FillWithZeroes(byte[] dest, int offset, int length)
		{
			int step;
			for (int cursor = 0; cursor < length; cursor += step)
			{
				step = Math.Min(cursor + 1024, length) - cursor;
				Array.Copy(Utils.ByteZeroes, 0, dest, offset + cursor, step);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A08C File Offset: 0x0000828C
		internal static void FillWithZeroes(int[] dest, int offset, int length)
		{
			int step;
			for (int cursor = 0; cursor < length; cursor += step)
			{
				step = Math.Min(cursor + 1024, length) - cursor;
				Array.Copy(Utils.IntZeroes, 0, dest, offset + cursor, step);
			}
		}

		// Token: 0x0400033E RID: 830
		private static readonly byte[] ByteZeroes = new byte[1024];

		// Token: 0x0400033F RID: 831
		private static readonly int[] IntZeroes = new int[1024];
	}
}
