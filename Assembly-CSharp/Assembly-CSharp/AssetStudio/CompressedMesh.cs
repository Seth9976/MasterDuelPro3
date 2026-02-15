using System;

namespace AssetStudio
{
	// Token: 0x020000ED RID: 237
	public class CompressedMesh
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		public CompressedMesh(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_Vertices = new PackedFloatVector(reader);
			this.m_UV = new PackedFloatVector(reader);
			if (version[0] < 5)
			{
				this.m_BindPoses = new PackedFloatVector(reader);
			}
			this.m_Normals = new PackedFloatVector(reader);
			this.m_Tangents = new PackedFloatVector(reader);
			this.m_Weights = new PackedIntVector(reader);
			this.m_NormalSigns = new PackedIntVector(reader);
			this.m_TangentSigns = new PackedIntVector(reader);
			if (version[0] >= 5)
			{
				this.m_FloatColors = new PackedFloatVector(reader);
			}
			this.m_BoneIndices = new PackedIntVector(reader);
			this.m_Triangles = new PackedIntVector(reader);
			if (version[0] > 3 || (version[0] == 3 && version[1] >= 5))
			{
				if (version[0] < 5)
				{
					this.m_Colors = new PackedIntVector(reader);
					return;
				}
				this.m_UVInfo = reader.ReadUInt32();
			}
		}

		// Token: 0x040006E0 RID: 1760
		public PackedFloatVector m_Vertices;

		// Token: 0x040006E1 RID: 1761
		public PackedFloatVector m_UV;

		// Token: 0x040006E2 RID: 1762
		public PackedFloatVector m_BindPoses;

		// Token: 0x040006E3 RID: 1763
		public PackedFloatVector m_Normals;

		// Token: 0x040006E4 RID: 1764
		public PackedFloatVector m_Tangents;

		// Token: 0x040006E5 RID: 1765
		public PackedIntVector m_Weights;

		// Token: 0x040006E6 RID: 1766
		public PackedIntVector m_NormalSigns;

		// Token: 0x040006E7 RID: 1767
		public PackedIntVector m_TangentSigns;

		// Token: 0x040006E8 RID: 1768
		public PackedFloatVector m_FloatColors;

		// Token: 0x040006E9 RID: 1769
		public PackedIntVector m_BoneIndices;

		// Token: 0x040006EA RID: 1770
		public PackedIntVector m_Triangles;

		// Token: 0x040006EB RID: 1771
		public PackedIntVector m_Colors;

		// Token: 0x040006EC RID: 1772
		public uint m_UVInfo;
	}
}
