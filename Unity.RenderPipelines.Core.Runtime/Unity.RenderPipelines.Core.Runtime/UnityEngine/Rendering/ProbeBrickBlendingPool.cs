using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020000FB RID: 251
	internal class ProbeBrickBlendingPool
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x00014853 File Offset: 0x00012A53
		internal static void Initialize()
		{
			ProbeVolumeRuntimeResources renderPipelineSettings = GraphicsSettings.GetRenderPipelineSettings<ProbeVolumeRuntimeResources>();
			ProbeBrickBlendingPool.stateBlendShader = ((renderPipelineSettings != null) ? renderPipelineSettings.probeVolumeBlendStatesCS : null);
			ProbeBrickBlendingPool.scenarioBlendingKernel = (ProbeBrickBlendingPool.stateBlendShader ? ProbeBrickBlendingPool.stateBlendShader.FindKernel("BlendScenarios") : (-1));
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001488E File Offset: 0x00012A8E
		internal bool isAllocated
		{
			get
			{
				return this.m_State0 != null;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001489C File Offset: 0x00012A9C
		internal int estimatedVMemCost
		{
			get
			{
				if (!ProbeReferenceVolume.instance.supportScenarioBlending)
				{
					return 0;
				}
				if (this.isAllocated)
				{
					return this.m_State0.estimatedVMemCost + this.m_State1.estimatedVMemCost;
				}
				return ProbeBrickPool.EstimateMemoryCostForBlending(this.m_MemoryBudget, false, this.m_ShBands) * 2;
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000148EB File Offset: 0x00012AEB
		internal int GetPoolWidth()
		{
			return this.m_State0.m_Pool.width;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000148FD File Offset: 0x00012AFD
		internal int GetPoolHeight()
		{
			return this.m_State0.m_Pool.height;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001490F File Offset: 0x00012B0F
		internal int GetPoolDepth()
		{
			return this.m_State0.m_Pool.depth;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00014921 File Offset: 0x00012B21
		internal ProbeBrickBlendingPool(ProbeVolumeBlendingTextureMemoryBudget memoryBudget, ProbeVolumeSHBands shBands, bool probeOcclusion)
		{
			this.m_MemoryBudget = (ProbeVolumeTextureMemoryBudget)memoryBudget;
			this.m_ShBands = shBands;
			this.m_ProbeOcclusion = probeOcclusion;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00014940 File Offset: 0x00012B40
		internal void AllocateResourcesIfNeeded()
		{
			if (this.isAllocated)
			{
				return;
			}
			this.m_State0 = new ProbeBrickPool(this.m_MemoryBudget, this.m_ShBands, false, false, false, false, this.m_ProbeOcclusion);
			this.m_State1 = new ProbeBrickPool(this.m_MemoryBudget, this.m_ShBands, false, false, false, false, this.m_ProbeOcclusion);
			int maxAvailablebrickCount = this.GetPoolWidth() / 512 * (this.GetPoolHeight() / 4) * (this.GetPoolDepth() / 4);
			this.m_ChunkList = new Vector4[maxAvailablebrickCount];
			this.m_MappedChunks = 0;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000149CA File Offset: 0x00012BCA
		internal void Update(ProbeBrickPool.DataLocation source, List<ProbeBrickPool.BrickChunkAlloc> srcLocations, List<ProbeBrickPool.BrickChunkAlloc> dstLocations, int destStartIndex, ProbeVolumeSHBands bands, int state)
		{
			((state == 0) ? this.m_State0 : this.m_State1).Update(source, srcLocations, dstLocations, destStartIndex, bands);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000149EC File Offset: 0x00012BEC
		internal void Update(CommandBuffer cmd, ProbeReferenceVolume.CellStreamingScratchBuffer dataBuffer, ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, List<ProbeBrickPool.BrickChunkAlloc> dstLocations, ProbeVolumeSHBands bands, int state, Texture validityTexture, bool skyOcclusion, Texture skyOcclusionTexture, bool skyShadingDirections, Texture skyShadingDirectionsTexture, bool probeOcclusion)
		{
			bool updateShared = state == 0;
			((state == 0) ? this.m_State0 : this.m_State1).Update(cmd, dataBuffer, layout, dstLocations, updateShared, validityTexture, bands, updateShared && skyOcclusion, skyOcclusionTexture, updateShared && skyShadingDirections, skyShadingDirectionsTexture, probeOcclusion);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00014A34 File Offset: 0x00012C34
		internal void PerformBlending(CommandBuffer cmd, float factor, ProbeBrickPool dstPool)
		{
			if (this.m_MappedChunks == 0)
			{
				return;
			}
			cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State0_L0_L1Rx, this.m_State0.m_Pool.TexL0_L1rx);
			cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State0_L1G_L1Ry, this.m_State0.m_Pool.TexL1_G_ry);
			cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State0_L1B_L1Rz, this.m_State0.m_Pool.TexL1_B_rz);
			cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State1_L0_L1Rx, this.m_State1.m_Pool.TexL0_L1rx);
			cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State1_L1G_L1Ry, this.m_State1.m_Pool.TexL1_G_ry);
			cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State1_L1B_L1Rz, this.m_State1.m_Pool.TexL1_B_rz);
			cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickPool._Out_L0_L1Rx, dstPool.m_Pool.TexL0_L1rx);
			cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickPool._Out_L1G_L1Ry, dstPool.m_Pool.TexL1_G_ry);
			cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickPool._Out_L1B_L1Rz, dstPool.m_Pool.TexL1_B_rz);
			if (this.m_ShBands == ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				ProbeBrickBlendingPool.stateBlendShader.EnableKeyword("PROBE_VOLUMES_L2");
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State0_L2_0, this.m_State0.m_Pool.TexL2_0);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State0_L2_1, this.m_State0.m_Pool.TexL2_1);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State0_L2_2, this.m_State0.m_Pool.TexL2_2);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State0_L2_3, this.m_State0.m_Pool.TexL2_3);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State1_L2_0, this.m_State1.m_Pool.TexL2_0);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State1_L2_1, this.m_State1.m_Pool.TexL2_1);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State1_L2_2, this.m_State1.m_Pool.TexL2_2);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State1_L2_3, this.m_State1.m_Pool.TexL2_3);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickPool._Out_L2_0, dstPool.m_Pool.TexL2_0);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickPool._Out_L2_1, dstPool.m_Pool.TexL2_1);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickPool._Out_L2_2, dstPool.m_Pool.TexL2_2);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickPool._Out_L2_3, dstPool.m_Pool.TexL2_3);
			}
			else
			{
				ProbeBrickBlendingPool.stateBlendShader.DisableKeyword("PROBE_VOLUMES_L2");
			}
			if (this.m_ProbeOcclusion)
			{
				ProbeBrickBlendingPool.stateBlendShader.EnableKeyword("USE_APV_PROBE_OCCLUSION");
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State0_ProbeOcclusion, this.m_State0.m_Pool.TexProbeOcclusion);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickBlendingPool._State1_ProbeOcclusion, this.m_State1.m_Pool.TexProbeOcclusion);
				cmd.SetComputeTextureParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, ProbeBrickPool._Out_ProbeOcclusion, dstPool.m_Pool.TexProbeOcclusion);
			}
			else
			{
				ProbeBrickBlendingPool.stateBlendShader.DisableKeyword("USE_APV_PROBE_OCCLUSION");
			}
			Vector4 poolDim_LerpFactor = new Vector4((float)dstPool.GetPoolWidth(), (float)dstPool.GetPoolHeight(), factor, 0f);
			int threadX = ProbeBrickPool.DivRoundUp(512, 4);
			int threadY = ProbeBrickPool.DivRoundUp(4, 4);
			int threadZ = ProbeBrickPool.DivRoundUp(4, 4);
			cmd.SetComputeVectorArrayParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool._ChunkList, this.m_ChunkList);
			cmd.SetComputeVectorParam(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool._PoolDim_LerpFactor, poolDim_LerpFactor);
			cmd.DispatchCompute(ProbeBrickBlendingPool.stateBlendShader, ProbeBrickBlendingPool.scenarioBlendingKernel, threadX, threadY, threadZ * this.m_MappedChunks);
			this.m_MappedChunks = 0;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00014EE8 File Offset: 0x000130E8
		internal void BlendChunks(ProbeReferenceVolume.Cell cell, ProbeBrickPool dstPool)
		{
			for (int c = 0; c < cell.blendingInfo.chunkList.Count; c++)
			{
				ProbeBrickPool.BrickChunkAlloc chunk = cell.blendingInfo.chunkList[c];
				int dst = cell.poolInfo.chunkList[c].flattenIndex(dstPool.GetPoolWidth(), dstPool.GetPoolHeight());
				Vector4[] chunkList = this.m_ChunkList;
				int mappedChunks = this.m_MappedChunks;
				this.m_MappedChunks = mappedChunks + 1;
				chunkList[mappedChunks] = new Vector4((float)chunk.x, (float)chunk.y, (float)chunk.z, (float)dst);
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00014F86 File Offset: 0x00013186
		internal void Clear()
		{
			ProbeBrickPool state = this.m_State0;
			if (state == null)
			{
				return;
			}
			state.Clear();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00014F98 File Offset: 0x00013198
		internal bool Allocate(int numberOfBrickChunks, List<ProbeBrickPool.BrickChunkAlloc> outAllocations)
		{
			this.AllocateResourcesIfNeeded();
			return numberOfBrickChunks <= this.m_State0.GetRemainingChunkCount() && this.m_State0.Allocate(numberOfBrickChunks, outAllocations, false);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00014FBE File Offset: 0x000131BE
		internal void Deallocate(List<ProbeBrickPool.BrickChunkAlloc> allocations)
		{
			if (allocations.Count == 0)
			{
				return;
			}
			this.m_State0.Deallocate(allocations);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00014FD5 File Offset: 0x000131D5
		internal void EnsureTextureValidity()
		{
			if (this.isAllocated)
			{
				this.m_State0.EnsureTextureValidity();
				this.m_State1.EnsureTextureValidity();
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00014FF5 File Offset: 0x000131F5
		internal void Cleanup()
		{
			if (this.isAllocated)
			{
				this.m_State0.Cleanup();
				this.m_State1.Cleanup();
			}
		}

		// Token: 0x04000368 RID: 872
		private static ComputeShader stateBlendShader;

		// Token: 0x04000369 RID: 873
		private static int scenarioBlendingKernel = -1;

		// Token: 0x0400036A RID: 874
		private static readonly int _PoolDim_LerpFactor = Shader.PropertyToID("_PoolDim_LerpFactor");

		// Token: 0x0400036B RID: 875
		private static readonly int _ChunkList = Shader.PropertyToID("_ChunkList");

		// Token: 0x0400036C RID: 876
		private static readonly int _State0_L0_L1Rx = Shader.PropertyToID("_State0_L0_L1Rx");

		// Token: 0x0400036D RID: 877
		private static readonly int _State0_L1G_L1Ry = Shader.PropertyToID("_State0_L1G_L1Ry");

		// Token: 0x0400036E RID: 878
		private static readonly int _State0_L1B_L1Rz = Shader.PropertyToID("_State0_L1B_L1Rz");

		// Token: 0x0400036F RID: 879
		private static readonly int _State0_L2_0 = Shader.PropertyToID("_State0_L2_0");

		// Token: 0x04000370 RID: 880
		private static readonly int _State0_L2_1 = Shader.PropertyToID("_State0_L2_1");

		// Token: 0x04000371 RID: 881
		private static readonly int _State0_L2_2 = Shader.PropertyToID("_State0_L2_2");

		// Token: 0x04000372 RID: 882
		private static readonly int _State0_L2_3 = Shader.PropertyToID("_State0_L2_3");

		// Token: 0x04000373 RID: 883
		private static readonly int _State0_ProbeOcclusion = Shader.PropertyToID("_State0_ProbeOcclusion");

		// Token: 0x04000374 RID: 884
		private static readonly int _State1_L0_L1Rx = Shader.PropertyToID("_State1_L0_L1Rx");

		// Token: 0x04000375 RID: 885
		private static readonly int _State1_L1G_L1Ry = Shader.PropertyToID("_State1_L1G_L1Ry");

		// Token: 0x04000376 RID: 886
		private static readonly int _State1_L1B_L1Rz = Shader.PropertyToID("_State1_L1B_L1Rz");

		// Token: 0x04000377 RID: 887
		private static readonly int _State1_L2_0 = Shader.PropertyToID("_State1_L2_0");

		// Token: 0x04000378 RID: 888
		private static readonly int _State1_L2_1 = Shader.PropertyToID("_State1_L2_1");

		// Token: 0x04000379 RID: 889
		private static readonly int _State1_L2_2 = Shader.PropertyToID("_State1_L2_2");

		// Token: 0x0400037A RID: 890
		private static readonly int _State1_L2_3 = Shader.PropertyToID("_State1_L2_3");

		// Token: 0x0400037B RID: 891
		private static readonly int _State1_ProbeOcclusion = Shader.PropertyToID("_State1_ProbeOcclusion");

		// Token: 0x0400037C RID: 892
		private Vector4[] m_ChunkList;

		// Token: 0x0400037D RID: 893
		private int m_MappedChunks;

		// Token: 0x0400037E RID: 894
		private ProbeBrickPool m_State0;

		// Token: 0x0400037F RID: 895
		private ProbeBrickPool m_State1;

		// Token: 0x04000380 RID: 896
		private ProbeVolumeTextureMemoryBudget m_MemoryBudget;

		// Token: 0x04000381 RID: 897
		private ProbeVolumeSHBands m_ShBands;

		// Token: 0x04000382 RID: 898
		private bool m_ProbeOcclusion;
	}
}
