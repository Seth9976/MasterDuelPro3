using System;

namespace TMPro
{
	// Token: 0x0200003A RID: 58
	public struct KerningPairKey
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00008ED2 File Offset: 0x000070D2
		public KerningPairKey(uint ascii_left, uint ascii_right)
		{
			this.ascii_Left = ascii_left;
			this.ascii_Right = ascii_right;
			this.key = (ascii_right << 16) + ascii_left;
		}

		// Token: 0x0400014D RID: 333
		public uint ascii_Left;

		// Token: 0x0400014E RID: 334
		public uint ascii_Right;

		// Token: 0x0400014F RID: 335
		public uint key;
	}
}
