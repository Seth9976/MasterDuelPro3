using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000213 RID: 531
	public class AttachToPanelEvent : PanelChangedEventBase<AttachToPanelEvent>
	{
		// Token: 0x06000E81 RID: 3713 RVA: 0x00040BB0 File Offset: 0x0003EDB0
		static AttachToPanelEvent()
		{
			EventBase<AttachToPanelEvent>.SetCreateFunction(() => new AttachToPanelEvent());
		}
	}
}
