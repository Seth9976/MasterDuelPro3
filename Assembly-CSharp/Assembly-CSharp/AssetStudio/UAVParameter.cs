using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000123 RID: 291
	public class UAVParameter
	{
		// Token: 0x0600037B RID: 891 RVA: 0x000129F8 File Offset: 0x00010BF8
		public UAVParameter(BinaryReader reader)
		{
			this.m_NameIndex = reader.ReadInt32();
			this.m_Index = reader.ReadInt32();
			this.m_OriginalIndex = reader.ReadInt32();
		}

		// Token: 0x040007E4 RID: 2020
		public int m_NameIndex;

		// Token: 0x040007E5 RID: 2021
		public int m_Index;

		// Token: 0x040007E6 RID: 2022
		public int m_OriginalIndex;
	}
}
