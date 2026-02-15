using System;
using UnityEngine.Pool;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CC RID: 460
	internal class EventCallbackFunctor<TEventType, TCallbackArgs> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0003D1AF File Offset: 0x0003B3AF
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x0003D1B7 File Offset: 0x0003B3B7
		internal TCallbackArgs userArgs { get; set; }

		// Token: 0x06000D02 RID: 3330 RVA: 0x0003D1C0 File Offset: 0x0003B3C0
		public static EventCallbackFunctor<TEventType, TCallbackArgs> GetPooled(long eventTypeId, EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, InvokePolicy invokePolicy = InvokePolicy.Default)
		{
			EventCallbackFunctor<TEventType, TCallbackArgs> self = GenericPool<EventCallbackFunctor<TEventType, TCallbackArgs>>.Get();
			self.eventTypeId = eventTypeId;
			self.invokePolicy = invokePolicy;
			self.userArgs = userArgs;
			self.m_Callback = callback;
			return self;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0003D1F8 File Offset: 0x0003B3F8
		public override void Dispose()
		{
			this.eventTypeId = 0L;
			this.invokePolicy = InvokePolicy.Default;
			this.userArgs = default(TCallbackArgs);
			this.m_Callback = null;
			GenericPool<EventCallbackFunctor<TEventType, TCallbackArgs>>.Release(this);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0003D234 File Offset: 0x0003B434
		public override void Invoke(EventBase evt)
		{
			using (new EventDebuggerLogCall(this.m_Callback, evt))
			{
				this.m_Callback(evt as TEventType, this.userArgs);
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003D290 File Offset: 0x0003B490
		public override void UnregisterCallback(CallbackEventHandler target, TrickleDown useTrickleDown)
		{
			target.UnregisterCallback<TEventType, TCallbackArgs>(this.m_Callback, useTrickleDown);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0003D2A4 File Offset: 0x0003B4A4
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback)
		{
			return this.eventTypeId == eventTypeId && this.m_Callback == callback;
		}

		// Token: 0x0400081C RID: 2076
		private EventCallback<TEventType, TCallbackArgs> m_Callback;
	}
}
