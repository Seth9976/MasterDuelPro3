using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Profiling;
using UnityEngine.Profiling;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x02000020 RID: 32
	public class ComputeCommandBuffer : BaseCommandBuffer, IComputeCommandBuffer, IBaseCommandBuffer
	{
		// Token: 0x060000CD RID: 205 RVA: 0x000054B7 File Offset: 0x000036B7
		internal ComputeCommandBuffer(CommandBuffer wrapped, RenderGraphPass executingPass, bool isAsync)
			: base(wrapped, executingPass, isAsync)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000054C2 File Offset: 0x000036C2
		public void SetInvertCulling(bool invertCulling)
		{
			this.m_WrappedCommandBuffer.SetInvertCulling(invertCulling);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000054D0 File Offset: 0x000036D0
		public void SetComputeFloatParam(ComputeShader computeShader, int nameID, float val)
		{
			this.m_WrappedCommandBuffer.SetComputeFloatParam(computeShader, nameID, val);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000054E0 File Offset: 0x000036E0
		public void SetComputeIntParam(ComputeShader computeShader, int nameID, int val)
		{
			this.m_WrappedCommandBuffer.SetComputeIntParam(computeShader, nameID, val);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000054F0 File Offset: 0x000036F0
		public void SetComputeVectorParam(ComputeShader computeShader, int nameID, Vector4 val)
		{
			this.m_WrappedCommandBuffer.SetComputeVectorParam(computeShader, nameID, val);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005500 File Offset: 0x00003700
		public void SetComputeVectorArrayParam(ComputeShader computeShader, int nameID, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeVectorArrayParam(computeShader, nameID, values);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005510 File Offset: 0x00003710
		public void SetComputeMatrixParam(ComputeShader computeShader, int nameID, Matrix4x4 val)
		{
			this.m_WrappedCommandBuffer.SetComputeMatrixParam(computeShader, nameID, val);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005520 File Offset: 0x00003720
		public void SetComputeMatrixArrayParam(ComputeShader computeShader, int nameID, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeMatrixArrayParam(computeShader, nameID, values);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005530 File Offset: 0x00003730
		public void SetViewport(Rect pixelRect)
		{
			this.m_WrappedCommandBuffer.SetViewport(pixelRect);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000553E File Offset: 0x0000373E
		public void EnableScissorRect(Rect scissor)
		{
			this.m_WrappedCommandBuffer.EnableScissorRect(scissor);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000554C File Offset: 0x0000374C
		public void DisableScissorRect()
		{
			this.m_WrappedCommandBuffer.DisableScissorRect();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005559 File Offset: 0x00003759
		public void SetGlobalFloat(int nameID, float value)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloat(nameID, value);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005568 File Offset: 0x00003768
		public void SetGlobalInt(int nameID, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInt(nameID, value);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005577 File Offset: 0x00003777
		public void SetGlobalInteger(int nameID, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInteger(nameID, value);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005586 File Offset: 0x00003786
		public void SetGlobalVector(int nameID, Vector4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalVector(nameID, value);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005595 File Offset: 0x00003795
		public void SetGlobalColor(int nameID, Color value)
		{
			this.m_WrappedCommandBuffer.SetGlobalColor(nameID, value);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000055A4 File Offset: 0x000037A4
		public void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrix(nameID, value);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000055B3 File Offset: 0x000037B3
		public void EnableShaderKeyword(string keyword)
		{
			this.m_WrappedCommandBuffer.EnableShaderKeyword(keyword);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000055C1 File Offset: 0x000037C1
		public void EnableKeyword(in GlobalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.EnableKeyword(in keyword);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000055CF File Offset: 0x000037CF
		public void EnableKeyword(Material material, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.EnableKeyword(material, in keyword);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000055DE File Offset: 0x000037DE
		public void EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.EnableKeyword(computeShader, in keyword);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000055ED File Offset: 0x000037ED
		public void DisableShaderKeyword(string keyword)
		{
			this.m_WrappedCommandBuffer.DisableShaderKeyword(keyword);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000055FB File Offset: 0x000037FB
		public void DisableKeyword(in GlobalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.DisableKeyword(in keyword);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005609 File Offset: 0x00003809
		public void DisableKeyword(Material material, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.DisableKeyword(material, in keyword);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005618 File Offset: 0x00003818
		public void DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.m_WrappedCommandBuffer.DisableKeyword(computeShader, in keyword);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005627 File Offset: 0x00003827
		public void SetKeyword(in GlobalKeyword keyword, bool value)
		{
			this.m_WrappedCommandBuffer.SetKeyword(in keyword, value);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005636 File Offset: 0x00003836
		public void SetKeyword(Material material, in LocalKeyword keyword, bool value)
		{
			this.m_WrappedCommandBuffer.SetKeyword(material, in keyword, value);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005646 File Offset: 0x00003846
		public void SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value)
		{
			this.m_WrappedCommandBuffer.SetKeyword(computeShader, in keyword, value);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005656 File Offset: 0x00003856
		public void SetViewProjectionMatrices(Matrix4x4 view, Matrix4x4 proj)
		{
			this.m_WrappedCommandBuffer.SetViewProjectionMatrices(view, proj);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005665 File Offset: 0x00003865
		public void SetGlobalDepthBias(float bias, float slopeBias)
		{
			this.m_WrappedCommandBuffer.SetGlobalDepthBias(bias, slopeBias);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005674 File Offset: 0x00003874
		public void SetGlobalFloatArray(int nameID, float[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(nameID, values);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005683 File Offset: 0x00003883
		public void SetGlobalVectorArray(int nameID, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(nameID, values);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005692 File Offset: 0x00003892
		public void SetGlobalMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(nameID, values);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000056A1 File Offset: 0x000038A1
		public void SetLateLatchProjectionMatrices(Matrix4x4[] projectionMat)
		{
			this.m_WrappedCommandBuffer.SetLateLatchProjectionMatrices(projectionMat);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000056AF File Offset: 0x000038AF
		public void MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID)
		{
			this.m_WrappedCommandBuffer.MarkLateLatchMatrixShaderPropertyID(matrixPropertyType, shaderPropertyID);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000056BE File Offset: 0x000038BE
		public void UnmarkLateLatchMatrix(CameraLateLatchMatrixType matrixPropertyType)
		{
			this.m_WrappedCommandBuffer.UnmarkLateLatchMatrix(matrixPropertyType);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000056CC File Offset: 0x000038CC
		public void BeginSample(string name)
		{
			this.m_WrappedCommandBuffer.BeginSample(name);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000056DA File Offset: 0x000038DA
		public void EndSample(string name)
		{
			this.m_WrappedCommandBuffer.EndSample(name);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000056E8 File Offset: 0x000038E8
		public void BeginSample(CustomSampler sampler)
		{
			this.m_WrappedCommandBuffer.BeginSample(sampler);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000056F6 File Offset: 0x000038F6
		public void EndSample(CustomSampler sampler)
		{
			this.m_WrappedCommandBuffer.EndSample(sampler);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005704 File Offset: 0x00003904
		public void BeginSample(ProfilerMarker marker)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005704 File Offset: 0x00003904
		public void EndSample(ProfilerMarker marker)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005706 File Offset: 0x00003906
		public void IncrementUpdateCount(RenderTargetIdentifier dest)
		{
			this.m_WrappedCommandBuffer.IncrementUpdateCount(dest);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005714 File Offset: 0x00003914
		public void SetBufferData(ComputeBuffer buffer, Array data)
		{
			this.m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005723 File Offset: 0x00003923
		public void SetBufferData<T>(ComputeBuffer buffer, List<T> data) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005732 File Offset: 0x00003932
		public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005741 File Offset: 0x00003941
		public void SetBufferData(ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			this.m_WrappedCommandBuffer.SetBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005755 File Offset: 0x00003955
		public void SetBufferData<T>(ComputeBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005769 File Offset: 0x00003969
		public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data, nativeBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000577D File Offset: 0x0000397D
		public void SetBufferCounterValue(ComputeBuffer buffer, uint counterValue)
		{
			this.m_WrappedCommandBuffer.SetBufferCounterValue(buffer, counterValue);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000578C File Offset: 0x0000398C
		public void SetBufferData(GraphicsBuffer buffer, Array data)
		{
			this.m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000579B File Offset: 0x0000399B
		public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000057AA File Offset: 0x000039AA
		public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000057B9 File Offset: 0x000039B9
		public void SetBufferData(GraphicsBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			this.m_WrappedCommandBuffer.SetBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000057CD File Offset: 0x000039CD
		public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000057E1 File Offset: 0x000039E1
		public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.m_WrappedCommandBuffer.SetBufferData<T>(buffer, data, nativeBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000057F5 File Offset: 0x000039F5
		public void SetBufferCounterValue(GraphicsBuffer buffer, uint counterValue)
		{
			this.m_WrappedCommandBuffer.SetBufferCounterValue(buffer, counterValue);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005804 File Offset: 0x00003A04
		public void SetupCameraProperties(Camera camera)
		{
			this.m_WrappedCommandBuffer.SetupCameraProperties(camera);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005812 File Offset: 0x00003A12
		public void InvokeOnRenderObjectCallbacks()
		{
			this.m_WrappedCommandBuffer.InvokeOnRenderObjectCallbacks();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000581F File Offset: 0x00003A1F
		public void SetComputeFloatParam(ComputeShader computeShader, string name, float val)
		{
			this.m_WrappedCommandBuffer.SetComputeFloatParam(computeShader, name, val);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000582F File Offset: 0x00003A2F
		public void SetComputeIntParam(ComputeShader computeShader, string name, int val)
		{
			this.m_WrappedCommandBuffer.SetComputeIntParam(computeShader, name, val);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000583F File Offset: 0x00003A3F
		public void SetComputeVectorParam(ComputeShader computeShader, string name, Vector4 val)
		{
			this.m_WrappedCommandBuffer.SetComputeVectorParam(computeShader, name, val);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000584F File Offset: 0x00003A4F
		public void SetComputeVectorArrayParam(ComputeShader computeShader, string name, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeVectorArrayParam(computeShader, name, values);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000585F File Offset: 0x00003A5F
		public void SetComputeMatrixParam(ComputeShader computeShader, string name, Matrix4x4 val)
		{
			this.m_WrappedCommandBuffer.SetComputeMatrixParam(computeShader, name, val);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000586F File Offset: 0x00003A6F
		public void SetComputeMatrixArrayParam(ComputeShader computeShader, string name, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeMatrixArrayParam(computeShader, name, values);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000587F File Offset: 0x00003A7F
		public void SetComputeFloatParams(ComputeShader computeShader, string name, params float[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeFloatParams(computeShader, name, values);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000588F File Offset: 0x00003A8F
		public void SetComputeFloatParams(ComputeShader computeShader, int nameID, params float[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeFloatParams(computeShader, nameID, values);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000589F File Offset: 0x00003A9F
		public void SetComputeIntParams(ComputeShader computeShader, string name, params int[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeIntParams(computeShader, name, values);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000058AF File Offset: 0x00003AAF
		public void SetComputeIntParams(ComputeShader computeShader, int nameID, params int[] values)
		{
			this.m_WrappedCommandBuffer.SetComputeIntParams(computeShader, nameID, values);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000058BF File Offset: 0x00003ABF
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, TextureHandle rt)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, name, rt);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000058D6 File Offset: 0x00003AD6
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, TextureHandle rt)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, nameID, rt);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000058ED File Offset: 0x00003AED
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, TextureHandle rt, int mipLevel)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, name, rt, mipLevel);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005906 File Offset: 0x00003B06
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, TextureHandle rt, int mipLevel)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, nameID, rt, mipLevel);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000591F File Offset: 0x00003B1F
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, TextureHandle rt, int mipLevel, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, name, rt, mipLevel, element);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000593A File Offset: 0x00003B3A
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, TextureHandle rt, int mipLevel, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, nameID, rt, mipLevel, element);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005955 File Offset: 0x00003B55
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005967 File Offset: 0x00003B67
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, name, buffer);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005979 File Offset: 0x00003B79
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBufferHandle bufferHandle)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, nameID, bufferHandle);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000598B File Offset: 0x00003B8B
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBufferHandle bufferHandle)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, name, bufferHandle);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000599D File Offset: 0x00003B9D
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000059AF File Offset: 0x00003BAF
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, name, buffer);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000059C1 File Offset: 0x00003BC1
		public void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, nameID, buffer, offset, size);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000059D5 File Offset: 0x00003BD5
		public void SetComputeConstantBufferParam(ComputeShader computeShader, string name, ComputeBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, name, buffer, offset, size);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000059E9 File Offset: 0x00003BE9
		public void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, nameID, buffer, offset, size);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000059FD File Offset: 0x00003BFD
		public void SetComputeConstantBufferParam(ComputeShader computeShader, string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, name, buffer, offset, size);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005A11 File Offset: 0x00003C11
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			this.m_WrappedCommandBuffer.DispatchCompute(computeShader, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005A25 File Offset: 0x00003C25
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset)
		{
			this.m_WrappedCommandBuffer.DispatchCompute(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005A37 File Offset: 0x00003C37
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset)
		{
			this.m_WrappedCommandBuffer.DispatchCompute(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005A49 File Offset: 0x00003C49
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure)
		{
			this.m_WrappedCommandBuffer.BuildRayTracingAccelerationStructure(accelerationStructure);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005A57 File Offset: 0x00003C57
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure, Vector3 relativeOrigin)
		{
			this.m_WrappedCommandBuffer.BuildRayTracingAccelerationStructure(accelerationStructure, relativeOrigin);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005A66 File Offset: 0x00003C66
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(rayTracingShader, name, rayTracingAccelerationStructure);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005A76 File Offset: 0x00003C76
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(rayTracingShader, nameID, rayTracingAccelerationStructure);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005A86 File Offset: 0x00003C86
		public void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(computeShader, kernelIndex, name, rayTracingAccelerationStructure);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005A98 File Offset: 0x00003C98
		public void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(computeShader, kernelIndex, nameID, rayTracingAccelerationStructure);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005AAA File Offset: 0x00003CAA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, name, buffer);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005ABA File Offset: 0x00003CBA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, nameID, buffer);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005ACA File Offset: 0x00003CCA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, name, buffer);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005ADA File Offset: 0x00003CDA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, nameID, buffer);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005AEA File Offset: 0x00003CEA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBufferHandle bufferHandle)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, name, bufferHandle);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005AFA File Offset: 0x00003CFA
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBufferHandle bufferHandle)
		{
			this.m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, nameID, bufferHandle);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005B0A File Offset: 0x00003D0A
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005B1E File Offset: 0x00003D1E
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, name, buffer, offset, size);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005B32 File Offset: 0x00003D32
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005B46 File Offset: 0x00003D46
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, name, buffer, offset, size);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005B5A File Offset: 0x00003D5A
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, string name, TextureHandle rt)
		{
			this.m_WrappedCommandBuffer.SetRayTracingTextureParam(rayTracingShader, name, rt);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005B6F File Offset: 0x00003D6F
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, int nameID, TextureHandle rt)
		{
			this.m_WrappedCommandBuffer.SetRayTracingTextureParam(rayTracingShader, nameID, rt);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005B84 File Offset: 0x00003D84
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, string name, float val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingFloatParam(rayTracingShader, name, val);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005B94 File Offset: 0x00003D94
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, int nameID, float val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingFloatParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, string name, params float[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingFloatParams(rayTracingShader, name, values);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, int nameID, params float[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingFloatParams(rayTracingShader, nameID, values);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, string name, int val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingIntParam(rayTracingShader, name, val);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, int nameID, int val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingIntParam(rayTracingShader, nameID, val);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, string name, params int[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingIntParams(rayTracingShader, name, values);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, int nameID, params int[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingIntParams(rayTracingShader, nameID, values);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005C04 File Offset: 0x00003E04
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, string name, Vector4 val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingVectorParam(rayTracingShader, name, val);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005C14 File Offset: 0x00003E14
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, int nameID, Vector4 val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingVectorParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005C24 File Offset: 0x00003E24
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, string name, params Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingVectorArrayParam(rayTracingShader, name, values);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005C34 File Offset: 0x00003E34
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, int nameID, params Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingVectorArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005C44 File Offset: 0x00003E44
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, string name, Matrix4x4 val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingMatrixParam(rayTracingShader, name, val);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005C54 File Offset: 0x00003E54
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, int nameID, Matrix4x4 val)
		{
			this.m_WrappedCommandBuffer.SetRayTracingMatrixParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005C64 File Offset: 0x00003E64
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, string name, params Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingMatrixArrayParam(rayTracingShader, name, values);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005C74 File Offset: 0x00003E74
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, int nameID, params Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetRayTracingMatrixArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005C84 File Offset: 0x00003E84
		public void DispatchRays(RayTracingShader rayTracingShader, string rayGenName, uint width, uint height, uint depth, Camera camera)
		{
			this.m_WrappedCommandBuffer.DispatchRays(rayTracingShader, rayGenName, width, height, depth, camera);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005C9A File Offset: 0x00003E9A
		public void CopyCounterValue(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005CAA File Offset: 0x00003EAA
		public void CopyCounterValue(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005CBA File Offset: 0x00003EBA
		public void CopyCounterValue(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005CCA File Offset: 0x00003ECA
		public void CopyCounterValue(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005CDA File Offset: 0x00003EDA
		public void SetGlobalFloat(string name, float value)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloat(name, value);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005CE9 File Offset: 0x00003EE9
		public void SetGlobalInt(string name, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInt(name, value);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public void SetGlobalInteger(string name, int value)
		{
			this.m_WrappedCommandBuffer.SetGlobalInteger(name, value);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005D07 File Offset: 0x00003F07
		public void SetGlobalVector(string name, Vector4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalVector(name, value);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005D16 File Offset: 0x00003F16
		public void SetGlobalColor(string name, Color value)
		{
			this.m_WrappedCommandBuffer.SetGlobalColor(name, value);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005D25 File Offset: 0x00003F25
		public void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrix(name, value);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005D34 File Offset: 0x00003F34
		public void SetGlobalFloatArray(string propertyName, List<float> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(propertyName, values);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005D43 File Offset: 0x00003F43
		public void SetGlobalFloatArray(int nameID, List<float> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(nameID, values);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005D52 File Offset: 0x00003F52
		public void SetGlobalFloatArray(string propertyName, float[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalFloatArray(propertyName, values);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005D61 File Offset: 0x00003F61
		public void SetGlobalVectorArray(string propertyName, List<Vector4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(propertyName, values);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005D70 File Offset: 0x00003F70
		public void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(nameID, values);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005D7F File Offset: 0x00003F7F
		public void SetGlobalVectorArray(string propertyName, Vector4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalVectorArray(propertyName, values);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005D8E File Offset: 0x00003F8E
		public void SetGlobalMatrixArray(string propertyName, List<Matrix4x4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(propertyName, values);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005D9D File Offset: 0x00003F9D
		public void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(nameID, values);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005DAC File Offset: 0x00003FAC
		public void SetGlobalMatrixArray(string propertyName, Matrix4x4[] values)
		{
			this.m_WrappedCommandBuffer.SetGlobalMatrixArray(propertyName, values);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005DBB File Offset: 0x00003FBB
		public void SetGlobalTexture(string name, TextureHandle value)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(name, value);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005DCF File Offset: 0x00003FCF
		public void SetGlobalTexture(int nameID, TextureHandle value)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(nameID, value);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005DE3 File Offset: 0x00003FE3
		public void SetGlobalTexture(string name, TextureHandle value, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(name, value, element);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public void SetGlobalTexture(int nameID, TextureHandle value, RenderTextureSubElement element)
		{
			this.m_WrappedCommandBuffer.SetGlobalTexture(nameID, value, element);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005E0D File Offset: 0x0000400D
		public void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(name, value);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005E1C File Offset: 0x0000401C
		public void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(nameID, value);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005E2B File Offset: 0x0000402B
		public void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(name, value);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005E3A File Offset: 0x0000403A
		public void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			this.m_WrappedCommandBuffer.SetGlobalBuffer(nameID, value);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005E49 File Offset: 0x00004049
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, int nameID, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, nameID, offset, size);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005E5B File Offset: 0x0000405B
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, string name, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, name, offset, size);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005E6D File Offset: 0x0000406D
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, int nameID, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, nameID, offset, size);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005E7F File Offset: 0x0000407F
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, string name, int offset, int size)
		{
			this.m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, name, offset, size);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005E91 File Offset: 0x00004091
		public void SetShadowSamplingMode(RenderTargetIdentifier shadowmap, ShadowSamplingMode mode)
		{
			this.m_WrappedCommandBuffer.SetShadowSamplingMode(shadowmap, mode);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005EA0 File Offset: 0x000040A0
		public void SetSinglePassStereo(SinglePassStereoMode mode)
		{
			this.m_WrappedCommandBuffer.SetSinglePassStereo(mode);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005EAE File Offset: 0x000040AE
		public void IssuePluginEvent(IntPtr callback, int eventID)
		{
			this.m_WrappedCommandBuffer.IssuePluginEvent(callback, eventID);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005EBD File Offset: 0x000040BD
		public void IssuePluginEventAndData(IntPtr callback, int eventID, IntPtr data)
		{
			this.m_WrappedCommandBuffer.IssuePluginEventAndData(callback, eventID, data);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005ECD File Offset: 0x000040CD
		public void IssuePluginCustomBlit(IntPtr callback, uint command, RenderTargetIdentifier source, RenderTargetIdentifier dest, uint commandParam, uint commandFlags)
		{
			this.m_WrappedCommandBuffer.IssuePluginCustomBlit(callback, command, source, dest, commandParam, commandFlags);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005EE3 File Offset: 0x000040E3
		public void IssuePluginCustomTextureUpdateV2(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.m_WrappedCommandBuffer.IssuePluginCustomTextureUpdateV2(callback, targetTexture, userData);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005EF3 File Offset: 0x000040F3
		void IBaseCommandBuffer.EnableKeyword(in GlobalKeyword keyword)
		{
			this.EnableKeyword(in keyword);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005EFC File Offset: 0x000040FC
		void IBaseCommandBuffer.EnableKeyword(Material material, in LocalKeyword keyword)
		{
			this.EnableKeyword(material, in keyword);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005F06 File Offset: 0x00004106
		void IBaseCommandBuffer.EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.EnableKeyword(computeShader, in keyword);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005F10 File Offset: 0x00004110
		void IBaseCommandBuffer.DisableKeyword(in GlobalKeyword keyword)
		{
			this.DisableKeyword(in keyword);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005F19 File Offset: 0x00004119
		void IBaseCommandBuffer.DisableKeyword(Material material, in LocalKeyword keyword)
		{
			this.DisableKeyword(material, in keyword);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005F23 File Offset: 0x00004123
		void IBaseCommandBuffer.DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.DisableKeyword(computeShader, in keyword);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005F2D File Offset: 0x0000412D
		void IBaseCommandBuffer.SetKeyword(in GlobalKeyword keyword, bool value)
		{
			this.SetKeyword(in keyword, value);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005F37 File Offset: 0x00004137
		void IBaseCommandBuffer.SetKeyword(Material material, in LocalKeyword keyword, bool value)
		{
			this.SetKeyword(material, in keyword, value);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005F42 File Offset: 0x00004142
		void IBaseCommandBuffer.SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value)
		{
			this.SetKeyword(computeShader, in keyword, value);
		}
	}
}
