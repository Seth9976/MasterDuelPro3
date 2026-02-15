using System;

namespace AssetStudio
{
	// Token: 0x020000B3 RID: 179
	public class ValueDelta
	{
		// Token: 0x060002EF RID: 751 RVA: 0x0000D41D File Offset: 0x0000B61D
		public ValueDelta(ObjectReader reader)
		{
			this.m_Start = reader.ReadSingle();
			this.m_Stop = reader.ReadSingle();
		}

		// Token: 0x04000597 RID: 1431
		public float m_Start;

		// Token: 0x04000598 RID: 1432
		public float m_Stop;
	}
}
