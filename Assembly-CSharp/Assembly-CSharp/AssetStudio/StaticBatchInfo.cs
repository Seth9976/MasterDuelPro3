using System;

namespace AssetStudio
{
	// Token: 0x0200010A RID: 266
	public class StaticBatchInfo
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00011F1F File Offset: 0x0001011F
		public StaticBatchInfo(ObjectReader reader)
		{
			this.firstSubMesh = reader.ReadUInt16();
			this.subMeshCount = reader.ReadUInt16();
		}

		// Token: 0x04000772 RID: 1906
		public ushort firstSubMesh;

		// Token: 0x04000773 RID: 1907
		public ushort subMeshCount;
	}
}
