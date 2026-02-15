using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000E97 RID: 3735
	public class GraveCardPlace : CardPlace
	{
		// Token: 0x06006C94 RID: 27796 RVA: 0x000F4E3E File Offset: 0x000F303E
		public GraveCardPlace(DuelFieldBase duelField, int team, int position, GameObject anchor, SharedDefinition.Location location)
			: base(null, 0, 0)
		{
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnPrepareToDuel(bool startAtZero, Action onFinished)
		{
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x0000216A File Offset: 0x0000036A
		protected override CardLocator OnEnter(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return null;
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnLeave(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return false;
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnRegister(CardRoot cardRoot, int index, bool withEffect)
		{
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePopUpText()
		{
		}

		// Token: 0x06006C9A RID: 27802 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTopCardInfo()
		{
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnUnregister(CardRoot cardRoot, int index)
		{
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Terminate()
		{
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnUpdate()
		{
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x0000216D File Offset: 0x0000036D
		public void SyncToEngine(Dictionary<string, object> savedEngineParams, Action onFinished, int num = 0)
		{
		}

		// Token: 0x06006C9F RID: 27807 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ReqHighlightImpl(bool available, uint cmdBit, Action onFinished)
		{
		}

		// Token: 0x06006CA0 RID: 27808 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ReqDecideEffectImpl(int index, Action onFinished)
		{
		}

		// Token: 0x06006CA1 RID: 27809 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowStatusLabel(bool immediate, bool showDetail)
		{
		}

		// Token: 0x06006CA2 RID: 27810 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideStatusLabel(bool immediate, bool finishDetail = false)
		{
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetupCardLocator(CardLocator cardLocator)
		{
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnSelected()
		{
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDeselected()
		{
		}

		// Token: 0x0400A7F0 RID: 42992
		private GameObject anchor;

		// Token: 0x0400A7F1 RID: 42993
		private SharedDefinition.Location location;

		// Token: 0x0400A7F2 RID: 42994
		private PlaceStatusLabel statusLabel;

		// Token: 0x0400A7F3 RID: 42995
		private int localCardNum;

		// Token: 0x0400A7F4 RID: 42996
		private bool showDetailStatus;

		// Token: 0x0400A7F5 RID: 42997
		private GhostCard topCardGhost;

		// Token: 0x0400A7F6 RID: 42998
		private bool effectActivation;

		// Token: 0x0400A7F7 RID: 42999
		private int topCardID;

		// Token: 0x0400A7F8 RID: 43000
		private int topCardUniqueID;

		// Token: 0x0400A7F9 RID: 43001
		private bool topCardFace;
	}
}
