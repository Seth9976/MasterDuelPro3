using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EE RID: 494
	[EventCategory(EventCategory.PointerDown)]
	public class MouseDownEvent : MouseEventBase<MouseDownEvent>
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003F99B File Offset: 0x0003DB9B
		static MouseDownEvent()
		{
			EventBase<MouseDownEvent>.SetCreateFunction(() => new MouseDownEvent());
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003F9B4 File Offset: 0x0003DBB4
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003F9C5 File Offset: 0x0003DBC5
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003F9D0 File Offset: 0x0003DBD0
		public MouseDownEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003F9E4 File Offset: 0x0003DBE4
		private static MouseDownEvent MakeFromPointerEvent(IPointerEvent pointerEvent)
		{
			return MouseEventBase<MouseDownEvent>.GetPooled(pointerEvent);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003F9FC File Offset: 0x0003DBFC
		internal static MouseDownEvent GetPooled(PointerDownEvent pointerEvent)
		{
			return MouseDownEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003FA14 File Offset: 0x0003DC14
		internal static MouseDownEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseDownEvent.MakeFromPointerEvent(pointerEvent);
		}
	}
}
