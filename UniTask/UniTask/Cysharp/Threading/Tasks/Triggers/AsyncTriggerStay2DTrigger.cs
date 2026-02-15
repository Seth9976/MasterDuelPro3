using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001FF RID: 511
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerStay2DTrigger : AsyncTriggerBase<Collider2D>
	{
		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002B711 File Offset: 0x00029911
		private void OnTriggerStay2D(Collider2D other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002B71A File Offset: 0x0002991A
		public IAsyncOnTriggerStay2DHandler GetOnTriggerStay2DAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider2D>(this, false);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002B723 File Offset: 0x00029923
		public IAsyncOnTriggerStay2DHandler GetOnTriggerStay2DAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider2D>(this, cancellationToken, false);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002B7A9 File Offset: 0x000299A9
		public UniTask<Collider2D> OnTriggerStay2DAsync()
		{
			return ((IAsyncOnTriggerStay2DHandler)new AsyncTriggerHandler<Collider2D>(this, true)).OnTriggerStay2DAsync();
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002B7B7 File Offset: 0x000299B7
		public UniTask<Collider2D> OnTriggerStay2DAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerStay2DHandler)new AsyncTriggerHandler<Collider2D>(this, cancellationToken, true)).OnTriggerStay2DAsync();
		}
	}
}
