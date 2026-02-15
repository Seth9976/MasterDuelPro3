using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000524 RID: 1316
	internal class Entry
	{
		// Token: 0x04001166 RID: 4454
		public EntryType type;

		// Token: 0x04001167 RID: 4455
		public EntryFlags flags;

		// Token: 0x04001168 RID: 4456
		public NativeSlice<Vertex> vertices;

		// Token: 0x04001169 RID: 4457
		public NativeSlice<ushort> indices;

		// Token: 0x0400116A RID: 4458
		public Texture texture;

		// Token: 0x0400116B RID: 4459
		public float textScale;

		// Token: 0x0400116C RID: 4460
		public float fontSharpness;

		// Token: 0x0400116D RID: 4461
		public VectorImage gradientsOwner;

		// Token: 0x0400116E RID: 4462
		public Material material;

		// Token: 0x0400116F RID: 4463
		public Action immediateCallback;

		// Token: 0x04001170 RID: 4464
		public Entry nextSibling;

		// Token: 0x04001171 RID: 4465
		public Entry firstChild;

		// Token: 0x04001172 RID: 4466
		public Entry lastChild;
	}
}
