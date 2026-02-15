using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005B5 RID: 1461
	public class RectSelectionItem : SelectionItem
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x000F203C File Offset: 0x000F023C
		public override Vector2 viewCenterPosition
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000F2054 File Offset: 0x000F0254
		public override Vector2 GetClosestPoint(Vector2 base_position, Vector2 direction, bool contains_check = true)
		{
			return default(Vector2);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void SetupViewPoints()
		{
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000F206C File Offset: 0x000F026C
		private Vector3 ElementScale(Vector3 a, Vector3 b)
		{
			return default(Vector3);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsContainsPoint(Vector2 view_position)
		{
			return false;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsRectContains(Vector2 rect_point0, Vector2 rect_point1, Vector2 rect_point2, Vector2 rect_point3, bool containedComplete)
		{
			return false;
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupWorldRect(bool use, Vector2 half_size, Vector3 center, Vector3 angle)
		{
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x0000216A File Offset: 0x0000036A
		public Vector2[] CloneViewPoints()
		{
			return null;
		}

		// Token: 0x04002BC4 RID: 11204
		private Vector3[] rectWorldCorners;

		// Token: 0x04002BC5 RID: 11205
		private Vector3[] boxWorldCorners;

		// Token: 0x04002BC6 RID: 11206
		private Vector2[] viewPoints;

		// Token: 0x04002BC7 RID: 11207
		public bool useWorldRectSetting;

		// Token: 0x04002BC8 RID: 11208
		public Vector2 worldRectHalfSize;

		// Token: 0x04002BC9 RID: 11209
		public Vector3 worldRectCenter;

		// Token: 0x04002BCA RID: 11210
		public Vector3 worldRectAngle;
	}
}
