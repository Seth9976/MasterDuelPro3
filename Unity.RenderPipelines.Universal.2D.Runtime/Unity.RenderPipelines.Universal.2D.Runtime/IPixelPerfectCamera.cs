using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004B RID: 75
	internal interface IPixelPerfectCamera
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001F6 RID: 502
		// (set) Token: 0x060001F7 RID: 503
		int assetsPPU { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001F8 RID: 504
		// (set) Token: 0x060001F9 RID: 505
		int refResolutionX { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001FA RID: 506
		// (set) Token: 0x060001FB RID: 507
		int refResolutionY { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001FC RID: 508
		// (set) Token: 0x060001FD RID: 509
		bool upscaleRT { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001FE RID: 510
		// (set) Token: 0x060001FF RID: 511
		bool pixelSnapping { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000200 RID: 512
		// (set) Token: 0x06000201 RID: 513
		bool cropFrameX { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000202 RID: 514
		// (set) Token: 0x06000203 RID: 515
		bool cropFrameY { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000204 RID: 516
		// (set) Token: 0x06000205 RID: 517
		bool stretchFill { get; set; }
	}
}
