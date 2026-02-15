using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001EF RID: 495
	[DisallowMultipleComponent]
	public sealed class AsyncServerInitializedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000BBD RID: 3005 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnServerInitialized()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnServerInitializedHandler GetOnServerInitializedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnServerInitializedHandler GetOnServerInitializedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002B679 File Offset: 0x00029879
		public UniTask OnServerInitializedAsync()
		{
			return ((IAsyncOnServerInitializedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnServerInitializedAsync();
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002B687 File Offset: 0x00029887
		public UniTask OnServerInitializedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnServerInitializedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnServerInitializedAsync();
		}
	}
}
