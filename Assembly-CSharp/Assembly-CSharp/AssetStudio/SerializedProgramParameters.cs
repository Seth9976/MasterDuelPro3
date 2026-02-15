using System;

namespace AssetStudio
{
	// Token: 0x02000125 RID: 293
	public class SerializedProgramParameters
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00012A24 File Offset: 0x00010C24
		public SerializedProgramParameters(ObjectReader reader)
		{
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
			int numTextureParams = reader.ReadInt32();
			this.m_TextureParams = new TextureParameter[numTextureParams];
			for (int k = 0; k < numTextureParams; k++)
			{
				this.m_TextureParams[k] = new TextureParameter(reader);
			}
			int numBufferParams = reader.ReadInt32();
			this.m_BufferParams = new BufferBinding[numBufferParams];
			for (int l = 0; l < numBufferParams; l++)
			{
				this.m_BufferParams[l] = new BufferBinding(reader);
			}
			int numConstantBuffers = reader.ReadInt32();
			this.m_ConstantBuffers = new ConstantBuffer[numConstantBuffers];
			for (int m = 0; m < numConstantBuffers; m++)
			{
				this.m_ConstantBuffers[m] = new ConstantBuffer(reader);
			}
			int numConstantBufferBindings = reader.ReadInt32();
			this.m_ConstantBufferBindings = new BufferBinding[numConstantBufferBindings];
			for (int n = 0; n < numConstantBufferBindings; n++)
			{
				this.m_ConstantBufferBindings[n] = new BufferBinding(reader);
			}
			int numUAVParams = reader.ReadInt32();
			this.m_UAVParams = new UAVParameter[numUAVParams];
			for (int i2 = 0; i2 < numUAVParams; i2++)
			{
				this.m_UAVParams[i2] = new UAVParameter(reader);
			}
			int numSamplers = reader.ReadInt32();
			this.m_Samplers = new SamplerParameter[numSamplers];
			for (int i3 = 0; i3 < numSamplers; i3++)
			{
				this.m_Samplers[i3] = new SamplerParameter(reader);
			}
		}

		// Token: 0x04000809 RID: 2057
		public VectorParameter[] m_VectorParams;

		// Token: 0x0400080A RID: 2058
		public MatrixParameter[] m_MatrixParams;

		// Token: 0x0400080B RID: 2059
		public TextureParameter[] m_TextureParams;

		// Token: 0x0400080C RID: 2060
		public BufferBinding[] m_BufferParams;

		// Token: 0x0400080D RID: 2061
		public ConstantBuffer[] m_ConstantBuffers;

		// Token: 0x0400080E RID: 2062
		public BufferBinding[] m_ConstantBufferBindings;

		// Token: 0x0400080F RID: 2063
		public UAVParameter[] m_UAVParams;

		// Token: 0x04000810 RID: 2064
		public SamplerParameter[] m_Samplers;
	}
}
