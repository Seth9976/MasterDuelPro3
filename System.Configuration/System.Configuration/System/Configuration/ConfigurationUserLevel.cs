using System;

namespace System.Configuration
{
	/// <summary>Used to specify which configuration file is to be represented by the Configuration object.</summary>
	// Token: 0x02000024 RID: 36
	public enum ConfigurationUserLevel
	{
		/// <summary>Get the <see cref="T:System.Configuration.Configuration" /> that applies to all users.</summary>
		// Token: 0x04000091 RID: 145
		None,
		/// <summary>Get the roaming <see cref="T:System.Configuration.Configuration" /> that applies to the current user.</summary>
		// Token: 0x04000092 RID: 146
		PerUserRoaming = 10,
		/// <summary>Get the local <see cref="T:System.Configuration.Configuration" /> that applies to the current user.</summary>
		// Token: 0x04000093 RID: 147
		PerUserRoamingAndLocal = 20
	}
}
