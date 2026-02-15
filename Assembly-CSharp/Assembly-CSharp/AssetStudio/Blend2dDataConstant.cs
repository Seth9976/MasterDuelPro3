using System;

namespace AssetStudio
{
	// Token: 0x020000C3 RID: 195
	public class Blend2dDataConstant
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000E034 File Offset: 0x0000C234
		public Blend2dDataConstant(ObjectReader reader)
		{
			this.m_ChildPositionArray = reader.ReadVector2Array();
			this.m_ChildMagnitudeArray = reader.ReadSingleArray();
			this.m_ChildPairVectorArray = reader.ReadVector2Array();
			this.m_ChildPairAvgMagInvArray = reader.ReadSingleArray();
			int numNeighbours = reader.ReadInt32();
			this.m_ChildNeighborListArray = new MotionNeighborList[numNeighbours];
			for (int i = 0; i < numNeighbours; i++)
			{
				this.m_ChildNeighborListArray[i] = new MotionNeighborList(reader);
			}
		}

		// Token: 0x04000603 RID: 1539
		public Vector2[] m_ChildPositionArray;

		// Token: 0x04000604 RID: 1540
		public float[] m_ChildMagnitudeArray;

		// Token: 0x04000605 RID: 1541
		public Vector2[] m_ChildPairVectorArray;

		// Token: 0x04000606 RID: 1542
		public float[] m_ChildPairAvgMagInvArray;

		// Token: 0x04000607 RID: 1543
		public MotionNeighborList[] m_ChildNeighborListArray;
	}
}
