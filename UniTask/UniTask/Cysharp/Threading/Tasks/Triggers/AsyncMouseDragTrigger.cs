using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001CD RID: 461
	[DisallowMultipleComponent]
	public sealed class AsyncMouseDragTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnMouseDrag()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnMouseDragHandler GetOnMouseDragAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnMouseDragHandler GetOnMouseDragAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002B41A File Offset: 0x0002961A
		public UniTask OnMouseDragAsync()
		{
			return ((IAsyncOnMouseDragHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseDragAsync();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002B428 File Offset: 0x00029628
		public UniTask OnMouseDragAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseDragHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseDragAsync();
		}
	}
}
