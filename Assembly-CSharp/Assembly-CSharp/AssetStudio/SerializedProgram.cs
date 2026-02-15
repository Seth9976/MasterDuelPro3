using System;

namespace AssetStudio
{
	// Token: 0x02000127 RID: 295
	public class SerializedProgram
	{
		// Token: 0x0600037E RID: 894 RVA: 0x00012EC4 File Offset: 0x000110C4
		public SerializedProgram(ObjectReader reader)
		{
			int[] version = reader.version;
			int numSubPrograms = reader.ReadInt32();
			this.m_SubPrograms = new SerializedSubProgram[numSubPrograms];
			for (int i = 0; i < numSubPrograms; i++)
			{
				this.m_SubPrograms[i] = new SerializedSubProgram(reader);
			}
			if ((version[0] == 2020 && version[1] > 3) || (version[0] == 2020 && version[1] == 3 && version[2] >= 2) || (version[0] > 2021 || (version[0] == 2021 && version[1] > 1)) || (version[0] == 2021 && version[1] == 1 && version[2] >= 1))
			{
				this.m_CommonParameters = new SerializedProgramParameters(reader);
			}
			if (version[0] > 2022 || (version[0] == 2022 && version[1] >= 1))
			{
				this.m_SerializedKeywordStateMask = reader.ReadUInt16Array();
				reader.AlignStream();
			}
		}

		// Token: 0x0400081F RID: 2079
		public SerializedSubProgram[] m_SubPrograms;

		// Token: 0x04000820 RID: 2080
		public SerializedProgramParameters m_CommonParameters;

		// Token: 0x04000821 RID: 2081
		public ushort[] m_SerializedKeywordStateMask;
	}
}
