using System;

namespace Unity.Collections
{
	// Token: 0x0200003B RID: 59
	internal sealed class BitField64DebugView
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00004D89 File Offset: 0x00002F89
		public BitField64DebugView(BitField64 data)
		{
			this.Data = data;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004D98 File Offset: 0x00002F98
		public bool[] Bits
		{
			get
			{
				bool[] array = new bool[64];
				for (int i = 0; i < 64; i++)
				{
					array[i] = this.Data.IsSet(i);
				}
				return array;
			}
		}

		// Token: 0x04000085 RID: 133
		private BitField64 Data;
	}
}
