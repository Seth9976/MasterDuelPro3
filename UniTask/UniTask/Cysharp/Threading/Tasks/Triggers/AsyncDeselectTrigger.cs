using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200020D RID: 525
	[DisallowMultipleComponent]
	public sealed class AsyncDeselectTrigger : AsyncTriggerBase<BaseEventData>, IDeselectHandler, IEventSystemHandler
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x0002B87B File Offset: 0x00029A7B
		void IDeselectHandler.OnDeselect(BaseEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002B884 File Offset: 0x00029A84
		public IAsyncOnDeselectHandler GetOnDeselectAsyncHandler()
		{
			return new AsyncTriggerHandler<BaseEventData>(this, false);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002B88D File Offset: 0x00029A8D
		public IAsyncOnDeselectHandler GetOnDeselectAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public UniTask<BaseEventData> OnDeselectAsync()
		{
			return ((IAsyncOnDeselectHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnDeselectAsync();
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002B8CA File Offset: 0x00029ACA
		public UniTask<BaseEventData> OnDeselectAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDeselectHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnDeselectAsync();
		}
	}
}
