using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x0200028D RID: 653
	[DebuggerDisplay("PassFragmentData: Res({resource.index}):{accessFlags}")]
	internal struct PassFragmentData
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x00042724 File Offset: 0x00040924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return ((this.resource.GetHashCode() * 23 + this.accessFlags.GetHashCode()) * 23 + this.mipLevel.GetHashCode()) * 23 + this.depthSlice.GetHashCode();
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00042778 File Offset: 0x00040978
		public static bool EqualForMerge(PassFragmentData x, PassFragmentData y)
		{
			return x.resource.index == y.resource.index && x.accessFlags == y.accessFlags && x.mipLevel == y.mipLevel && x.depthSlice == y.depthSlice;
		}

		// Token: 0x04000B76 RID: 2934
		public ResourceHandle resource;

		// Token: 0x04000B77 RID: 2935
		public AccessFlags accessFlags;

		// Token: 0x04000B78 RID: 2936
		public int mipLevel;

		// Token: 0x04000B79 RID: 2937
		public int depthSlice;
	}
}
