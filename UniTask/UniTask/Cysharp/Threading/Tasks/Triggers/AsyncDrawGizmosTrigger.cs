using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001BF RID: 447
	[DisallowMultipleComponent]
	public sealed class AsyncDrawGizmosTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B15 RID: 2837 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnDrawGizmos()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnDrawGizmosHandler GetOnDrawGizmosAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnDrawGizmosHandler GetOnDrawGizmosAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002B307 File Offset: 0x00029507
		public UniTask OnDrawGizmosAsync()
		{
			return ((IAsyncOnDrawGizmosHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnDrawGizmosAsync();
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002B315 File Offset: 0x00029515
		public UniTask OnDrawGizmosAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDrawGizmosHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnDrawGizmosAsync();
		}
	}
}
