using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000060 RID: 96
	internal struct GPUInstanceDataBufferGrower : IDisposable
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0000B2CC File Offset: 0x000094CC
		public unsafe GPUInstanceDataBufferGrower(GPUInstanceDataBuffer sourceBuffer, in InstanceNumInfo instanceNumInfo)
		{
			this.m_SrcBuffer = sourceBuffer;
			this.m_DstBuffer = null;
			bool needToGrow = false;
			for (int i = 0; i < 2; i++)
			{
				if (*((ref instanceNumInfo.InstanceNums.FixedElementField) + (IntPtr)i * 4) > *((ref sourceBuffer.instanceNumInfo.InstanceNums.FixedElementField) + (IntPtr)i * 4))
				{
					needToGrow = true;
				}
			}
			if (!needToGrow)
			{
				return;
			}
			GPUInstanceDataBufferBuilder builder = default(GPUInstanceDataBufferBuilder);
			foreach (GPUInstanceComponentDesc descriptor in sourceBuffer.descriptions)
			{
				builder.AddComponent(descriptor.propertyID, descriptor.isOverriden, descriptor.byteSize, descriptor.isPerInstance, descriptor.instanceType, descriptor.componentGroup);
			}
			this.m_DstBuffer = builder.Build(in instanceNumInfo);
			builder.Dispose();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000B3B4 File Offset: 0x000095B4
		public GPUInstanceDataBuffer SubmitToGpu(ref GPUInstanceDataBufferGrower.GPUResources gpuResources)
		{
			if (this.m_DstBuffer == null)
			{
				return this.m_SrcBuffer;
			}
			if (this.m_SrcBuffer.instanceNumInfo.GetTotalInstanceNum() == 0)
			{
				return this.m_DstBuffer;
			}
			gpuResources.CreateResources();
			gpuResources.cs.SetInt(GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InputValidComponentCounts, this.m_SrcBuffer.perInstanceComponentCount);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._ValidComponentIndices, this.m_SrcBuffer.validComponentsIndicesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._ComponentByteCounts, this.m_SrcBuffer.componentByteCountsGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InputComponentAddresses, this.m_SrcBuffer.componentAddressesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InputComponentInstanceIndexRanges, this.m_SrcBuffer.componentInstanceIndexRangesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._OutputComponentAddresses, this.m_DstBuffer.componentAddressesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InputBuffer, this.m_SrcBuffer.gpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._OutputBuffer, this.m_DstBuffer.gpuBuffer);
			for (int i = 0; i < 2; i++)
			{
				int instanceCount = this.m_SrcBuffer.instanceNumInfo.GetInstanceNum((InstanceType)i);
				if (instanceCount > 0)
				{
					int instanceOffset = this.m_SrcBuffer.instancesNumPrefixSum[i];
					int outputInstanceOffset = this.m_DstBuffer.instancesNumPrefixSum[i];
					gpuResources.cs.SetInt(GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InstanceCounts, instanceCount);
					gpuResources.cs.SetInt(GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InstanceOffset, instanceOffset);
					gpuResources.cs.SetInt(GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._OutputInstanceOffset, outputInstanceOffset);
					gpuResources.cs.Dispatch(gpuResources.kernelId, (instanceCount + 63) / 64, 1, 1);
				}
			}
			return this.m_DstBuffer;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004C45 File Offset: 0x00002E45
		public void Dispose()
		{
		}

		// Token: 0x040001D4 RID: 468
		private GPUInstanceDataBuffer m_SrcBuffer;

		// Token: 0x040001D5 RID: 469
		private GPUInstanceDataBuffer m_DstBuffer;

		// Token: 0x02000061 RID: 97
		private static class CopyInstancesKernelIDs
		{
			// Token: 0x040001D6 RID: 470
			public static readonly int _InputValidComponentCounts = Shader.PropertyToID("_InputValidComponentCounts");

			// Token: 0x040001D7 RID: 471
			public static readonly int _InstanceCounts = Shader.PropertyToID("_InstanceCounts");

			// Token: 0x040001D8 RID: 472
			public static readonly int _InstanceOffset = Shader.PropertyToID("_InstanceOffset");

			// Token: 0x040001D9 RID: 473
			public static readonly int _OutputInstanceOffset = Shader.PropertyToID("_OutputInstanceOffset");

			// Token: 0x040001DA RID: 474
			public static readonly int _ValidComponentIndices = Shader.PropertyToID("_ValidComponentIndices");

			// Token: 0x040001DB RID: 475
			public static readonly int _ComponentByteCounts = Shader.PropertyToID("_ComponentByteCounts");

			// Token: 0x040001DC RID: 476
			public static readonly int _InputComponentAddresses = Shader.PropertyToID("_InputComponentAddresses");

			// Token: 0x040001DD RID: 477
			public static readonly int _OutputComponentAddresses = Shader.PropertyToID("_OutputComponentAddresses");

			// Token: 0x040001DE RID: 478
			public static readonly int _InputComponentInstanceIndexRanges = Shader.PropertyToID("_InputComponentInstanceIndexRanges");

			// Token: 0x040001DF RID: 479
			public static readonly int _InputBuffer = Shader.PropertyToID("_InputBuffer");

			// Token: 0x040001E0 RID: 480
			public static readonly int _OutputBuffer = Shader.PropertyToID("_OutputBuffer");
		}

		// Token: 0x02000062 RID: 98
		public struct GPUResources : IDisposable
		{
			// Token: 0x06000191 RID: 401 RVA: 0x0000B642 File Offset: 0x00009842
			public void LoadShaders(GPUResidentDrawerResources resources)
			{
				if (this.cs == null)
				{
					this.cs = resources.instanceDataBufferCopyKernels;
					this.kernelId = this.cs.FindKernel("MainCopyInstances");
				}
			}

			// Token: 0x06000192 RID: 402 RVA: 0x00004C45 File Offset: 0x00002E45
			public void CreateResources()
			{
			}

			// Token: 0x06000193 RID: 403 RVA: 0x0000B674 File Offset: 0x00009874
			public void Dispose()
			{
				this.cs = null;
			}

			// Token: 0x040001E1 RID: 481
			public ComputeShader cs;

			// Token: 0x040001E2 RID: 482
			public int kernelId;
		}
	}
}
