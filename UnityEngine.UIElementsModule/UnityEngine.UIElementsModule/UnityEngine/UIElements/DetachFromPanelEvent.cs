using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000215 RID: 533
	public class DetachFromPanelEvent : PanelChangedEventBase<DetachFromPanelEvent>
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x00040BE5 File Offset: 0x0003EDE5
		static DetachFromPanelEvent()
		{
			EventBase<DetachFromPanelEvent>.SetCreateFunction(() => new DetachFromPanelEvent());
		}
	}
}
