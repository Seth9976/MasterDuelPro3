using System;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A1 RID: 673
	public class MeshWriteData
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x0004C177 File Offset: 0x0004A377
		internal MeshWriteData()
		{
		}

		// Token: 0x04000A8A RID: 2698
		internal NativeSlice<Vertex> m_Vertices;

		// Token: 0x04000A8B RID: 2699
		internal NativeSlice<ushort> m_Indices;

		// Token: 0x04000A8C RID: 2700
		internal int currentIndex;

		// Token: 0x04000A8D RID: 2701
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int currentVertex;
	}
}
