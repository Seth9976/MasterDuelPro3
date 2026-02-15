using System;

namespace AssetStudio
{
	// Token: 0x020000A7 RID: 167
	public class HandPose
	{
		// Token: 0x060002DF RID: 735 RVA: 0x0000CD10 File Offset: 0x0000AF10
		public HandPose(ObjectReader reader)
		{
			this.m_GrabX = new xform(reader);
			this.m_DoFArray = reader.ReadSingleArray();
			this.m_Override = reader.ReadSingle();
			this.m_CloseOpen = reader.ReadSingle();
			this.m_InOut = reader.ReadSingle();
			this.m_Grab = reader.ReadSingle();
		}

		// Token: 0x0400056A RID: 1386
		public xform m_GrabX;

		// Token: 0x0400056B RID: 1387
		public float[] m_DoFArray;

		// Token: 0x0400056C RID: 1388
		public float m_Override;

		// Token: 0x0400056D RID: 1389
		public float m_CloseOpen;

		// Token: 0x0400056E RID: 1390
		public float m_InOut;

		// Token: 0x0400056F RID: 1391
		public float m_Grab;
	}
}
