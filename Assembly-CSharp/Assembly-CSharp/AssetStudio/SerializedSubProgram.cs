using System;

namespace AssetStudio
{
	// Token: 0x02000126 RID: 294
	public class SerializedSubProgram
	{
		// Token: 0x0600037D RID: 893 RVA: 0x00012BD4 File Offset: 0x00010DD4
		public SerializedSubProgram(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_BlobIndex = reader.ReadUInt32();
			this.m_Channels = new ParserBindChannels(reader);
			if ((version[0] >= 2019 && version[0] < 2021) || (version[0] == 2021 && version[1] < 2))
			{
				reader.ReadUInt16Array();
				reader.AlignStream();
				reader.ReadUInt16Array();
				reader.AlignStream();
			}
			else
			{
				this.m_KeywordIndices = reader.ReadUInt16Array();
				if (version[0] >= 2017)
				{
					reader.AlignStream();
				}
			}
			this.m_ShaderHardwareTier = reader.ReadSByte();
			this.m_GpuProgramType = (ShaderGpuProgramType)reader.ReadSByte();
			reader.AlignStream();
			if ((version[0] == 2020 && version[1] > 3) || (version[0] == 2020 && version[1] == 3 && version[2] >= 2) || (version[0] > 2021 || (version[0] == 2021 && version[1] > 1)) || (version[0] == 2021 && version[1] == 1 && version[2] >= 1))
			{
				this.m_Parameters = new SerializedProgramParameters(reader);
			}
			else
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
				if (version[0] >= 2017)
				{
					int numSamplers = reader.ReadInt32();
					this.m_Samplers = new SamplerParameter[numSamplers];
					for (int i3 = 0; i3 < numSamplers; i3++)
					{
						this.m_Samplers[i3] = new SamplerParameter(reader);
					}
				}
			}
			if (version[0] > 2017 || (version[0] == 2017 && version[1] >= 2))
			{
				if (version[0] >= 2021)
				{
					reader.ReadInt64();
					return;
				}
				reader.ReadInt32();
			}
		}

		// Token: 0x04000811 RID: 2065
		public uint m_BlobIndex;

		// Token: 0x04000812 RID: 2066
		public ParserBindChannels m_Channels;

		// Token: 0x04000813 RID: 2067
		public ushort[] m_KeywordIndices;

		// Token: 0x04000814 RID: 2068
		public sbyte m_ShaderHardwareTier;

		// Token: 0x04000815 RID: 2069
		public ShaderGpuProgramType m_GpuProgramType;

		// Token: 0x04000816 RID: 2070
		public SerializedProgramParameters m_Parameters;

		// Token: 0x04000817 RID: 2071
		public VectorParameter[] m_VectorParams;

		// Token: 0x04000818 RID: 2072
		public MatrixParameter[] m_MatrixParams;

		// Token: 0x04000819 RID: 2073
		public TextureParameter[] m_TextureParams;

		// Token: 0x0400081A RID: 2074
		public BufferBinding[] m_BufferParams;

		// Token: 0x0400081B RID: 2075
		public ConstantBuffer[] m_ConstantBuffers;

		// Token: 0x0400081C RID: 2076
		public BufferBinding[] m_ConstantBufferBindings;

		// Token: 0x0400081D RID: 2077
		public UAVParameter[] m_UAVParams;

		// Token: 0x0400081E RID: 2078
		public SamplerParameter[] m_Samplers;
	}
}
