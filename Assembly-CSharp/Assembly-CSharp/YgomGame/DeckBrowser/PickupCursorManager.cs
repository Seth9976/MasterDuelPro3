using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F94 RID: 3988
	public class PickupCursorManager
	{
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06007554 RID: 30036 RVA: 0x0000216A File Offset: 0x0000036A
		public int[] pickupCardIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06007555 RID: 30037 RVA: 0x0000216A File Offset: 0x0000036A
		public int[] premiumIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06007556 RID: 30038 RVA: 0x0000216A File Offset: 0x0000036A
		public int[] deckCardIdx
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06007557 RID: 30039 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007558 RID: 30040 RVA: 0x0000216D File Offset: 0x0000036D
		private int selectedPickId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06007559 RID: 30041 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMobile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600755A RID: 30042 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeSelectedId(int id)
		{
		}

		// Token: 0x0600755B RID: 30043 RVA: 0x00002739 File Offset: 0x00000939
		public PickupCursorManager(ElementObjectManager eom, int[] mrks, int[] premiums, int protectorId)
		{
		}

		// Token: 0x0600755C RID: 30044 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializePickups(int[] mrks, int[] premiums, int protectorId)
		{
		}

		// Token: 0x0600755D RID: 30045 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPickup(PickupCursorWidget pickupCursor, int mrk, int premiumId, int deckIdx)
		{
		}

		// Token: 0x0600755E RID: 30046 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemovePickup(int pickupId)
		{
		}

		// Token: 0x0600755F RID: 30047 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateCursor(PickupCursorWidget pickupCursor, int mrk, int premiumId, int pickupId = 0, int deckIdx = -1, bool notRemove = false)
		{
		}

		// Token: 0x06007560 RID: 30048 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CheckPickup(int mrk, int premiumId, int deckIdx)
		{
			return 0;
		}

		// Token: 0x06007561 RID: 30049 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CheckPickupByDeckIdx(int deckIdx)
		{
			return 0;
		}

		// Token: 0x06007562 RID: 30050 RVA: 0x0000216D File Offset: 0x0000036D
		internal void PickupCardsCallBacks(DeckBrowserViewController deckBrowser)
		{
		}

		// Token: 0x0400AE9A RID: 44698
		private Dictionary<int, PickupCursorWidget> dictSelectedCursor;

		// Token: 0x0400AE9B RID: 44699
		private readonly string k_ELabelPickupCard0;

		// Token: 0x0400AE9C RID: 44700
		private readonly string k_ELabelPickupCard1;

		// Token: 0x0400AE9D RID: 44701
		private readonly string k_ELabelPickupCard2;

		// Token: 0x0400AE9E RID: 44702
		private const int numPickups = 3;

		// Token: 0x0400AE9F RID: 44703
		private int[] m_PickCardIds;

		// Token: 0x0400AEA0 RID: 44704
		private int[] m_PickPremiumIds;

		// Token: 0x0400AEA1 RID: 44705
		private int[] m_DeckCardIdx;

		// Token: 0x0400AEA2 RID: 44706
		private bool[] m_InitFlag;

		// Token: 0x0400AEA3 RID: 44707
		private int m_SelectedPickIdx;

		// Token: 0x0400AEA4 RID: 44708
		private bool isIni;

		// Token: 0x0400AEA5 RID: 44709
		private List<ElementObjectManager> m_PickupCardEoms;

		// Token: 0x0400AEA6 RID: 44710
		private List<DeckCardWidget> m_PickupCards;
	}
}
