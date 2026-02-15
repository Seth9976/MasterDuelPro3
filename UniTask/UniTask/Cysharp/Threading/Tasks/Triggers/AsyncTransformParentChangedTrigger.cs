using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001F3 RID: 499
	[DisallowMultipleComponent]
	public sealed class AsyncTransformParentChangedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnTransformParentChanged()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnTransformParentChangedHandler GetOnTransformParentChangedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnTransformParentChangedHandler GetOnTransformParentChangedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002B6B3 File Offset: 0x000298B3
		public UniTask OnTransformParentChangedAsync()
		{
			return ((IAsyncOnTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnTransformParentChangedAsync();
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002B6C1 File Offset: 0x000298C1
		public UniTask OnTransformParentChangedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnTransformParentChangedAsync();
		}
	}
}
