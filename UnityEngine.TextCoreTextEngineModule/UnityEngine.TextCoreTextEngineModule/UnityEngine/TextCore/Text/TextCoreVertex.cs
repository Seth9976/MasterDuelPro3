using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003B RID: 59
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	[UsedByNativeCode("TextCoreVertex")]
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextCoreVertex.h")]
	internal struct TextCoreVertex
	{
		// Token: 0x04000187 RID: 391
		public Vector3 position;

		// Token: 0x04000188 RID: 392
		public Color32 color;

		// Token: 0x04000189 RID: 393
		public Vector2 uv0;

		// Token: 0x0400018A RID: 394
		public Vector2 uv2;
	}
}
