using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000066 RID: 102
	public struct TMP_MeshInfo
	{
		// Token: 0x06000330 RID: 816 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		public TMP_MeshInfo(Mesh mesh, int size)
		{
			if (mesh == null)
			{
				mesh = new Mesh();
			}
			else
			{
				mesh.Clear();
			}
			this.mesh = mesh;
			size = Mathf.Min(size, 16383);
			int sizeX4 = size * 4;
			int sizeX5 = size * 6;
			this.vertexCount = 0;
			this.vertices = new Vector3[sizeX4];
			this.uvs0 = new Vector4[sizeX4];
			this.uvs2 = new Vector2[sizeX4];
			this.colors32 = new Color32[sizeX4];
			this.normals = new Vector3[sizeX4];
			this.tangents = new Vector4[sizeX4];
			this.triangles = new int[sizeX5];
			int index_X6 = 0;
			int index_X7 = 0;
			while (index_X7 / 4 < size)
			{
				for (int i = 0; i < 4; i++)
				{
					this.vertices[index_X7 + i] = Vector3.zero;
					this.uvs0[index_X7 + i] = Vector2.zero;
					this.uvs2[index_X7 + i] = Vector2.zero;
					this.colors32[index_X7 + i] = TMP_MeshInfo.s_DefaultColor;
					this.normals[index_X7 + i] = TMP_MeshInfo.s_DefaultNormal;
					this.tangents[index_X7 + i] = TMP_MeshInfo.s_DefaultTangent;
				}
				this.triangles[index_X6] = index_X7;
				this.triangles[index_X6 + 1] = index_X7 + 1;
				this.triangles[index_X6 + 2] = index_X7 + 2;
				this.triangles[index_X6 + 3] = index_X7 + 2;
				this.triangles[index_X6 + 4] = index_X7 + 3;
				this.triangles[index_X6 + 5] = index_X7;
				index_X7 += 4;
				index_X6 += 6;
			}
			this.mesh.vertices = this.vertices;
			this.mesh.normals = this.normals;
			this.mesh.tangents = this.tangents;
			this.mesh.triangles = this.triangles;
			this.mesh.bounds = TMP_MeshInfo.s_DefaultBounds;
			this.material = null;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00010D9C File Offset: 0x0000EF9C
		public TMP_MeshInfo(Mesh mesh, int size, bool isVolumetric)
		{
			if (mesh == null)
			{
				mesh = new Mesh();
			}
			else
			{
				mesh.Clear();
			}
			this.mesh = mesh;
			int s0 = ((!isVolumetric) ? 4 : 8);
			int s = ((!isVolumetric) ? 6 : 36);
			size = Mathf.Min(size, 65532 / s0);
			int size_x_s0 = size * s0;
			int size_x_s = size * s;
			this.vertexCount = 0;
			this.vertices = new Vector3[size_x_s0];
			this.uvs0 = new Vector4[size_x_s0];
			this.uvs2 = new Vector2[size_x_s0];
			this.colors32 = new Color32[size_x_s0];
			this.normals = new Vector3[size_x_s0];
			this.tangents = new Vector4[size_x_s0];
			this.triangles = new int[size_x_s];
			int index_x_s0 = 0;
			int index_x_s = 0;
			while (index_x_s0 / s0 < size)
			{
				for (int i = 0; i < s0; i++)
				{
					this.vertices[index_x_s0 + i] = Vector3.zero;
					this.uvs0[index_x_s0 + i] = Vector2.zero;
					this.uvs2[index_x_s0 + i] = Vector2.zero;
					this.colors32[index_x_s0 + i] = TMP_MeshInfo.s_DefaultColor;
					this.normals[index_x_s0 + i] = TMP_MeshInfo.s_DefaultNormal;
					this.tangents[index_x_s0 + i] = TMP_MeshInfo.s_DefaultTangent;
				}
				this.triangles[index_x_s] = index_x_s0;
				this.triangles[index_x_s + 1] = index_x_s0 + 1;
				this.triangles[index_x_s + 2] = index_x_s0 + 2;
				this.triangles[index_x_s + 3] = index_x_s0 + 2;
				this.triangles[index_x_s + 4] = index_x_s0 + 3;
				this.triangles[index_x_s + 5] = index_x_s0;
				if (isVolumetric)
				{
					this.triangles[index_x_s + 6] = index_x_s0 + 4;
					this.triangles[index_x_s + 7] = index_x_s0 + 5;
					this.triangles[index_x_s + 8] = index_x_s0 + 1;
					this.triangles[index_x_s + 9] = index_x_s0 + 1;
					this.triangles[index_x_s + 10] = index_x_s0;
					this.triangles[index_x_s + 11] = index_x_s0 + 4;
					this.triangles[index_x_s + 12] = index_x_s0 + 3;
					this.triangles[index_x_s + 13] = index_x_s0 + 2;
					this.triangles[index_x_s + 14] = index_x_s0 + 6;
					this.triangles[index_x_s + 15] = index_x_s0 + 6;
					this.triangles[index_x_s + 16] = index_x_s0 + 7;
					this.triangles[index_x_s + 17] = index_x_s0 + 3;
					this.triangles[index_x_s + 18] = index_x_s0 + 1;
					this.triangles[index_x_s + 19] = index_x_s0 + 5;
					this.triangles[index_x_s + 20] = index_x_s0 + 6;
					this.triangles[index_x_s + 21] = index_x_s0 + 6;
					this.triangles[index_x_s + 22] = index_x_s0 + 2;
					this.triangles[index_x_s + 23] = index_x_s0 + 1;
					this.triangles[index_x_s + 24] = index_x_s0 + 4;
					this.triangles[index_x_s + 25] = index_x_s0;
					this.triangles[index_x_s + 26] = index_x_s0 + 3;
					this.triangles[index_x_s + 27] = index_x_s0 + 3;
					this.triangles[index_x_s + 28] = index_x_s0 + 7;
					this.triangles[index_x_s + 29] = index_x_s0 + 4;
					this.triangles[index_x_s + 30] = index_x_s0 + 7;
					this.triangles[index_x_s + 31] = index_x_s0 + 6;
					this.triangles[index_x_s + 32] = index_x_s0 + 5;
					this.triangles[index_x_s + 33] = index_x_s0 + 5;
					this.triangles[index_x_s + 34] = index_x_s0 + 4;
					this.triangles[index_x_s + 35] = index_x_s0 + 7;
				}
				index_x_s0 += s0;
				index_x_s += s;
			}
			this.mesh.vertices = this.vertices;
			this.mesh.normals = this.normals;
			this.mesh.tangents = this.tangents;
			this.mesh.triangles = this.triangles;
			this.mesh.bounds = TMP_MeshInfo.s_DefaultBounds;
			this.material = null;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00011194 File Offset: 0x0000F394
		public void ResizeMeshInfo(int size)
		{
			size = Mathf.Min(size, 16383);
			int size_X4 = size * 4;
			int size_X5 = size * 6;
			int previousSize = this.vertices.Length / 4;
			Array.Resize<Vector3>(ref this.vertices, size_X4);
			Array.Resize<Vector3>(ref this.normals, size_X4);
			Array.Resize<Vector4>(ref this.tangents, size_X4);
			Array.Resize<Vector4>(ref this.uvs0, size_X4);
			Array.Resize<Vector2>(ref this.uvs2, size_X4);
			Array.Resize<Color32>(ref this.colors32, size_X4);
			Array.Resize<int>(ref this.triangles, size_X5);
			if (size <= previousSize)
			{
				this.mesh.triangles = this.triangles;
				this.mesh.vertices = this.vertices;
				this.mesh.normals = this.normals;
				this.mesh.tangents = this.tangents;
				return;
			}
			for (int i = previousSize; i < size; i++)
			{
				int index_X4 = i * 4;
				int index_X5 = i * 6;
				this.normals[index_X4] = TMP_MeshInfo.s_DefaultNormal;
				this.normals[1 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
				this.normals[2 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
				this.normals[3 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
				this.tangents[index_X4] = TMP_MeshInfo.s_DefaultTangent;
				this.tangents[1 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
				this.tangents[2 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
				this.tangents[3 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
				this.triangles[index_X5] = index_X4;
				this.triangles[1 + index_X5] = 1 + index_X4;
				this.triangles[2 + index_X5] = 2 + index_X4;
				this.triangles[3 + index_X5] = 2 + index_X4;
				this.triangles[4 + index_X5] = 3 + index_X4;
				this.triangles[5 + index_X5] = index_X4;
			}
			this.mesh.vertices = this.vertices;
			this.mesh.normals = this.normals;
			this.mesh.tangents = this.tangents;
			this.mesh.triangles = this.triangles;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000113B0 File Offset: 0x0000F5B0
		public void ResizeMeshInfo(int size, bool isVolumetric)
		{
			int s0 = ((!isVolumetric) ? 4 : 8);
			int s = ((!isVolumetric) ? 6 : 36);
			size = Mathf.Min(size, 65532 / s0);
			int size_X4 = size * s0;
			int size_X5 = size * s;
			int previousSize = this.vertices.Length / s0;
			Array.Resize<Vector3>(ref this.vertices, size_X4);
			Array.Resize<Vector3>(ref this.normals, size_X4);
			Array.Resize<Vector4>(ref this.tangents, size_X4);
			Array.Resize<Vector4>(ref this.uvs0, size_X4);
			Array.Resize<Vector2>(ref this.uvs2, size_X4);
			Array.Resize<Color32>(ref this.colors32, size_X4);
			Array.Resize<int>(ref this.triangles, size_X5);
			if (size <= previousSize)
			{
				this.mesh.triangles = this.triangles;
				this.mesh.vertices = this.vertices;
				this.mesh.normals = this.normals;
				this.mesh.tangents = this.tangents;
				return;
			}
			for (int i = previousSize; i < size; i++)
			{
				int index_X4 = i * s0;
				int index_X5 = i * s;
				this.normals[index_X4] = TMP_MeshInfo.s_DefaultNormal;
				this.normals[1 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
				this.normals[2 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
				this.normals[3 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
				this.tangents[index_X4] = TMP_MeshInfo.s_DefaultTangent;
				this.tangents[1 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
				this.tangents[2 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
				this.tangents[3 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
				if (isVolumetric)
				{
					this.normals[4 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
					this.normals[5 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
					this.normals[6 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
					this.normals[7 + index_X4] = TMP_MeshInfo.s_DefaultNormal;
					this.tangents[4 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
					this.tangents[5 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
					this.tangents[6 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
					this.tangents[7 + index_X4] = TMP_MeshInfo.s_DefaultTangent;
				}
				this.triangles[index_X5] = index_X4;
				this.triangles[1 + index_X5] = 1 + index_X4;
				this.triangles[2 + index_X5] = 2 + index_X4;
				this.triangles[3 + index_X5] = 2 + index_X4;
				this.triangles[4 + index_X5] = 3 + index_X4;
				this.triangles[5 + index_X5] = index_X4;
				if (isVolumetric)
				{
					this.triangles[index_X5 + 6] = index_X4 + 4;
					this.triangles[index_X5 + 7] = index_X4 + 5;
					this.triangles[index_X5 + 8] = index_X4 + 1;
					this.triangles[index_X5 + 9] = index_X4 + 1;
					this.triangles[index_X5 + 10] = index_X4;
					this.triangles[index_X5 + 11] = index_X4 + 4;
					this.triangles[index_X5 + 12] = index_X4 + 3;
					this.triangles[index_X5 + 13] = index_X4 + 2;
					this.triangles[index_X5 + 14] = index_X4 + 6;
					this.triangles[index_X5 + 15] = index_X4 + 6;
					this.triangles[index_X5 + 16] = index_X4 + 7;
					this.triangles[index_X5 + 17] = index_X4 + 3;
					this.triangles[index_X5 + 18] = index_X4 + 1;
					this.triangles[index_X5 + 19] = index_X4 + 5;
					this.triangles[index_X5 + 20] = index_X4 + 6;
					this.triangles[index_X5 + 21] = index_X4 + 6;
					this.triangles[index_X5 + 22] = index_X4 + 2;
					this.triangles[index_X5 + 23] = index_X4 + 1;
					this.triangles[index_X5 + 24] = index_X4 + 4;
					this.triangles[index_X5 + 25] = index_X4;
					this.triangles[index_X5 + 26] = index_X4 + 3;
					this.triangles[index_X5 + 27] = index_X4 + 3;
					this.triangles[index_X5 + 28] = index_X4 + 7;
					this.triangles[index_X5 + 29] = index_X4 + 4;
					this.triangles[index_X5 + 30] = index_X4 + 7;
					this.triangles[index_X5 + 31] = index_X4 + 6;
					this.triangles[index_X5 + 32] = index_X4 + 5;
					this.triangles[index_X5 + 33] = index_X4 + 5;
					this.triangles[index_X5 + 34] = index_X4 + 4;
					this.triangles[index_X5 + 35] = index_X4 + 7;
				}
			}
			this.mesh.vertices = this.vertices;
			this.mesh.normals = this.normals;
			this.mesh.tangents = this.tangents;
			this.mesh.triangles = this.triangles;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001186C File Offset: 0x0000FA6C
		public void Clear()
		{
			if (this.vertices == null)
			{
				return;
			}
			Array.Clear(this.vertices, 0, this.vertices.Length);
			this.vertexCount = 0;
			if (this.mesh != null)
			{
				this.mesh.vertices = this.vertices;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000118BC File Offset: 0x0000FABC
		public void Clear(bool uploadChanges)
		{
			if (this.vertices == null)
			{
				return;
			}
			Array.Clear(this.vertices, 0, this.vertices.Length);
			this.vertexCount = 0;
			if (uploadChanges && this.mesh != null)
			{
				this.mesh.vertices = this.vertices;
			}
			if (this.mesh != null)
			{
				this.mesh.bounds = TMP_MeshInfo.s_DefaultBounds;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00011930 File Offset: 0x0000FB30
		public void ClearUnusedVertices()
		{
			int length = this.vertices.Length - this.vertexCount;
			if (length > 0)
			{
				Array.Clear(this.vertices, this.vertexCount, length);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00011964 File Offset: 0x0000FB64
		public void ClearUnusedVertices(int startIndex)
		{
			int length = this.vertices.Length - startIndex;
			if (length > 0)
			{
				Array.Clear(this.vertices, startIndex, length);
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00011990 File Offset: 0x0000FB90
		public void ClearUnusedVertices(int startIndex, bool updateMesh)
		{
			int length = this.vertices.Length - startIndex;
			if (length > 0)
			{
				Array.Clear(this.vertices, startIndex, length);
			}
			if (updateMesh && this.mesh != null)
			{
				this.mesh.vertices = this.vertices;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000119DC File Offset: 0x0000FBDC
		public void SortGeometry(VertexSortingOrder order)
		{
			if (order != VertexSortingOrder.Normal && order == VertexSortingOrder.Reverse)
			{
				int size = this.vertexCount / 4;
				for (int i = 0; i < size; i++)
				{
					int src = i * 4;
					int dst = (size - i - 1) * 4;
					if (src < dst)
					{
						this.SwapVertexData(src, dst);
					}
				}
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00011A20 File Offset: 0x0000FC20
		public void SortGeometry(IList<int> sortingOrder)
		{
			int indexCount = sortingOrder.Count;
			if (indexCount * 4 > this.vertices.Length)
			{
				return;
			}
			for (int dst_index = 0; dst_index < indexCount; dst_index++)
			{
				int src_index;
				for (src_index = sortingOrder[dst_index]; src_index < dst_index; src_index = sortingOrder[src_index])
				{
				}
				if (src_index != dst_index)
				{
					this.SwapVertexData(src_index * 4, dst_index * 4);
				}
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00011A74 File Offset: 0x0000FC74
		public void SwapVertexData(int src, int dst)
		{
			Vector3 vertex = this.vertices[dst];
			this.vertices[dst] = this.vertices[src];
			this.vertices[src] = vertex;
			vertex = this.vertices[dst + 1];
			this.vertices[dst + 1] = this.vertices[src + 1];
			this.vertices[src + 1] = vertex;
			vertex = this.vertices[dst + 2];
			this.vertices[dst + 2] = this.vertices[src + 2];
			this.vertices[src + 2] = vertex;
			vertex = this.vertices[dst + 3];
			this.vertices[dst + 3] = this.vertices[src + 3];
			this.vertices[src + 3] = vertex;
			Vector4 uvs = this.uvs0[dst];
			this.uvs0[dst] = this.uvs0[src];
			this.uvs0[src] = uvs;
			uvs = this.uvs0[dst + 1];
			this.uvs0[dst + 1] = this.uvs0[src + 1];
			this.uvs0[src + 1] = uvs;
			uvs = this.uvs0[dst + 2];
			this.uvs0[dst + 2] = this.uvs0[src + 2];
			this.uvs0[src + 2] = uvs;
			uvs = this.uvs0[dst + 3];
			this.uvs0[dst + 3] = this.uvs0[src + 3];
			this.uvs0[src + 3] = uvs;
			uvs = this.uvs2[dst];
			this.uvs2[dst] = this.uvs2[src];
			this.uvs2[src] = uvs;
			uvs = this.uvs2[dst + 1];
			this.uvs2[dst + 1] = this.uvs2[src + 1];
			this.uvs2[src + 1] = uvs;
			uvs = this.uvs2[dst + 2];
			this.uvs2[dst + 2] = this.uvs2[src + 2];
			this.uvs2[src + 2] = uvs;
			uvs = this.uvs2[dst + 3];
			this.uvs2[dst + 3] = this.uvs2[src + 3];
			this.uvs2[src + 3] = uvs;
			Color32 color = this.colors32[dst];
			this.colors32[dst] = this.colors32[src];
			this.colors32[src] = color;
			color = this.colors32[dst + 1];
			this.colors32[dst + 1] = this.colors32[src + 1];
			this.colors32[src + 1] = color;
			color = this.colors32[dst + 2];
			this.colors32[dst + 2] = this.colors32[src + 2];
			this.colors32[src + 2] = color;
			color = this.colors32[dst + 3];
			this.colors32[dst + 3] = this.colors32[src + 3];
			this.colors32[src + 3] = color;
		}

		// Token: 0x04000241 RID: 577
		private static readonly Color32 s_DefaultColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x04000242 RID: 578
		private static readonly Vector3 s_DefaultNormal = new Vector3(0f, 0f, -1f);

		// Token: 0x04000243 RID: 579
		private static readonly Vector4 s_DefaultTangent = new Vector4(-1f, 0f, 0f, 1f);

		// Token: 0x04000244 RID: 580
		private static readonly Bounds s_DefaultBounds = default(Bounds);

		// Token: 0x04000245 RID: 581
		public Mesh mesh;

		// Token: 0x04000246 RID: 582
		public int vertexCount;

		// Token: 0x04000247 RID: 583
		public Vector3[] vertices;

		// Token: 0x04000248 RID: 584
		public Vector3[] normals;

		// Token: 0x04000249 RID: 585
		public Vector4[] tangents;

		// Token: 0x0400024A RID: 586
		public Vector4[] uvs0;

		// Token: 0x0400024B RID: 587
		public Vector2[] uvs2;

		// Token: 0x0400024C RID: 588
		public Color32[] colors32;

		// Token: 0x0400024D RID: 589
		public int[] triangles;

		// Token: 0x0400024E RID: 590
		public Material material;
	}
}
