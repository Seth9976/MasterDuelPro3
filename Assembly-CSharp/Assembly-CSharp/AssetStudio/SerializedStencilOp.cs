using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000118 RID: 280
	public class SerializedStencilOp
	{
		// Token: 0x06000371 RID: 881 RVA: 0x000124BB File Offset: 0x000106BB
		public SerializedStencilOp(BinaryReader reader)
		{
			this.pass = new SerializedShaderFloatValue(reader);
			this.fail = new SerializedShaderFloatValue(reader);
			this.zFail = new SerializedShaderFloatValue(reader);
			this.comp = new SerializedShaderFloatValue(reader);
		}

		// Token: 0x040007A0 RID: 1952
		public SerializedShaderFloatValue pass;

		// Token: 0x040007A1 RID: 1953
		public SerializedShaderFloatValue fail;

		// Token: 0x040007A2 RID: 1954
		public SerializedShaderFloatValue zFail;

		// Token: 0x040007A3 RID: 1955
		public SerializedShaderFloatValue comp;
	}
}
