using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	// Token: 0x020001F4 RID: 500
	[JobProducerType(typeof(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<>))]
	public interface IJobParallelForTransform
	{
		// Token: 0x0600139C RID: 5020
		void Execute(int index, TransformAccess transform);
	}
}
