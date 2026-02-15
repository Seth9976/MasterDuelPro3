using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200014A RID: 330
	internal class AssertSection : ConfigurationElement
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x0002B278 File Offset: 0x00029478
		static AssertSection()
		{
			AssertSection._properties.Add(AssertSection._propAssertUIEnabled);
			AssertSection._properties.Add(AssertSection._propLogFile);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0002B2EC File Offset: 0x000294EC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssertSection._properties;
			}
		}

		// Token: 0x04000607 RID: 1543
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04000608 RID: 1544
		private static readonly ConfigurationProperty _propAssertUIEnabled = new ConfigurationProperty("assertuienabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04000609 RID: 1545
		private static readonly ConfigurationProperty _propLogFile = new ConfigurationProperty("logfilename", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
