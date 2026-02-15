using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001E9 RID: 489
	[DisallowMultipleComponent]
	public sealed class AsyncRectTransformRemovedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnRectTransformRemoved()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnRectTransformRemovedHandler GetOnRectTransformRemovedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnRectTransformRemovedHandler GetOnRectTransformRemovedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002B5F8 File Offset: 0x000297F8
		public UniTask OnRectTransformRemovedAsync()
		{
			return ((IAsyncOnRectTransformRemovedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnRectTransformRemovedAsync();
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002B606 File Offset: 0x00029806
		public UniTask OnRectTransformRemovedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnRectTransformRemovedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnRectTransformRemovedAsync();
		}
	}
}
