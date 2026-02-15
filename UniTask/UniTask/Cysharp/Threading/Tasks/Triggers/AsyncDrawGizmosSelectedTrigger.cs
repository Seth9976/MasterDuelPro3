using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001C1 RID: 449
	[DisallowMultipleComponent]
	public sealed class AsyncDrawGizmosSelectedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B1C RID: 2844 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnDrawGizmosSelected()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnDrawGizmosSelectedHandler GetOnDrawGizmosSelectedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnDrawGizmosSelectedHandler GetOnDrawGizmosSelectedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002B324 File Offset: 0x00029524
		public UniTask OnDrawGizmosSelectedAsync()
		{
			return ((IAsyncOnDrawGizmosSelectedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnDrawGizmosSelectedAsync();
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002B332 File Offset: 0x00029532
		public UniTask OnDrawGizmosSelectedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDrawGizmosSelectedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnDrawGizmosSelectedAsync();
		}
	}
}
