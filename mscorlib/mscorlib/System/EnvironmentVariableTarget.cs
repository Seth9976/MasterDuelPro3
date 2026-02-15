using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the location where an environment variable is stored or retrieved in a set or get operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CE RID: 462
	[ComVisible(true)]
	public enum EnvironmentVariableTarget
	{
		/// <summary>The environment variable is stored or retrieved from the environment block associated with the current process. </summary>
		// Token: 0x0400075A RID: 1882
		Process,
		/// <summary>The environment variable is stored or retrieved from the HKEY_CURRENT_USER\Environment key in the Windows operating system registry. </summary>
		// Token: 0x0400075B RID: 1883
		User,
		/// <summary>The environment variable is stored or retrieved from the HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment key in the Windows operating system registry. </summary>
		// Token: 0x0400075C RID: 1884
		Machine
	}
}
