using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000278 RID: 632
	[DebuggerDisplay("Resource ({GetType().Name}:{GetName()})")]
	internal abstract class RenderGraphResource<DescType, ResType> : IRenderGraphResource where DescType : struct where ResType : class
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x0003E46D File Offset: 0x0003C66D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Reset(IRenderGraphResourcePool pool = null)
		{
			base.Reset(null);
			this.m_Pool = pool as RenderGraphResourcePool<ResType>;
			this.graphicsResource = default(ResType);
			this.validDesc = false;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0003E495 File Offset: 0x0003C695
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool IsCreated()
		{
			return this.graphicsResource != null;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003E4A5 File Offset: 0x0003C6A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void ReleaseGraphicsResource()
		{
			this.graphicsResource = default(ResType);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0003E4B4 File Offset: 0x0003C6B4
		public override void CreatePooledGraphicsResource()
		{
			int hashCode = this.GetDescHashCode();
			if (this.graphicsResource != null)
			{
				throw new InvalidOperationException("RenderGraphResource: Trying to create an already created resource (" + this.GetName() + "). Resource was probably declared for writing more than once in the same pass.");
			}
			if (!this.m_Pool.TryGetResource(hashCode, out this.graphicsResource))
			{
				this.CreateGraphicsResource();
			}
			else
			{
				this.UpdateGraphicsResource();
			}
			this.cachedHash = hashCode;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0003E51C File Offset: 0x0003C71C
		public override void ReleasePooledGraphicsResource(int frameIndex)
		{
			if (this.graphicsResource == null)
			{
				throw new InvalidOperationException("RenderGraphResource: Tried to release a resource (" + this.GetName() + ") that was never created. Check that there is at least one pass writing to it first.");
			}
			if (this.m_Pool != null)
			{
				this.m_Pool.ReleaseResource(this.cachedHash, this.graphicsResource, frameIndex);
			}
			this.Reset(null);
		}

		// Token: 0x04000AF2 RID: 2802
		public DescType desc;

		// Token: 0x04000AF3 RID: 2803
		public bool validDesc;

		// Token: 0x04000AF4 RID: 2804
		public ResType graphicsResource;

		// Token: 0x04000AF5 RID: 2805
		protected RenderGraphResourcePool<ResType> m_Pool;
	}
}
