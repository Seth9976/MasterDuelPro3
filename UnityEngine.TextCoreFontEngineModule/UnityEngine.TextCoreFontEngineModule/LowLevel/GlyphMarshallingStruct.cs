using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000E RID: 14
	[UsedByNativeCode]
	internal struct GlyphMarshallingStruct
	{
		// Token: 0x0400006A RID: 106
		public uint index;

		// Token: 0x0400006B RID: 107
		public GlyphMetrics metrics;

		// Token: 0x0400006C RID: 108
		public GlyphRect glyphRect;

		// Token: 0x0400006D RID: 109
		public float scale;

		// Token: 0x0400006E RID: 110
		public int atlasIndex;

		// Token: 0x0400006F RID: 111
		public GlyphClassDefinitionType classDefinitionType;
	}
}
