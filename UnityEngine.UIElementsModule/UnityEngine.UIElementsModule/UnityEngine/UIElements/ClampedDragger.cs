using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004A RID: 74
	internal class ClampedDragger<T> : Clickable where T : IComparable<T>
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000226 RID: 550 RVA: 0x0000AC10 File Offset: 0x00008E10
		// (remove) Token: 0x06000227 RID: 551 RVA: 0x0000AC48 File Offset: 0x00008E48
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action dragging;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000228 RID: 552 RVA: 0x0000AC80 File Offset: 0x00008E80
		// (remove) Token: 0x06000229 RID: 553 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action draggingEnded;

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000ACED File Offset: 0x00008EED
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000ACF5 File Offset: 0x00008EF5
		public ClampedDragger<T>.DragDirection dragDirection { get; set; }

		// Token: 0x17000048 RID: 72
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000ACFE File Offset: 0x00008EFE
		private BaseSlider<T> slider
		{
			[CompilerGenerated]
			set
			{
				this.<slider>k__BackingField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000AD07 File Offset: 0x00008F07
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000AD0F File Offset: 0x00008F0F
		public Vector2 startMousePosition { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000AD18 File Offset: 0x00008F18
		public Vector2 delta
		{
			get
			{
				return base.lastMousePosition - this.startMousePosition;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000AD2B File Offset: 0x00008F2B
		public ClampedDragger(BaseSlider<T> slider, Action clickHandler, Action dragHandler)
			: base(clickHandler, 250L, 30L)
		{
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			this.slider = slider;
			this.dragging += dragHandler;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000AD57 File Offset: 0x00008F57
		protected override void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.startMousePosition = localPosition;
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			base.ProcessDownEvent(evt, localPosition, pointerId);
			Action action = this.dragging;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000AD86 File Offset: 0x00008F86
		protected override void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			base.ProcessUpEvent(evt, localPosition, pointerId);
			Action action = this.draggingEnded;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		protected override void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			base.ProcessMoveEvent(evt, localPosition);
			bool flag = this.dragDirection == ClampedDragger<T>.DragDirection.None;
			if (flag)
			{
				this.dragDirection = ClampedDragger<T>.DragDirection.Free;
			}
			bool flag2 = this.dragDirection == ClampedDragger<T>.DragDirection.Free;
			if (flag2)
			{
				bool flag3 = evt.eventTypeId == EventBase<PointerMoveEvent>.TypeId();
				if (flag3)
				{
					PointerMoveEvent pointerMoveEvent = (PointerMoveEvent)evt;
					bool flag4 = pointerMoveEvent.pointerId != PointerId.mousePointerId;
					if (flag4)
					{
						pointerMoveEvent.isHandledByDraggable = true;
					}
				}
				Action action = this.dragging;
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x0200004B RID: 75
		[Flags]
		public enum DragDirection
		{
			// Token: 0x0400016C RID: 364
			None = 0,
			// Token: 0x0400016D RID: 365
			LowToHigh = 1,
			// Token: 0x0400016E RID: 366
			HighToLow = 2,
			// Token: 0x0400016F RID: 367
			Free = 4
		}
	}
}
