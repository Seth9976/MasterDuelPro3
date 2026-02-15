using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000229 RID: 553
	[DisallowMultipleComponent]
	public sealed class AsyncUpdateSelectedTrigger : AsyncTriggerBase<BaseEventData>, IUpdateSelectedHandler, IEventSystemHandler
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x0002B87B File Offset: 0x00029A7B
		void IUpdateSelectedHandler.OnUpdateSelected(BaseEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002B884 File Offset: 0x00029A84
		public IAsyncOnUpdateSelectedHandler GetOnUpdateSelectedAsyncHandler()
		{
			return new AsyncTriggerHandler<BaseEventData>(this, false);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002B88D File Offset: 0x00029A8D
		public IAsyncOnUpdateSelectedHandler GetOnUpdateSelectedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002BA76 File Offset: 0x00029C76
		public UniTask<BaseEventData> OnUpdateSelectedAsync()
		{
			return ((IAsyncOnUpdateSelectedHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnUpdateSelectedAsync();
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002BA84 File Offset: 0x00029C84
		public UniTask<BaseEventData> OnUpdateSelectedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnUpdateSelectedHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnUpdateSelectedAsync();
		}
	}
}
