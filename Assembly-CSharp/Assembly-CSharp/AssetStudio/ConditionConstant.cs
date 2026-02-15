using System;

namespace AssetStudio
{
	// Token: 0x020000BF RID: 191
	public class ConditionConstant
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public ConditionConstant(ObjectReader reader)
		{
			this.m_ConditionMode = reader.ReadUInt32();
			this.m_EventID = reader.ReadUInt32();
			this.m_EventThreshold = reader.ReadSingle();
			this.m_ExitTime = reader.ReadSingle();
		}

		// Token: 0x040005EE RID: 1518
		public uint m_ConditionMode;

		// Token: 0x040005EF RID: 1519
		public uint m_EventID;

		// Token: 0x040005F0 RID: 1520
		public float m_EventThreshold;

		// Token: 0x040005F1 RID: 1521
		public float m_ExitTime;
	}
}
