using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001DB RID: 475
	[DisallowMultipleComponent]
	public sealed class AsyncParticleSystemStoppedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnParticleSystemStopped()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnParticleSystemStoppedHandler GetOnParticleSystemStoppedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnParticleSystemStoppedHandler GetOnParticleSystemStoppedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002B509 File Offset: 0x00029709
		public UniTask OnParticleSystemStoppedAsync()
		{
			return ((IAsyncOnParticleSystemStoppedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnParticleSystemStoppedAsync();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002B517 File Offset: 0x00029717
		public UniTask OnParticleSystemStoppedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnParticleSystemStoppedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnParticleSystemStoppedAsync();
		}
	}
}
