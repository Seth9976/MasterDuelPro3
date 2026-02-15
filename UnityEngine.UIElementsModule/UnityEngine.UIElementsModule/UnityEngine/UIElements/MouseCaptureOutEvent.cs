using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B7 RID: 439
	public class MouseCaptureOutEvent : MouseCaptureEventBase<MouseCaptureOutEvent>
	{
		// Token: 0x06000C73 RID: 3187 RVA: 0x0003BF6F File Offset: 0x0003A16F
		static MouseCaptureOutEvent()
		{
			EventBase<MouseCaptureOutEvent>.SetCreateFunction(() => new MouseCaptureOutEvent());
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0003BF88 File Offset: 0x0003A188
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateCursorStyle(this.eventTypeId);
		}
	}
}
