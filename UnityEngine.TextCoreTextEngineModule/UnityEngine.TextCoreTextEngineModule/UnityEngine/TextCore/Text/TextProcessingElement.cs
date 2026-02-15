using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200004D RID: 77
	[DebuggerDisplay("Unicode ({unicode})  '{(char)unicode}'")]
	internal struct TextProcessingElement
	{
		// Token: 0x040002FC RID: 764
		public TextProcessingElementType elementType;

		// Token: 0x040002FD RID: 765
		public uint unicode;

		// Token: 0x040002FE RID: 766
		public int stringIndex;

		// Token: 0x040002FF RID: 767
		public int length;
	}
}
