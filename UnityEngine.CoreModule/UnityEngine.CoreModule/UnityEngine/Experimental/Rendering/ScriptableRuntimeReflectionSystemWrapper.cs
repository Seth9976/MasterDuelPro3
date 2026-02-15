using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003F7 RID: 1015
	[RequiredByNativeCode]
	internal class ScriptableRuntimeReflectionSystemWrapper
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0003C8EB File Offset: 0x0003AAEB
		// (set) Token: 0x06001B53 RID: 6995 RVA: 0x0003C8F3 File Offset: 0x0003AAF3
		internal IScriptableRuntimeReflectionSystem implementation { get; set; }

		// Token: 0x06001B54 RID: 6996 RVA: 0x0003C8FC File Offset: 0x0003AAFC
		[RequiredByNativeCode]
		private void Internal_ScriptableRuntimeReflectionSystemWrapper_TickRealtimeProbes(out bool result)
		{
			result = this.implementation != null && this.implementation.TickRealtimeProbes();
		}
	}
}
