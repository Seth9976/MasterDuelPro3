using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000207 RID: 519
	[DisallowMultipleComponent]
	public sealed class AsyncUpdateTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void Update()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncUpdateHandler GetUpdateAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncUpdateHandler GetUpdateAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002B81D File Offset: 0x00029A1D
		public UniTask UpdateAsync()
		{
			return ((IAsyncUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).UpdateAsync();
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002B82B File Offset: 0x00029A2B
		public UniTask UpdateAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).UpdateAsync();
		}
	}
}
