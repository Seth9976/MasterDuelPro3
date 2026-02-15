using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x02000053 RID: 83
	[VisibleToOtherModules(new string[] { "UnityEngine.AIModule" })]
	[UsedByNativeCode]
	internal enum LeakCategory
	{
		// Token: 0x040000F0 RID: 240
		Invalid,
		// Token: 0x040000F1 RID: 241
		Malloc,
		// Token: 0x040000F2 RID: 242
		TempJob,
		// Token: 0x040000F3 RID: 243
		Persistent,
		// Token: 0x040000F4 RID: 244
		LightProbesQuery,
		// Token: 0x040000F5 RID: 245
		NativeTest,
		// Token: 0x040000F6 RID: 246
		MeshDataArray,
		// Token: 0x040000F7 RID: 247
		TransformAccessArray,
		// Token: 0x040000F8 RID: 248
		NavMeshQuery
	}
}
