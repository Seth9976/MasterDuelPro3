using System;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010CA RID: 4298
	public class SequenceBehaviourCardsExit : SequenceBehaviour
	{
		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06007F9F RID: 32671 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool isAcceptToSkipLoop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007FA0 RID: 32672 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourCardsExit(SequenceBehaviourWork sequenceBehaviourWork)
			: base(null)
		{
		}

		// Token: 0x06007FA1 RID: 32673 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}
	}
}
