using System;

namespace YgomGame.Menu
{
	// Token: 0x02000AEF RID: 2799
	public abstract class SelectDialogViewControllerBase<ARG1, ARG2, ARG3, RESULT> : InformDialogViewControllerBase<ARG1, ARG2, ARG3, Action<RESULT>>
	{
		// Token: 0x06005169 RID: 20841 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnDecided(RESULT result)
		{
		}
	}
}
