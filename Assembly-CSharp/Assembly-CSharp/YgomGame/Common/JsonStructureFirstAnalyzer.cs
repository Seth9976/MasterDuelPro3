using System;
using System.Collections.Generic;

namespace YgomGame.Common
{
	// Token: 0x0200101F RID: 4127
	public class JsonStructureFirstAnalyzer : JsonObjectAanalyzerBase
	{
		// Token: 0x06007BFF RID: 31743 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsReceivable(object structureFirstData)
		{
			return false;
		}

		// Token: 0x06007C00 RID: 31744 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<object> GetSelectIds(object structureFirstData)
		{
			return null;
		}
	}
}
