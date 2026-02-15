using System;
using YgomGame.Deck;
using YgomSystem.ElementSystem;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F8A RID: 3978
	public class CardDetailWidget : CardDetailView
	{
		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060074A5 RID: 29861 RVA: 0x0000216A File Offset: 0x0000036A
		public ElementObjectManager eom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060074A6 RID: 29862 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060074A7 RID: 29863 RVA: 0x0000216D File Offset: 0x0000036D
		public bool regulationVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardDetailWidget Create(ElementObjectManager eom, bool isMobile = false)
		{
			return null;
		}

		// Token: 0x060074A9 RID: 29865 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardByID(int id)
		{
		}

		// Token: 0x060074AA RID: 29866 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPremiumID(int premiumId)
		{
		}

		// Token: 0x060074AB RID: 29867 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRegulation(int reg)
		{
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x0000216D File Offset: 0x0000036D
		private void setNumPremiums()
		{
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAsRental(int rentalPoolID, bool isRental, bool dispDismantleableText = false)
		{
		}

		// Token: 0x060074AE RID: 29870 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0400ADAA RID: 44458
		private ElementObjectManager m_EomCache;

		// Token: 0x0400ADAB RID: 44459
		private bool m_RagulationVisible;

		// Token: 0x0400ADAC RID: 44460
		private bool m_IsMobile;
	}
}
