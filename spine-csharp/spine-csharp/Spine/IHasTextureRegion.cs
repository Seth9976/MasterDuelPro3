using System;

namespace Spine
{
	// Token: 0x0200004F RID: 79
	public interface IHasTextureRegion
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001D3 RID: 467
		// (set) Token: 0x060001D4 RID: 468
		string Path { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D5 RID: 469
		// (set) Token: 0x060001D6 RID: 470
		TextureRegion Region { get; set; }

		// Token: 0x060001D7 RID: 471
		void UpdateRegion();

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001D8 RID: 472
		// (set) Token: 0x060001D9 RID: 473
		float R { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001DA RID: 474
		// (set) Token: 0x060001DB RID: 475
		float G { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001DC RID: 476
		// (set) Token: 0x060001DD RID: 477
		float B { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001DE RID: 478
		// (set) Token: 0x060001DF RID: 479
		float A { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001E0 RID: 480
		// (set) Token: 0x060001E1 RID: 481
		Sequence Sequence { get; set; }
	}
}
