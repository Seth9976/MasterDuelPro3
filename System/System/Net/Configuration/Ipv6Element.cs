using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Determines whether Internet Protocol version 6 is enabled on the local computer. This class cannot be inherited.</summary>
	// Token: 0x0200048F RID: 1167
	public sealed class Ipv6Element : ConfigurationElement
	{
		// Token: 0x06001CA0 RID: 7328 RVA: 0x0007CD90 File Offset: 0x0007AF90
		static Ipv6Element()
		{
			Ipv6Element.properties.Add(Ipv6Element.enabledProp);
		}

		/// <summary>Gets or sets a Boolean value that indicates whether Internet Protocol version 6 is enabled on the local computer.</summary>
		/// <returns>true if IPv6 is enabled; otherwise, false.</returns>
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x0007CDCA File Offset: 0x0007AFCA
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[Ipv6Element.enabledProp];
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0007CDDC File Offset: 0x0007AFDC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Ipv6Element.properties;
			}
		}

		// Token: 0x040013A7 RID: 5031
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040013A8 RID: 5032
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);
	}
}
