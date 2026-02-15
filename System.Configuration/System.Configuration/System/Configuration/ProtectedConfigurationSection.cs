using System;

namespace System.Configuration
{
	/// <summary>Provides programmatic access to the configProtectedData configuration section. This class cannot be inherited.</summary>
	// Token: 0x0200003B RID: 59
	public sealed class ProtectedConfigurationSection : ConfigurationSection
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00006798 File Offset: 0x00004998
		static ProtectedConfigurationSection()
		{
			ProtectedConfigurationSection.properties.Add(ProtectedConfigurationSection.defaultProviderProp);
			ProtectedConfigurationSection.properties.Add(ProtectedConfigurationSection.providersProp);
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of all the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects in all participating configuration files.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of all the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects in all participating configuration files. </returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006805 File Offset: 0x00004A05
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[ProtectedConfigurationSection.providersProp];
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006818 File Offset: 0x00004A18
		internal ProtectedConfigurationProviderCollection GetAllProviders()
		{
			if (this.providers == null)
			{
				this.providers = new ProtectedConfigurationProviderCollection();
				foreach (object obj in this.Providers)
				{
					ProviderSettings providerSettings = (ProviderSettings)obj;
					this.providers.Add(this.InstantiateProvider(providerSettings));
				}
			}
			return this.providers;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006898 File Offset: 0x00004A98
		private ProtectedConfigurationProvider InstantiateProvider(ProviderSettings ps)
		{
			ProtectedConfigurationProvider protectedConfigurationProvider = Activator.CreateInstance(Type.GetType(ps.Type, true)) as ProtectedConfigurationProvider;
			if (protectedConfigurationProvider == null)
			{
				throw new Exception("The type specified does not extend ProtectedConfigurationProvider class.");
			}
			protectedConfigurationProvider.Initialize(ps.Name, ps.Parameters);
			return protectedConfigurationProvider;
		}

		// Token: 0x040000BD RID: 189
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "RsaProtectedConfigurationProvider");

		// Token: 0x040000BE RID: 190
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null);

		// Token: 0x040000BF RID: 191
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040000C0 RID: 192
		private ProtectedConfigurationProviderCollection providers;
	}
}
