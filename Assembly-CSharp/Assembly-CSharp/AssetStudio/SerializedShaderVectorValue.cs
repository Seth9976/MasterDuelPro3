using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000119 RID: 281
	public class SerializedShaderVectorValue
	{
		// Token: 0x06000372 RID: 882 RVA: 0x000124F4 File Offset: 0x000106F4
		public SerializedShaderVectorValue(BinaryReader reader)
		{
			this.x = new SerializedShaderFloatValue(reader);
			this.y = new SerializedShaderFloatValue(reader);
			this.z = new SerializedShaderFloatValue(reader);
			this.w = new SerializedShaderFloatValue(reader);
			this.name = reader.ReadAlignedString();
		}

		// Token: 0x040007A4 RID: 1956
		public SerializedShaderFloatValue x;

		// Token: 0x040007A5 RID: 1957
		public SerializedShaderFloatValue y;

		// Token: 0x040007A6 RID: 1958
		public SerializedShaderFloatValue z;

		// Token: 0x040007A7 RID: 1959
		public SerializedShaderFloatValue w;

		// Token: 0x040007A8 RID: 1960
		public string name;
	}
}
