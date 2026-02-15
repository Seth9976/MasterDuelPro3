using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F0 RID: 496
	public class MouseUpEvent : MouseEventBase<MouseUpEvent>
	{
		// Token: 0x06000DEA RID: 3562 RVA: 0x0003FA3F File Offset: 0x0003DC3F
		static MouseUpEvent()
		{
			EventBase<MouseUpEvent>.SetCreateFunction(() => new MouseUpEvent());
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003FA58 File Offset: 0x0003DC58
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003F9C5 File Offset: 0x0003DBC5
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0003FA69 File Offset: 0x0003DC69
		public MouseUpEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0003FA7C File Offset: 0x0003DC7C
		private static MouseUpEvent MakeFromPointerEvent(IPointerEvent pointerEvent)
		{
			return MouseEventBase<MouseUpEvent>.GetPooled(pointerEvent);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0003FA94 File Offset: 0x0003DC94
		internal static MouseUpEvent GetPooled(PointerUpEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003FAAC File Offset: 0x0003DCAC
		internal static MouseUpEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003FAC4 File Offset: 0x0003DCC4
		internal static MouseUpEvent GetPooled(PointerCancelEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}
	}
}
