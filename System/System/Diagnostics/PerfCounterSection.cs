using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000155 RID: 341
	internal class PerfCounterSection : ConfigurationElement
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x0002BC20 File Offset: 0x00029E20
		static PerfCounterSection()
		{
			PerfCounterSection._properties.Add(PerfCounterSection._propFileMappingSize);
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0002BC5F File Offset: 0x00029E5F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerfCounterSection._properties;
			}
		}

		// Token: 0x04000617 RID: 1559
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04000618 RID: 1560
		private static readonly ConfigurationProperty _propFileMappingSize = new ConfigurationProperty("filemappingsize", typeof(int), 524288, ConfigurationPropertyOptions.None);
	}
}
