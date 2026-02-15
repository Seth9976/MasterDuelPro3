using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000117 RID: 279
	public class SerializedShaderRTBlendState
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00012454 File Offset: 0x00010654
		public SerializedShaderRTBlendState(BinaryReader reader)
		{
			this.srcBlend = new SerializedShaderFloatValue(reader);
			this.destBlend = new SerializedShaderFloatValue(reader);
			this.srcBlendAlpha = new SerializedShaderFloatValue(reader);
			this.destBlendAlpha = new SerializedShaderFloatValue(reader);
			this.blendOp = new SerializedShaderFloatValue(reader);
			this.blendOpAlpha = new SerializedShaderFloatValue(reader);
			this.colMask = new SerializedShaderFloatValue(reader);
		}

		// Token: 0x04000799 RID: 1945
		public SerializedShaderFloatValue srcBlend;

		// Token: 0x0400079A RID: 1946
		public SerializedShaderFloatValue destBlend;

		// Token: 0x0400079B RID: 1947
		public SerializedShaderFloatValue srcBlendAlpha;

		// Token: 0x0400079C RID: 1948
		public SerializedShaderFloatValue destBlendAlpha;

		// Token: 0x0400079D RID: 1949
		public SerializedShaderFloatValue blendOp;

		// Token: 0x0400079E RID: 1950
		public SerializedShaderFloatValue blendOpAlpha;

		// Token: 0x0400079F RID: 1951
		public SerializedShaderFloatValue colMask;
	}
}
