using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Creates server channel sinks for the server channel through which remoting messages flow.</summary>
	// Token: 0x0200045F RID: 1119
	[ComVisible(true)]
	public interface IServerChannelSinkProvider
	{
		/// <summary>Gets or sets the next sink provider in the channel sink provider chain.</summary>
		/// <returns>The next sink provider in the channel sink provider chain.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000460 RID: 1120
		// (set) Token: 0x06002491 RID: 9361
		IServerChannelSinkProvider Next { set; }
	}
}
