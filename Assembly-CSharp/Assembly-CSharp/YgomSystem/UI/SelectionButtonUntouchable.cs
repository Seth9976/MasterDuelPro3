using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005CB RID: 1483
	public class SelectionButtonUntouchable : SelectionButton
	{
		// Token: 0x06002ED5 RID: 11989 RVA: 0x000F20CC File Offset: 0x000F02CC
		public override Vector2 GetClosestPoint(Vector2 base_position, Vector2 direction, bool contains_check = true)
		{
			return default(Vector2);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsContainsPoint(Vector2 view_position)
		{
			return false;
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsRectContains(Vector2 rect_point0, Vector2 rect_point1, Vector2 rect_point2, Vector2 rect_point3, bool containedComplete)
		{
			return false;
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectionButtonUntouchable Create(GameObject target)
		{
			return null;
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetupViewPoints()
		{
		}
	}
}
