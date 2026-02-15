using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001BB RID: 443
	[DisallowMultipleComponent]
	public sealed class AsyncControllerColliderHitTrigger : AsyncTriggerBase<ControllerColliderHit>
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x0002B2A9 File Offset: 0x000294A9
		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			base.RaiseEvent(hit);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002B2B2 File Offset: 0x000294B2
		public IAsyncOnControllerColliderHitHandler GetOnControllerColliderHitAsyncHandler()
		{
			return new AsyncTriggerHandler<ControllerColliderHit>(this, false);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002B2BB File Offset: 0x000294BB
		public IAsyncOnControllerColliderHitHandler GetOnControllerColliderHitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<ControllerColliderHit>(this, cancellationToken, false);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002B2C5 File Offset: 0x000294C5
		public UniTask<ControllerColliderHit> OnControllerColliderHitAsync()
		{
			return ((IAsyncOnControllerColliderHitHandler)new AsyncTriggerHandler<ControllerColliderHit>(this, true)).OnControllerColliderHitAsync();
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002B2D3 File Offset: 0x000294D3
		public UniTask<ControllerColliderHit> OnControllerColliderHitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnControllerColliderHitHandler)new AsyncTriggerHandler<ControllerColliderHit>(this, cancellationToken, true)).OnControllerColliderHitAsync();
		}
	}
}
