using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200055A RID: 1370
	internal class TempMeshAllocatorImpl : IDisposable
	{
		// Token: 0x060025C2 RID: 9666 RVA: 0x00095FC8 File Offset: 0x000941C8
		public TempMeshAllocatorImpl()
		{
			this.m_GCHandle = GCHandle.Alloc(this);
			this.m_ThreadData = new TempMeshAllocatorImpl.ThreadData[JobsUtility.ThreadIndexCount];
			for (int i = 0; i < JobsUtility.ThreadIndexCount; i++)
			{
				this.m_ThreadData[i].allocations = new List<IntPtr>();
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00096059 File Offset: 0x00094259
		public void CreateNativeHandle(out TempMeshAllocator allocator)
		{
			TempMeshAllocator.Create(this.m_GCHandle, out allocator);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0009606C File Offset: 0x0009426C
		private unsafe NativeSlice<T> Allocate<T>(int count, int alignment) where T : struct
		{
			ref TempMeshAllocatorImpl.ThreadData threadData = ref this.m_ThreadData[UIRUtility.GetThreadIndex()];
			Debug.Assert(count > 0);
			long size = (long)(UnsafeUtility.SizeOf<T>() * count);
			void* address = UnsafeUtility.Malloc(size, UnsafeUtility.AlignOf<T>(), Allocator.TempJob);
			threadData.allocations.Add((IntPtr)address);
			NativeArray<T> array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(address, count, Allocator.Invalid);
			return array;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000960D4 File Offset: 0x000942D4
		public void AllocateTempMesh(int vertexCount, int indexCount, out NativeSlice<Vertex> vertices, out NativeSlice<ushort> indices)
		{
			bool flag = (long)vertexCount > (long)((ulong)UIRenderDevice.maxVerticesPerPage);
			if (flag)
			{
				throw new ArgumentOutOfRangeException("vertexCount", string.Format("Attempting to allocate {0} vertices which exceeds the limit of {1}.", vertexCount, UIRenderDevice.maxVerticesPerPage));
			}
			bool flag2 = !JobsUtility.IsExecutingJob;
			if (flag2)
			{
				bool disposed = this.disposed;
				if (disposed)
				{
					DisposeHelper.NotifyDisposedUsed(this);
					vertices = default(NativeSlice<Vertex>);
					indices = default(NativeSlice<ushort>);
				}
				else
				{
					vertices = ((vertexCount > 0) ? this.m_VertexPool.Alloc(vertexCount) : default(NativeSlice<Vertex>));
					indices = ((indexCount > 0) ? this.m_IndexPool.Alloc(indexCount) : default(NativeSlice<ushort>));
				}
			}
			else
			{
				vertices = ((vertexCount > 0) ? this.Allocate<Vertex>(vertexCount, 4) : default(NativeSlice<Vertex>));
				indices = ((indexCount > 0) ? this.Allocate<ushort>(indexCount, 2) : default(NativeSlice<ushort>));
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000961CC File Offset: 0x000943CC
		public void Clear()
		{
			for (int i = 0; i < this.m_ThreadData.Length; i++)
			{
				foreach (IntPtr ptr in this.m_ThreadData[i].allocations)
				{
					UnsafeUtility.Free(ptr.ToPointer(), Allocator.TempJob);
				}
				this.m_ThreadData[i].allocations.Clear();
			}
			this.m_VertexPool.Reset();
			this.m_IndexPool.Reset();
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x0009627C File Offset: 0x0009447C
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x00096284 File Offset: 0x00094484
		private protected bool disposed { protected get; private set; }

		// Token: 0x060025C9 RID: 9673 RVA: 0x0009628D File Offset: 0x0009448D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000962A0 File Offset: 0x000944A0
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.Clear();
					this.m_GCHandle.Free();
					this.m_VertexPool.Dispose();
					this.m_IndexPool.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x040012F1 RID: 4849
		private GCHandle m_GCHandle;

		// Token: 0x040012F2 RID: 4850
		private TempMeshAllocatorImpl.ThreadData[] m_ThreadData;

		// Token: 0x040012F3 RID: 4851
		private TempAllocator<Vertex> m_VertexPool = new TempAllocator<Vertex>(8192, 2048, 65536);

		// Token: 0x040012F4 RID: 4852
		private TempAllocator<ushort> m_IndexPool = new TempAllocator<ushort>(16384, 4096, 131072);

		// Token: 0x0200055B RID: 1371
		private struct ThreadData
		{
			// Token: 0x040012F6 RID: 4854
			public List<IntPtr> allocations;
		}
	}
}
