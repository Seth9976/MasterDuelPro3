using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001D1 RID: 465
	[DisallowMultipleComponent]
	public sealed class AsyncMouseExitTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnMouseExit()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnMouseExitHandler GetOnMouseExitAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnMouseExitHandler GetOnMouseExitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002B454 File Offset: 0x00029654
		public UniTask OnMouseExitAsync()
		{
			return ((IAsyncOnMouseExitHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseExitAsync();
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002B462 File Offset: 0x00029662
		public UniTask OnMouseExitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseExitHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseExitAsync();
		}
	}
}
