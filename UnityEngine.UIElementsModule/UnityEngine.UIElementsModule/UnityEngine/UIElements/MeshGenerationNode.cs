using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A5 RID: 677
	[NativeContainer]
	public struct MeshGenerationNode
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x0004C53D File Offset: 0x0004A73D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Create(GCHandle handle, out MeshGenerationNode node)
		{
			node = default(MeshGenerationNode);
			UnsafeMeshGenerationNode.Create(handle, out node.m_UnsafeNode);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0004C554 File Offset: 0x0004A754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Entry GetParentEntry()
		{
			return this.m_UnsafeNode.GetParentEntry();
		}

		// Token: 0x04000AA0 RID: 2720
		private UnsafeMeshGenerationNode m_UnsafeNode;
	}
}
