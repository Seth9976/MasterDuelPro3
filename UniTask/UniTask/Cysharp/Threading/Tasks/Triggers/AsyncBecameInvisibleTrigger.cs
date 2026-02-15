using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001A7 RID: 423
	[DisallowMultipleComponent]
	public sealed class AsyncBecameInvisibleTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnBecameInvisible()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnBecameInvisibleHandler GetOnBecameInvisibleAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnBecameInvisibleHandler GetOnBecameInvisibleAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002B13F File Offset: 0x0002933F
		public UniTask OnBecameInvisibleAsync()
		{
			return ((IAsyncOnBecameInvisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnBecameInvisibleAsync();
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002B14D File Offset: 0x0002934D
		public UniTask OnBecameInvisibleAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnBecameInvisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnBecameInvisibleAsync();
		}
	}
}
