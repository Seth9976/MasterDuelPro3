using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000022 RID: 34
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
	[UsedByNativeCode("MeshInfo")]
	[NativeHeader("Modules/TextCoreTextEngine/Native/MeshInfo.h")]
	internal struct MeshInfoBindings
	{
		// Token: 0x040000B2 RID: 178
		public TextCoreVertex[] vertexData;

		// Token: 0x040000B3 RID: 179
		public Material material;

		// Token: 0x040000B4 RID: 180
		public int vertexCount;
	}
}
