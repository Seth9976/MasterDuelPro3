using System;

namespace AssetStudio
{
	// Token: 0x020000F4 RID: 244
	public class MeshBlendShape
	{
		// Token: 0x06000339 RID: 825 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public MeshBlendShape(ObjectReader reader)
		{
			int[] version = reader.version;
			if (version[0] == 4 && version[1] < 3)
			{
				reader.ReadAlignedString();
			}
			this.firstVertex = reader.ReadUInt32();
			this.vertexCount = reader.ReadUInt32();
			if (version[0] == 4 && version[1] < 3)
			{
				reader.ReadVector3();
				reader.ReadVector3();
			}
			this.hasNormals = reader.ReadBoolean();
			this.hasTangents = reader.ReadBoolean();
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 3))
			{
				reader.AlignStream();
			}
		}

		// Token: 0x04000704 RID: 1796
		public uint firstVertex;

		// Token: 0x04000705 RID: 1797
		public uint vertexCount;

		// Token: 0x04000706 RID: 1798
		public bool hasNormals;

		// Token: 0x04000707 RID: 1799
		public bool hasTangents;
	}
}
