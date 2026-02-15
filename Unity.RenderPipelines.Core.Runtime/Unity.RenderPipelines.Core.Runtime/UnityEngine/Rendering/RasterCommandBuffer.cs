using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Profiling;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x02000025 RID: 37
	public class RasterCommandBuffer : BaseCommandBuffer, IRasterCommandBuffer, IBaseCommandBuffer
	{
		// Token: 0x06000254 RID: 596 RVA: 0x000054B7 File Offset: 0x000036B7
		internal RasterCommandBuffer(CommandBuffer wrapped, RenderGraphPass executingPass, bool isAsync)
			: base(wrapped, executingPass, isAsync)
		{
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000054C2 File Offset: 0x000036C2
		public void SetInvertCulling(bool invertCulling)
		{
			this.m_WrappedCommandBuffer.SetInvertCulling(invertCulling);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00005530 File Offset: 0x00003730
		public void SetViewport(Rect pixelRect)
		{
			this.m_WrappedCommandBuffer.SetViewport(pixelRect);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000553E File Offset: 0x0000373E
		public void EnableScissorRect(Rect scissor)
		{
			this.m_WrappedCommandBuffer.EnableScissorRect(scissor);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000554C File Offset: 0x0000374C
		public void DisableScissorRect()
		{
			this.m_WrappedCommandBuffer.DisableScissorRect();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00005F4D File Offset: 0x0000414D
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearDepth, clearColor, backgroundColor);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00005F5D File Offset: 0x0000415D
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00005F6F File Offset: 0x0000416F
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth, uint stencil)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearDepth, clearColor, backgroundColor, depth, stencil);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00005F83 File Offset: 0x00004183
		public void ClearRenderTarget(RTClearFlags clearFlags, Color backgroundColor, float depth, uint stencil)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearFlags, backgroundColor, depth, stencil);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00005F95 File Offset: 0x00004195
		public void ClearRenderTarget(RTClearFlags clearFlags, Color[] backgroundColors, float depth, uint stencil)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearFlags, backgroundColors, depth, stencil);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00005559 File Offset: 0x00003759
		public void SetGlobalFloat(int nameID, float value)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloat(nameID, value);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00005568 File Offset: 0x00003768
		public void SetGlobalInt(int nameID, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInt(nameID, value);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00005577 File Offset: 0x00003777
		public void SetGlobalInteger(int nameID, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInteger(nameID, value);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00005586 File Offset: 0x00003786
		public void SetGlobalVector(int nameID, Vector4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalVector(nameID, value);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00005595 File Offset: 0x00003795
		public void SetGlobalColor(int nameID, Color value)
		{
			this.m_WrappedCommandBuffer.SetGlobalColor(nameID, value);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000055A4 File Offset: 0x000037A4
		public void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrix(nameID, value);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000055B3 File Offset: 0x000037B3
		public void EnableShaderKeyword(string keyword)
		{
			this.m_WrappedCommandBuffer.EnableShaderKeyword(keyword);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000055C1 File Offset: 0x000037C1
		public void EnableKeyword(in GlobalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.EnableKeyword(in keyword);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000055CF File Offset: 0x000037CF
		public void EnableKeyword(Material material, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.EnableKeyword(material, in keyword);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000055DE File Offset: 0x000037DE
		public void EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.EnableKeyword(computeShader, in keyword);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000055ED File Offset: 0x000037ED
		public void DisableShaderKeyword(string keyword)
		{
			this.m_WrappedCommandBuffer.DisableShaderKeyword(keyword);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000055FB File Offset: 0x000037FB
		public void DisableKeyword(in GlobalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.DisableKeyword(in keyword);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00005609 File Offset: 0x00003809
		public void DisableKeyword(Material material, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.DisableKeyword(material, in keyword);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00005618 File Offset: 0x00003818
		public void DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.DisableKeyword(computeShader, in keyword);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00005627 File Offset: 0x00003827
		public void SetKeyword(in GlobalKeyword keyword, bool value)
		{
			this.m_WrappedCommandBuffer.SetKeyword(in keyword, value);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00005636 File Offset: 0x00003836
		public void SetKeyword(Material material, in LocalKeyword keyword, bool value)
		{
			this.m_WrappedCommandBuffer.SetKeyword(material, in keyword, value);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00005646 File Offset: 0x00003846
		public void SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value)
		{
			this.m_WrappedCommandBuffer.SetKeyword(computeShader, in keyword, value);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00005656 File Offset: 0x00003856
		public void SetViewProjectionMatrices(Matrix4x4 view, Matrix4x4 proj)
		{
			this.m_WrappedCommandBuffer.SetViewProjectionMatrices(view, proj);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00005665 File Offset: 0x00003865
		public void SetGlobalDepthBias(float bias, float slopeBias)
		{
			this.m_WrappedCommandBuffer.SetGlobalDepthBias(bias, slopeBias);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00005674 File Offset: 0x00003874
		public void SetGlobalFloatArray(int nameID, float[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(nameID, values);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00005683 File Offset: 0x00003883
		public void SetGlobalVectorArray(int nameID, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(nameID, values);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00005692 File Offset: 0x00003892
		public void SetGlobalMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(nameID, values);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000056A1 File Offset: 0x000038A1
		public void SetLateLatchProjectionMatrices(Matrix4x4[] projectionMat)
		{
			this.m_WrappedCommandBuffer.SetLateLatchProjectionMatrices(projectionMat);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000056AF File Offset: 0x000038AF
		public void MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID)
		{
			this.m_WrappedCommandBuffer.MarkLateLatchMatrixShaderPropertyID(matrixPropertyType, shaderPropertyID);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000056BE File Offset: 0x000038BE
		public void UnmarkLateLatchMatrix(CameraLateLatchMatrixType matrixPropertyType)
		{
			this.m_WrappedCommandBuffer.UnmarkLateLatchMatrix(matrixPropertyType);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000056CC File Offset: 0x000038CC
		public void BeginSample(string name)
		{
			this.m_WrappedCommandBuffer.BeginSample(name);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000056DA File Offset: 0x000038DA
		public void EndSample(string name)
		{
			this.m_WrappedCommandBuffer.EndSample(name);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000056E8 File Offset: 0x000038E8
		public void BeginSample(CustomSampler sampler)
		{
			this.m_WrappedCommandBuffer.BeginSample(sampler);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000056F6 File Offset: 0x000038F6
		public void EndSample(CustomSampler sampler)
		{
			this.m_WrappedCommandBuffer.EndSample(sampler);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00005704 File Offset: 0x00003904
		public void BeginSample(ProfilerMarker marker)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00005704 File Offset: 0x00003904
		public void EndSample(ProfilerMarker marker)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00005706 File Offset: 0x00003906
		public void IncrementUpdateCount(RenderTargetIdentifier dest)
		{
			this.m_WrappedCommandBuffer.IncrementUpdateCount(dest);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00005FA7 File Offset: 0x000041A7
		public void SetInstanceMultiplier(uint multiplier)
		{
			this.m_WrappedCommandBuffer.SetInstanceMultiplier(multiplier);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00005FB5 File Offset: 0x000041B5
		public void SetFoveatedRenderingMode(FoveatedRenderingMode foveatedRenderingMode)
		{
			this.m_WrappedCommandBuffer.SetFoveatedRenderingMode(foveatedRenderingMode);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00005FC3 File Offset: 0x000041C3
		public void SetWireframe(bool enable)
		{
			this.m_WrappedCommandBuffer.SetWireframe(enable);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00005FD1 File Offset: 0x000041D1
		public void ConfigureFoveatedRendering(IntPtr platformData)
		{
			this.m_WrappedCommandBuffer.ConfigureFoveatedRendering(platformData);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00005804 File Offset: 0x00003A04
		public void SetupCameraProperties(Camera camera)
		{
			this.m_WrappedCommandBuffer.SetupCameraProperties(camera);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00005812 File Offset: 0x00003A12
		public void InvokeOnRenderObjectCallbacks()
		{
			this.m_WrappedCommandBuffer.InvokeOnRenderObjectCallbacks();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00005FDF File Offset: 0x000041DF
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, properties);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00005FF5 File Offset: 0x000041F5
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass)
		{
			this.m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00006009 File Offset: 0x00004209
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex)
		{
			this.m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material, submeshIndex);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000601B File Offset: 0x0000421B
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material)
		{
			this.m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000602B File Offset: 0x0000422B
		public void DrawMultipleMeshes(Matrix4x4[] matrices, Mesh[] meshes, int[] subsetIndices, int count, Material material, int shaderPass, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMultipleMeshes(matrices, meshes, subsetIndices, count, material, shaderPass, properties);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00006043 File Offset: 0x00004243
		public void DrawRenderer(Renderer renderer, Material material, int submeshIndex, int shaderPass)
		{
			this.m_WrappedCommandBuffer.DrawRenderer(renderer, material, submeshIndex, shaderPass);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00006055 File Offset: 0x00004255
		public void DrawRenderer(Renderer renderer, Material material, int submeshIndex)
		{
			this.m_WrappedCommandBuffer.DrawRenderer(renderer, material, submeshIndex);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00006065 File Offset: 0x00004265
		public void DrawRenderer(Renderer renderer, Material material)
		{
			this.m_WrappedCommandBuffer.DrawRenderer(renderer, material);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00006074 File Offset: 0x00004274
		public void DrawRendererList(RendererList rendererList)
		{
			this.m_WrappedCommandBuffer.DrawRendererList(rendererList);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00006082 File Offset: 0x00004282
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount, properties);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000609A File Offset: 0x0000429A
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000060B0 File Offset: 0x000042B0
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(matrix, material, shaderPass, topology, vertexCount);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000060C4 File Offset: 0x000042C4
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount, properties);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000060E9 File Offset: 0x000042E9
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00006101 File Offset: 0x00004301
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006117 File Offset: 0x00004317
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000612F File Offset: 0x0000432F
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00006145 File Offset: 0x00004345
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000615C File Offset: 0x0000435C
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00006181 File Offset: 0x00004381
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00006199 File Offset: 0x00004399
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000061AF File Offset: 0x000043AF
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000061C7 File Offset: 0x000043C7
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000061DD File Offset: 0x000043DD
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000061F4 File Offset: 0x000043F4
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00006219 File Offset: 0x00004419
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00006231 File Offset: 0x00004431
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00006247 File Offset: 0x00004447
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count, properties);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000625F File Offset: 0x0000445F
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00006275 File Offset: 0x00004475
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00006289 File Offset: 0x00004489
		public void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedProcedural(mesh, submeshIndex, material, shaderPass, count, properties);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000629F File Offset: 0x0000449F
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000062B7 File Offset: 0x000044B7
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000062CD File Offset: 0x000044CD
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000062E1 File Offset: 0x000044E1
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000062F9 File Offset: 0x000044F9
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000630F File Offset: 0x0000450F
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00006323 File Offset: 0x00004523
		public void DrawOcclusionMesh(RectInt normalizedCamViewport)
		{
			this.m_WrappedCommandBuffer.DrawOcclusionMesh(normalizedCamViewport);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00005CDA File Offset: 0x00003EDA
		public void SetGlobalFloat(string name, float value)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloat(name, value);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00005CE9 File Offset: 0x00003EE9
		public void SetGlobalInt(string name, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInt(name, value);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public void SetGlobalInteger(string name, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInteger(name, value);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00005D07 File Offset: 0x00003F07
		public void SetGlobalVector(string name, Vector4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalVector(name, value);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00005D16 File Offset: 0x00003F16
		public void SetGlobalColor(string name, Color value)
		{
			this.m_WrappedCommandBuffer.SetGlobalColor(name, value);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00005D25 File Offset: 0x00003F25
		public void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrix(name, value);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00005D34 File Offset: 0x00003F34
		public void SetGlobalFloatArray(string propertyName, List<float> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(propertyName, values);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00005D43 File Offset: 0x00003F43
		public void SetGlobalFloatArray(int nameID, List<float> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(nameID, values);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00005D52 File Offset: 0x00003F52
		public void SetGlobalFloatArray(string propertyName, float[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(propertyName, values);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00005D61 File Offset: 0x00003F61
		public void SetGlobalVectorArray(string propertyName, List<Vector4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(propertyName, values);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00005D70 File Offset: 0x00003F70
		public void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(nameID, values);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00005D7F File Offset: 0x00003F7F
		public void SetGlobalVectorArray(string propertyName, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(propertyName, values);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00005D8E File Offset: 0x00003F8E
		public void SetGlobalMatrixArray(string propertyName, List<Matrix4x4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(propertyName, values);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00005D9D File Offset: 0x00003F9D
		public void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(nameID, values);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00005DAC File Offset: 0x00003FAC
		public void SetGlobalMatrixArray(string propertyName, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(propertyName, values);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00005DBB File Offset: 0x00003FBB
		public void SetGlobalTexture(string name, TextureHandle value)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(name, value);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00005DCF File Offset: 0x00003FCF
		public void SetGlobalTexture(int nameID, TextureHandle value)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(nameID, value);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00005DE3 File Offset: 0x00003FE3
		public void SetGlobalTexture(string name, TextureHandle value, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(name, value, element);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public void SetGlobalTexture(int nameID, TextureHandle value, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(nameID, value, element);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00005E0D File Offset: 0x0000400D
		public void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(name, value);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00005E1C File Offset: 0x0000401C
		public void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(nameID, value);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00005E2B File Offset: 0x0000402B
		public void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(name, value);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00005E3A File Offset: 0x0000403A
		public void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(nameID, value);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00005E49 File Offset: 0x00004049
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, int nameID, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, nameID, offset, size);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00005E5B File Offset: 0x0000405B
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, string name, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, name, offset, size);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00005E6D File Offset: 0x0000406D
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, int nameID, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, nameID, offset, size);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00005E7F File Offset: 0x0000407F
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, string name, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, name, offset, size);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00005E91 File Offset: 0x00004091
		public void SetShadowSamplingMode(RenderTargetIdentifier shadowmap, ShadowSamplingMode mode)
		{
			this.m_WrappedCommandBuffer.SetShadowSamplingMode(shadowmap, mode);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00005EA0 File Offset: 0x000040A0
		public void SetSinglePassStereo(SinglePassStereoMode mode)
		{
			this.m_WrappedCommandBuffer.SetSinglePassStereo(mode);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00005EAE File Offset: 0x000040AE
		public void IssuePluginEvent(IntPtr callback, int eventID)
		{
			this.m_WrappedCommandBuffer.IssuePluginEvent(callback, eventID);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00005EBD File Offset: 0x000040BD
		public void IssuePluginEventAndData(IntPtr callback, int eventID, IntPtr data)
		{
			this.m_WrappedCommandBuffer.IssuePluginEventAndData(callback, eventID, data);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00005ECD File Offset: 0x000040CD
		public void IssuePluginCustomBlit(IntPtr callback, uint command, RenderTargetIdentifier source, RenderTargetIdentifier dest, uint commandParam, uint commandFlags)
		{
			this.m_WrappedCommandBuffer.IssuePluginCustomBlit(callback, command, source, dest, commandParam, commandFlags);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00005EE3 File Offset: 0x000040E3
		public void IssuePluginCustomTextureUpdateV2(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.m_WrappedCommandBuffer.IssuePluginCustomTextureUpdateV2(callback, targetTexture, userData);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00006331 File Offset: 0x00004531
		void IBaseCommandBuffer.EnableKeyword(in GlobalKeyword keyword)
		{
			this.EnableKeyword(in keyword);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000633A File Offset: 0x0000453A
		void IBaseCommandBuffer.EnableKeyword(Material material, in LocalKeyword keyword)
		{
			this.EnableKeyword(material, in keyword);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00006344 File Offset: 0x00004544
		void IBaseCommandBuffer.EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.EnableKeyword(computeShader, in keyword);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000634E File Offset: 0x0000454E
		void IBaseCommandBuffer.DisableKeyword(in GlobalKeyword keyword)
		{
			this.DisableKeyword(in keyword);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00006357 File Offset: 0x00004557
		void IBaseCommandBuffer.DisableKeyword(Material material, in LocalKeyword keyword)
		{
			this.DisableKeyword(material, in keyword);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00006361 File Offset: 0x00004561
		void IBaseCommandBuffer.DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.DisableKeyword(computeShader, in keyword);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000636B File Offset: 0x0000456B
		void IBaseCommandBuffer.SetKeyword(in GlobalKeyword keyword, bool value)
		{
			this.SetKeyword(in keyword, value);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00006375 File Offset: 0x00004575
		void IBaseCommandBuffer.SetKeyword(Material material, in LocalKeyword keyword, bool value)
		{
			this.SetKeyword(material, in keyword, value);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00006380 File Offset: 0x00004580
		void IBaseCommandBuffer.SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value)
		{
			this.SetKeyword(computeShader, in keyword, value);
		}
	}
}
