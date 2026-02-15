using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.TerrainUtils
{
	// Token: 0x0200000C RID: 12
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainUtility
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000028A0 File Offset: 0x00000AA0
		internal static bool ValidTerrainsExist()
		{
			return Terrain.activeTerrains != null && Terrain.activeTerrains.Length != 0;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028C8 File Offset: 0x00000AC8
		internal static void ClearConnectivity()
		{
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				bool allowAutoConnect = terrain.allowAutoConnect;
				if (allowAutoConnect)
				{
					terrain.SetNeighbors(null, null, null, null);
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002908 File Offset: 0x00000B08
		internal static Dictionary<int, TerrainMap> CollectTerrains(bool onlyAutoConnectedTerrains = true)
		{
			bool flag = !TerrainUtility.ValidTerrainsExist();
			Dictionary<int, TerrainMap> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				Dictionary<int, TerrainMap> groups = new Dictionary<int, TerrainMap>();
				Terrain[] activeTerrains = Terrain.activeTerrains;
				for (int i = 0; i < activeTerrains.Length; i++)
				{
					Terrain t = activeTerrains[i];
					bool flag2 = onlyAutoConnectedTerrains && !t.allowAutoConnect;
					if (!flag2)
					{
						bool flag3 = !groups.ContainsKey(t.groupingID);
						if (flag3)
						{
							TerrainMap map = TerrainMap.CreateFromPlacement(t, (Terrain x) => x.groupingID == t.groupingID && (!onlyAutoConnectedTerrains || x.allowAutoConnect), true);
							bool flag4 = map != null;
							if (flag4)
							{
								groups.Add(t.groupingID, map);
							}
						}
					}
				}
				dictionary = ((groups.Count != 0) ? groups : null);
			}
			return dictionary;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A0C File Offset: 0x00000C0C
		[RequiredByNativeCode]
		public static void AutoConnect()
		{
			bool flag = !TerrainUtility.ValidTerrainsExist();
			if (!flag)
			{
				TerrainUtility.ClearConnectivity();
				Dictionary<int, TerrainMap> terrainGroups = TerrainUtility.CollectTerrains(true);
				bool flag2 = terrainGroups == null;
				if (!flag2)
				{
					foreach (KeyValuePair<int, TerrainMap> group in terrainGroups)
					{
						TerrainMap terrains = group.Value;
						foreach (KeyValuePair<TerrainTileCoord, Terrain> tile in terrains.terrainTiles)
						{
							TerrainTileCoord coords = tile.Key;
							Terrain center = terrains.GetTerrain(coords.tileX, coords.tileZ);
							Terrain left = terrains.GetTerrain(coords.tileX - 1, coords.tileZ);
							Terrain right = terrains.GetTerrain(coords.tileX + 1, coords.tileZ);
							Terrain top = terrains.GetTerrain(coords.tileX, coords.tileZ + 1);
							Terrain bottom = terrains.GetTerrain(coords.tileX, coords.tileZ - 1);
							center.SetNeighbors(left, top, right, bottom);
						}
					}
				}
			}
		}
	}
}
