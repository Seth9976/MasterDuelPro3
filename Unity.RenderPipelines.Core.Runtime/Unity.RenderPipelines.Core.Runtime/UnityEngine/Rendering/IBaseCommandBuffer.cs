using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Profiling;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x02000021 RID: 33
	public interface IBaseCommandBuffer
	{
		// Token: 0x06000176 RID: 374
		void SetInvertCulling(bool invertCulling);

		// Token: 0x06000177 RID: 375
		void SetViewport(Rect pixelRect);

		// Token: 0x06000178 RID: 376
		void EnableScissorRect(Rect scissor);

		// Token: 0x06000179 RID: 377
		void DisableScissorRect();

		// Token: 0x0600017A RID: 378
		void SetGlobalFloat(int nameID, float value);

		// Token: 0x0600017B RID: 379
		void SetGlobalInt(int nameID, int value);

		// Token: 0x0600017C RID: 380
		void SetGlobalInteger(int nameID, int value);

		// Token: 0x0600017D RID: 381
		void SetGlobalVector(int nameID, Vector4 value);

		// Token: 0x0600017E RID: 382
		void SetGlobalColor(int nameID, Color value);

		// Token: 0x0600017F RID: 383
		void SetGlobalMatrix(int nameID, Matrix4x4 value);

		// Token: 0x06000180 RID: 384
		void EnableShaderKeyword(string keyword);

		// Token: 0x06000181 RID: 385
		void EnableKeyword(in GlobalKeyword keyword);

		// Token: 0x06000182 RID: 386
		void EnableKeyword(Material material, in LocalKeyword keyword);

		// Token: 0x06000183 RID: 387
		void EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword);

		// Token: 0x06000184 RID: 388
		void DisableShaderKeyword(string keyword);

		// Token: 0x06000185 RID: 389
		void DisableKeyword(in GlobalKeyword keyword);

		// Token: 0x06000186 RID: 390
		void DisableKeyword(Material material, in LocalKeyword keyword);

		// Token: 0x06000187 RID: 391
		void DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword);

		// Token: 0x06000188 RID: 392
		void SetKeyword(in GlobalKeyword keyword, bool value);

		// Token: 0x06000189 RID: 393
		void SetKeyword(Material material, in LocalKeyword keyword, bool value);

		// Token: 0x0600018A RID: 394
		void SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value);

		// Token: 0x0600018B RID: 395
		void SetViewProjectionMatrices(Matrix4x4 view, Matrix4x4 proj);

		// Token: 0x0600018C RID: 396
		void SetGlobalDepthBias(float bias, float slopeBias);

		// Token: 0x0600018D RID: 397
		void SetGlobalFloatArray(int nameID, float[] values);

		// Token: 0x0600018E RID: 398
		void SetGlobalVectorArray(int nameID, Vector4[] values);

		// Token: 0x0600018F RID: 399
		void SetGlobalMatrixArray(int nameID, Matrix4x4[] values);

		// Token: 0x06000190 RID: 400
		void SetLateLatchProjectionMatrices(Matrix4x4[] projectionMat);

		// Token: 0x06000191 RID: 401
		void MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID);

		// Token: 0x06000192 RID: 402
		void UnmarkLateLatchMatrix(CameraLateLatchMatrixType matrixPropertyType);

		// Token: 0x06000193 RID: 403
		void BeginSample(string name);

		// Token: 0x06000194 RID: 404
		void EndSample(string name);

		// Token: 0x06000195 RID: 405
		void BeginSample(CustomSampler sampler);

		// Token: 0x06000196 RID: 406
		void EndSample(CustomSampler sampler);

		// Token: 0x06000197 RID: 407
		void BeginSample(ProfilerMarker marker);

		// Token: 0x06000198 RID: 408
		void EndSample(ProfilerMarker marker);

		// Token: 0x06000199 RID: 409
		void IncrementUpdateCount(RenderTargetIdentifier dest);

		// Token: 0x0600019A RID: 410
		void SetupCameraProperties(Camera camera);

		// Token: 0x0600019B RID: 411
		void InvokeOnRenderObjectCallbacks();

		// Token: 0x0600019C RID: 412
		void SetGlobalFloat(string name, float value);

		// Token: 0x0600019D RID: 413
		void SetGlobalInt(string name, int value);

		// Token: 0x0600019E RID: 414
		void SetGlobalInteger(string name, int value);

		// Token: 0x0600019F RID: 415
		void SetGlobalVector(string name, Vector4 value);

		// Token: 0x060001A0 RID: 416
		void SetGlobalColor(string name, Color value);

		// Token: 0x060001A1 RID: 417
		void SetGlobalMatrix(string name, Matrix4x4 value);

		// Token: 0x060001A2 RID: 418
		void SetGlobalFloatArray(string propertyName, List<float> values);

		// Token: 0x060001A3 RID: 419
		void SetGlobalFloatArray(int nameID, List<float> values);

		// Token: 0x060001A4 RID: 420
		void SetGlobalFloatArray(string propertyName, float[] values);

		// Token: 0x060001A5 RID: 421
		void SetGlobalVectorArray(string propertyName, List<Vector4> values);

		// Token: 0x060001A6 RID: 422
		void SetGlobalVectorArray(int nameID, List<Vector4> values);

		// Token: 0x060001A7 RID: 423
		void SetGlobalVectorArray(string propertyName, Vector4[] values);

		// Token: 0x060001A8 RID: 424
		void SetGlobalMatrixArray(string propertyName, List<Matrix4x4> values);

		// Token: 0x060001A9 RID: 425
		void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values);

		// Token: 0x060001AA RID: 426
		void SetGlobalMatrixArray(string propertyName, Matrix4x4[] values);

		// Token: 0x060001AB RID: 427
		void SetGlobalTexture(string name, TextureHandle value);

		// Token: 0x060001AC RID: 428
		void SetGlobalTexture(int nameID, TextureHandle value);

		// Token: 0x060001AD RID: 429
		void SetGlobalTexture(string name, TextureHandle value, RenderTextureSubElement element);

		// Token: 0x060001AE RID: 430
		void SetGlobalTexture(int nameID, TextureHandle value, RenderTextureSubElement element);

		// Token: 0x060001AF RID: 431
		void SetGlobalBuffer(string name, ComputeBuffer value);

		// Token: 0x060001B0 RID: 432
		void SetGlobalBuffer(int nameID, ComputeBuffer value);

		// Token: 0x060001B1 RID: 433
		void SetGlobalBuffer(string name, GraphicsBuffer value);

		// Token: 0x060001B2 RID: 434
		void SetGlobalBuffer(int nameID, GraphicsBuffer value);

		// Token: 0x060001B3 RID: 435
		void SetGlobalConstantBuffer(ComputeBuffer buffer, int nameID, int offset, int size);

		// Token: 0x060001B4 RID: 436
		void SetGlobalConstantBuffer(ComputeBuffer buffer, string name, int offset, int size);

		// Token: 0x060001B5 RID: 437
		void SetGlobalConstantBuffer(GraphicsBuffer buffer, int nameID, int offset, int size);

		// Token: 0x060001B6 RID: 438
		void SetGlobalConstantBuffer(GraphicsBuffer buffer, string name, int offset, int size);

		// Token: 0x060001B7 RID: 439
		void SetShadowSamplingMode(RenderTargetIdentifier shadowmap, ShadowSamplingMode mode);

		// Token: 0x060001B8 RID: 440
		void SetSinglePassStereo(SinglePassStereoMode mode);

		// Token: 0x060001B9 RID: 441
		void IssuePluginEvent(IntPtr callback, int eventID);

		// Token: 0x060001BA RID: 442
		void IssuePluginEventAndData(IntPtr callback, int eventID, IntPtr data);

		// Token: 0x060001BB RID: 443
		void IssuePluginCustomBlit(IntPtr callback, uint command, RenderTargetIdentifier source, RenderTargetIdentifier dest, uint commandParam, uint commandFlags);

		// Token: 0x060001BC RID: 444
		void IssuePluginCustomTextureUpdateV2(IntPtr callback, Texture targetTexture, uint userData);
	}
}
