using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200000D RID: 13
	public static class Clipping
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002A84 File Offset: 0x00000C84
		public static Rect FindCullAndClipWorldRect(List<RectMask2D> rectMaskParents, out bool validRect)
		{
			if (rectMaskParents.Count == 0)
			{
				validRect = false;
				return default(Rect);
			}
			Rect current = rectMaskParents[0].canvasRect;
			Vector4 offset = rectMaskParents[0].padding;
			float xMin = current.xMin + offset.x;
			float xMax = current.xMax - offset.z;
			float yMin = current.yMin + offset.y;
			float yMax = current.yMax - offset.w;
			int rectMaskParentsCount = rectMaskParents.Count;
			for (int i = 1; i < rectMaskParentsCount; i++)
			{
				current = rectMaskParents[i].canvasRect;
				offset = rectMaskParents[i].padding;
				if (xMin < current.xMin + offset.x)
				{
					xMin = current.xMin + offset.x;
				}
				if (yMin < current.yMin + offset.y)
				{
					yMin = current.yMin + offset.y;
				}
				if (xMax > current.xMax - offset.z)
				{
					xMax = current.xMax - offset.z;
				}
				if (yMax > current.yMax - offset.w)
				{
					yMax = current.yMax - offset.w;
				}
			}
			validRect = xMax > xMin && yMax > yMin;
			if (!validRect)
			{
				return default(Rect);
			}
			return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
		}
	}
}
