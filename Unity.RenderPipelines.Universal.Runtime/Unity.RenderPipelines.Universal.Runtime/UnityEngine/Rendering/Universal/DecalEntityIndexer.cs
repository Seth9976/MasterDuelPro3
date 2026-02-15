using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000088 RID: 136
	internal class DecalEntityIndexer
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000AD4F File Offset: 0x00008F4F
		public bool IsValid(DecalEntity decalEntity)
		{
			return this.m_Entities.Count > decalEntity.index && this.m_Entities[decalEntity.index].version == decalEntity.version;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000AD84 File Offset: 0x00008F84
		public DecalEntity CreateDecalEntity(int arrayIndex, int chunkIndex)
		{
			if (this.m_FreeIndices.Count != 0)
			{
				int entityIndex = this.m_FreeIndices.Dequeue();
				int newVersion = this.m_Entities[entityIndex].version + 1;
				this.m_Entities[entityIndex] = new DecalEntityIndexer.DecalEntityItem
				{
					arrayIndex = arrayIndex,
					chunkIndex = chunkIndex,
					version = newVersion
				};
				return new DecalEntity
				{
					index = entityIndex,
					version = newVersion
				};
			}
			int entityIndex2 = this.m_Entities.Count;
			int version = 1;
			this.m_Entities.Add(new DecalEntityIndexer.DecalEntityItem
			{
				arrayIndex = arrayIndex,
				chunkIndex = chunkIndex,
				version = version
			});
			return new DecalEntity
			{
				index = entityIndex2,
				version = version
			};
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000AE60 File Offset: 0x00009060
		public void DestroyDecalEntity(DecalEntity decalEntity)
		{
			this.m_FreeIndices.Enqueue(decalEntity.index);
			DecalEntityIndexer.DecalEntityItem item = this.m_Entities[decalEntity.index];
			item.version++;
			this.m_Entities[decalEntity.index] = item;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000AEAE File Offset: 0x000090AE
		public DecalEntityIndexer.DecalEntityItem GetItem(DecalEntity decalEntity)
		{
			return this.m_Entities[decalEntity.index];
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000AEC4 File Offset: 0x000090C4
		public void UpdateIndex(DecalEntity decalEntity, int newArrayIndex)
		{
			DecalEntityIndexer.DecalEntityItem item = this.m_Entities[decalEntity.index];
			item.arrayIndex = newArrayIndex;
			item.version = decalEntity.version;
			this.m_Entities[decalEntity.index] = item;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000AF0C File Offset: 0x0000910C
		public void RemapChunkIndices(List<int> remaper)
		{
			for (int i = 0; i < this.m_Entities.Count; i++)
			{
				int newChunkIndex = remaper[this.m_Entities[i].chunkIndex];
				DecalEntityIndexer.DecalEntityItem item = this.m_Entities[i];
				item.chunkIndex = newChunkIndex;
				this.m_Entities[i] = item;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000AF69 File Offset: 0x00009169
		public void Clear()
		{
			this.m_Entities.Clear();
			this.m_FreeIndices.Clear();
		}

		// Token: 0x04000284 RID: 644
		private List<DecalEntityIndexer.DecalEntityItem> m_Entities = new List<DecalEntityIndexer.DecalEntityItem>();

		// Token: 0x04000285 RID: 645
		private Queue<int> m_FreeIndices = new Queue<int>();

		// Token: 0x02000089 RID: 137
		public struct DecalEntityItem
		{
			// Token: 0x04000286 RID: 646
			public int chunkIndex;

			// Token: 0x04000287 RID: 647
			public int arrayIndex;

			// Token: 0x04000288 RID: 648
			public int version;
		}
	}
}
