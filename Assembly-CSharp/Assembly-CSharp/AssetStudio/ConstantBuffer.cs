using System;

namespace AssetStudio
{
	// Token: 0x02000122 RID: 290
	public class ConstantBuffer
	{
		// Token: 0x0600037A RID: 890 RVA: 0x000128B0 File Offset: 0x00010AB0
		public ConstantBuffer(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_NameIndex = reader.ReadInt32();
			int numMatrixParams = reader.ReadInt32();
			this.m_MatrixParams = new MatrixParameter[numMatrixParams];
			for (int i = 0; i < numMatrixParams; i++)
			{
				this.m_MatrixParams[i] = new MatrixParameter(reader);
			}
			int numVectorParams = reader.ReadInt32();
			this.m_VectorParams = new VectorParameter[numVectorParams];
			for (int j = 0; j < numVectorParams; j++)
			{
				this.m_VectorParams[j] = new VectorParameter(reader);
			}
			if (version[0] > 2017 || (version[0] == 2017 && version[1] >= 3))
			{
				int numStructParams = reader.ReadInt32();
				this.m_StructParams = new StructParameter[numStructParams];
				for (int k = 0; k < numStructParams; k++)
				{
					this.m_StructParams[k] = new StructParameter(reader);
				}
			}
			this.m_Size = reader.ReadInt32();
			if ((version[0] == 2020 && version[1] > 3) || (version[0] == 2020 && version[1] == 3 && version[2] >= 2) || (version[0] > 2021 || (version[0] == 2021 && version[1] > 1)) || (version[0] == 2021 && version[1] == 1 && version[2] >= 4))
			{
				this.m_IsPartialCB = reader.ReadBoolean();
				reader.AlignStream();
			}
		}

		// Token: 0x040007DE RID: 2014
		public int m_NameIndex;

		// Token: 0x040007DF RID: 2015
		public MatrixParameter[] m_MatrixParams;

		// Token: 0x040007E0 RID: 2016
		public VectorParameter[] m_VectorParams;

		// Token: 0x040007E1 RID: 2017
		public StructParameter[] m_StructParams;

		// Token: 0x040007E2 RID: 2018
		public int m_Size;

		// Token: 0x040007E3 RID: 2019
		public bool m_IsPartialCB;
	}
}
