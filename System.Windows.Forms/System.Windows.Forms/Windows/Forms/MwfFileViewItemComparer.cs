using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x0200008F RID: 143
	internal class MwfFileViewItemComparer : IComparer
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x00018965 File Offset: 0x00016B65
		public MwfFileViewItemComparer(bool asc)
		{
			this.asc = asc;
		}

		// Token: 0x1700016E RID: 366
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00018974 File Offset: 0x00016B74
		public int ColumnIndex
		{
			set
			{
				this.column_index = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001897D File Offset: 0x00016B7D
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00018985 File Offset: 0x00016B85
		public bool Ascendent
		{
			get
			{
				return this.asc;
			}
			set
			{
				this.asc = value;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00018990 File Offset: 0x00016B90
		public int Compare(object a, object b)
		{
			ListViewItem listViewItem = (ListViewItem)a;
			ListViewItem listViewItem2 = (ListViewItem)b;
			int num;
			if (this.asc)
			{
				num = string.Compare(listViewItem.SubItems[this.column_index].Text, listViewItem2.SubItems[this.column_index].Text);
			}
			else
			{
				num = string.Compare(listViewItem2.SubItems[this.column_index].Text, listViewItem.SubItems[this.column_index].Text);
			}
			return num;
		}

		// Token: 0x040003BD RID: 957
		private int column_index;

		// Token: 0x040003BE RID: 958
		private bool asc;
	}
}
