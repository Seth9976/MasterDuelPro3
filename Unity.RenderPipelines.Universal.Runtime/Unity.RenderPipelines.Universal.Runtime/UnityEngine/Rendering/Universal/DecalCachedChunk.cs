using System;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000090 RID: 144
	internal class DecalCachedChunk : DecalChunk
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		public override void RemoveAtSwapBack(int entityIndex)
		{
			base.RemoveAtSwapBack<float4x4>(ref this.decalToWorlds, entityIndex, base.count);
			base.RemoveAtSwapBack<float4x4>(ref this.normalToWorlds, entityIndex, base.count);
			base.RemoveAtSwapBack<float4x4>(ref this.sizeOffsets, entityIndex, base.count);
			base.RemoveAtSwapBack<float2>(ref this.drawDistances, entityIndex, base.count);
			base.RemoveAtSwapBack<float2>(ref this.angleFades, entityIndex, base.count);
			base.RemoveAtSwapBack<float4>(ref this.uvScaleBias, entityIndex, base.count);
			base.RemoveAtSwapBack<int>(ref this.layerMasks, entityIndex, base.count);
			base.RemoveAtSwapBack<ulong>(ref this.sceneLayerMasks, entityIndex, base.count);
			base.RemoveAtSwapBack<float>(ref this.fadeFactors, entityIndex, base.count);
			base.RemoveAtSwapBack<BoundingSphere>(ref this.boundingSphereArray, entityIndex, base.count);
			base.RemoveAtSwapBack<BoundingSphere>(ref this.boundingSpheres, entityIndex, base.count);
			base.RemoveAtSwapBack<DecalScaleMode>(ref this.scaleModes, entityIndex, base.count);
			base.RemoveAtSwapBack<uint>(ref this.renderingLayerMasks, entityIndex, base.count);
			base.RemoveAtSwapBack<float3>(ref this.positions, entityIndex, base.count);
			base.RemoveAtSwapBack<quaternion>(ref this.rotation, entityIndex, base.count);
			base.RemoveAtSwapBack<float3>(ref this.scales, entityIndex, base.count);
			base.RemoveAtSwapBack<bool>(ref this.dirty, entityIndex, base.count);
			int count = base.count;
			base.count = count - 1;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C108 File Offset: 0x0000A308
		public override void SetCapacity(int newCapacity)
		{
			(ref this.decalToWorlds).ResizeArray(newCapacity);
			(ref this.normalToWorlds).ResizeArray(newCapacity);
			(ref this.sizeOffsets).ResizeArray(newCapacity);
			(ref this.drawDistances).ResizeArray(newCapacity);
			(ref this.angleFades).ResizeArray(newCapacity);
			(ref this.uvScaleBias).ResizeArray(newCapacity);
			(ref this.layerMasks).ResizeArray(newCapacity);
			(ref this.sceneLayerMasks).ResizeArray(newCapacity);
			(ref this.fadeFactors).ResizeArray(newCapacity);
			(ref this.boundingSpheres).ResizeArray(newCapacity);
			(ref this.scaleModes).ResizeArray(newCapacity);
			(ref this.renderingLayerMasks).ResizeArray(newCapacity);
			(ref this.positions).ResizeArray(newCapacity);
			(ref this.rotation).ResizeArray(newCapacity);
			(ref this.scales).ResizeArray(newCapacity);
			(ref this.dirty).ResizeArray(newCapacity);
			ArrayExtensions.ResizeArray<BoundingSphere>(ref this.boundingSphereArray, newCapacity);
			base.capacity = newCapacity;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		public override void Dispose()
		{
			if (base.capacity == 0)
			{
				return;
			}
			this.decalToWorlds.Dispose();
			this.normalToWorlds.Dispose();
			this.sizeOffsets.Dispose();
			this.drawDistances.Dispose();
			this.angleFades.Dispose();
			this.uvScaleBias.Dispose();
			this.layerMasks.Dispose();
			this.sceneLayerMasks.Dispose();
			this.fadeFactors.Dispose();
			this.boundingSpheres.Dispose();
			this.scaleModes.Dispose();
			this.renderingLayerMasks.Dispose();
			this.positions.Dispose();
			this.rotation.Dispose();
			this.scales.Dispose();
			this.dirty.Dispose();
			base.count = 0;
			base.capacity = 0;
		}

		// Token: 0x040002A8 RID: 680
		public MaterialPropertyBlock propertyBlock;

		// Token: 0x040002A9 RID: 681
		public int passIndexDBuffer;

		// Token: 0x040002AA RID: 682
		public int passIndexEmissive;

		// Token: 0x040002AB RID: 683
		public int passIndexScreenSpace;

		// Token: 0x040002AC RID: 684
		public int passIndexGBuffer;

		// Token: 0x040002AD RID: 685
		public int drawOrder;

		// Token: 0x040002AE RID: 686
		public bool isCreated;

		// Token: 0x040002AF RID: 687
		public NativeArray<float4x4> decalToWorlds;

		// Token: 0x040002B0 RID: 688
		public NativeArray<float4x4> normalToWorlds;

		// Token: 0x040002B1 RID: 689
		public NativeArray<float4x4> sizeOffsets;

		// Token: 0x040002B2 RID: 690
		public NativeArray<float2> drawDistances;

		// Token: 0x040002B3 RID: 691
		public NativeArray<float2> angleFades;

		// Token: 0x040002B4 RID: 692
		public NativeArray<float4> uvScaleBias;

		// Token: 0x040002B5 RID: 693
		public NativeArray<int> layerMasks;

		// Token: 0x040002B6 RID: 694
		public NativeArray<ulong> sceneLayerMasks;

		// Token: 0x040002B7 RID: 695
		public NativeArray<float> fadeFactors;

		// Token: 0x040002B8 RID: 696
		public NativeArray<BoundingSphere> boundingSpheres;

		// Token: 0x040002B9 RID: 697
		public NativeArray<DecalScaleMode> scaleModes;

		// Token: 0x040002BA RID: 698
		public NativeArray<uint> renderingLayerMasks;

		// Token: 0x040002BB RID: 699
		public NativeArray<float3> positions;

		// Token: 0x040002BC RID: 700
		public NativeArray<quaternion> rotation;

		// Token: 0x040002BD RID: 701
		public NativeArray<float3> scales;

		// Token: 0x040002BE RID: 702
		public NativeArray<bool> dirty;

		// Token: 0x040002BF RID: 703
		public BoundingSphere[] boundingSphereArray;
	}
}
