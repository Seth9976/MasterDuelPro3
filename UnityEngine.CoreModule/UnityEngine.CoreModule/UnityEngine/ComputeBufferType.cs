using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000101 RID: 257
	[NativeHeader("Runtime/Export/Graphics/GraphicsBuffer.bindings.h")]
	[Flags]
	public enum ComputeBufferType
	{
		// Token: 0x040002EB RID: 747
		Default = 0,
		// Token: 0x040002EC RID: 748
		Raw = 1,
		// Token: 0x040002ED RID: 749
		Append = 2,
		// Token: 0x040002EE RID: 750
		Counter = 4,
		// Token: 0x040002EF RID: 751
		Constant = 8,
		// Token: 0x040002F0 RID: 752
		Structured = 16,
		// Token: 0x040002F1 RID: 753
		[Obsolete("Enum member DrawIndirect has been deprecated. Use IndirectArguments instead (UnityUpgradable) -> IndirectArguments", false)]
		DrawIndirect = 256,
		// Token: 0x040002F2 RID: 754
		IndirectArguments = 256,
		// Token: 0x040002F3 RID: 755
		[Obsolete("Enum member GPUMemory has been deprecated. All compute buffers now follow the behavior previously defined by this member.", false)]
		GPUMemory = 512
	}
}
