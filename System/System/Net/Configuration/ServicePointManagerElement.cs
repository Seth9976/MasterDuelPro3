using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the default settings used to create connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x02000497 RID: 1175
	public sealed class ServicePointManagerElement : ConfigurationElement
	{
		// Token: 0x06001CAF RID: 7343 RVA: 0x0007CF94 File Offset: 0x0007B194
		static ServicePointManagerElement()
		{
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateNameProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateRevocationListProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.dnsRefreshTimeoutProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.enableDnsRoundRobinProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.expect100ContinueProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.useNagleAlgorithmProp);
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x0007D0C3 File Offset: 0x0007B2C3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServicePointManagerElement.properties;
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00002FA0 File Offset: 0x000011A0
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x040013BF RID: 5055
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040013C0 RID: 5056
		private static ConfigurationProperty checkCertificateNameProp = new ConfigurationProperty("checkCertificateName", typeof(bool), true);

		// Token: 0x040013C1 RID: 5057
		private static ConfigurationProperty checkCertificateRevocationListProp = new ConfigurationProperty("checkCertificateRevocationList", typeof(bool), false);

		// Token: 0x040013C2 RID: 5058
		private static ConfigurationProperty dnsRefreshTimeoutProp = new ConfigurationProperty("dnsRefreshTimeout", typeof(int), 120000);

		// Token: 0x040013C3 RID: 5059
		private static ConfigurationProperty enableDnsRoundRobinProp = new ConfigurationProperty("enableDnsRoundRobin", typeof(bool), false);

		// Token: 0x040013C4 RID: 5060
		private static ConfigurationProperty expect100ContinueProp = new ConfigurationProperty("expect100Continue", typeof(bool), true);

		// Token: 0x040013C5 RID: 5061
		private static ConfigurationProperty useNagleAlgorithmProp = new ConfigurationProperty("useNagleAlgorithm", typeof(bool), true);
	}
}
