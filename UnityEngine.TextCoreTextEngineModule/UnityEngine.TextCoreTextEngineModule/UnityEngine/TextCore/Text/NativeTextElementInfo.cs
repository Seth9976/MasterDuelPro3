using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000026 RID: 38
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEngine.IMGUIModule" })]
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextElementInfo.h")]
	internal struct NativeTextElementInfo
	{
		// Token: 0x040000CC RID: 204
		public int glyphID;

		// Token: 0x040000CD RID: 205
		public TextCoreVertex bottomLeft;

		// Token: 0x040000CE RID: 206
		public TextCoreVertex topLeft;

		// Token: 0x040000CF RID: 207
		public TextCoreVertex topRight;

		// Token: 0x040000D0 RID: 208
		public TextCoreVertex bottomRight;
	}
}
