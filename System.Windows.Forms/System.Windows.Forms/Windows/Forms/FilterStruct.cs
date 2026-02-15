using System;
using System.Collections.Specialized;

namespace System.Windows.Forms
{
	// Token: 0x02000089 RID: 137
	internal struct FilterStruct
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x00016F87 File Offset: 0x00015187
		public FilterStruct(string filterName, string filter)
		{
			this.filterName = filterName;
			this.filters = new StringCollection();
			this.SplitFilters(filter);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00016FA4 File Offset: 0x000151A4
		private void SplitFilters(string filter)
		{
			foreach (string text in filter.Split(new char[] { ';' }))
			{
				this.filters.Add(text.Trim());
			}
		}

		// Token: 0x0400038F RID: 911
		public string filterName;

		// Token: 0x04000390 RID: 912
		public StringCollection filters;
	}
}
