using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F6 RID: 502
	public class WheelEvent : MouseEventBase<WheelEvent>
	{
		// Token: 0x06000E02 RID: 3586 RVA: 0x0003FB8C File Offset: 0x0003DD8C
		static WheelEvent()
		{
			EventBase<WheelEvent>.SetCreateFunction(() => new WheelEvent());
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0003FBA5 File Offset: 0x0003DDA5
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x0003FBAD File Offset: 0x0003DDAD
		public Vector3 delta { get; private set; }

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003FBB8 File Offset: 0x0003DDB8
		public new static WheelEvent GetPooled(Event systemEvent)
		{
			WheelEvent e = MouseEventBase<WheelEvent>.GetPooled(systemEvent);
			bool flag = systemEvent != null;
			if (flag)
			{
				e.delta = systemEvent.delta;
			}
			return e;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003FBF0 File Offset: 0x0003DDF0
		internal static WheelEvent GetPooled(Vector3 delta, Vector3 mousePosition, EventModifiers modifiers = EventModifiers.None)
		{
			WheelEvent e = EventBase<WheelEvent>.GetPooled();
			e.delta = delta;
			e.mousePosition = mousePosition;
			e.modifiers = modifiers;
			return e;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003FC28 File Offset: 0x0003DE28
		internal static WheelEvent GetPooled(Vector3 delta, IPointerEvent pointerEvent)
		{
			WheelEvent e = MouseEventBase<WheelEvent>.GetPooled(pointerEvent);
			e.delta = delta;
			return e;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003FC4A File Offset: 0x0003DE4A
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003FC5B File Offset: 0x0003DE5B
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			this.delta = Vector3.zero;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003FC72 File Offset: 0x0003DE72
		public WheelEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003FC83 File Offset: 0x0003DE83
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToElementUnderPointerOrPanelRoot(this, panel, PointerId.mousePointerId, base.mousePosition);
		}
	}
}
