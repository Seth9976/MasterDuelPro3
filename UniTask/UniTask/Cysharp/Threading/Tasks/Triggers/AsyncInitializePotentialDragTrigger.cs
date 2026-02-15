using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000215 RID: 533
	[DisallowMultipleComponent]
	public sealed class AsyncInitializePotentialDragTrigger : AsyncTriggerBase<PointerEventData>, IInitializePotentialDragHandler, IEventSystemHandler
	{
		// Token: 0x06000C42 RID: 3138 RVA: 0x0002B83A File Offset: 0x00029A3A
		void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002B843 File Offset: 0x00029A43
		public IAsyncOnInitializePotentialDragHandler GetOnInitializePotentialDragAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002B84C File Offset: 0x00029A4C
		public IAsyncOnInitializePotentialDragHandler GetOnInitializePotentialDragAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002B930 File Offset: 0x00029B30
		public UniTask<PointerEventData> OnInitializePotentialDragAsync()
		{
			return ((IAsyncOnInitializePotentialDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnInitializePotentialDragAsync();
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002B93E File Offset: 0x00029B3E
		public UniTask<PointerEventData> OnInitializePotentialDragAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnInitializePotentialDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnInitializePotentialDragAsync();
		}
	}
}
