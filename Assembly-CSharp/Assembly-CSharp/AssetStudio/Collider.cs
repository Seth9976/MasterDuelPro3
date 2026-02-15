using System;

namespace AssetStudio
{
	// Token: 0x020000DE RID: 222
	public class Collider
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		public Collider(ObjectReader reader)
		{
			this.m_X = new xform(reader);
			this.m_Type = reader.ReadUInt32();
			this.m_XMotionType = reader.ReadUInt32();
			this.m_YMotionType = reader.ReadUInt32();
			this.m_ZMotionType = reader.ReadUInt32();
			this.m_MinLimitX = reader.ReadSingle();
			this.m_MaxLimitX = reader.ReadSingle();
			this.m_MaxLimitY = reader.ReadSingle();
			this.m_MaxLimitZ = reader.ReadSingle();
		}

		// Token: 0x0400069B RID: 1691
		public xform m_X;

		// Token: 0x0400069C RID: 1692
		public uint m_Type;

		// Token: 0x0400069D RID: 1693
		public uint m_XMotionType;

		// Token: 0x0400069E RID: 1694
		public uint m_YMotionType;

		// Token: 0x0400069F RID: 1695
		public uint m_ZMotionType;

		// Token: 0x040006A0 RID: 1696
		public float m_MinLimitX;

		// Token: 0x040006A1 RID: 1697
		public float m_MaxLimitX;

		// Token: 0x040006A2 RID: 1698
		public float m_MaxLimitY;

		// Token: 0x040006A3 RID: 1699
		public float m_MaxLimitZ;
	}
}
