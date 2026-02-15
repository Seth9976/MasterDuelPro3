using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200012C RID: 300
	public class SerializedShaderDependency
	{
		// Token: 0x06000382 RID: 898 RVA: 0x00013240 File Offset: 0x00011440
		public SerializedShaderDependency(BinaryReader reader)
		{
			this.from = reader.ReadAlignedString();
			this.to = reader.ReadAlignedString();
		}

		// Token: 0x0400083E RID: 2110
		public string from;

		// Token: 0x0400083F RID: 2111
		public string to;
	}
}
