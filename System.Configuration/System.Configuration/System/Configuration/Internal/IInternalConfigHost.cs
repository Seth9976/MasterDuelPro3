using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines interfaces used by internal .NET structures to initialize application configuration properties.</summary>
	// Token: 0x0200004B RID: 75
	[ComVisible(false)]
	public interface IInternalConfigHost
	{
		/// <summary>Creates and returns a context object for a <see cref="T:System.Configuration.ConfigurationElement" /> of an application configuration.</summary>
		/// <returns>A context object for a <see cref="T:System.Configuration.ConfigurationElement" /> object of an application configuration.</returns>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="locationSubPath">A string representing a subpath location of the configuration element.</param>
		// Token: 0x060001E1 RID: 481
		object CreateConfigurationContext(string configPath, string locationSubPath);

		/// <summary>Creates and returns a deprecated context object of the application configuration.</summary>
		/// <returns>A deprecated context object of the application configuration.</returns>
		/// <param name="configPath">A string representing a path to an application configuration file.</param>
		// Token: 0x060001E2 RID: 482
		object CreateDeprecatedConfigContext(string configPath);

		/// <summary>Returns the complete path to an application configuration file based on the location subpath.</summary>
		/// <returns>A string representing the complete path to an application configuration file.</returns>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="locationSubPath">The subpath location of the configuration file.</param>
		// Token: 0x060001E3 RID: 483
		string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath);

		/// <summary>Returns a <see cref="T:System.Type" /> object representing the type of the configuration object.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type of the configuration object.</returns>
		/// <param name="typeName">The type name</param>
		/// <param name="throwOnError">true to throw an exception if an error occurs; otherwise, false</param>
		// Token: 0x060001E4 RID: 484
		Type GetConfigType(string typeName, bool throwOnError);

		/// <summary>Returns a string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</summary>
		/// <returns>A string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> I/O tasks on the configuration file.</returns>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		// Token: 0x060001E5 RID: 485
		string GetStreamName(string configPath);

		/// <summary>Initializes a configuration object.</summary>
		/// <param name="locationSubPath">The subpath location of the configuration file.</param>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="locationConfigPath">A string representing the location of a configuration path.</param>
		/// <param name="configRoot">The <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</param>
		/// <param name="hostInitConfigurationParams">The parameter object containing the values used for initializing the configuration host.</param>
		// Token: 0x060001E6 RID: 486
		void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams);

		/// <summary>Determines if a different <see cref="T:System.Type" /> definition is allowable for an application configuration object.</summary>
		/// <returns>true if a different <see cref="T:System.Type" /> definition is allowable for an application configuration object; otherwise, false.</returns>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="allowDefinition">A <see cref="T:System.Configuration.ConfigurationAllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">A <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object.</param>
		// Token: 0x060001E7 RID: 487
		bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition);

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> to read a configuration file.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		// Token: 0x060001E8 RID: 488
		Stream OpenStreamForRead(string streamName);
	}
}
