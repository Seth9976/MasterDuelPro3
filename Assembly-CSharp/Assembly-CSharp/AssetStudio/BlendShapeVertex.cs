using System;

namespace AssetStudio
{
	// Token: 0x020000F3 RID: 243
	public class BlendShapeVertex
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000FCAE File Offset: 0x0000DEAE
		public BlendShapeVertex(ObjectReader reader)
		{
			this.vertex = reader.ReadVector3();
			this.normal = reader.ReadVector3();
			this.tangent = reader.ReadVector3();
			this.index = reader.ReadUInt32();
		}

		// Token: 0x04000700 RID: 1792
		public Vector3 vertex;

		// Token: 0x04000701 RID: 1793
		public Vector3 normal;

		// Token: 0x04000702 RID: 1794
		public Vector3 tangent;

		// Token: 0x04000703 RID: 1795
		public uint index;
	}
}
