using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000081 RID: 129
	public class VertexHelper : IDisposable
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x000020BB File Offset: 0x000002BB
		public VertexHelper()
		{
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000167A8 File Offset: 0x000149A8
		public VertexHelper(Mesh m)
		{
			this.InitializeListIfRequired();
			this.m_Positions.AddRange(m.vertices);
			this.m_Colors.AddRange(m.colors32);
			List<Vector4> tempUVList = new List<Vector4>();
			m.GetUVs(0, tempUVList);
			this.m_Uv0S.AddRange(tempUVList);
			m.GetUVs(1, tempUVList);
			this.m_Uv1S.AddRange(tempUVList);
			m.GetUVs(2, tempUVList);
			this.m_Uv2S.AddRange(tempUVList);
			m.GetUVs(3, tempUVList);
			this.m_Uv3S.AddRange(tempUVList);
			this.m_Normals.AddRange(m.normals);
			this.m_Tangents.AddRange(m.tangents);
			this.m_Indices.AddRange(m.GetIndices(0));
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00016870 File Offset: 0x00014A70
		private void InitializeListIfRequired()
		{
			if (!this.m_ListsInitalized)
			{
				this.m_Positions = CollectionPool<List<Vector3>, Vector3>.Get();
				this.m_Colors = CollectionPool<List<Color32>, Color32>.Get();
				this.m_Uv0S = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Uv1S = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Uv2S = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Uv3S = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Normals = CollectionPool<List<Vector3>, Vector3>.Get();
				this.m_Tangents = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Indices = CollectionPool<List<int>, int>.Get();
				this.m_ListsInitalized = true;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000168F0 File Offset: 0x00014AF0
		public void Dispose()
		{
			if (this.m_ListsInitalized)
			{
				CollectionPool<List<Vector3>, Vector3>.Release(this.m_Positions);
				CollectionPool<List<Color32>, Color32>.Release(this.m_Colors);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Uv0S);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Uv1S);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Uv2S);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Uv3S);
				CollectionPool<List<Vector3>, Vector3>.Release(this.m_Normals);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Tangents);
				CollectionPool<List<int>, int>.Release(this.m_Indices);
				this.m_Positions = null;
				this.m_Colors = null;
				this.m_Uv0S = null;
				this.m_Uv1S = null;
				this.m_Uv2S = null;
				this.m_Uv3S = null;
				this.m_Normals = null;
				this.m_Tangents = null;
				this.m_Indices = null;
				this.m_ListsInitalized = false;
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000169B4 File Offset: 0x00014BB4
		public void Clear()
		{
			if (this.m_ListsInitalized)
			{
				this.m_Positions.Clear();
				this.m_Colors.Clear();
				this.m_Uv0S.Clear();
				this.m_Uv1S.Clear();
				this.m_Uv2S.Clear();
				this.m_Uv3S.Clear();
				this.m_Normals.Clear();
				this.m_Tangents.Clear();
				this.m_Indices.Clear();
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00016A2C File Offset: 0x00014C2C
		public int currentVertCount
		{
			get
			{
				if (this.m_Positions == null)
				{
					return 0;
				}
				return this.m_Positions.Count;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00016A43 File Offset: 0x00014C43
		public int currentIndexCount
		{
			get
			{
				if (this.m_Indices == null)
				{
					return 0;
				}
				return this.m_Indices.Count;
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00016A5C File Offset: 0x00014C5C
		public void PopulateUIVertex(ref UIVertex vertex, int i)
		{
			this.InitializeListIfRequired();
			vertex.position = this.m_Positions[i];
			vertex.color = this.m_Colors[i];
			vertex.uv0 = this.m_Uv0S[i];
			vertex.uv1 = this.m_Uv1S[i];
			vertex.uv2 = this.m_Uv2S[i];
			vertex.uv3 = this.m_Uv3S[i];
			vertex.normal = this.m_Normals[i];
			vertex.tangent = this.m_Tangents[i];
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00016B00 File Offset: 0x00014D00
		public void SetUIVertex(UIVertex vertex, int i)
		{
			this.InitializeListIfRequired();
			this.m_Positions[i] = vertex.position;
			this.m_Colors[i] = vertex.color;
			this.m_Uv0S[i] = vertex.uv0;
			this.m_Uv1S[i] = vertex.uv1;
			this.m_Uv2S[i] = vertex.uv2;
			this.m_Uv3S[i] = vertex.uv3;
			this.m_Normals[i] = vertex.normal;
			this.m_Tangents[i] = vertex.tangent;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00016BA4 File Offset: 0x00014DA4
		public void FillMesh(Mesh mesh)
		{
			this.InitializeListIfRequired();
			mesh.Clear();
			if (this.m_Positions.Count >= 65000)
			{
				throw new ArgumentException("Mesh can not have more than 65000 vertices");
			}
			mesh.SetVertices(this.m_Positions);
			mesh.SetColors(this.m_Colors);
			mesh.SetUVs(0, this.m_Uv0S);
			mesh.SetUVs(1, this.m_Uv1S);
			mesh.SetUVs(2, this.m_Uv2S);
			mesh.SetUVs(3, this.m_Uv3S);
			mesh.SetNormals(this.m_Normals);
			mesh.SetTangents(this.m_Tangents);
			mesh.SetTriangles(this.m_Indices, 0);
			mesh.RecalculateBounds();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00016C54 File Offset: 0x00014E54
		public void AddVert(Vector3 position, Color32 color, Vector4 uv0, Vector4 uv1, Vector4 uv2, Vector4 uv3, Vector3 normal, Vector4 tangent)
		{
			this.InitializeListIfRequired();
			this.m_Positions.Add(position);
			this.m_Colors.Add(color);
			this.m_Uv0S.Add(uv0);
			this.m_Uv1S.Add(uv1);
			this.m_Uv2S.Add(uv2);
			this.m_Uv3S.Add(uv3);
			this.m_Normals.Add(normal);
			this.m_Tangents.Add(tangent);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00016CCC File Offset: 0x00014ECC
		public void AddVert(Vector3 position, Color32 color, Vector4 uv0, Vector4 uv1, Vector3 normal, Vector4 tangent)
		{
			this.AddVert(position, color, uv0, uv1, Vector4.zero, Vector4.zero, normal, tangent);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00016CF2 File Offset: 0x00014EF2
		public void AddVert(Vector3 position, Color32 color, Vector4 uv0)
		{
			this.AddVert(position, color, uv0, Vector4.zero, VertexHelper.s_DefaultNormal, VertexHelper.s_DefaultTangent);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00016D0C File Offset: 0x00014F0C
		public void AddVert(UIVertex v)
		{
			this.AddVert(v.position, v.color, v.uv0, v.uv1, v.uv2, v.uv3, v.normal, v.tangent);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00016D4F File Offset: 0x00014F4F
		public void AddTriangle(int idx0, int idx1, int idx2)
		{
			this.InitializeListIfRequired();
			this.m_Indices.Add(idx0);
			this.m_Indices.Add(idx1);
			this.m_Indices.Add(idx2);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00016D7C File Offset: 0x00014F7C
		public void AddUIVertexQuad(UIVertex[] verts)
		{
			int startIndex = this.currentVertCount;
			for (int i = 0; i < 4; i++)
			{
				this.AddVert(verts[i].position, verts[i].color, verts[i].uv0, verts[i].uv1, verts[i].normal, verts[i].tangent);
			}
			this.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
			this.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00016E04 File Offset: 0x00015004
		public void AddUIVertexStream(List<UIVertex> verts, List<int> indices)
		{
			this.InitializeListIfRequired();
			if (verts != null)
			{
				CanvasRenderer.AddUIVertexStream(verts, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Uv2S, this.m_Uv3S, this.m_Normals, this.m_Tangents);
			}
			if (indices != null)
			{
				this.m_Indices.AddRange(indices);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00016E60 File Offset: 0x00015060
		public void AddUIVertexTriangleStream(List<UIVertex> verts)
		{
			if (verts == null)
			{
				return;
			}
			this.InitializeListIfRequired();
			CanvasRenderer.SplitUIVertexStreams(verts, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Uv2S, this.m_Uv3S, this.m_Normals, this.m_Tangents, this.m_Indices);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00016EB4 File Offset: 0x000150B4
		public void GetUIVertexStream(List<UIVertex> stream)
		{
			if (stream == null)
			{
				return;
			}
			this.InitializeListIfRequired();
			CanvasRenderer.CreateUIVertexStream(stream, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Uv2S, this.m_Uv3S, this.m_Normals, this.m_Tangents, this.m_Indices);
		}

		// Token: 0x04000262 RID: 610
		private List<Vector3> m_Positions;

		// Token: 0x04000263 RID: 611
		private List<Color32> m_Colors;

		// Token: 0x04000264 RID: 612
		private List<Vector4> m_Uv0S;

		// Token: 0x04000265 RID: 613
		private List<Vector4> m_Uv1S;

		// Token: 0x04000266 RID: 614
		private List<Vector4> m_Uv2S;

		// Token: 0x04000267 RID: 615
		private List<Vector4> m_Uv3S;

		// Token: 0x04000268 RID: 616
		private List<Vector3> m_Normals;

		// Token: 0x04000269 RID: 617
		private List<Vector4> m_Tangents;

		// Token: 0x0400026A RID: 618
		private List<int> m_Indices;

		// Token: 0x0400026B RID: 619
		private static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		// Token: 0x0400026C RID: 620
		private static readonly Vector3 s_DefaultNormal = Vector3.back;

		// Token: 0x0400026D RID: 621
		private bool m_ListsInitalized;
	}
}
