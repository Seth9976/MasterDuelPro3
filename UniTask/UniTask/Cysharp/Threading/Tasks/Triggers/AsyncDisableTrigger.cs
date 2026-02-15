using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001BD RID: 445
	[DisallowMultipleComponent]
	public sealed class AsyncDisableTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnDisable()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnDisableHandler GetOnDisableAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnDisableHandler GetOnDisableAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002B2EA File Offset: 0x000294EA
		public UniTask OnDisableAsync()
		{
			return ((IAsyncOnDisableHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnDisableAsync();
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002B2F8 File Offset: 0x000294F8
		public UniTask OnDisableAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDisableHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnDisableAsync();
		}
	}
}
