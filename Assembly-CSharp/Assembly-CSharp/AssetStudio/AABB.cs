using System;

namespace AssetStudio
{
	// Token: 0x020000A5 RID: 165
	public class AABB
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public AABB(ObjectReader reader)
		{
			this.m_Center = reader.ReadVector3();
			this.m_Extent = reader.ReadVector3();
		}

		// Token: 0x04000565 RID: 1381
		public Vector3 m_Center;

		// Token: 0x04000566 RID: 1382
		public Vector3 m_Extent;
	}
}
