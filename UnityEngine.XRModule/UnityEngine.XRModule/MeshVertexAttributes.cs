using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200001F RID: 31
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[Flags]
	public enum MeshVertexAttributes
	{
		// Token: 0x04000091 RID: 145
		None = 0,
		// Token: 0x04000092 RID: 146
		Normals = 1,
		// Token: 0x04000093 RID: 147
		Tangents = 2,
		// Token: 0x04000094 RID: 148
		UVs = 4,
		// Token: 0x04000095 RID: 149
		Colors = 8
	}
}
