using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003B0 RID: 944
	internal struct Caret
	{
		// Token: 0x04001D53 RID: 7507
		internal Timer Timer;

		// Token: 0x04001D54 RID: 7508
		internal IntPtr Hwnd;

		// Token: 0x04001D55 RID: 7509
		internal int X;

		// Token: 0x04001D56 RID: 7510
		internal int Y;

		// Token: 0x04001D57 RID: 7511
		internal int Width;

		// Token: 0x04001D58 RID: 7512
		internal int Height;

		// Token: 0x04001D59 RID: 7513
		internal int Visible;

		// Token: 0x04001D5A RID: 7514
		internal bool On;

		// Token: 0x04001D5B RID: 7515
		internal bool Paused;
	}
}
