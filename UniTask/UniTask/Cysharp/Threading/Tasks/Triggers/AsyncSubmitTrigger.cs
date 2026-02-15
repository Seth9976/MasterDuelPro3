using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000227 RID: 551
	[DisallowMultipleComponent]
	public sealed class AsyncSubmitTrigger : AsyncTriggerBase<BaseEventData>, ISubmitHandler, IEventSystemHandler
	{
		// Token: 0x06000C81 RID: 3201 RVA: 0x0002B87B File Offset: 0x00029A7B
		void ISubmitHandler.OnSubmit(BaseEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002B884 File Offset: 0x00029A84
		public IAsyncOnSubmitHandler GetOnSubmitAsyncHandler()
		{
			return new AsyncTriggerHandler<BaseEventData>(this, false);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002B88D File Offset: 0x00029A8D
		public IAsyncOnSubmitHandler GetOnSubmitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002BA59 File Offset: 0x00029C59
		public UniTask<BaseEventData> OnSubmitAsync()
		{
			return ((IAsyncOnSubmitHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnSubmitAsync();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002BA67 File Offset: 0x00029C67
		public UniTask<BaseEventData> OnSubmitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnSubmitHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnSubmitAsync();
		}
	}
}
