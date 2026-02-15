using System;

namespace Spine
{
	// Token: 0x02000046 RID: 70
	public class AtlasPage
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000A0E3 File Offset: 0x000082E3
		public AtlasPage Clone()
		{
			return base.MemberwiseClone() as AtlasPage;
		}

		// Token: 0x04000113 RID: 275
		public string name;

		// Token: 0x04000114 RID: 276
		public int width;

		// Token: 0x04000115 RID: 277
		public int height;

		// Token: 0x04000116 RID: 278
		public Format format = Format.RGBA8888;

		// Token: 0x04000117 RID: 279
		public TextureFilter minFilter;

		// Token: 0x04000118 RID: 280
		public TextureFilter magFilter;

		// Token: 0x04000119 RID: 281
		public TextureWrap uWrap = TextureWrap.ClampToEdge;

		// Token: 0x0400011A RID: 282
		public TextureWrap vWrap = TextureWrap.ClampToEdge;

		// Token: 0x0400011B RID: 283
		public bool pma;

		// Token: 0x0400011C RID: 284
		public object rendererObject;
	}
}
