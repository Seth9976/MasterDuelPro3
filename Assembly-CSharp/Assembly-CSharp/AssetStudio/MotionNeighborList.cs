using System;

namespace AssetStudio
{
	// Token: 0x020000C2 RID: 194
	public class MotionNeighborList
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000E01F File Offset: 0x0000C21F
		public MotionNeighborList(ObjectReader reader)
		{
			this.m_NeighborArray = reader.ReadUInt32Array();
		}

		// Token: 0x04000602 RID: 1538
		public uint[] m_NeighborArray;
	}
}
