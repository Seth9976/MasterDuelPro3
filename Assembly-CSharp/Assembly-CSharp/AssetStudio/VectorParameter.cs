using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200011E RID: 286
	public class VectorParameter
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00012754 File Offset: 0x00010954
		public VectorParameter(BinaryReader reader)
		{
			this.m_NameIndex = reader.ReadInt32();
			this.m_Index = reader.ReadInt32();
			this.m_ArraySize = reader.ReadInt32();
			this.m_Type = reader.ReadSByte();
			this.m_Dim = reader.ReadSByte();
			reader.AlignStream();
		}

		// Token: 0x040007CD RID: 1997
		public int m_NameIndex;

		// Token: 0x040007CE RID: 1998
		public int m_Index;

		// Token: 0x040007CF RID: 1999
		public int m_ArraySize;

		// Token: 0x040007D0 RID: 2000
		public sbyte m_Type;

		// Token: 0x040007D1 RID: 2001
		public sbyte m_Dim;
	}
}
