using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001FB RID: 507
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerExit2DTrigger : AsyncTriggerBase<Collider2D>
	{
		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002B711 File Offset: 0x00029911
		private void OnTriggerExit2D(Collider2D other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002B71A File Offset: 0x0002991A
		public IAsyncOnTriggerExit2DHandler GetOnTriggerExit2DAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider2D>(this, false);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002B723 File Offset: 0x00029923
		public IAsyncOnTriggerExit2DHandler GetOnTriggerExit2DAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider2D>(this, cancellationToken, false);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002B76F File Offset: 0x0002996F
		public UniTask<Collider2D> OnTriggerExit2DAsync()
		{
			return ((IAsyncOnTriggerExit2DHandler)new AsyncTriggerHandler<Collider2D>(this, true)).OnTriggerExit2DAsync();
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002B77D File Offset: 0x0002997D
		public UniTask<Collider2D> OnTriggerExit2DAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerExit2DHandler)new AsyncTriggerHandler<Collider2D>(this, cancellationToken, true)).OnTriggerExit2DAsync();
		}
	}
}
