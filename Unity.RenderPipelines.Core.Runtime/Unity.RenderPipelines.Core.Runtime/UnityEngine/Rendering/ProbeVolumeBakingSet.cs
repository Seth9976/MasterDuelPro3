using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x0200012A RID: 298
	public sealed class ProbeVolumeBakingSet : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001E51F File Offset: 0x0001C71F
		internal bool hasDilation
		{
			get
			{
				return this.settings.dilationSettings.enableDilation && this.settings.dilationSettings.dilationDistance > 0f;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0001E54C File Offset: 0x0001C74C
		public IReadOnlyList<string> sceneGUIDs
		{
			get
			{
				return this.m_SceneGUIDs;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001E554 File Offset: 0x0001C754
		public IReadOnlyList<string> lightingScenarios
		{
			get
			{
				return this.m_LightingScenarios;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0001E55C File Offset: 0x0001C75C
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0001E56A File Offset: 0x0001C76A
		internal bool bakedSkyOcclusion
		{
			get
			{
				return this.bakedSkyOcclusionValue > 0;
			}
			set
			{
				this.bakedSkyOcclusionValue = (value ? 1 : 0);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0001E579 File Offset: 0x0001C779
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x0001E587 File Offset: 0x0001C787
		internal bool bakedSkyShadingDirection
		{
			get
			{
				return this.bakedSkyShadingDirectionValue > 0;
			}
			set
			{
				this.bakedSkyShadingDirectionValue = (value ? 1 : 0);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001E596 File Offset: 0x0001C796
		internal string otherScenario
		{
			get
			{
				return this.m_OtherScenario;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001E59E File Offset: 0x0001C79E
		internal float scenarioBlendingFactor
		{
			get
			{
				return this.m_ScenarioBlendingFactor;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0001E5A6 File Offset: 0x0001C7A6
		public int cellSizeInBricks
		{
			get
			{
				return ProbeVolumeBakingSet.GetCellSizeInBricks(this.bakedSimplificationLevels);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0001E5B3 File Offset: 0x0001C7B3
		public int maxSubdivision
		{
			get
			{
				return ProbeVolumeBakingSet.GetMaxSubdivision(this.bakedSimplificationLevels);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
		public float minBrickSize
		{
			get
			{
				return ProbeVolumeBakingSet.GetMinBrickSize(this.bakedMinDistanceBetweenProbes);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0001E5CD File Offset: 0x0001C7CD
		public float cellSizeInMeters
		{
			get
			{
				return (float)this.cellSizeInBricks * this.minBrickSize;
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001E5E0 File Offset: 0x0001C7E0
		internal uint4 ComputeRegionMasks()
		{
			uint4 masks = 0U;
			if (!this.useRenderingLayers || this.renderingLayerMasks == null)
			{
				masks.x = uint.MaxValue;
			}
			else
			{
				for (int i = 0; i < this.renderingLayerMasks.Length; i++)
				{
					masks[i] = this.renderingLayerMasks[i].mask;
				}
			}
			return masks;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001B6A5 File Offset: 0x000198A5
		internal static int GetCellSizeInBricks(int simplificationLevels)
		{
			return (int)Mathf.Pow(3f, (float)simplificationLevels);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001E640 File Offset: 0x0001C840
		internal static int GetMaxSubdivision(int simplificationLevels)
		{
			return simplificationLevels + 1;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001E645 File Offset: 0x0001C845
		internal static float GetMinBrickSize(float minDistanceBetweenProbes)
		{
			return Mathf.Max(0.01f, minDistanceBetweenProbes * 3f);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001E658 File Offset: 0x0001C858
		private void OnValidate()
		{
			this.singleSceneMode &= this.m_SceneGUIDs.Count <= 1;
			if (this.m_LightingScenarios.Count == 0)
			{
				this.m_LightingScenarios = new List<string> { ProbeReferenceVolume.defaultLightingScenario };
			}
			this.settings.Upgrade();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001E6B1 File Offset: 0x0001C8B1
		private void OnEnable()
		{
			this.Migrate();
			this.m_HasSupportData = this.ComputeHasSupportData();
			this.m_SharedDataIsValid = this.ComputeHasValidSharedData();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
		internal void Migrate()
		{
			if (this.version != CoreUtils.GetLastEnumValue<ProbeVolumeBakingSet.Version>())
			{
				ProbeVolumeBakingSet.Version version = this.version;
			}
			if (this.sharedValidityMaskChunkSize == 0)
			{
				this.sharedValidityMaskChunkSize = ProbeBrickPool.GetChunkSizeInProbeCount();
			}
			if (this.settings.virtualOffsetSettings.validityThreshold == 0f)
			{
				this.settings.virtualOffsetSettings.validityThreshold = 0.25f;
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001E736 File Offset: 0x0001C936
		private bool ComputeHasValidSharedData()
		{
			return this.cellSharedDataAsset != null && this.cellSharedDataAsset.FileExists() && this.cellBricksDataAsset.FileExists();
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001E75A File Offset: 0x0001C95A
		internal bool HasValidSharedData()
		{
			return this.m_SharedDataIsValid;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001E764 File Offset: 0x0001C964
		internal bool CheckCompatibleCellLayout()
		{
			return this.simplificationLevels == this.bakedSimplificationLevels && this.minDistanceBetweenProbes == this.bakedMinDistanceBetweenProbes && this.skyOcclusion == this.bakedSkyOcclusion && this.skyOcclusionShadingDirection == this.bakedSkyShadingDirection && this.settings.virtualOffsetSettings.useVirtualOffset == (this.supportOffsetsChunkSize != 0) && this.useRenderingLayers == (this.bakedMaskCount != 1);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001E7DA File Offset: 0x0001C9DA
		private bool ComputeHasSupportData()
		{
			return this.cellSupportDataAsset != null && this.cellSupportDataAsset.IsValid() && this.cellSupportDataAsset.FileExists();
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001E7FE File Offset: 0x0001C9FE
		internal bool HasSupportData()
		{
			return this.m_HasSupportData;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001E806 File Offset: 0x0001CA06
		public bool HasBakedData(string scenario = null)
		{
			if (scenario == null)
			{
				return this.scenarios.ContainsKey(ProbeReferenceVolume.defaultLightingScenario);
			}
			return (ProbeReferenceVolume.instance.supportLightingScenarios || !(scenario != ProbeReferenceVolume.defaultLightingScenario)) && this.scenarios.ContainsKey(scenario);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001E844 File Offset: 0x0001CA44
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (!this.m_LightingScenarios.Contains(this.lightingScenario))
			{
				if (this.m_LightingScenarios.Count != 0)
				{
					this.lightingScenario = this.m_LightingScenarios[0];
				}
				else
				{
					this.lightingScenario = ProbeReferenceVolume.defaultLightingScenario;
				}
			}
			this.perSceneCellLists.Clear();
			foreach (ProbeVolumeBakingSet.SerializedPerSceneCellList scene in this.m_SerializedPerSceneCellList)
			{
				this.perSceneCellLists.Add(scene.sceneGUID, scene.cellList);
			}
			if (this.m_OtherScenario == "")
			{
				this.m_OtherScenario = null;
			}
			if (this.bakedSimplificationLevels == -1)
			{
				this.bakedSimplificationLevels = this.simplificationLevels;
				this.bakedMinDistanceBetweenProbes = this.minDistanceBetweenProbes;
			}
			if (this.bakedSkyOcclusionValue == -1)
			{
				this.bakedSkyOcclusion = false;
			}
			if (this.bakedSkyShadingDirectionValue == -1)
			{
				this.bakedSkyShadingDirection = false;
			}
			if (this.cellDescs.Count != 0)
			{
				Dictionary<int, ProbeReferenceVolume.CellDesc>.ValueCollection.Enumerator enumerator = this.cellDescs.Values.GetEnumerator();
				enumerator.MoveNext();
				if (enumerator.Current.bricksCount == 0)
				{
					foreach (ProbeReferenceVolume.CellDesc cellDesc in this.cellDescs.Values)
					{
						cellDesc.probeCount /= 64;
					}
				}
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001E9CC File Offset: 0x0001CBCC
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_SerializedPerSceneCellList = new List<ProbeVolumeBakingSet.SerializedPerSceneCellList>();
			foreach (KeyValuePair<string, List<int>> kvp in this.perSceneCellLists)
			{
				this.m_SerializedPerSceneCellList.Add(new ProbeVolumeBakingSet.SerializedPerSceneCellList
				{
					sceneGUID = kvp.Key,
					cellList = kvp.Value
				});
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001EA54 File Offset: 0x0001CC54
		internal void Initialize(bool useStreamingAsset)
		{
			foreach (KeyValuePair<string, ProbeVolumeBakingSet.PerScenarioDataInfo> scenario in this.scenarios)
			{
				scenario.Value.Initialize(ProbeReferenceVolume.instance.shBands);
			}
			if (!useStreamingAsset)
			{
				this.m_UseStreamingAsset = false;
				this.m_TotalIndexList.Clear();
				foreach (int index in this.cellDescs.Keys)
				{
					this.m_TotalIndexList.Add(index);
				}
				this.ResolveAllCellData();
			}
			if (ProbeReferenceVolume.instance.supportScenarioBlending)
			{
				this.BlendLightingScenario(null, 0f);
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001EB38 File Offset: 0x0001CD38
		internal void Cleanup()
		{
			if (this.cellSharedDataAsset != null)
			{
				this.cellSharedDataAsset.Dispose();
				foreach (KeyValuePair<string, ProbeVolumeBakingSet.PerScenarioDataInfo> scenario in this.scenarios)
				{
					if (scenario.Value.IsValid())
					{
						scenario.Value.cellDataAsset.Dispose();
						scenario.Value.cellOptionalDataAsset.Dispose();
						scenario.Value.cellProbeOcclusionDataAsset.Dispose();
					}
				}
			}
			if (this.m_ReadCommandBuffer.IsCreated)
			{
				this.m_ReadCommandBuffer.Dispose();
			}
			foreach (NativeArray<byte> buffer in this.m_ReadOperationScratchBuffers)
			{
				buffer.Dispose();
			}
			this.m_ReadOperationScratchBuffers.Clear();
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001EC40 File Offset: 0x0001CE40
		internal void SetActiveScenario(string scenario, bool verbose = true)
		{
			if (this.lightingScenario == scenario)
			{
				return;
			}
			if (!this.m_LightingScenarios.Contains(scenario))
			{
				if (verbose)
				{
					Debug.LogError("Scenario '" + scenario + "' does not exist.");
				}
				return;
			}
			if (!this.scenarios.ContainsKey(scenario) && verbose)
			{
				Debug.LogError("Scenario '" + scenario + "' has not been baked.");
			}
			this.lightingScenario = scenario;
			this.m_ScenarioBlendingFactor = 0f;
			if (ProbeReferenceVolume.instance.supportScenarioBlending)
			{
				ProbeReferenceVolume.instance.ScenarioBlendingChanged(true);
				return;
			}
			ProbeReferenceVolume.instance.UnloadAllCells();
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		internal void BlendLightingScenario(string otherScenario, float blendingFactor)
		{
			if (!string.IsNullOrEmpty(otherScenario) && !ProbeReferenceVolume.instance.supportScenarioBlending)
			{
				return;
			}
			if (otherScenario != null && !this.m_LightingScenarios.Contains(otherScenario))
			{
				Debug.LogError("Scenario '" + otherScenario + "' does not exist.");
				return;
			}
			if (otherScenario != null && !this.scenarios.ContainsKey(otherScenario))
			{
				Debug.LogError("Scenario '" + otherScenario + "' has not been baked.");
				return;
			}
			blendingFactor = Mathf.Clamp01(blendingFactor);
			if (otherScenario == this.lightingScenario || string.IsNullOrEmpty(otherScenario))
			{
				otherScenario = null;
			}
			if (otherScenario == null)
			{
				blendingFactor = 0f;
			}
			if (otherScenario == this.m_OtherScenario && Mathf.Approximately(blendingFactor, this.m_ScenarioBlendingFactor))
			{
				return;
			}
			bool scenarioChanged = otherScenario != this.m_OtherScenario;
			this.m_OtherScenario = otherScenario;
			this.m_ScenarioBlendingFactor = blendingFactor;
			ProbeReferenceVolume.instance.ScenarioBlendingChanged(scenarioChanged);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001EDC0 File Offset: 0x0001CFC0
		internal int GetBakingHashCode()
		{
			return ((((this.maxCellPosition.GetHashCode() * 23 + this.minCellPosition.GetHashCode()) * 23 + this.globalBounds.GetHashCode()) * 23 + this.cellSizeInBricks.GetHashCode()) * 23 + this.simplificationLevels.GetHashCode()) * 23 + this.minDistanceBetweenProbes.GetHashCode();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001EE38 File Offset: 0x0001D038
		private static int AlignUp16(int count)
		{
			int alignment = 16;
			int remainder = count % alignment;
			return count + ((remainder == 0) ? 0 : (alignment - remainder));
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001EE58 File Offset: 0x0001D058
		private NativeArray<T> GetSubArray<T>(NativeArray<byte> input, int count, ref int offset) where T : struct
		{
			int size = count * UnsafeUtility.SizeOf<T>();
			if (offset + size > input.Length)
			{
				return default(NativeArray<T>);
			}
			NativeArray<T> nativeArray = input.GetSubArray(offset, size).Reinterpret<T>(1);
			offset = ProbeVolumeBakingSet.AlignUp16(offset + size);
			return nativeArray;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		private NativeArray<byte> RequestScratchBuffer(int size)
		{
			if (this.m_ReadOperationScratchBuffers.Count == 0)
			{
				return new NativeArray<byte>(size, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
			NativeArray<byte> buffer = this.m_ReadOperationScratchBuffers.Pop();
			if (buffer.Length < size)
			{
				buffer.Dispose();
				return new NativeArray<byte>(size, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
			return buffer;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001EEF0 File Offset: 0x0001D0F0
		private unsafe NativeArray<T> LoadStreambleAssetData<T>(ProbeVolumeStreamableAsset asset, List<int> cellIndices) where T : struct
		{
			if (!this.m_UseStreamingAsset)
			{
				return asset.asset.GetData<byte>().Reinterpret<T>(1);
			}
			if (!this.m_ReadCommandBuffer.IsCreated || this.m_ReadCommandBuffer.Length < cellIndices.Count)
			{
				if (this.m_ReadCommandBuffer.IsCreated)
				{
					this.m_ReadCommandBuffer.Dispose();
				}
				this.m_ReadCommandBuffer = new NativeArray<ReadCommand>(cellIndices.Count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
			int totalSize = 0;
			int commandIndex = 0;
			foreach (int cellIndex in cellIndices)
			{
				ProbeReferenceVolume.CellDesc cellDesc = this.cellDescs[cellIndex];
				ProbeVolumeStreamableAsset.StreamableCellDesc streamableCellDesc = asset.streamableCellDescs[cellIndex];
				ReadCommand command = default(ReadCommand);
				command.Offset = (long)streamableCellDesc.offset;
				command.Size = (long)(streamableCellDesc.elementCount * asset.elementSize);
				command.Buffer = null;
				this.m_ReadCommandBuffer[commandIndex++] = command;
				totalSize += (int)command.Size;
			}
			NativeArray<byte> scratchBuffer = this.RequestScratchBuffer(totalSize);
			commandIndex = 0;
			long outputOffset = 0L;
			byte* scratchPtr = (byte*)scratchBuffer.GetUnsafePtr<byte>();
			foreach (int num in cellIndices)
			{
				ReadCommand command2 = this.m_ReadCommandBuffer[commandIndex];
				command2.Buffer = (void*)(scratchPtr + outputOffset);
				outputOffset += command2.Size;
				this.m_ReadCommandBuffer[commandIndex++] = command2;
			}
			this.m_ReadCommandArray.CommandCount = cellIndices.Count;
			this.m_ReadCommandArray.ReadCommands = (ReadCommand*)this.m_ReadCommandBuffer.GetUnsafePtr<ReadCommand>();
			FileHandle fileHandle = asset.OpenFile();
			ReadHandle readHandle = AsyncReadManager.Read(in fileHandle, this.m_ReadCommandArray);
			readHandle.JobHandle.Complete();
			asset.CloseFile();
			readHandle.Dispose();
			return scratchBuffer.Reinterpret<T>(1);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001F104 File Offset: 0x0001D304
		private void ReleaseStreamableAssetData<T>(NativeArray<T> buffer) where T : struct
		{
			if (this.m_UseStreamingAsset)
			{
				this.m_ReadOperationScratchBuffers.Push(buffer.Reinterpret<byte>(UnsafeUtility.SizeOf<T>()));
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001F128 File Offset: 0x0001D328
		private void PruneCellIndexList(List<int> cellIndices, List<int> prunedIndexList)
		{
			prunedIndexList.Clear();
			foreach (int cellIndex in cellIndices)
			{
				if (!this.cellDataMap.ContainsKey(cellIndex))
				{
					prunedIndexList.Add(cellIndex);
				}
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001F18C File Offset: 0x0001D38C
		private void PruneCellIndexListForScenario(List<int> cellIndices, ProbeVolumeBakingSet.PerScenarioDataInfo scenarioData, List<int> prunedIndexList)
		{
			prunedIndexList.Clear();
			foreach (int cellIndex in cellIndices)
			{
				if (scenarioData.cellDataAsset.streamableCellDescs.ContainsKey(cellIndex))
				{
					prunedIndexList.Add(cellIndex);
				}
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
		internal List<int> GetSceneCellIndexList(string sceneGUID)
		{
			List<int> indexList;
			if (this.perSceneCellLists.TryGetValue(sceneGUID, out indexList))
			{
				return indexList;
			}
			return null;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001F214 File Offset: 0x0001D414
		private bool ResolveAllCellData()
		{
			return this.ResolveSharedCellData(this.m_TotalIndexList) && this.ResolvePerScenarioCellData(this.m_TotalIndexList);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001F234 File Offset: 0x0001D434
		internal bool ResolveCellData(List<int> cellIndices)
		{
			if (!this.m_UseStreamingAsset)
			{
				return true;
			}
			if (cellIndices == null)
			{
				return false;
			}
			this.PruneCellIndexList(cellIndices, this.m_PrunedIndexList);
			if (ProbeReferenceVolume.instance.diskStreamingEnabled)
			{
				foreach (int cell in this.m_PrunedIndexList)
				{
					ProbeReferenceVolume.CellData newCellData = new ProbeReferenceVolume.CellData();
					foreach (KeyValuePair<string, ProbeVolumeBakingSet.PerScenarioDataInfo> scenario in this.scenarios)
					{
						newCellData.scenarios.Add(scenario.Key, default(ProbeReferenceVolume.CellData.PerScenarioData));
					}
					this.cellDataMap.Add(cell, newCellData);
				}
				return true;
			}
			return this.ResolveSharedCellData(this.m_PrunedIndexList) && this.ResolvePerScenarioCellData(this.m_PrunedIndexList);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001F334 File Offset: 0x0001D534
		private void ResolveSharedCellData(List<int> cellIndices, NativeArray<ProbeBrickIndex.Brick> bricksData, NativeArray<byte> cellSharedData, NativeArray<byte> cellSupportData)
		{
			ProbeReferenceVolume prv = ProbeReferenceVolume.instance;
			bool hasSupportData = cellSupportData.Length != 0;
			int sharedDataChunkOffset = 0;
			int supportDataChunkOffset = 0;
			int totalBricksCount = 0;
			int totalSHChunkCount = 0;
			for (int i = 0; i < cellIndices.Count; i++)
			{
				int cellIndex = cellIndices[i];
				ProbeReferenceVolume.CellData cellData = new ProbeReferenceVolume.CellData();
				ProbeReferenceVolume.CellDesc cellDesc = this.cellDescs[cellIndex];
				int bricksCount = cellDesc.bricksCount;
				int shChunkCount = cellDesc.shChunkCount;
				NativeArray<ProbeBrickIndex.Brick> sourceBricks = bricksData.GetSubArray(totalBricksCount, bricksCount);
				NativeArray<byte> sourceValidityNeightMaskData = cellSharedData.GetSubArray(sharedDataChunkOffset, this.sharedValidityMaskChunkSize * shChunkCount);
				sharedDataChunkOffset += this.sharedValidityMaskChunkSize * shChunkCount;
				cellData.bricks = (this.m_UseStreamingAsset ? new NativeArray<ProbeBrickIndex.Brick>(sourceBricks, Allocator.Persistent) : sourceBricks);
				cellData.validityNeighMaskData = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceValidityNeightMaskData, Allocator.Persistent) : sourceValidityNeightMaskData);
				if (this.bakedSkyOcclusion)
				{
					if (prv.skyOcclusion)
					{
						NativeArray<ushort> sourceSkyOcclusionDataL0L = cellSharedData.GetSubArray(sharedDataChunkOffset, this.sharedSkyOcclusionL0L1ChunkSize * shChunkCount).Reinterpret<ushort>(1);
						cellData.skyOcclusionDataL0L1 = (this.m_UseStreamingAsset ? new NativeArray<ushort>(sourceSkyOcclusionDataL0L, Allocator.Persistent) : sourceSkyOcclusionDataL0L);
					}
					sharedDataChunkOffset += this.sharedSkyOcclusionL0L1ChunkSize * shChunkCount;
					if (this.bakedSkyShadingDirection)
					{
						if (prv.skyOcclusion && prv.skyOcclusionShadingDirection)
						{
							NativeArray<byte> sourceSkyShadingDirectionIndices = cellSharedData.GetSubArray(sharedDataChunkOffset, this.sharedSkyShadingDirectionIndicesChunkSize * shChunkCount);
							cellData.skyShadingDirectionIndices = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceSkyShadingDirectionIndices, Allocator.Persistent) : sourceSkyShadingDirectionIndices);
						}
						sharedDataChunkOffset += this.sharedSkyShadingDirectionIndicesChunkSize * shChunkCount;
					}
				}
				if (hasSupportData)
				{
					NativeArray<Vector3> sourcePositions = cellSupportData.GetSubArray(supportDataChunkOffset, shChunkCount * this.supportPositionChunkSize).Reinterpret<Vector3>(1);
					supportDataChunkOffset += shChunkCount * this.supportPositionChunkSize;
					cellData.probePositions = (this.m_UseStreamingAsset ? new NativeArray<Vector3>(sourcePositions, Allocator.Persistent) : sourcePositions);
					NativeArray<float> sourceValidity = cellSupportData.GetSubArray(supportDataChunkOffset, shChunkCount * this.supportValidityChunkSize).Reinterpret<float>(1);
					supportDataChunkOffset += shChunkCount * this.supportValidityChunkSize;
					cellData.validity = (this.m_UseStreamingAsset ? new NativeArray<float>(sourceValidity, Allocator.Persistent) : sourceValidity);
					NativeArray<float> sourceTouchup = cellSupportData.GetSubArray(supportDataChunkOffset, shChunkCount * this.supportTouchupChunkSize).Reinterpret<float>(1);
					supportDataChunkOffset += shChunkCount * this.supportTouchupChunkSize;
					cellData.touchupVolumeInteraction = (this.m_UseStreamingAsset ? new NativeArray<float>(sourceTouchup, Allocator.Persistent) : sourceTouchup);
					if (this.supportLayerMaskChunkSize != 0)
					{
						NativeArray<byte> sourceLayer = cellSupportData.GetSubArray(supportDataChunkOffset, shChunkCount * this.supportLayerMaskChunkSize).Reinterpret<byte>(1);
						supportDataChunkOffset += shChunkCount * this.supportLayerMaskChunkSize;
						cellData.layer = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceLayer, Allocator.Persistent) : sourceLayer);
					}
					if (this.supportOffsetsChunkSize != 0)
					{
						NativeArray<Vector3> sourceOffsetVectors = cellSupportData.GetSubArray(supportDataChunkOffset, shChunkCount * this.supportOffsetsChunkSize).Reinterpret<Vector3>(1);
						supportDataChunkOffset += shChunkCount * this.supportOffsetsChunkSize;
						cellData.offsetVectors = (this.m_UseStreamingAsset ? new NativeArray<Vector3>(sourceOffsetVectors, Allocator.Persistent) : sourceOffsetVectors);
					}
				}
				this.cellDataMap.Add(cellIndex, cellData);
				totalBricksCount += bricksCount;
				totalSHChunkCount += shChunkCount;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001F638 File Offset: 0x0001D838
		internal bool ResolveSharedCellData(List<int> cellIndices)
		{
			if (this.cellSharedDataAsset == null || !this.cellSharedDataAsset.IsValid())
			{
				return false;
			}
			if (!this.HasValidSharedData())
			{
				Debug.LogError("One or more data file missing for baking set " + base.name + ". Cannot load shared data.");
				return false;
			}
			NativeArray<byte> cellSharedData = this.LoadStreambleAssetData<byte>(this.cellSharedDataAsset, cellIndices);
			NativeArray<ProbeBrickIndex.Brick> bricksData = this.LoadStreambleAssetData<ProbeBrickIndex.Brick>(this.cellBricksDataAsset, cellIndices);
			bool flag = this.HasSupportData();
			NativeArray<byte> cellSupportData = (flag ? this.LoadStreambleAssetData<byte>(this.cellSupportDataAsset, cellIndices) : default(NativeArray<byte>));
			this.ResolveSharedCellData(cellIndices, bricksData, cellSharedData, cellSupportData);
			this.ReleaseStreamableAssetData<byte>(cellSharedData);
			this.ReleaseStreamableAssetData<ProbeBrickIndex.Brick>(bricksData);
			if (flag)
			{
				this.ReleaseStreamableAssetData<byte>(cellSupportData);
			}
			return true;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		internal bool ResolvePerScenarioCellData(List<int> cellIndices)
		{
			bool shUseL2 = ProbeReferenceVolume.instance.shBands == ProbeVolumeSHBands.SphericalHarmonicsL2;
			foreach (KeyValuePair<string, ProbeVolumeBakingSet.PerScenarioDataInfo> scenario in this.scenarios)
			{
				string name = scenario.Key;
				ProbeVolumeBakingSet.PerScenarioDataInfo data = scenario.Value;
				this.PruneCellIndexListForScenario(cellIndices, data, this.m_PrunedScenarioIndexList);
				if (!data.HasValidData(ProbeReferenceVolume.instance.shBands))
				{
					Debug.LogError(string.Concat(new string[] { "One or more data file missing for baking set ", name, " scenario ", this.lightingScenario, ". Cannot load scenario data." }));
					return false;
				}
				NativeArray<byte> cellData = this.LoadStreambleAssetData<byte>(data.cellDataAsset, this.m_PrunedScenarioIndexList);
				NativeArray<byte> cellOptionalData = (shUseL2 ? this.LoadStreambleAssetData<byte>(data.cellOptionalDataAsset, this.m_PrunedScenarioIndexList) : default(NativeArray<byte>));
				NativeArray<byte> cellProbeOcclusionData = (this.bakedProbeOcclusion ? this.LoadStreambleAssetData<byte>(data.cellProbeOcclusionDataAsset, this.m_PrunedScenarioIndexList) : default(NativeArray<byte>));
				if (!this.ResolvePerScenarioCellData(cellData, cellOptionalData, cellProbeOcclusionData, name, this.m_PrunedScenarioIndexList))
				{
					Debug.LogError("Baked data for scenario '" + name + "' cannot be loaded.");
					return false;
				}
				this.ReleaseStreamableAssetData<byte>(cellData);
				if (shUseL2)
				{
					this.ReleaseStreamableAssetData<byte>(cellOptionalData);
				}
				if (this.bakedProbeOcclusion)
				{
					this.ReleaseStreamableAssetData<byte>(cellProbeOcclusionData);
				}
			}
			return true;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001F874 File Offset: 0x0001DA74
		internal bool ResolvePerScenarioCellData(NativeArray<byte> cellData, NativeArray<byte> cellOptionalData, NativeArray<byte> cellProbeOcclusionData, string scenario, List<int> cellIndices)
		{
			if (!cellData.IsCreated)
			{
				return false;
			}
			bool hasOptionalData = cellOptionalData.IsCreated;
			bool hasProbeOcclusionData = cellProbeOcclusionData.IsCreated && cellProbeOcclusionData.Length > 0;
			int chunkOffsetL0L = 0;
			int chunkOffsetL2 = 0;
			int chunkOffsetProbeOcclusion = 0;
			for (int i = 0; i < cellIndices.Count; i++)
			{
				int cellIndex = cellIndices[i];
				ProbeReferenceVolume.CellData cellData2 = this.cellDataMap[cellIndex];
				ProbeReferenceVolume.CellDesc cellDesc = this.cellDescs[cellIndex];
				ProbeReferenceVolume.CellData.PerScenarioData cellState = default(ProbeReferenceVolume.CellData.PerScenarioData);
				int shChunkCount = cellDesc.shChunkCount;
				NativeArray<ushort> sourceShL0L1RxDataSource = cellData.GetSubArray(chunkOffsetL0L, this.L0ChunkSize * shChunkCount).Reinterpret<ushort>(1);
				NativeArray<byte> sourceShL1GL1RyDataSource = cellData.GetSubArray(chunkOffsetL0L + this.L0ChunkSize * shChunkCount, this.L1ChunkSize * shChunkCount);
				NativeArray<byte> sourceShL1BL1RzDataSource = cellData.GetSubArray(chunkOffsetL0L + (this.L0ChunkSize + this.L1ChunkSize) * shChunkCount, this.L1ChunkSize * shChunkCount);
				cellState.shL0L1RxData = (this.m_UseStreamingAsset ? new NativeArray<ushort>(sourceShL0L1RxDataSource, Allocator.Persistent) : sourceShL0L1RxDataSource);
				cellState.shL1GL1RyData = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceShL1GL1RyDataSource, Allocator.Persistent) : sourceShL1GL1RyDataSource);
				cellState.shL1BL1RzData = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceShL1BL1RzDataSource, Allocator.Persistent) : sourceShL1BL1RzDataSource);
				if (hasOptionalData)
				{
					int L2DataSize = shChunkCount * this.L2TextureChunkSize;
					NativeArray<byte> sourceShL2Data_0 = cellOptionalData.GetSubArray(chunkOffsetL2, L2DataSize);
					NativeArray<byte> sourceShL2Data_ = cellOptionalData.GetSubArray(chunkOffsetL2 + L2DataSize, L2DataSize);
					NativeArray<byte> sourceShL2Data_2 = cellOptionalData.GetSubArray(chunkOffsetL2 + L2DataSize * 2, L2DataSize);
					NativeArray<byte> sourceShL2Data_3 = cellOptionalData.GetSubArray(chunkOffsetL2 + L2DataSize * 3, L2DataSize);
					cellState.shL2Data_0 = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceShL2Data_0, Allocator.Persistent) : sourceShL2Data_0);
					cellState.shL2Data_1 = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceShL2Data_, Allocator.Persistent) : sourceShL2Data_);
					cellState.shL2Data_2 = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceShL2Data_2, Allocator.Persistent) : sourceShL2Data_2);
					cellState.shL2Data_3 = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceShL2Data_3, Allocator.Persistent) : sourceShL2Data_3);
				}
				if (hasProbeOcclusionData)
				{
					NativeArray<byte> sourceProbeOcclusionDataSource = cellProbeOcclusionData.GetSubArray(chunkOffsetProbeOcclusion, this.ProbeOcclusionChunkSize * shChunkCount);
					cellState.probeOcclusion = (this.m_UseStreamingAsset ? new NativeArray<byte>(sourceProbeOcclusionDataSource, Allocator.Persistent) : sourceProbeOcclusionDataSource);
				}
				chunkOffsetL0L += (this.L0ChunkSize + 2 * this.L1ChunkSize) * shChunkCount;
				chunkOffsetL2 += this.L2TextureChunkSize * 4 * shChunkCount;
				chunkOffsetProbeOcclusion += this.ProbeOcclusionChunkSize * shChunkCount;
				cellData2.scenarios.Add(scenario, cellState);
			}
			return true;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001FADD File Offset: 0x0001DCDD
		internal void ReleaseCell(int cellIndex)
		{
			this.cellDataMap[cellIndex].Cleanup(true);
			this.cellDataMap.Remove(cellIndex);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001FB00 File Offset: 0x0001DD00
		internal ProbeReferenceVolume.CellDesc GetCellDesc(int cellIndex)
		{
			ProbeReferenceVolume.CellDesc cellDesc;
			if (this.cellDescs.TryGetValue(cellIndex, out cellDesc))
			{
				return cellDesc;
			}
			return null;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001FB20 File Offset: 0x0001DD20
		internal ProbeReferenceVolume.CellData GetCellData(int cellIndex)
		{
			ProbeReferenceVolume.CellData cellData;
			if (this.cellDataMap.TryGetValue(cellIndex, out cellData))
			{
				return cellData;
			}
			return null;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001FB40 File Offset: 0x0001DD40
		internal int GetChunkGPUMemory(ProbeVolumeSHBands shBands)
		{
			int size = this.L0ChunkSize + 2 * this.L1ChunkSize + this.sharedDataChunkSize;
			if (shBands == ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				size += 4 * this.L2TextureChunkSize;
			}
			if (this.bakedProbeOcclusion)
			{
				size += this.ProbeOcclusionChunkSize;
			}
			return size;
		}

		// Token: 0x04000557 RID: 1367
		[SerializeField]
		internal bool singleSceneMode = true;

		// Token: 0x04000558 RID: 1368
		[SerializeField]
		internal bool dialogNoProbeVolumeInSetShown;

		// Token: 0x04000559 RID: 1369
		[SerializeField]
		internal ProbeVolumeBakingProcessSettings settings;

		// Token: 0x0400055A RID: 1370
		[SerializeField]
		private List<string> m_SceneGUIDs = new List<string>();

		// Token: 0x0400055B RID: 1371
		[SerializeField]
		[Obsolete("This is now contained in the SceneBakeData structure")]
		[FormerlySerializedAs("scenesToNotBake")]
		internal List<string> obsoleteScenesToNotBake = new List<string>();

		// Token: 0x0400055C RID: 1372
		[SerializeField]
		[FormerlySerializedAs("lightingScenarios")]
		internal List<string> m_LightingScenarios = new List<string>();

		// Token: 0x0400055D RID: 1373
		[SerializeField]
		internal SerializedDictionary<int, ProbeReferenceVolume.CellDesc> cellDescs = new SerializedDictionary<int, ProbeReferenceVolume.CellDesc>();

		// Token: 0x0400055E RID: 1374
		internal Dictionary<int, ProbeReferenceVolume.CellData> cellDataMap = new Dictionary<int, ProbeReferenceVolume.CellData>();

		// Token: 0x0400055F RID: 1375
		private List<int> m_TotalIndexList = new List<int>();

		// Token: 0x04000560 RID: 1376
		[SerializeField]
		private List<ProbeVolumeBakingSet.SerializedPerSceneCellList> m_SerializedPerSceneCellList;

		// Token: 0x04000561 RID: 1377
		internal Dictionary<string, List<int>> perSceneCellLists = new Dictionary<string, List<int>>();

		// Token: 0x04000562 RID: 1378
		[SerializeField]
		internal ProbeVolumeStreamableAsset cellSharedDataAsset;

		// Token: 0x04000563 RID: 1379
		[SerializeField]
		internal SerializedDictionary<string, ProbeVolumeBakingSet.PerScenarioDataInfo> scenarios = new SerializedDictionary<string, ProbeVolumeBakingSet.PerScenarioDataInfo>();

		// Token: 0x04000564 RID: 1380
		[SerializeField]
		internal ProbeVolumeStreamableAsset cellBricksDataAsset;

		// Token: 0x04000565 RID: 1381
		[SerializeField]
		internal ProbeVolumeStreamableAsset cellSupportDataAsset;

		// Token: 0x04000566 RID: 1382
		[SerializeField]
		internal int chunkSizeInBricks;

		// Token: 0x04000567 RID: 1383
		[SerializeField]
		internal Vector3Int maxCellPosition;

		// Token: 0x04000568 RID: 1384
		[SerializeField]
		internal Vector3Int minCellPosition;

		// Token: 0x04000569 RID: 1385
		[SerializeField]
		internal Bounds globalBounds;

		// Token: 0x0400056A RID: 1386
		[SerializeField]
		internal int bakedSimplificationLevels = -1;

		// Token: 0x0400056B RID: 1387
		[SerializeField]
		internal float bakedMinDistanceBetweenProbes = -1f;

		// Token: 0x0400056C RID: 1388
		[SerializeField]
		internal bool bakedProbeOcclusion;

		// Token: 0x0400056D RID: 1389
		[SerializeField]
		internal int bakedSkyOcclusionValue = -1;

		// Token: 0x0400056E RID: 1390
		[SerializeField]
		internal int bakedSkyShadingDirectionValue = -1;

		// Token: 0x0400056F RID: 1391
		[SerializeField]
		internal Vector3 bakedProbeOffset = Vector3.zero;

		// Token: 0x04000570 RID: 1392
		[SerializeField]
		internal int bakedMaskCount = 1;

		// Token: 0x04000571 RID: 1393
		[SerializeField]
		internal uint4 bakedLayerMasks;

		// Token: 0x04000572 RID: 1394
		[SerializeField]
		internal int maxSHChunkCount = -1;

		// Token: 0x04000573 RID: 1395
		[SerializeField]
		internal int L0ChunkSize;

		// Token: 0x04000574 RID: 1396
		[SerializeField]
		internal int L1ChunkSize;

		// Token: 0x04000575 RID: 1397
		[SerializeField]
		internal int L2TextureChunkSize;

		// Token: 0x04000576 RID: 1398
		[SerializeField]
		internal int ProbeOcclusionChunkSize;

		// Token: 0x04000577 RID: 1399
		[SerializeField]
		internal int sharedValidityMaskChunkSize;

		// Token: 0x04000578 RID: 1400
		[SerializeField]
		internal int sharedSkyOcclusionL0L1ChunkSize;

		// Token: 0x04000579 RID: 1401
		[SerializeField]
		internal int sharedSkyShadingDirectionIndicesChunkSize;

		// Token: 0x0400057A RID: 1402
		[SerializeField]
		internal int sharedDataChunkSize;

		// Token: 0x0400057B RID: 1403
		[SerializeField]
		internal int supportPositionChunkSize;

		// Token: 0x0400057C RID: 1404
		[SerializeField]
		internal int supportValidityChunkSize;

		// Token: 0x0400057D RID: 1405
		[SerializeField]
		internal int supportTouchupChunkSize;

		// Token: 0x0400057E RID: 1406
		[SerializeField]
		internal int supportLayerMaskChunkSize;

		// Token: 0x0400057F RID: 1407
		[SerializeField]
		internal int supportOffsetsChunkSize;

		// Token: 0x04000580 RID: 1408
		[SerializeField]
		internal int supportDataChunkSize;

		// Token: 0x04000581 RID: 1409
		[SerializeField]
		internal string lightingScenario = ProbeReferenceVolume.defaultLightingScenario;

		// Token: 0x04000582 RID: 1410
		private string m_OtherScenario;

		// Token: 0x04000583 RID: 1411
		private float m_ScenarioBlendingFactor;

		// Token: 0x04000584 RID: 1412
		private ReadCommandArray m_ReadCommandArray;

		// Token: 0x04000585 RID: 1413
		private NativeArray<ReadCommand> m_ReadCommandBuffer;

		// Token: 0x04000586 RID: 1414
		private Stack<NativeArray<byte>> m_ReadOperationScratchBuffers = new Stack<NativeArray<byte>>();

		// Token: 0x04000587 RID: 1415
		private List<int> m_PrunedIndexList = new List<int>();

		// Token: 0x04000588 RID: 1416
		private List<int> m_PrunedScenarioIndexList = new List<int>();

		// Token: 0x04000589 RID: 1417
		internal const int k_MaxSkyOcclusionBakingSamples = 8192;

		// Token: 0x0400058A RID: 1418
		[SerializeField]
		private ProbeVolumeBakingSet.Version version = CoreUtils.GetLastEnumValue<ProbeVolumeBakingSet.Version>();

		// Token: 0x0400058B RID: 1419
		[SerializeField]
		internal bool freezePlacement;

		// Token: 0x0400058C RID: 1420
		[SerializeField]
		public Vector3 probeOffset = Vector3.zero;

		// Token: 0x0400058D RID: 1421
		[Range(2f, 5f)]
		public int simplificationLevels = 3;

		// Token: 0x0400058E RID: 1422
		[Min(0.1f)]
		public float minDistanceBetweenProbes = 1f;

		// Token: 0x0400058F RID: 1423
		public LayerMask renderersLayerMask = -1;

		// Token: 0x04000590 RID: 1424
		[Min(0f)]
		public float minRendererVolumeSize = 0.1f;

		// Token: 0x04000591 RID: 1425
		public bool skyOcclusion;

		// Token: 0x04000592 RID: 1426
		[Logarithmic(1, 8192)]
		public int skyOcclusionBakingSamples = 2048;

		// Token: 0x04000593 RID: 1427
		[Range(0f, 5f)]
		public int skyOcclusionBakingBounces = 2;

		// Token: 0x04000594 RID: 1428
		[Range(0f, 1f)]
		public float skyOcclusionAverageAlbedo = 0.6f;

		// Token: 0x04000595 RID: 1429
		public bool skyOcclusionBackFaceCulling;

		// Token: 0x04000596 RID: 1430
		public bool skyOcclusionShadingDirection;

		// Token: 0x04000597 RID: 1431
		[SerializeField]
		internal bool useRenderingLayers;

		// Token: 0x04000598 RID: 1432
		[SerializeField]
		internal ProbeVolumeBakingSet.ProbeLayerMask[] renderingLayerMasks;

		// Token: 0x04000599 RID: 1433
		private bool m_HasSupportData;

		// Token: 0x0400059A RID: 1434
		private bool m_SharedDataIsValid;

		// Token: 0x0400059B RID: 1435
		private bool m_UseStreamingAsset = true;

		// Token: 0x0200012B RID: 299
		internal enum Version
		{
			// Token: 0x0400059D RID: 1437
			Initial,
			// Token: 0x0400059E RID: 1438
			RemoveProbeVolumeSceneData
		}

		// Token: 0x0200012C RID: 300
		[Serializable]
		internal class PerScenarioDataInfo
		{
			// Token: 0x060009C7 RID: 2503 RVA: 0x0001FCC2 File Offset: 0x0001DEC2
			public void Initialize(ProbeVolumeSHBands shBands)
			{
				this.m_HasValidData = this.ComputeHasValidData(shBands);
			}

			// Token: 0x060009C8 RID: 2504 RVA: 0x0001FCD1 File Offset: 0x0001DED1
			public bool IsValid()
			{
				return this.cellDataAsset != null && this.cellDataAsset.IsValid();
			}

			// Token: 0x060009C9 RID: 2505 RVA: 0x0001FCE8 File Offset: 0x0001DEE8
			public bool HasValidData(ProbeVolumeSHBands shBands)
			{
				return this.m_HasValidData;
			}

			// Token: 0x060009CA RID: 2506 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
			public bool ComputeHasValidData(ProbeVolumeSHBands shBands)
			{
				return this.cellDataAsset.FileExists() && (shBands == ProbeVolumeSHBands.SphericalHarmonicsL1 || this.cellOptionalDataAsset.FileExists());
			}

			// Token: 0x0400059F RID: 1439
			public int sceneHash;

			// Token: 0x040005A0 RID: 1440
			public ProbeVolumeStreamableAsset cellDataAsset;

			// Token: 0x040005A1 RID: 1441
			public ProbeVolumeStreamableAsset cellOptionalDataAsset;

			// Token: 0x040005A2 RID: 1442
			public ProbeVolumeStreamableAsset cellProbeOcclusionDataAsset;

			// Token: 0x040005A3 RID: 1443
			private bool m_HasValidData;
		}

		// Token: 0x0200012D RID: 301
		[Serializable]
		internal struct CellCounts
		{
			// Token: 0x060009CC RID: 2508 RVA: 0x0001FD12 File Offset: 0x0001DF12
			public void Add(ProbeVolumeBakingSet.CellCounts o)
			{
				this.bricksCount += o.bricksCount;
				this.chunksCount += o.chunksCount;
			}

			// Token: 0x040005A4 RID: 1444
			public int bricksCount;

			// Token: 0x040005A5 RID: 1445
			public int chunksCount;
		}

		// Token: 0x0200012E RID: 302
		[Serializable]
		private struct SerializedPerSceneCellList
		{
			// Token: 0x040005A6 RID: 1446
			public string sceneGUID;

			// Token: 0x040005A7 RID: 1447
			public List<int> cellList;
		}

		// Token: 0x0200012F RID: 303
		[Serializable]
		internal struct ProbeLayerMask
		{
			// Token: 0x040005A8 RID: 1448
			public RenderingLayerMask mask;

			// Token: 0x040005A9 RID: 1449
			public string name;
		}
	}
}
