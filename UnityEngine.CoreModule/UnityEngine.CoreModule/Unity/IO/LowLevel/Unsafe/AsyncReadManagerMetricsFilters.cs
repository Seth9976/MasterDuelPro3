using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000045 RID: 69
	[RequiredByNativeCode]
	[NativeAsStruct]
	[NativeConditional("ENABLE_PROFILER")]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncReadManagerMetricsFilters
	{
		// Token: 0x040000D9 RID: 217
		[NativeName("typeIDs")]
		internal ulong[] TypeIDs;

		// Token: 0x040000DA RID: 218
		[NativeName("states")]
		internal ProcessingState[] States;

		// Token: 0x040000DB RID: 219
		[NativeName("readTypes")]
		internal FileReadType[] ReadTypes;

		// Token: 0x040000DC RID: 220
		[NativeName("priorityLevels")]
		internal Priority[] PriorityLevels;

		// Token: 0x040000DD RID: 221
		[NativeName("subsystems")]
		internal AssetLoadingSubsystem[] Subsystems;
	}
}
