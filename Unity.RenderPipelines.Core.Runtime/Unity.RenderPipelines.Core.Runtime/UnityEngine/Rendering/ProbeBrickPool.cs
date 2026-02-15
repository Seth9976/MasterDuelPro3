using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020000F8 RID: 248
	internal class ProbeBrickPool
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x0001350A File Offset: 0x0001170A
		internal static int DivRoundUp(int x, int y)
		{
			return (x + y - 1) / y;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00013513 File Offset: 0x00011713
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x0001351B File Offset: 0x0001171B
		internal int estimatedVMemCost { get; private set; }

		// Token: 0x060007D7 RID: 2007 RVA: 0x00013524 File Offset: 0x00011724
		internal static void Initialize()
		{
			if (!SystemInfo.supportsComputeShaders)
			{
				return;
			}
			ProbeVolumeRuntimeResources renderPipelineSettings = GraphicsSettings.GetRenderPipelineSettings<ProbeVolumeRuntimeResources>();
			ProbeBrickPool.s_DataUploadCS = ((renderPipelineSettings != null) ? renderPipelineSettings.probeVolumeUploadDataCS : null);
			ProbeVolumeRuntimeResources renderPipelineSettings2 = GraphicsSettings.GetRenderPipelineSettings<ProbeVolumeRuntimeResources>();
			ProbeBrickPool.s_DataUploadL2CS = ((renderPipelineSettings2 != null) ? renderPipelineSettings2.probeVolumeUploadDataL2CS : null);
			if (ProbeBrickPool.s_DataUploadCS != null)
			{
				ProbeBrickPool.s_DataUploadKernel = (ProbeBrickPool.s_DataUploadCS ? ProbeBrickPool.s_DataUploadCS.FindKernel("UploadData") : (-1));
				ProbeBrickPool.s_DataUpload_Shared = new LocalKeyword(ProbeBrickPool.s_DataUploadCS, "PROBE_VOLUMES_SHARED_DATA");
				ProbeBrickPool.s_DataUpload_ProbeOcclusion = new LocalKeyword(ProbeBrickPool.s_DataUploadCS, "PROBE_VOLUMES_PROBE_OCCLUSION");
				ProbeBrickPool.s_DataUpload_SkyOcclusion = new LocalKeyword(ProbeBrickPool.s_DataUploadCS, "PROBE_VOLUMES_SKY_OCCLUSION");
				ProbeBrickPool.s_DataUpload_SkyShadingDirection = new LocalKeyword(ProbeBrickPool.s_DataUploadCS, "PROBE_VOLUMES_SKY_SHADING_DIRECTION");
			}
			if (ProbeBrickPool.s_DataUploadL2CS != null)
			{
				ProbeBrickPool.s_DataUploadL2Kernel = (ProbeBrickPool.s_DataUploadL2CS ? ProbeBrickPool.s_DataUploadL2CS.FindKernel("UploadDataL2") : (-1));
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00013615 File Offset: 0x00011815
		internal Texture GetValidityTexture()
		{
			return this.m_Pool.TexValidity;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00013622 File Offset: 0x00011822
		internal Texture GetSkyOcclusionTexture()
		{
			return this.m_Pool.TexSkyOcclusion;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001362F File Offset: 0x0001182F
		internal Texture GetSkyShadingDirectionIndicesTexture()
		{
			return this.m_Pool.TexSkyShadingDirectionIndices;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001363C File Offset: 0x0001183C
		internal Texture GetProbeOcclusionTexture()
		{
			return this.m_Pool.TexProbeOcclusion;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001364C File Offset: 0x0001184C
		internal ProbeBrickPool(ProbeVolumeTextureMemoryBudget memoryBudget, ProbeVolumeSHBands shBands, bool allocateValidityData = false, bool allocateRenderingLayerData = false, bool allocateSkyOcclusion = false, bool allocateSkyShadingData = false, bool allocateProbeOcclusionData = false)
		{
			this.m_NextFreeChunk.x = (this.m_NextFreeChunk.y = (this.m_NextFreeChunk.z = 0));
			this.m_SHBands = shBands;
			this.m_ContainsValidity = allocateValidityData;
			this.m_ContainsProbeOcclusion = allocateProbeOcclusionData;
			this.m_ContainsRenderingLayers = allocateRenderingLayerData;
			this.m_ContainsSkyOcclusion = allocateSkyOcclusion;
			this.m_ContainsSkyShadingDirection = allocateSkyShadingData;
			this.m_FreeList = new Stack<ProbeBrickPool.BrickChunkAlloc>(256);
			int width;
			int height;
			int depth;
			ProbeBrickPool.DerivePoolSizeFromBudget(memoryBudget, out width, out height, out depth);
			this.AllocatePool(width, height, depth);
			this.m_AvailableChunkCount = this.m_Pool.width / 512 * (this.m_Pool.height / 4) * (this.m_Pool.depth / 4);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00013710 File Offset: 0x00011910
		internal void AllocatePool(int width, int height, int depth)
		{
			int estimatedCost;
			this.m_Pool = ProbeBrickPool.CreateDataLocation(width * height * depth, false, this.m_SHBands, "APV", true, this.m_ContainsValidity, this.m_ContainsRenderingLayers, this.m_ContainsSkyOcclusion, this.m_ContainsSkyShadingDirection, this.m_ContainsProbeOcclusion, out estimatedCost);
			this.estimatedVMemCost = estimatedCost;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00013761 File Offset: 0x00011961
		public int GetRemainingChunkCount()
		{
			return this.m_AvailableChunkCount;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001376C File Offset: 0x0001196C
		internal void EnsureTextureValidity()
		{
			if (this.m_Pool.TexL0_L1rx == null)
			{
				this.m_Pool.Cleanup();
				this.AllocatePool(this.m_Pool.width, this.m_Pool.height, this.m_Pool.depth);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000137C0 File Offset: 0x000119C0
		internal bool EnsureTextureValidity(bool renderingLayers, bool skyOcclusion, bool skyDirection, bool probeOcclusion)
		{
			if (this.m_ContainsRenderingLayers != renderingLayers || this.m_ContainsSkyOcclusion != skyOcclusion || this.m_ContainsSkyShadingDirection != skyDirection || this.m_ContainsProbeOcclusion != probeOcclusion)
			{
				this.m_Pool.Cleanup();
				this.m_ContainsRenderingLayers = renderingLayers;
				this.m_ContainsSkyOcclusion = skyOcclusion;
				this.m_ContainsSkyShadingDirection = skyDirection;
				this.m_ContainsProbeOcclusion = probeOcclusion;
				this.AllocatePool(this.m_Pool.width, this.m_Pool.height, this.m_Pool.depth);
				return false;
			}
			return true;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00013844 File Offset: 0x00011A44
		internal static int GetChunkSizeInBrickCount()
		{
			return 128;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001384B File Offset: 0x00011A4B
		internal static int GetChunkSizeInProbeCount()
		{
			return 8192;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00013852 File Offset: 0x00011A52
		internal int GetPoolWidth()
		{
			return this.m_Pool.width;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001385F File Offset: 0x00011A5F
		internal int GetPoolHeight()
		{
			return this.m_Pool.height;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001386C File Offset: 0x00011A6C
		internal Vector3Int GetPoolDimensions()
		{
			return new Vector3Int(this.m_Pool.width, this.m_Pool.height, this.m_Pool.depth);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00013894 File Offset: 0x00011A94
		internal void GetRuntimeResources(ref ProbeReferenceVolume.RuntimeResources rr)
		{
			rr.L0_L1rx = this.m_Pool.TexL0_L1rx as RenderTexture;
			rr.L1_G_ry = this.m_Pool.TexL1_G_ry as RenderTexture;
			rr.L1_B_rz = this.m_Pool.TexL1_B_rz as RenderTexture;
			rr.L2_0 = this.m_Pool.TexL2_0 as RenderTexture;
			rr.L2_1 = this.m_Pool.TexL2_1 as RenderTexture;
			rr.L2_2 = this.m_Pool.TexL2_2 as RenderTexture;
			rr.L2_3 = this.m_Pool.TexL2_3 as RenderTexture;
			rr.ProbeOcclusion = this.m_Pool.TexProbeOcclusion as RenderTexture;
			rr.Validity = this.m_Pool.TexValidity as RenderTexture;
			rr.SkyOcclusionL0L1 = this.m_Pool.TexSkyOcclusion as RenderTexture;
			rr.SkyShadingDirectionIndices = this.m_Pool.TexSkyShadingDirectionIndices as RenderTexture;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00013994 File Offset: 0x00011B94
		internal void Clear()
		{
			this.m_FreeList.Clear();
			this.m_NextFreeChunk.x = (this.m_NextFreeChunk.y = (this.m_NextFreeChunk.z = 0));
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000139D4 File Offset: 0x00011BD4
		internal static int GetChunkCount(int brickCount)
		{
			int chunkSize = 128;
			return (brickCount + chunkSize - 1) / chunkSize;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000139F0 File Offset: 0x00011BF0
		internal bool Allocate(int numberOfBrickChunks, List<ProbeBrickPool.BrickChunkAlloc> outAllocations, bool ignoreErrorLog)
		{
			while (this.m_FreeList.Count > 0 && numberOfBrickChunks > 0)
			{
				outAllocations.Add(this.m_FreeList.Pop());
				numberOfBrickChunks--;
				this.m_AvailableChunkCount--;
			}
			uint i = 0U;
			while ((ulong)i < (ulong)((long)numberOfBrickChunks))
			{
				if (this.m_NextFreeChunk.z >= this.m_Pool.depth)
				{
					if (!ignoreErrorLog)
					{
						Debug.LogError("Cannot allocate more brick chunks, probe volume brick pool is full.");
					}
					outAllocations.Clear();
					return false;
				}
				outAllocations.Add(this.m_NextFreeChunk);
				this.m_AvailableChunkCount--;
				this.m_NextFreeChunk.x = this.m_NextFreeChunk.x + 512;
				if (this.m_NextFreeChunk.x >= this.m_Pool.width)
				{
					this.m_NextFreeChunk.x = 0;
					this.m_NextFreeChunk.y = this.m_NextFreeChunk.y + 4;
					if (this.m_NextFreeChunk.y >= this.m_Pool.height)
					{
						this.m_NextFreeChunk.y = 0;
						this.m_NextFreeChunk.z = this.m_NextFreeChunk.z + 4;
					}
				}
				i += 1U;
			}
			return true;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00013B10 File Offset: 0x00011D10
		internal void Deallocate(List<ProbeBrickPool.BrickChunkAlloc> allocations)
		{
			this.m_AvailableChunkCount += allocations.Count;
			foreach (ProbeBrickPool.BrickChunkAlloc brick in allocations)
			{
				this.m_FreeList.Push(brick);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00013B78 File Offset: 0x00011D78
		internal void Update(ProbeBrickPool.DataLocation source, List<ProbeBrickPool.BrickChunkAlloc> srcLocations, List<ProbeBrickPool.BrickChunkAlloc> dstLocations, int destStartIndex, ProbeVolumeSHBands bands)
		{
			for (int i = 0; i < srcLocations.Count; i++)
			{
				ProbeBrickPool.BrickChunkAlloc src = srcLocations[i];
				ProbeBrickPool.BrickChunkAlloc dst = dstLocations[destStartIndex + i];
				for (int j = 0; j < 4; j++)
				{
					int width = Mathf.Min(512, source.width - src.x);
					Graphics.CopyTexture(source.TexL0_L1rx, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexL0_L1rx, dst.z + j, 0, dst.x, dst.y);
					Graphics.CopyTexture(source.TexL1_G_ry, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexL1_G_ry, dst.z + j, 0, dst.x, dst.y);
					Graphics.CopyTexture(source.TexL1_B_rz, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexL1_B_rz, dst.z + j, 0, dst.x, dst.y);
					if (this.m_ContainsValidity)
					{
						Graphics.CopyTexture(source.TexValidity, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexValidity, dst.z + j, 0, dst.x, dst.y);
					}
					if (this.m_ContainsSkyOcclusion)
					{
						Graphics.CopyTexture(source.TexSkyOcclusion, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexSkyOcclusion, dst.z + j, 0, dst.x, dst.y);
						if (this.m_ContainsSkyShadingDirection)
						{
							Graphics.CopyTexture(source.TexSkyShadingDirectionIndices, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexSkyShadingDirectionIndices, dst.z + j, 0, dst.x, dst.y);
						}
					}
					if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
					{
						Graphics.CopyTexture(source.TexL2_0, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexL2_0, dst.z + j, 0, dst.x, dst.y);
						Graphics.CopyTexture(source.TexL2_1, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexL2_1, dst.z + j, 0, dst.x, dst.y);
						Graphics.CopyTexture(source.TexL2_2, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexL2_2, dst.z + j, 0, dst.x, dst.y);
						Graphics.CopyTexture(source.TexL2_3, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexL2_3, dst.z + j, 0, dst.x, dst.y);
					}
					if (this.m_ContainsProbeOcclusion)
					{
						Graphics.CopyTexture(source.TexProbeOcclusion, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexProbeOcclusion, dst.z + j, 0, dst.x, dst.y);
					}
				}
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00013EE8 File Offset: 0x000120E8
		internal void Update(CommandBuffer cmd, ProbeReferenceVolume.CellStreamingScratchBuffer dataBuffer, ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, List<ProbeBrickPool.BrickChunkAlloc> dstLocations, bool updateSharedData, Texture validityTexture, ProbeVolumeSHBands bands, bool skyOcclusion, Texture skyOcclusionTexture, bool skyShadingDirections, Texture skyShadingDirectionsTexture, bool probeOcclusion)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<CoreProfileId>(CoreProfileId.APVDiskStreamingUpdatePool)))
			{
				int chunkCount = dstLocations.Count;
				cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadCS, ProbeBrickPool.s_DataUploadKernel, ProbeBrickPool._Out_L0_L1Rx, this.m_Pool.TexL0_L1rx);
				cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadCS, ProbeBrickPool.s_DataUploadKernel, ProbeBrickPool._Out_L1G_L1Ry, this.m_Pool.TexL1_G_ry);
				cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadCS, ProbeBrickPool.s_DataUploadKernel, ProbeBrickPool._Out_L1B_L1Rz, this.m_Pool.TexL1_B_rz);
				if (updateSharedData)
				{
					cmd.EnableKeyword(ProbeBrickPool.s_DataUploadCS, in ProbeBrickPool.s_DataUpload_Shared);
					cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadCS, ProbeBrickPool.s_DataUploadKernel, ProbeBrickPool._Out_Shared, validityTexture);
					if (skyOcclusion)
					{
						cmd.EnableKeyword(ProbeBrickPool.s_DataUploadCS, in ProbeBrickPool.s_DataUpload_SkyOcclusion);
						cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadCS, ProbeBrickPool.s_DataUploadKernel, ProbeBrickPool._Out_SkyOcclusionL0L1, skyOcclusionTexture);
						if (skyShadingDirections)
						{
							cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadCS, ProbeBrickPool.s_DataUploadKernel, ProbeBrickPool._Out_SkyShadingDirectionIndices, skyShadingDirectionsTexture);
							cmd.EnableKeyword(ProbeBrickPool.s_DataUploadCS, in ProbeBrickPool.s_DataUpload_SkyShadingDirection);
						}
						else
						{
							cmd.DisableKeyword(ProbeBrickPool.s_DataUploadCS, in ProbeBrickPool.s_DataUpload_SkyShadingDirection);
						}
					}
				}
				else
				{
					cmd.DisableKeyword(ProbeBrickPool.s_DataUploadCS, in ProbeBrickPool.s_DataUpload_Shared);
					cmd.DisableKeyword(ProbeBrickPool.s_DataUploadCS, in ProbeBrickPool.s_DataUpload_SkyOcclusion);
					cmd.DisableKeyword(ProbeBrickPool.s_DataUploadCS, in ProbeBrickPool.s_DataUpload_SkyShadingDirection);
				}
				if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
				{
					cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadL2CS, ProbeBrickPool.s_DataUploadL2Kernel, ProbeBrickPool._Out_L2_0, this.m_Pool.TexL2_0);
					cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadL2CS, ProbeBrickPool.s_DataUploadL2Kernel, ProbeBrickPool._Out_L2_1, this.m_Pool.TexL2_1);
					cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadL2CS, ProbeBrickPool.s_DataUploadL2Kernel, ProbeBrickPool._Out_L2_2, this.m_Pool.TexL2_2);
					cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadL2CS, ProbeBrickPool.s_DataUploadL2Kernel, ProbeBrickPool._Out_L2_3, this.m_Pool.TexL2_3);
				}
				if (probeOcclusion)
				{
					cmd.EnableKeyword(ProbeBrickPool.s_DataUploadCS, in ProbeBrickPool.s_DataUpload_ProbeOcclusion);
					cmd.SetComputeTextureParam(ProbeBrickPool.s_DataUploadCS, ProbeBrickPool.s_DataUploadKernel, ProbeBrickPool._Out_ProbeOcclusion, this.m_Pool.TexProbeOcclusion);
				}
				else
				{
					cmd.DisableKeyword(ProbeBrickPool.s_DataUploadCS, in ProbeBrickPool.s_DataUpload_ProbeOcclusion);
				}
				int threadX = ProbeBrickPool.DivRoundUp(2048, 64);
				ConstantBuffer.Push<ProbeReferenceVolume.CellStreamingScratchBufferLayout>(cmd, in layout, ProbeBrickPool.s_DataUploadCS, ProbeBrickPool._ProbeVolumeScratchBufferLayout);
				cmd.SetComputeBufferParam(ProbeBrickPool.s_DataUploadCS, ProbeBrickPool.s_DataUploadKernel, ProbeBrickPool._ProbeVolumeScratchBuffer, dataBuffer.buffer);
				cmd.DispatchCompute(ProbeBrickPool.s_DataUploadCS, ProbeBrickPool.s_DataUploadKernel, threadX, 1, chunkCount);
				if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
				{
					ConstantBuffer.Push<ProbeReferenceVolume.CellStreamingScratchBufferLayout>(cmd, in layout, ProbeBrickPool.s_DataUploadL2CS, ProbeBrickPool._ProbeVolumeScratchBufferLayout);
					cmd.SetComputeBufferParam(ProbeBrickPool.s_DataUploadL2CS, ProbeBrickPool.s_DataUploadL2Kernel, ProbeBrickPool._ProbeVolumeScratchBuffer, dataBuffer.buffer);
					cmd.DispatchCompute(ProbeBrickPool.s_DataUploadL2CS, ProbeBrickPool.s_DataUploadL2Kernel, threadX, 1, chunkCount);
				}
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000141FC File Offset: 0x000123FC
		internal void UpdateValidity(ProbeBrickPool.DataLocation source, List<ProbeBrickPool.BrickChunkAlloc> srcLocations, List<ProbeBrickPool.BrickChunkAlloc> dstLocations, int destStartIndex)
		{
			for (int i = 0; i < srcLocations.Count; i++)
			{
				ProbeBrickPool.BrickChunkAlloc src = srcLocations[i];
				ProbeBrickPool.BrickChunkAlloc dst = dstLocations[destStartIndex + i];
				for (int j = 0; j < 4; j++)
				{
					int width = Mathf.Min(512, source.width - src.x);
					Graphics.CopyTexture(source.TexValidity, src.z + j, 0, src.x, src.y, width, 4, this.m_Pool.TexValidity, dst.z + j, 0, dst.x, dst.y);
				}
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00014298 File Offset: 0x00012498
		internal static Vector3Int ProbeCountToDataLocSize(int numProbes)
		{
			int numBricks = numProbes / 64;
			int poolWidth = 512;
			int depth = (numBricks + poolWidth * poolWidth - 1) / (poolWidth * poolWidth);
			int width;
			int height;
			if (depth > 1)
			{
				height = (width = poolWidth);
			}
			else
			{
				height = (numBricks + poolWidth - 1) / poolWidth;
				if (height > 1)
				{
					width = poolWidth;
				}
				else
				{
					width = numBricks;
				}
			}
			width *= 4;
			height *= 4;
			depth *= 4;
			return new Vector3Int(width, height, depth);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000142F4 File Offset: 0x000124F4
		private static int EstimateMemoryCost(int width, int height, int depth, GraphicsFormat format)
		{
			int elementSize = ((format == GraphicsFormat.R16G16B16A16_SFloat) ? 8 : ((format == GraphicsFormat.R8G8B8A8_UNorm) ? 4 : 1));
			return width * height * depth * elementSize;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001431C File Offset: 0x0001251C
		internal static int EstimateMemoryCostForBlending(ProbeVolumeTextureMemoryBudget memoryBudget, bool compressed, ProbeVolumeSHBands bands)
		{
			if (memoryBudget == (ProbeVolumeTextureMemoryBudget)0)
			{
				return 0;
			}
			int width;
			int height;
			int depth;
			ProbeBrickPool.DerivePoolSizeFromBudget(memoryBudget, out width, out height, out depth);
			Vector3Int locSize = ProbeBrickPool.ProbeCountToDataLocSize(width * height * depth);
			width = locSize.x;
			height = locSize.y;
			depth = locSize.z;
			int allocatedBytes = 0;
			GraphicsFormat L0Format = GraphicsFormat.R16G16B16A16_SFloat;
			GraphicsFormat L1L2Format = (compressed ? GraphicsFormat.RGBA_BC7_UNorm : GraphicsFormat.R8G8B8A8_UNorm);
			allocatedBytes += ProbeBrickPool.EstimateMemoryCost(width, height, depth, L0Format);
			allocatedBytes += ProbeBrickPool.EstimateMemoryCost(width, height, depth, L1L2Format) * 2;
			if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				allocatedBytes += ProbeBrickPool.EstimateMemoryCost(width, height, depth, L1L2Format) * 3;
			}
			return allocatedBytes;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000143A8 File Offset: 0x000125A8
		public static Texture CreateDataTexture(int width, int height, int depth, GraphicsFormat format, string name, bool allocateRendertexture, ref int allocatedBytes)
		{
			allocatedBytes += ProbeBrickPool.EstimateMemoryCost(width, height, depth, format);
			Texture texture;
			if (allocateRendertexture)
			{
				texture = new RenderTexture(new RenderTextureDescriptor
				{
					width = width,
					height = height,
					volumeDepth = depth,
					graphicsFormat = format,
					mipCount = 1,
					enableRandomWrite = true,
					dimension = TextureDimension.Tex3D,
					msaaSamples = 1
				});
			}
			else
			{
				texture = new Texture3D(width, height, depth, format, TextureCreationFlags.None, 1);
			}
			texture.hideFlags = HideFlags.HideAndDontSave;
			texture.name = name;
			if (allocateRendertexture)
			{
				(texture as RenderTexture).Create();
			}
			return texture;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00014448 File Offset: 0x00012648
		public static ProbeBrickPool.DataLocation CreateDataLocation(int numProbes, bool compressed, ProbeVolumeSHBands bands, string name, bool allocateRendertexture, bool allocateValidityData, bool allocateRenderingLayers, bool allocateSkyOcclusionData, bool allocateSkyShadingDirectionData, bool allocateProbeOcclusionData, out int allocatedBytes)
		{
			Vector3Int locSize = ProbeBrickPool.ProbeCountToDataLocSize(numProbes);
			int width = locSize.x;
			int height = locSize.y;
			int depth = locSize.z;
			GraphicsFormat L0Format = GraphicsFormat.R16G16B16A16_SFloat;
			GraphicsFormat L1L2Format = (compressed ? GraphicsFormat.RGBA_BC7_UNorm : GraphicsFormat.R8G8B8A8_UNorm);
			GraphicsFormat ValidityFormat = (allocateRenderingLayers ? GraphicsFormat.R32_SFloat : (SystemInfo.IsFormatSupported(GraphicsFormat.R8_UNorm, GraphicsFormatUsage.Sample | GraphicsFormatUsage.LoadStore) ? GraphicsFormat.R8_UNorm : GraphicsFormat.R8G8B8A8_UNorm));
			allocatedBytes = 0;
			ProbeBrickPool.DataLocation loc;
			loc.TexL0_L1rx = ProbeBrickPool.CreateDataTexture(width, height, depth, L0Format, name + "_TexL0_L1rx", allocateRendertexture, ref allocatedBytes);
			loc.TexL1_G_ry = ProbeBrickPool.CreateDataTexture(width, height, depth, L1L2Format, name + "_TexL1_G_ry", allocateRendertexture, ref allocatedBytes);
			loc.TexL1_B_rz = ProbeBrickPool.CreateDataTexture(width, height, depth, L1L2Format, name + "_TexL1_B_rz", allocateRendertexture, ref allocatedBytes);
			if (allocateValidityData)
			{
				loc.TexValidity = ProbeBrickPool.CreateDataTexture(width, height, depth, ValidityFormat, name + "_Validity", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				loc.TexValidity = null;
			}
			if (allocateSkyOcclusionData)
			{
				loc.TexSkyOcclusion = ProbeBrickPool.CreateDataTexture(width, height, depth, GraphicsFormat.R16G16B16A16_SFloat, name + "_SkyOcclusion", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				loc.TexSkyOcclusion = null;
			}
			if (allocateSkyShadingDirectionData)
			{
				loc.TexSkyShadingDirectionIndices = ProbeBrickPool.CreateDataTexture(width, height, depth, GraphicsFormat.R8_UNorm, name + "_SkyShadingDirectionIndices", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				loc.TexSkyShadingDirectionIndices = null;
			}
			if (allocateProbeOcclusionData)
			{
				loc.TexProbeOcclusion = ProbeBrickPool.CreateDataTexture(width, height, depth, GraphicsFormat.R8G8B8A8_UNorm, name + "_ProbeOcclusion", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				loc.TexProbeOcclusion = null;
			}
			if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				loc.TexL2_0 = ProbeBrickPool.CreateDataTexture(width, height, depth, L1L2Format, name + "_TexL2_0", allocateRendertexture, ref allocatedBytes);
				loc.TexL2_1 = ProbeBrickPool.CreateDataTexture(width, height, depth, L1L2Format, name + "_TexL2_1", allocateRendertexture, ref allocatedBytes);
				loc.TexL2_2 = ProbeBrickPool.CreateDataTexture(width, height, depth, L1L2Format, name + "_TexL2_2", allocateRendertexture, ref allocatedBytes);
				loc.TexL2_3 = ProbeBrickPool.CreateDataTexture(width, height, depth, L1L2Format, name + "_TexL2_3", allocateRendertexture, ref allocatedBytes);
			}
			else
			{
				loc.TexL2_0 = null;
				loc.TexL2_1 = null;
				loc.TexL2_2 = null;
				loc.TexL2_3 = null;
			}
			loc.width = width;
			loc.height = height;
			loc.depth = depth;
			return loc;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001467A File Offset: 0x0001287A
		private static void DerivePoolSizeFromBudget(ProbeVolumeTextureMemoryBudget memoryBudget, out int width, out int height, out int depth)
		{
			width = (int)memoryBudget;
			height = (int)memoryBudget;
			depth = 4;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00014685 File Offset: 0x00012885
		internal void Cleanup()
		{
			this.m_Pool.Cleanup();
		}

		// Token: 0x04000331 RID: 817
		internal static readonly int _Out_L0_L1Rx = Shader.PropertyToID("_Out_L0_L1Rx");

		// Token: 0x04000332 RID: 818
		internal static readonly int _Out_L1G_L1Ry = Shader.PropertyToID("_Out_L1G_L1Ry");

		// Token: 0x04000333 RID: 819
		internal static readonly int _Out_L1B_L1Rz = Shader.PropertyToID("_Out_L1B_L1Rz");

		// Token: 0x04000334 RID: 820
		internal static readonly int _Out_Shared = Shader.PropertyToID("_Out_Shared");

		// Token: 0x04000335 RID: 821
		internal static readonly int _Out_ProbeOcclusion = Shader.PropertyToID("_Out_ProbeOcclusion");

		// Token: 0x04000336 RID: 822
		internal static readonly int _Out_SkyOcclusionL0L1 = Shader.PropertyToID("_Out_SkyOcclusionL0L1");

		// Token: 0x04000337 RID: 823
		internal static readonly int _Out_SkyShadingDirectionIndices = Shader.PropertyToID("_Out_SkyShadingDirectionIndices");

		// Token: 0x04000338 RID: 824
		internal static readonly int _Out_L2_0 = Shader.PropertyToID("_Out_L2_0");

		// Token: 0x04000339 RID: 825
		internal static readonly int _Out_L2_1 = Shader.PropertyToID("_Out_L2_1");

		// Token: 0x0400033A RID: 826
		internal static readonly int _Out_L2_2 = Shader.PropertyToID("_Out_L2_2");

		// Token: 0x0400033B RID: 827
		internal static readonly int _Out_L2_3 = Shader.PropertyToID("_Out_L2_3");

		// Token: 0x0400033C RID: 828
		internal static readonly int _ProbeVolumeScratchBufferLayout = Shader.PropertyToID("CellStreamingScratchBufferLayout");

		// Token: 0x0400033D RID: 829
		internal static readonly int _ProbeVolumeScratchBuffer = Shader.PropertyToID("_ScratchBuffer");

		// Token: 0x0400033E RID: 830
		private const int kChunkSizeInBricks = 128;

		// Token: 0x0400033F RID: 831
		internal const int kBrickCellCount = 3;

		// Token: 0x04000340 RID: 832
		internal const int kBrickProbeCountPerDim = 4;

		// Token: 0x04000341 RID: 833
		internal const int kBrickProbeCountTotal = 64;

		// Token: 0x04000342 RID: 834
		internal const int kChunkProbeCountPerDim = 512;

		// Token: 0x04000344 RID: 836
		private const int kMaxPoolWidth = 2048;

		// Token: 0x04000345 RID: 837
		internal ProbeBrickPool.DataLocation m_Pool;

		// Token: 0x04000346 RID: 838
		private ProbeBrickPool.BrickChunkAlloc m_NextFreeChunk;

		// Token: 0x04000347 RID: 839
		private Stack<ProbeBrickPool.BrickChunkAlloc> m_FreeList;

		// Token: 0x04000348 RID: 840
		private int m_AvailableChunkCount;

		// Token: 0x04000349 RID: 841
		private ProbeVolumeSHBands m_SHBands;

		// Token: 0x0400034A RID: 842
		private bool m_ContainsValidity;

		// Token: 0x0400034B RID: 843
		private bool m_ContainsProbeOcclusion;

		// Token: 0x0400034C RID: 844
		private bool m_ContainsRenderingLayers;

		// Token: 0x0400034D RID: 845
		private bool m_ContainsSkyOcclusion;

		// Token: 0x0400034E RID: 846
		private bool m_ContainsSkyShadingDirection;

		// Token: 0x0400034F RID: 847
		private static ComputeShader s_DataUploadCS;

		// Token: 0x04000350 RID: 848
		private static int s_DataUploadKernel;

		// Token: 0x04000351 RID: 849
		private static ComputeShader s_DataUploadL2CS;

		// Token: 0x04000352 RID: 850
		private static int s_DataUploadL2Kernel;

		// Token: 0x04000353 RID: 851
		private static LocalKeyword s_DataUpload_Shared;

		// Token: 0x04000354 RID: 852
		private static LocalKeyword s_DataUpload_ProbeOcclusion;

		// Token: 0x04000355 RID: 853
		private static LocalKeyword s_DataUpload_SkyOcclusion;

		// Token: 0x04000356 RID: 854
		private static LocalKeyword s_DataUpload_SkyShadingDirection;

		// Token: 0x020000F9 RID: 249
		[DebuggerDisplay("Chunk ({x}, {y}, {z})")]
		public struct BrickChunkAlloc
		{
			// Token: 0x060007F6 RID: 2038 RVA: 0x00014764 File Offset: 0x00012964
			internal int flattenIndex(int sx, int sy)
			{
				return this.z * (sx * sy) + this.y * sx + this.x;
			}

			// Token: 0x04000357 RID: 855
			public int x;

			// Token: 0x04000358 RID: 856
			public int y;

			// Token: 0x04000359 RID: 857
			public int z;
		}

		// Token: 0x020000FA RID: 250
		public struct DataLocation
		{
			// Token: 0x060007F7 RID: 2039 RVA: 0x00014780 File Offset: 0x00012980
			internal void Cleanup()
			{
				CoreUtils.Destroy(this.TexL0_L1rx);
				CoreUtils.Destroy(this.TexL1_G_ry);
				CoreUtils.Destroy(this.TexL1_B_rz);
				CoreUtils.Destroy(this.TexL2_0);
				CoreUtils.Destroy(this.TexL2_1);
				CoreUtils.Destroy(this.TexL2_2);
				CoreUtils.Destroy(this.TexL2_3);
				CoreUtils.Destroy(this.TexProbeOcclusion);
				CoreUtils.Destroy(this.TexValidity);
				CoreUtils.Destroy(this.TexSkyOcclusion);
				CoreUtils.Destroy(this.TexSkyShadingDirectionIndices);
				this.TexL0_L1rx = null;
				this.TexL1_G_ry = null;
				this.TexL1_B_rz = null;
				this.TexL2_0 = null;
				this.TexL2_1 = null;
				this.TexL2_2 = null;
				this.TexL2_3 = null;
				this.TexProbeOcclusion = null;
				this.TexValidity = null;
				this.TexSkyOcclusion = null;
				this.TexSkyShadingDirectionIndices = null;
			}

			// Token: 0x0400035A RID: 858
			internal Texture TexL0_L1rx;

			// Token: 0x0400035B RID: 859
			internal Texture TexL1_G_ry;

			// Token: 0x0400035C RID: 860
			internal Texture TexL1_B_rz;

			// Token: 0x0400035D RID: 861
			internal Texture TexL2_0;

			// Token: 0x0400035E RID: 862
			internal Texture TexL2_1;

			// Token: 0x0400035F RID: 863
			internal Texture TexL2_2;

			// Token: 0x04000360 RID: 864
			internal Texture TexL2_3;

			// Token: 0x04000361 RID: 865
			internal Texture TexProbeOcclusion;

			// Token: 0x04000362 RID: 866
			internal Texture TexValidity;

			// Token: 0x04000363 RID: 867
			internal Texture TexSkyOcclusion;

			// Token: 0x04000364 RID: 868
			internal Texture TexSkyShadingDirectionIndices;

			// Token: 0x04000365 RID: 869
			internal int width;

			// Token: 0x04000366 RID: 870
			internal int height;

			// Token: 0x04000367 RID: 871
			internal int depth;
		}
	}
}
