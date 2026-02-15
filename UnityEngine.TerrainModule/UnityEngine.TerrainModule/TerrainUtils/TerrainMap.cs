using System;
using System.Collections.Generic;

namespace UnityEngine.TerrainUtils
{
	// Token: 0x0200000A RID: 10
	public class TerrainMap
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002278 File Offset: 0x00000478
		public Terrain GetTerrain(int tileX, int tileZ)
		{
			Terrain result = null;
			this.m_terrainTiles.TryGetValue(new TerrainTileCoord(tileX, tileZ), out result);
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022A4 File Offset: 0x000004A4
		public static TerrainMap CreateFromPlacement(Terrain originTerrain, Predicate<Terrain> filter = null, bool fullValidation = true)
		{
			bool flag = Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0 || originTerrain == null;
			TerrainMap terrainMap;
			if (flag)
			{
				terrainMap = null;
			}
			else
			{
				bool flag2 = originTerrain.terrainData == null;
				if (flag2)
				{
					terrainMap = null;
				}
				else
				{
					int groupID = originTerrain.groupingID;
					float gridOriginX = originTerrain.transform.position.x;
					float gridOriginZ = originTerrain.transform.position.z;
					float gridSizeX = originTerrain.terrainData.size.x;
					float gridSizeZ = originTerrain.terrainData.size.z;
					bool flag3 = filter == null;
					if (flag3)
					{
						filter = (Terrain x) => x.groupingID == groupID;
					}
					terrainMap = TerrainMap.CreateFromPlacement(new Vector2(gridOriginX, gridOriginZ), new Vector2(gridSizeX, gridSizeZ), filter, fullValidation);
				}
			}
			return terrainMap;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002380 File Offset: 0x00000580
		public static TerrainMap CreateFromPlacement(Vector2 gridOrigin, Vector2 gridSize, Predicate<Terrain> filter = null, bool fullValidation = true)
		{
			bool flag = Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0;
			TerrainMap terrainMap2;
			if (flag)
			{
				terrainMap2 = null;
			}
			else
			{
				TerrainMap terrainMap = new TerrainMap();
				float gridScaleX = 1f / gridSize.x;
				float gridScaleZ = 1f / gridSize.y;
				foreach (Terrain terrain in Terrain.activeTerrains)
				{
					bool flag2 = terrain.terrainData == null;
					if (!flag2)
					{
						bool flag3 = filter == null || filter(terrain);
						if (flag3)
						{
							Vector3 pos = terrain.transform.position;
							int tileX = Mathf.RoundToInt((pos.x - gridOrigin.x) * gridScaleX);
							int tileZ = Mathf.RoundToInt((pos.z - gridOrigin.y) * gridScaleZ);
							terrainMap.TryToAddTerrain(tileX, tileZ, terrain);
						}
					}
				}
				if (fullValidation)
				{
					terrainMap.Validate();
				}
				terrainMap2 = ((terrainMap.m_terrainTiles.Count > 0) ? terrainMap : null);
			}
			return terrainMap2;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002491 File Offset: 0x00000691
		public Dictionary<TerrainTileCoord, Terrain> terrainTiles
		{
			get
			{
				return this.m_terrainTiles;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002499 File Offset: 0x00000699
		public TerrainMap()
		{
			this.m_errorCode = TerrainMapStatusCode.OK;
			this.m_terrainTiles = new Dictionary<TerrainTileCoord, Terrain>();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024B8 File Offset: 0x000006B8
		private void AddTerrainInternal(int x, int z, Terrain terrain)
		{
			bool flag = this.m_terrainTiles.Count == 0;
			if (flag)
			{
				this.m_patchSize = terrain.terrainData.size;
			}
			else
			{
				bool flag2 = terrain.terrainData.size != this.m_patchSize;
				if (flag2)
				{
					this.m_errorCode |= TerrainMapStatusCode.SizeMismatch;
				}
			}
			this.m_terrainTiles.Add(new TerrainTileCoord(x, z), terrain);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000252C File Offset: 0x0000072C
		private bool TryToAddTerrain(int tileX, int tileZ, Terrain terrain)
		{
			bool added = false;
			bool flag = terrain != null;
			if (flag)
			{
				Terrain existing = this.GetTerrain(tileX, tileZ);
				bool flag2 = existing != null;
				if (flag2)
				{
					bool flag3 = existing != terrain;
					if (flag3)
					{
						this.m_errorCode |= TerrainMapStatusCode.Overlapping;
					}
				}
				else
				{
					this.AddTerrainInternal(tileX, tileZ, terrain);
					added = true;
				}
			}
			return added;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002594 File Offset: 0x00000794
		private void ValidateTerrain(int tileX, int tileZ)
		{
			Terrain terrain = this.GetTerrain(tileX, tileZ);
			bool flag = terrain != null;
			if (flag)
			{
				Terrain left = this.GetTerrain(tileX - 1, tileZ);
				Terrain right = this.GetTerrain(tileX + 1, tileZ);
				Terrain top = this.GetTerrain(tileX, tileZ + 1);
				Terrain bottom = this.GetTerrain(tileX, tileZ - 1);
				bool flag2 = left;
				if (flag2)
				{
					bool flag3 = !Mathf.Approximately(terrain.transform.position.x, left.transform.position.x + left.terrainData.size.x) || !Mathf.Approximately(terrain.transform.position.z, left.transform.position.z);
					if (flag3)
					{
						this.m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
					}
				}
				bool flag4 = right;
				if (flag4)
				{
					bool flag5 = !Mathf.Approximately(terrain.transform.position.x + terrain.terrainData.size.x, right.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, right.transform.position.z);
					if (flag5)
					{
						this.m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
					}
				}
				bool flag6 = top;
				if (flag6)
				{
					bool flag7 = !Mathf.Approximately(terrain.transform.position.x, top.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z + terrain.terrainData.size.z, top.transform.position.z);
					if (flag7)
					{
						this.m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
					}
				}
				bool flag8 = bottom;
				if (flag8)
				{
					bool flag9 = !Mathf.Approximately(terrain.transform.position.x, bottom.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, bottom.transform.position.z + bottom.terrainData.size.z);
					if (flag9)
					{
						this.m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
					}
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002810 File Offset: 0x00000A10
		private TerrainMapStatusCode Validate()
		{
			foreach (TerrainTileCoord coord in this.m_terrainTiles.Keys)
			{
				this.ValidateTerrain(coord.tileX, coord.tileZ);
			}
			return this.m_errorCode;
		}

		// Token: 0x0400001C RID: 28
		private Vector3 m_patchSize;

		// Token: 0x0400001D RID: 29
		private TerrainMapStatusCode m_errorCode;

		// Token: 0x0400001E RID: 30
		private Dictionary<TerrainTileCoord, Terrain> m_terrainTiles;
	}
}
