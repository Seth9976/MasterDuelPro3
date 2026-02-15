using System;

namespace System.Configuration.Assemblies
{
	/// <summary>Defines the different types of assembly version compatibility. This feature is not available in version 1.0 of the .NET Framework.</summary>
	// Token: 0x020006F2 RID: 1778
	public enum AssemblyVersionCompatibility
	{
		/// <summary>The assembly cannot execute with other versions if they are executing on the same machine.</summary>
		// Token: 0x04001E3C RID: 7740
		SameMachine = 1,
		/// <summary>The assembly cannot execute with other versions if they are executing in the same process.</summary>
		// Token: 0x04001E3D RID: 7741
		SameProcess,
		/// <summary>The assembly cannot execute with other versions if they are executing in the same application domain.</summary>
		// Token: 0x04001E3E RID: 7742
		SameDomain
	}
}
