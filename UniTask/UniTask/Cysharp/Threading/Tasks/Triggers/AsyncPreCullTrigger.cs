using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001E3 RID: 483
	[DisallowMultipleComponent]
	public sealed class AsyncPreCullTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnPreCull()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnPreCullHandler GetOnPreCullAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnPreCullHandler GetOnPreCullAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002B5A1 File Offset: 0x000297A1
		public UniTask OnPreCullAsync()
		{
			return ((IAsyncOnPreCullHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnPreCullAsync();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002B5AF File Offset: 0x000297AF
		public UniTask OnPreCullAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPreCullHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnPreCullAsync();
		}
	}
}
