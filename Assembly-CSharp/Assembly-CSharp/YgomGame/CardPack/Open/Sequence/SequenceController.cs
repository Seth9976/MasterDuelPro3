using System;
using System.Collections.Generic;

namespace YgomGame.CardPack.Open.Sequence
{
	// Token: 0x020010D7 RID: 4311
	public class SequenceController
	{
		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06007FEE RID: 32750 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDone
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x00002739 File Offset: 0x00000939
		public SequenceController(SequenceBehaviourWork work)
		{
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Update()
		{
			return false;
		}

		// Token: 0x06007FF1 RID: 32753 RVA: 0x0000216D File Offset: 0x0000036D
		public void Skip()
		{
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnBack()
		{
			return false;
		}

		// Token: 0x0400B875 RID: 47221
		public readonly SequenceBehaviourWork work;

		// Token: 0x0400B876 RID: 47222
		public readonly List<ISequenceBehaviour> behaviourList;

		// Token: 0x0400B877 RID: 47223
		private int m_BehaviourIdx;
	}
}
