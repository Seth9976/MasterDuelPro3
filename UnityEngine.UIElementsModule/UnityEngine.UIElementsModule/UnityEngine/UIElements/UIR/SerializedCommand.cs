using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000502 RID: 1282
	internal struct SerializedCommand
	{
		// Token: 0x04001058 RID: 4184
		public SerializedCommandType type;

		// Token: 0x04001059 RID: 4185
		public IntPtr vertexBuffer;

		// Token: 0x0400105A RID: 4186
		public IntPtr indexBuffer;

		// Token: 0x0400105B RID: 4187
		public int firstRange;

		// Token: 0x0400105C RID: 4188
		public int rangeCount;

		// Token: 0x0400105D RID: 4189
		public int textureName;

		// Token: 0x0400105E RID: 4190
		public Texture texture;

		// Token: 0x0400105F RID: 4191
		public int gpuDataOffset;

		// Token: 0x04001060 RID: 4192
		public Vector4 gpuData0;

		// Token: 0x04001061 RID: 4193
		public Vector4 gpuData1;
	}
}
