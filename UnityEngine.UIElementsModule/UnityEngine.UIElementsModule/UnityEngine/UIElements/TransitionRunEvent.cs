using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023C RID: 572
	public sealed class TransitionRunEvent : TransitionEventBase<TransitionRunEvent>
	{
		// Token: 0x06000F7E RID: 3966 RVA: 0x000432F9 File Offset: 0x000414F9
		static TransitionRunEvent()
		{
			EventBase<TransitionRunEvent>.SetCreateFunction(() => new TransitionRunEvent());
		}
	}
}
