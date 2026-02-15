using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000023 RID: 35
	public interface IRasterCommandBuffer : IBaseCommandBuffer
	{
		// Token: 0x06000215 RID: 533
		void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor);

		// Token: 0x06000216 RID: 534
		void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth);

		// Token: 0x06000217 RID: 535
		void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth, uint stencil);

		// Token: 0x06000218 RID: 536
		void ClearRenderTarget(RTClearFlags clearFlags, Color backgroundColor, float depth, uint stencil);

		// Token: 0x06000219 RID: 537
		void ClearRenderTarget(RTClearFlags clearFlags, Color[] backgroundColors, float depth, uint stencil);

		// Token: 0x0600021A RID: 538
		void SetInstanceMultiplier(uint multiplier);

		// Token: 0x0600021B RID: 539
		void SetFoveatedRenderingMode(FoveatedRenderingMode foveatedRenderingMode);

		// Token: 0x0600021C RID: 540
		void SetWireframe(bool enable);

		// Token: 0x0600021D RID: 541
		void ConfigureFoveatedRendering(IntPtr platformData);

		// Token: 0x0600021E RID: 542
		void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties);

		// Token: 0x0600021F RID: 543
		void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass);

		// Token: 0x06000220 RID: 544
		void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex);

		// Token: 0x06000221 RID: 545
		void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material);

		// Token: 0x06000222 RID: 546
		void DrawMultipleMeshes(Matrix4x4[] matrices, Mesh[] meshes, int[] subsetIndices, int count, Material material, int shaderPass, MaterialPropertyBlock properties);

		// Token: 0x06000223 RID: 547
		void DrawRenderer(Renderer renderer, Material material, int submeshIndex, int shaderPass);

		// Token: 0x06000224 RID: 548
		void DrawRenderer(Renderer renderer, Material material, int submeshIndex);

		// Token: 0x06000225 RID: 549
		void DrawRenderer(Renderer renderer, Material material);

		// Token: 0x06000226 RID: 550
		void DrawRendererList(RendererList rendererList);

		// Token: 0x06000227 RID: 551
		void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, MaterialPropertyBlock properties);

		// Token: 0x06000228 RID: 552
		void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount);

		// Token: 0x06000229 RID: 553
		void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount);

		// Token: 0x0600022A RID: 554
		void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties);

		// Token: 0x0600022B RID: 555
		void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount);

		// Token: 0x0600022C RID: 556
		void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount);

		// Token: 0x0600022D RID: 557
		void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x0600022E RID: 558
		void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset);

		// Token: 0x0600022F RID: 559
		void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs);

		// Token: 0x06000230 RID: 560
		void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06000231 RID: 561
		void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset);

		// Token: 0x06000232 RID: 562
		void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs);

		// Token: 0x06000233 RID: 563
		void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06000234 RID: 564
		void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset);

		// Token: 0x06000235 RID: 565
		void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs);

		// Token: 0x06000236 RID: 566
		void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06000237 RID: 567
		void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset);

		// Token: 0x06000238 RID: 568
		void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs);

		// Token: 0x06000239 RID: 569
		void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties);

		// Token: 0x0600023A RID: 570
		void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count);

		// Token: 0x0600023B RID: 571
		void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices);

		// Token: 0x0600023C RID: 572
		void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties);

		// Token: 0x0600023D RID: 573
		void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x0600023E RID: 574
		void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset);

		// Token: 0x0600023F RID: 575
		void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs);

		// Token: 0x06000240 RID: 576
		void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

		// Token: 0x06000241 RID: 577
		void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset);

		// Token: 0x06000242 RID: 578
		void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs);

		// Token: 0x06000243 RID: 579
		void DrawOcclusionMesh(RectInt normalizedCamViewport);
	}
}
