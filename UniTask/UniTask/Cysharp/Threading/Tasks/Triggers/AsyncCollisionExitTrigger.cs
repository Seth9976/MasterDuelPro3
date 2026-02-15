using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001B3 RID: 435
	[DisallowMultipleComponent]
	public sealed class AsyncCollisionExitTrigger : AsyncTriggerBase<Collision>
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x0002B1B3 File Offset: 0x000293B3
		private void OnCollisionExit(Collision coll)
		{
			base.RaiseEvent(coll);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002B1BC File Offset: 0x000293BC
		public IAsyncOnCollisionExitHandler GetOnCollisionExitAsyncHandler()
		{
			return new AsyncTriggerHandler<Collision>(this, false);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002B1C5 File Offset: 0x000293C5
		public IAsyncOnCollisionExitHandler GetOnCollisionExitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collision>(this, cancellationToken, false);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002B235 File Offset: 0x00029435
		public UniTask<Collision> OnCollisionExitAsync()
		{
			return ((IAsyncOnCollisionExitHandler)new AsyncTriggerHandler<Collision>(this, true)).OnCollisionExitAsync();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002B243 File Offset: 0x00029443
		public UniTask<Collision> OnCollisionExitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCollisionExitHandler)new AsyncTriggerHandler<Collision>(this, cancellationToken, true)).OnCollisionExitAsync();
		}
	}
}
