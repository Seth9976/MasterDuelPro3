using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003E RID: 62
	public readonly struct DataSourceContext
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009763 File Offset: 0x00007963
		public object dataSource { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000976B File Offset: 0x0000796B
		public PropertyPath dataSourcePath { get; }

		// Token: 0x060001F6 RID: 502 RVA: 0x00009773 File Offset: 0x00007973
		public DataSourceContext(object dataSource, in PropertyPath dataSourcePath)
		{
			this.dataSource = dataSource;
			this.dataSourcePath = dataSourcePath;
		}
	}
}
