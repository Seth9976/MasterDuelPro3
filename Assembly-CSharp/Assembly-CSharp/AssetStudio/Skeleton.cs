using System;

namespace AssetStudio
{
	// Token: 0x020000DA RID: 218
	public class Skeleton
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		public Skeleton(ObjectReader reader)
		{
			int numNodes = reader.ReadInt32();
			this.m_Node = new Node[numNodes];
			for (int i = 0; i < numNodes; i++)
			{
				this.m_Node[i] = new Node(reader);
			}
			this.m_ID = reader.ReadUInt32Array();
			int numAxes = reader.ReadInt32();
			this.m_AxesArray = new Axes[numAxes];
			for (int j = 0; j < numAxes; j++)
			{
				this.m_AxesArray[j] = new Axes(reader);
			}
		}

		// Token: 0x04000693 RID: 1683
		public Node[] m_Node;

		// Token: 0x04000694 RID: 1684
		public uint[] m_ID;

		// Token: 0x04000695 RID: 1685
		public Axes[] m_AxesArray;
	}
}
