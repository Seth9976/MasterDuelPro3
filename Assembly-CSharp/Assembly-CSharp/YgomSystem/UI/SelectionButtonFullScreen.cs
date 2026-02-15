using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005CA RID: 1482
	public class SelectionButtonFullScreen : SelectionButton
	{
		// Token: 0x06002ED1 RID: 11985 RVA: 0x000F20AC File Offset: 0x000F02AC
		public override Vector2 GetClosestPoint(Vector2 base_position, Vector2 direction, bool contains_check = true)
		{
			return default(Vector2);
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsContainsPoint(Vector2 view_position)
		{
			return false;
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsRectContains(Vector2 rect_point0, Vector2 rect_point1, Vector2 rect_point2, Vector2 rect_point3, bool containedComplete)
		{
			return false;
		}
	}
}
