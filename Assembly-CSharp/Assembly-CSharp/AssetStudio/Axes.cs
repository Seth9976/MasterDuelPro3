using System;

namespace AssetStudio
{
	// Token: 0x020000D9 RID: 217
	public class Axes
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0000EC38 File Offset: 0x0000CE38
		public Axes(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_PreQ = reader.ReadVector4();
			this.m_PostQ = reader.ReadVector4();
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 4))
			{
				this.m_Sgn = reader.ReadVector3();
			}
			else
			{
				this.m_Sgn = reader.ReadVector4();
			}
			this.m_Limit = new Limit(reader);
			this.m_Length = reader.ReadSingle();
			this.m_Type = reader.ReadUInt32();
		}

		// Token: 0x0400068D RID: 1677
		public Vector4 m_PreQ;

		// Token: 0x0400068E RID: 1678
		public Vector4 m_PostQ;

		// Token: 0x0400068F RID: 1679
		public object m_Sgn;

		// Token: 0x04000690 RID: 1680
		public Limit m_Limit;

		// Token: 0x04000691 RID: 1681
		public float m_Length;

		// Token: 0x04000692 RID: 1682
		public uint m_Type;
	}
}
