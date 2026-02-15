using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;

namespace YgomDebug
{
	// Token: 0x0200115F RID: 4447
	public class LogShowChainForAnalysis : LogItemBaseForAnalysis
	{
		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06008485 RID: 33925 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008486 RID: 33926 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowChainDataForAnalysis data)
		{
		}

		// Token: 0x06008487 RID: 33927 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetWordTable()
		{
		}

		// Token: 0x0400BFCF RID: 49103
		protected string LABEL_EO_CHAINNUM;

		// Token: 0x0400BFD0 RID: 49104
		protected string LABEL_EO_CHAINTEXT;

		// Token: 0x0400BFD1 RID: 49105
		protected string LABEL_EO_MINICARD;

		// Token: 0x0400BFD2 RID: 49106
		protected static Dictionary<ShowChainDataForAnalysis.ChainDataTypeForAnalysis, string> m_ChainLabelDict;

		// Token: 0x0400BFD3 RID: 49107
		private ElementObjectManager m_EOManager_Origin;
	}
}
