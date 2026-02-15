using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000519 RID: 1305
	internal enum CommandType
	{
		// Token: 0x040010EE RID: 4334
		Draw,
		// Token: 0x040010EF RID: 4335
		ImmediateCull,
		// Token: 0x040010F0 RID: 4336
		Immediate,
		// Token: 0x040010F1 RID: 4337
		PushView,
		// Token: 0x040010F2 RID: 4338
		PopView,
		// Token: 0x040010F3 RID: 4339
		PushScissor,
		// Token: 0x040010F4 RID: 4340
		PopScissor,
		// Token: 0x040010F5 RID: 4341
		PushRenderTexture,
		// Token: 0x040010F6 RID: 4342
		PopRenderTexture,
		// Token: 0x040010F7 RID: 4343
		BlitToPreviousRT,
		// Token: 0x040010F8 RID: 4344
		PushDefaultMaterial,
		// Token: 0x040010F9 RID: 4345
		PopDefaultMaterial,
		// Token: 0x040010FA RID: 4346
		BeginDisable,
		// Token: 0x040010FB RID: 4347
		EndDisable,
		// Token: 0x040010FC RID: 4348
		CutRenderChain
	}
}
