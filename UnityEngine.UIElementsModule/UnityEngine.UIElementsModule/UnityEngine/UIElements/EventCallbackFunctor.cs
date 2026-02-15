using System;
using UnityEngine.Pool;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CB RID: 459
	internal class EventCallbackFunctor<TEventType> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x0003D0C0 File Offset: 0x0003B2C0
		public static EventCallbackFunctor<TEventType> GetPooled(long eventTypeId, EventCallback<TEventType> callback, InvokePolicy invokePolicy = InvokePolicy.Default)
		{
			EventCallbackFunctor<TEventType> self = GenericPool<EventCallbackFunctor<TEventType>>.Get();
			self.eventTypeId = eventTypeId;
			self.invokePolicy = invokePolicy;
			self.m_Callback = callback;
			return self;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0003D0EE File Offset: 0x0003B2EE
		public override void Dispose()
		{
			this.eventTypeId = 0L;
			this.invokePolicy = InvokePolicy.Default;
			this.m_Callback = null;
			GenericPool<EventCallbackFunctor<TEventType>>.Release(this);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0003D110 File Offset: 0x0003B310
		public override void Invoke(EventBase evt)
		{
			using (new EventDebuggerLogCall(this.m_Callback, evt))
			{
				this.m_Callback(evt as TEventType);
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0003D168 File Offset: 0x0003B368
		public override void UnregisterCallback(CallbackEventHandler target, TrickleDown useTrickleDown)
		{
			target.UnregisterCallback<TEventType>(this.m_Callback, useTrickleDown);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0003D17C File Offset: 0x0003B37C
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback)
		{
			return this.eventTypeId == eventTypeId && this.m_Callback == callback;
		}

		// Token: 0x0400081B RID: 2075
		private EventCallback<TEventType> m_Callback;
	}
}
