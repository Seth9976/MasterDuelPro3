using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000240 RID: 576
	public sealed class TransitionEndEvent : TransitionEventBase<TransitionEndEvent>
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x00043363 File Offset: 0x00041563
		static TransitionEndEvent()
		{
			EventBase<TransitionEndEvent>.SetCreateFunction(() => new TransitionEndEvent());
		}
	}
}
