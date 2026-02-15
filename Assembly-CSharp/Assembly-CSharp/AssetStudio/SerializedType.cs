using System;

namespace AssetStudio
{
	// Token: 0x0200017C RID: 380
	public class SerializedType
	{
		// Token: 0x04000A07 RID: 2567
		public int classID;

		// Token: 0x04000A08 RID: 2568
		public bool m_IsStrippedType;

		// Token: 0x04000A09 RID: 2569
		public short m_ScriptTypeIndex = -1;

		// Token: 0x04000A0A RID: 2570
		public TypeTree m_Type;

		// Token: 0x04000A0B RID: 2571
		public byte[] m_ScriptID;

		// Token: 0x04000A0C RID: 2572
		public byte[] m_OldTypeHash;

		// Token: 0x04000A0D RID: 2573
		public int[] m_TypeDependencies;

		// Token: 0x04000A0E RID: 2574
		public string m_KlassName;

		// Token: 0x04000A0F RID: 2575
		public string m_NameSpace;

		// Token: 0x04000A10 RID: 2576
		public string m_AsmName;
	}
}
