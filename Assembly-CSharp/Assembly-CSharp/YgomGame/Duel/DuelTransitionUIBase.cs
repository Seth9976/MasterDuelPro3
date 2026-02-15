using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DA1 RID: 3489
	public abstract class DuelTransitionUIBase : DuelUIBase
	{
		// Token: 0x060066A4 RID: 26276 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateOperation()
		{
		}

		// Token: 0x060066A5 RID: 26277 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Update()
		{
		}

		// Token: 0x0400A0BA RID: 41146
		protected Queue<DuelTransitionUIBase.OperationInfoBase> operationQueue;

		// Token: 0x0400A0BB RID: 41147
		private DuelTransitionUIBase.OperationInfoBase currentOperation;

		// Token: 0x02000DA2 RID: 3490
		protected abstract class OperationInfoBase
		{
			// Token: 0x0400A0BC RID: 41148
			public Action act;

			// Token: 0x0400A0BD RID: 41149
			public Func<bool> wait;
		}
	}
}
