using System;
using UnityEngine.ParticleSystemJobs;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001DE RID: 478
	public interface IAsyncOnParticleUpdateJobScheduledHandler
	{
		// Token: 0x06000B84 RID: 2948
		UniTask<ParticleSystemJobData> OnParticleUpdateJobScheduledAsync();
	}
}
