using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Provides custom channel information that is carried along with the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
	// Token: 0x02000413 RID: 1043
	[ComVisible(true)]
	public interface IChannelInfo
	{
		/// <summary>Gets and sets the channel data for each channel.</summary>
		/// <returns>The channel data for each channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060022C8 RID: 8904
		object[] ChannelData { get; }
	}
}
