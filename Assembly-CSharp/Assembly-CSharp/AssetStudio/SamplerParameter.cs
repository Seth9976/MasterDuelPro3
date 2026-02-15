using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000110 RID: 272
	public class SamplerParameter
	{
		// Token: 0x0600036B RID: 875 RVA: 0x00012349 File Offset: 0x00010549
		public SamplerParameter(BinaryReader reader)
		{
			this.sampler = reader.ReadUInt32();
			this.bindPoint = reader.ReadInt32();
		}

		// Token: 0x0400077B RID: 1915
		public uint sampler;

		// Token: 0x0400077C RID: 1916
		public int bindPoint;
	}
}
