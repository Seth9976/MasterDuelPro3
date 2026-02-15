using System;
using System.Diagnostics;
using System.Runtime.Diagnostics;

namespace System.Runtime
{
	// Token: 0x02000003 RID: 3
	internal abstract class ActionItem
	{
		// Token: 0x17000001 RID: 1
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		protected bool LowPriority
		{
			set
			{
				this.lowPriority = value;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public static void Schedule(Action<object> callback, object state)
		{
			ActionItem.Schedule(callback, state, false);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206B File Offset: 0x0000026B
		public static void Schedule(Action<object> callback, object state, bool lowPriority)
		{
			if (PartialTrustHelpers.ShouldFlowSecurityContext || WaitCallbackActionItem.ShouldUseActivity || Fx.Trace.IsEnd2EndActivityTracingEnabled)
			{
				new ActionItem.DefaultActionItem(callback, state, lowPriority).Schedule();
				return;
			}
			ActionItem.ScheduleCallback(callback, state, lowPriority);
		}

		// Token: 0x06000005 RID: 5
		protected abstract void Invoke();

		// Token: 0x06000006 RID: 6 RVA: 0x0000209D File Offset: 0x0000029D
		protected void Schedule()
		{
			if (this.isScheduled)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("Action Item Is Already Scheduled"));
			}
			this.isScheduled = true;
			this.ScheduleCallback(ActionItem.CallbackHelper.InvokeWithoutContextCallback);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020CE File Offset: 0x000002CE
		private static void ScheduleCallback(Action<object> callback, object state, bool lowPriority)
		{
			if (lowPriority)
			{
				IOThreadScheduler.ScheduleCallbackLowPriNoFlow(callback, state);
				return;
			}
			IOThreadScheduler.ScheduleCallbackNoFlow(callback, state);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E2 File Offset: 0x000002E2
		private void ScheduleCallback(Action<object> callback)
		{
			ActionItem.ScheduleCallback(callback, this, this.lowPriority);
		}

		// Token: 0x04000002 RID: 2
		private bool isScheduled;

		// Token: 0x04000003 RID: 3
		private bool lowPriority;

		// Token: 0x02000004 RID: 4
		private static class CallbackHelper
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000009 RID: 9 RVA: 0x000020F1 File Offset: 0x000002F1
			public static Action<object> InvokeWithoutContextCallback
			{
				get
				{
					if (ActionItem.CallbackHelper.invokeWithoutContextCallback == null)
					{
						ActionItem.CallbackHelper.invokeWithoutContextCallback = new Action<object>(ActionItem.CallbackHelper.InvokeWithoutContext);
					}
					return ActionItem.CallbackHelper.invokeWithoutContextCallback;
				}
			}

			// Token: 0x0600000A RID: 10 RVA: 0x00002110 File Offset: 0x00000310
			private static void InvokeWithoutContext(object state)
			{
				((ActionItem)state).Invoke();
				((ActionItem)state).isScheduled = false;
			}

			// Token: 0x04000004 RID: 4
			private static Action<object> invokeWithoutContextCallback;
		}

		// Token: 0x02000005 RID: 5
		private class DefaultActionItem : ActionItem
		{
			// Token: 0x0600000B RID: 11 RVA: 0x0000212C File Offset: 0x0000032C
			public DefaultActionItem(Action<object> callback, object state, bool isLowPriority)
			{
				base.LowPriority = isLowPriority;
				this.callback = callback;
				this.state = state;
				if (WaitCallbackActionItem.ShouldUseActivity)
				{
					this.flowLegacyActivityId = true;
					this.activityId = DiagnosticTraceBase.ActivityId;
				}
				if (Fx.Trace.IsEnd2EndActivityTracingEnabled)
				{
					this.eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
					if (TraceCore.ActionItemScheduledIsEnabled(Fx.Trace))
					{
						TraceCore.ActionItemScheduled(Fx.Trace, this.eventTraceActivity);
					}
				}
			}

			// Token: 0x0600000C RID: 12 RVA: 0x000021A1 File Offset: 0x000003A1
			protected override void Invoke()
			{
				if (this.flowLegacyActivityId || Fx.Trace.IsEnd2EndActivityTracingEnabled)
				{
					this.TraceAndInvoke();
					return;
				}
				this.callback(this.state);
			}

			// Token: 0x0600000D RID: 13 RVA: 0x000021D0 File Offset: 0x000003D0
			private void TraceAndInvoke()
			{
				if (this.flowLegacyActivityId)
				{
					Guid guid = DiagnosticTraceBase.ActivityId;
					try
					{
						DiagnosticTraceBase.ActivityId = this.activityId;
						this.callback(this.state);
						return;
					}
					finally
					{
						DiagnosticTraceBase.ActivityId = guid;
					}
				}
				Guid empty = Guid.Empty;
				bool flag = false;
				try
				{
					if (this.eventTraceActivity != null)
					{
						empty = Trace.CorrelationManager.ActivityId;
						flag = true;
						Trace.CorrelationManager.ActivityId = this.eventTraceActivity.ActivityId;
						if (TraceCore.ActionItemCallbackInvokedIsEnabled(Fx.Trace))
						{
							TraceCore.ActionItemCallbackInvoked(Fx.Trace, this.eventTraceActivity);
						}
					}
					this.callback(this.state);
				}
				finally
				{
					if (flag)
					{
						Trace.CorrelationManager.ActivityId = empty;
					}
				}
			}

			// Token: 0x04000005 RID: 5
			private Action<object> callback;

			// Token: 0x04000006 RID: 6
			private object state;

			// Token: 0x04000007 RID: 7
			private bool flowLegacyActivityId;

			// Token: 0x04000008 RID: 8
			private Guid activityId;

			// Token: 0x04000009 RID: 9
			private EventTraceActivity eventTraceActivity;
		}
	}
}
