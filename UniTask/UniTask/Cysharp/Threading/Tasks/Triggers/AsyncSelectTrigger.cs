using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000225 RID: 549
	[DisallowMultipleComponent]
	public sealed class AsyncSelectTrigger : AsyncTriggerBase<BaseEventData>, ISelectHandler, IEventSystemHandler
	{
		// Token: 0x06000C7A RID: 3194 RVA: 0x0002B87B File Offset: 0x00029A7B
		void ISelectHandler.OnSelect(BaseEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002B884 File Offset: 0x00029A84
		public IAsyncOnSelectHandler GetOnSelectAsyncHandler()
		{
			return new AsyncTriggerHandler<BaseEventData>(this, false);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002B88D File Offset: 0x00029A8D
		public IAsyncOnSelectHandler GetOnSelectAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002BA3C File Offset: 0x00029C3C
		public UniTask<BaseEventData> OnSelectAsync()
		{
			return ((IAsyncOnSelectHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnSelectAsync();
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002BA4A File Offset: 0x00029C4A
		public UniTask<BaseEventData> OnSelectAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnSelectHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnSelectAsync();
		}
	}
}
