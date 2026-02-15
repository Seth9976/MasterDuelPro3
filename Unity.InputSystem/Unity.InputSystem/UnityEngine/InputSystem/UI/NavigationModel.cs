using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200011C RID: 284
	internal struct NavigationModel
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x00044E54 File Offset: 0x00043054
		public void Reset()
		{
			this.move = Vector2.zero;
		}

		// Token: 0x04000683 RID: 1667
		public Vector2 move;

		// Token: 0x04000684 RID: 1668
		public int consecutiveMoveCount;

		// Token: 0x04000685 RID: 1669
		public MoveDirection lastMoveDirection;

		// Token: 0x04000686 RID: 1670
		public float lastMoveTime;

		// Token: 0x04000687 RID: 1671
		public AxisEventData eventData;
	}
}
