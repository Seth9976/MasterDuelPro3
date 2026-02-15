using System;

namespace AssetStudio
{
	// Token: 0x020000C4 RID: 196
	public class Blend1dDataConstant
	{
		// Token: 0x06000302 RID: 770 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		public Blend1dDataConstant(ObjectReader reader)
		{
			this.m_ChildThresholdArray = reader.ReadSingleArray();
		}

		// Token: 0x04000608 RID: 1544
		public float[] m_ChildThresholdArray;
	}
}
