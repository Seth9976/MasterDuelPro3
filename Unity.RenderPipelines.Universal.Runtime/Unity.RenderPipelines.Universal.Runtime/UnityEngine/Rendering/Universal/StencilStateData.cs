using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class StencilStateData
	{
		// Token: 0x0400003E RID: 62
		public bool overrideStencilState;

		// Token: 0x0400003F RID: 63
		public int stencilReference;

		// Token: 0x04000040 RID: 64
		public CompareFunction stencilCompareFunction = CompareFunction.Always;

		// Token: 0x04000041 RID: 65
		public StencilOp passOperation;

		// Token: 0x04000042 RID: 66
		public StencilOp failOperation;

		// Token: 0x04000043 RID: 67
		public StencilOp zFailOperation;
	}
}
