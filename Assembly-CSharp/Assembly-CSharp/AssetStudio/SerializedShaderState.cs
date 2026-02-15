using System;

namespace AssetStudio
{
	// Token: 0x0200011B RID: 283
	public class SerializedShaderState
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00012544 File Offset: 0x00010744
		public SerializedShaderState(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_Name = reader.ReadAlignedString();
			this.rtBlend = new SerializedShaderRTBlendState[8];
			for (int i = 0; i < 8; i++)
			{
				this.rtBlend[i] = new SerializedShaderRTBlendState(reader);
			}
			this.rtSeparateBlend = reader.ReadBoolean();
			reader.AlignStream();
			if (version[0] > 2017 || (version[0] == 2017 && version[1] >= 2))
			{
				this.zClip = new SerializedShaderFloatValue(reader);
			}
			this.zTest = new SerializedShaderFloatValue(reader);
			this.zWrite = new SerializedShaderFloatValue(reader);
			this.culling = new SerializedShaderFloatValue(reader);
			if (version[0] >= 2020)
			{
				this.conservative = new SerializedShaderFloatValue(reader);
			}
			this.offsetFactor = new SerializedShaderFloatValue(reader);
			this.offsetUnits = new SerializedShaderFloatValue(reader);
			this.alphaToMask = new SerializedShaderFloatValue(reader);
			this.stencilOp = new SerializedStencilOp(reader);
			this.stencilOpFront = new SerializedStencilOp(reader);
			this.stencilOpBack = new SerializedStencilOp(reader);
			this.stencilReadMask = new SerializedShaderFloatValue(reader);
			this.stencilWriteMask = new SerializedShaderFloatValue(reader);
			this.stencilRef = new SerializedShaderFloatValue(reader);
			this.fogStart = new SerializedShaderFloatValue(reader);
			this.fogEnd = new SerializedShaderFloatValue(reader);
			this.fogDensity = new SerializedShaderFloatValue(reader);
			this.fogColor = new SerializedShaderVectorValue(reader);
			this.fogMode = (FogMode)reader.ReadInt32();
			this.gpuProgramID = reader.ReadInt32();
			this.m_Tags = new SerializedTagMap(reader);
			this.m_LOD = reader.ReadInt32();
			this.lighting = reader.ReadBoolean();
			reader.AlignStream();
		}

		// Token: 0x040007AF RID: 1967
		public string m_Name;

		// Token: 0x040007B0 RID: 1968
		public SerializedShaderRTBlendState[] rtBlend;

		// Token: 0x040007B1 RID: 1969
		public bool rtSeparateBlend;

		// Token: 0x040007B2 RID: 1970
		public SerializedShaderFloatValue zClip;

		// Token: 0x040007B3 RID: 1971
		public SerializedShaderFloatValue zTest;

		// Token: 0x040007B4 RID: 1972
		public SerializedShaderFloatValue zWrite;

		// Token: 0x040007B5 RID: 1973
		public SerializedShaderFloatValue culling;

		// Token: 0x040007B6 RID: 1974
		public SerializedShaderFloatValue conservative;

		// Token: 0x040007B7 RID: 1975
		public SerializedShaderFloatValue offsetFactor;

		// Token: 0x040007B8 RID: 1976
		public SerializedShaderFloatValue offsetUnits;

		// Token: 0x040007B9 RID: 1977
		public SerializedShaderFloatValue alphaToMask;

		// Token: 0x040007BA RID: 1978
		public SerializedStencilOp stencilOp;

		// Token: 0x040007BB RID: 1979
		public SerializedStencilOp stencilOpFront;

		// Token: 0x040007BC RID: 1980
		public SerializedStencilOp stencilOpBack;

		// Token: 0x040007BD RID: 1981
		public SerializedShaderFloatValue stencilReadMask;

		// Token: 0x040007BE RID: 1982
		public SerializedShaderFloatValue stencilWriteMask;

		// Token: 0x040007BF RID: 1983
		public SerializedShaderFloatValue stencilRef;

		// Token: 0x040007C0 RID: 1984
		public SerializedShaderFloatValue fogStart;

		// Token: 0x040007C1 RID: 1985
		public SerializedShaderFloatValue fogEnd;

		// Token: 0x040007C2 RID: 1986
		public SerializedShaderFloatValue fogDensity;

		// Token: 0x040007C3 RID: 1987
		public SerializedShaderVectorValue fogColor;

		// Token: 0x040007C4 RID: 1988
		public FogMode fogMode;

		// Token: 0x040007C5 RID: 1989
		public int gpuProgramID;

		// Token: 0x040007C6 RID: 1990
		public SerializedTagMap m_Tags;

		// Token: 0x040007C7 RID: 1991
		public int m_LOD;

		// Token: 0x040007C8 RID: 1992
		public bool lighting;
	}
}
