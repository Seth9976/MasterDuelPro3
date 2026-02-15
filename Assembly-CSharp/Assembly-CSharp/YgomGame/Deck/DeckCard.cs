using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Deck
{
	// Token: 0x02000FC6 RID: 4038
	public class DeckCard : DeckEditCard
	{
		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x060078DB RID: 30939 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060078DC RID: 30940 RVA: 0x0000216D File Offset: 0x0000036D
		public DeckCard.LocationInDeck m_Location
		{
			[CompilerGenerated]
			get
			{
				return DeckCard.LocationInDeck.NA;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x060078DD RID: 30941 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060078DE RID: 30942 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x060078DF RID: 30943 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060078E0 RID: 30944 RVA: 0x0000216A File Offset: 0x0000036A
		public static DeckCard Create(Transform parent)
		{
			return null;
		}

		// Token: 0x060078E1 RID: 30945 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRarity(bool b)
		{
		}

		// Token: 0x060078E2 RID: 30946 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRegulation(int regurationID)
		{
		}

		// Token: 0x060078E3 RID: 30947 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRegulationVisible(bool b)
		{
		}

		// Token: 0x02000FC7 RID: 4039
		public enum LocationInDeck
		{
			// Token: 0x0400B09E RID: 45214
			NA,
			// Token: 0x0400B09F RID: 45215
			M,
			// Token: 0x0400B0A0 RID: 45216
			E,
			// Token: 0x0400B0A1 RID: 45217
			S,
			// Token: 0x0400B0A2 RID: 45218
			T,
			// Token: 0x0400B0A3 RID: 45219
			D
		}
	}
}
