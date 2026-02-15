using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200021F RID: 543
	[DisallowMultipleComponent]
	public sealed class AsyncPointerExitTrigger : AsyncTriggerBase<PointerEventData>, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnPointerExitHandler GetOnPointerExitAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnPointerExitHandler GetOnPointerExitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002B9E5 File Offset: 0x00029BE5
		public UniTask<PointerEventData> OnPointerExitAsync()
		{
			return ((IAsyncOnPointerExitHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerExitAsync();
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002B9F3 File Offset: 0x00029BF3
		public UniTask<PointerEventData> OnPointerExitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerExitHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerExitAsync();
		}
	}
}
