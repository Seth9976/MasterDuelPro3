using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001D9 RID: 473
	[DisallowMultipleComponent]
	public sealed class AsyncParticleCollisionTrigger : AsyncTriggerBase<GameObject>
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x0002B4C8 File Offset: 0x000296C8
		private void OnParticleCollision(GameObject other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002B4D1 File Offset: 0x000296D1
		public IAsyncOnParticleCollisionHandler GetOnParticleCollisionAsyncHandler()
		{
			return new AsyncTriggerHandler<GameObject>(this, false);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002B4DA File Offset: 0x000296DA
		public IAsyncOnParticleCollisionHandler GetOnParticleCollisionAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<GameObject>(this, cancellationToken, false);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002B4E4 File Offset: 0x000296E4
		public UniTask<GameObject> OnParticleCollisionAsync()
		{
			return ((IAsyncOnParticleCollisionHandler)new AsyncTriggerHandler<GameObject>(this, true)).OnParticleCollisionAsync();
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002B4F2 File Offset: 0x000296F2
		public UniTask<GameObject> OnParticleCollisionAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnParticleCollisionHandler)new AsyncTriggerHandler<GameObject>(this, cancellationToken, true)).OnParticleCollisionAsync();
		}
	}
}
