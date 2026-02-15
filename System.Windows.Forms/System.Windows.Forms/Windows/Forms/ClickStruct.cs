using System;

namespace System.Windows.Forms
{
	// Token: 0x02000286 RID: 646
	internal struct ClickStruct
	{
		// Token: 0x04001178 RID: 4472
		internal IntPtr Hwnd;

		// Token: 0x04001179 RID: 4473
		internal Msg Message;

		// Token: 0x0400117A RID: 4474
		internal IntPtr wParam;

		// Token: 0x0400117B RID: 4475
		internal IntPtr lParam;

		// Token: 0x0400117C RID: 4476
		internal long Time;

		// Token: 0x0400117D RID: 4477
		internal bool Pending;
	}
}
