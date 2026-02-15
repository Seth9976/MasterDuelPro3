using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000ED5 RID: 3797
	public class MonsterCardPlace : BasicCardPlace
	{
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06006E9C RID: 28316 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isActiveAimingEffect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006E9D RID: 28317 RVA: 0x000F6102 File Offset: 0x000F4302
		public MonsterCardPlace(DuelFieldBase duelField, int team, int position, GameObject anchor)
			: base(null, 0, 0, null)
		{
		}

		// Token: 0x06006E9E RID: 28318 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Terminate()
		{
		}

		// Token: 0x06006E9F RID: 28319 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnEnterImpl(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
		}

		// Token: 0x06006EA0 RID: 28320 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnLeaveImpl(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
		}

		// Token: 0x06006EA1 RID: 28321 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnRegisterImpl(CardRoot cardRoot, int index)
		{
		}

		// Token: 0x06006EA2 RID: 28322 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndBattleAimingEffect()
		{
		}

		// Token: 0x06006EA3 RID: 28323 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetVisibleAimingEffect(bool visible)
		{
		}

		// Token: 0x06006EA4 RID: 28324 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateXyzIndices()
		{
		}

		// Token: 0x0400A979 RID: 43385
		private BattleAimingEffect btlAimingEff;
	}
}
