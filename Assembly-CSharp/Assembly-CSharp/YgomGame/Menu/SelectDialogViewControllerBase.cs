using System;

namespace YgomGame.Menu
{
	// Token: 0x02000AEC RID: 2796
	public abstract class SelectDialogViewControllerBase<RESULT> : InformDialogViewControllerBase<Action<RESULT>>
	{
		// Token: 0x06005162 RID: 20834 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnDecided(RESULT result)
		{
		}
	}
}
