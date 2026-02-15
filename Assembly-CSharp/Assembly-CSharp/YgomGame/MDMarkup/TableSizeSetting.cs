using System;
using System.Collections.Generic;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BD8 RID: 3032
	[Serializable]
	public class TableSizeSetting
	{
		// Token: 0x06005662 RID: 22114 RVA: 0x0000216A File Offset: 0x0000036A
		public object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x0400934A RID: 37706
		public int colLength;

		// Token: 0x0400934B RID: 37707
		public List<float> colSizes;
	}
}
