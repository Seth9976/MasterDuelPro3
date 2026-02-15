using System;

namespace YgomGame.Menu
{
	// Token: 0x02000AED RID: 2797
	public abstract class SelectDialogViewControllerBase<ARG, RESULT> : InformDialogViewControllerBase<ARG, Action<RESULT>>
	{
		// Token: 0x06005164 RID: 20836 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnDecided(RESULT result)
		{
		}
	}
}
