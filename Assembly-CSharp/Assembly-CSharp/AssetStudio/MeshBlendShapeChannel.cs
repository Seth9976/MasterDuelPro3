using System;

namespace AssetStudio
{
	// Token: 0x020000F5 RID: 245
	public class MeshBlendShapeChannel
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0000FD77 File Offset: 0x0000DF77
		public MeshBlendShapeChannel(ObjectReader reader)
		{
			this.name = reader.ReadAlignedString();
			this.nameHash = reader.ReadUInt32();
			this.frameIndex = reader.ReadInt32();
			this.frameCount = reader.ReadInt32();
		}

		// Token: 0x04000708 RID: 1800
		public string name;

		// Token: 0x04000709 RID: 1801
		public uint nameHash;

		// Token: 0x0400070A RID: 1802
		public int frameIndex;

		// Token: 0x0400070B RID: 1803
		public int frameCount;
	}
}
