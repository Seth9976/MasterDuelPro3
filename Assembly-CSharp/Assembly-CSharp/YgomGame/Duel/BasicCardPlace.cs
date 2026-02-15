using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000C92 RID: 3218
	public class BasicCardPlace : CardPlace
	{
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06005C27 RID: 23591 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005C28 RID: 23592 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject anchor
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06005C29 RID: 23593 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isStatusVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnEnterImpl(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnLeaveImpl(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
		}

		// Token: 0x06005C2C RID: 23596 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnRegisterImpl(CardRoot cardRoot, int index)
		{
		}

		// Token: 0x06005C2D RID: 23597 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnUnregisterImpl(CardRoot cardRoot, int index)
		{
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x000F4E3E File Offset: 0x000F303E
		public BasicCardPlace(DuelFieldBase duelField, int team, int position, GameObject anchor)
			: base(null, 0, 0)
		{
		}

		// Token: 0x06005C2F RID: 23599 RVA: 0x000F4E3E File Offset: 0x000F303E
		public BasicCardPlace(BasicCardPlace src)
			: base(null, 0, 0)
		{
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x0000216D File Offset: 0x0000036D
		private void Init(DuelFieldBase duelField, int team, int position, GameObject anchor)
		{
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateTerminate()
		{
			return false;
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnLeave(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return false;
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x0000216A File Offset: 0x0000036A
		protected override CardLocator OnEnter(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return null;
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnRegister(CardRoot cardRoot, int index, bool withEffect)
		{
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnUnregister(CardRoot cardRoot, int index)
		{
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ReqDecideEffectImpl(int index, Action onFinished)
		{
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void EndSacrificeTargetEffectImpl(int index, Action onFinished)
		{
		}

		// Token: 0x06005C38 RID: 23608 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateStateImpl(Action onFinished)
		{
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x000F4E4C File Offset: 0x000F304C
		public override Vector3 GetScreenPos(int index, Vector2 ofsRate)
		{
			return default(Vector3);
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapTo(BasicCardPlace to, int toIndexOffset, CardRootTransition transition, Action onFinished)
		{
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateLocators()
		{
		}

		// Token: 0x06005C3C RID: 23612 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetupCardLocator(CardLocator cardLocator)
		{
		}

		// Token: 0x04009767 RID: 38759
		private const float maxStacking = 3f;

		// Token: 0x04009768 RID: 38760
		private bool showUnavailavleZoneEff;
	}
}
