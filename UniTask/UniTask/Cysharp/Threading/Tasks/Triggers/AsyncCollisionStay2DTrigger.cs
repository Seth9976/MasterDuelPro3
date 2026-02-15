using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B9 RID: 441
	[DisallowMultipleComponent]
	public sealed class AsyncCollisionStay2DTrigger : AsyncTriggerBase<Collision2D>
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x0002B1F4 File Offset: 0x000293F4
		private void OnCollisionStay2D(Collision2D coll)
		{
			base.RaiseEvent(coll);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002B1FD File Offset: 0x000293FD
		public IAsyncOnCollisionStay2DHandler GetOnCollisionStay2DAsyncHandler()
		{
			return new AsyncTriggerHandler<Collision2D>(this, false);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002B206 File Offset: 0x00029406
		public IAsyncOnCollisionStay2DHandler GetOnCollisionStay2DAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collision2D>(this, cancellationToken, false);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002B28C File Offset: 0x0002948C
		public UniTask<Collision2D> OnCollisionStay2DAsync()
		{
			return ((IAsyncOnCollisionStay2DHandler)new AsyncTriggerHandler<Collision2D>(this, true)).OnCollisionStay2DAsync();
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002B29A File Offset: 0x0002949A
		public UniTask<Collision2D> OnCollisionStay2DAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCollisionStay2DHandler)new AsyncTriggerHandler<Collision2D>(this, cancellationToken, true)).OnCollisionStay2DAsync();
		}
	}
}
