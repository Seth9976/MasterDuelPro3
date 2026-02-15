using System;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010D3 RID: 4307
	public class SequenceBehaviourPacksEntryNext : SequenceBehaviour
	{
		// Token: 0x06007FE0 RID: 32736 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourPacksEntryNext(SequenceBehaviourWork sequenceBehaviourWork, int packTotal, int packIdx, DrawPackData drawPackData)
			: base(null)
		{
		}

		// Token: 0x06007FE1 RID: 32737 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBegin()
		{
		}

		// Token: 0x06007FE2 RID: 32738 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}

		// Token: 0x06007FE3 RID: 32739 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0400B867 RID: 47207
		private readonly int m_PackTotal;

		// Token: 0x0400B868 RID: 47208
		private readonly int m_PackIdx;

		// Token: 0x0400B869 RID: 47209
		private readonly DrawPackData m_DrawPackData;
	}
}
