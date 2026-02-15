using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000209 RID: 521
	[DisallowMultipleComponent]
	public sealed class AsyncBeginDragTrigger : AsyncTriggerBase<PointerEventData>, IBeginDragHandler, IEventSystemHandler
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnBeginDragHandler GetOnBeginDragAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnBeginDragHandler GetOnBeginDragAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002B856 File Offset: 0x00029A56
		public UniTask<PointerEventData> OnBeginDragAsync()
		{
			return ((IAsyncOnBeginDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnBeginDragAsync();
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002B864 File Offset: 0x00029A64
		public UniTask<PointerEventData> OnBeginDragAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnBeginDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnBeginDragAsync();
		}
	}
}
