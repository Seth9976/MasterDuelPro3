using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B4 RID: 436
	public class PointerCaptureEvent : PointerCaptureEventBase<PointerCaptureEvent>
	{
		// Token: 0x06000C6C RID: 3180 RVA: 0x0003BF14 File Offset: 0x0003A114
		static PointerCaptureEvent()
		{
			EventBase<PointerCaptureEvent>.SetCreateFunction(() => new PointerCaptureEvent());
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0003BF2D File Offset: 0x0003A12D
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateHoverPseudoStateAfterCaptureChange(base.pointerId);
		}
	}
}
