using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009E RID: 158
	internal struct IndirectBufferContextStorage : IDisposable
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00010E45 File Offset: 0x0000F045
		public GraphicsBuffer instanceBuffer
		{
			get
			{
				return this.m_InstanceBuffer;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00010E4D File Offset: 0x0000F04D
		public GraphicsBuffer instanceInfoBuffer
		{
			get
			{
				return this.m_InstanceInfoBuffer;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00010E55 File Offset: 0x0000F055
		public GraphicsBuffer argsBuffer
		{
			get
			{
				return this.m_ArgsBuffer;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00010E5D File Offset: 0x0000F05D
		public GraphicsBuffer drawInfoBuffer
		{
			get
			{
				return this.m_DrawInfoBuffer;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00010E65 File Offset: 0x0000F065
		public GraphicsBufferHandle visibleInstanceBufferHandle
		{
			get
			{
				return this.m_InstanceBuffer.bufferHandle;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00010E72 File Offset: 0x0000F072
		public GraphicsBufferHandle indirectArgsBufferHandle
		{
			get
			{
				return this.m_ArgsBuffer.bufferHandle;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00010E80 File Offset: 0x0000F080
		public IndirectBufferContextHandles ImportBuffers(RenderGraph renderGraph)
		{
			return new IndirectBufferContextHandles
			{
				instanceBuffer = renderGraph.ImportBuffer(this.m_InstanceBuffer, false),
				instanceInfoBuffer = renderGraph.ImportBuffer(this.m_InstanceInfoBuffer, false),
				argsBuffer = renderGraph.ImportBuffer(this.m_ArgsBuffer, false),
				drawInfoBuffer = renderGraph.ImportBuffer(this.m_DrawInfoBuffer, false)
			};
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00010EE6 File Offset: 0x0000F0E6
		public NativeArray<IndirectInstanceInfo> instanceInfoGlobalArray
		{
			get
			{
				return this.m_InstanceInfoStaging;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00010EEE File Offset: 0x0000F0EE
		public NativeArray<IndirectDrawInfo> drawInfoGlobalArray
		{
			get
			{
				return this.m_DrawInfoStaging;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00010EF6 File Offset: 0x0000F0F6
		public NativeArray<int> allocationCounters
		{
			get
			{
				return this.m_AllocationCounters;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00010F00 File Offset: 0x0000F100
		public void Init()
		{
			int initialDrawCount = 256;
			int initialInstanceCount = 64 * initialDrawCount;
			int initialContextCount = 8;
			this.AllocateInstanceBuffers(initialInstanceCount);
			this.AllocateDrawBuffers(initialDrawCount);
			this.m_ContextIndexFromViewID = new NativeHashMap<int, int>(initialContextCount, Allocator.Persistent);
			this.m_Contexts = new NativeList<IndirectBufferContext>(initialContextCount, Allocator.Persistent);
			this.m_ContextAllocInfo = new NativeArray<IndirectBufferAllocInfo>(initialContextCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			this.m_AllocationCounters = new NativeArray<int>(2, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			this.ResetAllocators();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00010F6E File Offset: 0x0000F16E
		private void AllocateInstanceBuffers(int maxInstanceCount)
		{
			this.m_InstanceBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Raw, maxInstanceCount, 4);
			this.m_InstanceInfoBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, 2 * maxInstanceCount, Marshal.SizeOf<IndirectInstanceInfo>());
			this.m_InstanceInfoStaging = new NativeArray<IndirectInstanceInfo>(maxInstanceCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			this.m_BufferLimits.maxInstanceCount = maxInstanceCount;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00010FAE File Offset: 0x0000F1AE
		private void FreeInstanceBuffers()
		{
			this.m_InstanceBuffer.Release();
			this.m_InstanceInfoBuffer.Release();
			this.m_InstanceInfoStaging.Dispose();
			this.m_BufferLimits.maxInstanceCount = 0;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00010FE0 File Offset: 0x0000F1E0
		private void AllocateDrawBuffers(int maxDrawCount)
		{
			this.m_ArgsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured | GraphicsBuffer.Target.IndirectArguments, (maxDrawCount + 1) * 5, 4);
			this.m_DrawInfoBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, maxDrawCount, Marshal.SizeOf<IndirectDrawInfo>());
			this.m_DrawInfoStaging = new NativeArray<IndirectDrawInfo>(maxDrawCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			this.m_BufferLimits.maxDrawCount = maxDrawCount;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00011030 File Offset: 0x0000F230
		private void FreeDrawBuffers()
		{
			this.m_ArgsBuffer.Release();
			this.m_DrawInfoBuffer.Release();
			this.m_DrawInfoStaging.Dispose();
			this.m_BufferLimits.maxDrawCount = 0;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0001105F File Offset: 0x0000F25F
		public void Dispose()
		{
			this.SyncContexts();
			this.FreeInstanceBuffers();
			this.FreeDrawBuffers();
			this.m_ContextIndexFromViewID.Dispose();
			this.m_Contexts.Dispose();
			this.m_ContextAllocInfo.Dispose();
			this.m_AllocationCounters.Dispose();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000110A0 File Offset: 0x0000F2A0
		private void SyncContexts()
		{
			for (int contextIndex = 0; contextIndex < this.m_Contexts.Length; contextIndex++)
			{
				this.m_Contexts[contextIndex].cullingJobHandle.Complete();
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000110DC File Offset: 0x0000F2DC
		private void ResetAllocators()
		{
			this.m_ContextAllocCounter = 0;
			this.m_ContextIndexFromViewID.Clear();
			this.m_Contexts.Clear();
			int num = 0;
			(ref this.m_AllocationCounters).FillArray(in num, 0, -1);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00011118 File Offset: 0x0000F318
		private void GrowBuffers()
		{
			if (this.m_ContextAllocCounter > this.m_ContextAllocInfo.Length)
			{
				int newContextCount = this.m_ContextAllocCounter * 6 / 5;
				this.m_Contexts.Clear();
				this.m_Contexts.SetCapacity(newContextCount);
				this.m_ContextAllocInfo.Dispose();
				this.m_ContextAllocInfo = new NativeArray<IndirectBufferAllocInfo>(newContextCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			int instanceAllocCounter = this.m_AllocationCounters[0];
			if (instanceAllocCounter > this.m_BufferLimits.maxInstanceCount)
			{
				int newInstanceCount = instanceAllocCounter * 6 / 5;
				this.FreeInstanceBuffers();
				this.AllocateInstanceBuffers(newInstanceCount);
			}
			int drawAllocCounter = this.m_AllocationCounters[1];
			if (drawAllocCounter > this.m_BufferLimits.maxDrawCount)
			{
				int newDrawCount = drawAllocCounter * 6 / 5;
				this.FreeDrawBuffers();
				this.AllocateDrawBuffers(newDrawCount);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000111D1 File Offset: 0x0000F3D1
		public void ClearContextsAndGrowBuffers()
		{
			this.SyncContexts();
			this.GrowBuffers();
			this.ResetAllocators();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000111E8 File Offset: 0x0000F3E8
		public int TryAllocateContext(int viewID)
		{
			if (this.m_ContextIndexFromViewID.ContainsKey(viewID))
			{
				return -1;
			}
			int contextIndex = -1;
			this.m_ContextAllocCounter++;
			if (this.m_Contexts.Length < this.m_ContextAllocInfo.Length)
			{
				contextIndex = this.m_Contexts.Length;
				IndirectBufferContext indirectBufferContext = default(IndirectBufferContext);
				this.m_Contexts.Add(in indirectBufferContext);
				this.m_ContextIndexFromViewID.Add(viewID, contextIndex);
			}
			return contextIndex;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0001125C File Offset: 0x0000F45C
		public int TryGetContextIndex(int viewID)
		{
			int contextIndex;
			if (!this.m_ContextIndexFromViewID.TryGetValue(viewID, out contextIndex))
			{
				contextIndex = -1;
			}
			return contextIndex;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0001127C File Offset: 0x0000F47C
		public NativeArray<IndirectBufferAllocInfo> GetAllocInfoSubArray(int contextIndex)
		{
			int safeIndex = Mathf.Max(contextIndex, 0);
			return this.m_ContextAllocInfo.GetSubArray(safeIndex, 1);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000112A0 File Offset: 0x0000F4A0
		public IndirectBufferAllocInfo GetAllocInfo(int contextIndex)
		{
			IndirectBufferAllocInfo allocInfo = default(IndirectBufferAllocInfo);
			if (0 <= contextIndex && contextIndex < this.m_Contexts.Length)
			{
				allocInfo = this.m_ContextAllocInfo[contextIndex];
			}
			return allocInfo;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public void CopyFromStaging(CommandBuffer cmd, in IndirectBufferAllocInfo allocInfo)
		{
			IndirectBufferAllocInfo indirectBufferAllocInfo = allocInfo;
			if (!indirectBufferAllocInfo.IsEmpty())
			{
				cmd.SetBufferData<IndirectDrawInfo>(this.m_DrawInfoBuffer, this.m_DrawInfoStaging, allocInfo.drawAllocIndex, allocInfo.drawAllocIndex, allocInfo.drawCount);
				cmd.SetBufferData<IndirectInstanceInfo>(this.m_InstanceInfoBuffer, this.m_InstanceInfoStaging, allocInfo.instanceAllocIndex, 2 * allocInfo.instanceAllocIndex, allocInfo.instanceCount);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00011340 File Offset: 0x0000F540
		public IndirectBufferLimits GetLimits(int contextIndex)
		{
			IndirectBufferLimits limits = default(IndirectBufferLimits);
			if (contextIndex >= 0)
			{
				limits = this.m_BufferLimits;
			}
			return limits;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00011364 File Offset: 0x0000F564
		public IndirectBufferContext GetBufferContext(int contextIndex)
		{
			IndirectBufferContext ctx = default(IndirectBufferContext);
			if (0 <= contextIndex && contextIndex < this.m_Contexts.Length)
			{
				ctx = this.m_Contexts[contextIndex];
			}
			return ctx;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00011399 File Offset: 0x0000F599
		public void SetBufferContext(int contextIndex, IndirectBufferContext ctx)
		{
			if (0 <= contextIndex && contextIndex < this.m_Contexts.Length)
			{
				this.m_Contexts[contextIndex] = ctx;
			}
		}

		// Token: 0x04000336 RID: 822
		private const int kAllocatorCount = 2;

		// Token: 0x04000337 RID: 823
		internal const int kExtraDrawAllocationCount = 1;

		// Token: 0x04000338 RID: 824
		internal const int kInstanceInfoGpuOffsetMultiplier = 2;

		// Token: 0x04000339 RID: 825
		private IndirectBufferLimits m_BufferLimits;

		// Token: 0x0400033A RID: 826
		private GraphicsBuffer m_InstanceBuffer;

		// Token: 0x0400033B RID: 827
		private GraphicsBuffer m_InstanceInfoBuffer;

		// Token: 0x0400033C RID: 828
		private NativeArray<IndirectInstanceInfo> m_InstanceInfoStaging;

		// Token: 0x0400033D RID: 829
		private GraphicsBuffer m_ArgsBuffer;

		// Token: 0x0400033E RID: 830
		private GraphicsBuffer m_DrawInfoBuffer;

		// Token: 0x0400033F RID: 831
		private NativeArray<IndirectDrawInfo> m_DrawInfoStaging;

		// Token: 0x04000340 RID: 832
		private int m_ContextAllocCounter;

		// Token: 0x04000341 RID: 833
		private NativeHashMap<int, int> m_ContextIndexFromViewID;

		// Token: 0x04000342 RID: 834
		private NativeList<IndirectBufferContext> m_Contexts;

		// Token: 0x04000343 RID: 835
		private NativeArray<IndirectBufferAllocInfo> m_ContextAllocInfo;

		// Token: 0x04000344 RID: 836
		private NativeArray<int> m_AllocationCounters;
	}
}
