using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E3 RID: 483
	public static class TileLayoutUtils
	{
		// Token: 0x06000DBB RID: 3515 RVA: 0x00032A48 File Offset: 0x00030C48
		public static bool TryLayoutByTiles(RectInt src, uint tileSize, out RectInt main, out RectInt topRow, out RectInt rightCol, out RectInt topRight)
		{
			if ((long)src.width < (long)((ulong)tileSize) || (long)src.height < (long)((ulong)tileSize))
			{
				main = new RectInt(0, 0, 0, 0);
				topRow = new RectInt(0, 0, 0, 0);
				rightCol = new RectInt(0, 0, 0, 0);
				topRight = new RectInt(0, 0, 0, 0);
				return false;
			}
			int num = src.height / (int)tileSize;
			int mainWidth = src.width / (int)tileSize * (int)tileSize;
			int mainHeight = num * (int)tileSize;
			main = new RectInt
			{
				x = src.x,
				y = src.y,
				width = mainWidth,
				height = mainHeight
			};
			topRow = new RectInt
			{
				x = src.x,
				y = src.y + mainHeight,
				width = mainWidth,
				height = src.height - mainHeight
			};
			rightCol = new RectInt
			{
				x = src.x + mainWidth,
				y = src.y,
				width = src.width - mainWidth,
				height = mainHeight
			};
			topRight = new RectInt
			{
				x = src.x + mainWidth,
				y = src.y + mainHeight,
				width = src.width - mainWidth,
				height = src.height - mainHeight
			};
			return true;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00032BDC File Offset: 0x00030DDC
		public static bool TryLayoutByRow(RectInt src, uint tileSize, out RectInt main, out RectInt other)
		{
			if ((long)src.height < (long)((ulong)tileSize))
			{
				main = new RectInt(0, 0, 0, 0);
				other = new RectInt(0, 0, 0, 0);
				return false;
			}
			int mainHeight = src.height / (int)tileSize * (int)tileSize;
			main = new RectInt
			{
				x = src.x,
				y = src.y,
				width = src.width,
				height = mainHeight
			};
			other = new RectInt
			{
				x = src.x,
				y = src.y + mainHeight,
				width = src.width,
				height = src.height - mainHeight
			};
			return true;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00032CB0 File Offset: 0x00030EB0
		public static bool TryLayoutByCol(RectInt src, uint tileSize, out RectInt main, out RectInt other)
		{
			if ((long)src.width < (long)((ulong)tileSize))
			{
				main = new RectInt(0, 0, 0, 0);
				other = new RectInt(0, 0, 0, 0);
				return false;
			}
			int mainWidth = src.width / (int)tileSize * (int)tileSize;
			main = new RectInt
			{
				x = src.x,
				y = src.y,
				width = mainWidth,
				height = src.height
			};
			other = new RectInt
			{
				x = src.x + mainWidth,
				y = src.y,
				width = src.width - mainWidth,
				height = src.height
			};
			return true;
		}
	}
}
