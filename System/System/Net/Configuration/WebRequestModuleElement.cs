using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a URI prefix and the associated class that handles creating Web requests for the prefix. This class cannot be inherited.</summary>
	// Token: 0x0200049B RID: 1179
	public sealed class WebRequestModuleElement : ConfigurationElement
	{
		// Token: 0x06001CBE RID: 7358 RVA: 0x0007D2C0 File Offset: 0x0007B4C0
		static WebRequestModuleElement()
		{
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.prefixProp);
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.typeProp);
		}

		/// <summary>Gets or sets the URI prefix for the current Web request module.</summary>
		/// <returns>A string that contains a URI prefix.</returns>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0007D329 File Offset: 0x0007B529
		[ConfigurationProperty("prefix", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Prefix
		{
			get
			{
				return (string)base[WebRequestModuleElement.prefixProp];
			}
		}

		/// <summary>Gets or sets a class that creates Web requests.</summary>
		/// <returns>A <see cref="T:System.Type" /> instance that identifies a Web request module.</returns>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0007D33B File Offset: 0x0007B53B
		[ConfigurationProperty("type")]
		[TypeConverter(typeof(TypeConverter))]
		public Type Type
		{
			get
			{
				return Type.GetType((string)base[WebRequestModuleElement.typeProp]);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0007D352 File Offset: 0x0007B552
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModuleElement.properties;
			}
		}

		// Token: 0x040013D2 RID: 5074
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040013D3 RID: 5075
		private static ConfigurationProperty prefixProp = new ConfigurationProperty("prefix", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040013D4 RID: 5076
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string));
	}
}
