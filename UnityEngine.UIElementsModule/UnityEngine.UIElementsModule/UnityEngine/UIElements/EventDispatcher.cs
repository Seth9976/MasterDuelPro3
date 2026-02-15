using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AD RID: 429
	public sealed class EventDispatcher
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0003B8E8 File Offset: 0x00039AE8
		internal PointerDispatchState pointerState { get; } = new PointerDispatchState();

		// Token: 0x06000C51 RID: 3153 RVA: 0x0003B8F0 File Offset: 0x00039AF0
		internal static EventDispatcher CreateDefault()
		{
			return new EventDispatcher();
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0003B908 File Offset: 0x00039B08
		[Obsolete("Please use EventDispatcher.CreateDefault().")]
		internal EventDispatcher()
		{
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0003B964 File Offset: 0x00039B64
		private bool dispatchImmediately
		{
			get
			{
				return this.m_Immediate || this.m_GateCount == 0U;
			}
		}

		// Token: 0x1700023C RID: 572
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0003B98A File Offset: 0x00039B8A
		private bool processingEvents
		{
			[CompilerGenerated]
			set
			{
				this.<processingEvents>k__BackingField = value;
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0003B994 File Offset: 0x00039B94
		internal void Dispatch(EventBase evt, [NotNull] BaseVisualElementPanel panel, DispatchMode dispatchMode)
		{
			evt.MarkReceivedByDispatcher();
			bool flag = evt.eventTypeId == EventBase<IMGUIEvent>.TypeId();
			if (flag)
			{
				Event e = evt.imguiEvent;
				bool flag2 = e.rawType == EventType.Repaint;
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.dispatchImmediately || dispatchMode == DispatchMode.Immediate;
			if (flag3)
			{
				this.ProcessEvent(evt, panel);
			}
			else
			{
				bool flag4 = this.HandleRecursiveState(evt);
				if (!flag4)
				{
					evt.Acquire();
					this.m_Queue.Enqueue(new EventDispatcher.EventRecord
					{
						m_Event = evt,
						m_Panel = panel
					});
				}
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0003BA34 File Offset: 0x00039C34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool HandleRecursiveState(EventBase evt)
		{
			bool flag = this.m_GateDepth <= 400U;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.m_DispatchStackFrame != 0;
				if (flag3)
				{
					StackTrace stack = new StackTrace(1, true);
					StringBuilder sb = new StringBuilder();
					int frameToDisplay = stack.FrameCount - this.m_DispatchStackFrame;
					sb.AppendLine(string.Format("Recursively dispatching event {0} from another event {1} (depth = {2})", evt, this.m_CurrentEvent, this.m_GateDepth));
					for (int i = 0; i < frameToDisplay; i++)
					{
						StackFrame frame = stack.GetFrame(i);
						sb.Append(frame.GetMethod()).AppendFormat("({0}:{1}", frame.GetFileName(), frame.GetFileLineNumber()).AppendLine(")");
					}
					Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, sb.ToString(), Array.Empty<object>());
				}
				else
				{
					Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, string.Format("Recursively dispatching event {0} from another event {1} (depth = {2})", evt, this.m_CurrentEvent, this.m_GateDepth), Array.Empty<object>());
				}
				bool flag4 = this.m_GateDepth > 500U;
				if (flag4)
				{
					Debug.LogErrorFormat("Ignoring event {0}: too many events dispatched recurively", new object[] { evt });
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0003BB7F File Offset: 0x00039D7F
		internal void CloseGate()
		{
			this.m_GateCount += 1U;
			this.m_GateDepth += 1U;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0003BBA0 File Offset: 0x00039DA0
		internal void OpenGate()
		{
			Debug.Assert(this.m_GateCount > 0U, "m_GateCount > 0");
			bool flag = this.m_GateCount > 0U;
			if (flag)
			{
				this.m_GateCount -= 1U;
			}
			try
			{
				bool flag2 = this.m_GateCount == 0U;
				if (flag2)
				{
					this.ProcessEventQueue();
				}
			}
			finally
			{
				Debug.Assert(this.m_GateDepth > 0U, "m_GateDepth > 0");
				bool flag3 = this.m_GateDepth > 0U;
				if (flag3)
				{
					this.m_GateDepth -= 1U;
				}
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0003BC40 File Offset: 0x00039E40
		private void ProcessEventQueue()
		{
			Queue<EventDispatcher.EventRecord> queueToProcess = this.m_Queue;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
			ExitGUIException caughtExitGUIException = null;
			try
			{
				this.processingEvents = true;
				while (queueToProcess.Count > 0)
				{
					EventDispatcher.EventRecord eventRecord = queueToProcess.Dequeue();
					EventBase evt = eventRecord.m_Event;
					BaseVisualElementPanel panel = eventRecord.m_Panel;
					try
					{
						this.ProcessEvent(evt, panel);
					}
					catch (ExitGUIException e)
					{
						Debug.Assert(caughtExitGUIException == null);
						caughtExitGUIException = e;
					}
					finally
					{
						evt.Dispose();
					}
				}
			}
			finally
			{
				this.processingEvents = false;
				EventDispatcher.k_EventQueuePool.Release(queueToProcess);
			}
			bool flag = caughtExitGUIException != null;
			if (flag)
			{
				throw caughtExitGUIException;
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0003BD18 File Offset: 0x00039F18
		private void ProcessEvent(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			Event e = evt.imguiEvent;
			bool imguiEventIsInitiallyUsed = e != null && e.rawType == EventType.Used;
			using (new EventDispatcherGate(this))
			{
				evt.PreDispatch(panel);
				try
				{
					this.m_CurrentEvent = evt;
					this.m_DispatchStackFrame = ((this.m_GateDepth > 490U) ? new StackTrace().FrameCount : 0);
					evt.Dispatch(panel);
				}
				finally
				{
					this.m_CurrentEvent = null;
				}
				evt.PostDispatch(panel);
				Debug.Assert(imguiEventIsInitiallyUsed || evt.isPropagationStopped || e == null || e.rawType != EventType.Used, "Event is used but not stopped.");
			}
		}

		// Token: 0x040007D2 RID: 2002
		internal ClickDetector m_ClickDetector = new ClickDetector();

		// Token: 0x040007D3 RID: 2003
		private static readonly ObjectPool<Queue<EventDispatcher.EventRecord>> k_EventQueuePool = new ObjectPool<Queue<EventDispatcher.EventRecord>>(() => new Queue<EventDispatcher.EventRecord>(), 100);

		// Token: 0x040007D4 RID: 2004
		private Queue<EventDispatcher.EventRecord> m_Queue;

		// Token: 0x040007D6 RID: 2006
		private uint m_GateCount;

		// Token: 0x040007D7 RID: 2007
		private uint m_GateDepth = 0U;

		// Token: 0x040007D8 RID: 2008
		internal const int k_MaxGateDepth = 500;

		// Token: 0x040007D9 RID: 2009
		internal const int k_NumberOfEventsWithStackInfo = 10;

		// Token: 0x040007DA RID: 2010
		internal const int k_NumberOfEventsWithEventInfo = 100;

		// Token: 0x040007DB RID: 2011
		private int m_DispatchStackFrame = 0;

		// Token: 0x040007DC RID: 2012
		private EventBase m_CurrentEvent;

		// Token: 0x040007DD RID: 2013
		private Stack<EventDispatcher.DispatchContext> m_DispatchContexts = new Stack<EventDispatcher.DispatchContext>();

		// Token: 0x040007DE RID: 2014
		private bool m_Immediate = false;

		// Token: 0x020001AE RID: 430
		private struct EventRecord
		{
			// Token: 0x040007E0 RID: 2016
			public EventBase m_Event;

			// Token: 0x040007E1 RID: 2017
			public BaseVisualElementPanel m_Panel;
		}

		// Token: 0x020001AF RID: 431
		private struct DispatchContext
		{
			// Token: 0x040007E2 RID: 2018
			public uint m_GateCount;

			// Token: 0x040007E3 RID: 2019
			public Queue<EventDispatcher.EventRecord> m_Queue;
		}
	}
}
