using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001AB RID: 427
	[DisallowMultipleComponent]
	public sealed class AsyncBeforeTransformParentChangedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnBeforeTransformParentChanged()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnBeforeTransformParentChangedHandler GetOnBeforeTransformParentChangedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnBeforeTransformParentChangedHandler GetOnBeforeTransformParentChangedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002B179 File Offset: 0x00029379
		public UniTask OnBeforeTransformParentChangedAsync()
		{
			return ((IAsyncOnBeforeTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnBeforeTransformParentChangedAsync();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002B187 File Offset: 0x00029387
		public UniTask OnBeforeTransformParentChangedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnBeforeTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnBeforeTransformParentChangedAsync();
		}
	}
}
