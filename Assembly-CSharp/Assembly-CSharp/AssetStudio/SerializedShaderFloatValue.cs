using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000116 RID: 278
	public class SerializedShaderFloatValue
	{
		// Token: 0x0600036F RID: 879 RVA: 0x00012434 File Offset: 0x00010634
		public SerializedShaderFloatValue(BinaryReader reader)
		{
			this.val = reader.ReadSingle();
			this.name = reader.ReadAlignedString();
		}

		// Token: 0x04000797 RID: 1943
		public float val;

		// Token: 0x04000798 RID: 1944
		public string name;
	}
}
