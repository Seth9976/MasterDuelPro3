using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x0200008A RID: 138
	internal class FileFilter
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x00016FE7 File Offset: 0x000151E7
		public FileFilter()
		{
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00016FFA File Offset: 0x000151FA
		public FileFilter(string filter)
		{
			this.filter = filter;
			this.SplitFilter();
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001701A File Offset: 0x0001521A
		public static bool CheckFilter(string val)
		{
			return val.Length == 0 || val.Split(new char[] { '|' }).Length % 2 == 0;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00017040 File Offset: 0x00015240
		public ArrayList FilterArrayList
		{
			get
			{
				return this.filterArrayList;
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00017048 File Offset: 0x00015248
		private void SplitFilter()
		{
			this.filterArrayList.Clear();
			if (this.filter.Length == 0)
			{
				return;
			}
			string[] array = this.filter.Split(new char[] { '|' });
			for (int i = 0; i < array.Length; i += 2)
			{
				FilterStruct filterStruct = new FilterStruct(array[i], array[i + 1]);
				this.filterArrayList.Add(filterStruct);
			}
		}

		// Token: 0x04000391 RID: 913
		private ArrayList filterArrayList = new ArrayList();

		// Token: 0x04000392 RID: 914
		private string filter;
	}
}
