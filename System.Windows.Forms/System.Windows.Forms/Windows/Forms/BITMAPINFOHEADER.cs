using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A9 RID: 681
	internal struct BITMAPINFOHEADER
	{
		// Token: 0x0400144D RID: 5197
		internal uint biSize;

		// Token: 0x0400144E RID: 5198
		internal int biWidth;

		// Token: 0x0400144F RID: 5199
		internal int biHeight;

		// Token: 0x04001450 RID: 5200
		internal ushort biPlanes;

		// Token: 0x04001451 RID: 5201
		internal ushort biBitCount;

		// Token: 0x04001452 RID: 5202
		internal uint biCompression;

		// Token: 0x04001453 RID: 5203
		internal uint biSizeImage;

		// Token: 0x04001454 RID: 5204
		internal int biXPelsPerMeter;

		// Token: 0x04001455 RID: 5205
		internal int biYPelsPerMeter;

		// Token: 0x04001456 RID: 5206
		internal uint biClrUsed;

		// Token: 0x04001457 RID: 5207
		internal uint biClrImportant;
	}
}
