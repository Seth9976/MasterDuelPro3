using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000009 RID: 9
	[JobProducerType(typeof(IJobExtensions.JobStruct<>))]
	public interface IJob
	{
		// Token: 0x06000009 RID: 9
		void Execute();
	}
}
