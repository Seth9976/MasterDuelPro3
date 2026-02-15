using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001D RID: 29
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal struct LineInfo
	{
		// Token: 0x04000088 RID: 136
		internal int controlCharacterCount;

		// Token: 0x04000089 RID: 137
		public int characterCount;

		// Token: 0x0400008A RID: 138
		public int visibleCharacterCount;

		// Token: 0x0400008B RID: 139
		public int spaceCount;

		// Token: 0x0400008C RID: 140
		public int wordCount;

		// Token: 0x0400008D RID: 141
		public int firstCharacterIndex;

		// Token: 0x0400008E RID: 142
		public int firstVisibleCharacterIndex;

		// Token: 0x0400008F RID: 143
		public int lastCharacterIndex;

		// Token: 0x04000090 RID: 144
		public int lastVisibleCharacterIndex;

		// Token: 0x04000091 RID: 145
		public float length;

		// Token: 0x04000092 RID: 146
		public float lineHeight;

		// Token: 0x04000093 RID: 147
		public float ascender;

		// Token: 0x04000094 RID: 148
		public float baseline;

		// Token: 0x04000095 RID: 149
		public float descender;

		// Token: 0x04000096 RID: 150
		public float maxAdvance;

		// Token: 0x04000097 RID: 151
		public float width;

		// Token: 0x04000098 RID: 152
		public float marginLeft;

		// Token: 0x04000099 RID: 153
		public float marginRight;

		// Token: 0x0400009A RID: 154
		public TextAlignment alignment;

		// Token: 0x0400009B RID: 155
		public Extents lineExtents;
	}
}
