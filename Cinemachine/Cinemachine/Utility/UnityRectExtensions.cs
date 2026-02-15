using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000F2 RID: 242
	public static class UnityRectExtensions
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x00023490 File Offset: 0x00021690
		public static Rect Inflated(this Rect r, Vector2 delta)
		{
			return new Rect(r.xMin - delta.x, r.yMin - delta.y, r.width + delta.x * 2f, r.height + delta.y * 2f);
		}
	}
}
