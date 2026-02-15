using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001C3 RID: 451
	[DisallowMultipleComponent]
	public sealed class AsyncEnableTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnEnable()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnEnableHandler GetOnEnableAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnEnableHandler GetOnEnableAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002B341 File Offset: 0x00029541
		public UniTask OnEnableAsync()
		{
			return ((IAsyncOnEnableHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnEnableAsync();
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002B34F File Offset: 0x0002954F
		public UniTask OnEnableAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnEnableHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnEnableAsync();
		}
	}
}
