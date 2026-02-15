using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004C RID: 76
	public class Clickable : PointerManipulator
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000234 RID: 564 RVA: 0x0000AE30 File Offset: 0x00009030
		// (remove) Token: 0x06000235 RID: 565 RVA: 0x0000AE68 File Offset: 0x00009068
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<EventBase> clickedWithEventInfo;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000236 RID: 566 RVA: 0x0000AEA0 File Offset: 0x000090A0
		// (remove) Token: 0x06000237 RID: 567 RVA: 0x0000AED8 File Offset: 0x000090D8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action clicked;

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000AF0D File Offset: 0x0000910D
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000AF15 File Offset: 0x00009115
		protected bool active { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000AF1E File Offset: 0x0000911E
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000AF26 File Offset: 0x00009126
		public Vector2 lastMousePosition { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000AF2F File Offset: 0x0000912F
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000AF38 File Offset: 0x00009138
		internal bool acceptClicksIfDisabled
		{
			get
			{
				return this.m_AcceptClicksIfDisabled;
			}
			set
			{
				bool flag = this.m_AcceptClicksIfDisabled == value;
				if (!flag)
				{
					bool flag2 = base.target != null;
					if (flag2)
					{
						this.UnregisterCallbacksFromTarget();
					}
					this.m_AcceptClicksIfDisabled = value;
					bool flag3 = base.target != null;
					if (flag3)
					{
						this.RegisterCallbacksOnTarget();
					}
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000AF84 File Offset: 0x00009184
		private InvokePolicy invokePolicy
		{
			get
			{
				return this.acceptClicksIfDisabled ? InvokePolicy.IncludeDisabled : InvokePolicy.Default;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000AF92 File Offset: 0x00009192
		public Clickable(Action handler, long delay, long interval)
			: this(handler)
		{
			this.m_Delay = delay;
			this.m_Interval = interval;
			this.active = false;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000AFB4 File Offset: 0x000091B4
		public Clickable(Action<EventBase> handler)
		{
			this.m_ActivePointerId = -1;
			base..ctor();
			this.clickedWithEventInfo = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000AFF8 File Offset: 0x000091F8
		public Clickable(Action handler)
		{
			this.m_ActivePointerId = -1;
			base..ctor();
			this.clicked = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.active = false;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000B044 File Offset: 0x00009244
		private void OnTimer(TimerState timerState)
		{
			bool flag = (this.clicked != null || this.clickedWithEventInfo != null) && this.IsRepeatable();
			if (flag)
			{
				bool flag2 = this.ContainsPointer(this.m_ActivePointerId) && (base.target.enabledInHierarchy || this.acceptClicksIfDisabled);
				if (flag2)
				{
					this.Invoke(null);
					base.target.pseudoStates |= PseudoStates.Active;
				}
				else
				{
					base.target.pseudoStates &= ~PseudoStates.Active;
				}
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B0D4 File Offset: 0x000092D4
		private bool IsRepeatable()
		{
			return this.m_Delay > 0L || this.m_Interval > 0L;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B100 File Offset: 0x00009300
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B19C File Offset: 0x0000939C
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			this.ResetActivePseudoState();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B230 File Offset: 0x00009430
		protected void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B268 File Offset: 0x00009468
		protected void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessMoveEvent(evt, evt.localPosition);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B298 File Offset: 0x00009498
		protected void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B2DC File Offset: 0x000094DC
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B314 File Offset: 0x00009514
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B340 File Offset: 0x00009540
		private bool ContainsPointer(int pointerId)
		{
			VisualElement elementUnderPointer = base.target.elementPanel.GetTopElementUnderPointer(pointerId);
			return base.target == elementUnderPointer || base.target.Contains(elementUnderPointer);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B37C File Offset: 0x0000957C
		protected void Invoke(EventBase evt)
		{
			Action action = this.clicked;
			if (action != null)
			{
				action();
			}
			Action<EventBase> action2 = this.clickedWithEventInfo;
			if (action2 != null)
			{
				action2(evt);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B3A4 File Offset: 0x000095A4
		internal void SimulateSingleClick(EventBase evt, int delayMs = 100)
		{
			base.target.pseudoStates |= PseudoStates.Active;
			this.m_PendingActivePseudoStateReset = base.target.schedule.Execute(new Action(this.ResetActivePseudoState));
			this.m_PendingActivePseudoStateReset.ExecuteLater((long)delayMs);
			this.Invoke(evt);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B400 File Offset: 0x00009600
		private void ResetActivePseudoState()
		{
			bool flag = this.m_PendingActivePseudoStateReset == null;
			if (!flag)
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
				this.m_PendingActivePseudoStateReset = null;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B43C File Offset: 0x0000963C
		protected virtual void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = true;
			this.m_ActivePointerId = pointerId;
			base.target.CapturePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			this.lastMousePosition = localPosition;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				bool flag3 = this.ContainsPointer(pointerId) && (base.target.enabledInHierarchy || this.acceptClicksIfDisabled);
				if (flag3)
				{
					this.Invoke(evt);
				}
				bool flag4 = this.m_Repeater == null;
				if (flag4)
				{
					this.m_Repeater = base.target.schedule.Execute(new Action<TimerState>(this.OnTimer)).Every(this.m_Interval).StartingIn(this.m_Delay);
				}
				else
				{
					this.m_Repeater.ExecuteLater(this.m_Delay);
				}
			}
			base.target.pseudoStates |= PseudoStates.Active;
			evt.StopImmediatePropagation();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B548 File Offset: 0x00009748
		protected virtual void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			this.lastMousePosition = localPosition;
			bool flag = this.ContainsPointer(this.m_ActivePointerId);
			if (flag)
			{
				base.target.pseudoStates |= PseudoStates.Active;
			}
			else
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
			}
			evt.StopPropagation();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B5A4 File Offset: 0x000097A4
		protected virtual void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			this.m_ActivePointerId = -1;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem repeater = this.m_Repeater;
				if (repeater != null)
				{
					repeater.Pause();
				}
			}
			else
			{
				bool flag3 = this.ContainsPointer(pointerId) && (base.target.enabledInHierarchy || this.acceptClicksIfDisabled);
				if (flag3)
				{
					this.Invoke(evt);
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B660 File Offset: 0x00009860
		protected virtual void ProcessCancelEvent(EventBase evt, int pointerId)
		{
			this.active = false;
			this.m_ActivePointerId = -1;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem repeater = this.m_Repeater;
				if (repeater != null)
				{
					repeater.Pause();
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x04000172 RID: 370
		private readonly long m_Delay;

		// Token: 0x04000173 RID: 371
		private readonly long m_Interval;

		// Token: 0x04000176 RID: 374
		private int m_ActivePointerId;

		// Token: 0x04000177 RID: 375
		private bool m_AcceptClicksIfDisabled;

		// Token: 0x04000178 RID: 376
		private IVisualElementScheduledItem m_Repeater;

		// Token: 0x04000179 RID: 377
		private IVisualElementScheduledItem m_PendingActivePseudoStateReset;
	}
}
