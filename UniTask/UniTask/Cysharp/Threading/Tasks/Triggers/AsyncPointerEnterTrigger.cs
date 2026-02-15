using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200021D RID: 541
	[DisallowMultipleComponent]
	public sealed class AsyncPointerEnterTrigger : AsyncTriggerBase<PointerEventData>, IPointerEnterHandler, IEventSystemHandler
	{
		// Token: 0x06000C5E RID: 3166 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnPointerEnterHandler GetOnPointerEnterAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnPointerEnterHandler GetOnPointerEnterAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002B9C8 File Offset: 0x00029BC8
		public UniTask<PointerEventData> OnPointerEnterAsync()
		{
			return ((IAsyncOnPointerEnterHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerEnterAsync();
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002B9D6 File Offset: 0x00029BD6
		public UniTask<PointerEventData> OnPointerEnterAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerEnterHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerEnterAsync();
		}
	}
}
