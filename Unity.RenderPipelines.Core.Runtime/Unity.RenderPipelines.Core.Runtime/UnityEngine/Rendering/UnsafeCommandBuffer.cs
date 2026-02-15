using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Profiling;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x02000026 RID: 38
	public class UnsafeCommandBuffer : BaseCommandBuffer, IUnsafeCommandBuffer, IBaseCommandBuffer, IRasterCommandBuffer, IComputeCommandBuffer
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x000054B7 File Offset: 0x000036B7
		internal UnsafeCommandBuffer(CommandBuffer wrapped, RenderGraphPass executingPass, bool isAsync)
			: base(wrapped, executingPass, isAsync)
		{
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000638B File Offset: 0x0000458B
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, callback);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000639B File Offset: 0x0000459B
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, size, offset, callback);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000063AF File Offset: 0x000045AF
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, callback);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000063BF File Offset: 0x000045BF
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, size, offset, callback);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000063D3 File Offset: 0x000045D3
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, callback);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000063E3 File Offset: 0x000045E3
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, mipIndex, callback);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000063F5 File Offset: 0x000045F5
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, mipIndex, dstFormat, callback);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00006409 File Offset: 0x00004609
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, mipIndex, dstFormat, callback);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00006420 File Offset: 0x00004620
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, mipIndex, x, width, y, height, z, depth, callback);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000644C File Offset: 0x0000464C
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, mipIndex, x, width, y, height, z, depth, dstFormat, callback);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00006478 File Offset: 0x00004678
		public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.m_WrappedCommandBuffer.RequestAsyncReadbackIntoNativeArray<T>(ref output, src, mipIndex, x, width, y, height, z, depth, dstFormat, callback);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000054C2 File Offset: 0x000036C2
		public void SetInvertCulling(bool invertCulling)
		{
			this.m_WrappedCommandBuffer.SetInvertCulling(invertCulling);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000054D0 File Offset: 0x000036D0
		public void SetComputeFloatParam(ComputeShader computeShader, int nameID, float val)
		{
			this.m_WrappedCommandBuffer.SetComputeFloatParam(computeShader, nameID, val);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000054E0 File Offset: 0x000036E0
		public void SetComputeIntParam(ComputeShader computeShader, int nameID, int val)
		{
			this.m_WrappedCommandBuffer.SetComputeIntParam(computeShader, nameID, val);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000054F0 File Offset: 0x000036F0
		public void SetComputeVectorParam(ComputeShader computeShader, int nameID, Vector4 val)
		{
			this.m_WrappedCommandBuffer.SetComputeVectorParam(computeShader, nameID, val);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00005500 File Offset: 0x00003700
		public void SetComputeVectorArrayParam(ComputeShader computeShader, int nameID, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeVectorArrayParam(computeShader, nameID, values);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00005510 File Offset: 0x00003710
		public void SetComputeMatrixParam(ComputeShader computeShader, int nameID, Matrix4x4 val)
		{
			this.m_WrappedCommandBuffer.SetComputeMatrixParam(computeShader, nameID, val);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00005520 File Offset: 0x00003720
		public void SetComputeMatrixArrayParam(ComputeShader computeShader, int nameID, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeMatrixArrayParam(computeShader, nameID, values);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000064A3 File Offset: 0x000046A3
		public void Clear()
		{
			this.m_WrappedCommandBuffer.Clear();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00005530 File Offset: 0x00003730
		public void SetViewport(Rect pixelRect)
		{
			this.m_WrappedCommandBuffer.SetViewport(pixelRect);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000553E File Offset: 0x0000373E
		public void EnableScissorRect(Rect scissor)
		{
			this.m_WrappedCommandBuffer.EnableScissorRect(scissor);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000554C File Offset: 0x0000374C
		public void DisableScissorRect()
		{
			this.m_WrappedCommandBuffer.DisableScissorRect();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00005F4D File Offset: 0x0000414D
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearDepth, clearColor, backgroundColor);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00005F5D File Offset: 0x0000415D
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00005F6F File Offset: 0x0000416F
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth, uint stencil)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearDepth, clearColor, backgroundColor, depth, stencil);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00005F83 File Offset: 0x00004183
		public void ClearRenderTarget(RTClearFlags clearFlags, Color backgroundColor, float depth, uint stencil)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearFlags, backgroundColor, depth, stencil);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00005F95 File Offset: 0x00004195
		public void ClearRenderTarget(RTClearFlags clearFlags, Color[] backgroundColors, float depth, uint stencil)
		{
			this.m_WrappedCommandBuffer.ClearRenderTarget(clearFlags, backgroundColors, depth, stencil);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00005559 File Offset: 0x00003759
		public void SetGlobalFloat(int nameID, float value)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloat(nameID, value);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00005568 File Offset: 0x00003768
		public void SetGlobalInt(int nameID, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInt(nameID, value);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00005577 File Offset: 0x00003777
		public void SetGlobalInteger(int nameID, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInteger(nameID, value);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00005586 File Offset: 0x00003786
		public void SetGlobalVector(int nameID, Vector4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalVector(nameID, value);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00005595 File Offset: 0x00003795
		public void SetGlobalColor(int nameID, Color value)
		{
			this.m_WrappedCommandBuffer.SetGlobalColor(nameID, value);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000055A4 File Offset: 0x000037A4
		public void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrix(nameID, value);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000055B3 File Offset: 0x000037B3
		public void EnableShaderKeyword(string keyword)
		{
			this.m_WrappedCommandBuffer.EnableShaderKeyword(keyword);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000055C1 File Offset: 0x000037C1
		public void EnableKeyword(in GlobalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.EnableKeyword(in keyword);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000055CF File Offset: 0x000037CF
		public void EnableKeyword(Material material, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.EnableKeyword(material, in keyword);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000055DE File Offset: 0x000037DE
		public void EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.EnableKeyword(computeShader, in keyword);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000055ED File Offset: 0x000037ED
		public void DisableShaderKeyword(string keyword)
		{
			this.m_WrappedCommandBuffer.DisableShaderKeyword(keyword);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000055FB File Offset: 0x000037FB
		public void DisableKeyword(in GlobalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.DisableKeyword(in keyword);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00005609 File Offset: 0x00003809
		public void DisableKeyword(Material material, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.DisableKeyword(material, in keyword);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00005618 File Offset: 0x00003818
		public void DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.DisableKeyword(computeShader, in keyword);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00005627 File Offset: 0x00003827
		public void SetKeyword(in GlobalKeyword keyword, bool value)
		{
			this.m_WrappedCommandBuffer.SetKeyword(in keyword, value);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00005636 File Offset: 0x00003836
		public void SetKeyword(Material material, in LocalKeyword keyword, bool value)
		{
			this.m_WrappedCommandBuffer.SetKeyword(material, in keyword, value);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00005646 File Offset: 0x00003846
		public void SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value)
		{
			this.m_WrappedCommandBuffer.SetKeyword(computeShader, in keyword, value);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00005656 File Offset: 0x00003856
		public void SetViewProjectionMatrices(Matrix4x4 view, Matrix4x4 proj)
		{
			this.m_WrappedCommandBuffer.SetViewProjectionMatrices(view, proj);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00005665 File Offset: 0x00003865
		public void SetGlobalDepthBias(float bias, float slopeBias)
		{
			this.m_WrappedCommandBuffer.SetGlobalDepthBias(bias, slopeBias);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00005674 File Offset: 0x00003874
		public void SetGlobalFloatArray(int nameID, float[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(nameID, values);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00005683 File Offset: 0x00003883
		public void SetGlobalVectorArray(int nameID, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(nameID, values);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00005692 File Offset: 0x00003892
		public void SetGlobalMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(nameID, values);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000056A1 File Offset: 0x000038A1
		public void SetLateLatchProjectionMatrices(Matrix4x4[] projectionMat)
		{
			this.m_WrappedCommandBuffer.SetLateLatchProjectionMatrices(projectionMat);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000056AF File Offset: 0x000038AF
		public void MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID)
		{
			this.m_WrappedCommandBuffer.MarkLateLatchMatrixShaderPropertyID(matrixPropertyType, shaderPropertyID);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000056BE File Offset: 0x000038BE
		public void UnmarkLateLatchMatrix(CameraLateLatchMatrixType matrixPropertyType)
		{
			this.m_WrappedCommandBuffer.UnmarkLateLatchMatrix(matrixPropertyType);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000056CC File Offset: 0x000038CC
		public void BeginSample(string name)
		{
			this.m_WrappedCommandBuffer.BeginSample(name);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000056DA File Offset: 0x000038DA
		public void EndSample(string name)
		{
			this.m_WrappedCommandBuffer.EndSample(name);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000056E8 File Offset: 0x000038E8
		public void BeginSample(CustomSampler sampler)
		{
			this.m_WrappedCommandBuffer.BeginSample(sampler);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000056F6 File Offset: 0x000038F6
		public void EndSample(CustomSampler sampler)
		{
			this.m_WrappedCommandBuffer.EndSample(sampler);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00005704 File Offset: 0x00003904
		public void BeginSample(ProfilerMarker marker)
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00005704 File Offset: 0x00003904
		public void EndSample(ProfilerMarker marker)
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00005706 File Offset: 0x00003906
		public void IncrementUpdateCount(RenderTargetIdentifier dest)
		{
			this.m_WrappedCommandBuffer.IncrementUpdateCount(dest);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00005FA7 File Offset: 0x000041A7
		public void SetInstanceMultiplier(uint multiplier)
		{
			this.m_WrappedCommandBuffer.SetInstanceMultiplier(multiplier);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00005FB5 File Offset: 0x000041B5
		public void SetFoveatedRenderingMode(FoveatedRenderingMode foveatedRenderingMode)
		{
			this.m_WrappedCommandBuffer.SetFoveatedRenderingMode(foveatedRenderingMode);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00005FC3 File Offset: 0x000041C3
		public void SetWireframe(bool enable)
		{
			this.m_WrappedCommandBuffer.SetWireframe(enable);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00005FD1 File Offset: 0x000041D1
		public void ConfigureFoveatedRendering(IntPtr platformData)
		{
			this.m_WrappedCommandBuffer.ConfigureFoveatedRendering(platformData);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000064B0 File Offset: 0x000046B0
		public void SetRenderTarget(RenderTargetIdentifier rt)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(rt);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000064BE File Offset: 0x000046BE
		public void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(rt, loadAction, storeAction);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000064CE File Offset: 0x000046CE
		public void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(rt, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000064E2 File Offset: 0x000046E2
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(rt, mipLevel);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000064F1 File Offset: 0x000046F1
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(rt, mipLevel, cubemapFace);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00006501 File Offset: 0x00004701
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(rt, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00006513 File Offset: 0x00004713
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(color, depth);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00006522 File Offset: 0x00004722
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(color, depth, mipLevel);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00006532 File Offset: 0x00004732
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(color, depth, mipLevel, cubemapFace);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00006544 File Offset: 0x00004744
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(color, depth, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00006558 File Offset: 0x00004758
		public void SetRenderTarget(RenderTargetIdentifier color, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depth, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(color, colorLoadAction, colorStoreAction, depth, depthLoadAction, depthStoreAction);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000656E File Offset: 0x0000476E
		public void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(colors, depth);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000657D File Offset: 0x0000477D
		public void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(colors, depth, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00006591 File Offset: 0x00004791
		public void SetRenderTarget(RenderTargetBinding binding, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(binding, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000065A3 File Offset: 0x000047A3
		public void SetRenderTarget(RenderTargetBinding binding)
		{
			this.m_WrappedCommandBuffer.SetRenderTarget(binding);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00005714 File Offset: 0x00003914
		public void SetBufferData(ComputeBuffer buffer, Array data)
		{
			this.m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00005723 File Offset: 0x00003923
		public void SetBufferData<T>(ComputeBuffer buffer, List<T> data) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00005732 File Offset: 0x00003932
		public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00005741 File Offset: 0x00003941
		public void SetBufferData(ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			this.m_WrappedCommandBuffer.SetBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00005755 File Offset: 0x00003955
		public void SetBufferData<T>(ComputeBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00005769 File Offset: 0x00003969
		public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data, nativeBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000577D File Offset: 0x0000397D
		public void SetBufferCounterValue(ComputeBuffer buffer, uint counterValue)
		{
			this.m_WrappedCommandBuffer.SetBufferCounterValue(buffer, counterValue);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000578C File Offset: 0x0000398C
		public void SetBufferData(GraphicsBuffer buffer, Array data)
		{
			this.m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000579B File Offset: 0x0000399B
		public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000057AA File Offset: 0x000039AA
		public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000057B9 File Offset: 0x000039B9
		public void SetBufferData(GraphicsBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			this.m_WrappedCommandBuffer.SetBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000057CD File Offset: 0x000039CD
		public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000057E1 File Offset: 0x000039E1
		public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data, nativeBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000057F5 File Offset: 0x000039F5
		public void SetBufferCounterValue(GraphicsBuffer buffer, uint counterValue)
		{
			this.m_WrappedCommandBuffer.SetBufferCounterValue(buffer, counterValue);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00005804 File Offset: 0x00003A04
		public void SetupCameraProperties(Camera camera)
		{
			this.m_WrappedCommandBuffer.SetupCameraProperties(camera);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00005812 File Offset: 0x00003A12
		public void InvokeOnRenderObjectCallbacks()
		{
			this.m_WrappedCommandBuffer.InvokeOnRenderObjectCallbacks();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000581F File Offset: 0x00003A1F
		public void SetComputeFloatParam(ComputeShader computeShader, string name, float val)
		{
			this.m_WrappedCommandBuffer.SetComputeFloatParam(computeShader, name, val);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000582F File Offset: 0x00003A2F
		public void SetComputeIntParam(ComputeShader computeShader, string name, int val)
		{
			this.m_WrappedCommandBuffer.SetComputeIntParam(computeShader, name, val);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000583F File Offset: 0x00003A3F
		public void SetComputeVectorParam(ComputeShader computeShader, string name, Vector4 val)
		{
			this.m_WrappedCommandBuffer.SetComputeVectorParam(computeShader, name, val);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000584F File Offset: 0x00003A4F
		public void SetComputeVectorArrayParam(ComputeShader computeShader, string name, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeVectorArrayParam(computeShader, name, values);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000585F File Offset: 0x00003A5F
		public void SetComputeMatrixParam(ComputeShader computeShader, string name, Matrix4x4 val)
		{
			this.m_WrappedCommandBuffer.SetComputeMatrixParam(computeShader, name, val);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000586F File Offset: 0x00003A6F
		public void SetComputeMatrixArrayParam(ComputeShader computeShader, string name, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeMatrixArrayParam(computeShader, name, values);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000587F File Offset: 0x00003A7F
		public void SetComputeFloatParams(ComputeShader computeShader, string name, params float[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeFloatParams(computeShader, name, values);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000588F File Offset: 0x00003A8F
		public void SetComputeFloatParams(ComputeShader computeShader, int nameID, params float[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeFloatParams(computeShader, nameID, values);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000589F File Offset: 0x00003A9F
		public void SetComputeIntParams(ComputeShader computeShader, string name, params int[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeIntParams(computeShader, name, values);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000058AF File Offset: 0x00003AAF
		public void SetComputeIntParams(ComputeShader computeShader, int nameID, params int[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeIntParams(computeShader, nameID, values);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000058BF File Offset: 0x00003ABF
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, TextureHandle rt)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, name, rt);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000058D6 File Offset: 0x00003AD6
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, TextureHandle rt)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, nameID, rt);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000058ED File Offset: 0x00003AED
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, TextureHandle rt, int mipLevel)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, name, rt, mipLevel);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00005906 File Offset: 0x00003B06
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, TextureHandle rt, int mipLevel)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, nameID, rt, mipLevel);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000591F File Offset: 0x00003B1F
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, TextureHandle rt, int mipLevel, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, name, rt, mipLevel, element);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000593A File Offset: 0x00003B3A
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, TextureHandle rt, int mipLevel, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, nameID, rt, mipLevel, element);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00005955 File Offset: 0x00003B55
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00005967 File Offset: 0x00003B67
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, name, buffer);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00005979 File Offset: 0x00003B79
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBufferHandle bufferHandle)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, nameID, bufferHandle);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000598B File Offset: 0x00003B8B
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBufferHandle bufferHandle)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, name, bufferHandle);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000599D File Offset: 0x00003B9D
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000059AF File Offset: 0x00003BAF
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, name, buffer);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000059C1 File Offset: 0x00003BC1
		public void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, nameID, buffer, offset, size);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000059D5 File Offset: 0x00003BD5
		public void SetComputeConstantBufferParam(ComputeShader computeShader, string name, ComputeBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, name, buffer, offset, size);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000059E9 File Offset: 0x00003BE9
		public void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, nameID, buffer, offset, size);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000059FD File Offset: 0x00003BFD
		public void SetComputeConstantBufferParam(ComputeShader computeShader, string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, name, buffer, offset, size);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00005A11 File Offset: 0x00003C11
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			this.m_WrappedCommandBuffer.DispatchCompute(computeShader, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00005A25 File Offset: 0x00003C25
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset)
		{
			this.m_WrappedCommandBuffer.DispatchCompute(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00005A37 File Offset: 0x00003C37
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset)
		{
			this.m_WrappedCommandBuffer.DispatchCompute(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00005A49 File Offset: 0x00003C49
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure)
		{
			this.m_WrappedCommandBuffer.BuildRayTracingAccelerationStructure(accelerationStructure);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00005A57 File Offset: 0x00003C57
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure, Vector3 relativeOrigin)
		{
			this.m_WrappedCommandBuffer.BuildRayTracingAccelerationStructure(accelerationStructure, relativeOrigin);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00005A66 File Offset: 0x00003C66
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(rayTracingShader, name, rayTracingAccelerationStructure);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00005A76 File Offset: 0x00003C76
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(rayTracingShader, nameID, rayTracingAccelerationStructure);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00005A86 File Offset: 0x00003C86
		public void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(computeShader, kernelIndex, name, rayTracingAccelerationStructure);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00005A98 File Offset: 0x00003C98
		public void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(computeShader, kernelIndex, nameID, rayTracingAccelerationStructure);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00005AAA File Offset: 0x00003CAA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, name, buffer);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00005ABA File Offset: 0x00003CBA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, nameID, buffer);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00005ACA File Offset: 0x00003CCA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, name, buffer);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00005ADA File Offset: 0x00003CDA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, nameID, buffer);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00005AEA File Offset: 0x00003CEA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBufferHandle bufferHandle)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, name, bufferHandle);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00005AFA File Offset: 0x00003CFA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBufferHandle bufferHandle)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, nameID, bufferHandle);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00005B0A File Offset: 0x00003D0A
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00005B1E File Offset: 0x00003D1E
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, name, buffer, offset, size);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00005B32 File Offset: 0x00003D32
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00005B46 File Offset: 0x00003D46
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, name, buffer, offset, size);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00005B5A File Offset: 0x00003D5A
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, string name, TextureHandle rt)
		{
			this.m_WrappedCommandBuffer.SetRayTracingTextureParam(rayTracingShader, name, rt);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00005B6F File Offset: 0x00003D6F
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, int nameID, TextureHandle rt)
		{
			this.m_WrappedCommandBuffer.SetRayTracingTextureParam(rayTracingShader, nameID, rt);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00005B84 File Offset: 0x00003D84
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, string name, float val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingFloatParam(rayTracingShader, name, val);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00005B94 File Offset: 0x00003D94
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, int nameID, float val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingFloatParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, string name, params float[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingFloatParams(rayTracingShader, name, values);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, int nameID, params float[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingFloatParams(rayTracingShader, nameID, values);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, string name, int val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingIntParam(rayTracingShader, name, val);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, int nameID, int val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingIntParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, string name, params int[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingIntParams(rayTracingShader, name, values);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, int nameID, params int[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingIntParams(rayTracingShader, nameID, values);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00005C04 File Offset: 0x00003E04
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, string name, Vector4 val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingVectorParam(rayTracingShader, name, val);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00005C14 File Offset: 0x00003E14
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, int nameID, Vector4 val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingVectorParam(rayTracingShader, nameID, val);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00005C24 File Offset: 0x00003E24
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, string name, params Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingVectorArrayParam(rayTracingShader, name, values);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00005C34 File Offset: 0x00003E34
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, int nameID, params Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingVectorArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00005C44 File Offset: 0x00003E44
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, string name, Matrix4x4 val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingMatrixParam(rayTracingShader, name, val);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00005C54 File Offset: 0x00003E54
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, int nameID, Matrix4x4 val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingMatrixParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00005C64 File Offset: 0x00003E64
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, string name, params Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingMatrixArrayParam(rayTracingShader, name, values);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00005C74 File Offset: 0x00003E74
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, int nameID, params Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingMatrixArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00005C84 File Offset: 0x00003E84
		public void DispatchRays(RayTracingShader rayTracingShader, string rayGenName, uint width, uint height, uint depth, Camera camera)
		{
			this.m_WrappedCommandBuffer.DispatchRays(rayTracingShader, rayGenName, width, height, depth, camera);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00005FDF File Offset: 0x000041DF
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, properties);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00005FF5 File Offset: 0x000041F5
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass)
		{
			this.m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00006009 File Offset: 0x00004209
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex)
		{
			this.m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material, submeshIndex);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000601B File Offset: 0x0000421B
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material)
		{
			this.m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000602B File Offset: 0x0000422B
		public void DrawMultipleMeshes(Matrix4x4[] matrices, Mesh[] meshes, int[] subsetIndices, int count, Material material, int shaderPass, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMultipleMeshes(matrices, meshes, subsetIndices, count, material, shaderPass, properties);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00006043 File Offset: 0x00004243
		public void DrawRenderer(Renderer renderer, Material material, int submeshIndex, int shaderPass)
		{
			this.m_WrappedCommandBuffer.DrawRenderer(renderer, material, submeshIndex, shaderPass);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00006055 File Offset: 0x00004255
		public void DrawRenderer(Renderer renderer, Material material, int submeshIndex)
		{
			this.m_WrappedCommandBuffer.DrawRenderer(renderer, material, submeshIndex);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00006065 File Offset: 0x00004265
		public void DrawRenderer(Renderer renderer, Material material)
		{
			this.m_WrappedCommandBuffer.DrawRenderer(renderer, material);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00006074 File Offset: 0x00004274
		public void DrawRendererList(RendererList rendererList)
		{
			this.m_WrappedCommandBuffer.DrawRendererList(rendererList);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00006082 File Offset: 0x00004282
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount, properties);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000609A File Offset: 0x0000429A
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000060B0 File Offset: 0x000042B0
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(matrix, material, shaderPass, topology, vertexCount);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000065B4 File Offset: 0x000047B4
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount, properties);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000060E9 File Offset: 0x000042E9
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00006101 File Offset: 0x00004301
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount)
		{
			this.m_WrappedCommandBuffer.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00006117 File Offset: 0x00004317
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000612F File Offset: 0x0000432F
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00006145 File Offset: 0x00004345
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000065DC File Offset: 0x000047DC
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00006181 File Offset: 0x00004381
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00006199 File Offset: 0x00004399
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000061AF File Offset: 0x000043AF
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000061C7 File Offset: 0x000043C7
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000061DD File Offset: 0x000043DD
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00006604 File Offset: 0x00004804
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00006219 File Offset: 0x00004419
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00006231 File Offset: 0x00004431
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00006247 File Offset: 0x00004447
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count, properties);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000625F File Offset: 0x0000445F
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00006275 File Offset: 0x00004475
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00006289 File Offset: 0x00004489
		public void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedProcedural(mesh, submeshIndex, material, shaderPass, count, properties);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000629F File Offset: 0x0000449F
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000062B7 File Offset: 0x000044B7
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000062CD File Offset: 0x000044CD
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000062E1 File Offset: 0x000044E1
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000062F9 File Offset: 0x000044F9
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000630F File Offset: 0x0000450F
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs)
		{
			this.m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00006323 File Offset: 0x00004523
		public void DrawOcclusionMesh(RectInt normalizedCamViewport)
		{
			this.m_WrappedCommandBuffer.DrawOcclusionMesh(normalizedCamViewport);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00005C9A File Offset: 0x00003E9A
		public void CopyCounterValue(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00005CAA File Offset: 0x00003EAA
		public void CopyCounterValue(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00005CBA File Offset: 0x00003EBA
		public void CopyCounterValue(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00005CCA File Offset: 0x00003ECA
		public void CopyCounterValue(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00005CDA File Offset: 0x00003EDA
		public void SetGlobalFloat(string name, float value)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloat(name, value);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00005CE9 File Offset: 0x00003EE9
		public void SetGlobalInt(string name, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInt(name, value);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public void SetGlobalInteger(string name, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInteger(name, value);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00005D07 File Offset: 0x00003F07
		public void SetGlobalVector(string name, Vector4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalVector(name, value);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00005D16 File Offset: 0x00003F16
		public void SetGlobalColor(string name, Color value)
		{
			this.m_WrappedCommandBuffer.SetGlobalColor(name, value);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00005D25 File Offset: 0x00003F25
		public void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrix(name, value);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00005D34 File Offset: 0x00003F34
		public void SetGlobalFloatArray(string propertyName, List<float> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(propertyName, values);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00005D43 File Offset: 0x00003F43
		public void SetGlobalFloatArray(int nameID, List<float> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(nameID, values);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00005D52 File Offset: 0x00003F52
		public void SetGlobalFloatArray(string propertyName, float[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(propertyName, values);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00005D61 File Offset: 0x00003F61
		public void SetGlobalVectorArray(string propertyName, List<Vector4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(propertyName, values);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00005D70 File Offset: 0x00003F70
		public void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(nameID, values);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00005D7F File Offset: 0x00003F7F
		public void SetGlobalVectorArray(string propertyName, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(propertyName, values);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00005D8E File Offset: 0x00003F8E
		public void SetGlobalMatrixArray(string propertyName, List<Matrix4x4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(propertyName, values);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00005D9D File Offset: 0x00003F9D
		public void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(nameID, values);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00005DAC File Offset: 0x00003FAC
		public void SetGlobalMatrixArray(string propertyName, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(propertyName, values);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00005DBB File Offset: 0x00003FBB
		public void SetGlobalTexture(string name, TextureHandle value)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(name, value);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00005DCF File Offset: 0x00003FCF
		public void SetGlobalTexture(int nameID, TextureHandle value)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(nameID, value);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00005DE3 File Offset: 0x00003FE3
		public void SetGlobalTexture(string name, TextureHandle value, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(name, value, element);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public void SetGlobalTexture(int nameID, TextureHandle value, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(nameID, value, element);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00005E0D File Offset: 0x0000400D
		public void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(name, value);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00005E1C File Offset: 0x0000401C
		public void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(nameID, value);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00005E2B File Offset: 0x0000402B
		public void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(name, value);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00005E3A File Offset: 0x0000403A
		public void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(nameID, value);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00005E49 File Offset: 0x00004049
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, int nameID, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, nameID, offset, size);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00005E5B File Offset: 0x0000405B
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, string name, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, name, offset, size);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00005E6D File Offset: 0x0000406D
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, int nameID, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, nameID, offset, size);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00005E7F File Offset: 0x0000407F
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, string name, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, name, offset, size);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00005E91 File Offset: 0x00004091
		public void SetShadowSamplingMode(RenderTargetIdentifier shadowmap, ShadowSamplingMode mode)
		{
			this.m_WrappedCommandBuffer.SetShadowSamplingMode(shadowmap, mode);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00005EA0 File Offset: 0x000040A0
		public void SetSinglePassStereo(SinglePassStereoMode mode)
		{
			this.m_WrappedCommandBuffer.SetSinglePassStereo(mode);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00005EAE File Offset: 0x000040AE
		public void IssuePluginEvent(IntPtr callback, int eventID)
		{
			this.m_WrappedCommandBuffer.IssuePluginEvent(callback, eventID);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00005EBD File Offset: 0x000040BD
		public void IssuePluginEventAndData(IntPtr callback, int eventID, IntPtr data)
		{
			this.m_WrappedCommandBuffer.IssuePluginEventAndData(callback, eventID, data);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00005ECD File Offset: 0x000040CD
		public void IssuePluginCustomBlit(IntPtr callback, uint command, RenderTargetIdentifier source, RenderTargetIdentifier dest, uint commandParam, uint commandFlags)
		{
			this.m_WrappedCommandBuffer.IssuePluginCustomBlit(callback, command, source, dest, commandParam, commandFlags);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00005EE3 File Offset: 0x000040E3
		public void IssuePluginCustomTextureUpdateV2(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.m_WrappedCommandBuffer.IssuePluginCustomTextureUpdateV2(callback, targetTexture, userData);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00006629 File Offset: 0x00004829
		void IBaseCommandBuffer.EnableKeyword(in GlobalKeyword keyword)
		{
			this.EnableKeyword(in keyword);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00006632 File Offset: 0x00004832
		void IBaseCommandBuffer.EnableKeyword(Material material, in LocalKeyword keyword)
		{
			this.EnableKeyword(material, in keyword);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000663C File Offset: 0x0000483C
		void IBaseCommandBuffer.EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.EnableKeyword(computeShader, in keyword);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00006646 File Offset: 0x00004846
		void IBaseCommandBuffer.DisableKeyword(in GlobalKeyword keyword)
		{
			this.DisableKeyword(in keyword);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000664F File Offset: 0x0000484F
		void IBaseCommandBuffer.DisableKeyword(Material material, in LocalKeyword keyword)
		{
			this.DisableKeyword(material, in keyword);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00006659 File Offset: 0x00004859
		void IBaseCommandBuffer.DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.DisableKeyword(computeShader, in keyword);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00006663 File Offset: 0x00004863
		void IBaseCommandBuffer.SetKeyword(in GlobalKeyword keyword, bool value)
		{
			this.SetKeyword(in keyword, value);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000666D File Offset: 0x0000486D
		void IBaseCommandBuffer.SetKeyword(Material material, in LocalKeyword keyword, bool value)
		{
			this.SetKeyword(material, in keyword, value);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00006678 File Offset: 0x00004878
		void IBaseCommandBuffer.SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value)
		{
			this.SetKeyword(computeShader, in keyword, value);
		}
	}
}
