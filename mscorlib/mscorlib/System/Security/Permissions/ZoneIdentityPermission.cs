using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for the zone from which the code originates. This class cannot be inherited.</summary>
	// Token: 0x02000372 RID: 882
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermission : CodeAccessPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> class to represent the specified zone identity.</summary>
		/// <param name="zone">The zone identifier. </param>
		// Token: 0x06001EC7 RID: 7879 RVA: 0x00079CF8 File Offset: 0x00077EF8
		public ZoneIdentityPermission(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06001EC8 RID: 7880 RVA: 0x00079D07 File Offset: 0x00077F07
		public override IPermission Copy()
		{
			return new ZoneIdentityPermission(this.zone);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null, this permission does not represent the <see cref="F:System.Security.SecurityZone.NoZone" /> security zone, and the specified permission is not equal to the current permission. </exception>
		// Token: 0x06001EC9 RID: 7881 RVA: 0x00079D14 File Offset: 0x00077F14
		public override bool IsSubsetOf(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null)
			{
				return this.zone == SecurityZone.NoZone;
			}
			return this.zone == SecurityZone.NoZone || this.zone == zoneIdentityPermission.zone;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The two permissions are not equal and the current permission does not represent the <see cref="F:System.Security.SecurityZone.NoZone" /> security zone.</exception>
		// Token: 0x06001ECA RID: 7882 RVA: 0x00079D50 File Offset: 0x00077F50
		public override IPermission Union(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null)
			{
				if (this.zone != SecurityZone.NoZone)
				{
					return this.Copy();
				}
				return null;
			}
			else
			{
				if (this.zone == zoneIdentityPermission.zone || zoneIdentityPermission.zone == SecurityZone.NoZone)
				{
					return this.Copy();
				}
				if (this.zone == SecurityZone.NoZone)
				{
					return zoneIdentityPermission.Copy();
				}
				throw new ArgumentException(Locale.GetText("Union impossible"));
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001ECB RID: 7883 RVA: 0x00079DB8 File Offset: 0x00077FB8
		public override IPermission Intersect(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null || this.zone == SecurityZone.NoZone)
			{
				return null;
			}
			if (this.zone == zoneIdentityPermission.zone)
			{
				return this.Copy();
			}
			return null;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06001ECC RID: 7884 RVA: 0x00079DF4 File Offset: 0x00077FF4
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Zone");
			if (text == null)
			{
				this.zone = SecurityZone.NoZone;
				return;
			}
			this.zone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06001ECD RID: 7885 RVA: 0x00079E44 File Offset: 0x00078044
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.zone != SecurityZone.NoZone)
			{
				securityElement.AddAttribute("Zone", this.zone.ToString());
			}
			return securityElement;
		}

		/// <summary>Gets or sets the zone represented by the current <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityZone" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The parameter value is not a valid value of <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x1700035A RID: 858
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x00079E7F File Offset: 0x0007807F
		public SecurityZone SecurityZone
		{
			set
			{
				if (!Enum.IsDefined(typeof(SecurityZone), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "SecurityZone");
				}
				this.zone = value;
			}
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00079EBF File Offset: 0x000780BF
		private ZoneIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
			if (zoneIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(ZoneIdentityPermission));
			}
			return zoneIdentityPermission;
		}

		// Token: 0x04000E45 RID: 3653
		private SecurityZone zone;
	}
}
