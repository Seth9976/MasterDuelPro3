using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000522 RID: 1314
	internal enum EntryType : ushort
	{
		// Token: 0x0400114D RID: 4429
		DrawSolidMesh,
		// Token: 0x0400114E RID: 4430
		DrawTexturedMesh,
		// Token: 0x0400114F RID: 4431
		DrawTexturedMeshSkipAtlas,
		// Token: 0x04001150 RID: 4432
		DrawTextMesh,
		// Token: 0x04001151 RID: 4433
		DrawGradients,
		// Token: 0x04001152 RID: 4434
		DrawImmediate,
		// Token: 0x04001153 RID: 4435
		DrawImmediateCull,
		// Token: 0x04001154 RID: 4436
		DrawChildren,
		// Token: 0x04001155 RID: 4437
		BeginStencilMask,
		// Token: 0x04001156 RID: 4438
		EndStencilMask,
		// Token: 0x04001157 RID: 4439
		PopStencilMask,
		// Token: 0x04001158 RID: 4440
		PushClippingRect,
		// Token: 0x04001159 RID: 4441
		PopClippingRect,
		// Token: 0x0400115A RID: 4442
		PushScissors,
		// Token: 0x0400115B RID: 4443
		PopScissors,
		// Token: 0x0400115C RID: 4444
		PushGroupMatrix,
		// Token: 0x0400115D RID: 4445
		PopGroupMatrix,
		// Token: 0x0400115E RID: 4446
		PushRenderTexture,
		// Token: 0x0400115F RID: 4447
		BlitAndPopRenderTexture,
		// Token: 0x04001160 RID: 4448
		PushDefaultMaterial,
		// Token: 0x04001161 RID: 4449
		PopDefaultMaterial,
		// Token: 0x04001162 RID: 4450
		CutRenderChain,
		// Token: 0x04001163 RID: 4451
		DedicatedPlaceholder
	}
}
