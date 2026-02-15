using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x02000022 RID: 34
	public interface IComputeCommandBuffer : IBaseCommandBuffer
	{
		// Token: 0x060001BD RID: 445
		void SetComputeFloatParam(ComputeShader computeShader, int nameID, float val);

		// Token: 0x060001BE RID: 446
		void SetComputeIntParam(ComputeShader computeShader, int nameID, int val);

		// Token: 0x060001BF RID: 447
		void SetComputeVectorParam(ComputeShader computeShader, int nameID, Vector4 val);

		// Token: 0x060001C0 RID: 448
		void SetComputeVectorArrayParam(ComputeShader computeShader, int nameID, Vector4[] values);

		// Token: 0x060001C1 RID: 449
		void SetComputeMatrixParam(ComputeShader computeShader, int nameID, Matrix4x4 val);

		// Token: 0x060001C2 RID: 450
		void SetComputeMatrixArrayParam(ComputeShader computeShader, int nameID, Matrix4x4[] values);

		// Token: 0x060001C3 RID: 451
		void SetBufferData(ComputeBuffer buffer, Array data);

		// Token: 0x060001C4 RID: 452
		void SetBufferData<T>(ComputeBuffer buffer, List<T> data) where T : struct;

		// Token: 0x060001C5 RID: 453
		void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data) where T : struct;

		// Token: 0x060001C6 RID: 454
		void SetBufferData(ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count);

		// Token: 0x060001C7 RID: 455
		void SetBufferData<T>(ComputeBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct;

		// Token: 0x060001C8 RID: 456
		void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct;

		// Token: 0x060001C9 RID: 457
		void SetBufferCounterValue(ComputeBuffer buffer, uint counterValue);

		// Token: 0x060001CA RID: 458
		void SetBufferData(GraphicsBuffer buffer, Array data);

		// Token: 0x060001CB RID: 459
		void SetBufferData<T>(GraphicsBuffer buffer, List<T> data) where T : struct;

		// Token: 0x060001CC RID: 460
		void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data) where T : struct;

		// Token: 0x060001CD RID: 461
		void SetBufferData(GraphicsBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count);

		// Token: 0x060001CE RID: 462
		void SetBufferData<T>(GraphicsBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct;

		// Token: 0x060001CF RID: 463
		void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct;

		// Token: 0x060001D0 RID: 464
		void SetBufferCounterValue(GraphicsBuffer buffer, uint counterValue);

		// Token: 0x060001D1 RID: 465
		void SetComputeFloatParam(ComputeShader computeShader, string name, float val);

		// Token: 0x060001D2 RID: 466
		void SetComputeIntParam(ComputeShader computeShader, string name, int val);

		// Token: 0x060001D3 RID: 467
		void SetComputeVectorParam(ComputeShader computeShader, string name, Vector4 val);

		// Token: 0x060001D4 RID: 468
		void SetComputeVectorArrayParam(ComputeShader computeShader, string name, Vector4[] values);

		// Token: 0x060001D5 RID: 469
		void SetComputeMatrixParam(ComputeShader computeShader, string name, Matrix4x4 val);

		// Token: 0x060001D6 RID: 470
		void SetComputeMatrixArrayParam(ComputeShader computeShader, string name, Matrix4x4[] values);

		// Token: 0x060001D7 RID: 471
		void SetComputeFloatParams(ComputeShader computeShader, string name, params float[] values);

		// Token: 0x060001D8 RID: 472
		void SetComputeFloatParams(ComputeShader computeShader, int nameID, params float[] values);

		// Token: 0x060001D9 RID: 473
		void SetComputeIntParams(ComputeShader computeShader, string name, params int[] values);

		// Token: 0x060001DA RID: 474
		void SetComputeIntParams(ComputeShader computeShader, int nameID, params int[] values);

		// Token: 0x060001DB RID: 475
		void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, TextureHandle rt);

		// Token: 0x060001DC RID: 476
		void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, TextureHandle rt);

		// Token: 0x060001DD RID: 477
		void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, TextureHandle rt, int mipLevel);

		// Token: 0x060001DE RID: 478
		void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, TextureHandle rt, int mipLevel);

		// Token: 0x060001DF RID: 479
		void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, TextureHandle rt, int mipLevel, RenderTextureSubElement element);

		// Token: 0x060001E0 RID: 480
		void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, TextureHandle rt, int mipLevel, RenderTextureSubElement element);

		// Token: 0x060001E1 RID: 481
		void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer);

		// Token: 0x060001E2 RID: 482
		void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, ComputeBuffer buffer);

		// Token: 0x060001E3 RID: 483
		void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBufferHandle bufferHandle);

		// Token: 0x060001E4 RID: 484
		void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBufferHandle bufferHandle);

		// Token: 0x060001E5 RID: 485
		void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer);

		// Token: 0x060001E6 RID: 486
		void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBuffer buffer);

		// Token: 0x060001E7 RID: 487
		void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, ComputeBuffer buffer, int offset, int size);

		// Token: 0x060001E8 RID: 488
		void SetComputeConstantBufferParam(ComputeShader computeShader, string name, ComputeBuffer buffer, int offset, int size);

		// Token: 0x060001E9 RID: 489
		void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, GraphicsBuffer buffer, int offset, int size);

		// Token: 0x060001EA RID: 490
		void SetComputeConstantBufferParam(ComputeShader computeShader, string name, GraphicsBuffer buffer, int offset, int size);

		// Token: 0x060001EB RID: 491
		void DispatchCompute(ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		// Token: 0x060001EC RID: 492
		void DispatchCompute(ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset);

		// Token: 0x060001ED RID: 493
		void DispatchCompute(ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset);

		// Token: 0x060001EE RID: 494
		void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure);

		// Token: 0x060001EF RID: 495
		void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure, Vector3 relativeOrigin);

		// Token: 0x060001F0 RID: 496
		void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure);

		// Token: 0x060001F1 RID: 497
		void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure);

		// Token: 0x060001F2 RID: 498
		void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure);

		// Token: 0x060001F3 RID: 499
		void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure);

		// Token: 0x060001F4 RID: 500
		void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer);

		// Token: 0x060001F5 RID: 501
		void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer);

		// Token: 0x060001F6 RID: 502
		void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer);

		// Token: 0x060001F7 RID: 503
		void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer);

		// Token: 0x060001F8 RID: 504
		void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBufferHandle bufferHandle);

		// Token: 0x060001F9 RID: 505
		void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBufferHandle bufferHandle);

		// Token: 0x060001FA RID: 506
		void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer, int offset, int size);

		// Token: 0x060001FB RID: 507
		void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer, int offset, int size);

		// Token: 0x060001FC RID: 508
		void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer, int offset, int size);

		// Token: 0x060001FD RID: 509
		void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer, int offset, int size);

		// Token: 0x060001FE RID: 510
		void SetRayTracingTextureParam(RayTracingShader rayTracingShader, string name, TextureHandle rt);

		// Token: 0x060001FF RID: 511
		void SetRayTracingTextureParam(RayTracingShader rayTracingShader, int nameID, TextureHandle rt);

		// Token: 0x06000200 RID: 512
		void SetRayTracingFloatParam(RayTracingShader rayTracingShader, string name, float val);

		// Token: 0x06000201 RID: 513
		void SetRayTracingFloatParam(RayTracingShader rayTracingShader, int nameID, float val);

		// Token: 0x06000202 RID: 514
		void SetRayTracingFloatParams(RayTracingShader rayTracingShader, string name, params float[] values);

		// Token: 0x06000203 RID: 515
		void SetRayTracingFloatParams(RayTracingShader rayTracingShader, int nameID, params float[] values);

		// Token: 0x06000204 RID: 516
		void SetRayTracingIntParam(RayTracingShader rayTracingShader, string name, int val);

		// Token: 0x06000205 RID: 517
		void SetRayTracingIntParam(RayTracingShader rayTracingShader, int nameID, int val);

		// Token: 0x06000206 RID: 518
		void SetRayTracingIntParams(RayTracingShader rayTracingShader, string name, params int[] values);

		// Token: 0x06000207 RID: 519
		void SetRayTracingIntParams(RayTracingShader rayTracingShader, int nameID, params int[] values);

		// Token: 0x06000208 RID: 520
		void SetRayTracingVectorParam(RayTracingShader rayTracingShader, string name, Vector4 val);

		// Token: 0x06000209 RID: 521
		void SetRayTracingVectorParam(RayTracingShader rayTracingShader, int nameID, Vector4 val);

		// Token: 0x0600020A RID: 522
		void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, string name, params Vector4[] values);

		// Token: 0x0600020B RID: 523
		void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, int nameID, params Vector4[] values);

		// Token: 0x0600020C RID: 524
		void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, string name, Matrix4x4 val);

		// Token: 0x0600020D RID: 525
		void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, int nameID, Matrix4x4 val);

		// Token: 0x0600020E RID: 526
		void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, string name, params Matrix4x4[] values);

		// Token: 0x0600020F RID: 527
		void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, int nameID, params Matrix4x4[] values);

		// Token: 0x06000210 RID: 528
		void DispatchRays(RayTracingShader rayTracingShader, string rayGenName, uint width, uint height, uint depth, Camera camera);

		// Token: 0x06000211 RID: 529
		void CopyCounterValue(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes);

		// Token: 0x06000212 RID: 530
		void CopyCounterValue(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes);

		// Token: 0x06000213 RID: 531
		void CopyCounterValue(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes);

		// Token: 0x06000214 RID: 532
		void CopyCounterValue(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes);
	}
}
