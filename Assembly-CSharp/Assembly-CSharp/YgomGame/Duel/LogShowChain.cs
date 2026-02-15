using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000EC1 RID: 3777
	public class LogShowChain : LogItemBase
	{
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06006E2E RID: 28206 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006E2F RID: 28207 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowChainData data)
		{
		}

		// Token: 0x06006E30 RID: 28208 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetWordTable()
		{
		}

		// Token: 0x0400A900 RID: 43264
		protected string LABEL_EO_CHAINNUM;

		// Token: 0x0400A901 RID: 43265
		protected string LABEL_EO_CHAINTEXT;

		// Token: 0x0400A902 RID: 43266
		protected string LABEL_EO_MINICARD;

		// Token: 0x0400A903 RID: 43267
		protected static Dictionary<ShowChainData.ChainDataType, string> m_ChainLabelDict;

		// Token: 0x0400A904 RID: 43268
		private ElementObjectManager m_EOManager_Origin;
	}
}
