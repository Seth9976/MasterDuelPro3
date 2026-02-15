using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Defines a lifetime lease object that is used by the remoting lifetime service.</summary>
	// Token: 0x02000435 RID: 1077
	[ComVisible(true)]
	public interface ILease
	{
		/// <summary>Gets the amount of time remaining on the lease.</summary>
		/// <returns>The amount of time remaining on the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060023DB RID: 9179
		TimeSpan CurrentLeaseTime { get; }

		/// <summary>Gets the current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</summary>
		/// <returns>The current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060023DC RID: 9180
		LeaseState CurrentState { get; }

		/// <summary>Gets or sets the amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</summary>
		/// <returns>The amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060023DD RID: 9181
		TimeSpan RenewOnCallTime { get; }

		/// <summary>Renews a lease for the specified time.</summary>
		/// <returns>The new expiration time of the lease.</returns>
		/// <param name="renewalTime">The length of time to renew the lease by. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060023DE RID: 9182
		TimeSpan Renew(TimeSpan renewalTime);
	}
}
