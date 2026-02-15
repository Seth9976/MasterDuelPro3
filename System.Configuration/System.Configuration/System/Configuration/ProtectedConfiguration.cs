using System;

namespace System.Configuration
{
	/// <summary>Provides access to the protected-configuration providers for the current application's configuration file. </summary>
	// Token: 0x02000038 RID: 56
	public static class ProtectedConfiguration
	{
		/// <summary>Gets a collection of the installed protected-configuration providers.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProtectedConfigurationProviderCollection" /> collection of installed <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006733 File Offset: 0x00004933
		public static ProtectedConfigurationProviderCollection Providers
		{
			get
			{
				return ProtectedConfiguration.Section.GetAllProviders();
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000673F File Offset: 0x0000493F
		internal static ProtectedConfigurationSection Section
		{
			get
			{
				return (ProtectedConfigurationSection)ConfigurationManager.GetSection("configProtectedData");
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006750 File Offset: 0x00004950
		internal static ProtectedConfigurationProvider GetProvider(string name, bool throwOnError)
		{
			ProtectedConfigurationProvider protectedConfigurationProvider = ProtectedConfiguration.Providers[name];
			if (protectedConfigurationProvider == null && throwOnError)
			{
				throw new Exception(string.Format("The protection provider '{0}' was not found.", name));
			}
			return protectedConfigurationProvider;
		}
	}
}
