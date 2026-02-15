using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001ED RID: 493
	[DisallowMultipleComponent]
	public sealed class AsyncRenderObjectTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnRenderObject()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnRenderObjectHandler GetOnRenderObjectAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnRenderObjectHandler GetOnRenderObjectAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002B65C File Offset: 0x0002985C
		public UniTask OnRenderObjectAsync()
		{
			return ((IAsyncOnRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnRenderObjectAsync();
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002B66A File Offset: 0x0002986A
		public UniTask OnRenderObjectAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnRenderObjectAsync();
		}
	}
}
