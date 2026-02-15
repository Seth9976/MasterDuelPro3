using System;
using System.Collections.Generic;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200012A RID: 298
	public class SerializedTagMap
	{
		// Token: 0x06000380 RID: 896 RVA: 0x00013198 File Offset: 0x00011398
		public SerializedTagMap(BinaryReader reader)
		{
			int numTags = reader.ReadInt32();
			this.tags = new KeyValuePair<string, string>[numTags];
			for (int i = 0; i < numTags; i++)
			{
				this.tags[i] = new KeyValuePair<string, string>(reader.ReadAlignedString(), reader.ReadAlignedString());
			}
		}

		// Token: 0x0400083A RID: 2106
		public KeyValuePair<string, string>[] tags;
	}
}
