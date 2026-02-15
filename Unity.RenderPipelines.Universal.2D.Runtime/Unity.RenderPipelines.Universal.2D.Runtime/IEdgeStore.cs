using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000074 RID: 116
	internal interface IEdgeStore
	{
		// Token: 0x060002DE RID: 734
		NativeArray<ShadowEdge> GetOutsideEdges(NativeArray<Vector3> vertices, NativeArray<int> indices);
	}
}
