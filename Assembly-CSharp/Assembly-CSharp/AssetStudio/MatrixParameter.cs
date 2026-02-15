using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200011F RID: 287
	public class MatrixParameter
	{
		// Token: 0x06000377 RID: 887 RVA: 0x000127AC File Offset: 0x000109AC
		public MatrixParameter(BinaryReader reader)
		{
			this.m_NameIndex = reader.ReadInt32();
			this.m_Index = reader.ReadInt32();
			this.m_ArraySize = reader.ReadInt32();
			this.m_Type = reader.ReadSByte();
			this.m_RowCount = reader.ReadSByte();
			reader.AlignStream();
		}

		// Token: 0x040007D2 RID: 2002
		public int m_NameIndex;

		// Token: 0x040007D3 RID: 2003
		public int m_Index;

		// Token: 0x040007D4 RID: 2004
		public int m_ArraySize;

		// Token: 0x040007D5 RID: 2005
		public sbyte m_Type;

		// Token: 0x040007D6 RID: 2006
		public sbyte m_RowCount;
	}
}
