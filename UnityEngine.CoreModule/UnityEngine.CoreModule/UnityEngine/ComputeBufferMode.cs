using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000DA RID: 218
	[NativeType("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum ComputeBufferMode
	{
		// Token: 0x0400028E RID: 654
		Immutable,
		// Token: 0x0400028F RID: 655
		Dynamic,
		// Token: 0x04000290 RID: 656
		[Obsolete("ComputeBufferMode.Circular is deprecated (legacy mode)")]
		Circular,
		// Token: 0x04000291 RID: 657
		[Obsolete("ComputeBufferMode.StreamOut is deprecated (internal use only)")]
		StreamOut,
		// Token: 0x04000292 RID: 658
		SubUpdates
	}
}
