using System;

namespace System.IO.Compression
{
	// Token: 0x02000004 RID: 4
	internal sealed class CopyEncoder
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		public void GetBlock(DeflateInput input, OutputBuffer output, bool isFinal)
		{
			int num = 0;
			if (input != null)
			{
				num = Math.Min(input.Count, output.FreeBytes - 5 - output.BitsInBuffer);
				if (num > 65531)
				{
					num = 65531;
				}
			}
			if (isFinal)
			{
				output.WriteBits(3, 1U);
			}
			else
			{
				output.WriteBits(3, 0U);
			}
			output.FlushBits();
			this.WriteLenNLen((ushort)num, output);
			if (input != null && num > 0)
			{
				output.WriteBytes(input.Buffer, input.StartIndex, num);
				input.ConsumeBytes(num);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		private void WriteLenNLen(ushort len, OutputBuffer output)
		{
			output.WriteUInt16(len);
			ushort num = ~len;
			output.WriteUInt16(num);
		}
	}
}
