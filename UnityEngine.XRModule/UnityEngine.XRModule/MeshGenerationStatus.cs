using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200001C RID: 28
	[RequiredByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public enum MeshGenerationStatus
	{
		// Token: 0x04000082 RID: 130
		Success,
		// Token: 0x04000083 RID: 131
		InvalidMeshId,
		// Token: 0x04000084 RID: 132
		GenerationAlreadyInProgress,
		// Token: 0x04000085 RID: 133
		Canceled,
		// Token: 0x04000086 RID: 134
		UnknownError
	}
}
