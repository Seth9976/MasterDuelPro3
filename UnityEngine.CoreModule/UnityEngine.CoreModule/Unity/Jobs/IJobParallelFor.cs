using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000011 RID: 17
	[JobProducerType(typeof(IJobParallelForExtensions.ParallelForJobStruct<>))]
	public interface IJobParallelFor
	{
		// Token: 0x0600001C RID: 28
		void Execute(int index);
	}
}
