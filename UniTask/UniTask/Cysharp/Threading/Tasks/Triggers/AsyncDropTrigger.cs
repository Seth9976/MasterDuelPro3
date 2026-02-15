using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000211 RID: 529
	[DisallowMultipleComponent]
	public sealed class AsyncDropTrigger : AsyncTriggerBase<PointerEventData>, IDropHandler, IEventSystemHandler
	{
		// Token: 0x06000C34 RID: 3124 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnDropHandler GetOnDropAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnDropHandler GetOnDropAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002B8F6 File Offset: 0x00029AF6
		public UniTask<PointerEventData> OnDropAsync()
		{
			return ((IAsyncOnDropHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnDropAsync();
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002B904 File Offset: 0x00029B04
		public UniTask<PointerEventData> OnDropAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDropHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnDropAsync();
		}
	}
}
