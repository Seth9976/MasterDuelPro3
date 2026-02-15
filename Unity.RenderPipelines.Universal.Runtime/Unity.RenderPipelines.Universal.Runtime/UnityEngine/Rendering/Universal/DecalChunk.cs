using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000082 RID: 130
	internal abstract class DecalChunk : IDisposable
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00009FDB File Offset: 0x000081DB
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00009FE3 File Offset: 0x000081E3
		public int count { get; protected set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00009FEC File Offset: 0x000081EC
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00009FF4 File Offset: 0x000081F4
		public int capacity { get; protected set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00009FFD File Offset: 0x000081FD
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000A005 File Offset: 0x00008205
		public JobHandle currentJobHandle { get; set; }

		// Token: 0x06000301 RID: 769 RVA: 0x0000A010 File Offset: 0x00008210
		public virtual void Push()
		{
			int count = this.count;
			this.count = count + 1;
		}

		// Token: 0x06000302 RID: 770
		public abstract void RemoveAtSwapBack(int index);

		// Token: 0x06000303 RID: 771
		public abstract void SetCapacity(int capacity);

		// Token: 0x06000304 RID: 772 RVA: 0x0000217F File Offset: 0x0000037F
		public virtual void Dispose()
		{
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000A030 File Offset: 0x00008230
		protected void ResizeNativeArray(ref TransformAccessArray array, DecalProjector[] decalProjectors, int capacity)
		{
			TransformAccessArray newArray = new TransformAccessArray(capacity, -1);
			if (array.isCreated)
			{
				for (int i = 0; i < array.length; i++)
				{
					newArray.Add(decalProjectors[i].transform);
				}
				array.Dispose();
			}
			array = newArray;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000A07B File Offset: 0x0000827B
		protected void RemoveAtSwapBack<T>(ref NativeArray<T> array, int index, int count) where T : struct
		{
			array[index] = array[count - 1];
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000A08D File Offset: 0x0000828D
		protected void RemoveAtSwapBack<T>(ref T[] array, int index, int count)
		{
			array[index] = array[count - 1];
		}
	}
}
