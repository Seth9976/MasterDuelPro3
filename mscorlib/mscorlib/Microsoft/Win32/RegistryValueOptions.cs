using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies optional behavior when retrieving name/value pairs from a registry key.</summary>
	// Token: 0x02000084 RID: 132
	[Flags]
	public enum RegistryValueOptions
	{
		/// <summary>No optional behavior is specified.</summary>
		// Token: 0x0400026A RID: 618
		None = 0,
		/// <summary>A value of type <see cref="F:Microsoft.Win32.RegistryValueKind.ExpandString" /> is retrieved without expanding its embedded environment variables. </summary>
		// Token: 0x0400026B RID: 619
		DoNotExpandEnvironmentNames = 1
	}
}
