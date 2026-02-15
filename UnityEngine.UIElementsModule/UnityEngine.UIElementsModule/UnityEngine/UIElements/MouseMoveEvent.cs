using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F2 RID: 498
	[EventCategory(EventCategory.PointerMove)]
	public class MouseMoveEvent : MouseEventBase<MouseMoveEvent>
	{
		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003FAEF File Offset: 0x0003DCEF
		static MouseMoveEvent()
		{
			EventBase<MouseMoveEvent>.SetCreateFunction(() => new MouseMoveEvent());
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003FB08 File Offset: 0x0003DD08
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003EBEB File Offset: 0x0003CDEB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003FB19 File Offset: 0x0003DD19
		public MouseMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003FB2C File Offset: 0x0003DD2C
		internal static MouseMoveEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseEventBase<MouseMoveEvent>.GetPooled(pointerEvent);
		}
	}
}
