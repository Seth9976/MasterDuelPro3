using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000051 RID: 81
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct FindNonRegisteredInstancesJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IJobParallelForBatch where T : struct, ValueType
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00008AE4 File Offset: 0x00006CE4
		public unsafe void Execute(int startIndex, int count)
		{
			int* notFoundinstanceIDs = stackalloc int[(UIntPtr)512];
			int length = 0;
			for (int i = startIndex; i < startIndex + count; i++)
			{
				int instanceID = this.instanceIDs[i];
				if (!this.hashMap.ContainsKey(instanceID))
				{
					notFoundinstanceIDs[(IntPtr)(length++) * 4] = instanceID;
				}
			}
			this.outInstancesWriter.AddRangeNoResize((void*)notFoundinstanceIDs, length);
		}

		// Token: 0x04000167 RID: 359
		public const int k_BatchSize = 128;

		// Token: 0x04000168 RID: 360
		[ReadOnly]
		public NativeArray<int> instanceIDs;

		// Token: 0x04000169 RID: 361
		[ReadOnly]
		public NativeParallelHashMap<int, T> hashMap;

		// Token: 0x0400016A RID: 362
		[WriteOnly]
		public NativeList<int>.ParallelWriter outInstancesWriter;
	}
}
