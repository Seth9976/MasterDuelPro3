using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000054 RID: 84
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct CreateDrawBatchesJob : IJob
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00008D4C File Offset: 0x00006F4C
		private ref DrawRange EditDrawRange(in RangeKey key)
		{
			int drawRangeIndex;
			if (!this.rangeHash.TryGetValue(key, out drawRangeIndex))
			{
				DrawRange drawRange = new DrawRange
				{
					key = key,
					drawCount = 0,
					drawOffset = 0
				};
				drawRangeIndex = this.drawRanges.Length;
				this.rangeHash.Add(key, drawRangeIndex);
				this.drawRanges.Add(in drawRange);
			}
			return this.drawRanges.ElementAt(drawRangeIndex);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00008DCC File Offset: 0x00006FCC
		private ref DrawBatch EditDrawBatch(in DrawKey key, in SubMeshDescriptor subMeshDescriptor)
		{
			MeshProceduralInfo procInfo = default(MeshProceduralInfo);
			procInfo.topology = subMeshDescriptor.topology;
			procInfo.baseVertex = (uint)subMeshDescriptor.baseVertex;
			procInfo.firstIndex = (uint)subMeshDescriptor.indexStart;
			procInfo.indexCount = (uint)subMeshDescriptor.indexCount;
			int drawBatchIndex;
			if (!this.batchHash.TryGetValue(key, out drawBatchIndex))
			{
				DrawBatch drawBatch = new DrawBatch
				{
					key = key,
					instanceCount = 0,
					instanceOffset = 0,
					procInfo = procInfo
				};
				drawBatchIndex = this.drawBatches.Length;
				this.batchHash.Add(key, drawBatchIndex);
				this.drawBatches.Add(in drawBatch);
			}
			return this.drawBatches.ElementAt(drawBatchIndex);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00008E90 File Offset: 0x00007090
		public void ProcessRenderer(int i)
		{
			int meshIndex = this.rendererData.meshIndex[i];
			int meshID = this.rendererData.meshID[meshIndex];
			short submeshCount = this.rendererData.subMeshCount[meshIndex];
			int subMeshDescOffset = this.rendererData.subMeshDescOffset[meshIndex];
			BatchMeshID batchMeshID = this.batchMeshHash[meshID];
			int rendererGroupID = this.rendererData.rendererGroupID[i];
			short startSubMesh = this.rendererData.subMeshStartIndex[i];
			int gameObjectLayer = this.rendererData.gameObjectLayer[i];
			uint renderingLayerMask = this.rendererData.renderingLayerMask[i];
			int materialsOffset = this.rendererData.materialsOffset[i];
			short materialsCount = this.rendererData.materialsCount[i];
			int lightmapIndex = this.rendererData.lightmapIndex[i];
			GPUDrivenPackedRendererData packedRendererData = this.rendererData.packedRendererData[i];
			int rendererPriority = this.rendererData.rendererPriority[i];
			int num = this.rendererData.lodGroupID[i];
			int instanceCount;
			int instanceOffset;
			if (this.implicitInstanceIndices)
			{
				instanceCount = 1;
				instanceOffset = i;
			}
			else
			{
				instanceCount = this.rendererData.instancesCount[i];
				instanceOffset = this.rendererData.instancesOffset[i];
			}
			if (instanceCount == 0)
			{
				return;
			}
			InstanceComponentGroup overridenComponents = InstanceComponentGroup.Default;
			if (packedRendererData.hasTree)
			{
				overridenComponents |= InstanceComponentGroup.Wind;
			}
			if ((lightmapIndex & 65535) >= 65534)
			{
				if (packedRendererData.lightProbeUsage == LightProbeUsage.BlendProbes)
				{
					overridenComponents |= InstanceComponentGroup.LightProbe;
				}
			}
			else
			{
				overridenComponents |= InstanceComponentGroup.Lightmap;
			}
			bool supportsIndirect = true;
			for (int matIndex = 0; matIndex < (int)materialsCount; matIndex++)
			{
				if (matIndex >= (int)submeshCount)
				{
					Debug.LogWarning("Material count in the shared material list is higher than sub mesh count for the mesh. Object may be corrupted.");
				}
				else
				{
					int materialIndex = this.rendererData.materialIndex[materialsOffset + matIndex];
					supportsIndirect &= this.rendererData.packedMaterialData[materialIndex].isIndirectSupported;
				}
			}
			RangeKey rangeKey = new RangeKey
			{
				layer = (byte)gameObjectLayer,
				renderingLayerMask = renderingLayerMask,
				motionMode = packedRendererData.motionVecGenMode,
				shadowCastingMode = packedRendererData.shadowCastingMode,
				staticShadowCaster = packedRendererData.staticShadowCaster,
				rendererPriority = rendererPriority,
				supportsIndirect = supportsIndirect
			};
			ref DrawRange drawRange = ref this.EditDrawRange(in rangeKey);
			for (int matIndex2 = 0; matIndex2 < (int)materialsCount; matIndex2++)
			{
				if (matIndex2 >= (int)submeshCount)
				{
					Debug.LogWarning("Material count in the shared material list is higher than sub mesh count for the mesh. Object may be corrupted.");
				}
				else
				{
					int materialIndex2 = this.rendererData.materialIndex[materialsOffset + matIndex2];
					int materialID = this.rendererData.materialID[materialIndex2];
					GPUDrivenPackedMaterialData packedMaterialData = this.rendererData.packedMaterialData[materialIndex2];
					if (materialID == 0)
					{
						Debug.LogWarning("Material in the shared materials list is null. Object will be partially rendered.");
					}
					else
					{
						BatchMaterialID batchMaterialID;
						this.batchMaterialHash.TryGetValue(materialID, out batchMaterialID);
						BatchDrawCommandFlags flags = BatchDrawCommandFlags.LODCrossFadeValuePacked;
						flags |= BatchDrawCommandFlags.UseLegacyLightmapsKeyword;
						if (packedMaterialData.isMotionVectorsPassEnabled)
						{
							flags |= BatchDrawCommandFlags.HasMotion;
						}
						if (packedMaterialData.isTransparent)
						{
							flags |= BatchDrawCommandFlags.HasSortingPosition;
						}
						int submeshIndex = (int)startSubMesh + matIndex2;
						SubMeshDescriptor subMeshDesc = this.rendererData.subMeshDesc[subMeshDescOffset + submeshIndex];
						DrawKey drawKey = new DrawKey
						{
							materialID = batchMaterialID,
							meshID = batchMeshID,
							submeshIndex = submeshIndex,
							flags = flags,
							transparentInstanceId = (packedMaterialData.isTransparent ? rendererGroupID : 0),
							range = rangeKey,
							overridenComponents = (uint)overridenComponents,
							lightmapIndex = lightmapIndex
						};
						ref DrawBatch ptr = ref this.EditDrawBatch(in drawKey, in subMeshDesc);
						if (ptr.instanceCount == 0)
						{
							drawRange.drawCount++;
						}
						ptr.instanceCount += instanceCount;
						for (int j = 0; j < instanceCount; j++)
						{
							int instanceIndex = instanceOffset + j;
							InstanceHandle instance = this.instances[instanceIndex];
							DrawInstance drawInstance = default(DrawInstance);
							drawInstance.key = drawKey;
							drawInstance.instanceIndex = instance.index;
							this.drawInstances.Add(in drawInstance);
						}
					}
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009294 File Offset: 0x00007494
		public void Execute()
		{
			for (int i = 0; i < this.rendererData.rendererGroupID.Length; i++)
			{
				this.ProcessRenderer(i);
			}
		}

		// Token: 0x04000175 RID: 373
		[ReadOnly]
		public bool implicitInstanceIndices;

		// Token: 0x04000176 RID: 374
		[ReadOnly]
		public NativeArray<InstanceHandle> instances;

		// Token: 0x04000177 RID: 375
		[ReadOnly]
		public GPUDrivenRendererGroupData rendererData;

		// Token: 0x04000178 RID: 376
		[ReadOnly]
		public NativeParallelHashMap<int, BatchMeshID> batchMeshHash;

		// Token: 0x04000179 RID: 377
		[ReadOnly]
		public NativeParallelHashMap<int, BatchMaterialID> batchMaterialHash;

		// Token: 0x0400017A RID: 378
		public NativeParallelHashMap<RangeKey, int> rangeHash;

		// Token: 0x0400017B RID: 379
		public NativeList<DrawRange> drawRanges;

		// Token: 0x0400017C RID: 380
		public NativeParallelHashMap<DrawKey, int> batchHash;

		// Token: 0x0400017D RID: 381
		public NativeList<DrawBatch> drawBatches;

		// Token: 0x0400017E RID: 382
		[WriteOnly]
		public NativeList<DrawInstance> drawInstances;
	}
}
