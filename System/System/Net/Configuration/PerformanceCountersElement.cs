using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the performance counter element in the System.Net configuration file that determines whether networking performance counters are enabled. This class cannot be inherited.</summary>
	// Token: 0x02000492 RID: 1170
	public sealed class PerformanceCountersElement : ConfigurationElement
	{
		// Token: 0x06001CA6 RID: 7334 RVA: 0x0007CE20 File Offset: 0x0007B020
		static PerformanceCountersElement()
		{
			PerformanceCountersElement.properties.Add(PerformanceCountersElement.enabledProp);
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x0007CE5A File Offset: 0x0007B05A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerformanceCountersElement.properties;
			}
		}

		// Token: 0x040013AB RID: 5035
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x040013AC RID: 5036
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
