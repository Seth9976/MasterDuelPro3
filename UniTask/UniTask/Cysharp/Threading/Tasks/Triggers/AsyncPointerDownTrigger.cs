using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200021B RID: 539
	[DisallowMultipleComponent]
	public sealed class AsyncPointerDownTrigger : AsyncTriggerBase<PointerEventData>, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnPointerDownHandler GetOnPointerDownAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnPointerDownHandler GetOnPointerDownAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002B9AB File Offset: 0x00029BAB
		public UniTask<PointerEventData> OnPointerDownAsync()
		{
			return ((IAsyncOnPointerDownHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerDownAsync();
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002B9B9 File Offset: 0x00029BB9
		public UniTask<PointerEventData> OnPointerDownAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerDownHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerDownAsync();
		}
	}
}
