using System;
using UnityEngine.Bindings;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000025 RID: 37
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal struct MeshInfo
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00004904 File Offset: 0x00002B04
		public MeshInfo(int size, VertexDataLayout layout, bool isIMGUI)
		{
			this = default(MeshInfo);
			this.applySDF = true;
			this.vertexDataLayout = layout;
			this.material = null;
			if (isIMGUI)
			{
				size = Mathf.Min(size, 16383);
			}
			int sizeX4 = size * 4;
			int sizeX5 = size * 6;
			this.vertexCount = 0;
			this.vertexBufferSize = sizeX4;
			bool flag = layout == VertexDataLayout.VBO;
			if (flag)
			{
				this.vertexData = new TextCoreVertex[sizeX4];
			}
			else
			{
				this.vertices = new Vector3[sizeX4];
				this.uvs0 = new Vector4[sizeX4];
				this.uvs2 = new Vector2[sizeX4];
				this.colors32 = new Color32[sizeX4];
				this.normals = new Vector3[sizeX4];
				this.tangents = new Vector4[sizeX4];
				this.triangles = new int[sizeX5];
				int indexX6 = 0;
				int indexX7 = 0;
				while (indexX7 / 4 < size)
				{
					for (int i = 0; i < 4; i++)
					{
						this.vertices[indexX7 + i] = Vector3.zero;
						this.uvs0[indexX7 + i] = Vector2.zero;
						this.uvs2[indexX7 + i] = Vector2.zero;
						this.colors32[indexX7 + i] = MeshInfo.k_DefaultColor;
						this.normals[indexX7 + i] = MeshInfo.k_DefaultNormal;
						this.tangents[indexX7 + i] = MeshInfo.k_DefaultTangent;
					}
					this.triangles[indexX6] = indexX7;
					this.triangles[indexX6 + 1] = indexX7 + 1;
					this.triangles[indexX6 + 2] = indexX7 + 2;
					this.triangles[indexX6 + 3] = indexX7 + 2;
					this.triangles[indexX6 + 4] = indexX7 + 3;
					this.triangles[indexX6 + 5] = indexX7;
					indexX7 += 4;
					indexX6 += 6;
				}
			}
			this.material = null;
			this.glyphRenderMode = (GlyphRenderMode)0;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004AF8 File Offset: 0x00002CF8
		internal void ResizeMeshInfo(int size, bool isIMGUI)
		{
			if (isIMGUI)
			{
				size = Mathf.Min(size, 16383);
			}
			int sizeX4 = size * 4;
			int sizeX5 = size * 6;
			this.vertexBufferSize = sizeX4;
			bool flag = this.vertexDataLayout == VertexDataLayout.VBO;
			if (flag)
			{
				Array.Resize<TextCoreVertex>(ref this.vertexData, sizeX4);
			}
			else
			{
				int previousSize = this.vertices.Length / 4;
				Array.Resize<Vector3>(ref this.vertices, sizeX4);
				Array.Resize<Vector4>(ref this.uvs0, sizeX4);
				Array.Resize<Vector2>(ref this.uvs2, sizeX4);
				Array.Resize<Color32>(ref this.colors32, sizeX4);
				Array.Resize<int>(ref this.triangles, sizeX5);
				for (int i = previousSize; i < size; i++)
				{
					int indexX4 = i * 4;
					int indexX5 = i * 6;
					this.triangles[indexX5] = indexX4;
					this.triangles[1 + indexX5] = 1 + indexX4;
					this.triangles[2 + indexX5] = 2 + indexX4;
					this.triangles[3 + indexX5] = 2 + indexX4;
					this.triangles[4 + indexX5] = 3 + indexX4;
					this.triangles[5 + indexX5] = indexX4;
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004C14 File Offset: 0x00002E14
		internal void Clear(bool uploadChanges)
		{
			bool flag = this.vertexDataLayout == VertexDataLayout.VBO;
			if (flag)
			{
				bool flag2 = this.vertexData == null;
				if (flag2)
				{
					return;
				}
				Array.Clear(this.vertexData, 0, this.vertexData.Length);
				this.vertexBufferSize = this.vertexData.Length;
			}
			else
			{
				bool flag3 = this.vertices == null;
				if (flag3)
				{
					return;
				}
				Array.Clear(this.vertices, 0, this.vertices.Length);
				this.vertexBufferSize = this.vertices.Length;
			}
			this.vertexCount = 0;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004CA0 File Offset: 0x00002EA0
		internal void ClearUnusedVertices()
		{
			bool flag = this.vertexDataLayout == VertexDataLayout.VBO;
			if (flag)
			{
				int length = this.vertexData.Length - this.vertexCount;
				bool flag2 = length > 0;
				if (flag2)
				{
					Array.Clear(this.vertexData, this.vertexCount, length);
				}
				this.vertexBufferSize = this.vertexData.Length;
			}
			else
			{
				int length2 = this.vertices.Length - this.vertexCount;
				bool flag3 = length2 > 0;
				if (flag3)
				{
					Array.Clear(this.vertices, this.vertexCount, length2);
				}
				this.vertexBufferSize = this.vertices.Length;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004D38 File Offset: 0x00002F38
		internal void SortGeometry(VertexSortingOrder order)
		{
			if (order != VertexSortingOrder.Normal)
			{
				if (order == VertexSortingOrder.Reverse)
				{
					int size = this.vertexCount / 4;
					for (int i = 0; i < size; i++)
					{
						int src = i * 4;
						int dst = (size - i - 1) * 4;
						bool flag = src < dst;
						if (flag)
						{
							this.SwapVertexData(src, dst);
						}
					}
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004D9C File Offset: 0x00002F9C
		internal void SwapVertexData(int src, int dst)
		{
			bool flag = this.vertexDataLayout == VertexDataLayout.VBO;
			if (flag)
			{
				TextCoreVertex vertex = this.vertexData[dst];
				this.vertexData[dst] = this.vertexData[src];
				this.vertexData[src] = vertex;
				vertex = this.vertexData[dst + 1];
				this.vertexData[dst + 1] = this.vertexData[src + 1];
				this.vertexData[src + 1] = vertex;
				vertex = this.vertexData[dst + 2];
				this.vertexData[dst + 2] = this.vertexData[src + 2];
				this.vertexData[src + 2] = vertex;
				vertex = this.vertexData[dst + 3];
				this.vertexData[dst + 3] = this.vertexData[src + 3];
				this.vertexData[src + 3] = vertex;
			}
			else
			{
				Vector3 vertex2 = this.vertices[dst];
				this.vertices[dst] = this.vertices[src];
				this.vertices[src] = vertex2;
				vertex2 = this.vertices[dst + 1];
				this.vertices[dst + 1] = this.vertices[src + 1];
				this.vertices[src + 1] = vertex2;
				vertex2 = this.vertices[dst + 2];
				this.vertices[dst + 2] = this.vertices[src + 2];
				this.vertices[src + 2] = vertex2;
				vertex2 = this.vertices[dst + 3];
				this.vertices[dst + 3] = this.vertices[src + 3];
				this.vertices[src + 3] = vertex2;
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
		}

		// Token: 0x040000BB RID: 187
		public int vertexCount;

		// Token: 0x040000BC RID: 188
		public TextCoreVertex[] vertexData;

		// Token: 0x040000BD RID: 189
		public Material material;

		// Token: 0x040000BE RID: 190
		[Ignore]
		private static readonly Color32 k_DefaultColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x040000BF RID: 191
		[Ignore]
		private static readonly Vector3 k_DefaultNormal = new Vector3(0f, 0f, -1f);

		// Token: 0x040000C0 RID: 192
		[Ignore]
		private static readonly Vector4 k_DefaultTangent = new Vector4(-1f, 0f, 0f, 1f);

		// Token: 0x040000C1 RID: 193
		[Ignore]
		public Vector3[] vertices;

		// Token: 0x040000C2 RID: 194
		[Ignore]
		public Vector3[] normals;

		// Token: 0x040000C3 RID: 195
		[Ignore]
		public Vector4[] tangents;

		// Token: 0x040000C4 RID: 196
		[Ignore]
		public int vertexBufferSize;

		// Token: 0x040000C5 RID: 197
		[Ignore]
		public Vector4[] uvs0;

		// Token: 0x040000C6 RID: 198
		[Ignore]
		public Vector2[] uvs2;

		// Token: 0x040000C7 RID: 199
		[Ignore]
		public Color32[] colors32;

		// Token: 0x040000C8 RID: 200
		[Ignore]
		public int[] triangles;

		// Token: 0x040000C9 RID: 201
		[Ignore]
		public VertexDataLayout vertexDataLayout;

		// Token: 0x040000CA RID: 202
		[Ignore]
		public bool applySDF;

		// Token: 0x040000CB RID: 203
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal GlyphRenderMode glyphRenderMode;
	}
}
