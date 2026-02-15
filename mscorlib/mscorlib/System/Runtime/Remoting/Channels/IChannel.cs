using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides conduits for messages that cross remoting boundaries.</summary>
	// Token: 0x02000459 RID: 1113
	[ComVisible(true)]
	public interface IChannel
	{
		/// <summary>Gets the name of the channel.</summary>
		/// <returns>The name of the channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600248A RID: 9354
		string ChannelName { get; }

		/// <summary>Gets the priority of the channel.</summary>
		/// <returns>An integer that indicates the priority of the channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600248B RID: 9355
		int ChannelPriority { get; }
	}
}
