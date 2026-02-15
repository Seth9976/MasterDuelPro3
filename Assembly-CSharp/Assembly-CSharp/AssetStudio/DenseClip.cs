using System;

namespace AssetStudio
{
	// Token: 0x020000AE RID: 174
	public class DenseClip
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		public DenseClip(ObjectReader reader)
		{
			this.m_FrameCount = reader.ReadInt32();
			this.m_CurveCount = reader.ReadUInt32();
			this.m_SampleRate = reader.ReadSingle();
			this.m_BeginTime = reader.ReadSingle();
			this.m_SampleArray = reader.ReadSingleArray();
		}

		// Token: 0x04000588 RID: 1416
		public int m_FrameCount;

		// Token: 0x04000589 RID: 1417
		public uint m_CurveCount;

		// Token: 0x0400058A RID: 1418
		public float m_SampleRate;

		// Token: 0x0400058B RID: 1419
		public float m_BeginTime;

		// Token: 0x0400058C RID: 1420
		public float[] m_SampleArray;
	}
}
