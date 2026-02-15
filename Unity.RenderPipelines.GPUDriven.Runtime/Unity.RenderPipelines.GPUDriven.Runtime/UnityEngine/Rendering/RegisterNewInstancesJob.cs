using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000052 RID: 82
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct RegisterNewInstancesJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IJobParallelFor where T : struct, ValueType
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00008B3E File Offset: 0x00006D3E
		public void Execute(int index)
		{
			this.hashMap.TryAdd(this.instanceIDs[index], this.batchIDs[index]);
		}

		// Token: 0x0400016B RID: 363
		public const int k_BatchSize = 128;

		// Token: 0x0400016C RID: 364
		[ReadOnly]
		public NativeArray<int> instanceIDs;

		// Token: 0x0400016D RID: 365
		[ReadOnly]
		public NativeArray<T> batchIDs;

		// Token: 0x0400016E RID: 366
		[WriteOnly]
		public NativeParallelHashMap<int, T>.ParallelWriter hashMap;
	}
}
