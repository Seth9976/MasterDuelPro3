using System;
using System.Threading;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001DF RID: 479
	[DisallowMultipleComponent]
	public sealed class AsyncParticleUpdateJobScheduledTrigger : AsyncTriggerBase<ParticleSystemJobData>
	{
		// Token: 0x06000B85 RID: 2949 RVA: 0x0002B543 File Offset: 0x00029743
		private void OnParticleUpdateJobScheduled(ParticleSystemJobData particles)
		{
			base.RaiseEvent(particles);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002B54C File Offset: 0x0002974C
		public IAsyncOnParticleUpdateJobScheduledHandler GetOnParticleUpdateJobScheduledAsyncHandler()
		{
			return new AsyncTriggerHandler<ParticleSystemJobData>(this, false);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002B555 File Offset: 0x00029755
		public IAsyncOnParticleUpdateJobScheduledHandler GetOnParticleUpdateJobScheduledAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<ParticleSystemJobData>(this, cancellationToken, false);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002B55F File Offset: 0x0002975F
		public UniTask<ParticleSystemJobData> OnParticleUpdateJobScheduledAsync()
		{
			return ((IAsyncOnParticleUpdateJobScheduledHandler)new AsyncTriggerHandler<ParticleSystemJobData>(this, true)).OnParticleUpdateJobScheduledAsync();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002B56D File Offset: 0x0002976D
		public UniTask<ParticleSystemJobData> OnParticleUpdateJobScheduledAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnParticleUpdateJobScheduledHandler)new AsyncTriggerHandler<ParticleSystemJobData>(this, cancellationToken, true)).OnParticleUpdateJobScheduledAsync();
		}
	}
}
