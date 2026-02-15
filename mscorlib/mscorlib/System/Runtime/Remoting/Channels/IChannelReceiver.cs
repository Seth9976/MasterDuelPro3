using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides required functions and properties for the receiver channels.</summary>
	// Token: 0x0200045B RID: 1115
	[ComVisible(true)]
	public interface IChannelReceiver : IChannel
	{
		/// <summary>Gets the channel-specific data.</summary>
		/// <returns>The channel data.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600248C RID: 9356
		object ChannelData { get; }

		/// <summary>Instructs the current channel to start listening for requests.</summary>
		/// <param name="data">Optional initialization information. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x0600248D RID: 9357
		void StartListening(object data);
	}
}
