using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200020F RID: 527
	[DisallowMultipleComponent]
	public sealed class AsyncDragTrigger : AsyncTriggerBase<PointerEventData>, IDragHandler, IEventSystemHandler
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnDragHandler GetOnDragAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnDragHandler GetOnDragAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002B8D9 File Offset: 0x00029AD9
		public UniTask<PointerEventData> OnDragAsync()
		{
			return ((IAsyncOnDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnDragAsync();
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002B8E7 File Offset: 0x00029AE7
		public UniTask<PointerEventData> OnDragAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnDragAsync();
		}
	}
}
