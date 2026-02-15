using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for an authentication module. This class cannot be inherited.</summary>
	// Token: 0x02000484 RID: 1156
	public sealed class AuthenticationModuleElement : ConfigurationElement
	{
		// Token: 0x06001C76 RID: 7286 RVA: 0x0007C862 File Offset: 0x0007AA62
		static AuthenticationModuleElement()
		{
			AuthenticationModuleElement.properties.Add(AuthenticationModuleElement.typeProp);
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x0007C898 File Offset: 0x0007AA98
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements an authentication module or null if no value has been specified.</returns>
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0007C89F File Offset: 0x0007AA9F
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Type
		{
			get
			{
				return (string)base[AuthenticationModuleElement.typeProp];
			}
		}

		// Token: 0x04001390 RID: 5008
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001391 RID: 5009
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
