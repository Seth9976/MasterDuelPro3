using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CA RID: 458
	internal abstract class EventCallbackFunctorBase : IDisposable
	{
		// Token: 0x06000CF5 RID: 3317
		public abstract void Invoke(EventBase evt);

		// Token: 0x06000CF6 RID: 3318
		public abstract void UnregisterCallback(CallbackEventHandler target, TrickleDown useTrickleDown);

		// Token: 0x06000CF7 RID: 3319
		public abstract void Dispose();

		// Token: 0x06000CF8 RID: 3320
		public abstract bool IsEquivalentTo(long eventTypeId, Delegate callback);

		// Token: 0x04000819 RID: 2073
		public long eventTypeId;

		// Token: 0x0400081A RID: 2074
		public InvokePolicy invokePolicy;
	}
}
