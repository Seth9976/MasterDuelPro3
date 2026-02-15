using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for a custom <see cref="T:System.Net.IWebProxy" /> module. This class cannot be inherited.</summary>
	// Token: 0x02000490 RID: 1168
	public sealed class ModuleElement : ConfigurationElement
	{
		// Token: 0x06001CA4 RID: 7332 RVA: 0x0007CDE3 File Offset: 0x0007AFE3
		static ModuleElement()
		{
			ModuleElement.properties.Add(ModuleElement.typeProp);
		}

		// Token: 0x040013A9 RID: 5033
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040013AA RID: 5034
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null);
	}
}
