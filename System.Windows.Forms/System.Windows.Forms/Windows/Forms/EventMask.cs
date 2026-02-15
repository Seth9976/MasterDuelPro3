using System;

namespace System.Windows.Forms
{
	// Token: 0x02000267 RID: 615
	[Flags]
	internal enum EventMask
	{
		// Token: 0x04001046 RID: 4166
		NoEventMask = 0,
		// Token: 0x04001047 RID: 4167
		KeyPressMask = 1,
		// Token: 0x04001048 RID: 4168
		KeyReleaseMask = 2,
		// Token: 0x04001049 RID: 4169
		ButtonPressMask = 4,
		// Token: 0x0400104A RID: 4170
		ButtonReleaseMask = 8,
		// Token: 0x0400104B RID: 4171
		EnterWindowMask = 16,
		// Token: 0x0400104C RID: 4172
		LeaveWindowMask = 32,
		// Token: 0x0400104D RID: 4173
		PointerMotionMask = 64,
		// Token: 0x0400104E RID: 4174
		PointerMotionHintMask = 128,
		// Token: 0x0400104F RID: 4175
		Button1MotionMask = 256,
		// Token: 0x04001050 RID: 4176
		Button2MotionMask = 512,
		// Token: 0x04001051 RID: 4177
		Button3MotionMask = 1024,
		// Token: 0x04001052 RID: 4178
		Button4MotionMask = 2048,
		// Token: 0x04001053 RID: 4179
		Button5MotionMask = 4096,
		// Token: 0x04001054 RID: 4180
		ButtonMotionMask = 8192,
		// Token: 0x04001055 RID: 4181
		KeymapStateMask = 16384,
		// Token: 0x04001056 RID: 4182
		ExposureMask = 32768,
		// Token: 0x04001057 RID: 4183
		VisibilityChangeMask = 65536,
		// Token: 0x04001058 RID: 4184
		StructureNotifyMask = 131072,
		// Token: 0x04001059 RID: 4185
		ResizeRedirectMask = 262144,
		// Token: 0x0400105A RID: 4186
		SubstructureNotifyMask = 524288,
		// Token: 0x0400105B RID: 4187
		SubstructureRedirectMask = 1048576,
		// Token: 0x0400105C RID: 4188
		FocusChangeMask = 2097152,
		// Token: 0x0400105D RID: 4189
		PropertyChangeMask = 4194304,
		// Token: 0x0400105E RID: 4190
		ColormapChangeMask = 8388608,
		// Token: 0x0400105F RID: 4191
		OwnerGrabButtonMask = 16777216
	}
}
