using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000199 RID: 409
	[DisallowMultipleComponent]
	public sealed class AsyncLateUpdateTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void LateUpdate()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncLateUpdateHandler GetLateUpdateAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncLateUpdateHandler GetLateUpdateAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002B002 File Offset: 0x00029202
		public UniTask LateUpdateAsync()
		{
			return ((IAsyncLateUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).LateUpdateAsync();
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002B010 File Offset: 0x00029210
		public UniTask LateUpdateAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncLateUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).LateUpdateAsync();
		}
	}
}
