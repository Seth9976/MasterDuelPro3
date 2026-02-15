using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000219 RID: 537
	[DisallowMultipleComponent]
	public sealed class AsyncPointerClickTrigger : AsyncTriggerBase<PointerEventData>, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnPointerClickHandler GetOnPointerClickAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnPointerClickHandler GetOnPointerClickAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002B98E File Offset: 0x00029B8E
		public UniTask<PointerEventData> OnPointerClickAsync()
		{
			return ((IAsyncOnPointerClickHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerClickAsync();
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002B99C File Offset: 0x00029B9C
		public UniTask<PointerEventData> OnPointerClickAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerClickHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerClickAsync();
		}
	}
}
