using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200024C RID: 588
	public sealed class RenderGraphObjectPool
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003A36B File Offset: 0x0003856B
		internal RenderGraphObjectPool()
		{
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003A394 File Offset: 0x00038594
		public T[] GetTempArray<T>(int size)
		{
			Stack<object> stack;
			if (!this.m_ArrayPool.TryGetValue(new ValueTuple<Type, int>(typeof(T), size), out stack))
			{
				stack = new Stack<object>();
				this.m_ArrayPool.Add(new ValueTuple<Type, int>(typeof(T), size), stack);
			}
			T[] result = ((stack.Count > 0) ? ((T[])stack.Pop()) : new T[size]);
			this.m_AllocatedArrays.Add(new ValueTuple<object, ValueTuple<Type, int>>(result, new ValueTuple<Type, int>(typeof(T), size)));
			return result;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003A424 File Offset: 0x00038624
		public MaterialPropertyBlock GetTempMaterialPropertyBlock()
		{
			MaterialPropertyBlock result = RenderGraphObjectPool.SharedObjectPool<MaterialPropertyBlock>.Get();
			result.Clear();
			this.m_AllocatedMaterialPropertyBlocks.Add(result);
			return result;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003A44C File Offset: 0x0003864C
		internal void ReleaseAllTempAlloc()
		{
			foreach (ValueTuple<object, ValueTuple<Type, int>> arrayDesc in this.m_AllocatedArrays)
			{
				Stack<object> stack;
				this.m_ArrayPool.TryGetValue(arrayDesc.Item2, out stack);
				stack.Push(arrayDesc.Item1);
			}
			this.m_AllocatedArrays.Clear();
			foreach (MaterialPropertyBlock materialPropertyBlock in this.m_AllocatedMaterialPropertyBlocks)
			{
				RenderGraphObjectPool.SharedObjectPool<MaterialPropertyBlock>.Release(materialPropertyBlock);
			}
			this.m_AllocatedMaterialPropertyBlocks.Clear();
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003A510 File Offset: 0x00038710
		internal T Get<T>() where T : class, new()
		{
			return RenderGraphObjectPool.SharedObjectPool<T>.Get();
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003A517 File Offset: 0x00038717
		internal void Release<T>(T value) where T : class, new()
		{
			RenderGraphObjectPool.SharedObjectPool<T>.Release(value);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003A520 File Offset: 0x00038720
		internal void Cleanup()
		{
			this.m_AllocatedArrays.Clear();
			this.m_AllocatedMaterialPropertyBlocks.Clear();
			this.m_ArrayPool.Clear();
			foreach (ref RenderGraphObjectPool.SharedObjectPoolBase ptr in RenderGraphObjectPool.s_AllocatedPools)
			{
				ptr.Clear();
			}
		}

		// Token: 0x04000A46 RID: 2630
		private static DynamicArray<RenderGraphObjectPool.SharedObjectPoolBase> s_AllocatedPools = new DynamicArray<RenderGraphObjectPool.SharedObjectPoolBase>();

		// Token: 0x04000A47 RID: 2631
		private Dictionary<ValueTuple<Type, int>, Stack<object>> m_ArrayPool = new Dictionary<ValueTuple<Type, int>, Stack<object>>();

		// Token: 0x04000A48 RID: 2632
		private List<ValueTuple<object, ValueTuple<Type, int>>> m_AllocatedArrays = new List<ValueTuple<object, ValueTuple<Type, int>>>();

		// Token: 0x04000A49 RID: 2633
		private List<MaterialPropertyBlock> m_AllocatedMaterialPropertyBlocks = new List<MaterialPropertyBlock>();

		// Token: 0x0200024D RID: 589
		private class SharedObjectPoolBase
		{
			// Token: 0x06000FFC RID: 4092 RVA: 0x00005704 File Offset: 0x00003904
			public virtual void Clear()
			{
			}
		}

		// Token: 0x0200024E RID: 590
		private class SharedObjectPool<T> : RenderGraphObjectPool.SharedObjectPoolBase where T : class, new()
		{
			// Token: 0x06000FFD RID: 4093 RVA: 0x0003A580 File Offset: 0x00038780
			private static ObjectPool<T> AllocatePool()
			{
				ObjectPool<T> objectPool = new ObjectPool<T>(() => new T(), null, null, null, true, 10, 10000);
				DynamicArray<RenderGraphObjectPool.SharedObjectPoolBase> s_AllocatedPools = RenderGraphObjectPool.s_AllocatedPools;
				RenderGraphObjectPool.SharedObjectPoolBase sharedObjectPoolBase = new RenderGraphObjectPool.SharedObjectPool<T>();
				s_AllocatedPools.Add(in sharedObjectPoolBase);
				return objectPool;
			}

			// Token: 0x06000FFE RID: 4094 RVA: 0x0003A5CF File Offset: 0x000387CF
			public override void Clear()
			{
				RenderGraphObjectPool.SharedObjectPool<T>.s_Pool.Clear();
			}

			// Token: 0x06000FFF RID: 4095 RVA: 0x0003A5DB File Offset: 0x000387DB
			public static T Get()
			{
				return RenderGraphObjectPool.SharedObjectPool<T>.s_Pool.Get();
			}

			// Token: 0x06001000 RID: 4096 RVA: 0x0003A5E7 File Offset: 0x000387E7
			public static void Release(T toRelease)
			{
				RenderGraphObjectPool.SharedObjectPool<T>.s_Pool.Release(toRelease);
			}

			// Token: 0x04000A4A RID: 2634
			private static readonly ObjectPool<T> s_Pool = RenderGraphObjectPool.SharedObjectPool<T>.AllocatePool();
		}
	}
}
