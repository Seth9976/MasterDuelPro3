using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000C84 RID: 3204
	public class AttackTargetingOperation
	{
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06005BE6 RID: 23526 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005BE7 RID: 23527 RVA: 0x0000216D File Offset: 0x0000036D
		public bool activate
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06005BE8 RID: 23528 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005BE9 RID: 23529 RVA: 0x0000216D File Offset: 0x0000036D
		public bool dragOperation
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x0000216A File Offset: 0x0000036A
		public static AttackTargetingOperation Create(DuelGameObjectManager goManager)
		{
			return null;
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(DuelGameObjectManager goManager)
		{
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x0000216D File Offset: 0x0000036D
		public void Tarminate()
		{
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x0000216D File Offset: 0x0000036D
		public void BeginDragTargeting(int attackPlayer, int attackPosition, int targetMask, Vector2 screenPoint)
		{
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x0000216D File Offset: 0x0000036D
		public void BeginSingleTargeting(int attackPlayer, int attackPosition, int targetPlayer, int targetPosition)
		{
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x0000216D File Offset: 0x0000036D
		private void BeginTargeting(int attackPlayer, int attackPosition, Vector2 screenPoint)
		{
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateTargeting(Vector2 screenPoint)
		{
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x000F4DF0 File Offset: 0x000F2FF0
		private Vector3 GetTargetWorldPosition(int player, int position)
		{
			return default(Vector3);
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x000F4E08 File Offset: 0x000F3008
		public ValueTuple<int, int> EndTargeting(bool forceHideLine)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectAttackableZone(int player, int position)
		{
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispLine(bool disp)
		{
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x000F4E20 File Offset: 0x000F3020
		public ValueTuple<int, int, int, int> GetCurrentTargetingInfo()
		{
			return default(ValueTuple<int, int, int, int>);
		}

		// Token: 0x04009714 RID: 38676
		private DuelGameObjectManager goManager;

		// Token: 0x04009715 RID: 38677
		private List<int> targets;

		// Token: 0x04009716 RID: 38678
		private TargetingLine targetingLine;

		// Token: 0x04009717 RID: 38679
		private AttackZoneIconController zoneIcon;

		// Token: 0x04009718 RID: 38680
		private Vector3 basePosition;

		// Token: 0x04009719 RID: 38681
		private Vector2 startScreenPoint;

		// Token: 0x0400971A RID: 38682
		private int attackPlayer;

		// Token: 0x0400971B RID: 38683
		private int attackPosition;

		// Token: 0x0400971C RID: 38684
		private int targetPlayer;

		// Token: 0x0400971D RID: 38685
		private int targetPosition;
	}
}
