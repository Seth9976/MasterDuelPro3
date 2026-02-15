using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200020B RID: 523
	[DisallowMultipleComponent]
	public sealed class AsyncCancelTrigger : AsyncTriggerBase<BaseEventData>, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0002B87B File Offset: 0x00029A7B
		void ICancelHandler.OnCancel(BaseEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002B884 File Offset: 0x00029A84
		public IAsyncOnCancelHandler GetOnCancelAsyncHandler()
		{
			return new AsyncTriggerHandler<BaseEventData>(this, false);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002B88D File Offset: 0x00029A8D
		public IAsyncOnCancelHandler GetOnCancelAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002B897 File Offset: 0x00029A97
		public UniTask<BaseEventData> OnCancelAsync()
		{
			return ((IAsyncOnCancelHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnCancelAsync();
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002B8A5 File Offset: 0x00029AA5
		public UniTask<BaseEventData> OnCancelAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCancelHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnCancelAsync();
		}
	}
}
