using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x020001BF RID: 447
	public struct GPUPrefixSum
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x0002FA90 File Offset: 0x0002DC90
		public GPUPrefixSum(GPUPrefixSum.SystemResources resources)
		{
			this.resources = resources;
			this.resources.LoadKernels();
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0002FAA4 File Offset: 0x0002DCA4
		private unsafe Vector4 PackPrefixSumArgs(int a, int b, int c, int d)
		{
			return new Vector4(*(float*)(&a), *(float*)(&b), *(float*)(&c), *(float*)(&d));
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002FABC File Offset: 0x0002DCBC
		internal void ExecuteCommonIndirect(CommandBuffer cmdBuffer, GraphicsBuffer inputBuffer, in GPUPrefixSum.SupportResources supportResources, bool isExclusive)
		{
			int sumOnGroupKernel = (isExclusive ? this.resources.kernelPrefixSumOnGroupExclusive : this.resources.kernelPrefixSumOnGroup);
			int sumResolveParentKernel = (isExclusive ? this.resources.kernelPrefixSumResolveParentExclusive : this.resources.kernelPrefixSumResolveParent);
			for (int levelId = 0; levelId < supportResources.maxLevelCount; levelId++)
			{
				Vector4 packedArgs = this.PackPrefixSumArgs(0, 0, 0, levelId);
				cmdBuffer.SetComputeVectorParam(this.resources.computeAsset, GPUPrefixSum.ShaderIDs._PrefixSumIntArgs, packedArgs);
				if (levelId == 0)
				{
					cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, sumOnGroupKernel, GPUPrefixSum.ShaderIDs._InputBuffer, inputBuffer);
				}
				else
				{
					cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, sumOnGroupKernel, GPUPrefixSum.ShaderIDs._InputBuffer, supportResources.prefixBuffer1);
				}
				cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, sumOnGroupKernel, GPUPrefixSum.ShaderIDs._TotalLevelsBuffer, supportResources.totalLevelCountBuffer);
				cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, sumOnGroupKernel, GPUPrefixSum.ShaderIDs._LevelsOffsetsBuffer, supportResources.levelOffsetBuffer);
				cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, sumOnGroupKernel, GPUPrefixSum.ShaderIDs._OutputBuffer, supportResources.prefixBuffer0);
				cmdBuffer.DispatchCompute(this.resources.computeAsset, sumOnGroupKernel, supportResources.indirectDispatchArgsBuffer, (uint)(levelId * 16 * 4));
				if (levelId != supportResources.maxLevelCount - 1)
				{
					cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelPrefixSumNextInput, GPUPrefixSum.ShaderIDs._InputBuffer, supportResources.prefixBuffer0);
					cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelPrefixSumNextInput, GPUPrefixSum.ShaderIDs._LevelsOffsetsBuffer, supportResources.levelOffsetBuffer);
					cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelPrefixSumNextInput, GPUPrefixSum.ShaderIDs._OutputBuffer, supportResources.prefixBuffer1);
					cmdBuffer.DispatchCompute(this.resources.computeAsset, this.resources.kernelPrefixSumNextInput, supportResources.indirectDispatchArgsBuffer, (uint)((levelId + 1) * 16 * 4));
				}
			}
			for (int levelId2 = supportResources.maxLevelCount - 1; levelId2 >= 1; levelId2--)
			{
				Vector4 packedArgs2 = this.PackPrefixSumArgs(0, 0, 0, levelId2);
				cmdBuffer.SetComputeVectorParam(this.resources.computeAsset, GPUPrefixSum.ShaderIDs._PrefixSumIntArgs, packedArgs2);
				cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, sumResolveParentKernel, GPUPrefixSum.ShaderIDs._InputBuffer, inputBuffer);
				cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, sumResolveParentKernel, GPUPrefixSum.ShaderIDs._OutputBuffer, supportResources.prefixBuffer0);
				cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, sumResolveParentKernel, GPUPrefixSum.ShaderIDs._LevelsOffsetsBuffer, supportResources.levelOffsetBuffer);
				cmdBuffer.DispatchCompute(this.resources.computeAsset, sumResolveParentKernel, supportResources.indirectDispatchArgsBuffer, (uint)(((levelId2 - 1) * 16 + 8) * 4));
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002FD50 File Offset: 0x0002DF50
		public void DispatchDirect(CommandBuffer cmdBuffer, in GPUPrefixSum.DirectArgs arguments)
		{
			if (arguments.supportResources.prefixBuffer0 == null || arguments.supportResources.prefixBuffer1 == null)
			{
				throw new Exception("Support resources are not valid.");
			}
			if (arguments.input == null)
			{
				throw new Exception("Input source buffer cannot be null.");
			}
			if (arguments.inputCount > arguments.supportResources.alignedElementCount)
			{
				throw new Exception("Input count exceeds maximum count of support resources. Ensure to create support resources with enough space.");
			}
			Vector4 packedArgs = this.PackPrefixSumArgs(arguments.inputCount, arguments.supportResources.maxLevelCount, 0, 0);
			cmdBuffer.SetComputeVectorParam(this.resources.computeAsset, GPUPrefixSum.ShaderIDs._PrefixSumIntArgs, packedArgs);
			cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelCalculateLevelDispatchArgsFromConst, GPUPrefixSum.ShaderIDs._OutputLevelsOffsetsBuffer, arguments.supportResources.levelOffsetBuffer);
			cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelCalculateLevelDispatchArgsFromConst, GPUPrefixSum.ShaderIDs._OutputDispatchLevelArgsBuffer, arguments.supportResources.indirectDispatchArgsBuffer);
			cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelCalculateLevelDispatchArgsFromConst, GPUPrefixSum.ShaderIDs._OutputTotalLevelsBuffer, arguments.supportResources.totalLevelCountBuffer);
			cmdBuffer.DispatchCompute(this.resources.computeAsset, this.resources.kernelCalculateLevelDispatchArgsFromConst, 1, 1, 1);
			this.ExecuteCommonIndirect(cmdBuffer, arguments.input, in arguments.supportResources, arguments.exclusive);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002FEA0 File Offset: 0x0002E0A0
		public void DispatchIndirect(CommandBuffer cmdBuffer, in GPUPrefixSum.IndirectDirectArgs arguments)
		{
			if (arguments.supportResources.prefixBuffer0 == null || arguments.supportResources.prefixBuffer1 == null)
			{
				throw new Exception("Support resources are not valid.");
			}
			if (arguments.input == null || arguments.inputCountBuffer == null)
			{
				throw new Exception("Input source buffer and inputCountBuffer cannot be null.");
			}
			Vector4 packedArgs = this.PackPrefixSumArgs(0, arguments.supportResources.maxLevelCount, arguments.inputCountBufferByteOffset, 0);
			cmdBuffer.SetComputeVectorParam(this.resources.computeAsset, GPUPrefixSum.ShaderIDs._PrefixSumIntArgs, packedArgs);
			cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelCalculateLevelDispatchArgsFromBuffer, GPUPrefixSum.ShaderIDs._InputCountBuffer, arguments.inputCountBuffer);
			cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelCalculateLevelDispatchArgsFromBuffer, GPUPrefixSum.ShaderIDs._OutputLevelsOffsetsBuffer, arguments.supportResources.levelOffsetBuffer);
			cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelCalculateLevelDispatchArgsFromBuffer, GPUPrefixSum.ShaderIDs._OutputDispatchLevelArgsBuffer, arguments.supportResources.indirectDispatchArgsBuffer);
			cmdBuffer.SetComputeBufferParam(this.resources.computeAsset, this.resources.kernelCalculateLevelDispatchArgsFromBuffer, GPUPrefixSum.ShaderIDs._OutputTotalLevelsBuffer, arguments.supportResources.totalLevelCountBuffer);
			cmdBuffer.DispatchCompute(this.resources.computeAsset, this.resources.kernelCalculateLevelDispatchArgsFromBuffer, 1, 1, 1);
			this.ExecuteCommonIndirect(cmdBuffer, arguments.input, in arguments.supportResources, arguments.exclusive);
		}

		// Token: 0x0400089D RID: 2205
		private GPUPrefixSum.SystemResources resources;

		// Token: 0x020001C0 RID: 448
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/Utilities/GPUPrefixSum/GPUPrefixSum.Data.cs")]
		internal static class ShaderDefs
		{
			// Token: 0x06000D29 RID: 3369 RVA: 0x00030001 File Offset: 0x0002E201
			public static int DivUpGroup(int value)
			{
				return (value + 128 - 1) / 128;
			}

			// Token: 0x06000D2A RID: 3370 RVA: 0x00030012 File Offset: 0x0002E212
			public static int AlignUpGroup(int value)
			{
				return GPUPrefixSum.ShaderDefs.DivUpGroup(value) * 128;
			}

			// Token: 0x06000D2B RID: 3371 RVA: 0x00030020 File Offset: 0x0002E220
			public static void CalculateTotalBufferSize(int maxElementCount, out int totalSize, out int levelCounts)
			{
				int alignedSupportMaxCount = GPUPrefixSum.ShaderDefs.AlignUpGroup(maxElementCount);
				totalSize = alignedSupportMaxCount;
				levelCounts = 1;
				while (alignedSupportMaxCount > 128)
				{
					alignedSupportMaxCount = GPUPrefixSum.ShaderDefs.AlignUpGroup(GPUPrefixSum.ShaderDefs.DivUpGroup(alignedSupportMaxCount));
					totalSize += alignedSupportMaxCount;
					levelCounts++;
				}
			}

			// Token: 0x0400089E RID: 2206
			public const int GroupSize = 128;

			// Token: 0x0400089F RID: 2207
			public const int ArgsBufferStride = 16;

			// Token: 0x040008A0 RID: 2208
			public const int ArgsBufferUpper = 0;

			// Token: 0x040008A1 RID: 2209
			public const int ArgsBufferLower = 8;
		}

		// Token: 0x020001C1 RID: 449
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/Utilities/GPUPrefixSum/GPUPrefixSum.Data.cs")]
		public struct LevelOffsets
		{
			// Token: 0x040008A2 RID: 2210
			public uint count;

			// Token: 0x040008A3 RID: 2211
			public uint offset;

			// Token: 0x040008A4 RID: 2212
			public uint parentOffset;
		}

		// Token: 0x020001C2 RID: 450
		public struct RenderGraphResources
		{
			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0003005C File Offset: 0x0002E25C
			public BufferHandle output
			{
				get
				{
					return this.prefixBuffer0;
				}
			}

			// Token: 0x06000D2D RID: 3373 RVA: 0x00030064 File Offset: 0x0002E264
			public static GPUPrefixSum.RenderGraphResources Create(int newMaxElementCount, RenderGraph renderGraph, RenderGraphBuilder builder, bool outputIsTemp = false)
			{
				GPUPrefixSum.RenderGraphResources resources = default(GPUPrefixSum.RenderGraphResources);
				resources.Initialize(newMaxElementCount, renderGraph, builder, outputIsTemp);
				return resources;
			}

			// Token: 0x06000D2E RID: 3374 RVA: 0x00030088 File Offset: 0x0002E288
			private void Initialize(int newMaxElementCount, RenderGraph renderGraph, RenderGraphBuilder builder, bool outputIsTemp = false)
			{
				newMaxElementCount = Math.Max(newMaxElementCount, 1);
				int totalSize;
				int levelCounts;
				GPUPrefixSum.ShaderDefs.CalculateTotalBufferSize(newMaxElementCount, out totalSize, out levelCounts);
				BufferDesc bufferDesc = new BufferDesc(totalSize, 4, GraphicsBuffer.Target.Raw)
				{
					name = "prefixBuffer0"
				};
				BufferDesc prefixBuffer0Desc = bufferDesc;
				BufferHandle bufferHandle2;
				if (!outputIsTemp)
				{
					BufferHandle bufferHandle = renderGraph.CreateBuffer(in prefixBuffer0Desc);
					bufferHandle2 = builder.WriteBuffer(in bufferHandle);
				}
				else
				{
					bufferHandle2 = builder.CreateTransientBuffer(in prefixBuffer0Desc);
				}
				this.prefixBuffer0 = bufferHandle2;
				bufferDesc = new BufferDesc(newMaxElementCount, 4, GraphicsBuffer.Target.Raw);
				bufferDesc.name = "prefixBuffer1";
				this.prefixBuffer1 = builder.CreateTransientBuffer(in bufferDesc);
				bufferDesc = new BufferDesc(1, 4, GraphicsBuffer.Target.Raw);
				bufferDesc.name = "totalLevelCountBuffer";
				this.totalLevelCountBuffer = builder.CreateTransientBuffer(in bufferDesc);
				bufferDesc = new BufferDesc(levelCounts, Marshal.SizeOf<GPUPrefixSum.LevelOffsets>(), GraphicsBuffer.Target.Structured);
				bufferDesc.name = "levelOffsetBuffer";
				this.levelOffsetBuffer = builder.CreateTransientBuffer(in bufferDesc);
				bufferDesc = new BufferDesc(16 * levelCounts, 4, GraphicsBuffer.Target.Structured | GraphicsBuffer.Target.IndirectArguments);
				bufferDesc.name = "indirectDispatchArgsBuffer";
				this.indirectDispatchArgsBuffer = builder.CreateTransientBuffer(in bufferDesc);
				this.alignedElementCount = GPUPrefixSum.ShaderDefs.AlignUpGroup(newMaxElementCount);
				this.maxBufferCount = totalSize;
				this.maxLevelCount = levelCounts;
			}

			// Token: 0x040008A5 RID: 2213
			internal int alignedElementCount;

			// Token: 0x040008A6 RID: 2214
			internal int maxBufferCount;

			// Token: 0x040008A7 RID: 2215
			internal int maxLevelCount;

			// Token: 0x040008A8 RID: 2216
			internal BufferHandle prefixBuffer0;

			// Token: 0x040008A9 RID: 2217
			internal BufferHandle prefixBuffer1;

			// Token: 0x040008AA RID: 2218
			internal BufferHandle totalLevelCountBuffer;

			// Token: 0x040008AB RID: 2219
			internal BufferHandle levelOffsetBuffer;

			// Token: 0x040008AC RID: 2220
			internal BufferHandle indirectDispatchArgsBuffer;
		}

		// Token: 0x020001C3 RID: 451
		public struct SupportResources
		{
			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000D2F RID: 3375 RVA: 0x000301A5 File Offset: 0x0002E3A5
			public GraphicsBuffer output
			{
				get
				{
					return this.prefixBuffer0;
				}
			}

			// Token: 0x06000D30 RID: 3376 RVA: 0x000301B0 File Offset: 0x0002E3B0
			public static GPUPrefixSum.SupportResources Create(int maxElementCount)
			{
				GPUPrefixSum.SupportResources resources = new GPUPrefixSum.SupportResources
				{
					alignedElementCount = 0,
					ownsResources = true
				};
				resources.Resize(maxElementCount);
				return resources;
			}

			// Token: 0x06000D31 RID: 3377 RVA: 0x000301E0 File Offset: 0x0002E3E0
			public static GPUPrefixSum.SupportResources Load(GPUPrefixSum.RenderGraphResources shaderGraphResources)
			{
				GPUPrefixSum.SupportResources resources = new GPUPrefixSum.SupportResources
				{
					alignedElementCount = 0,
					ownsResources = false
				};
				resources.LoadFromShaderGraph(shaderGraphResources);
				return resources;
			}

			// Token: 0x06000D32 RID: 3378 RVA: 0x00030210 File Offset: 0x0002E410
			internal void Resize(int newMaxElementCount)
			{
				if (!this.ownsResources)
				{
					throw new Exception("Cannot resize resources unless they are owned. Use GpuPrefixSumSupportResources.Create() for this.");
				}
				newMaxElementCount = Math.Max(newMaxElementCount, 1);
				if (this.alignedElementCount >= newMaxElementCount)
				{
					return;
				}
				this.Dispose();
				int totalSize;
				int levelCounts;
				GPUPrefixSum.ShaderDefs.CalculateTotalBufferSize(newMaxElementCount, out totalSize, out levelCounts);
				this.alignedElementCount = GPUPrefixSum.ShaderDefs.AlignUpGroup(newMaxElementCount);
				this.maxBufferCount = totalSize;
				this.maxLevelCount = levelCounts;
				this.prefixBuffer0 = new GraphicsBuffer(GraphicsBuffer.Target.Raw, totalSize, 4);
				this.prefixBuffer1 = new GraphicsBuffer(GraphicsBuffer.Target.Raw, newMaxElementCount, 4);
				this.totalLevelCountBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Raw, 1, 4);
				this.levelOffsetBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, levelCounts, Marshal.SizeOf<GPUPrefixSum.LevelOffsets>());
				this.indirectDispatchArgsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, 16 * levelCounts, 4);
			}

			// Token: 0x06000D33 RID: 3379 RVA: 0x000302C4 File Offset: 0x0002E4C4
			private void LoadFromShaderGraph(GPUPrefixSum.RenderGraphResources shaderGraphResources)
			{
				this.alignedElementCount = shaderGraphResources.alignedElementCount;
				this.maxBufferCount = shaderGraphResources.maxBufferCount;
				this.maxLevelCount = shaderGraphResources.maxLevelCount;
				this.prefixBuffer0 = shaderGraphResources.prefixBuffer0;
				this.prefixBuffer1 = shaderGraphResources.prefixBuffer1;
				this.totalLevelCountBuffer = shaderGraphResources.totalLevelCountBuffer;
				this.levelOffsetBuffer = shaderGraphResources.levelOffsetBuffer;
				this.indirectDispatchArgsBuffer = shaderGraphResources.indirectDispatchArgsBuffer;
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x0003034C File Offset: 0x0002E54C
			public void Dispose()
			{
				if (this.alignedElementCount == 0 || !this.ownsResources)
				{
					return;
				}
				this.alignedElementCount = 0;
				GPUPrefixSum.SupportResources.<Dispose>g__TryFreeBuffer|15_0(this.prefixBuffer0);
				GPUPrefixSum.SupportResources.<Dispose>g__TryFreeBuffer|15_0(this.prefixBuffer1);
				GPUPrefixSum.SupportResources.<Dispose>g__TryFreeBuffer|15_0(this.levelOffsetBuffer);
				GPUPrefixSum.SupportResources.<Dispose>g__TryFreeBuffer|15_0(this.indirectDispatchArgsBuffer);
				GPUPrefixSum.SupportResources.<Dispose>g__TryFreeBuffer|15_0(this.totalLevelCountBuffer);
			}

			// Token: 0x06000D35 RID: 3381 RVA: 0x000303A8 File Offset: 0x0002E5A8
			[CompilerGenerated]
			internal static void <Dispose>g__TryFreeBuffer|15_0(GraphicsBuffer resource)
			{
				if (resource != null)
				{
					resource.Dispose();
					resource = null;
				}
			}

			// Token: 0x040008AD RID: 2221
			internal bool ownsResources;

			// Token: 0x040008AE RID: 2222
			internal int alignedElementCount;

			// Token: 0x040008AF RID: 2223
			internal int maxBufferCount;

			// Token: 0x040008B0 RID: 2224
			internal int maxLevelCount;

			// Token: 0x040008B1 RID: 2225
			internal GraphicsBuffer prefixBuffer0;

			// Token: 0x040008B2 RID: 2226
			internal GraphicsBuffer prefixBuffer1;

			// Token: 0x040008B3 RID: 2227
			internal GraphicsBuffer totalLevelCountBuffer;

			// Token: 0x040008B4 RID: 2228
			internal GraphicsBuffer levelOffsetBuffer;

			// Token: 0x040008B5 RID: 2229
			internal GraphicsBuffer indirectDispatchArgsBuffer;
		}

		// Token: 0x020001C4 RID: 452
		public struct DirectArgs
		{
			// Token: 0x040008B6 RID: 2230
			public bool exclusive;

			// Token: 0x040008B7 RID: 2231
			public int inputCount;

			// Token: 0x040008B8 RID: 2232
			public GraphicsBuffer input;

			// Token: 0x040008B9 RID: 2233
			public GPUPrefixSum.SupportResources supportResources;
		}

		// Token: 0x020001C5 RID: 453
		public struct IndirectDirectArgs
		{
			// Token: 0x040008BA RID: 2234
			public bool exclusive;

			// Token: 0x040008BB RID: 2235
			public int inputCountBufferByteOffset;

			// Token: 0x040008BC RID: 2236
			public ComputeBuffer inputCountBuffer;

			// Token: 0x040008BD RID: 2237
			public GraphicsBuffer input;

			// Token: 0x040008BE RID: 2238
			public GPUPrefixSum.SupportResources supportResources;
		}

		// Token: 0x020001C6 RID: 454
		public struct SystemResources
		{
			// Token: 0x06000D36 RID: 3382 RVA: 0x000303B8 File Offset: 0x0002E5B8
			internal void LoadKernels()
			{
				if (this.computeAsset == null)
				{
					return;
				}
				this.kernelCalculateLevelDispatchArgsFromConst = this.computeAsset.FindKernel("MainCalculateLevelDispatchArgsFromConst");
				this.kernelCalculateLevelDispatchArgsFromBuffer = this.computeAsset.FindKernel("MainCalculateLevelDispatchArgsFromBuffer");
				this.kernelPrefixSumOnGroup = this.computeAsset.FindKernel("MainPrefixSumOnGroup");
				this.kernelPrefixSumOnGroupExclusive = this.computeAsset.FindKernel("MainPrefixSumOnGroupExclusive");
				this.kernelPrefixSumNextInput = this.computeAsset.FindKernel("MainPrefixSumNextInput");
				this.kernelPrefixSumResolveParent = this.computeAsset.FindKernel("MainPrefixSumResolveParent");
				this.kernelPrefixSumResolveParentExclusive = this.computeAsset.FindKernel("MainPrefixSumResolveParentExclusive");
			}

			// Token: 0x040008BF RID: 2239
			public ComputeShader computeAsset;

			// Token: 0x040008C0 RID: 2240
			internal int kernelCalculateLevelDispatchArgsFromConst;

			// Token: 0x040008C1 RID: 2241
			internal int kernelCalculateLevelDispatchArgsFromBuffer;

			// Token: 0x040008C2 RID: 2242
			internal int kernelPrefixSumOnGroup;

			// Token: 0x040008C3 RID: 2243
			internal int kernelPrefixSumOnGroupExclusive;

			// Token: 0x040008C4 RID: 2244
			internal int kernelPrefixSumNextInput;

			// Token: 0x040008C5 RID: 2245
			internal int kernelPrefixSumResolveParent;

			// Token: 0x040008C6 RID: 2246
			internal int kernelPrefixSumResolveParentExclusive;
		}

		// Token: 0x020001C7 RID: 455
		private static class ShaderIDs
		{
			// Token: 0x040008C7 RID: 2247
			public static readonly int _InputBuffer = Shader.PropertyToID("_InputBuffer");

			// Token: 0x040008C8 RID: 2248
			public static readonly int _OutputBuffer = Shader.PropertyToID("_OutputBuffer");

			// Token: 0x040008C9 RID: 2249
			public static readonly int _InputCountBuffer = Shader.PropertyToID("_InputCountBuffer");

			// Token: 0x040008CA RID: 2250
			public static readonly int _TotalLevelsBuffer = Shader.PropertyToID("_TotalLevelsBuffer");

			// Token: 0x040008CB RID: 2251
			public static readonly int _OutputTotalLevelsBuffer = Shader.PropertyToID("_OutputTotalLevelsBuffer");

			// Token: 0x040008CC RID: 2252
			public static readonly int _OutputDispatchLevelArgsBuffer = Shader.PropertyToID("_OutputDispatchLevelArgsBuffer");

			// Token: 0x040008CD RID: 2253
			public static readonly int _LevelsOffsetsBuffer = Shader.PropertyToID("_LevelsOffsetsBuffer");

			// Token: 0x040008CE RID: 2254
			public static readonly int _OutputLevelsOffsetsBuffer = Shader.PropertyToID("_OutputLevelsOffsetsBuffer");

			// Token: 0x040008CF RID: 2255
			public static readonly int _PrefixSumIntArgs = Shader.PropertyToID("_PrefixSumIntArgs");
		}
	}
}
