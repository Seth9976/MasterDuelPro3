using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000213 RID: 531
	[DisallowMultipleComponent]
	public sealed class AsyncEndDragTrigger : AsyncTriggerBase<PointerEventData>, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x06000C3B RID: 3131 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnEndDragHandler GetOnEndDragAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnEndDragHandler GetOnEndDragAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002B913 File Offset: 0x00029B13
		public UniTask<PointerEventData> OnEndDragAsync()
		{
			return ((IAsyncOnEndDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnEndDragAsync();
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002B921 File Offset: 0x00029B21
		public UniTask<PointerEventData> OnEndDragAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnEndDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnEndDragAsync();
		}
	}
}
