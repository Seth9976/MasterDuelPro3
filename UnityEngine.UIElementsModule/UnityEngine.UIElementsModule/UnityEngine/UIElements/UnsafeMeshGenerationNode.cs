using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A6 RID: 678
	internal struct UnsafeMeshGenerationNode
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x0004C561 File Offset: 0x0004A761
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private MeshGenerationNodeImpl GetManaged()
		{
			return (MeshGenerationNodeImpl)this.m_Handle.Target;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0004C574 File Offset: 0x0004A774
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Create(GCHandle handle, out UnsafeMeshGenerationNode node)
		{
			node = new UnsafeMeshGenerationNode
			{
				m_Handle = handle
			};
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004C599 File Offset: 0x0004A799
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DrawMesh(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture = null)
		{
			this.GetManaged().DrawMesh(vertices, indices, texture, false);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004C5AC File Offset: 0x0004A7AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void DrawMeshInternal(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture = null, bool skipAtlas = false)
		{
			this.GetManaged().DrawMesh(vertices, indices, texture, skipAtlas);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0004C5C0 File Offset: 0x0004A7C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void DrawGradientsInternal(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, VectorImage gradientsOwner)
		{
			this.GetManaged().DrawGradients(vertices, indices, gradientsOwner);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004C5D2 File Offset: 0x0004A7D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Entry GetParentEntry()
		{
			return this.GetManaged().GetParentEntry();
		}

		// Token: 0x04000AA1 RID: 2721
		private GCHandle m_Handle;
	}
}
