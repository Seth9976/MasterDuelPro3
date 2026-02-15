using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000242 RID: 578
	public sealed class TransitionCancelEvent : TransitionEventBase<TransitionCancelEvent>
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x00043398 File Offset: 0x00041598
		static TransitionCancelEvent()
		{
			EventBase<TransitionCancelEvent>.SetCreateFunction(() => new TransitionCancelEvent());
		}
	}
}
