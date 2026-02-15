using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010D0 RID: 4304
	public class SequenceBehaviourLoadCards : SequenceBehaviour
	{
		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06007FD4 RID: 32724 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool isAcceptToSkipLoop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007FD5 RID: 32725 RVA: 0x000F68C6 File Offset: 0x000F4AC6
		public SequenceBehaviourLoadCards(SequenceBehaviourWork sequenceBehaviourWork, GameObject owner, List<int> mrks)
			: base(null)
		{
		}

		// Token: 0x06007FD6 RID: 32726 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBegin()
		{
		}

		// Token: 0x06007FD7 RID: 32727 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}

		// Token: 0x06007FD8 RID: 32728 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnEnd()
		{
		}

		// Token: 0x0400B85F RID: 47199
		private readonly GameObject m_Owner;

		// Token: 0x0400B860 RID: 47200
		private readonly List<int> m_Mrks;

		// Token: 0x0400B861 RID: 47201
		private bool m_IsDone;
	}
}
