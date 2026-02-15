using System;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000084 RID: 132
	internal class DecalDrawCallChunk : DecalChunk
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000A0BF File Offset: 0x000082BF
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000A0B0 File Offset: 0x000082B0
		public int subCallCount
		{
			get
			{
				return this.subCallCounts[0];
			}
			set
			{
				this.subCallCounts[0] = value;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A0D0 File Offset: 0x000082D0
		public override void RemoveAtSwapBack(int entityIndex)
		{
			base.RemoveAtSwapBack<float4x4>(ref this.decalToWorlds, entityIndex, base.count);
			base.RemoveAtSwapBack<float4x4>(ref this.normalToDecals, entityIndex, base.count);
			base.RemoveAtSwapBack<float>(ref this.renderingLayerMasks, entityIndex, base.count);
			base.RemoveAtSwapBack<DecalSubDrawCall>(ref this.subCalls, entityIndex, base.count);
			int count = base.count;
			base.count = count - 1;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A139 File Offset: 0x00008339
		public override void SetCapacity(int newCapacity)
		{
			(ref this.decalToWorlds).ResizeArray(newCapacity);
			(ref this.normalToDecals).ResizeArray(newCapacity);
			(ref this.renderingLayerMasks).ResizeArray(newCapacity);
			(ref this.subCalls).ResizeArray(newCapacity);
			base.capacity = newCapacity;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A174 File Offset: 0x00008374
		public override void Dispose()
		{
			this.subCallCounts.Dispose();
			if (base.capacity == 0)
			{
				return;
			}
			this.decalToWorlds.Dispose();
			this.normalToDecals.Dispose();
			this.renderingLayerMasks.Dispose();
			this.subCalls.Dispose();
			base.count = 0;
			base.capacity = 0;
		}

		// Token: 0x0400025F RID: 607
		public NativeArray<float4x4> decalToWorlds;

		// Token: 0x04000260 RID: 608
		public NativeArray<float4x4> normalToDecals;

		// Token: 0x04000261 RID: 609
		public NativeArray<float> renderingLayerMasks;

		// Token: 0x04000262 RID: 610
		public NativeArray<DecalSubDrawCall> subCalls;

		// Token: 0x04000263 RID: 611
		public NativeArray<int> subCallCounts;
	}
}
