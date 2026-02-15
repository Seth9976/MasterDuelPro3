using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AE RID: 686
	[NativeContainerIsReadOnly]
	[NativeContainer]
	public struct TempMeshAllocator
	{
		// Token: 0x06001292 RID: 4754 RVA: 0x0004D2E4 File Offset: 0x0004B4E4
		internal static void Create(GCHandle handle, out TempMeshAllocator allocator)
		{
			allocator = new TempMeshAllocator
			{
				m_Handle = handle
			};
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0004D30C File Offset: 0x0004B50C
		public void AllocateTempMesh(int vertexCount, int indexCount, out NativeSlice<Vertex> vertices, out NativeSlice<ushort> indices)
		{
			TempMeshAllocatorImpl impl = this.m_Handle.Target as TempMeshAllocatorImpl;
			Debug.Assert(impl != null);
			impl.AllocateTempMesh(vertexCount, indexCount, out vertices, out indices);
		}

		// Token: 0x04000AC2 RID: 2754
		private GCHandle m_Handle;
	}
}
