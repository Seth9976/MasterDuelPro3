using System;

namespace Org.Brotli.Dec
{
	// Token: 0x02000080 RID: 128
	internal sealed class IntReader
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00009224 File Offset: 0x00007424
		internal static void Init(IntReader ir, byte[] byteBuffer, int[] intBuffer)
		{
			ir.byteBuffer = byteBuffer;
			ir.intBuffer = intBuffer;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009234 File Offset: 0x00007434
		internal static void Convert(IntReader ir, int intLen)
		{
			for (int i = 0; i < intLen; i++)
			{
				ir.intBuffer[i] = (int)(ir.byteBuffer[i * 4] & byte.MaxValue) | ((int)(ir.byteBuffer[i * 4 + 1] & byte.MaxValue) << 8) | ((int)(ir.byteBuffer[i * 4 + 2] & byte.MaxValue) << 16) | ((int)(ir.byteBuffer[i * 4 + 3] & byte.MaxValue) << 24);
			}
		}

		// Token: 0x040002EF RID: 751
		private byte[] byteBuffer;

		// Token: 0x040002F0 RID: 752
		private int[] intBuffer;
	}
}
