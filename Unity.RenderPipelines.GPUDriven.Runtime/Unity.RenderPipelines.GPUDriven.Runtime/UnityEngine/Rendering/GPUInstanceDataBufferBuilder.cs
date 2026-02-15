using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005B RID: 91
	internal struct GPUInstanceDataBufferBuilder : IDisposable
	{
		// Token: 0x06000178 RID: 376 RVA: 0x0000A5A4 File Offset: 0x000087A4
		private MetadataValue CreateMetadataValue(int nameID, int gpuAddress, bool isOverridden)
		{
			return new MetadataValue
			{
				NameID = nameID,
				Value = (uint)(gpuAddress | (isOverridden ? int.MinValue : 0))
			};
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000A5D6 File Offset: 0x000087D6
		public void AddComponent<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int propertyID, bool isOverriden, bool isPerInstance, InstanceType instanceType, InstanceComponentGroup componentGroup = InstanceComponentGroup.Default) where T : struct, ValueType
		{
			this.AddComponent(propertyID, isOverriden, UnsafeUtility.SizeOf<T>(), isPerInstance, instanceType, componentGroup);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000A5EC File Offset: 0x000087EC
		public void AddComponent(int propertyID, bool isOverriden, int byteSize, bool isPerInstance, InstanceType instanceType, InstanceComponentGroup componentGroup)
		{
			if (!this.m_Components.IsCreated)
			{
				this.m_Components = new NativeList<GPUInstanceComponentDesc>(64, Allocator.Temp);
			}
			int length = this.m_Components.Length;
			GPUInstanceComponentDesc gpuinstanceComponentDesc = new GPUInstanceComponentDesc(propertyID, byteSize, isOverriden, isPerInstance, instanceType, componentGroup);
			this.m_Components.Add(in gpuinstanceComponentDesc);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000A644 File Offset: 0x00008844
		public unsafe GPUInstanceDataBuffer Build(in InstanceNumInfo instanceNumInfo)
		{
			int perInstanceComponentCounts = 0;
			NativeArray<int> perInstanceComponentIndices = new NativeArray<int>(this.m_Components.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<int> componentAddresses = new NativeArray<int>(this.m_Components.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<int> componentByteSizes = new NativeArray<int>(this.m_Components.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<Vector2Int> componentInstanceIndexRanges = new NativeArray<Vector2Int>(this.m_Components.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			GPUInstanceDataBuffer newBuffer = new GPUInstanceDataBuffer();
			newBuffer.instanceNumInfo = instanceNumInfo;
			newBuffer.instancesNumPrefixSum = new NativeArray<int>(2, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			newBuffer.instancesSpan = new NativeArray<int>(2, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			int sum = 0;
			for (int i = 0; i < 2; i++)
			{
				newBuffer.instancesNumPrefixSum[i] = sum;
				sum += *((ref instanceNumInfo.InstanceNums.FixedElementField) + (IntPtr)i * 4);
				GPUInstanceDataBuffer gpuinstanceDataBuffer = newBuffer;
				int num = i;
				InstanceNumInfo instanceNumInfo2 = instanceNumInfo;
				gpuinstanceDataBuffer.instancesSpan[num] = instanceNumInfo2.GetInstanceNumIncludingChildren((InstanceType)i);
			}
			newBuffer.layoutVersion = GPUInstanceDataBuffer.NextVersion();
			newBuffer.version = 0;
			newBuffer.defaultMetadata = new NativeArray<MetadataValue>(this.m_Components.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			newBuffer.descriptions = new NativeArray<GPUInstanceComponentDesc>(this.m_Components.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			newBuffer.nameToMetadataMap = new NativeParallelHashMap<int, int>(this.m_Components.Length, Allocator.Persistent);
			newBuffer.gpuBufferComponentAddress = new NativeArray<int>(this.m_Components.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			int vec4Size = UnsafeUtility.SizeOf<Vector4>();
			int byteOffset = 4 * vec4Size;
			for (int c = 0; c < this.m_Components.Length; c++)
			{
				GPUInstanceComponentDesc componentDesc = this.m_Components[c];
				newBuffer.descriptions[c] = componentDesc;
				int instancesBegin = newBuffer.instancesNumPrefixSum[(int)componentDesc.instanceType];
				int instancesEnd = instancesBegin + newBuffer.instancesSpan[(int)componentDesc.instanceType];
				int instancesNum = (componentDesc.isPerInstance ? (instancesEnd - instancesBegin) : 1);
				componentInstanceIndexRanges[c] = new Vector2Int(instancesBegin, instancesBegin + instancesNum);
				int componentGPUAddress = byteOffset - instancesBegin * componentDesc.byteSize;
				newBuffer.gpuBufferComponentAddress[c] = componentGPUAddress;
				newBuffer.defaultMetadata[c] = this.CreateMetadataValue(componentDesc.propertyID, componentGPUAddress, componentDesc.isOverriden);
				componentAddresses[c] = componentGPUAddress;
				componentByteSizes[c] = componentDesc.byteSize;
				int componentByteSize = componentDesc.byteSize * instancesNum;
				byteOffset += componentByteSize;
				newBuffer.nameToMetadataMap.TryAdd(componentDesc.propertyID, c);
				if (componentDesc.isPerInstance)
				{
					perInstanceComponentIndices[perInstanceComponentCounts] = c;
					perInstanceComponentCounts++;
				}
			}
			newBuffer.byteSize = byteOffset;
			newBuffer.gpuBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Raw, newBuffer.byteSize / 4, 4);
			newBuffer.gpuBuffer.SetData<Vector4>(new NativeArray<Vector4>(4, Allocator.Temp, NativeArrayOptions.ClearMemory), 0, 0, 4);
			newBuffer.validComponentsIndicesGpuBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Raw, perInstanceComponentCounts, 4);
			newBuffer.validComponentsIndicesGpuBuffer.SetData<int>(perInstanceComponentIndices, 0, 0, perInstanceComponentCounts);
			newBuffer.componentAddressesGpuBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Raw, this.m_Components.Length, 4);
			newBuffer.componentAddressesGpuBuffer.SetData<int>(componentAddresses, 0, 0, this.m_Components.Length);
			newBuffer.componentInstanceIndexRangesGpuBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Raw, this.m_Components.Length, 8);
			newBuffer.componentInstanceIndexRangesGpuBuffer.SetData<Vector2Int>(componentInstanceIndexRanges, 0, 0, this.m_Components.Length);
			newBuffer.componentByteCountsGpuBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Raw, this.m_Components.Length, 4);
			newBuffer.componentByteCountsGpuBuffer.SetData<int>(componentByteSizes, 0, 0, this.m_Components.Length);
			newBuffer.perInstanceComponentCount = perInstanceComponentCounts;
			perInstanceComponentIndices.Dispose();
			componentAddresses.Dispose();
			componentByteSizes.Dispose();
			return newBuffer;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000A9FD File Offset: 0x00008BFD
		public void Dispose()
		{
			if (this.m_Components.IsCreated)
			{
				this.m_Components.Dispose();
			}
		}

		// Token: 0x040001AC RID: 428
		private NativeList<GPUInstanceComponentDesc> m_Components;
	}
}
