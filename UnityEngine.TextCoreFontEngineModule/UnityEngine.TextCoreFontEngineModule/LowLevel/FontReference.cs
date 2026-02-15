using System;
using System.Diagnostics;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000B RID: 11
	[DebuggerDisplay("{familyName} - {styleName}")]
	[UsedByNativeCode]
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	internal struct FontReference
	{
		// Token: 0x0400005B RID: 91
		public string familyName;

		// Token: 0x0400005C RID: 92
		public string styleName;

		// Token: 0x0400005D RID: 93
		public int faceIndex;

		// Token: 0x0400005E RID: 94
		public string filePath;
	}
}
