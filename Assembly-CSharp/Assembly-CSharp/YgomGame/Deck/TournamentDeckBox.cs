using System;
using UnityEngine.UI;
using YgomGame.Menu.Common;

namespace YgomGame.Deck
{
	// Token: 0x02001009 RID: 4105
	public class TournamentDeckBox : DeckBox
	{
		// Token: 0x06007B95 RID: 31637 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007B96 RID: 31638 RVA: 0x0000216A File Offset: 0x0000036A
		public IAsyncProgressContainer SetData(int id, string name, int case_id, int protector_id, int[] pickup_ids, int[] pickup_decos, int logoId, int stage = 0, bool opened = false)
		{
			return null;
		}

		// Token: 0x0400B332 RID: 45874
		private Image m_TournamentLogo;

		// Token: 0x0400B333 RID: 45875
		private Image m_TournamentBG;
	}
}
