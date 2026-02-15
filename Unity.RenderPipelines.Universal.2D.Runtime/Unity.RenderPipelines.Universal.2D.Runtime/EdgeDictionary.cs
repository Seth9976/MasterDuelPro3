using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000072 RID: 114
	internal struct EdgeDictionary : IEdgeStore
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00015990 File Offset: 0x00013B90
		public NativeArray<ShadowEdge> GetOutsideEdges(NativeArray<Vector3> vertices, NativeArray<int> indices)
		{
			EdgeDictionary.m_EdgeDictionary.Clear();
			EdgeDictionary.m_EdgeDictionary.EnsureCapacity(indices.Length);
			for (int i = 0; i < indices.Length; i += 3)
			{
				int v0Index = indices[i];
				int v1Index = indices[i + 1];
				int v2Index = indices[i + 2];
				ShadowEdge edge0 = new ShadowEdge(v0Index, v1Index);
				ShadowEdge edge = new ShadowEdge(v1Index, v2Index);
				ShadowEdge edge2 = new ShadowEdge(v2Index, v0Index);
				if (EdgeDictionary.m_EdgeDictionary.ContainsKey(edge0))
				{
					EdgeDictionary.m_EdgeDictionary[edge0] = EdgeDictionary.m_EdgeDictionary[edge0] + 1;
				}
				else
				{
					EdgeDictionary.m_EdgeDictionary.Add(edge0, 1);
				}
				if (EdgeDictionary.m_EdgeDictionary.ContainsKey(edge))
				{
					EdgeDictionary.m_EdgeDictionary[edge] = EdgeDictionary.m_EdgeDictionary[edge] + 1;
				}
				else
				{
					EdgeDictionary.m_EdgeDictionary.Add(edge, 1);
				}
				if (EdgeDictionary.m_EdgeDictionary.ContainsKey(edge2))
				{
					EdgeDictionary.m_EdgeDictionary[edge2] = EdgeDictionary.m_EdgeDictionary[edge2] + 1;
				}
				else
				{
					EdgeDictionary.m_EdgeDictionary.Add(edge2, 1);
				}
			}
			int outsideEdges = 0;
			foreach (KeyValuePair<ShadowEdge, int> keyValuePair in EdgeDictionary.m_EdgeDictionary)
			{
				if (keyValuePair.Value == 1)
				{
					outsideEdges++;
				}
			}
			int edgeIndex = 0;
			NativeArray<ShadowEdge> edges = new NativeArray<ShadowEdge>(outsideEdges, Allocator.Temp, NativeArrayOptions.ClearMemory);
			foreach (KeyValuePair<ShadowEdge, int> keyValuePair2 in EdgeDictionary.m_EdgeDictionary)
			{
				if (keyValuePair2.Value == 1)
				{
					edges[edgeIndex++] = keyValuePair2.Key;
				}
			}
			return edges;
		}

		// Token: 0x04000291 RID: 657
		private static Dictionary<ShadowEdge, int> m_EdgeDictionary = new Dictionary<ShadowEdge, int>(new EdgeDictionary.EdgeComparer());

		// Token: 0x02000073 RID: 115
		private class EdgeComparer : IEqualityComparer<ShadowEdge>
		{
			// Token: 0x060002DB RID: 731 RVA: 0x00015B81 File Offset: 0x00013D81
			public bool Equals(ShadowEdge edge0, ShadowEdge edge1)
			{
				return (edge0.v0 == edge1.v0 && edge0.v1 == edge1.v1) || (edge0.v1 == edge1.v0 && edge0.v0 == edge1.v1);
			}

			// Token: 0x060002DC RID: 732 RVA: 0x00015BC0 File Offset: 0x00013DC0
			public int GetHashCode(ShadowEdge edge)
			{
				int v0 = edge.v0;
				int v = edge.v1;
				if (edge.v1 < edge.v0)
				{
					v0 = edge.v1;
					v = edge.v0;
				}
				return ((v0 << 15) | v).GetHashCode();
			}
		}
	}
}
