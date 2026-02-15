using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B9 RID: 441
	public class MouseCaptureEvent : MouseCaptureEventBase<MouseCaptureEvent>
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x0003BFC1 File Offset: 0x0003A1C1
		static MouseCaptureEvent()
		{
			EventBase<MouseCaptureEvent>.SetCreateFunction(() => new MouseCaptureEvent());
		}
	}
}
