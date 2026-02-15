using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x0200029A RID: 666
	internal struct ResourceUnversionedData
	{
		// Token: 0x060011CC RID: 4556 RVA: 0x00043D89 File Offset: 0x00041F89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetName(CompilerContextData ctx, ResourceHandle h)
		{
			return ctx.GetResourceName(h);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00043D94 File Offset: 0x00041F94
		public ResourceUnversionedData(IRenderGraphResource rll, ref RenderTargetInfo info, ref TextureDesc desc, bool isResourceShared)
		{
			this.isImported = rll.imported;
			this.isShared = isResourceShared;
			this.tag = 0;
			this.firstUsePassID = -1;
			this.lastUsePassID = -1;
			this.lastWritePassID = -1;
			this.memoryLess = false;
			this.width = info.width;
			this.height = info.height;
			this.volumeDepth = info.volumeDepth;
			this.msaaSamples = info.msaaSamples;
			this.latestVersionNumber = rll.version;
			this.clear = desc.clearBuffer;
			this.discard = desc.discardBuffer;
			this.bindMS = info.bindMS;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00043E38 File Offset: 0x00042038
		public ResourceUnversionedData(IRenderGraphResource rll, ref BufferDesc _, bool isResourceShared)
		{
			this.isImported = rll.imported;
			this.isShared = isResourceShared;
			this.tag = 0;
			this.firstUsePassID = -1;
			this.lastUsePassID = -1;
			this.lastWritePassID = -1;
			this.memoryLess = false;
			this.width = -1;
			this.height = -1;
			this.volumeDepth = -1;
			this.msaaSamples = -1;
			this.latestVersionNumber = rll.version;
			this.clear = false;
			this.discard = false;
			this.bindMS = false;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00043EB8 File Offset: 0x000420B8
		public ResourceUnversionedData(IRenderGraphResource rll, ref RayTracingAccelerationStructureDesc _, bool isResourceShared)
		{
			this.isImported = rll.imported;
			this.isShared = isResourceShared;
			this.tag = 0;
			this.firstUsePassID = -1;
			this.lastUsePassID = -1;
			this.lastWritePassID = -1;
			this.memoryLess = false;
			this.width = -1;
			this.height = -1;
			this.volumeDepth = -1;
			this.msaaSamples = -1;
			this.latestVersionNumber = rll.version;
			this.clear = false;
			this.discard = false;
			this.bindMS = false;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00043F38 File Offset: 0x00042138
		public void InitializeNullResource()
		{
			this.firstUsePassID = -1;
			this.lastUsePassID = -1;
			this.lastWritePassID = -1;
		}

		// Token: 0x04000BE3 RID: 3043
		public readonly bool isImported;

		// Token: 0x04000BE4 RID: 3044
		public bool isShared;

		// Token: 0x04000BE5 RID: 3045
		public int tag;

		// Token: 0x04000BE6 RID: 3046
		public int lastUsePassID;

		// Token: 0x04000BE7 RID: 3047
		public int lastWritePassID;

		// Token: 0x04000BE8 RID: 3048
		public int firstUsePassID;

		// Token: 0x04000BE9 RID: 3049
		public bool memoryLess;

		// Token: 0x04000BEA RID: 3050
		public readonly int width;

		// Token: 0x04000BEB RID: 3051
		public readonly int height;

		// Token: 0x04000BEC RID: 3052
		public readonly int volumeDepth;

		// Token: 0x04000BED RID: 3053
		public readonly int msaaSamples;

		// Token: 0x04000BEE RID: 3054
		public readonly int latestVersionNumber;

		// Token: 0x04000BEF RID: 3055
		public readonly bool clear;

		// Token: 0x04000BF0 RID: 3056
		public readonly bool discard;

		// Token: 0x04000BF1 RID: 3057
		public readonly bool bindMS;
	}
}
