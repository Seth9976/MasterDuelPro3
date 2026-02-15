using System;

namespace AssetStudio
{
	// Token: 0x020000F8 RID: 248
	public class SubMesh
	{
		// Token: 0x0600033C RID: 828 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		public SubMesh(ObjectReader reader)
		{
			int[] version = reader.version;
			this.firstByte = reader.ReadUInt32();
			this.indexCount = reader.ReadUInt32();
			this.topology = (GfxPrimitiveType)reader.ReadInt32();
			if (version[0] < 4)
			{
				this.triangleCount = reader.ReadUInt32();
			}
			if (version[0] > 2017 || (version[0] == 2017 && version[1] >= 3))
			{
				this.baseVertex = reader.ReadUInt32();
			}
			if (version[0] >= 3)
			{
				this.firstVertex = reader.ReadUInt32();
				this.vertexCount = reader.ReadUInt32();
				this.localAABB = new AABB(reader);
			}
		}

		// Token: 0x04000717 RID: 1815
		public uint firstByte;

		// Token: 0x04000718 RID: 1816
		public uint indexCount;

		// Token: 0x04000719 RID: 1817
		public GfxPrimitiveType topology;

		// Token: 0x0400071A RID: 1818
		public uint triangleCount;

		// Token: 0x0400071B RID: 1819
		public uint baseVertex;

		// Token: 0x0400071C RID: 1820
		public uint firstVertex;

		// Token: 0x0400071D RID: 1821
		public uint vertexCount;

		// Token: 0x0400071E RID: 1822
		public AABB localAABB;
	}
}
