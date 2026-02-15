using System;

namespace Unity.Collections
{
	// Token: 0x02000039 RID: 57
	internal sealed class BitField32DebugView
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00004C20 File Offset: 0x00002E20
		public BitField32DebugView(BitField32 bitfield)
		{
			this.BitField = bitfield;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004C30 File Offset: 0x00002E30
		public bool[] Bits
		{
			get
			{
				bool[] array = new bool[32];
				for (int i = 0; i < 32; i++)
				{
					array[i] = this.BitField.IsSet(i);
				}
				return array;
			}
		}

		// Token: 0x04000083 RID: 131
		private BitField32 BitField;
	}
}
