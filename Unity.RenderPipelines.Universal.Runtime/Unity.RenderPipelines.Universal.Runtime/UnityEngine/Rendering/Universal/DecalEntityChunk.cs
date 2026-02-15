using System;
using Unity.Collections;
using UnityEngine.Jobs;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200008B RID: 139
	internal class DecalEntityChunk : DecalChunk
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public override void Push()
		{
			int count = base.count;
			base.count = count + 1;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000AFC0 File Offset: 0x000091C0
		public override void RemoveAtSwapBack(int entityIndex)
		{
			base.RemoveAtSwapBack<DecalEntity>(ref this.decalEntities, entityIndex, base.count);
			base.RemoveAtSwapBack<DecalProjector>(ref this.decalProjectors, entityIndex, base.count);
			this.transformAccessArray.RemoveAtSwapBack(entityIndex);
			int count = base.count;
			base.count = count - 1;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B00F File Offset: 0x0000920F
		public override void SetCapacity(int newCapacity)
		{
			(ref this.decalEntities).ResizeArray(newCapacity);
			base.ResizeNativeArray(ref this.transformAccessArray, this.decalProjectors, newCapacity);
			ArrayExtensions.ResizeArray<DecalProjector>(ref this.decalProjectors, newCapacity);
			base.capacity = newCapacity;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B043 File Offset: 0x00009243
		public override void Dispose()
		{
			if (base.capacity == 0)
			{
				return;
			}
			this.decalEntities.Dispose();
			this.transformAccessArray.Dispose();
			this.decalProjectors = null;
			base.count = 0;
			base.capacity = 0;
		}

		// Token: 0x0400028B RID: 651
		public Material material;

		// Token: 0x0400028C RID: 652
		public NativeArray<DecalEntity> decalEntities;

		// Token: 0x0400028D RID: 653
		public DecalProjector[] decalProjectors;

		// Token: 0x0400028E RID: 654
		public TransformAccessArray transformAccessArray;
	}
}
