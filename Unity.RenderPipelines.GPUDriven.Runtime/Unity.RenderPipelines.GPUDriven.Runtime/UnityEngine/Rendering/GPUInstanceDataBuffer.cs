using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000058 RID: 88
	internal class GPUInstanceDataBuffer : IDisposable
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000A252 File Offset: 0x00008452
		public static int NextVersion()
		{
			return ++GPUInstanceDataBuffer.s_NextLayoutVersion;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000A261 File Offset: 0x00008461
		public bool valid
		{
			get
			{
				return this.instancesSpan.IsCreated;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000A270 File Offset: 0x00008470
		private static GPUInstanceIndex CPUInstanceToGPUInstance(in NativeArray<int> instancesNumPrefixSum, InstanceHandle instance)
		{
			if (!instance.valid || instance.type >= InstanceType.Count)
			{
				return GPUInstanceIndex.Invalid;
			}
			int instanceType = (int)instance.type;
			int perTypeInstanceIndex = instance.instanceIndex;
			NativeArray<int> nativeArray = instancesNumPrefixSum;
			int gpuInstanceIndex = nativeArray[instanceType] + perTypeInstanceIndex;
			return new GPUInstanceIndex
			{
				index = gpuInstanceIndex
			};
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000A2D0 File Offset: 0x000084D0
		public int GetPropertyIndex(int propertyID, bool assertOnFail = true)
		{
			int componentIndex;
			if (this.nameToMetadataMap.TryGetValue(propertyID, out componentIndex))
			{
				return componentIndex;
			}
			return -1;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public int GetGpuAddress(string strName, bool assertOnFail = true)
		{
			int componentIndex = this.GetPropertyIndex(Shader.PropertyToID(strName), false);
			if (assertOnFail)
			{
			}
			if (componentIndex == -1)
			{
				return -1;
			}
			return this.gpuBufferComponentAddress[componentIndex];
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000A328 File Offset: 0x00008528
		public int GetGpuAddress(int propertyID, bool assertOnFail = true)
		{
			int componentIndex = this.GetPropertyIndex(propertyID, assertOnFail);
			if (componentIndex == -1)
			{
				return -1;
			}
			return this.gpuBufferComponentAddress[componentIndex];
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000A350 File Offset: 0x00008550
		public GPUInstanceIndex CPUInstanceToGPUInstance(InstanceHandle instance)
		{
			return GPUInstanceDataBuffer.CPUInstanceToGPUInstance(in this.instancesNumPrefixSum, instance);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000A360 File Offset: 0x00008560
		public InstanceHandle GPUInstanceToCPUInstance(GPUInstanceIndex gpuInstanceIndex)
		{
			int instanceIndex = gpuInstanceIndex.index;
			InstanceType instanceType = InstanceType.Count;
			for (int i = 0; i < 2; i++)
			{
				int instanceNum = this.instanceNumInfo.GetInstanceNum((InstanceType)i);
				if (instanceIndex < instanceNum)
				{
					instanceType = (InstanceType)i;
					break;
				}
				instanceIndex -= instanceNum;
			}
			if (instanceType == InstanceType.Count)
			{
				return InstanceHandle.Invalid;
			}
			return InstanceHandle.Create(instanceIndex, instanceType);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000A3B0 File Offset: 0x000085B0
		public void CPUInstanceArrayToGPUInstanceArray(NativeArray<InstanceHandle> instances, NativeArray<GPUInstanceIndex> gpuInstanceIndices)
		{
			new GPUInstanceDataBuffer.ConvertCPUInstancesToGPUInstancesJob
			{
				instancesNumPrefixSum = this.instancesNumPrefixSum,
				instances = instances,
				gpuInstanceIndices = gpuInstanceIndices
			}.Schedule(instances.Length, 512, default(JobHandle)).Complete();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000A408 File Offset: 0x00008608
		public void Dispose()
		{
			if (this.instancesSpan.IsCreated)
			{
				this.instancesSpan.Dispose();
			}
			if (this.instancesNumPrefixSum.IsCreated)
			{
				this.instancesNumPrefixSum.Dispose();
			}
			if (this.descriptions.IsCreated)
			{
				this.descriptions.Dispose();
			}
			if (this.defaultMetadata.IsCreated)
			{
				this.defaultMetadata.Dispose();
			}
			if (this.gpuBufferComponentAddress.IsCreated)
			{
				this.gpuBufferComponentAddress.Dispose();
			}
			if (this.nameToMetadataMap.IsCreated)
			{
				this.nameToMetadataMap.Dispose();
			}
			if (this.gpuBuffer != null)
			{
				this.gpuBuffer.Release();
			}
			if (this.validComponentsIndicesGpuBuffer != null)
			{
				this.validComponentsIndicesGpuBuffer.Release();
			}
			if (this.componentAddressesGpuBuffer != null)
			{
				this.componentAddressesGpuBuffer.Release();
			}
			if (this.componentInstanceIndexRangesGpuBuffer != null)
			{
				this.componentInstanceIndexRangesGpuBuffer.Release();
			}
			if (this.componentByteCountsGpuBuffer != null)
			{
				this.componentByteCountsGpuBuffer.Release();
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A504 File Offset: 0x00008704
		public GPUInstanceDataBuffer.ReadOnly AsReadOnly()
		{
			return new GPUInstanceDataBuffer.ReadOnly(this);
		}

		// Token: 0x04000196 RID: 406
		private static int s_NextLayoutVersion;

		// Token: 0x04000197 RID: 407
		public InstanceNumInfo instanceNumInfo;

		// Token: 0x04000198 RID: 408
		public NativeArray<int> instancesNumPrefixSum;

		// Token: 0x04000199 RID: 409
		public NativeArray<int> instancesSpan;

		// Token: 0x0400019A RID: 410
		public int byteSize;

		// Token: 0x0400019B RID: 411
		public int perInstanceComponentCount;

		// Token: 0x0400019C RID: 412
		public int version;

		// Token: 0x0400019D RID: 413
		public int layoutVersion;

		// Token: 0x0400019E RID: 414
		public GraphicsBuffer gpuBuffer;

		// Token: 0x0400019F RID: 415
		public GraphicsBuffer validComponentsIndicesGpuBuffer;

		// Token: 0x040001A0 RID: 416
		public GraphicsBuffer componentAddressesGpuBuffer;

		// Token: 0x040001A1 RID: 417
		public GraphicsBuffer componentInstanceIndexRangesGpuBuffer;

		// Token: 0x040001A2 RID: 418
		public GraphicsBuffer componentByteCountsGpuBuffer;

		// Token: 0x040001A3 RID: 419
		public NativeArray<GPUInstanceComponentDesc> descriptions;

		// Token: 0x040001A4 RID: 420
		public NativeArray<MetadataValue> defaultMetadata;

		// Token: 0x040001A5 RID: 421
		public NativeArray<int> gpuBufferComponentAddress;

		// Token: 0x040001A6 RID: 422
		public NativeParallelHashMap<int, int> nameToMetadataMap;

		// Token: 0x02000059 RID: 89
		internal readonly struct ReadOnly
		{
			// Token: 0x06000174 RID: 372 RVA: 0x0000A50C File Offset: 0x0000870C
			public ReadOnly(GPUInstanceDataBuffer buffer)
			{
				this.instancesNumPrefixSum = buffer.instancesNumPrefixSum;
			}

			// Token: 0x06000175 RID: 373 RVA: 0x0000A51A File Offset: 0x0000871A
			public GPUInstanceIndex CPUInstanceToGPUInstance(InstanceHandle instance)
			{
				return GPUInstanceDataBuffer.CPUInstanceToGPUInstance(in this.instancesNumPrefixSum, instance);
			}

			// Token: 0x06000176 RID: 374 RVA: 0x0000A528 File Offset: 0x00008728
			public void CPUInstanceArrayToGPUInstanceArray(NativeArray<InstanceHandle> instances, NativeArray<GPUInstanceIndex> gpuInstanceIndices)
			{
				new GPUInstanceDataBuffer.ConvertCPUInstancesToGPUInstancesJob
				{
					instancesNumPrefixSum = this.instancesNumPrefixSum,
					instances = instances,
					gpuInstanceIndices = gpuInstanceIndices
				}.Schedule(instances.Length, 512, default(JobHandle)).Complete();
			}

			// Token: 0x040001A7 RID: 423
			private readonly NativeArray<int> instancesNumPrefixSum;
		}

		// Token: 0x0200005A RID: 90
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct ConvertCPUInstancesToGPUInstancesJob : IJobParallelFor
		{
			// Token: 0x06000177 RID: 375 RVA: 0x0000A57D File Offset: 0x0000877D
			public void Execute(int index)
			{
				this.gpuInstanceIndices[index] = GPUInstanceDataBuffer.CPUInstanceToGPUInstance(in this.instancesNumPrefixSum, this.instances[index]);
			}

			// Token: 0x040001A8 RID: 424
			public const int k_BatchSize = 512;

			// Token: 0x040001A9 RID: 425
			[ReadOnly]
			public NativeArray<int> instancesNumPrefixSum;

			// Token: 0x040001AA RID: 426
			[ReadOnly]
			public NativeArray<InstanceHandle> instances;

			// Token: 0x040001AB RID: 427
			[WriteOnly]
			public NativeArray<GPUInstanceIndex> gpuInstanceIndices;
		}
	}
}
