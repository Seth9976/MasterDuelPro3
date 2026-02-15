using System;

namespace UnityEngine.TerrainUtils
{
	// Token: 0x02000009 RID: 9
	public readonly struct TerrainTileCoord
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002265 File Offset: 0x00000465
		public TerrainTileCoord(int tileX, int tileZ)
		{
			this.tileX = tileX;
			this.tileZ = tileZ;
		}

		// Token: 0x0400001A RID: 26
		public readonly int tileX;

		// Token: 0x0400001B RID: 27
		public readonly int tileZ;
	}
}
