using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200008C RID: 140
	internal class DecalEntityManager : IDisposable
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000B079 File Offset: 0x00009279
		public Material errorMaterial
		{
			get
			{
				if (this.m_ErrorMaterial == null)
				{
					this.m_ErrorMaterial = CoreUtils.CreateEngineMaterial(Shader.Find("Hidden/InternalErrorShader"));
				}
				return this.m_ErrorMaterial;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public Mesh decalProjectorMesh
		{
			get
			{
				if (this.m_DecalProjectorMesh == null)
				{
					this.m_DecalProjectorMesh = CoreUtils.CreateCubeMesh(new Vector4(-0.5f, -0.5f, -0.5f, 1f), new Vector4(0.5f, 0.5f, 0.5f, 1f));
				}
				return this.m_DecalProjectorMesh;
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000B10C File Offset: 0x0000930C
		public DecalEntityManager()
		{
			this.m_AddDecalSampler = new ProfilingSampler("DecalEntityManager.CreateDecalEntity");
			this.m_ResizeChunks = new ProfilingSampler("DecalEntityManager.ResizeChunks");
			this.m_SortChunks = new ProfilingSampler("DecalEntityManager.SortChunks");
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B1A7 File Offset: 0x000093A7
		public bool IsValid(DecalEntity decalEntity)
		{
			return this.m_DecalEntityIndexer.IsValid(decalEntity);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public DecalEntity CreateDecalEntity(DecalProjector decalProjector)
		{
			Material material = decalProjector.material;
			if (material == null)
			{
				material = this.errorMaterial;
			}
			DecalEntity decalEntity;
			using (new ProfilingScope(this.m_AddDecalSampler))
			{
				int chunkIndex = this.CreateChunkIndex(material);
				int entityIndex = this.entityChunks[chunkIndex].count;
				DecalEntity entity = this.m_DecalEntityIndexer.CreateDecalEntity(entityIndex, chunkIndex);
				DecalEntityChunk entityChunk = this.entityChunks[chunkIndex];
				DecalCachedChunk cachedChunk = this.cachedChunks[chunkIndex];
				DecalCulledChunk culledChunk = this.culledChunks[chunkIndex];
				DecalDrawCallChunk drawCallChunk = this.drawCallChunks[chunkIndex];
				if (this.entityChunks[chunkIndex].capacity == this.entityChunks[chunkIndex].count)
				{
					using (new ProfilingScope(this.m_ResizeChunks))
					{
						int newCapacity = this.entityChunks[chunkIndex].capacity + this.entityChunks[chunkIndex].capacity;
						newCapacity = math.max(8, newCapacity);
						entityChunk.SetCapacity(newCapacity);
						cachedChunk.SetCapacity(newCapacity);
						culledChunk.SetCapacity(newCapacity);
						drawCallChunk.SetCapacity(newCapacity);
					}
				}
				entityChunk.Push();
				cachedChunk.Push();
				culledChunk.Push();
				drawCallChunk.Push();
				entityChunk.decalProjectors[entityIndex] = decalProjector;
				entityChunk.decalEntities[entityIndex] = entity;
				entityChunk.transformAccessArray.Add(decalProjector.transform);
				this.UpdateDecalEntityData(entity, decalProjector);
				decalEntity = entity;
			}
			return decalEntity;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B378 File Offset: 0x00009578
		private int CreateChunkIndex(Material material)
		{
			int chunkIndex;
			if (!this.m_MaterialToChunkIndex.TryGetValue(material, out chunkIndex))
			{
				MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
				propertyBlock.SetMatrixArray("_NormalToWorld", new Matrix4x4[DecalDrawSystem.MaxBatchSize]);
				propertyBlock.SetFloatArray("_DecalLayerMaskFromDecal", new float[DecalDrawSystem.MaxBatchSize]);
				this.entityChunks.Add(new DecalEntityChunk
				{
					material = material
				});
				this.cachedChunks.Add(new DecalCachedChunk
				{
					propertyBlock = propertyBlock
				});
				this.culledChunks.Add(new DecalCulledChunk());
				this.drawCallChunks.Add(new DecalDrawCallChunk
				{
					subCallCounts = new NativeArray<int>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory)
				});
				this.m_CombinedChunks.Add(default(DecalEntityManager.CombinedChunks));
				this.m_CombinedChunkRemmap.Add(0);
				this.m_MaterialToChunkIndex.Add(material, this.chunkCount);
				int num = this.chunkCount;
				this.chunkCount = num + 1;
				return num;
			}
			return chunkIndex;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B46C File Offset: 0x0000966C
		public void UpdateAllDecalEntitiesData()
		{
			foreach (DecalEntityChunk entityChunk in this.entityChunks)
			{
				for (int i = 0; i < entityChunk.count; i++)
				{
					DecalProjector decalProjector = entityChunk.decalProjectors[i];
					if (!(decalProjector == null))
					{
						DecalEntity entity = entityChunk.decalEntities[i];
						if (this.IsValid(entity))
						{
							this.UpdateDecalEntityData(entity, decalProjector);
						}
					}
				}
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B4FC File Offset: 0x000096FC
		public void UpdateDecalEntityData(DecalEntity decalEntity, DecalProjector decalProjector)
		{
			DecalEntityIndexer.DecalEntityItem item = this.m_DecalEntityIndexer.GetItem(decalEntity);
			int chunkIndex = item.chunkIndex;
			int arrayIndex = item.arrayIndex;
			DecalCachedChunk cachedChunk = this.cachedChunks[chunkIndex];
			cachedChunk.sizeOffsets[arrayIndex] = Matrix4x4.Translate(decalProjector.decalOffset) * Matrix4x4.Scale(decalProjector.decalSize);
			float drawDistance = decalProjector.drawDistance;
			float fadeScale = decalProjector.fadeScale;
			float startAngleFade = decalProjector.startAngleFade;
			float endAngleFade = decalProjector.endAngleFade;
			Vector4 uvScaleBias = decalProjector.uvScaleBias;
			int layerMask = decalProjector.gameObject.layer;
			ulong sceneLayerMask = decalProjector.gameObject.sceneCullingMask;
			float fadeFactor = decalProjector.fadeFactor;
			cachedChunk.drawDistances[arrayIndex] = new Vector2(drawDistance, fadeScale);
			if (startAngleFade == 180f)
			{
				cachedChunk.angleFades[arrayIndex] = new Vector2(0f, 0f);
			}
			else
			{
				float angleStart = startAngleFade / 180f;
				float angleEnd = endAngleFade / 180f;
				float range = Mathf.Max(0.0001f, angleEnd - angleStart);
				cachedChunk.angleFades[arrayIndex] = new Vector2(1f - (0.25f - angleStart) / range, -0.25f / range);
			}
			cachedChunk.uvScaleBias[arrayIndex] = uvScaleBias;
			cachedChunk.layerMasks[arrayIndex] = layerMask;
			cachedChunk.sceneLayerMasks[arrayIndex] = sceneLayerMask;
			cachedChunk.fadeFactors[arrayIndex] = fadeFactor;
			cachedChunk.scaleModes[arrayIndex] = decalProjector.scaleMode;
			cachedChunk.renderingLayerMasks[arrayIndex] = RenderingLayerUtils.ToValidRenderingLayers(decalProjector.renderingLayerMask);
			cachedChunk.positions[arrayIndex] = decalProjector.transform.position;
			cachedChunk.rotation[arrayIndex] = decalProjector.transform.rotation;
			cachedChunk.scales[arrayIndex] = decalProjector.transform.lossyScale;
			cachedChunk.dirty[arrayIndex] = true;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000B704 File Offset: 0x00009904
		public void DestroyDecalEntity(DecalEntity decalEntity)
		{
			if (!this.m_DecalEntityIndexer.IsValid(decalEntity))
			{
				return;
			}
			DecalEntityIndexer.DecalEntityItem item = this.m_DecalEntityIndexer.GetItem(decalEntity);
			this.m_DecalEntityIndexer.DestroyDecalEntity(decalEntity);
			int chunkIndex = item.chunkIndex;
			int arrayIndex = item.arrayIndex;
			DecalEntityChunk entityChunk = this.entityChunks[chunkIndex];
			DecalCachedChunk cachedChunk = this.cachedChunks[chunkIndex];
			DecalCulledChunk culledChunk = this.culledChunks[chunkIndex];
			DecalChunk decalChunk = this.drawCallChunks[chunkIndex];
			int lastArrayIndex = entityChunk.count - 1;
			if (arrayIndex != lastArrayIndex)
			{
				this.m_DecalEntityIndexer.UpdateIndex(entityChunk.decalEntities[lastArrayIndex], arrayIndex);
			}
			entityChunk.RemoveAtSwapBack(arrayIndex);
			cachedChunk.RemoveAtSwapBack(arrayIndex);
			culledChunk.RemoveAtSwapBack(arrayIndex);
			decalChunk.RemoveAtSwapBack(arrayIndex);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000B7C0 File Offset: 0x000099C0
		public void Update()
		{
			using (new ProfilingScope(this.m_SortChunks))
			{
				for (int i = 0; i < this.chunkCount; i++)
				{
					if (this.entityChunks[i].material == null)
					{
						this.entityChunks[i].material = this.errorMaterial;
					}
				}
				for (int j = 0; j < this.chunkCount; j++)
				{
					this.m_CombinedChunks[j] = new DecalEntityManager.CombinedChunks
					{
						entityChunk = this.entityChunks[j],
						cachedChunk = this.cachedChunks[j],
						culledChunk = this.culledChunks[j],
						drawCallChunk = this.drawCallChunks[j],
						previousChunkIndex = j,
						valid = (this.entityChunks[j].count != 0)
					};
				}
				this.m_CombinedChunks.Sort(delegate(DecalEntityManager.CombinedChunks a, DecalEntityManager.CombinedChunks b)
				{
					if (a.valid && !b.valid)
					{
						return -1;
					}
					if (!a.valid && b.valid)
					{
						return 1;
					}
					if (a.cachedChunk.drawOrder < b.cachedChunk.drawOrder)
					{
						return -1;
					}
					if (a.cachedChunk.drawOrder > b.cachedChunk.drawOrder)
					{
						return 1;
					}
					return a.entityChunk.material.GetHashCode().CompareTo(b.entityChunk.material.GetHashCode());
				});
				bool dirty = false;
				for (int k = 0; k < this.chunkCount; k++)
				{
					if (this.m_CombinedChunks[k].previousChunkIndex != k || !this.m_CombinedChunks[k].valid)
					{
						dirty = true;
						break;
					}
				}
				if (dirty)
				{
					int count = 0;
					this.m_MaterialToChunkIndex.Clear();
					for (int l = 0; l < this.chunkCount; l++)
					{
						DecalEntityManager.CombinedChunks combinedChunk = this.m_CombinedChunks[l];
						if (!this.m_CombinedChunks[l].valid)
						{
							combinedChunk.entityChunk.currentJobHandle.Complete();
							combinedChunk.cachedChunk.currentJobHandle.Complete();
							combinedChunk.culledChunk.currentJobHandle.Complete();
							combinedChunk.drawCallChunk.currentJobHandle.Complete();
							combinedChunk.entityChunk.Dispose();
							combinedChunk.cachedChunk.Dispose();
							combinedChunk.culledChunk.Dispose();
							combinedChunk.drawCallChunk.Dispose();
						}
						else
						{
							this.entityChunks[l] = combinedChunk.entityChunk;
							this.cachedChunks[l] = combinedChunk.cachedChunk;
							this.culledChunks[l] = combinedChunk.culledChunk;
							this.drawCallChunks[l] = combinedChunk.drawCallChunk;
							if (!this.m_MaterialToChunkIndex.ContainsKey(this.entityChunks[l].material))
							{
								this.m_MaterialToChunkIndex.Add(this.entityChunks[l].material, l);
							}
							this.m_CombinedChunkRemmap[combinedChunk.previousChunkIndex] = l;
							count++;
						}
					}
					if (this.chunkCount > count)
					{
						this.entityChunks.RemoveRange(count, this.chunkCount - count);
						this.cachedChunks.RemoveRange(count, this.chunkCount - count);
						this.culledChunks.RemoveRange(count, this.chunkCount - count);
						this.drawCallChunks.RemoveRange(count, this.chunkCount - count);
						this.m_CombinedChunks.RemoveRange(count, this.chunkCount - count);
						this.chunkCount = count;
					}
					this.m_DecalEntityIndexer.RemapChunkIndices(this.m_CombinedChunkRemmap);
				}
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000BB68 File Offset: 0x00009D68
		public void Dispose()
		{
			CoreUtils.Destroy(this.m_ErrorMaterial);
			CoreUtils.Destroy(this.m_DecalProjectorMesh);
			foreach (DecalEntityChunk decalEntityChunk in this.entityChunks)
			{
				decalEntityChunk.currentJobHandle.Complete();
			}
			foreach (DecalCachedChunk decalCachedChunk in this.cachedChunks)
			{
				decalCachedChunk.currentJobHandle.Complete();
			}
			foreach (DecalCulledChunk decalCulledChunk in this.culledChunks)
			{
				decalCulledChunk.currentJobHandle.Complete();
			}
			foreach (DecalDrawCallChunk decalDrawCallChunk in this.drawCallChunks)
			{
				decalDrawCallChunk.currentJobHandle.Complete();
			}
			foreach (DecalEntityChunk decalEntityChunk2 in this.entityChunks)
			{
				decalEntityChunk2.Dispose();
			}
			foreach (DecalCachedChunk decalCachedChunk2 in this.cachedChunks)
			{
				decalCachedChunk2.Dispose();
			}
			foreach (DecalCulledChunk decalCulledChunk2 in this.culledChunks)
			{
				decalCulledChunk2.Dispose();
			}
			foreach (DecalDrawCallChunk decalDrawCallChunk2 in this.drawCallChunks)
			{
				decalDrawCallChunk2.Dispose();
			}
			this.m_DecalEntityIndexer.Clear();
			this.m_MaterialToChunkIndex.Clear();
			this.entityChunks.Clear();
			this.cachedChunks.Clear();
			this.culledChunks.Clear();
			this.drawCallChunks.Clear();
			this.m_CombinedChunks.Clear();
			this.chunkCount = 0;
		}

		// Token: 0x0400028F RID: 655
		public List<DecalEntityChunk> entityChunks = new List<DecalEntityChunk>();

		// Token: 0x04000290 RID: 656
		public List<DecalCachedChunk> cachedChunks = new List<DecalCachedChunk>();

		// Token: 0x04000291 RID: 657
		public List<DecalCulledChunk> culledChunks = new List<DecalCulledChunk>();

		// Token: 0x04000292 RID: 658
		public List<DecalDrawCallChunk> drawCallChunks = new List<DecalDrawCallChunk>();

		// Token: 0x04000293 RID: 659
		public int chunkCount;

		// Token: 0x04000294 RID: 660
		private ProfilingSampler m_AddDecalSampler;

		// Token: 0x04000295 RID: 661
		private ProfilingSampler m_ResizeChunks;

		// Token: 0x04000296 RID: 662
		private ProfilingSampler m_SortChunks;

		// Token: 0x04000297 RID: 663
		private DecalEntityIndexer m_DecalEntityIndexer = new DecalEntityIndexer();

		// Token: 0x04000298 RID: 664
		private Dictionary<Material, int> m_MaterialToChunkIndex = new Dictionary<Material, int>();

		// Token: 0x04000299 RID: 665
		private List<DecalEntityManager.CombinedChunks> m_CombinedChunks = new List<DecalEntityManager.CombinedChunks>();

		// Token: 0x0400029A RID: 666
		private List<int> m_CombinedChunkRemmap = new List<int>();

		// Token: 0x0400029B RID: 667
		private Material m_ErrorMaterial;

		// Token: 0x0400029C RID: 668
		private Mesh m_DecalProjectorMesh;

		// Token: 0x0200008D RID: 141
		private struct CombinedChunks
		{
			// Token: 0x0400029D RID: 669
			public DecalEntityChunk entityChunk;

			// Token: 0x0400029E RID: 670
			public DecalCachedChunk cachedChunk;

			// Token: 0x0400029F RID: 671
			public DecalCulledChunk culledChunk;

			// Token: 0x040002A0 RID: 672
			public DecalDrawCallChunk drawCallChunk;

			// Token: 0x040002A1 RID: 673
			public int previousChunkIndex;

			// Token: 0x040002A2 RID: 674
			public bool valid;
		}
	}
}
