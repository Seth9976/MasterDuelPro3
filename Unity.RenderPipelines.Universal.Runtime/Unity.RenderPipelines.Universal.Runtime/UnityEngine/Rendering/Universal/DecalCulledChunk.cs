using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000094 RID: 148
	internal class DecalCulledChunk : DecalChunk
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000C890 File Offset: 0x0000AA90
		public override void RemoveAtSwapBack(int entityIndex)
		{
			base.RemoveAtSwapBack<int>(ref this.visibleDecalIndexArray, entityIndex, base.count);
			base.RemoveAtSwapBack<int>(ref this.visibleDecalIndices, entityIndex, base.count);
			int count = base.count;
			base.count = count - 1;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		public override void SetCapacity(int newCapacity)
		{
			ArrayExtensions.ResizeArray<int>(ref this.visibleDecalIndexArray, newCapacity);
			(ref this.visibleDecalIndices).ResizeArray(newCapacity);
			if (this.cullingGroups == null)
			{
				this.cullingGroups = new CullingGroup();
			}
			base.capacity = newCapacity;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C907 File Offset: 0x0000AB07
		public override void Dispose()
		{
			if (base.capacity == 0)
			{
				return;
			}
			this.visibleDecalIndices.Dispose();
			this.visibleDecalIndexArray = null;
			base.count = 0;
			base.capacity = 0;
			this.cullingGroups.Dispose();
			this.cullingGroups = null;
		}

		// Token: 0x040002D0 RID: 720
		public Vector3 cameraPosition;

		// Token: 0x040002D1 RID: 721
		public ulong sceneCullingMask;

		// Token: 0x040002D2 RID: 722
		public int cullingMask;

		// Token: 0x040002D3 RID: 723
		public CullingGroup cullingGroups;

		// Token: 0x040002D4 RID: 724
		public int[] visibleDecalIndexArray;

		// Token: 0x040002D5 RID: 725
		public NativeArray<int> visibleDecalIndices;

		// Token: 0x040002D6 RID: 726
		public int visibleDecalCount;
	}
}
