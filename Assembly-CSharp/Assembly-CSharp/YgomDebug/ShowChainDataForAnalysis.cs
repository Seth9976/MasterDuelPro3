using System;

namespace YgomDebug
{
	// Token: 0x02001166 RID: 4454
	[Serializable]
	public struct ShowChainDataForAnalysis
	{
		// Token: 0x0400BFF5 RID: 49141
		public short cardid;

		// Token: 0x0400BFF6 RID: 49142
		public short chainnum;

		// Token: 0x0400BFF7 RID: 49143
		public bool team;

		// Token: 0x0400BFF8 RID: 49144
		public ShowChainDataForAnalysis.ChainDataTypeForAnalysis type;

		// Token: 0x02001167 RID: 4455
		public enum ChainDataTypeForAnalysis
		{
			// Token: 0x0400BFFA RID: 49146
			SET,
			// Token: 0x0400BFFB RID: 49147
			RUN,
			// Token: 0x0400BFFC RID: 49148
			STEP,
			// Token: 0x0400BFFD RID: 49149
			END
		}
	}
}
