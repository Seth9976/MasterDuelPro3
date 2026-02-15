using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001F9 RID: 505
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerExitTrigger : AsyncTriggerBase<Collider>
	{
		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002B6D0 File Offset: 0x000298D0
		private void OnTriggerExit(Collider other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002B6D9 File Offset: 0x000298D9
		public IAsyncOnTriggerExitHandler GetOnTriggerExitAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider>(this, false);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002B6E2 File Offset: 0x000298E2
		public IAsyncOnTriggerExitHandler GetOnTriggerExitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider>(this, cancellationToken, false);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002B752 File Offset: 0x00029952
		public UniTask<Collider> OnTriggerExitAsync()
		{
			return ((IAsyncOnTriggerExitHandler)new AsyncTriggerHandler<Collider>(this, true)).OnTriggerExitAsync();
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002B760 File Offset: 0x00029960
		public UniTask<Collider> OnTriggerExitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerExitHandler)new AsyncTriggerHandler<Collider>(this, cancellationToken, true)).OnTriggerExitAsync();
		}
	}
}
