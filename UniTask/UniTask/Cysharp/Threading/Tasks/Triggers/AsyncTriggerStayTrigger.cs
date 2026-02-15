using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001FD RID: 509
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerStayTrigger : AsyncTriggerBase<Collider>
	{
		// Token: 0x06000BEE RID: 3054 RVA: 0x0002B6D0 File Offset: 0x000298D0
		private void OnTriggerStay(Collider other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002B6D9 File Offset: 0x000298D9
		public IAsyncOnTriggerStayHandler GetOnTriggerStayAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider>(this, false);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002B6E2 File Offset: 0x000298E2
		public IAsyncOnTriggerStayHandler GetOnTriggerStayAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider>(this, cancellationToken, false);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002B78C File Offset: 0x0002998C
		public UniTask<Collider> OnTriggerStayAsync()
		{
			return ((IAsyncOnTriggerStayHandler)new AsyncTriggerHandler<Collider>(this, true)).OnTriggerStayAsync();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002B79A File Offset: 0x0002999A
		public UniTask<Collider> OnTriggerStayAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerStayHandler)new AsyncTriggerHandler<Collider>(this, cancellationToken, true)).OnTriggerStayAsync();
		}
	}
}
