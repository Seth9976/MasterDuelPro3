using System;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010D1 RID: 4305
	public class SequenceBehaviourPackOpen : SequenceBehaviour
	{
		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06007FD9 RID: 32729 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool isAcceptToSkipLoop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007FDA RID: 32730 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourPackOpen(SequenceBehaviourWork sequenceBehaviourWork)
			: base(null)
		{
		}
	}
}
