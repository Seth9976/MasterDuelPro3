using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Controls the.NET remoting lifetime services.</summary>
	// Token: 0x0200043C RID: 1084
	[ComVisible(true)]
	public sealed class LifetimeServices
	{
		/// <summary>Gets or sets the time interval between each activation of the lease manager to clean up expired leases.</summary>
		/// <returns>The default amount of time the lease manager sleeps after checking for expired leases.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x0009439B File Offset: 0x0009259B
		// (set) Token: 0x060023FB RID: 9211 RVA: 0x000943A2 File Offset: 0x000925A2
		public static TimeSpan LeaseManagerPollTime
		{
			get
			{
				return LifetimeServices._leaseManagerPollTime;
			}
			set
			{
				LifetimeServices._leaseManagerPollTime = value;
				LifetimeServices._leaseManager.SetPollTime(value);
			}
		}

		/// <summary>Gets or sets the initial lease time span for an <see cref="T:System.AppDomain" />.</summary>
		/// <returns>The initial lease <see cref="T:System.TimeSpan" /> for objects that can have leases in the <see cref="T:System.AppDomain" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x000943B5 File Offset: 0x000925B5
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x000943BC File Offset: 0x000925BC
		public static TimeSpan LeaseTime
		{
			get
			{
				return LifetimeServices._leaseTime;
			}
			set
			{
				LifetimeServices._leaseTime = value;
			}
		}

		/// <summary>Gets or sets the amount of time by which the lease is extended every time a call comes in on the server object.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> by which a lifetime lease in the current <see cref="T:System.AppDomain" /> is extended after each call.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x000943C4 File Offset: 0x000925C4
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x000943CB File Offset: 0x000925CB
		public static TimeSpan RenewOnCallTime
		{
			get
			{
				return LifetimeServices._renewOnCallTime;
			}
			set
			{
				LifetimeServices._renewOnCallTime = value;
			}
		}

		/// <summary>Gets or sets the amount of time the lease manager waits for a sponsor to return with a lease renewal time.</summary>
		/// <returns>The initial sponsorship time-out.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x000943D3 File Offset: 0x000925D3
		// (set) Token: 0x06002401 RID: 9217 RVA: 0x000943DA File Offset: 0x000925DA
		public static TimeSpan SponsorshipTimeout
		{
			get
			{
				return LifetimeServices._sponsorshipTimeout;
			}
			set
			{
				LifetimeServices._sponsorshipTimeout = value;
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000943E2 File Offset: 0x000925E2
		internal static void TrackLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.TrackLifetime(identity);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000943EF File Offset: 0x000925EF
		internal static void StopTrackingLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.StopTrackingLifetime(identity);
		}

		// Token: 0x04001162 RID: 4450
		private static TimeSpan _leaseManagerPollTime = TimeSpan.FromSeconds(10.0);

		// Token: 0x04001163 RID: 4451
		private static TimeSpan _leaseTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x04001164 RID: 4452
		private static TimeSpan _renewOnCallTime = TimeSpan.FromMinutes(2.0);

		// Token: 0x04001165 RID: 4453
		private static TimeSpan _sponsorshipTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04001166 RID: 4454
		private static LeaseManager _leaseManager = new LeaseManager();
	}
}
