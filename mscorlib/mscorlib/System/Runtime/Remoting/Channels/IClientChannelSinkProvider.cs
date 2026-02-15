using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Creates client channel sinks for the client channel through which remoting messages flow.</summary>
	// Token: 0x0200045D RID: 1117
	[ComVisible(true)]
	public interface IClientChannelSinkProvider
	{
		/// <summary>Gets or sets the next sink provider in the channel sink provider chain.</summary>
		/// <returns>The next sink provider in the channel sink provider chain.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700045E RID: 1118
		// (set) Token: 0x0600248F RID: 9359
		IClientChannelSinkProvider Next { set; }
	}
}
