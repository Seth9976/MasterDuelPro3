using System;

namespace System.Windows.Forms
{
	// Token: 0x02000264 RID: 612
	[Flags]
	internal enum SetWindowValuemask
	{
		// Token: 0x0400100C RID: 4108
		Nothing = 0,
		// Token: 0x0400100D RID: 4109
		BackPixmap = 1,
		// Token: 0x0400100E RID: 4110
		BackPixel = 2,
		// Token: 0x0400100F RID: 4111
		BorderPixmap = 4,
		// Token: 0x04001010 RID: 4112
		BorderPixel = 8,
		// Token: 0x04001011 RID: 4113
		BitGravity = 16,
		// Token: 0x04001012 RID: 4114
		WinGravity = 32,
		// Token: 0x04001013 RID: 4115
		BackingStore = 64,
		// Token: 0x04001014 RID: 4116
		BackingPlanes = 128,
		// Token: 0x04001015 RID: 4117
		BackingPixel = 256,
		// Token: 0x04001016 RID: 4118
		OverrideRedirect = 512,
		// Token: 0x04001017 RID: 4119
		SaveUnder = 1024,
		// Token: 0x04001018 RID: 4120
		EventMask = 2048,
		// Token: 0x04001019 RID: 4121
		DontPropagate = 4096,
		// Token: 0x0400101A RID: 4122
		ColorMap = 8192,
		// Token: 0x0400101B RID: 4123
		Cursor = 16384
	}
}
