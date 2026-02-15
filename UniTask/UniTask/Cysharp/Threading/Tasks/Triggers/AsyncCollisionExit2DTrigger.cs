using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B5 RID: 437
	[DisallowMultipleComponent]
	public sealed class AsyncCollisionExit2DTrigger : AsyncTriggerBase<Collision2D>
	{
		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002B1F4 File Offset: 0x000293F4
		private void OnCollisionExit2D(Collision2D coll)
		{
			base.RaiseEvent(coll);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002B1FD File Offset: 0x000293FD
		public IAsyncOnCollisionExit2DHandler GetOnCollisionExit2DAsyncHandler()
		{
			return new AsyncTriggerHandler<Collision2D>(this, false);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002B206 File Offset: 0x00029406
		public IAsyncOnCollisionExit2DHandler GetOnCollisionExit2DAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collision2D>(this, cancellationToken, false);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002B252 File Offset: 0x00029452
		public UniTask<Collision2D> OnCollisionExit2DAsync()
		{
			return ((IAsyncOnCollisionExit2DHandler)new AsyncTriggerHandler<Collision2D>(this, true)).OnCollisionExit2DAsync();
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002B260 File Offset: 0x00029460
		public UniTask<Collision2D> OnCollisionExit2DAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCollisionExit2DHandler)new AsyncTriggerHandler<Collision2D>(this, cancellationToken, true)).OnCollisionExit2DAsync();
		}
	}
}
