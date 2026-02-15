using System;

namespace YgomGame.Menu
{
	// Token: 0x02000AEE RID: 2798
	public abstract class SelectDialogViewControllerBase<ARG1, ARG2, RESULT> : InformDialogViewControllerBase<ARG1, ARG2, Action<RESULT>>
	{
		// Token: 0x06005166 RID: 20838 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnDecided(RESULT result)
		{
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SendResult(RESULT result)
		{
		}
	}
}
