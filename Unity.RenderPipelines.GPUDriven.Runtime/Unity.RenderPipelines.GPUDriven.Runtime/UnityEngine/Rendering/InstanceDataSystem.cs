using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000074 RID: 116
	internal class InstanceDataSystem : IDisposable
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000D370 File Offset: 0x0000B570
		private unsafe static int AtomicAddLengthNoResize<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(in NativeList<T> list, int count) where T : struct, ValueType
		{
			NativeList<T> nativeList = list;
			return Interlocked.Add(ref nativeList.GetUnsafeList()->m_length, count) - count;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000D398 File Offset: 0x0000B598
		public bool hasBoundingSpheres
		{
			get
			{
				return this.m_EnableBoundingSpheres;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public CPUInstanceData.ReadOnly instanceData
		{
			get
			{
				return this.m_InstanceData.AsReadOnly();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000D3AD File Offset: 0x0000B5AD
		public CPUSharedInstanceData.ReadOnly sharedInstanceData
		{
			get
			{
				return this.m_SharedInstanceData.AsReadOnly();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000D3BA File Offset: 0x0000B5BA
		public NativeArray<InstanceHandle> aliveInstances
		{
			get
			{
				return this.m_InstanceData.instances.GetSubArray(0, this.m_InstanceData.instancesLength);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		public InstanceDataSystem(int maxInstances, bool enableBoundingSpheres, GPUResidentDrawerResources resources)
		{
			this.m_InstanceAllocators = default(InstanceAllocators);
			this.m_SharedInstanceData = default(CPUSharedInstanceData);
			this.m_InstanceData = default(CPUInstanceData);
			this.m_InstanceAllocators.Initialize();
			this.m_SharedInstanceData.Initialize(maxInstances);
			this.m_InstanceData.Initialize(maxInstances);
			this.m_RendererGroupInstanceMultiHash = new NativeParallelMultiHashMap<int, InstanceHandle>(maxInstances, Allocator.Persistent);
			this.m_TransformUpdateCS = resources.transformUpdaterKernels;
			this.m_WindDataUpdateCS = resources.windDataUpdaterKernels;
			this.m_TransformInitKernel = this.m_TransformUpdateCS.FindKernel("ScatterInitTransformMain");
			this.m_TransformUpdateKernel = this.m_TransformUpdateCS.FindKernel("ScatterUpdateTransformMain");
			this.m_MotionUpdateKernel = this.m_TransformUpdateCS.FindKernel("ScatterUpdateMotionMain");
			this.m_ProbeUpdateKernel = this.m_TransformUpdateCS.FindKernel("ScatterUpdateProbesMain");
			if (enableBoundingSpheres)
			{
				this.m_TransformUpdateCS.EnableKeyword("PROCESS_BOUNDING_SPHERES");
			}
			else
			{
				this.m_TransformUpdateCS.DisableKeyword("PROCESS_BOUNDING_SPHERES");
			}
			this.m_WindDataCopyHistoryKernel = this.m_WindDataUpdateCS.FindKernel("WindDataCopyHistoryMain");
			this.m_EnableBoundingSpheres = enableBoundingSpheres;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D504 File Offset: 0x0000B704
		public void Dispose()
		{
			this.m_InstanceAllocators.Dispose();
			this.m_SharedInstanceData.Dispose();
			this.m_InstanceData.Dispose();
			this.m_RendererGroupInstanceMultiHash.Dispose();
			ComputeBuffer updateIndexQueueBuffer = this.m_UpdateIndexQueueBuffer;
			if (updateIndexQueueBuffer != null)
			{
				updateIndexQueueBuffer.Dispose();
			}
			ComputeBuffer probeUpdateDataQueueBuffer = this.m_ProbeUpdateDataQueueBuffer;
			if (probeUpdateDataQueueBuffer != null)
			{
				probeUpdateDataQueueBuffer.Dispose();
			}
			ComputeBuffer probeOcclusionUpdateDataQueueBuffer = this.m_ProbeOcclusionUpdateDataQueueBuffer;
			if (probeOcclusionUpdateDataQueueBuffer != null)
			{
				probeOcclusionUpdateDataQueueBuffer.Dispose();
			}
			ComputeBuffer transformUpdateDataQueueBuffer = this.m_TransformUpdateDataQueueBuffer;
			if (transformUpdateDataQueueBuffer != null)
			{
				transformUpdateDataQueueBuffer.Dispose();
			}
			ComputeBuffer boundingSpheresUpdateDataQueueBuffer = this.m_BoundingSpheresUpdateDataQueueBuffer;
			if (boundingSpheresUpdateDataQueueBuffer == null)
			{
				return;
			}
			boundingSpheresUpdateDataQueueBuffer.Dispose();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D591 File Offset: 0x0000B791
		public int GetMaxInstancesOfType(InstanceType instanceType)
		{
			return this.m_InstanceAllocators.GetInstanceHandlesLength(instanceType);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D59F File Offset: 0x0000B79F
		public int GetAliveInstancesOfType(InstanceType instanceType)
		{
			return this.m_InstanceAllocators.GetInstancesLength(instanceType);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D5AD File Offset: 0x0000B7AD
		private void EnsureIndexQueueBufferCapacity(int capacity)
		{
			if (this.m_UpdateIndexQueueBuffer == null || this.m_UpdateIndexQueueBuffer.count < capacity)
			{
				ComputeBuffer updateIndexQueueBuffer = this.m_UpdateIndexQueueBuffer;
				if (updateIndexQueueBuffer != null)
				{
					updateIndexQueueBuffer.Dispose();
				}
				this.m_UpdateIndexQueueBuffer = new ComputeBuffer(capacity, 4, ComputeBufferType.Raw);
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		private void EnsureProbeBuffersCapacity(int capacity)
		{
			this.EnsureIndexQueueBufferCapacity(capacity);
			if (this.m_ProbeUpdateDataQueueBuffer == null || this.m_ProbeUpdateDataQueueBuffer.count < capacity)
			{
				ComputeBuffer probeUpdateDataQueueBuffer = this.m_ProbeUpdateDataQueueBuffer;
				if (probeUpdateDataQueueBuffer != null)
				{
					probeUpdateDataQueueBuffer.Dispose();
				}
				ComputeBuffer probeOcclusionUpdateDataQueueBuffer = this.m_ProbeOcclusionUpdateDataQueueBuffer;
				if (probeOcclusionUpdateDataQueueBuffer != null)
				{
					probeOcclusionUpdateDataQueueBuffer.Dispose();
				}
				this.m_ProbeUpdateDataQueueBuffer = new ComputeBuffer(capacity, Marshal.SizeOf<SHUpdatePacket>(), ComputeBufferType.Structured);
				this.m_ProbeOcclusionUpdateDataQueueBuffer = new ComputeBuffer(capacity, Marshal.SizeOf<Vector4>(), ComputeBufferType.Structured);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D658 File Offset: 0x0000B858
		private void EnsureTransformBuffersCapacity(int capacity)
		{
			this.EnsureIndexQueueBufferCapacity(capacity);
			int transformsCapacity = capacity * 2;
			if (this.m_TransformUpdateDataQueueBuffer == null || this.m_TransformUpdateDataQueueBuffer.count < transformsCapacity)
			{
				ComputeBuffer transformUpdateDataQueueBuffer = this.m_TransformUpdateDataQueueBuffer;
				if (transformUpdateDataQueueBuffer != null)
				{
					transformUpdateDataQueueBuffer.Dispose();
				}
				ComputeBuffer boundingSpheresUpdateDataQueueBuffer = this.m_BoundingSpheresUpdateDataQueueBuffer;
				if (boundingSpheresUpdateDataQueueBuffer != null)
				{
					boundingSpheresUpdateDataQueueBuffer.Dispose();
				}
				this.m_TransformUpdateDataQueueBuffer = new ComputeBuffer(transformsCapacity, Marshal.SizeOf<TransformUpdatePacket>(), ComputeBufferType.Structured);
				if (this.m_EnableBoundingSpheres)
				{
					this.m_BoundingSpheresUpdateDataQueueBuffer = new ComputeBuffer(capacity, Marshal.SizeOf<float4>(), ComputeBufferType.Structured);
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		private JobHandle ScheduleInterpolateProbesAndUpdateTetrahedronCache(int queueCount, NativeArray<InstanceHandle> probeUpdateInstanceQueue, NativeArray<int> compactTetrahedronCache, NativeArray<Vector3> probeQueryPosition, NativeArray<SphericalHarmonicsL2> probeUpdateDataQueue, NativeArray<Vector4> probeOcclusionUpdateDataQueue)
		{
			LightProbesQuery lightProbesQuery = new LightProbesQuery(Allocator.TempJob);
			InstanceDataSystem.CalculateInterpolatedLightAndOcclusionProbesBatchJob calculateInterpolatedLightAndOcclusionProbesBatchJob = new InstanceDataSystem.CalculateInterpolatedLightAndOcclusionProbesBatchJob
			{
				lightProbesQuery = lightProbesQuery,
				probesCount = queueCount,
				queryPostitions = probeQueryPosition,
				compactTetrahedronCache = compactTetrahedronCache,
				probesSphericalHarmonics = probeUpdateDataQueue,
				probesOcclusion = probeOcclusionUpdateDataQueue
			};
			int totalBatchCount = 1 + queueCount / 8;
			JobHandle calculateProbesJobHandle = calculateInterpolatedLightAndOcclusionProbesBatchJob.Schedule(totalBatchCount, 1, default(JobHandle));
			lightProbesQuery.Dispose(calculateProbesJobHandle);
			return new InstanceDataSystem.ScatterTetrahedronCacheIndicesJob
			{
				compactTetrahedronCache = compactTetrahedronCache,
				probeInstances = probeUpdateInstanceQueue,
				instanceData = this.m_InstanceData
			}.Schedule(queueCount, 128, calculateProbesJobHandle);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D780 File Offset: 0x0000B980
		private void DispatchProbeUpdateCommand(int queueCount, NativeArray<InstanceHandle> probeInstanceQueue, NativeArray<SphericalHarmonicsL2> probeUpdateDataQueue, NativeArray<Vector4> probeOcclusionUpdateDataQueue, RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			this.EnsureProbeBuffersCapacity(queueCount);
			NativeArray<GPUInstanceIndex> gpuInstanceIndices = new NativeArray<GPUInstanceIndex>(queueCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			outputBuffer.CPUInstanceArrayToGPUInstanceArray(probeInstanceQueue.GetSubArray(0, queueCount), gpuInstanceIndices);
			this.m_UpdateIndexQueueBuffer.SetData<GPUInstanceIndex>(gpuInstanceIndices, 0, 0, queueCount);
			this.m_ProbeUpdateDataQueueBuffer.SetData<SphericalHarmonicsL2>(probeUpdateDataQueue, 0, 0, queueCount);
			this.m_ProbeOcclusionUpdateDataQueueBuffer.SetData<Vector4>(probeOcclusionUpdateDataQueue, 0, 0, queueCount);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._ProbeUpdateQueueCount, queueCount);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._SHUpdateVec4Offset, renderersParameters.shCoefficients.uintOffset);
			this.m_TransformUpdateCS.SetBuffer(this.m_ProbeUpdateKernel, InstanceDataSystem.InstanceTransformUpdateIDs._ProbeUpdateIndexQueue, this.m_UpdateIndexQueueBuffer);
			this.m_TransformUpdateCS.SetBuffer(this.m_ProbeUpdateKernel, InstanceDataSystem.InstanceTransformUpdateIDs._ProbeUpdateDataQueue, this.m_ProbeUpdateDataQueueBuffer);
			this.m_TransformUpdateCS.SetBuffer(this.m_ProbeUpdateKernel, InstanceDataSystem.InstanceTransformUpdateIDs._ProbeOcclusionUpdateDataQueue, this.m_ProbeOcclusionUpdateDataQueueBuffer);
			this.m_TransformUpdateCS.SetBuffer(this.m_ProbeUpdateKernel, InstanceDataSystem.InstanceTransformUpdateIDs._OutputProbeBuffer, outputBuffer.gpuBuffer);
			this.m_TransformUpdateCS.Dispatch(this.m_ProbeUpdateKernel, (queueCount + 63) / 64, 1, 1);
			gpuInstanceIndices.Dispose();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D89C File Offset: 0x0000BA9C
		private void DispatchMotionUpdateCommand(int motionQueueCount, NativeArray<InstanceHandle> transformInstanceQueue, RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			this.EnsureTransformBuffersCapacity(motionQueueCount);
			NativeArray<GPUInstanceIndex> gpuInstanceIndices = new NativeArray<GPUInstanceIndex>(motionQueueCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			outputBuffer.CPUInstanceArrayToGPUInstanceArray(transformInstanceQueue.GetSubArray(0, motionQueueCount), gpuInstanceIndices);
			this.m_UpdateIndexQueueBuffer.SetData<GPUInstanceIndex>(gpuInstanceIndices, 0, 0, motionQueueCount);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateQueueCount, motionQueueCount);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputL2WVec4Offset, renderersParameters.localToWorld.uintOffset);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputW2LVec4Offset, renderersParameters.worldToLocal.uintOffset);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputPrevL2WVec4Offset, renderersParameters.matrixPreviousM.uintOffset);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputPrevW2LVec4Offset, renderersParameters.matrixPreviousMI.uintOffset);
			this.m_TransformUpdateCS.SetBuffer(this.m_MotionUpdateKernel, InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateIndexQueue, this.m_UpdateIndexQueueBuffer);
			this.m_TransformUpdateCS.SetBuffer(this.m_MotionUpdateKernel, InstanceDataSystem.InstanceTransformUpdateIDs._OutputTransformBuffer, outputBuffer.gpuBuffer);
			this.m_TransformUpdateCS.Dispatch(this.m_MotionUpdateKernel, (motionQueueCount + 63) / 64, 1, 1);
			gpuInstanceIndices.Dispose();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		private void DispatchTransformUpdateCommand(bool initialize, int transformQueueCount, NativeArray<InstanceHandle> transformInstanceQueue, NativeArray<TransformUpdatePacket> updateDataQueue, NativeArray<float4> boundingSphereUpdateDataQueue, RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			this.EnsureTransformBuffersCapacity(transformQueueCount);
			int transformQueueDataSize;
			int kernel;
			if (initialize)
			{
				transformQueueDataSize = transformQueueCount * 2;
				kernel = this.m_TransformInitKernel;
			}
			else
			{
				transformQueueDataSize = transformQueueCount;
				kernel = this.m_TransformUpdateKernel;
			}
			NativeArray<GPUInstanceIndex> gpuInstanceIndices = new NativeArray<GPUInstanceIndex>(transformQueueCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			outputBuffer.CPUInstanceArrayToGPUInstanceArray(transformInstanceQueue.GetSubArray(0, transformQueueCount), gpuInstanceIndices);
			this.m_UpdateIndexQueueBuffer.SetData<GPUInstanceIndex>(gpuInstanceIndices, 0, 0, transformQueueCount);
			this.m_TransformUpdateDataQueueBuffer.SetData<TransformUpdatePacket>(updateDataQueue, 0, 0, transformQueueDataSize);
			if (this.m_EnableBoundingSpheres)
			{
				this.m_BoundingSpheresUpdateDataQueueBuffer.SetData<float4>(boundingSphereUpdateDataQueue, 0, 0, transformQueueCount);
			}
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateQueueCount, transformQueueCount);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputL2WVec4Offset, renderersParameters.localToWorld.uintOffset);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputW2LVec4Offset, renderersParameters.worldToLocal.uintOffset);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputPrevL2WVec4Offset, renderersParameters.matrixPreviousM.uintOffset);
			this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateOutputPrevW2LVec4Offset, renderersParameters.matrixPreviousMI.uintOffset);
			this.m_TransformUpdateCS.SetBuffer(kernel, InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateIndexQueue, this.m_UpdateIndexQueueBuffer);
			this.m_TransformUpdateCS.SetBuffer(kernel, InstanceDataSystem.InstanceTransformUpdateIDs._TransformUpdateDataQueue, this.m_TransformUpdateDataQueueBuffer);
			if (this.m_EnableBoundingSpheres)
			{
				this.m_TransformUpdateCS.SetInt(InstanceDataSystem.InstanceTransformUpdateIDs._BoundingSphereOutputVec4Offset, renderersParameters.boundingSphere.uintOffset);
				this.m_TransformUpdateCS.SetBuffer(kernel, InstanceDataSystem.InstanceTransformUpdateIDs._BoundingSphereDataQueue, this.m_BoundingSpheresUpdateDataQueueBuffer);
			}
			this.m_TransformUpdateCS.SetBuffer(kernel, InstanceDataSystem.InstanceTransformUpdateIDs._OutputTransformBuffer, outputBuffer.gpuBuffer);
			this.m_TransformUpdateCS.Dispatch(kernel, (transformQueueCount + 63) / 64, 1, 1);
			gpuInstanceIndices.Dispose();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000DB54 File Offset: 0x0000BD54
		private void DispatchWindDataCopyHistoryCommand(NativeArray<GPUInstanceIndex> gpuInstanceIndices, RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			int kernel = this.m_WindDataCopyHistoryKernel;
			int instancesCount = gpuInstanceIndices.Length;
			this.EnsureIndexQueueBufferCapacity(instancesCount);
			this.m_UpdateIndexQueueBuffer.SetData<GPUInstanceIndex>(gpuInstanceIndices, 0, 0, instancesCount);
			this.m_WindDataUpdateCS.SetInt(InstanceDataSystem.InstanceWindDataUpdateIDs._WindDataQueueCount, instancesCount);
			for (int i = 0; i < 16; i++)
			{
				this.m_ScratchWindParamAddressArray[i * 4] = renderersParameters.windParams[i].gpuAddress;
			}
			this.m_WindDataUpdateCS.SetInts(InstanceDataSystem.InstanceWindDataUpdateIDs._WindParamAddressArray, this.m_ScratchWindParamAddressArray);
			for (int j = 0; j < 16; j++)
			{
				this.m_ScratchWindParamAddressArray[j * 4] = renderersParameters.windHistoryParams[j].gpuAddress;
			}
			this.m_WindDataUpdateCS.SetInts(InstanceDataSystem.InstanceWindDataUpdateIDs._WindHistoryParamAddressArray, this.m_ScratchWindParamAddressArray);
			this.m_WindDataUpdateCS.SetBuffer(kernel, InstanceDataSystem.InstanceWindDataUpdateIDs._WindDataUpdateIndexQueue, this.m_UpdateIndexQueueBuffer);
			this.m_WindDataUpdateCS.SetBuffer(kernel, InstanceDataSystem.InstanceWindDataUpdateIDs._WindDataBuffer, outputBuffer.gpuBuffer);
			this.m_WindDataUpdateCS.Dispatch(kernel, (instancesCount + 63) / 64, 1, 1);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000DC58 File Offset: 0x0000BE58
		private unsafe void UpdateInstanceMotionsData(in RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			NativeArray<InstanceHandle> transformUpdateInstanceQueue = new NativeArray<InstanceHandle>(this.m_InstanceData.instancesLength, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			int motionQueueCount = 0;
			new InstanceDataSystem.MotionUpdateJob
			{
				queueWriteBase = 0,
				instanceData = this.m_InstanceData,
				atomicUpdateQueueCount = new UnsafeAtomicCounter32((void*)(&motionQueueCount)),
				transformUpdateInstanceQueue = transformUpdateInstanceQueue
			}.Schedule((this.m_InstanceData.instancesLength + 63) / 64, 16, default(JobHandle)).Complete();
			if (motionQueueCount > 0)
			{
				this.DispatchMotionUpdateCommand(motionQueueCount, transformUpdateInstanceQueue, renderersParameters, outputBuffer);
			}
			transformUpdateInstanceQueue.Dispose();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		private unsafe void UpdateInstanceTransformsData(bool initialize, NativeArray<InstanceHandle> instances, NativeArray<Matrix4x4> localToWorldMatrices, NativeArray<Matrix4x4> prevLocalToWorldMatrices, in RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			NativeArray<InstanceHandle> transformUpdateInstanceQueue = new NativeArray<InstanceHandle>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<TransformUpdatePacket> transformUpdateDataQueue = new NativeArray<TransformUpdatePacket>(initialize ? (instances.Length * 2) : instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<float4> boundingSpheresUpdateDataQueue = new NativeArray<float4>(this.m_EnableBoundingSpheres ? instances.Length : 0, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<InstanceHandle> probeInstanceQueue = new NativeArray<InstanceHandle>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<int> compactTetrahedronCache = new NativeArray<int>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<Vector3> probeQueryPosition = new NativeArray<Vector3>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<SphericalHarmonicsL2> probeUpdateDataQueue = new NativeArray<SphericalHarmonicsL2>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<Vector4> probeOcclusionUpdateDataQueue = new NativeArray<Vector4>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			int transformQueueCount = 0;
			int probesQueueCount = 0;
			InstanceDataSystem.TransformUpdateJob transformJob = new InstanceDataSystem.TransformUpdateJob
			{
				initialize = initialize,
				enableBoundingSpheres = this.m_EnableBoundingSpheres,
				instances = instances,
				localToWorldMatrices = localToWorldMatrices,
				prevLocalToWorldMatrices = prevLocalToWorldMatrices,
				atomicTransformQueueCount = new UnsafeAtomicCounter32((void*)(&transformQueueCount)),
				sharedInstanceData = this.m_SharedInstanceData,
				instanceData = this.m_InstanceData,
				transformUpdateInstanceQueue = transformUpdateInstanceQueue,
				transformUpdateDataQueue = transformUpdateDataQueue,
				boundingSpheresDataQueue = boundingSpheresUpdateDataQueue
			};
			InstanceDataSystem.ProbesUpdateJob probesUpdateJob = new InstanceDataSystem.ProbesUpdateJob
			{
				initialize = initialize,
				instances = instances,
				instanceData = this.m_InstanceData,
				sharedInstanceData = this.m_SharedInstanceData,
				atomicProbesQueueCount = new UnsafeAtomicCounter32((void*)(&probesQueueCount)),
				probeInstanceQueue = probeInstanceQueue,
				compactTetrahedronCache = compactTetrahedronCache,
				probeQueryPosition = probeQueryPosition
			};
			JobHandle jobHandle = transformJob.ScheduleBatch(instances.Length, 64, default(JobHandle));
			probesUpdateJob.ScheduleBatch(instances.Length, 64, jobHandle).Complete();
			if (probesQueueCount > 0)
			{
				this.ScheduleInterpolateProbesAndUpdateTetrahedronCache(probesQueueCount, probeInstanceQueue, compactTetrahedronCache, probeQueryPosition, probeUpdateDataQueue, probeOcclusionUpdateDataQueue).Complete();
				this.DispatchProbeUpdateCommand(probesQueueCount, probeInstanceQueue, probeUpdateDataQueue, probeOcclusionUpdateDataQueue, renderersParameters, outputBuffer);
			}
			if (transformQueueCount > 0)
			{
				this.DispatchTransformUpdateCommand(initialize, transformQueueCount, transformUpdateInstanceQueue, transformUpdateDataQueue, boundingSpheresUpdateDataQueue, renderersParameters, outputBuffer);
			}
			transformUpdateInstanceQueue.Dispose();
			transformUpdateDataQueue.Dispose();
			boundingSpheresUpdateDataQueue.Dispose();
			probeInstanceQueue.Dispose();
			compactTetrahedronCache.Dispose();
			probeQueryPosition.Dispose();
			probeUpdateDataQueue.Dispose();
			probeOcclusionUpdateDataQueue.Dispose();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000DF34 File Offset: 0x0000C134
		private unsafe void UpdateInstanceProbesData(NativeArray<InstanceHandle> instances, in RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			NativeArray<InstanceHandle> probeInstanceQueue = new NativeArray<InstanceHandle>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<int> compactTetrahedronCache = new NativeArray<int>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<Vector3> probeQueryPosition = new NativeArray<Vector3>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<SphericalHarmonicsL2> probeUpdateDataQueue = new NativeArray<SphericalHarmonicsL2>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<Vector4> probeOcclusionUpdateDataQueue = new NativeArray<Vector4>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			int probesQueueCount = 0;
			new InstanceDataSystem.ProbesUpdateJob
			{
				initialize = false,
				instances = instances,
				instanceData = this.m_InstanceData,
				sharedInstanceData = this.m_SharedInstanceData,
				atomicProbesQueueCount = new UnsafeAtomicCounter32((void*)(&probesQueueCount)),
				probeInstanceQueue = probeInstanceQueue,
				compactTetrahedronCache = compactTetrahedronCache,
				probeQueryPosition = probeQueryPosition
			}.ScheduleBatch(instances.Length, 64, default(JobHandle)).Complete();
			if (probesQueueCount > 0)
			{
				this.ScheduleInterpolateProbesAndUpdateTetrahedronCache(probesQueueCount, probeInstanceQueue, compactTetrahedronCache, probeQueryPosition, probeUpdateDataQueue, probeOcclusionUpdateDataQueue).Complete();
				this.DispatchProbeUpdateCommand(probesQueueCount, probeInstanceQueue, probeUpdateDataQueue, probeOcclusionUpdateDataQueue, renderersParameters, outputBuffer);
			}
			probeInstanceQueue.Dispose();
			compactTetrahedronCache.Dispose();
			probeQueryPosition.Dispose();
			probeUpdateDataQueue.Dispose();
			probeOcclusionUpdateDataQueue.Dispose();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000E062 File Offset: 0x0000C262
		public void UpdateInstanceWindDataHistory(NativeArray<GPUInstanceIndex> gpuInstanceIndices, RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			if (gpuInstanceIndices.Length == 0)
			{
				return;
			}
			this.DispatchWindDataCopyHistoryCommand(gpuInstanceIndices, renderersParameters, outputBuffer);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000E078 File Offset: 0x0000C278
		public unsafe void ReallocateAndGetInstances(in GPUDrivenRendererGroupData rendererData, NativeArray<InstanceHandle> instances)
		{
			int newSharedInstancesCount = 0;
			int newInstancesCount = 0;
			NativeArray<int> nativeArray = rendererData.instancesCount;
			bool implicitInstanceIndices = nativeArray.Length == 0;
			if (implicitInstanceIndices)
			{
				InstanceDataSystem.QueryRendererGroupInstancesJob queryRendererGroupInstancesJob = new InstanceDataSystem.QueryRendererGroupInstancesJob
				{
					rendererGroupInstanceMultiHash = this.m_RendererGroupInstanceMultiHash,
					rendererGroupIDs = rendererData.rendererGroupID,
					instances = instances,
					atomicNonFoundInstancesCount = new UnsafeAtomicCounter32((void*)(&newInstancesCount))
				};
				nativeArray = rendererData.rendererGroupID;
				queryRendererGroupInstancesJob.ScheduleBatch(nativeArray.Length, 128, default(JobHandle)).Complete();
				newSharedInstancesCount = newInstancesCount;
			}
			else
			{
				InstanceDataSystem.QueryRendererGroupInstancesMultiJob queryRendererGroupInstancesMultiJob = new InstanceDataSystem.QueryRendererGroupInstancesMultiJob
				{
					rendererGroupInstanceMultiHash = this.m_RendererGroupInstanceMultiHash,
					rendererGroupIDs = rendererData.rendererGroupID,
					instancesOffsets = rendererData.instancesOffset,
					instancesCounts = rendererData.instancesCount,
					instances = instances,
					atomicNonFoundSharedInstancesCount = new UnsafeAtomicCounter32((void*)(&newSharedInstancesCount)),
					atomicNonFoundInstancesCount = new UnsafeAtomicCounter32((void*)(&newInstancesCount))
				};
				nativeArray = rendererData.rendererGroupID;
				queryRendererGroupInstancesMultiJob.ScheduleBatch(nativeArray.Length, 128, default(JobHandle)).Complete();
			}
			this.m_InstanceData.EnsureFreeInstances(newInstancesCount);
			this.m_SharedInstanceData.EnsureFreeInstances(newSharedInstancesCount);
			new InstanceDataSystem.ReallocateInstancesJob
			{
				implicitInstanceIndices = implicitInstanceIndices,
				rendererGroupInstanceMultiHash = this.m_RendererGroupInstanceMultiHash,
				instanceAllocators = this.m_InstanceAllocators,
				sharedInstanceData = this.m_SharedInstanceData,
				instanceData = this.m_InstanceData,
				rendererGroupIDs = rendererData.rendererGroupID,
				packedRendererData = rendererData.packedRendererData,
				instanceOffsets = rendererData.instancesOffset,
				instanceCounts = rendererData.instancesCount,
				instances = instances
			}.Run<InstanceDataSystem.ReallocateInstancesJob>();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000E23C File Offset: 0x0000C43C
		public void FreeRendererGroupInstances(NativeArray<int> rendererGroupsID)
		{
			new InstanceDataSystem.FreeRendererGroupInstancesJob
			{
				rendererGroupInstanceMultiHash = this.m_RendererGroupInstanceMultiHash,
				instanceAllocators = this.m_InstanceAllocators,
				sharedInstanceData = this.m_SharedInstanceData,
				instanceData = this.m_InstanceData,
				rendererGroupsID = rendererGroupsID
			}.Run<InstanceDataSystem.FreeRendererGroupInstancesJob>();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000E294 File Offset: 0x0000C494
		public void FreeInstances(NativeArray<InstanceHandle> instances)
		{
			new InstanceDataSystem.FreeInstancesJob
			{
				rendererGroupInstanceMultiHash = this.m_RendererGroupInstanceMultiHash,
				instanceAllocators = this.m_InstanceAllocators,
				sharedInstanceData = this.m_SharedInstanceData,
				instanceData = this.m_InstanceData,
				instances = instances
			}.Run<InstanceDataSystem.FreeInstancesJob>();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		public JobHandle ScheduleUpdateInstanceDataJob(NativeArray<InstanceHandle> instances, in GPUDrivenRendererGroupData rendererData, NativeParallelHashMap<int, GPUInstanceIndex> lodGroupDataMap)
		{
			NativeArray<int> nativeArray = rendererData.instancesCount;
			bool implicitInstanceIndices = nativeArray.Length == 0;
			InstanceDataSystem.UpdateRendererInstancesJob updateRendererInstancesJob = new InstanceDataSystem.UpdateRendererInstancesJob
			{
				implicitInstanceIndices = implicitInstanceIndices,
				instances = instances,
				rendererData = rendererData,
				lodGroupDataMap = lodGroupDataMap,
				instanceData = this.m_InstanceData,
				sharedInstanceData = this.m_SharedInstanceData
			};
			nativeArray = rendererData.rendererGroupID;
			return updateRendererInstancesJob.Schedule(nativeArray.Length, 128, default(JobHandle));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000E378 File Offset: 0x0000C578
		public void UpdateAllInstanceProbes(in RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			NativeArray<InstanceHandle> instances = this.m_InstanceData.instances.GetSubArray(0, this.m_InstanceData.instancesLength);
			if (instances.Length == 0)
			{
				return;
			}
			this.UpdateInstanceProbesData(instances, in renderersParameters, outputBuffer);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000E3B5 File Offset: 0x0000C5B5
		public void InitializeInstanceTransforms(NativeArray<InstanceHandle> instances, NativeArray<Matrix4x4> localToWorldMatrices, NativeArray<Matrix4x4> prevLocalToWorldMatrices, in RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			if (instances.Length == 0)
			{
				return;
			}
			this.UpdateInstanceTransformsData(true, instances, localToWorldMatrices, prevLocalToWorldMatrices, in renderersParameters, outputBuffer);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000E3CF File Offset: 0x0000C5CF
		public void UpdateInstanceTransforms(NativeArray<InstanceHandle> instances, NativeArray<Matrix4x4> localToWorldMatrices, in RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			if (instances.Length == 0)
			{
				return;
			}
			this.UpdateInstanceTransformsData(false, instances, localToWorldMatrices, localToWorldMatrices, in renderersParameters, outputBuffer);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		public void UpdateInstanceMotions(in RenderersParameters renderersParameters, GPUInstanceDataBuffer outputBuffer)
		{
			if (this.m_InstanceData.instancesLength == 0)
			{
				return;
			}
			this.UpdateInstanceMotionsData(in renderersParameters, outputBuffer);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000E400 File Offset: 0x0000C600
		public JobHandle ScheduleQueryRendererGroupInstancesJob(NativeArray<int> rendererGroupIDs, NativeArray<InstanceHandle> instances)
		{
			if (rendererGroupIDs.Length == 0)
			{
				return default(JobHandle);
			}
			return new InstanceDataSystem.QueryRendererGroupInstancesJob
			{
				rendererGroupInstanceMultiHash = this.m_RendererGroupInstanceMultiHash,
				rendererGroupIDs = rendererGroupIDs,
				instances = instances
			}.ScheduleBatch(rendererGroupIDs.Length, 128, default(JobHandle));
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000E460 File Offset: 0x0000C660
		public JobHandle ScheduleQueryRendererGroupInstancesJob(NativeArray<int> rendererGroupIDs, NativeList<InstanceHandle> instances)
		{
			if (rendererGroupIDs.Length == 0)
			{
				return default(JobHandle);
			}
			NativeArray<int> instancesOffset = new NativeArray<int>(rendererGroupIDs.Length, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			NativeArray<int> instancesCount = new NativeArray<int>(rendererGroupIDs.Length, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			JobHandle jobHandle = this.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instancesOffset, instancesCount, instances);
			instancesOffset.Dispose(jobHandle);
			instancesCount.Dispose(jobHandle);
			return jobHandle;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		public JobHandle ScheduleQueryRendererGroupInstancesJob(NativeArray<int> rendererGroupIDs, NativeArray<int> instancesOffset, NativeArray<int> instancesCount, NativeList<InstanceHandle> instances)
		{
			if (rendererGroupIDs.Length == 0)
			{
				return default(JobHandle);
			}
			JobHandle queryCountJobHandle = new InstanceDataSystem.QueryRendererGroupInstancesCountJob
			{
				instanceData = this.m_InstanceData,
				sharedInstanceData = this.m_SharedInstanceData,
				rendererGroupInstanceMultiHash = this.m_RendererGroupInstanceMultiHash,
				rendererGroupIDs = rendererGroupIDs,
				instancesCount = instancesCount
			}.ScheduleBatch(rendererGroupIDs.Length, 128, default(JobHandle));
			JobHandle computeOffsetsAndResizeArrayJobHandle = new InstanceDataSystem.ComputeInstancesOffsetAndResizeInstancesArrayJob
			{
				instancesCount = instancesCount,
				instancesOffset = instancesOffset,
				instances = instances
			}.Schedule(queryCountJobHandle);
			return new InstanceDataSystem.QueryRendererGroupInstancesMultiJob
			{
				rendererGroupInstanceMultiHash = this.m_RendererGroupInstanceMultiHash,
				rendererGroupIDs = rendererGroupIDs,
				instancesOffsets = instancesOffset,
				instancesCounts = instancesCount,
				instances = instances.AsDeferredJobArray()
			}.ScheduleBatch(rendererGroupIDs.Length, 128, computeOffsetsAndResizeArrayJobHandle);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		public JobHandle ScheduleQuerySortedMeshInstancesJob(NativeArray<int> sortedMeshIDs, NativeList<InstanceHandle> instances)
		{
			if (sortedMeshIDs.Length == 0)
			{
				return default(JobHandle);
			}
			instances.Capacity = this.m_InstanceData.instancesLength;
			return new InstanceDataSystem.QuerySortedMeshInstancesJob
			{
				instanceData = this.m_InstanceData,
				sharedInstanceData = this.m_SharedInstanceData,
				sortedMeshID = sortedMeshIDs,
				instances = instances
			}.ScheduleBatch(this.m_InstanceData.instancesLength, 64, default(JobHandle));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000E634 File Offset: 0x0000C834
		public JobHandle ScheduleCollectInstancesLODGroupAndMasksJob(NativeArray<InstanceHandle> instances, NativeArray<uint> lodGroupAndMasks)
		{
			return new InstanceDataSystem.CollectInstancesLODGroupsAndMasksJob
			{
				instanceData = this.instanceData,
				sharedInstanceData = this.sharedInstanceData,
				instances = instances,
				lodGroupAndMasks = lodGroupAndMasks
			}.Schedule(instances.Length, 128, default(JobHandle));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000E690 File Offset: 0x0000C890
		public bool InternalSanityCheckStates()
		{
			NativeParallelHashMap<SharedInstanceHandle, int> instanceRefCountsHash = new NativeParallelHashMap<SharedInstanceHandle, int>(64, Allocator.Temp);
			int totalValidInstances = 0;
			for (int i = 0; i < this.m_InstanceData.handlesLength; i++)
			{
				InstanceHandle instance = InstanceHandle.FromInt(i);
				if (this.m_InstanceData.IsValidInstance(instance))
				{
					SharedInstanceHandle sharedInstance = this.m_InstanceData.Get_SharedInstance(instance);
					int refCounts;
					if (instanceRefCountsHash.TryGetValue(sharedInstance, out refCounts))
					{
						instanceRefCountsHash[sharedInstance] = refCounts + 1;
					}
					else
					{
						instanceRefCountsHash.Add(sharedInstance, 1);
					}
					totalValidInstances++;
				}
			}
			if (this.m_InstanceData.instancesLength != totalValidInstances)
			{
				return false;
			}
			int totalValidSharedInstances = 0;
			for (int j = 0; j < this.m_SharedInstanceData.handlesLength; j++)
			{
				SharedInstanceHandle sharedInstance2 = new SharedInstanceHandle
				{
					index = j
				};
				if (this.m_SharedInstanceData.IsValidInstance(sharedInstance2))
				{
					int refCount = this.m_SharedInstanceData.Get_RefCount(sharedInstance2);
					if (instanceRefCountsHash[sharedInstance2] != refCount)
					{
						return false;
					}
					totalValidSharedInstances++;
				}
			}
			return this.m_SharedInstanceData.instancesLength == totalValidSharedInstances;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E798 File Offset: 0x0000C998
		public unsafe void GetVisibleTreeInstances(in ParallelBitArray compactedVisibilityMasks, in ParallelBitArray processedBits, NativeList<int> visibeTreeRendererIDs, NativeList<InstanceHandle> visibeTreeInstances, bool becomeVisibleOnly, out int becomeVisibeTreeInstancesCount)
		{
			becomeVisibeTreeInstancesCount = 0;
			int maxTreeInstancesCount = this.GetAliveInstancesOfType(InstanceType.SpeedTree);
			if (maxTreeInstancesCount == 0)
			{
				return;
			}
			visibeTreeRendererIDs.ResizeUninitialized(maxTreeInstancesCount);
			visibeTreeInstances.ResizeUninitialized(maxTreeInstancesCount);
			int visibleTreeInstancesCount = 0;
			new InstanceDataSystem.GetVisibleNonProcessedTreeInstancesJob
			{
				becomeVisible = true,
				instanceData = this.m_InstanceData,
				sharedInstanceData = this.m_SharedInstanceData,
				compactedVisibilityMasks = compactedVisibilityMasks,
				processedBits = processedBits,
				rendererIDs = visibeTreeRendererIDs.AsArray(),
				instances = visibeTreeInstances.AsArray(),
				atomicTreeInstancesCount = new UnsafeAtomicCounter32((void*)(&visibleTreeInstancesCount))
			}.ScheduleBatch(this.m_InstanceData.instancesLength, 64, default(JobHandle)).Complete();
			becomeVisibeTreeInstancesCount = visibleTreeInstancesCount;
			if (!becomeVisibleOnly)
			{
				new InstanceDataSystem.GetVisibleNonProcessedTreeInstancesJob
				{
					becomeVisible = false,
					instanceData = this.m_InstanceData,
					sharedInstanceData = this.m_SharedInstanceData,
					compactedVisibilityMasks = compactedVisibilityMasks,
					processedBits = processedBits,
					rendererIDs = visibeTreeRendererIDs.AsArray(),
					instances = visibeTreeInstances.AsArray(),
					atomicTreeInstancesCount = new UnsafeAtomicCounter32((void*)(&visibleTreeInstancesCount))
				}.ScheduleBatch(this.m_InstanceData.instancesLength, 64, default(JobHandle)).Complete();
			}
			visibeTreeRendererIDs.ResizeUninitialized(visibleTreeInstancesCount);
			visibeTreeInstances.ResizeUninitialized(visibleTreeInstancesCount);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000E908 File Offset: 0x0000CB08
		public void UpdatePerFrameInstanceVisibility(in ParallelBitArray compactedVisibilityMasks)
		{
			new InstanceDataSystem.UpdateCompactedInstanceVisibilityJob
			{
				instanceData = this.m_InstanceData,
				compactedVisibilityMasks = compactedVisibilityMasks
			}.ScheduleBatch(this.m_InstanceData.instancesLength, 64, default(JobHandle)).Complete();
		}

		// Token: 0x04000232 RID: 562
		private InstanceAllocators m_InstanceAllocators;

		// Token: 0x04000233 RID: 563
		private CPUSharedInstanceData m_SharedInstanceData;

		// Token: 0x04000234 RID: 564
		private CPUInstanceData m_InstanceData;

		// Token: 0x04000235 RID: 565
		private NativeParallelMultiHashMap<int, InstanceHandle> m_RendererGroupInstanceMultiHash;

		// Token: 0x04000236 RID: 566
		private ComputeShader m_TransformUpdateCS;

		// Token: 0x04000237 RID: 567
		private ComputeShader m_WindDataUpdateCS;

		// Token: 0x04000238 RID: 568
		private int m_TransformInitKernel;

		// Token: 0x04000239 RID: 569
		private int m_TransformUpdateKernel;

		// Token: 0x0400023A RID: 570
		private int m_MotionUpdateKernel;

		// Token: 0x0400023B RID: 571
		private int m_ProbeUpdateKernel;

		// Token: 0x0400023C RID: 572
		private int m_LODUpdateKernel;

		// Token: 0x0400023D RID: 573
		private int m_WindDataCopyHistoryKernel;

		// Token: 0x0400023E RID: 574
		private ComputeBuffer m_UpdateIndexQueueBuffer;

		// Token: 0x0400023F RID: 575
		private ComputeBuffer m_ProbeUpdateDataQueueBuffer;

		// Token: 0x04000240 RID: 576
		private ComputeBuffer m_ProbeOcclusionUpdateDataQueueBuffer;

		// Token: 0x04000241 RID: 577
		private ComputeBuffer m_TransformUpdateDataQueueBuffer;

		// Token: 0x04000242 RID: 578
		private ComputeBuffer m_BoundingSpheresUpdateDataQueueBuffer;

		// Token: 0x04000243 RID: 579
		private bool m_EnableBoundingSpheres;

		// Token: 0x04000244 RID: 580
		private readonly int[] m_ScratchWindParamAddressArray = new int[64];

		// Token: 0x02000075 RID: 117
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct QueryRendererGroupInstancesCountJob : IJobParallelForBatch
		{
			// Token: 0x06000251 RID: 593 RVA: 0x0000E95C File Offset: 0x0000CB5C
			public void Execute(int startIndex, int count)
			{
				for (int i = startIndex; i < startIndex + count; i++)
				{
					int rendererGroupID = this.rendererGroupIDs[i];
					InstanceHandle instance;
					NativeParallelMultiHashMapIterator<int> it;
					if (this.rendererGroupInstanceMultiHash.TryGetFirstValue(rendererGroupID, out instance, out it))
					{
						SharedInstanceHandle sharedInstance = this.instanceData.Get_SharedInstance(instance);
						int refCount = this.sharedInstanceData.Get_RefCount(sharedInstance);
						this.instancesCount[i] = refCount;
					}
					else
					{
						this.instancesCount[i] = 0;
					}
				}
			}

			// Token: 0x04000245 RID: 581
			public const int k_BatchSize = 128;

			// Token: 0x04000246 RID: 582
			[ReadOnly]
			public CPUInstanceData instanceData;

			// Token: 0x04000247 RID: 583
			[ReadOnly]
			public CPUSharedInstanceData sharedInstanceData;

			// Token: 0x04000248 RID: 584
			[ReadOnly]
			public NativeParallelMultiHashMap<int, InstanceHandle> rendererGroupInstanceMultiHash;

			// Token: 0x04000249 RID: 585
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[ReadOnly]
			public NativeArray<int> rendererGroupIDs;

			// Token: 0x0400024A RID: 586
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[WriteOnly]
			public NativeArray<int> instancesCount;
		}

		// Token: 0x02000076 RID: 118
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct ComputeInstancesOffsetAndResizeInstancesArrayJob : IJob
		{
			// Token: 0x06000252 RID: 594 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
			public void Execute()
			{
				int totalInstancesCount = 0;
				for (int i = 0; i < this.instancesCount.Length; i++)
				{
					this.instancesOffset[i] = totalInstancesCount;
					totalInstancesCount += this.instancesCount[i];
				}
				this.instances.ResizeUninitialized(totalInstancesCount);
			}

			// Token: 0x0400024B RID: 587
			[ReadOnly]
			public NativeArray<int> instancesCount;

			// Token: 0x0400024C RID: 588
			[WriteOnly]
			public NativeArray<int> instancesOffset;

			// Token: 0x0400024D RID: 589
			public NativeList<InstanceHandle> instances;
		}

		// Token: 0x02000077 RID: 119
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct QueryRendererGroupInstancesJob : IJobParallelForBatch
		{
			// Token: 0x06000253 RID: 595 RVA: 0x0000EA20 File Offset: 0x0000CC20
			public void Execute(int startIndex, int count)
			{
				int newInstancesCountJob = 0;
				for (int i = startIndex; i < startIndex + count; i++)
				{
					InstanceHandle instance;
					NativeParallelMultiHashMapIterator<int> it;
					if (this.rendererGroupInstanceMultiHash.TryGetFirstValue(this.rendererGroupIDs[i], out instance, out it))
					{
						this.instances[i] = instance;
					}
					else
					{
						newInstancesCountJob++;
						this.instances[i] = InstanceHandle.Invalid;
					}
				}
				if (this.atomicNonFoundInstancesCount.Counter != null && newInstancesCountJob > 0)
				{
					this.atomicNonFoundInstancesCount.Add(newInstancesCountJob);
				}
			}

			// Token: 0x0400024E RID: 590
			public const int k_BatchSize = 128;

			// Token: 0x0400024F RID: 591
			[ReadOnly]
			public NativeParallelMultiHashMap<int, InstanceHandle> rendererGroupInstanceMultiHash;

			// Token: 0x04000250 RID: 592
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[ReadOnly]
			public NativeArray<int> rendererGroupIDs;

			// Token: 0x04000251 RID: 593
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[WriteOnly]
			public NativeArray<InstanceHandle> instances;

			// Token: 0x04000252 RID: 594
			[NativeDisableUnsafePtrRestriction]
			public UnsafeAtomicCounter32 atomicNonFoundInstancesCount;
		}

		// Token: 0x02000078 RID: 120
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct QueryRendererGroupInstancesMultiJob : IJobParallelForBatch
		{
			// Token: 0x06000254 RID: 596 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
			public void Execute(int startIndex, int count)
			{
				int newSharedInstancesCountJob = 0;
				int newInstancesCountJob = 0;
				for (int i = startIndex; i < startIndex + count; i++)
				{
					int rendererGroupID = this.rendererGroupIDs[i];
					int instancesOffset = this.instancesOffsets[i];
					int instancesCount = this.instancesCounts[i];
					InstanceHandle storedInstance;
					NativeParallelMultiHashMapIterator<int> it;
					bool success = this.rendererGroupInstanceMultiHash.TryGetFirstValue(rendererGroupID, out storedInstance, out it);
					if (!success)
					{
						newSharedInstancesCountJob++;
					}
					for (int j = 0; j < instancesCount; j++)
					{
						int index = instancesOffset + j;
						if (success)
						{
							this.instances[index] = storedInstance;
							success = this.rendererGroupInstanceMultiHash.TryGetNextValue(out storedInstance, ref it);
						}
						else
						{
							newInstancesCountJob++;
							this.instances[index] = InstanceHandle.Invalid;
						}
					}
				}
				if (this.atomicNonFoundSharedInstancesCount.Counter != null && newSharedInstancesCountJob > 0)
				{
					this.atomicNonFoundSharedInstancesCount.Add(newSharedInstancesCountJob);
				}
				if (this.atomicNonFoundInstancesCount.Counter != null && newInstancesCountJob > 0)
				{
					this.atomicNonFoundInstancesCount.Add(newInstancesCountJob);
				}
			}

			// Token: 0x04000253 RID: 595
			public const int k_BatchSize = 128;

			// Token: 0x04000254 RID: 596
			[ReadOnly]
			public NativeParallelMultiHashMap<int, InstanceHandle> rendererGroupInstanceMultiHash;

			// Token: 0x04000255 RID: 597
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[ReadOnly]
			public NativeArray<int> rendererGroupIDs;

			// Token: 0x04000256 RID: 598
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[ReadOnly]
			public NativeArray<int> instancesOffsets;

			// Token: 0x04000257 RID: 599
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[ReadOnly]
			public NativeArray<int> instancesCounts;

			// Token: 0x04000258 RID: 600
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[WriteOnly]
			public NativeArray<InstanceHandle> instances;

			// Token: 0x04000259 RID: 601
			[NativeDisableUnsafePtrRestriction]
			public UnsafeAtomicCounter32 atomicNonFoundSharedInstancesCount;

			// Token: 0x0400025A RID: 602
			[NativeDisableUnsafePtrRestriction]
			public UnsafeAtomicCounter32 atomicNonFoundInstancesCount;
		}

		// Token: 0x02000079 RID: 121
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct QuerySortedMeshInstancesJob : IJobParallelForBatch
		{
			// Token: 0x06000255 RID: 597 RVA: 0x0000EB9C File Offset: 0x0000CD9C
			public void Execute(int startIndex, int count)
			{
				ulong validBits = 0UL;
				for (int i = 0; i < count; i++)
				{
					int instanceIndex = startIndex + i;
					InstanceHandle instanceHandle = this.instanceData.instances[instanceIndex];
					SharedInstanceHandle sharedInstance = this.instanceData.sharedInstances[instanceIndex];
					int meshID = this.sharedInstanceData.Get_MeshID(sharedInstance);
					if (this.sortedMeshID.BinarySearch(meshID) >= 0)
					{
						validBits |= 1UL << i;
					}
				}
				int validBitCount = math.countbits(validBits);
				if (validBitCount > 0)
				{
					int writeIndex = InstanceDataSystem.AtomicAddLengthNoResize<InstanceHandle>(in this.instances, validBitCount);
					int validBitIndex = math.tzcnt(validBits);
					while (validBits != 0UL)
					{
						int instanceIndex2 = startIndex + validBitIndex;
						this.instances[writeIndex] = this.instanceData.instances[instanceIndex2];
						writeIndex++;
						validBits &= ~(1UL << validBitIndex);
						validBitIndex = math.tzcnt(validBits);
					}
				}
			}

			// Token: 0x0400025B RID: 603
			public const int k_BatchSize = 64;

			// Token: 0x0400025C RID: 604
			[ReadOnly]
			public CPUInstanceData instanceData;

			// Token: 0x0400025D RID: 605
			[ReadOnly]
			public CPUSharedInstanceData sharedInstanceData;

			// Token: 0x0400025E RID: 606
			[ReadOnly]
			public NativeArray<int> sortedMeshID;

			// Token: 0x0400025F RID: 607
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeList<InstanceHandle> instances;
		}

		// Token: 0x0200007A RID: 122
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct CalculateInterpolatedLightAndOcclusionProbesBatchJob : IJobParallelFor
		{
			// Token: 0x06000256 RID: 598 RVA: 0x0000EC70 File Offset: 0x0000CE70
			public void Execute(int index)
			{
				int startIndex = index * 8;
				int count = math.min(this.probesCount, startIndex + 8) - startIndex;
				NativeArray<int> compactTetrahedronCacheSubArray = this.compactTetrahedronCache.GetSubArray(startIndex, count);
				NativeArray<Vector3> queryPostitionsSubArray = this.queryPostitions.GetSubArray(startIndex, count);
				NativeArray<SphericalHarmonicsL2> probesSphericalHarmonicsSubArray = this.probesSphericalHarmonics.GetSubArray(startIndex, count);
				NativeArray<Vector4> probesOcclusionSubArray = this.probesOcclusion.GetSubArray(startIndex, count);
				this.lightProbesQuery.CalculateInterpolatedLightAndOcclusionProbes(queryPostitionsSubArray, compactTetrahedronCacheSubArray, probesSphericalHarmonicsSubArray, probesOcclusionSubArray);
			}

			// Token: 0x04000260 RID: 608
			public const int k_BatchSize = 1;

			// Token: 0x04000261 RID: 609
			public const int k_CalculatedProbesPerBatch = 8;

			// Token: 0x04000262 RID: 610
			[ReadOnly]
			public int probesCount;

			// Token: 0x04000263 RID: 611
			[ReadOnly]
			public LightProbesQuery lightProbesQuery;

			// Token: 0x04000264 RID: 612
			[NativeDisableParallelForRestriction]
			[ReadOnly]
			public NativeArray<Vector3> queryPostitions;

			// Token: 0x04000265 RID: 613
			[NativeDisableParallelForRestriction]
			public NativeArray<int> compactTetrahedronCache;

			// Token: 0x04000266 RID: 614
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<SphericalHarmonicsL2> probesSphericalHarmonics;

			// Token: 0x04000267 RID: 615
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<Vector4> probesOcclusion;
		}

		// Token: 0x0200007B RID: 123
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct ScatterTetrahedronCacheIndicesJob : IJobParallelFor
		{
			// Token: 0x06000257 RID: 599 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
			public void Execute(int index)
			{
				InstanceHandle instance = this.probeInstances[index];
				this.instanceData.Set_TetrahedronCacheIndex(instance, this.compactTetrahedronCache[index]);
			}

			// Token: 0x04000268 RID: 616
			public const int k_BatchSize = 128;

			// Token: 0x04000269 RID: 617
			[ReadOnly]
			public NativeArray<InstanceHandle> probeInstances;

			// Token: 0x0400026A RID: 618
			[ReadOnly]
			public NativeArray<int> compactTetrahedronCache;

			// Token: 0x0400026B RID: 619
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[NativeDisableParallelForRestriction]
			public CPUInstanceData instanceData;
		}

		// Token: 0x0200007C RID: 124
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct TransformUpdateJob : IJobParallelForBatch
		{
			// Token: 0x06000258 RID: 600 RVA: 0x0000ED14 File Offset: 0x0000CF14
			public void Execute(int startIndex, int count)
			{
				ulong validBits = 0UL;
				for (int i = 0; i < count; i++)
				{
					InstanceHandle instance = this.instances[startIndex + i];
					if (instance.valid)
					{
						if (!this.initialize)
						{
							int instanceIndex = this.instanceData.InstanceToIndex(instance);
							int sharedInstanceIndex = this.sharedInstanceData.InstanceToIndex(in this.instanceData, instance);
							int transformUpdateFlags = (int)this.sharedInstanceData.flags[sharedInstanceIndex].transformUpdateFlags;
							bool movedCurrentFrame = this.instanceData.movedInCurrentFrameBits.Get(instanceIndex);
							if ((transformUpdateFlags & 2) != 0 || movedCurrentFrame)
							{
								goto IL_008B;
							}
						}
						validBits |= 1UL << i;
					}
					IL_008B:;
				}
				int validBitCount = math.countbits(validBits);
				if (validBitCount > 0)
				{
					int writeIndex = this.atomicTransformQueueCount.Add(validBitCount);
					int validBitIndex = math.tzcnt(validBits);
					while (validBits != 0UL)
					{
						int index = startIndex + validBitIndex;
						InstanceHandle instance2 = this.instances[index];
						int instanceIndex2 = this.instanceData.InstanceToIndex(instance2);
						int sharedInstanceIndex2 = this.sharedInstanceData.InstanceToIndex(in this.instanceData, instance2);
						bool isStaticObject = (this.sharedInstanceData.flags[sharedInstanceIndex2].transformUpdateFlags & TransformUpdateFlags.IsPartOfStaticBatch) > TransformUpdateFlags.None;
						this.instanceData.movedInCurrentFrameBits.Set(instanceIndex2, !isStaticObject);
						this.transformUpdateInstanceQueue[writeIndex] = instance2;
						ref float4x4 l2w = ref UnsafeUtility.ArrayElementAsRef<float4x4>(this.localToWorldMatrices.GetUnsafeReadOnlyPtr<Matrix4x4>(), index);
						ref AABB localAABB = ref UnsafeUtility.ArrayElementAsRef<AABB>(this.sharedInstanceData.localAABBs.GetUnsafePtr<AABB>(), sharedInstanceIndex2);
						AABB worldAABB = AABB.Transform(l2w, localAABB);
						this.instanceData.worldAABBs[instanceIndex2] = worldAABB;
						if (this.initialize)
						{
							PackedMatrix l2wPacked = PackedMatrix.FromFloat4x4(in l2w);
							Matrix4x4 matrix4x = this.prevLocalToWorldMatrices[index];
							PackedMatrix l2wPrevPacked = PackedMatrix.FromMatrix4x4(in matrix4x);
							this.transformUpdateDataQueue[writeIndex * 2] = new TransformUpdatePacket
							{
								localToWorld0 = l2wPacked.packed0,
								localToWorld1 = l2wPacked.packed1,
								localToWorld2 = l2wPacked.packed2
							};
							this.transformUpdateDataQueue[writeIndex * 2 + 1] = new TransformUpdatePacket
							{
								localToWorld0 = l2wPrevPacked.packed0,
								localToWorld1 = l2wPrevPacked.packed1,
								localToWorld2 = l2wPrevPacked.packed2
							};
						}
						else
						{
							Matrix4x4 matrix4x = l2w;
							PackedMatrix l2wPacked2 = PackedMatrix.FromMatrix4x4(in matrix4x);
							this.transformUpdateDataQueue[writeIndex] = new TransformUpdatePacket
							{
								localToWorld0 = l2wPacked2.packed0,
								localToWorld1 = l2wPacked2.packed1,
								localToWorld2 = l2wPacked2.packed2
							};
							float det = math.determinant((float3x3)l2w);
							this.instanceData.localToWorldIsFlippedBits.Set(instanceIndex2, det < 0f);
						}
						if (this.enableBoundingSpheres)
						{
							this.boundingSpheresDataQueue[writeIndex] = new float4(worldAABB.center.x, worldAABB.center.y, worldAABB.center.z, math.distance(worldAABB.max, worldAABB.min) * 0.5f);
						}
						writeIndex++;
						validBits &= ~(1UL << validBitIndex);
						validBitIndex = math.tzcnt(validBits);
					}
				}
			}

			// Token: 0x0400026C RID: 620
			public const int k_BatchSize = 64;

			// Token: 0x0400026D RID: 621
			[ReadOnly]
			public bool initialize;

			// Token: 0x0400026E RID: 622
			[ReadOnly]
			public bool enableBoundingSpheres;

			// Token: 0x0400026F RID: 623
			[ReadOnly]
			public NativeArray<InstanceHandle> instances;

			// Token: 0x04000270 RID: 624
			[ReadOnly]
			public NativeArray<Matrix4x4> localToWorldMatrices;

			// Token: 0x04000271 RID: 625
			[ReadOnly]
			public NativeArray<Matrix4x4> prevLocalToWorldMatrices;

			// Token: 0x04000272 RID: 626
			[NativeDisableUnsafePtrRestriction]
			public UnsafeAtomicCounter32 atomicTransformQueueCount;

			// Token: 0x04000273 RID: 627
			[NativeDisableParallelForRestriction]
			public CPUSharedInstanceData sharedInstanceData;

			// Token: 0x04000274 RID: 628
			[NativeDisableParallelForRestriction]
			public CPUInstanceData instanceData;

			// Token: 0x04000275 RID: 629
			[NativeDisableParallelForRestriction]
			public NativeArray<InstanceHandle> transformUpdateInstanceQueue;

			// Token: 0x04000276 RID: 630
			[NativeDisableParallelForRestriction]
			public NativeArray<TransformUpdatePacket> transformUpdateDataQueue;

			// Token: 0x04000277 RID: 631
			[NativeDisableParallelForRestriction]
			public NativeArray<float4> boundingSpheresDataQueue;
		}

		// Token: 0x0200007D RID: 125
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct ProbesUpdateJob : IJobParallelForBatch
		{
			// Token: 0x06000259 RID: 601 RVA: 0x0000F06C File Offset: 0x0000D26C
			public void Execute(int startIndex, int count)
			{
				ulong validBits = 0UL;
				for (int i = 0; i < count; i++)
				{
					InstanceHandle instance = this.instances[startIndex + i];
					if (instance.valid)
					{
						int sharedInstanceIndex = this.sharedInstanceData.InstanceToIndex(in this.instanceData, instance);
						TransformUpdateFlags flags = this.sharedInstanceData.flags[sharedInstanceIndex].transformUpdateFlags;
						bool isStaticObject = (flags & TransformUpdateFlags.IsPartOfStaticBatch) > TransformUpdateFlags.None;
						if ((this.initialize || !isStaticObject) && (flags & TransformUpdateFlags.HasLightProbeCombined) > TransformUpdateFlags.None)
						{
							validBits |= 1UL << i;
						}
					}
				}
				int validBitCount = math.countbits(validBits);
				if (validBitCount > 0)
				{
					int writeIndex = this.atomicProbesQueueCount.Add(validBitCount);
					int validBitIndex = math.tzcnt(validBits);
					while (validBits != 0UL)
					{
						InstanceHandle instance2 = this.instances[startIndex + validBitIndex];
						int instanceIndex = this.instanceData.InstanceToIndex(instance2);
						ref AABB worldAABB = ref UnsafeUtility.ArrayElementAsRef<AABB>(this.instanceData.worldAABBs.GetUnsafePtr<AABB>(), instanceIndex);
						this.probeInstanceQueue[writeIndex] = instance2;
						this.probeQueryPosition[writeIndex] = worldAABB.center;
						this.compactTetrahedronCache[writeIndex] = this.instanceData.tetrahedronCacheIndices[instanceIndex];
						writeIndex++;
						validBits &= ~(1UL << validBitIndex);
						validBitIndex = math.tzcnt(validBits);
					}
				}
			}

			// Token: 0x04000278 RID: 632
			public const int k_BatchSize = 64;

			// Token: 0x04000279 RID: 633
			[ReadOnly]
			public bool initialize;

			// Token: 0x0400027A RID: 634
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[ReadOnly]
			public NativeArray<InstanceHandle> instances;

			// Token: 0x0400027B RID: 635
			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public CPUInstanceData instanceData;

			// Token: 0x0400027C RID: 636
			[ReadOnly]
			public CPUSharedInstanceData sharedInstanceData;

			// Token: 0x0400027D RID: 637
			[NativeDisableUnsafePtrRestriction]
			public UnsafeAtomicCounter32 atomicProbesQueueCount;

			// Token: 0x0400027E RID: 638
			[NativeDisableParallelForRestriction]
			public NativeArray<InstanceHandle> probeInstanceQueue;

			// Token: 0x0400027F RID: 639
			[NativeDisableParallelForRestriction]
			public NativeArray<int> compactTetrahedronCache;

			// Token: 0x04000280 RID: 640
			[NativeDisableParallelForRestriction]
			public NativeArray<Vector3> probeQueryPosition;
		}

		// Token: 0x0200007E RID: 126
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct MotionUpdateJob : IJobParallelFor
		{
			// Token: 0x0600025A RID: 602 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
			public void Execute(int chunk_index)
			{
				int maxChunkBitCount = math.min(this.instanceData.instancesLength - 64 * chunk_index, 64);
				ulong chunkBitMask = ulong.MaxValue >> 64 - maxChunkBitCount;
				ulong currentChunkBits = this.instanceData.movedInCurrentFrameBits.GetChunk(chunk_index) & chunkBitMask;
				ulong num = this.instanceData.movedInPreviousFrameBits.GetChunk(chunk_index) & chunkBitMask;
				this.instanceData.movedInCurrentFrameBits.SetChunk(chunk_index, 0UL);
				this.instanceData.movedInPreviousFrameBits.SetChunk(chunk_index, currentChunkBits);
				ulong remainingChunkBits = num & ~currentChunkBits;
				int chunkBitCount = math.countbits(remainingChunkBits);
				int writeIndex = this.queueWriteBase;
				if (chunkBitCount > 0)
				{
					writeIndex += this.atomicUpdateQueueCount.Add(chunkBitCount);
				}
				for (int indexInChunk = math.tzcnt(remainingChunkBits); indexInChunk < 64; indexInChunk = math.tzcnt(remainingChunkBits))
				{
					int instanceIndex = 64 * chunk_index + indexInChunk;
					this.transformUpdateInstanceQueue[writeIndex] = this.instanceData.IndexToInstance(instanceIndex);
					writeIndex++;
					remainingChunkBits &= ~(1UL << indexInChunk);
				}
			}

			// Token: 0x04000281 RID: 641
			public const int k_BatchSize = 16;

			// Token: 0x04000282 RID: 642
			[ReadOnly]
			public int queueWriteBase;

			// Token: 0x04000283 RID: 643
			[NativeDisableParallelForRestriction]
			public CPUInstanceData instanceData;

			// Token: 0x04000284 RID: 644
			[NativeDisableUnsafePtrRestriction]
			public UnsafeAtomicCounter32 atomicUpdateQueueCount;

			// Token: 0x04000285 RID: 645
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<InstanceHandle> transformUpdateInstanceQueue;
		}

		// Token: 0x0200007F RID: 127
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct ReallocateInstancesJob : IJob
		{
			// Token: 0x0600025B RID: 603 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
			public void Execute()
			{
				for (int i = 0; i < this.rendererGroupIDs.Length; i++)
				{
					int rendererGroupID = this.rendererGroupIDs[i];
					bool hasTree = this.packedRendererData[i].hasTree;
					int instanceCount;
					int instanceOffset;
					if (this.implicitInstanceIndices)
					{
						instanceCount = 1;
						instanceOffset = i;
					}
					else
					{
						instanceCount = this.instanceCounts[i];
						instanceOffset = this.instanceOffsets[i];
					}
					InstanceHandle instance;
					NativeParallelMultiHashMapIterator<int> it;
					SharedInstanceHandle sharedInstance;
					if (this.rendererGroupInstanceMultiHash.TryGetFirstValue(rendererGroupID, out instance, out it))
					{
						sharedInstance = this.instanceData.Get_SharedInstance(instance);
						if (this.sharedInstanceData.Get_RefCount(sharedInstance) - instanceCount > 0)
						{
							bool success = true;
							int freedInstancesCount = 0;
							for (int j = 0; j < instanceCount; j++)
							{
								success = this.rendererGroupInstanceMultiHash.TryGetNextValue(out instance, ref it);
							}
							while (success)
							{
								this.instanceData.Remove(instance);
								this.instanceAllocators.FreeInstance(instance);
								this.rendererGroupInstanceMultiHash.Remove(it);
								freedInstancesCount++;
								success = this.rendererGroupInstanceMultiHash.TryGetNextValue(out instance, ref it);
							}
						}
					}
					else
					{
						sharedInstance = this.instanceAllocators.AllocateSharedInstance();
						this.sharedInstanceData.AddNoGrow(sharedInstance);
					}
					if (instanceCount > 0)
					{
						this.sharedInstanceData.Set_RefCount(sharedInstance, instanceCount);
						for (int k = 0; k < instanceCount; k++)
						{
							int instanceIndex = instanceOffset + k;
							if (!this.instances[instanceIndex].valid)
							{
								InstanceHandle newInstance;
								if (!hasTree)
								{
									newInstance = this.instanceAllocators.AllocateInstance(InstanceType.MeshRenderer);
								}
								else
								{
									newInstance = this.instanceAllocators.AllocateInstance(InstanceType.SpeedTree);
								}
								this.instanceData.AddNoGrow(newInstance);
								int index = this.instanceData.InstanceToIndex(newInstance);
								this.instanceData.sharedInstances[index] = sharedInstance;
								this.instanceData.movedInCurrentFrameBits.Set(index, false);
								this.instanceData.movedInPreviousFrameBits.Set(index, false);
								this.instanceData.visibleInPreviousFrameBits.Set(index, false);
								this.rendererGroupInstanceMultiHash.Add(rendererGroupID, newInstance);
								this.instances[instanceIndex] = newInstance;
							}
						}
					}
					else
					{
						this.sharedInstanceData.Remove(sharedInstance);
						this.instanceAllocators.FreeSharedInstance(sharedInstance);
					}
				}
			}

			// Token: 0x04000286 RID: 646
			[ReadOnly]
			public bool implicitInstanceIndices;

			// Token: 0x04000287 RID: 647
			[ReadOnly]
			public NativeArray<int> rendererGroupIDs;

			// Token: 0x04000288 RID: 648
			[ReadOnly]
			public NativeArray<GPUDrivenPackedRendererData> packedRendererData;

			// Token: 0x04000289 RID: 649
			[ReadOnly]
			public NativeArray<int> instanceOffsets;

			// Token: 0x0400028A RID: 650
			[ReadOnly]
			public NativeArray<int> instanceCounts;

			// Token: 0x0400028B RID: 651
			public InstanceAllocators instanceAllocators;

			// Token: 0x0400028C RID: 652
			public CPUInstanceData instanceData;

			// Token: 0x0400028D RID: 653
			public CPUSharedInstanceData sharedInstanceData;

			// Token: 0x0400028E RID: 654
			public NativeArray<InstanceHandle> instances;

			// Token: 0x0400028F RID: 655
			public NativeParallelMultiHashMap<int, InstanceHandle> rendererGroupInstanceMultiHash;
		}

		// Token: 0x02000080 RID: 128
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct FreeInstancesJob : IJob
		{
			// Token: 0x0600025C RID: 604 RVA: 0x0000F4FC File Offset: 0x0000D6FC
			public void Execute()
			{
				foreach (InstanceHandle instance in this.instances)
				{
					if (this.instanceData.IsValidInstance(instance))
					{
						int instanceIndex = this.instanceData.InstanceToIndex(instance);
						SharedInstanceHandle sharedInstance = this.instanceData.sharedInstances[instanceIndex];
						int sharedInstanceIndex = this.sharedInstanceData.SharedInstanceToIndex(sharedInstance);
						int refCount = this.sharedInstanceData.refCounts[sharedInstanceIndex];
						int rendererGroupID = this.sharedInstanceData.rendererGroupIDs[sharedInstanceIndex];
						if (refCount > 1)
						{
							this.sharedInstanceData.refCounts[sharedInstanceIndex] = refCount - 1;
						}
						else
						{
							this.sharedInstanceData.Remove(sharedInstance);
							this.instanceAllocators.FreeSharedInstance(sharedInstance);
						}
						this.instanceData.Remove(instance);
						this.instanceAllocators.FreeInstance(instance);
						InstanceHandle i;
						NativeParallelMultiHashMapIterator<int> it;
						bool success = this.rendererGroupInstanceMultiHash.TryGetFirstValue(rendererGroupID, out i, out it);
						while (success)
						{
							if (instance.Equals(i))
							{
								this.rendererGroupInstanceMultiHash.Remove(it);
								break;
							}
							success = this.rendererGroupInstanceMultiHash.TryGetNextValue(out i, ref it);
						}
					}
				}
			}

			// Token: 0x04000290 RID: 656
			[ReadOnly]
			public NativeArray<InstanceHandle> instances;

			// Token: 0x04000291 RID: 657
			public InstanceAllocators instanceAllocators;

			// Token: 0x04000292 RID: 658
			public CPUInstanceData instanceData;

			// Token: 0x04000293 RID: 659
			public CPUSharedInstanceData sharedInstanceData;

			// Token: 0x04000294 RID: 660
			public NativeParallelMultiHashMap<int, InstanceHandle> rendererGroupInstanceMultiHash;
		}

		// Token: 0x02000081 RID: 129
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct FreeRendererGroupInstancesJob : IJob
		{
			// Token: 0x0600025D RID: 605 RVA: 0x0000F654 File Offset: 0x0000D854
			public void Execute()
			{
				foreach (int rendererGroupID in this.rendererGroupsID)
				{
					InstanceHandle instance;
					NativeParallelMultiHashMapIterator<int> it;
					bool success = this.rendererGroupInstanceMultiHash.TryGetFirstValue(rendererGroupID, out instance, out it);
					while (success)
					{
						SharedInstanceHandle sharedInstance = this.instanceData.Get_SharedInstance(instance);
						int sharedInstanceIndex = this.sharedInstanceData.SharedInstanceToIndex(sharedInstance);
						int refCount = this.sharedInstanceData.refCounts[sharedInstanceIndex];
						if (refCount > 1)
						{
							this.sharedInstanceData.refCounts[sharedInstanceIndex] = refCount - 1;
						}
						else
						{
							this.sharedInstanceData.Remove(sharedInstance);
							this.instanceAllocators.FreeSharedInstance(sharedInstance);
						}
						this.instanceData.Remove(instance);
						this.instanceAllocators.FreeInstance(instance);
						success = this.rendererGroupInstanceMultiHash.TryGetNextValue(out instance, ref it);
					}
					this.rendererGroupInstanceMultiHash.Remove(rendererGroupID);
				}
			}

			// Token: 0x04000295 RID: 661
			[ReadOnly]
			public NativeArray<int> rendererGroupsID;

			// Token: 0x04000296 RID: 662
			public InstanceAllocators instanceAllocators;

			// Token: 0x04000297 RID: 663
			public CPUInstanceData instanceData;

			// Token: 0x04000298 RID: 664
			public CPUSharedInstanceData sharedInstanceData;

			// Token: 0x04000299 RID: 665
			public NativeParallelMultiHashMap<int, InstanceHandle> rendererGroupInstanceMultiHash;
		}

		// Token: 0x02000082 RID: 130
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct UpdateRendererInstancesJob : IJobParallelFor
		{
			// Token: 0x0600025E RID: 606 RVA: 0x0000F760 File Offset: 0x0000D960
			public void Execute(int index)
			{
				int rendererGroupID = this.rendererData.rendererGroupID[index];
				int meshIndex = this.rendererData.meshIndex[index];
				GPUDrivenPackedRendererData packedRendererData = this.rendererData.packedRendererData[index];
				int lodGroupID = this.rendererData.lodGroupID[index];
				int gameObjectLayer = this.rendererData.gameObjectLayer[index];
				int num = this.rendererData.lightmapIndex[index];
				AABB localAABB = this.rendererData.localBounds[index].ToAABB();
				int materialOffset = this.rendererData.materialsOffset[index];
				int materialCount = (int)this.rendererData.materialsCount[index];
				int meshID = this.rendererData.meshID[meshIndex];
				InstanceFlags instanceFlags = InstanceFlags.None;
				TransformUpdateFlags transformUpdateFlags = TransformUpdateFlags.None;
				int lmIndexMasked = num & 65535;
				if (lmIndexMasked >= 65534 && packedRendererData.lightProbeUsage == LightProbeUsage.BlendProbes)
				{
					transformUpdateFlags |= TransformUpdateFlags.HasLightProbeCombined;
				}
				if (packedRendererData.isPartOfStaticBatch)
				{
					transformUpdateFlags |= TransformUpdateFlags.IsPartOfStaticBatch;
				}
				ShadowCastingMode shadowCastingMode = packedRendererData.shadowCastingMode;
				if (shadowCastingMode != ShadowCastingMode.Off)
				{
					if (shadowCastingMode == ShadowCastingMode.ShadowsOnly)
					{
						instanceFlags |= InstanceFlags.IsShadowsOnly;
					}
				}
				else
				{
					instanceFlags |= InstanceFlags.IsShadowsOff;
				}
				if (lmIndexMasked != 65535)
				{
					instanceFlags |= InstanceFlags.AffectsLightmaps;
				}
				if (packedRendererData.smallMeshCulling)
				{
					instanceFlags |= InstanceFlags.SmallMeshCulling;
				}
				uint lodGroupAndMask = uint.MaxValue;
				GPUInstanceIndex lodGroupHandle;
				if (this.lodGroupDataMap.TryGetValue(lodGroupID, out lodGroupHandle) && packedRendererData.lodMask > 0)
				{
					lodGroupAndMask = (uint)((lodGroupHandle.index << 8) | (int)packedRendererData.lodMask);
				}
				int instancesCount;
				int instancesOffset;
				if (this.implicitInstanceIndices)
				{
					instancesCount = 1;
					instancesOffset = index;
				}
				else
				{
					instancesCount = this.rendererData.instancesCount[index];
					instancesOffset = this.rendererData.instancesOffset[index];
				}
				if (instancesCount > 0)
				{
					InstanceHandle instance = this.instances[instancesOffset];
					SharedInstanceHandle sharedInstance = this.instanceData.Get_SharedInstance(instance);
					SmallIntegerArray materialIDs = new SmallIntegerArray(materialCount, Allocator.Persistent);
					for (int i = 0; i < materialCount; i++)
					{
						int matIndex = this.rendererData.materialIndex[materialOffset + i];
						int materialInstanceID = this.rendererData.materialID[matIndex];
						materialIDs[i] = materialInstanceID;
					}
					this.sharedInstanceData.Set(sharedInstance, rendererGroupID, in materialIDs, meshID, in localAABB, transformUpdateFlags, instanceFlags, lodGroupAndMask, gameObjectLayer, this.sharedInstanceData.Get_RefCount(sharedInstance));
					for (int j = 0; j < instancesCount; j++)
					{
						int inputIndex = instancesOffset + j;
						ref Matrix4x4 ptr = ref UnsafeUtility.ArrayElementAsRef<Matrix4x4>(this.rendererData.localToWorldMatrix.GetUnsafeReadOnlyPtr<Matrix4x4>(), inputIndex);
						AABB worldAABB = AABB.Transform(ptr, localAABB);
						instance = this.instances[inputIndex];
						bool isFlipped = math.determinant((float3x3)ptr) < 0f;
						int instanceIndex = this.instanceData.InstanceToIndex(instance);
						this.instanceData.localToWorldIsFlippedBits.Set(instanceIndex, isFlipped);
						this.instanceData.worldAABBs[instanceIndex] = worldAABB;
						this.instanceData.tetrahedronCacheIndices[instanceIndex] = -1;
					}
				}
			}

			// Token: 0x0400029A RID: 666
			public const int k_BatchSize = 128;

			// Token: 0x0400029B RID: 667
			[ReadOnly]
			public bool implicitInstanceIndices;

			// Token: 0x0400029C RID: 668
			[ReadOnly]
			public GPUDrivenRendererGroupData rendererData;

			// Token: 0x0400029D RID: 669
			[ReadOnly]
			public NativeArray<InstanceHandle> instances;

			// Token: 0x0400029E RID: 670
			[ReadOnly]
			public NativeParallelHashMap<int, GPUInstanceIndex> lodGroupDataMap;

			// Token: 0x0400029F RID: 671
			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public CPUInstanceData instanceData;

			// Token: 0x040002A0 RID: 672
			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public CPUSharedInstanceData sharedInstanceData;
		}

		// Token: 0x02000083 RID: 131
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct CollectInstancesLODGroupsAndMasksJob : IJobParallelFor
		{
			// Token: 0x0600025F RID: 607 RVA: 0x0000FA68 File Offset: 0x0000DC68
			public void Execute(int index)
			{
				InstanceHandle instance = this.instances[index];
				int sharedInstanceIndex = this.sharedInstanceData.InstanceToIndex(in this.instanceData, instance);
				this.lodGroupAndMasks[index] = this.sharedInstanceData.lodGroupAndMasks[sharedInstanceIndex];
			}

			// Token: 0x040002A1 RID: 673
			public const int k_BatchSize = 128;

			// Token: 0x040002A2 RID: 674
			[ReadOnly]
			public NativeArray<InstanceHandle> instances;

			// Token: 0x040002A3 RID: 675
			[ReadOnly]
			public CPUInstanceData.ReadOnly instanceData;

			// Token: 0x040002A4 RID: 676
			[ReadOnly]
			public CPUSharedInstanceData.ReadOnly sharedInstanceData;

			// Token: 0x040002A5 RID: 677
			[WriteOnly]
			public NativeArray<uint> lodGroupAndMasks;
		}

		// Token: 0x02000084 RID: 132
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct GetVisibleNonProcessedTreeInstancesJob : IJobParallelForBatch
		{
			// Token: 0x06000260 RID: 608 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
			public void Execute(int startIndex, int count)
			{
				int chunkIndex = startIndex / 64;
				ulong visibleInPrevFrameChunk = this.instanceData.visibleInPreviousFrameBits.GetChunk(chunkIndex);
				ulong processedChunk = this.processedBits.GetChunk(chunkIndex);
				ulong validBits = 0UL;
				for (int i = 0; i < count; i++)
				{
					int instanceIndex = startIndex + i;
					InstanceHandle instance = this.instanceData.IndexToInstance(instanceIndex);
					if (instance.type == InstanceType.SpeedTree && this.compactedVisibilityMasks.Get(instance.index))
					{
						ulong bitMask = 1UL << i;
						if ((processedChunk & bitMask) <= 0UL)
						{
							bool visibleInPrevFrame = (visibleInPrevFrameChunk & bitMask) > 0UL;
							if (this.becomeVisible)
							{
								if (!visibleInPrevFrame)
								{
									validBits |= bitMask;
								}
							}
							else if (visibleInPrevFrame)
							{
								validBits |= bitMask;
							}
						}
					}
				}
				int validBitsCount = math.countbits(validBits);
				if (validBitsCount > 0)
				{
					this.processedBits.SetChunk(chunkIndex, processedChunk | validBits);
					int writeIndex = this.atomicTreeInstancesCount.Add(validBitsCount);
					int validBitIndex = math.tzcnt(validBits);
					while (validBits != 0UL)
					{
						int instanceIndex2 = startIndex + validBitIndex;
						InstanceHandle instance2 = this.instanceData.IndexToInstance(instanceIndex2);
						SharedInstanceHandle sharedInstanceHandle = this.instanceData.Get_SharedInstance(instance2);
						int rendererID = this.sharedInstanceData.Get_RendererGroupID(sharedInstanceHandle);
						this.rendererIDs[writeIndex] = rendererID;
						this.instances[writeIndex] = instance2;
						writeIndex++;
						validBits &= ~(1UL << validBitIndex);
						validBitIndex = math.tzcnt(validBits);
					}
				}
			}

			// Token: 0x040002A6 RID: 678
			public const int k_BatchSize = 64;

			// Token: 0x040002A7 RID: 679
			[ReadOnly]
			public CPUInstanceData instanceData;

			// Token: 0x040002A8 RID: 680
			[ReadOnly]
			public CPUSharedInstanceData sharedInstanceData;

			// Token: 0x040002A9 RID: 681
			[ReadOnly]
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public ParallelBitArray compactedVisibilityMasks;

			// Token: 0x040002AA RID: 682
			[ReadOnly]
			public bool becomeVisible;

			// Token: 0x040002AB RID: 683
			[NativeDisableParallelForRestriction]
			public ParallelBitArray processedBits;

			// Token: 0x040002AC RID: 684
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> rendererIDs;

			// Token: 0x040002AD RID: 685
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<InstanceHandle> instances;

			// Token: 0x040002AE RID: 686
			[NativeDisableUnsafePtrRestriction]
			public UnsafeAtomicCounter32 atomicTreeInstancesCount;
		}

		// Token: 0x02000085 RID: 133
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct UpdateCompactedInstanceVisibilityJob : IJobParallelForBatch
		{
			// Token: 0x06000261 RID: 609 RVA: 0x0000FC14 File Offset: 0x0000DE14
			public void Execute(int startIndex, int count)
			{
				ulong visibleBits = 0UL;
				for (int i = 0; i < count; i++)
				{
					int instanceIndex = startIndex + i;
					if (this.compactedVisibilityMasks.Get(this.instanceData.IndexToInstance(instanceIndex).index))
					{
						visibleBits |= 1UL << i;
					}
				}
				this.instanceData.visibleInPreviousFrameBits.SetChunk(startIndex / 64, visibleBits);
			}

			// Token: 0x040002AF RID: 687
			public const int k_BatchSize = 64;

			// Token: 0x040002B0 RID: 688
			[ReadOnly]
			public ParallelBitArray compactedVisibilityMasks;

			// Token: 0x040002B1 RID: 689
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[NativeDisableParallelForRestriction]
			public CPUInstanceData instanceData;
		}

		// Token: 0x02000086 RID: 134
		private static class InstanceTransformUpdateIDs
		{
			// Token: 0x040002B2 RID: 690
			public static readonly int _TransformUpdateQueueCount = Shader.PropertyToID("_TransformUpdateQueueCount");

			// Token: 0x040002B3 RID: 691
			public static readonly int _TransformUpdateOutputL2WVec4Offset = Shader.PropertyToID("_TransformUpdateOutputL2WVec4Offset");

			// Token: 0x040002B4 RID: 692
			public static readonly int _TransformUpdateOutputW2LVec4Offset = Shader.PropertyToID("_TransformUpdateOutputW2LVec4Offset");

			// Token: 0x040002B5 RID: 693
			public static readonly int _TransformUpdateOutputPrevL2WVec4Offset = Shader.PropertyToID("_TransformUpdateOutputPrevL2WVec4Offset");

			// Token: 0x040002B6 RID: 694
			public static readonly int _TransformUpdateOutputPrevW2LVec4Offset = Shader.PropertyToID("_TransformUpdateOutputPrevW2LVec4Offset");

			// Token: 0x040002B7 RID: 695
			public static readonly int _BoundingSphereOutputVec4Offset = Shader.PropertyToID("_BoundingSphereOutputVec4Offset");

			// Token: 0x040002B8 RID: 696
			public static readonly int _TransformUpdateDataQueue = Shader.PropertyToID("_TransformUpdateDataQueue");

			// Token: 0x040002B9 RID: 697
			public static readonly int _TransformUpdateIndexQueue = Shader.PropertyToID("_TransformUpdateIndexQueue");

			// Token: 0x040002BA RID: 698
			public static readonly int _BoundingSphereDataQueue = Shader.PropertyToID("_BoundingSphereDataQueue");

			// Token: 0x040002BB RID: 699
			public static readonly int _OutputTransformBuffer = Shader.PropertyToID("_OutputTransformBuffer");

			// Token: 0x040002BC RID: 700
			public static readonly int _ProbeUpdateQueueCount = Shader.PropertyToID("_ProbeUpdateQueueCount");

			// Token: 0x040002BD RID: 701
			public static readonly int _SHUpdateVec4Offset = Shader.PropertyToID("_SHUpdateVec4Offset");

			// Token: 0x040002BE RID: 702
			public static readonly int _ProbeUpdateDataQueue = Shader.PropertyToID("_ProbeUpdateDataQueue");

			// Token: 0x040002BF RID: 703
			public static readonly int _ProbeOcclusionUpdateDataQueue = Shader.PropertyToID("_ProbeOcclusionUpdateDataQueue");

			// Token: 0x040002C0 RID: 704
			public static readonly int _ProbeUpdateIndexQueue = Shader.PropertyToID("_ProbeUpdateIndexQueue");

			// Token: 0x040002C1 RID: 705
			public static readonly int _OutputProbeBuffer = Shader.PropertyToID("_OutputProbeBuffer");
		}

		// Token: 0x02000087 RID: 135
		private static class InstanceWindDataUpdateIDs
		{
			// Token: 0x040002C2 RID: 706
			public static readonly int _WindDataQueueCount = Shader.PropertyToID("_WindDataQueueCount");

			// Token: 0x040002C3 RID: 707
			public static readonly int _WindDataUpdateIndexQueue = Shader.PropertyToID("_WindDataUpdateIndexQueue");

			// Token: 0x040002C4 RID: 708
			public static readonly int _WindDataBuffer = Shader.PropertyToID("_WindDataBuffer");

			// Token: 0x040002C5 RID: 709
			public static readonly int _WindParamAddressArray = Shader.PropertyToID("_WindParamAddressArray");

			// Token: 0x040002C6 RID: 710
			public static readonly int _WindHistoryParamAddressArray = Shader.PropertyToID("_WindHistoryParamAddressArray");
		}
	}
}
