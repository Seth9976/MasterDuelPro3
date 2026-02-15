using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000197 RID: 407
	[DisallowMultipleComponent]
	public sealed class AsyncFixedUpdateTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void FixedUpdate()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncFixedUpdateHandler GetFixedUpdateAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncFixedUpdateHandler GetFixedUpdateAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002AFE5 File Offset: 0x000291E5
		public UniTask FixedUpdateAsync()
		{
			return ((IAsyncFixedUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).FixedUpdateAsync();
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002AFF3 File Offset: 0x000291F3
		public UniTask FixedUpdateAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncFixedUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).FixedUpdateAsync();
		}
	}
}
