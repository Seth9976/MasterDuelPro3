using System;

namespace AssetStudio
{
	// Token: 0x020000D7 RID: 215
	public class Node
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		public Node(ObjectReader reader)
		{
			this.m_ParentId = reader.ReadInt32();
			this.m_AxesId = reader.ReadInt32();
		}

		// Token: 0x04000689 RID: 1673
		public int m_ParentId;

		// Token: 0x0400068A RID: 1674
		public int m_AxesId;
	}
}
