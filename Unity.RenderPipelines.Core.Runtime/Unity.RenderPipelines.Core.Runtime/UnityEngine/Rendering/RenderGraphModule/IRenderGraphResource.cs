using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000277 RID: 631
	internal class IRenderGraphResource
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x0003E3D0 File Offset: 0x0003C5D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual void Reset(IRenderGraphResourcePool _ = null)
		{
			this.imported = false;
			this.shared = false;
			this.sharedExplicitRelease = false;
			this.cachedHash = -1;
			this.transientPassIndex = -1;
			this.sharedResourceLastFrameUsed = -1;
			this.requestFallBack = false;
			this.forceRelease = false;
			this.writeCount = 0U;
			this.version = 0;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0003E423 File Offset: 0x0003C623
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual string GetName()
		{
			return "";
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000090C6 File Offset: 0x000072C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual bool IsCreated()
		{
			return false;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0003E42A File Offset: 0x0003C62A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual void IncrementWriteCount()
		{
			this.writeCount += 1U;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0003E43A File Offset: 0x0003C63A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual int NewVersion()
		{
			this.version++;
			return this.version;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0003E450 File Offset: 0x0003C650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual bool NeedsFallBack()
		{
			return this.requestFallBack && this.writeCount == 0U;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void CreatePooledGraphicsResource()
		{
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void CreateGraphicsResource()
		{
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void UpdateGraphicsResource()
		{
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void ReleasePooledGraphicsResource(int frameIndex)
		{
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void ReleaseGraphicsResource()
		{
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void LogCreation(RenderGraphLogger logger)
		{
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void LogRelease(RenderGraphLogger logger)
		{
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000090C6 File Offset: 0x000072C6
		public virtual int GetSortIndex()
		{
			return 0;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000090C6 File Offset: 0x000072C6
		public virtual int GetDescHashCode()
		{
			return 0;
		}

		// Token: 0x04000AE8 RID: 2792
		public bool imported;

		// Token: 0x04000AE9 RID: 2793
		public bool shared;

		// Token: 0x04000AEA RID: 2794
		public bool sharedExplicitRelease;

		// Token: 0x04000AEB RID: 2795
		public bool requestFallBack;

		// Token: 0x04000AEC RID: 2796
		public bool forceRelease;

		// Token: 0x04000AED RID: 2797
		public uint writeCount;

		// Token: 0x04000AEE RID: 2798
		public int cachedHash;

		// Token: 0x04000AEF RID: 2799
		public int transientPassIndex;

		// Token: 0x04000AF0 RID: 2800
		public int sharedResourceLastFrameUsed;

		// Token: 0x04000AF1 RID: 2801
		public int version;
	}
}
