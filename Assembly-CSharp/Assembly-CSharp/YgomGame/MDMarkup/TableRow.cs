using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BD7 RID: 3031
	[Serializable]
	public class TableRow
	{
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600565D RID: 22109 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600565E RID: 22110 RVA: 0x0000216D File Offset: 0x0000036D
		public MDMarkupDef.TableRowStyle styleType
		{
			get
			{
				return MDMarkupDef.TableRowStyle.Normal;
			}
			set
			{
			}
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x0000216A File Offset: 0x0000036A
		public object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009347 RID: 37703
		[SerializeField]
		public string style;

		// Token: 0x04009348 RID: 37704
		[SerializeField]
		public bool border;

		// Token: 0x04009349 RID: 37705
		[SerializeField]
		public List<TableCell> cells;
	}
}
