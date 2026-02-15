using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

// Token: 0x02000003 RID: 3
internal struct VertexDictionary
{
	// Token: 0x06000003 RID: 3 RVA: 0x000020AC File Offset: 0x000002AC
	public NativeArray<int> GetIndexRemap(NativeArray<Vector3> vertices, NativeArray<int> indices)
	{
		NativeArray<int> vertexMapping = new NativeArray<int>(vertices.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
		VertexDictionary.m_VertexDictionary.Clear();
		VertexDictionary.m_VertexDictionary.EnsureCapacity(vertices.Length);
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 vertex = vertices[i];
			if (!VertexDictionary.m_VertexDictionary.ContainsKey(vertex))
			{
				vertexMapping[i] = i;
				VertexDictionary.m_VertexDictionary.Add(vertex, i);
			}
			else
			{
				vertexMapping[i] = VertexDictionary.m_VertexDictionary[vertex];
			}
		}
		NativeArray<int> remappedIndices = new NativeArray<int>(indices.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
		for (int j = 0; j < indices.Length; j++)
		{
			remappedIndices[j] = vertexMapping[indices[j]];
		}
		return remappedIndices;
	}

	// Token: 0x04000001 RID: 1
	private static Dictionary<Vector3, int> m_VertexDictionary = new Dictionary<Vector3, int>();
}
