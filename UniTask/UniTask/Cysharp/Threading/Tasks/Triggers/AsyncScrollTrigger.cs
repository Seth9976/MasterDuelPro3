using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000223 RID: 547
	[DisallowMultipleComponent]
	public sealed class AsyncScrollTrigger : AsyncTriggerBase<PointerEventData>, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x06000C73 RID: 3187 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IScrollHandler.OnScroll(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnScrollHandler GetOnScrollAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnScrollHandler GetOnScrollAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002BA1F File Offset: 0x00029C1F
		public UniTask<PointerEventData> OnScrollAsync()
		{
			return ((IAsyncOnScrollHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnScrollAsync();
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002BA2D File Offset: 0x00029C2D
		public UniTask<PointerEventData> OnScrollAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnScrollHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnScrollAsync();
		}
	}
}
