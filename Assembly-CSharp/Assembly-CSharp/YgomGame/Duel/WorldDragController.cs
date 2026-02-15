using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F43 RID: 3907
	public class WorldDragController
	{
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06007349 RID: 29513 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool dragging
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600734A RID: 29514 RVA: 0x0000216A File Offset: 0x0000036A
		public static WorldDragController Create(ITranslateScreenToWorld scrToWorld)
		{
			return null;
		}

		// Token: 0x0600734B RID: 29515 RVA: 0x000F64DE File Offset: 0x000F46DE
		public WorldDragController.State Update(out Vector3 move, out Vector3 average)
		{
			move = default(Vector3);
			average = default(Vector3);
			return WorldDragController.State.Idle;
		}

		// Token: 0x0600734C RID: 29516 RVA: 0x0000216D File Offset: 0x0000036D
		private void EnqueueDragHistory(Vector3 deltaMove)
		{
		}

		// Token: 0x0600734D RID: 29517 RVA: 0x000F64F0 File Offset: 0x000F46F0
		private Vector3 GetDirection(Vector2 s0, Vector2 s1)
		{
			return default(Vector3);
		}

		// Token: 0x0400AC7D RID: 44157
		private ITranslateScreenToWorld scrToWorld;

		// Token: 0x0400AC7E RID: 44158
		private Queue<Vector3> dragHistory;

		// Token: 0x0400AC7F RID: 44159
		private float currentTime;

		// Token: 0x0400AC80 RID: 44160
		private Vector3 move;

		// Token: 0x0400AC81 RID: 44161
		private bool draggingOnThisFrame;

		// Token: 0x0400AC82 RID: 44162
		private WorldDragController.State state;

		// Token: 0x0400AC83 RID: 44163
		private static readonly int numDragSpeedHistories;

		// Token: 0x02000F44 RID: 3908
		public enum State
		{
			// Token: 0x0400AC85 RID: 44165
			Idle,
			// Token: 0x0400AC86 RID: 44166
			BeginDrag,
			// Token: 0x0400AC87 RID: 44167
			Dragging,
			// Token: 0x0400AC88 RID: 44168
			EndDrag
		}
	}
}
