using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x02000384 RID: 900
	public struct BatchCullingOutput
	{
		// Token: 0x04000AEA RID: 2794
		public NativeArray<BatchCullingOutputDrawCommands> drawCommands;

		// Token: 0x04000AEB RID: 2795
		public NativeArray<IntPtr> customCullingResult;
	}
}
