using System;

namespace AssetStudio
{
	// Token: 0x0200012B RID: 299
	public class SerializedSubShader
	{
		// Token: 0x06000381 RID: 897 RVA: 0x000131E8 File Offset: 0x000113E8
		public SerializedSubShader(ObjectReader reader)
		{
			int numPasses = reader.ReadInt32();
			this.m_Passes = new SerializedPass[numPasses];
			for (int i = 0; i < numPasses; i++)
			{
				this.m_Passes[i] = new SerializedPass(reader);
			}
			this.m_Tags = new SerializedTagMap(reader);
			this.m_LOD = reader.ReadInt32();
		}

		// Token: 0x0400083B RID: 2107
		public SerializedPass[] m_Passes;

		// Token: 0x0400083C RID: 2108
		public SerializedTagMap m_Tags;

		// Token: 0x0400083D RID: 2109
		public int m_LOD;
	}
}
