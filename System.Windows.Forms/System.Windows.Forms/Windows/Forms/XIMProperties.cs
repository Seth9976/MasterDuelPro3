using System;

namespace System.Windows.Forms
{
	// Token: 0x0200028A RID: 650
	[Flags]
	internal enum XIMProperties
	{
		// Token: 0x040011FB RID: 4603
		XIMPreeditArea = 1,
		// Token: 0x040011FC RID: 4604
		XIMPreeditCallbacks = 2,
		// Token: 0x040011FD RID: 4605
		XIMPreeditPosition = 4,
		// Token: 0x040011FE RID: 4606
		XIMPreeditNothing = 8,
		// Token: 0x040011FF RID: 4607
		XIMPreeditNone = 16,
		// Token: 0x04001200 RID: 4608
		XIMStatusArea = 256,
		// Token: 0x04001201 RID: 4609
		XIMStatusCallbacks = 512,
		// Token: 0x04001202 RID: 4610
		XIMStatusNothing = 1024,
		// Token: 0x04001203 RID: 4611
		XIMStatusNone = 2048
	}
}
