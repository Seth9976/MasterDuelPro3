using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x020000EA RID: 234
	public class UnityPropertySheet
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000F4FC File Offset: 0x0000D6FC
		public UnityPropertySheet(ObjectReader reader)
		{
			int[] version = reader.version;
			int m_TexEnvsSize = reader.ReadInt32();
			this.m_TexEnvs = new KeyValuePair<string, UnityTexEnv>[m_TexEnvsSize];
			for (int i = 0; i < m_TexEnvsSize; i++)
			{
				this.m_TexEnvs[i] = new KeyValuePair<string, UnityTexEnv>(reader.ReadAlignedString(), new UnityTexEnv(reader));
			}
			if (version[0] >= 2021)
			{
				int m_IntsSize = reader.ReadInt32();
				this.m_Ints = new KeyValuePair<string, int>[m_IntsSize];
				for (int j = 0; j < m_IntsSize; j++)
				{
					this.m_Ints[j] = new KeyValuePair<string, int>(reader.ReadAlignedString(), reader.ReadInt32());
				}
			}
			int m_FloatsSize = reader.ReadInt32();
			this.m_Floats = new KeyValuePair<string, float>[m_FloatsSize];
			for (int k = 0; k < m_FloatsSize; k++)
			{
				this.m_Floats[k] = new KeyValuePair<string, float>(reader.ReadAlignedString(), reader.ReadSingle());
			}
			int m_ColorsSize = reader.ReadInt32();
			this.m_Colors = new KeyValuePair<string, Color>[m_ColorsSize];
			for (int l = 0; l < m_ColorsSize; l++)
			{
				this.m_Colors[l] = new KeyValuePair<string, Color>(reader.ReadAlignedString(), reader.ReadColor4());
			}
		}

		// Token: 0x040006D8 RID: 1752
		public KeyValuePair<string, UnityTexEnv>[] m_TexEnvs;

		// Token: 0x040006D9 RID: 1753
		public KeyValuePair<string, int>[] m_Ints;

		// Token: 0x040006DA RID: 1754
		public KeyValuePair<string, float>[] m_Floats;

		// Token: 0x040006DB RID: 1755
		public KeyValuePair<string, Color>[] m_Colors;
	}
}
