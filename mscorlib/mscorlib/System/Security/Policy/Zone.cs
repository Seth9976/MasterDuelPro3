using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the security zone of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200034F RID: 847
	[ComVisible(true)]
	[Serializable]
	public sealed class Zone : EvidenceBase, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Zone" /> class with the zone from which a code assembly originates.</summary>
		/// <param name="zone">The zone of origin for the associated code assembly. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="zone" /> parameter is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06001DD8 RID: 7640 RVA: 0x00075A3C File Offset: 0x00073C3C
		public Zone(SecurityZone zone)
		{
			if (!Enum.IsDefined(typeof(SecurityZone), zone))
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid zone {0}."), zone), "zone");
			}
			this.zone = zone;
		}

		/// <summary>Gets the zone from which the code assembly originates.</summary>
		/// <returns>The zone from which the code assembly originates.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x00075A8D File Offset: 0x00073C8D
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
		}

		/// <summary>Creates an identity permission that corresponds to the current instance of the <see cref="T:System.Security.Policy.Zone" /> evidence class.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Zone" /> evidence.</returns>
		/// <param name="evidence">The evidence set from which to construct the identity permission. </param>
		// Token: 0x06001DDA RID: 7642 RVA: 0x00075A95 File Offset: 0x00073C95
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new ZoneIdentityPermission(this.zone);
		}

		/// <summary>Creates a new zone with the specified URL.</summary>
		/// <returns>A new zone with the specified URL.</returns>
		/// <param name="url">The URL from which to create the zone. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> parameter is null. </exception>
		// Token: 0x06001DDB RID: 7643 RVA: 0x00075AA4 File Offset: 0x00073CA4
		[MonoTODO("Not user configurable yet")]
		public static Zone CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			SecurityZone securityZone = SecurityZone.NoZone;
			if (url.Length == 0)
			{
				return new Zone(securityZone);
			}
			Uri uri = null;
			try
			{
				uri = new Uri(url);
			}
			catch
			{
				return new Zone(securityZone);
			}
			if (securityZone == SecurityZone.NoZone)
			{
				if (uri.IsFile)
				{
					if (File.Exists(uri.LocalPath))
					{
						securityZone = SecurityZone.MyComputer;
					}
					else if (string.Compare("FILE://", 0, url, 0, 7, true, CultureInfo.InvariantCulture) == 0)
					{
						securityZone = SecurityZone.Intranet;
					}
					else
					{
						securityZone = SecurityZone.Internet;
					}
				}
				else if (uri.IsLoopback)
				{
					securityZone = SecurityZone.Intranet;
				}
				else
				{
					securityZone = SecurityZone.Internet;
				}
			}
			return new Zone(securityZone);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Zone" /> evidence object to the specified object for equivalence.</summary>
		/// <returns>true if the two <see cref="T:System.Security.Policy.Zone" /> objects are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Zone" /> evidence object to test for equivalence with the current object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="o" /> parameter is not a <see cref="T:System.Security.Policy.Zone" /> object. </exception>
		// Token: 0x06001DDC RID: 7644 RVA: 0x00075B48 File Offset: 0x00073D48
		public override bool Equals(object o)
		{
			Zone zone = o as Zone;
			return zone != null && zone.zone == this.zone;
		}

		/// <summary>Gets the hash code of the current zone.</summary>
		/// <returns>The hash code of the current zone.</returns>
		// Token: 0x06001DDD RID: 7645 RVA: 0x00075A8D File Offset: 0x00073C8D
		public override int GetHashCode()
		{
			return (int)this.zone;
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Zone" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Zone" />.</returns>
		// Token: 0x06001DDE RID: 7646 RVA: 0x00075B70 File Offset: 0x00073D70
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Zone", this.zone.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x04000DBC RID: 3516
		private SecurityZone zone;
	}
}
