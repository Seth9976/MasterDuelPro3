using System;
using Unity.Collections;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000383 RID: 899
	[UsedByNativeCode]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	public struct BatchCullingContext
	{
		// Token: 0x060018D5 RID: 6357 RVA: 0x00035260 File Offset: 0x00033460
		internal BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<CullingSplit> inCullingSplits, LODParameters inLodParameters, Matrix4x4 inLocalToWorldMatrix, BatchCullingViewType inViewType, BatchCullingProjectionType inProjectionType, BatchCullingFlags inBatchCullingFlags, ulong inViewID, uint inCullingLayerMask, ulong inSceneCullingMask, byte inExclusionSplitMask, int inReceiverPlaneOffset, int inReceiverPlaneCount, IntPtr inOcclusionBuffer)
		{
			this.cullingPlanes = inCullingPlanes;
			this.cullingSplits = inCullingSplits;
			this.lodParameters = inLodParameters;
			this.localToWorldMatrix = inLocalToWorldMatrix;
			this.viewType = inViewType;
			this.projectionType = inProjectionType;
			this.cullingFlags = inBatchCullingFlags;
			this.viewID = new BatchPackedCullingViewID
			{
				handle = inViewID
			};
			this.cullingLayerMask = inCullingLayerMask;
			this.sceneCullingMask = inSceneCullingMask;
			this.splitExclusionMask = (ushort)inExclusionSplitMask;
			this.receiverPlaneOffset = inReceiverPlaneOffset;
			this.receiverPlaneCount = inReceiverPlaneCount;
			this.isOrthographic = 0;
			this.occlusionBuffer = inOcclusionBuffer;
		}

		// Token: 0x04000ADB RID: 2779
		public readonly NativeArray<Plane> cullingPlanes;

		// Token: 0x04000ADC RID: 2780
		public readonly NativeArray<CullingSplit> cullingSplits;

		// Token: 0x04000ADD RID: 2781
		public readonly LODParameters lodParameters;

		// Token: 0x04000ADE RID: 2782
		public readonly Matrix4x4 localToWorldMatrix;

		// Token: 0x04000ADF RID: 2783
		public readonly BatchCullingViewType viewType;

		// Token: 0x04000AE0 RID: 2784
		public readonly BatchCullingProjectionType projectionType;

		// Token: 0x04000AE1 RID: 2785
		public readonly BatchCullingFlags cullingFlags;

		// Token: 0x04000AE2 RID: 2786
		public readonly BatchPackedCullingViewID viewID;

		// Token: 0x04000AE3 RID: 2787
		public readonly uint cullingLayerMask;

		// Token: 0x04000AE4 RID: 2788
		public readonly ulong sceneCullingMask;

		// Token: 0x04000AE5 RID: 2789
		public readonly ushort splitExclusionMask;

		// Token: 0x04000AE6 RID: 2790
		[Obsolete("BatchCullingContext.isOrthographic is deprecated. Use BatchCullingContext.projectionType instead.")]
		public readonly byte isOrthographic;

		// Token: 0x04000AE7 RID: 2791
		public readonly int receiverPlaneOffset;

		// Token: 0x04000AE8 RID: 2792
		public readonly int receiverPlaneCount;

		// Token: 0x04000AE9 RID: 2793
		internal readonly IntPtr occlusionBuffer;
	}
}
