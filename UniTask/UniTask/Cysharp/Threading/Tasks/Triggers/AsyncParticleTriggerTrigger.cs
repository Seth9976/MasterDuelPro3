using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001DD RID: 477
	[DisallowMultipleComponent]
	public sealed class AsyncParticleTriggerTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x0002AFC5 File Offset: 0x000291C5
		private void OnParticleTrigger()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public IAsyncOnParticleTriggerHandler GetOnParticleTriggerAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002AFDB File Offset: 0x000291DB
		public IAsyncOnParticleTriggerHandler GetOnParticleTriggerAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002B526 File Offset: 0x00029726
		public UniTask OnParticleTriggerAsync()
		{
			return ((IAsyncOnParticleTriggerHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnParticleTriggerAsync();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002B534 File Offset: 0x00029734
		public UniTask OnParticleTriggerAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnParticleTriggerHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnParticleTriggerAsync();
		}
	}
}
