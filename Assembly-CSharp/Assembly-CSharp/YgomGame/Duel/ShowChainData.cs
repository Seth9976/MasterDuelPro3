using System;

namespace YgomGame.Duel
{
	// Token: 0x02000F0F RID: 3855
	public struct ShowChainData
	{
		// Token: 0x0400AB81 RID: 43905
		public short cardid;

		// Token: 0x0400AB82 RID: 43906
		public short chainnum;

		// Token: 0x0400AB83 RID: 43907
		public bool team;

		// Token: 0x0400AB84 RID: 43908
		public ShowChainData.ChainDataType type;

		// Token: 0x02000F10 RID: 3856
		public enum ChainDataType
		{
			// Token: 0x0400AB86 RID: 43910
			SET,
			// Token: 0x0400AB87 RID: 43911
			RUN,
			// Token: 0x0400AB88 RID: 43912
			STEP,
			// Token: 0x0400AB89 RID: 43913
			END
		}
	}
}
