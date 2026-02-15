using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x0200028E RID: 654
	[DebuggerDisplay("PassRandomWriteData: Res({resource.index}):{index}:{preserveCounterValue}")]
	internal struct PassRandomWriteData
	{
		// Token: 0x060011A9 RID: 4521 RVA: 0x000427CB File Offset: 0x000409CB
		public override int GetHashCode()
		{
			return this.resource.GetHashCode() * 23 + this.index.GetHashCode();
		}

		// Token: 0x04000B7A RID: 2938
		public ResourceHandle resource;

		// Token: 0x04000B7B RID: 2939
		public int index;

		// Token: 0x04000B7C RID: 2940
		public bool preserveCounterValue;
	}
}
