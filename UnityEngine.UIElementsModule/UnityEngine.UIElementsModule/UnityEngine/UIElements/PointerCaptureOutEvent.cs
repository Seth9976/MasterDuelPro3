using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B2 RID: 434
	public class PointerCaptureOutEvent : PointerCaptureEventBase<PointerCaptureOutEvent>
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x0003BEC2 File Offset: 0x0003A0C2
		static PointerCaptureOutEvent()
		{
			EventBase<PointerCaptureOutEvent>.SetCreateFunction(() => new PointerCaptureOutEvent());
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0003BEDB File Offset: 0x0003A0DB
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateHoverPseudoStateAfterCaptureChange(base.pointerId);
		}
	}
}
