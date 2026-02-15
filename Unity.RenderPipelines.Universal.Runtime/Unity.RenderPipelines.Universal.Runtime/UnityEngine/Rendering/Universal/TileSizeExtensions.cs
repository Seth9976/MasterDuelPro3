using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001A5 RID: 421
	internal static class TileSizeExtensions
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0002AA8F File Offset: 0x00028C8F
		public static bool IsValid(this TileSize tileSize)
		{
			return tileSize == TileSize._8 || tileSize == TileSize._16 || tileSize == TileSize._32 || tileSize == TileSize._64;
		}
	}
}
