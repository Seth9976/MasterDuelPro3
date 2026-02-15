using System;

namespace AssetStudio
{
	// Token: 0x02000181 RID: 385
	public class TypeTreeNode
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x00002739 File Offset: 0x00000939
		public TypeTreeNode()
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001A3BF File Offset: 0x000185BF
		public TypeTreeNode(string type, string name, int level, bool align)
		{
			this.m_Type = type;
			this.m_Name = name;
			this.m_Level = level;
			this.m_MetaFlag = (align ? 16384 : 0);
		}

		// Token: 0x04000A16 RID: 2582
		public string m_Type;

		// Token: 0x04000A17 RID: 2583
		public string m_Name;

		// Token: 0x04000A18 RID: 2584
		public int m_ByteSize;

		// Token: 0x04000A19 RID: 2585
		public int m_Index;

		// Token: 0x04000A1A RID: 2586
		public int m_TypeFlags;

		// Token: 0x04000A1B RID: 2587
		public int m_Version;

		// Token: 0x04000A1C RID: 2588
		public int m_MetaFlag;

		// Token: 0x04000A1D RID: 2589
		public int m_Level;

		// Token: 0x04000A1E RID: 2590
		public uint m_TypeStrOffset;

		// Token: 0x04000A1F RID: 2591
		public uint m_NameStrOffset;

		// Token: 0x04000A20 RID: 2592
		public ulong m_RefTypeHash;
	}
}
