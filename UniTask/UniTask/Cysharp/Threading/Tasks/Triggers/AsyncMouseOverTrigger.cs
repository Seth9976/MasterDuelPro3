using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001D3 RID: 467
	[DisallowMultipleComponent]
	public sealed class AsyncMouseOverTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B5B RID: 2907 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnMouseOver()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnMouseOverHandler GetOnMouseOverAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnMouseOverHandler GetOnMouseOverAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002B471 File Offset: 0x00029671
		public UniTask OnMouseOverAsync()
		{
			return ((IAsyncOnMouseOverHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseOverAsync();
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002B47F File Offset: 0x0002967F
		public UniTask OnMouseOverAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseOverHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseOverAsync();
		}
	}
}
