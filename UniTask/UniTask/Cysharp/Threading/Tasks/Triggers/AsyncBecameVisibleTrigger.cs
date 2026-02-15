using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001A9 RID: 425
	[DisallowMultipleComponent]
	public sealed class AsyncBecameVisibleTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnBecameVisible()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnBecameVisibleHandler GetOnBecameVisibleAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnBecameVisibleHandler GetOnBecameVisibleAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002B15C File Offset: 0x0002935C
		public UniTask OnBecameVisibleAsync()
		{
			return ((IAsyncOnBecameVisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnBecameVisibleAsync();
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002B16A File Offset: 0x0002936A
		public UniTask OnBecameVisibleAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnBecameVisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnBecameVisibleAsync();
		}
	}
}
