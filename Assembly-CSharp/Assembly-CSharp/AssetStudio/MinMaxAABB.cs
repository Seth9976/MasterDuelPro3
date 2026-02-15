using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x020000EC RID: 236
	public class MinMaxAABB
	{
		// Token: 0x0600032A RID: 810 RVA: 0x0000F78E File Offset: 0x0000D98E
		public MinMaxAABB(BinaryReader reader)
		{
			this.m_Min = reader.ReadVector3();
			this.m_Max = reader.ReadVector3();
		}

		// Token: 0x040006DE RID: 1758
		public Vector3 m_Min;

		// Token: 0x040006DF RID: 1759
		public Vector3 m_Max;
	}
}
