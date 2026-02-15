using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a collection of static methods that return information about the common language runtime environment.</summary>
	// Token: 0x02000541 RID: 1345
	[ComVisible(true)]
	public class RuntimeEnvironment
	{
		/// <summary>Gets the path to the system configuration file.</summary>
		/// <returns>The path to the system configuration file.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x000A81F2 File Offset: 0x000A63F2
		public static string SystemConfigurationFile
		{
			get
			{
				return Environment.GetMachineConfigPath();
			}
		}
	}
}
