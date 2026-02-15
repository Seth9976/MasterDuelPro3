using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000010 RID: 16
	internal class XROcclusionMesh
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000034EC File Offset: 0x000016EC
		internal XROcclusionMesh(XRPass xrPass)
		{
			this.m_Pass = xrPass;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000034FB File Offset: 0x000016FB
		internal void SetMaterial(Material mat)
		{
			this.m_Material = mat;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003504 File Offset: 0x00001704
		internal bool hasValidOcclusionMesh
		{
			get
			{
				if (!this.IsOcclusionMeshSupported())
				{
					return false;
				}
				if (this.m_Pass.singlePassEnabled)
				{
					return this.m_CombinedMesh != null;
				}
				return this.m_Pass.GetOcclusionMesh(0) != null;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000353C File Offset: 0x0000173C
		internal void RenderOcclusionMesh(CommandBuffer cmd, float occlusionMeshScale, bool yFlip = false)
		{
			if (this.IsOcclusionMeshSupported())
			{
				using (new ProfilingScope(cmd, XROcclusionMesh.k_OcclusionMeshProfilingSampler))
				{
					if (this.m_Pass.singlePassEnabled)
					{
						if (this.m_CombinedMesh != null && SystemInfo.supportsMultiview)
						{
							cmd.EnableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
							Vector3 scale = new Vector3(occlusionMeshScale, yFlip ? occlusionMeshScale : (-occlusionMeshScale), 1f);
							cmd.DrawMesh(this.m_CombinedMesh, Matrix4x4.Scale(scale), this.m_Material);
							cmd.DisableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
						}
						else if (this.m_CombinedMesh != null && SystemInfo.supportsRenderTargetArrayIndexFromVertexShader)
						{
							this.m_Pass.StopSinglePass(cmd);
							cmd.EnableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
							Vector3 scale2 = new Vector3(occlusionMeshScale, yFlip ? occlusionMeshScale : (-occlusionMeshScale), 1f);
							cmd.DrawMesh(this.m_CombinedMesh, Matrix4x4.Scale(scale2), this.m_Material);
							cmd.DisableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
							this.m_Pass.StartSinglePass(cmd);
						}
					}
					else
					{
						Mesh mesh = this.m_Pass.GetOcclusionMesh(0);
						if (mesh != null)
						{
							cmd.DrawMesh(mesh, Matrix4x4.identity, this.m_Material);
						}
					}
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000369C File Offset: 0x0000189C
		internal void UpdateCombinedMesh()
		{
			int hashCode;
			if (this.IsOcclusionMeshSupported() && this.m_Pass.singlePassEnabled && this.TryGetOcclusionMeshCombinedHashCode(out hashCode))
			{
				if (this.m_CombinedMesh == null || hashCode != this.m_CombinedMeshHashCode)
				{
					this.CreateOcclusionMeshCombined();
					this.m_CombinedMeshHashCode = hashCode;
					return;
				}
			}
			else
			{
				this.m_CombinedMesh = null;
				this.m_CombinedMeshHashCode = 0;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000036FB File Offset: 0x000018FB
		private bool IsOcclusionMeshSupported()
		{
			return this.m_Pass.enabled && this.m_Material != null;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003718 File Offset: 0x00001918
		private bool TryGetOcclusionMeshCombinedHashCode(out int hashCode)
		{
			hashCode = 17;
			for (int viewId = 0; viewId < this.m_Pass.viewCount; viewId++)
			{
				Mesh mesh = this.m_Pass.GetOcclusionMesh(viewId);
				if (!(mesh != null))
				{
					hashCode = 0;
					return false;
				}
				hashCode = hashCode * 23 + mesh.GetHashCode();
			}
			return true;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000376C File Offset: 0x0000196C
		private void CreateOcclusionMeshCombined()
		{
			CoreUtils.Destroy(this.m_CombinedMesh);
			this.m_CombinedMesh = new Mesh();
			this.m_CombinedMesh.indexFormat = IndexFormat.UInt16;
			int combinedVertexCount = 0;
			uint combinedIndexCount = 0U;
			for (int viewId = 0; viewId < this.m_Pass.viewCount; viewId++)
			{
				Mesh mesh = this.m_Pass.GetOcclusionMesh(viewId);
				combinedVertexCount += mesh.vertexCount;
				combinedIndexCount += mesh.GetIndexCount(0);
			}
			Vector3[] vertices = new Vector3[combinedVertexCount];
			ushort[] indices = new ushort[combinedIndexCount];
			int vertexStart = 0;
			int indexStart = 0;
			for (int viewId2 = 0; viewId2 < this.m_Pass.viewCount; viewId2++)
			{
				Mesh mesh2 = this.m_Pass.GetOcclusionMesh(viewId2);
				int[] meshIndices = mesh2.GetIndices(0);
				mesh2.vertices.CopyTo(vertices, vertexStart);
				for (int i = 0; i < mesh2.vertices.Length; i++)
				{
					vertices[vertexStart + i].z = (float)viewId2;
				}
				for (int j = 0; j < meshIndices.Length; j++)
				{
					int newIndex = vertexStart + meshIndices[j];
					indices[indexStart + j] = (ushort)newIndex;
				}
				vertexStart += mesh2.vertexCount;
				indexStart += meshIndices.Length;
			}
			this.m_CombinedMesh.vertices = vertices;
			this.m_CombinedMesh.SetIndices(indices, MeshTopology.Triangles, 0, true, 0);
		}

		// Token: 0x04000043 RID: 67
		private XRPass m_Pass;

		// Token: 0x04000044 RID: 68
		private Mesh m_CombinedMesh;

		// Token: 0x04000045 RID: 69
		private Material m_Material;

		// Token: 0x04000046 RID: 70
		private int m_CombinedMeshHashCode;

		// Token: 0x04000047 RID: 71
		private static readonly ProfilingSampler k_OcclusionMeshProfilingSampler = new ProfilingSampler("XR Occlusion Mesh");
	}
}
