using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001F5 RID: 501
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerEnterTrigger : AsyncTriggerBase<Collider>
	{
		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002B6D0 File Offset: 0x000298D0
		private void OnTriggerEnter(Collider other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002B6D9 File Offset: 0x000298D9
		public IAsyncOnTriggerEnterHandler GetOnTriggerEnterAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider>(this, false);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002B6E2 File Offset: 0x000298E2
		public IAsyncOnTriggerEnterHandler GetOnTriggerEnterAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider>(this, cancellationToken, false);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002B6EC File Offset: 0x000298EC
		public UniTask<Collider> OnTriggerEnterAsync()
		{
			return ((IAsyncOnTriggerEnterHandler)new AsyncTriggerHandler<Collider>(this, true)).OnTriggerEnterAsync();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002B6FA File Offset: 0x000298FA
		public UniTask<Collider> OnTriggerEnterAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerEnterHandler)new AsyncTriggerHandler<Collider>(this, cancellationToken, true)).OnTriggerEnterAsync();
		}
	}
}
