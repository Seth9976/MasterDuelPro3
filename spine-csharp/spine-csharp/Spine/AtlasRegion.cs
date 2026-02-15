using System;

namespace Spine
{
	// Token: 0x02000047 RID: 71
	public class AtlasRegion : TextureRegion
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000A10D File Offset: 0x0000830D
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000A115 File Offset: 0x00008315
		public int packedWidth
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000A11E File Offset: 0x0000831E
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000A126 File Offset: 0x00008326
		public int packedHeight
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000A12F File Offset: 0x0000832F
		public override int OriginalWidth
		{
			get
			{
				return this.originalWidth;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000A137 File Offset: 0x00008337
		public override int OriginalHeight
		{
			get
			{
				return this.originalHeight;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A13F File Offset: 0x0000833F
		public AtlasRegion Clone()
		{
			return base.MemberwiseClone() as AtlasRegion;
		}

		// Token: 0x0400011D RID: 285
		public AtlasPage page;

		// Token: 0x0400011E RID: 286
		public string name;

		// Token: 0x0400011F RID: 287
		public int x;

		// Token: 0x04000120 RID: 288
		public int y;

		// Token: 0x04000121 RID: 289
		public float offsetX;

		// Token: 0x04000122 RID: 290
		public float offsetY;

		// Token: 0x04000123 RID: 291
		public int originalWidth;

		// Token: 0x04000124 RID: 292
		public int originalHeight;

		// Token: 0x04000125 RID: 293
		public int degrees;

		// Token: 0x04000126 RID: 294
		public bool rotate;

		// Token: 0x04000127 RID: 295
		public int index;

		// Token: 0x04000128 RID: 296
		public string[] names;

		// Token: 0x04000129 RID: 297
		public int[][] values;
	}
}
