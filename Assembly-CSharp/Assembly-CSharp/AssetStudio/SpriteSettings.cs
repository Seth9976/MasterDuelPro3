using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000137 RID: 311
	public class SpriteSettings
	{
		// Token: 0x0600038E RID: 910 RVA: 0x000136C4 File Offset: 0x000118C4
		public SpriteSettings(BinaryReader reader)
		{
			this.settingsRaw = reader.ReadUInt32();
			this.packed = this.settingsRaw & 1U;
			this.packingMode = (SpritePackingMode)((this.settingsRaw >> 1) & 1U);
			this.packingRotation = (SpritePackingRotation)((this.settingsRaw >> 2) & 15U);
			this.meshType = (SpriteMeshType)((this.settingsRaw >> 6) & 1U);
		}

		// Token: 0x04000886 RID: 2182
		public uint settingsRaw;

		// Token: 0x04000887 RID: 2183
		public uint packed;

		// Token: 0x04000888 RID: 2184
		public SpritePackingMode packingMode;

		// Token: 0x04000889 RID: 2185
		public SpritePackingRotation packingRotation;

		// Token: 0x0400088A RID: 2186
		public SpriteMeshType meshType;
	}
}
