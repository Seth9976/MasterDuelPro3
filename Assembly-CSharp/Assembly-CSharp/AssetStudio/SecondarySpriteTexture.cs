using System;

namespace AssetStudio
{
	// Token: 0x02000133 RID: 307
	public class SecondarySpriteTexture
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0001369F File Offset: 0x0001189F
		public SecondarySpriteTexture(ObjectReader reader)
		{
			this.texture = new PPtr<Texture2D>(reader);
			this.name = reader.ReadStringToNull(32767);
		}

		// Token: 0x04000878 RID: 2168
		public PPtr<Texture2D> texture;

		// Token: 0x04000879 RID: 2169
		public string name;
	}
}
