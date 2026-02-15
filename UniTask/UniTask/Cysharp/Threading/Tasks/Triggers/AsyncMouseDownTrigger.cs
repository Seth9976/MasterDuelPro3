using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001CB RID: 459
	[DisallowMultipleComponent]
	public sealed class AsyncMouseDownTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B3F RID: 2879 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnMouseDown()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnMouseDownHandler GetOnMouseDownAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnMouseDownHandler GetOnMouseDownAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002B3FD File Offset: 0x000295FD
		public UniTask OnMouseDownAsync()
		{
			return ((IAsyncOnMouseDownHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseDownAsync();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002B40B File Offset: 0x0002960B
		public UniTask OnMouseDownAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseDownHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseDownAsync();
		}
	}
}
