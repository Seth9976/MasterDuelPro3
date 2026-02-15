using System;
using System.Collections.Generic;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010C9 RID: 4297
	public class SequenceBehaviourCardsEntry : SequenceBehaviour
	{
		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06007F9B RID: 32667 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool isAcceptToSkipLoop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007F9C RID: 32668 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourCardsEntry(SequenceBehaviourWork sequenceBehaviourWork, DrawPackData drawPackData)
			: base(null)
		{
		}

		// Token: 0x06007F9D RID: 32669 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBegin()
		{
		}

		// Token: 0x06007F9E RID: 32670 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}

		// Token: 0x0400B831 RID: 47153
		private readonly DrawPackData m_DrawPackData;

		// Token: 0x0400B832 RID: 47154
		private double m_WaitSec;

		// Token: 0x0400B833 RID: 47155
		private List<int> m_ExistsPosList;
	}
}
