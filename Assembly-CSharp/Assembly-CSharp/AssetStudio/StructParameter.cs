using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200010F RID: 271
	public class StructParameter
	{
		// Token: 0x0600036A RID: 874 RVA: 0x000122C0 File Offset: 0x000104C0
		public StructParameter(BinaryReader reader)
		{
			reader.ReadInt32();
			reader.ReadInt32();
			reader.ReadInt32();
			reader.ReadInt32();
			int numVectorParams = reader.ReadInt32();
			this.m_VectorParams = new VectorParameter[numVectorParams];
			for (int i = 0; i < numVectorParams; i++)
			{
				this.m_VectorParams[i] = new VectorParameter(reader);
			}
			int numMatrixParams = reader.ReadInt32();
			this.m_MatrixParams = new MatrixParameter[numMatrixParams];
			for (int j = 0; j < numMatrixParams; j++)
			{
				this.m_MatrixParams[j] = new MatrixParameter(reader);
			}
		}

		// Token: 0x04000779 RID: 1913
		public MatrixParameter[] m_MatrixParams;

		// Token: 0x0400077A RID: 1914
		public VectorParameter[] m_VectorParams;
	}
}
