using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200019D RID: 413
	[DisallowMultipleComponent]
	public sealed class AsyncAnimatorMoveTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnAnimatorMove()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnAnimatorMoveHandler GetOnAnimatorMoveAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnAnimatorMoveHandler GetOnAnimatorMoveAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002B060 File Offset: 0x00029260
		public UniTask OnAnimatorMoveAsync()
		{
			return ((IAsyncOnAnimatorMoveHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnAnimatorMoveAsync();
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002B06E File Offset: 0x0002926E
		public UniTask OnAnimatorMoveAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnAnimatorMoveHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnAnimatorMoveAsync();
		}
	}
}
