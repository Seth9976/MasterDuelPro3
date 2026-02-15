using System;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BD5 RID: 3029
	[Serializable]
	public class TableCell
	{
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06005652 RID: 22098 RVA: 0x0000216A File Offset: 0x0000036A
		public TableCellValue value
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x0000216A File Offset: 0x0000036A
		public object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009332 RID: 37682
		[SerializeField]
		public bool border;

		// Token: 0x04009333 RID: 37683
		[SerializeField]
		public bool ignorePadding;

		// Token: 0x04009334 RID: 37684
		[SerializeField]
		private TableCellValue cellValue;
	}
}
