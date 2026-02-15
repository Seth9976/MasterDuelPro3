using System;

namespace System.Configuration
{
	/// <summary>Specifies the locations within the configuration-file hierarchy that can set or override the properties contained within a <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
	// Token: 0x0200000A RID: 10
	public enum ConfigurationAllowDefinition
	{
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined only in the Machine.config file.</summary>
		// Token: 0x04000019 RID: 25
		MachineOnly,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in either the Machine.config file or the machine-level Web.config file found in the same directory as Machine.config, but not in application Web.config files.</summary>
		// Token: 0x0400001A RID: 26
		MachineToWebRoot = 100,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in either the Machine.config file, the machine-level Web.config file found in the same directory as Machine.config, or the top-level application Web.config file found in the virtual-directory root, but not in subdirectories of a virtual root.</summary>
		// Token: 0x0400001B RID: 27
		MachineToApplication = 200,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined anywhere.</summary>
		// Token: 0x0400001C RID: 28
		Everywhere = 300
	}
}
