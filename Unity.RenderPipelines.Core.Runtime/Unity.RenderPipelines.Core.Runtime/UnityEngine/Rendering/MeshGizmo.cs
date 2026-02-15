using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020001DC RID: 476
	internal class MeshGizmo : IDisposable
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x00031F84 File Offset: 0x00030184
		public MeshGizmo(int capacity = 0)
		{
			this.vertices = new List<Vector3>(capacity);
			this.indices = new List<int>(capacity);
			this.colors = new List<Color>(capacity);
			this.mesh = new Mesh
			{
				indexFormat = IndexFormat.UInt32,
				hideFlags = HideFlags.HideAndDontSave
			};
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00031FD5 File Offset: 0x000301D5
		public void Clear()
		{
			this.vertices.Clear();
			this.indices.Clear();
			this.colors.Clear();
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00031FF8 File Offset: 0x000301F8
		public void AddWireCube(Vector3 center, Vector3 size, Color color)
		{
			MeshGizmo.<>c__DisplayClass10_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.color = color;
			Vector3 halfSize = size / 2f;
			Vector3 p0 = new Vector3(halfSize.x, halfSize.y, halfSize.z);
			Vector3 p = new Vector3(-halfSize.x, halfSize.y, halfSize.z);
			Vector3 p2 = new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
			Vector3 p3 = new Vector3(halfSize.x, -halfSize.y, halfSize.z);
			Vector3 p4 = new Vector3(halfSize.x, halfSize.y, -halfSize.z);
			Vector3 p5 = new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
			Vector3 p6 = new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
			Vector3 p7 = new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
			this.<AddWireCube>g__AddEdge|10_0(center + p0, center + p, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p, center + p2, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p2, center + p3, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p3, center + p0, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p4, center + p5, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p5, center + p6, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p6, center + p7, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p7, center + p4, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p0, center + p4, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p, center + p5, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p2, center + p6, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + p3, center + p7, ref CS$<>8__locals1);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00032210 File Offset: 0x00030410
		private void DrawMesh(Matrix4x4 trs, Material mat, MeshTopology topology, CompareFunction depthTest, string gizmoName)
		{
			this.mesh.Clear();
			this.mesh.SetVertices(this.vertices);
			this.mesh.SetColors(this.colors);
			this.mesh.SetIndices(this.indices, topology, 0, true, 0);
			mat.SetFloat("_HandleZTest", (float)depthTest);
			CommandBuffer commandBuffer = CommandBufferPool.Get(gizmoName ?? "Mesh Gizmo Rendering");
			commandBuffer.DrawMesh(this.mesh, trs, mat, 0, 0);
			Graphics.ExecuteCommandBuffer(commandBuffer);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00032292 File Offset: 0x00030492
		public void RenderWireframe(Matrix4x4 trs, CompareFunction depthTest = CompareFunction.LessEqual, string gizmoName = null)
		{
			this.DrawMesh(trs, this.wireMaterial, MeshTopology.Lines, depthTest, gizmoName);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000322A4 File Offset: 0x000304A4
		public void Dispose()
		{
			CoreUtils.Destroy(this.mesh);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000322BC File Offset: 0x000304BC
		[CompilerGenerated]
		private void <AddWireCube>g__AddEdge|10_0(Vector3 p1, Vector3 p2, ref MeshGizmo.<>c__DisplayClass10_0 A_3)
		{
			this.vertices.Add(p1);
			this.vertices.Add(p2);
			this.indices.Add(this.indices.Count);
			this.indices.Add(this.indices.Count);
			this.colors.Add(A_3.color);
			this.colors.Add(A_3.color);
		}

		// Token: 0x04000915 RID: 2325
		public static readonly int vertexCountPerCube = 24;

		// Token: 0x04000916 RID: 2326
		public Mesh mesh;

		// Token: 0x04000917 RID: 2327
		private List<Vector3> vertices;

		// Token: 0x04000918 RID: 2328
		private List<int> indices;

		// Token: 0x04000919 RID: 2329
		private List<Color> colors;

		// Token: 0x0400091A RID: 2330
		private Material wireMaterial;

		// Token: 0x0400091B RID: 2331
		private Material dottedWireMaterial;

		// Token: 0x0400091C RID: 2332
		private Material solidMaterial;
	}
}
