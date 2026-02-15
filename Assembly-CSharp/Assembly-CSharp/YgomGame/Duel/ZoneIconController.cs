using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F4C RID: 3916
	public abstract class ZoneIconController
	{
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x0600736F RID: 29551 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool useCardEffect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007370 RID: 29552 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06007371 RID: 29553 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06007372 RID: 29554 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Activate(int player, int position, bool ignoreCard = false)
		{
			return false;
		}

		// Token: 0x06007373 RID: 29555 RVA: 0x0000216A File Offset: 0x0000036A
		protected ZoneIconController.IconInfo CreateEffect(int player, int position, bool ignoreCard)
		{
			return null;
		}

		// Token: 0x06007374 RID: 29556 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show(int player, int position)
		{
		}

		// Token: 0x06007375 RID: 29557 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowAvailableZone()
		{
		}

		// Token: 0x06007376 RID: 29558 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide(int player, int position)
		{
		}

		// Token: 0x06007377 RID: 29559 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeactivateAll()
		{
		}

		// Token: 0x06007378 RID: 29560 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsActivated(int player, int position, bool transEx = false)
		{
			return false;
		}

		// Token: 0x06007379 RID: 29561 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStatus(int player, int position, bool selected)
		{
		}

		// Token: 0x0600737A RID: 29562 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetStatus(ZoneIconController.IconInfo info, bool selected)
		{
		}

		// Token: 0x0600737B RID: 29563 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStatusAll(bool selected)
		{
		}

		// Token: 0x0600737C RID: 29564 RVA: 0x000F6510 File Offset: 0x000F4710
		public ValueTuple<int, int> GetHighPriorityAvailableZone()
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x0400AC97 RID: 44183
		protected Dictionary<int, Dictionary<int, ZoneIconController.IconInfo>> availableList;

		// Token: 0x0400AC98 RID: 44184
		protected RunEffectWorker worker;

		// Token: 0x02000F4D RID: 3917
		protected class IconInfo
		{
			// Token: 0x0400AC99 RID: 44185
			public bool available;

			// Token: 0x0400AC9A RID: 44186
			public DuelEffectPool.Type effectTypeIcon;

			// Token: 0x0400AC9B RID: 44187
			public SimpleEffect effect;

			// Token: 0x0400AC9C RID: 44188
			public Material rollover;

			// Token: 0x0400AC9D RID: 44189
			public Material rolloverCard;
		}
	}
}
