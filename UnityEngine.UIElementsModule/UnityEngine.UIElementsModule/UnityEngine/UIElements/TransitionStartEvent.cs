using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023E RID: 574
	public sealed class TransitionStartEvent : TransitionEventBase<TransitionStartEvent>
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x0004332E File Offset: 0x0004152E
		static TransitionStartEvent()
		{
			EventBase<TransitionStartEvent>.SetCreateFunction(() => new TransitionStartEvent());
		}
	}
}
