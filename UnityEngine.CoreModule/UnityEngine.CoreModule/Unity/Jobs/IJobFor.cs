using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200000D RID: 13
	[JobProducerType(typeof(IJobForExtensions.ForJobStruct<>))]
	public interface IJobFor
	{
		// Token: 0x06000013 RID: 19
		void Execute(int index);
	}
}
