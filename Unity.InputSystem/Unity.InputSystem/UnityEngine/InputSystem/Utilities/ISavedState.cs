using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000260 RID: 608
	internal interface ISavedState
	{
		// Token: 0x0600161E RID: 5662
		void StaticDisposeCurrentState();

		// Token: 0x0600161F RID: 5663
		void RestoreSavedState();
	}
}
