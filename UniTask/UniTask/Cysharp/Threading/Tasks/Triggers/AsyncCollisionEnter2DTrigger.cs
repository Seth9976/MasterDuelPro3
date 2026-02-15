using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B1 RID: 433
	[DisallowMultipleComponent]
	public sealed class AsyncCollisionEnter2DTrigger : AsyncTriggerBase<Collision2D>
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002B1F4 File Offset: 0x000293F4
		private void OnCollisionEnter2D(Collision2D coll)
		{
			base.RaiseEvent(coll);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002B1FD File Offset: 0x000293FD
		public IAsyncOnCollisionEnter2DHandler GetOnCollisionEnter2DAsyncHandler()
		{
			return new AsyncTriggerHandler<Collision2D>(this, false);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002B206 File Offset: 0x00029406
		public IAsyncOnCollisionEnter2DHandler GetOnCollisionEnter2DAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collision2D>(this, cancellationToken, false);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002B210 File Offset: 0x00029410
		public UniTask<Collision2D> OnCollisionEnter2DAsync()
		{
			return ((IAsyncOnCollisionEnter2DHandler)new AsyncTriggerHandler<Collision2D>(this, true)).OnCollisionEnter2DAsync();
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002B21E File Offset: 0x0002941E
		public UniTask<Collision2D> OnCollisionEnter2DAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCollisionEnter2DHandler)new AsyncTriggerHandler<Collision2D>(this, cancellationToken, true)).OnCollisionEnter2DAsync();
		}
	}
}
