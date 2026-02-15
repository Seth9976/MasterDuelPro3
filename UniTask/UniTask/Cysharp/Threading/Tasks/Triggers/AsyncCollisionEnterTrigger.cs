using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001AF RID: 431
	[DisallowMultipleComponent]
	public sealed class AsyncCollisionEnterTrigger : AsyncTriggerBase<Collision>
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x0002B1B3 File Offset: 0x000293B3
		private void OnCollisionEnter(Collision coll)
		{
			base.RaiseEvent(coll);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002B1BC File Offset: 0x000293BC
		public IAsyncOnCollisionEnterHandler GetOnCollisionEnterAsyncHandler()
		{
			return new AsyncTriggerHandler<Collision>(this, false);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002B1C5 File Offset: 0x000293C5
		public IAsyncOnCollisionEnterHandler GetOnCollisionEnterAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collision>(this, cancellationToken, false);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002B1CF File Offset: 0x000293CF
		public UniTask<Collision> OnCollisionEnterAsync()
		{
			return ((IAsyncOnCollisionEnterHandler)new AsyncTriggerHandler<Collision>(this, true)).OnCollisionEnterAsync();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002B1DD File Offset: 0x000293DD
		public UniTask<Collision> OnCollisionEnterAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCollisionEnterHandler)new AsyncTriggerHandler<Collision>(this, cancellationToken, true)).OnCollisionEnterAsync();
		}
	}
}
