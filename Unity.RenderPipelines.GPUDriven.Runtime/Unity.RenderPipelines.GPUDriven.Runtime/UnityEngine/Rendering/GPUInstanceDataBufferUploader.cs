using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005C RID: 92
	internal struct GPUInstanceDataBufferUploader : IDisposable
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000AA18 File Offset: 0x00008C18
		public GPUInstanceDataBufferUploader(in NativeArray<GPUInstanceComponentDesc> descriptions, int capacity, InstanceType instanceType)
		{
			this.m_Capacity = capacity;
			this.m_InstanceCount = 0;
			this.m_UintPerInstance = 0;
			NativeArray<GPUInstanceComponentDesc> nativeArray = descriptions;
			this.m_ComponentDataIndex = new NativeArray<int>(nativeArray.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			nativeArray = descriptions;
			this.m_ComponentIsInstanced = new NativeArray<bool>(nativeArray.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			nativeArray = descriptions;
			this.m_DescriptionsUintSize = new NativeArray<int>(nativeArray.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			nativeArray = descriptions;
			this.m_WritenComponentIndices = new NativeList<int>(nativeArray.Length, Allocator.TempJob);
			this.m_DummyArray = new NativeArray<int>(0, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			int uintSize = UnsafeUtility.SizeOf<uint>();
			int c = 0;
			for (;;)
			{
				int num = c;
				nativeArray = descriptions;
				if (num >= nativeArray.Length)
				{
					break;
				}
				nativeArray = descriptions;
				GPUInstanceComponentDesc componentDesc = nativeArray[c];
				this.m_ComponentIsInstanced[c] = componentDesc.isPerInstance;
				if (componentDesc.instanceType == instanceType)
				{
					this.m_ComponentDataIndex[c] = this.m_UintPerInstance;
					int num2 = c;
					nativeArray = descriptions;
					this.m_DescriptionsUintSize[num2] = nativeArray[c].byteSize / uintSize;
					this.m_UintPerInstance += (componentDesc.isPerInstance ? (componentDesc.byteSize / uintSize) : 0);
				}
				else
				{
					this.m_ComponentDataIndex[c] = -1;
					this.m_DescriptionsUintSize[c] = 0;
				}
				c++;
			}
			this.m_TmpDataBuffer = new NativeArray<uint>(this.m_Capacity * this.m_UintPerInstance, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000AB92 File Offset: 0x00008D92
		public IntPtr GetUploadBufferPtr()
		{
			return new IntPtr(this.m_TmpDataBuffer.GetUnsafePtr<uint>());
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		public int GetUIntPerInstance()
		{
			return this.m_UintPerInstance;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000ABAC File Offset: 0x00008DAC
		public int GetParamUIntOffset(int parameterIndex)
		{
			return this.m_ComponentDataIndex[parameterIndex];
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000ABBA File Offset: 0x00008DBA
		public int PrepareParamWrite<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int parameterIndex) where T : struct, ValueType
		{
			int num = UnsafeUtility.SizeOf<T>() / UnsafeUtility.SizeOf<uint>();
			if (!this.m_WritenComponentIndices.Contains(parameterIndex))
			{
				this.m_WritenComponentIndices.Add(in parameterIndex);
			}
			return this.GetParamUIntOffset(parameterIndex);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000ABEA File Offset: 0x00008DEA
		public void AllocateUploadHandles(int handlesLength)
		{
			this.m_InstanceCount = handlesLength;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000ABF3 File Offset: 0x00008DF3
		public JobHandle WriteInstanceDataJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int parameterIndex, NativeArray<T> instanceData) where T : struct, ValueType
		{
			return this.WriteInstanceDataJob<T>(parameterIndex, instanceData, this.m_DummyArray);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000AC04 File Offset: 0x00008E04
		public JobHandle WriteInstanceDataJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int parameterIndex, NativeArray<T> instanceData, NativeArray<int> gatherIndices) where T : struct, ValueType
		{
			if (this.m_InstanceCount == 0)
			{
				return default(JobHandle);
			}
			bool gatherData = gatherIndices.Length != 0;
			int uintPerParameter = UnsafeUtility.SizeOf<T>() / UnsafeUtility.SizeOf<uint>();
			if (!this.m_WritenComponentIndices.Contains(parameterIndex))
			{
				this.m_WritenComponentIndices.Add(in parameterIndex);
			}
			return new GPUInstanceDataBufferUploader.WriteInstanceDataParameterJob
			{
				gatherData = gatherData,
				gatherIndices = gatherIndices,
				parameterIndex = parameterIndex,
				uintPerParameter = uintPerParameter,
				uintPerInstance = this.m_UintPerInstance,
				componentDataIndex = this.m_ComponentDataIndex,
				instanceData = instanceData.Reinterpret<uint>(UnsafeUtility.SizeOf<T>()),
				tmpDataBuffer = this.m_TmpDataBuffer
			}.Schedule(this.m_InstanceCount, 512, default(JobHandle));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		public void SubmitToGpu(GPUInstanceDataBuffer instanceDataBuffer, NativeArray<GPUInstanceIndex> gpuInstanceIndices, ref GPUInstanceDataBufferUploader.GPUResources gpuResources, bool submitOnlyWrittenParams)
		{
			if (this.m_InstanceCount == 0)
			{
				return;
			}
			instanceDataBuffer.version++;
			int uintSize = UnsafeUtility.SizeOf<uint>();
			int instanceByteSize = this.m_UintPerInstance * uintSize;
			gpuResources.CreateResources(this.m_InstanceCount, instanceByteSize, this.m_ComponentDataIndex.Length, this.m_WritenComponentIndices.Length);
			gpuResources.instanceData.SetData<uint>(this.m_TmpDataBuffer, 0, 0, this.m_InstanceCount * this.m_UintPerInstance);
			gpuResources.instanceIndices.SetData<GPUInstanceIndex>(gpuInstanceIndices, 0, 0, this.m_InstanceCount);
			gpuResources.inputComponentOffsets.SetData<int>(this.m_ComponentDataIndex, 0, 0, this.m_ComponentDataIndex.Length);
			gpuResources.cs.SetInt(GPUInstanceDataBufferUploader.UploadKernelIDs._InputInstanceCounts, this.m_InstanceCount);
			gpuResources.cs.SetInt(GPUInstanceDataBufferUploader.UploadKernelIDs._InputInstanceByteSize, instanceByteSize);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferUploader.UploadKernelIDs._InputInstanceData, gpuResources.instanceData);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferUploader.UploadKernelIDs._InputInstanceIndices, gpuResources.instanceIndices);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferUploader.UploadKernelIDs._InputComponentOffsets, gpuResources.inputComponentOffsets);
			if (submitOnlyWrittenParams)
			{
				gpuResources.validComponentIndices.SetData<int>(this.m_WritenComponentIndices.AsArray(), 0, 0, this.m_WritenComponentIndices.Length);
				gpuResources.cs.SetInt(GPUInstanceDataBufferUploader.UploadKernelIDs._InputValidComponentCounts, this.m_WritenComponentIndices.Length);
				gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferUploader.UploadKernelIDs._InputValidComponentIndices, gpuResources.validComponentIndices);
			}
			else
			{
				gpuResources.cs.SetInt(GPUInstanceDataBufferUploader.UploadKernelIDs._InputValidComponentCounts, instanceDataBuffer.perInstanceComponentCount);
				gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferUploader.UploadKernelIDs._InputValidComponentIndices, instanceDataBuffer.validComponentsIndicesGpuBuffer);
			}
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferUploader.UploadKernelIDs._InputComponentAddresses, instanceDataBuffer.componentAddressesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferUploader.UploadKernelIDs._InputComponentByteCounts, instanceDataBuffer.componentByteCountsGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferUploader.UploadKernelIDs._InputComponentInstanceIndexRanges, instanceDataBuffer.componentInstanceIndexRangesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferUploader.UploadKernelIDs._OutputBuffer, instanceDataBuffer.gpuBuffer);
			gpuResources.cs.Dispatch(gpuResources.kernelId, (this.m_InstanceCount + 63) / 64, 1, 1);
			this.m_InstanceCount = 0;
			this.m_WritenComponentIndices.Clear();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000AF2C File Offset: 0x0000912C
		public void SubmitToGpu(GPUInstanceDataBuffer instanceDataBuffer, NativeArray<InstanceHandle> instances, ref GPUInstanceDataBufferUploader.GPUResources gpuResources, bool submitOnlyWrittenParams)
		{
			if (this.m_InstanceCount == 0)
			{
				return;
			}
			NativeArray<GPUInstanceIndex> gpuInstanceIndices = new NativeArray<GPUInstanceIndex>(instances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			instanceDataBuffer.CPUInstanceArrayToGPUInstanceArray(instances, gpuInstanceIndices);
			this.SubmitToGpu(instanceDataBuffer, gpuInstanceIndices, ref gpuResources, submitOnlyWrittenParams);
			gpuInstanceIndices.Dispose();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000AF6C File Offset: 0x0000916C
		public void Dispose()
		{
			if (this.m_ComponentDataIndex.IsCreated)
			{
				this.m_ComponentDataIndex.Dispose();
			}
			if (this.m_ComponentIsInstanced.IsCreated)
			{
				this.m_ComponentIsInstanced.Dispose();
			}
			if (this.m_DescriptionsUintSize.IsCreated)
			{
				this.m_DescriptionsUintSize.Dispose();
			}
			if (this.m_TmpDataBuffer.IsCreated)
			{
				this.m_TmpDataBuffer.Dispose();
			}
			if (this.m_WritenComponentIndices.IsCreated)
			{
				this.m_WritenComponentIndices.Dispose();
			}
			if (this.m_DummyArray.IsCreated)
			{
				this.m_DummyArray.Dispose();
			}
		}

		// Token: 0x040001AD RID: 429
		private int m_UintPerInstance;

		// Token: 0x040001AE RID: 430
		private int m_Capacity;

		// Token: 0x040001AF RID: 431
		private int m_InstanceCount;

		// Token: 0x040001B0 RID: 432
		private NativeArray<bool> m_ComponentIsInstanced;

		// Token: 0x040001B1 RID: 433
		private NativeArray<int> m_ComponentDataIndex;

		// Token: 0x040001B2 RID: 434
		private NativeArray<int> m_DescriptionsUintSize;

		// Token: 0x040001B3 RID: 435
		private NativeArray<uint> m_TmpDataBuffer;

		// Token: 0x040001B4 RID: 436
		private NativeList<int> m_WritenComponentIndices;

		// Token: 0x040001B5 RID: 437
		private NativeArray<int> m_DummyArray;

		// Token: 0x0200005D RID: 93
		private static class UploadKernelIDs
		{
			// Token: 0x040001B6 RID: 438
			public static readonly int _InputValidComponentCounts = Shader.PropertyToID("_InputValidComponentCounts");

			// Token: 0x040001B7 RID: 439
			public static readonly int _InputInstanceCounts = Shader.PropertyToID("_InputInstanceCounts");

			// Token: 0x040001B8 RID: 440
			public static readonly int _InputInstanceByteSize = Shader.PropertyToID("_InputInstanceByteSize");

			// Token: 0x040001B9 RID: 441
			public static readonly int _InputComponentOffsets = Shader.PropertyToID("_InputComponentOffsets");

			// Token: 0x040001BA RID: 442
			public static readonly int _InputInstanceData = Shader.PropertyToID("_InputInstanceData");

			// Token: 0x040001BB RID: 443
			public static readonly int _InputInstanceIndices = Shader.PropertyToID("_InputInstanceIndices");

			// Token: 0x040001BC RID: 444
			public static readonly int _InputValidComponentIndices = Shader.PropertyToID("_InputValidComponentIndices");

			// Token: 0x040001BD RID: 445
			public static readonly int _InputComponentAddresses = Shader.PropertyToID("_InputComponentAddresses");

			// Token: 0x040001BE RID: 446
			public static readonly int _InputComponentByteCounts = Shader.PropertyToID("_InputComponentByteCounts");

			// Token: 0x040001BF RID: 447
			public static readonly int _InputComponentInstanceIndexRanges = Shader.PropertyToID("_InputComponentInstanceIndexRanges");

			// Token: 0x040001C0 RID: 448
			public static readonly int _OutputBuffer = Shader.PropertyToID("_OutputBuffer");
		}

		// Token: 0x0200005E RID: 94
		public struct GPUResources : IDisposable
		{
			// Token: 0x06000189 RID: 393 RVA: 0x0000B0BE File Offset: 0x000092BE
			public void LoadShaders(GPUResidentDrawerResources resources)
			{
				if (this.cs == null)
				{
					this.cs = resources.instanceDataBufferUploadKernels;
					this.kernelId = this.cs.FindKernel("MainUploadScatterInstances");
				}
			}

			// Token: 0x0600018A RID: 394 RVA: 0x0000B0F0 File Offset: 0x000092F0
			public void CreateResources(int newInstanceCount, int sizePerInstance, int newComponentCounts, int validComponentIndicesCount)
			{
				int newInstanceDataByteSize = newInstanceCount * sizePerInstance;
				if (newInstanceDataByteSize > this.m_InstanceDataByteSize || this.instanceData == null)
				{
					if (this.instanceData != null)
					{
						this.instanceData.Release();
					}
					this.instanceData = new ComputeBuffer((newInstanceDataByteSize + 3) / 4, 4, ComputeBufferType.Raw);
					this.m_InstanceDataByteSize = newInstanceDataByteSize;
				}
				if (newInstanceCount > this.m_InstanceCount || this.instanceIndices == null)
				{
					if (this.instanceIndices != null)
					{
						this.instanceIndices.Release();
					}
					this.instanceIndices = new ComputeBuffer(newInstanceCount, 4, ComputeBufferType.Raw);
					this.m_InstanceCount = newInstanceCount;
				}
				if (newComponentCounts > this.m_ComponentCounts || this.inputComponentOffsets == null)
				{
					if (this.inputComponentOffsets != null)
					{
						this.inputComponentOffsets.Release();
					}
					this.inputComponentOffsets = new ComputeBuffer(newComponentCounts, 4, ComputeBufferType.Raw);
					this.m_ComponentCounts = newComponentCounts;
				}
				if (validComponentIndicesCount > this.m_ValidComponentIndicesCount || this.validComponentIndices == null)
				{
					if (this.validComponentIndices != null)
					{
						this.validComponentIndices.Release();
					}
					this.validComponentIndices = new ComputeBuffer(validComponentIndicesCount, 4, ComputeBufferType.Raw);
					this.m_ValidComponentIndicesCount = validComponentIndicesCount;
				}
			}

			// Token: 0x0600018B RID: 395 RVA: 0x0000B1EC File Offset: 0x000093EC
			public void Dispose()
			{
				this.cs = null;
				if (this.instanceData != null)
				{
					this.instanceData.Release();
				}
				if (this.instanceIndices != null)
				{
					this.instanceIndices.Release();
				}
				if (this.inputComponentOffsets != null)
				{
					this.inputComponentOffsets.Release();
				}
				if (this.validComponentIndices != null)
				{
					this.validComponentIndices.Release();
				}
			}

			// Token: 0x040001C1 RID: 449
			public ComputeBuffer instanceData;

			// Token: 0x040001C2 RID: 450
			public ComputeBuffer instanceIndices;

			// Token: 0x040001C3 RID: 451
			public ComputeBuffer inputComponentOffsets;

			// Token: 0x040001C4 RID: 452
			public ComputeBuffer validComponentIndices;

			// Token: 0x040001C5 RID: 453
			public ComputeShader cs;

			// Token: 0x040001C6 RID: 454
			public int kernelId;

			// Token: 0x040001C7 RID: 455
			private int m_InstanceDataByteSize;

			// Token: 0x040001C8 RID: 456
			private int m_InstanceCount;

			// Token: 0x040001C9 RID: 457
			private int m_ComponentCounts;

			// Token: 0x040001CA RID: 458
			private int m_ValidComponentIndicesCount;
		}

		// Token: 0x0200005F RID: 95
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		internal struct WriteInstanceDataParameterJob : IJobParallelFor
		{
			// Token: 0x0600018C RID: 396 RVA: 0x0000B24C File Offset: 0x0000944C
			public unsafe void Execute(int index)
			{
				int dataOffset = (this.gatherData ? this.gatherIndices[index] : index) * this.uintPerParameter;
				int uintSize = UnsafeUtility.SizeOf<uint>();
				uint* data = (uint*)((byte*)this.instanceData.GetUnsafePtr<uint>() + (IntPtr)dataOffset * 4);
				UnsafeUtility.MemCpy((void*)((byte*)((byte*)this.tmpDataBuffer.GetUnsafePtr<uint>() + (IntPtr)(index * this.uintPerInstance) * 4) + (IntPtr)this.componentDataIndex[this.parameterIndex] * 4), (void*)data, (long)(this.uintPerParameter * uintSize));
			}

			// Token: 0x040001CB RID: 459
			public const int k_BatchSize = 512;

			// Token: 0x040001CC RID: 460
			[ReadOnly]
			public bool gatherData;

			// Token: 0x040001CD RID: 461
			[ReadOnly]
			public int parameterIndex;

			// Token: 0x040001CE RID: 462
			[ReadOnly]
			public int uintPerParameter;

			// Token: 0x040001CF RID: 463
			[ReadOnly]
			public int uintPerInstance;

			// Token: 0x040001D0 RID: 464
			[ReadOnly]
			public NativeArray<int> componentDataIndex;

			// Token: 0x040001D1 RID: 465
			[ReadOnly]
			public NativeArray<int> gatherIndices;

			// Token: 0x040001D2 RID: 466
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[ReadOnly]
			public NativeArray<uint> instanceData;

			// Token: 0x040001D3 RID: 467
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			[WriteOnly]
			public NativeArray<uint> tmpDataBuffer;
		}
	}
}
