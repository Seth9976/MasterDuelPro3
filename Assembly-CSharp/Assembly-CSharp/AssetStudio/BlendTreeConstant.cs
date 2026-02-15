using System;

namespace AssetStudio
{
	// Token: 0x020000C7 RID: 199
	public class BlendTreeConstant
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0000E21C File Offset: 0x0000C41C
		public BlendTreeConstant(ObjectReader reader)
		{
			int[] version = reader.version;
			int numNodes = reader.ReadInt32();
			this.m_NodeArray = new BlendTreeNodeConstant[numNodes];
			for (int i = 0; i < numNodes; i++)
			{
				this.m_NodeArray[i] = new BlendTreeNodeConstant(reader);
			}
			if (version[0] < 4 || (version[0] == 4 && version[1] < 5))
			{
				this.m_BlendEventArrayConstant = new ValueArrayConstant(reader);
			}
		}

		// Token: 0x04000618 RID: 1560
		public BlendTreeNodeConstant[] m_NodeArray;

		// Token: 0x04000619 RID: 1561
		public ValueArrayConstant m_BlendEventArrayConstant;
	}
}
