using System;

namespace AssetStudio
{
	// Token: 0x020000C1 RID: 193
	public class LeafInfoConstant
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000DFFF File Offset: 0x0000C1FF
		public LeafInfoConstant(ObjectReader reader)
		{
			this.m_IDArray = reader.ReadUInt32Array();
			this.m_IndexOffset = reader.ReadUInt32();
		}

		// Token: 0x04000600 RID: 1536
		public uint[] m_IDArray;

		// Token: 0x04000601 RID: 1537
		public uint m_IndexOffset;
	}
}
