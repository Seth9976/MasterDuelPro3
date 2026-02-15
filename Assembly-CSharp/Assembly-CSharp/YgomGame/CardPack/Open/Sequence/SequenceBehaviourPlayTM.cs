using System;
using UnityEngine.Playables;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010D5 RID: 4309
	public class SequenceBehaviourPlayTM : SequenceBehaviour
	{
		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06007FE9 RID: 32745 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool isAcceptToSkipLoop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007FEA RID: 32746 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourPlayTM(SequenceBehaviourWork sequenceBehaviourWork, PlayableAsset playableAsset)
			: base(null)
		{
		}

		// Token: 0x06007FEB RID: 32747 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBegin()
		{
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}

		// Token: 0x0400B872 RID: 47218
		private PlayableAsset m_PlayableAsset;
	}
}
