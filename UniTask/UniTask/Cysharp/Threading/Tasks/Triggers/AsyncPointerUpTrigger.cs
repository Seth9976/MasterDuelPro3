using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000221 RID: 545
	[DisallowMultipleComponent]
	public sealed class AsyncPointerUpTrigger : AsyncTriggerBase<PointerEventData>, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x06000C6C RID: 3180 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnPointerUpHandler GetOnPointerUpAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnPointerUpHandler GetOnPointerUpAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002BA02 File Offset: 0x00029C02
		public UniTask<PointerEventData> OnPointerUpAsync()
		{
			return ((IAsyncOnPointerUpHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerUpAsync();
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002BA10 File Offset: 0x00029C10
		public UniTask<PointerEventData> OnPointerUpAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerUpHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerUpAsync();
		}
	}
}
