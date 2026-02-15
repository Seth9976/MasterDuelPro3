using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000027 RID: 39
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEngine.IMGUIModule" })]
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextInfo.h")]
	internal struct NativeTextInfo
	{
		// Token: 0x040000D1 RID: 209
		public ATGMeshInfo[] meshInfos;
	}
}
