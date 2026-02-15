using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013FB RID: 5115
	public static class GridLayoutGroupExtension
	{
		// Token: 0x0600943D RID: 37949 RVA: 0x00152048 File Offset: 0x00150248
		public static Vector2Int Size(this GridLayoutGroup grid)
		{
			int itemsCount = grid.transform.childCount;
			Vector2Int size = Vector2Int.zero;
			if (itemsCount == 0)
			{
				return size;
			}
			switch (grid.constraint)
			{
			case GridLayoutGroup.Constraint.Flexible:
				size = grid.flexibleSize();
				break;
			case GridLayoutGroup.Constraint.FixedColumnCount:
				size.x = grid.constraintCount;
				size.y = GridLayoutGroupExtension.GetAnotherAxisCount(itemsCount, size.x);
				break;
			case GridLayoutGroup.Constraint.FixedRowCount:
				size.y = grid.constraintCount;
				size.x = GridLayoutGroupExtension.GetAnotherAxisCount(itemsCount, size.y);
				break;
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unexpected constraint: {0}", grid.constraint));
			}
			return size;
		}

		// Token: 0x0600943E RID: 37950 RVA: 0x001520F4 File Offset: 0x001502F4
		private static Vector2Int flexibleSize(this GridLayoutGroup grid)
		{
			int itemsCount = grid.transform.childCount;
			float prevX = float.NegativeInfinity;
			int xCount = 0;
			for (int i = 0; i < itemsCount; i++)
			{
				Vector2 pos = ((RectTransform)grid.transform.GetChild(i)).anchoredPosition;
				if (pos.x <= prevX)
				{
					break;
				}
				prevX = pos.x;
				xCount++;
			}
			int yCount = GridLayoutGroupExtension.GetAnotherAxisCount(itemsCount, xCount);
			return new Vector2Int(xCount, yCount);
		}

		// Token: 0x0600943F RID: 37951 RVA: 0x00152163 File Offset: 0x00150363
		private static int GetAnotherAxisCount(int totalCount, int axisCount)
		{
			return totalCount / axisCount + Mathf.Min(1, totalCount % axisCount);
		}
	}
}
