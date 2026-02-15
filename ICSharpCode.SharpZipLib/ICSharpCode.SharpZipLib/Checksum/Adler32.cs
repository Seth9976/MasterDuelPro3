using System;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x020000C2 RID: 194
	public sealed class Adler32 : IChecksum
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x0001B4AF File Offset: 0x000196AF
		public Adler32()
		{
			this.Reset();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001B4BD File Offset: 0x000196BD
		public void Reset()
		{
			this.checkValue = 1U;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001B4C6 File Offset: 0x000196C6
		public long Value
		{
			get
			{
				return (long)((ulong)this.checkValue);
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001B4D0 File Offset: 0x000196D0
		public void Update(int bval)
		{
			uint num = this.checkValue & 65535U;
			uint num2 = this.checkValue >> 16;
			num = (num + (uint)(bval & 255)) % Adler32.BASE;
			num2 = (num + num2) % Adler32.BASE;
			this.checkValue = (num2 << 16) + num;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001B51A File Offset: 0x0001971A
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(new ArraySegment<byte>(buffer, 0, buffer.Length));
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001B53C File Offset: 0x0001973C
		public void Update(ArraySegment<byte> segment)
		{
			uint num = this.checkValue & 65535U;
			uint num2 = this.checkValue >> 16;
			int i = segment.Count;
			int offset = segment.Offset;
			while (i > 0)
			{
				int num3 = 3800;
				if (num3 > i)
				{
					num3 = i;
				}
				i -= num3;
				while (--num3 >= 0)
				{
					num += (uint)(segment.Array[offset++] & byte.MaxValue);
					num2 += num;
				}
				num %= Adler32.BASE;
				num2 %= Adler32.BASE;
			}
			this.checkValue = (num2 << 16) | num;
		}

		// Token: 0x0400045D RID: 1117
		private static readonly uint BASE = 65521U;

		// Token: 0x0400045E RID: 1118
		private uint checkValue;
	}
}
