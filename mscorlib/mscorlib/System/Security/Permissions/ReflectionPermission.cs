using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls access to non-public types and members through the <see cref="N:System.Reflection" /> APIs. Controls some features of the <see cref="N:System.Reflection.Emit" /> APIs.</summary>
	// Token: 0x02000366 RID: 870
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001E56 RID: 7766 RVA: 0x0007792D File Offset: 0x00075B2D
		public ReflectionPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.flags = ReflectionPermissionFlag.AllFlags;
				return;
			}
			this.flags = ReflectionPermissionFlag.NoFlags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermission" /> class with the specified access.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" />. </exception>
		// Token: 0x06001E57 RID: 7767 RVA: 0x0007794E File Offset: 0x00075B4E
		public ReflectionPermission(ReflectionPermissionFlag flag)
		{
			this.Flags = flag;
		}

		/// <summary>Gets or sets the type of reflection allowed for the current permission.</summary>
		/// <returns>The set flags for the current permission.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> for the valid values. </exception>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0007795D File Offset: 0x00075B5D
		// (set) Token: 0x06001E59 RID: 7769 RVA: 0x00077965 File Offset: 0x00075B65
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				if ((value & (ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess)) != value)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid flags {0}"), value), "ReflectionPermissionFlag");
				}
				this.flags = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06001E5A RID: 7770 RVA: 0x00077995 File Offset: 0x00075B95
		public override IPermission Copy()
		{
			return new ReflectionPermission(this.flags);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06001E5B RID: 7771 RVA: 0x000779A4 File Offset: 0x00075BA4
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.flags = ReflectionPermissionFlag.AllFlags;
				return;
			}
			this.flags = ReflectionPermissionFlag.NoFlags;
			string text = esd.Attributes["Flags"] as string;
			if (text.IndexOf("MemberAccess") >= 0)
			{
				this.flags |= ReflectionPermissionFlag.MemberAccess;
			}
			if (text.IndexOf("ReflectionEmit") >= 0)
			{
				this.flags |= ReflectionPermissionFlag.ReflectionEmit;
			}
			if (text.IndexOf("TypeInformation") >= 0)
			{
				this.flags |= ReflectionPermissionFlag.TypeInformation;
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E5C RID: 7772 RVA: 0x00077A40 File Offset: 0x00075C40
		public override IPermission Intersect(IPermission target)
		{
			ReflectionPermission reflectionPermission = this.Cast(target);
			if (reflectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				if (reflectionPermission.Flags == ReflectionPermissionFlag.NoFlags)
				{
					return null;
				}
				return reflectionPermission.Copy();
			}
			else if (reflectionPermission.IsUnrestricted())
			{
				if (this.flags == ReflectionPermissionFlag.NoFlags)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				ReflectionPermission reflectionPermission2 = (ReflectionPermission)reflectionPermission.Copy();
				reflectionPermission2.Flags &= this.flags;
				if (reflectionPermission2.Flags != ReflectionPermissionFlag.NoFlags)
				{
					return reflectionPermission2;
				}
				return null;
			}
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E5D RID: 7773 RVA: 0x00077AB8 File Offset: 0x00075CB8
		public override bool IsSubsetOf(IPermission target)
		{
			ReflectionPermission reflectionPermission = this.Cast(target);
			if (reflectionPermission == null)
			{
				return this.flags == ReflectionPermissionFlag.NoFlags;
			}
			if (this.IsUnrestricted())
			{
				return reflectionPermission.IsUnrestricted();
			}
			return reflectionPermission.IsUnrestricted() || (this.flags & reflectionPermission.Flags) == this.flags;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06001E5E RID: 7774 RVA: 0x00077B08 File Offset: 0x00075D08
		public bool IsUnrestricted()
		{
			return this.flags == ReflectionPermissionFlag.AllFlags;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06001E5F RID: 7775 RVA: 0x00077B14 File Offset: 0x00075D14
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.flags == ReflectionPermissionFlag.NoFlags)
			{
				securityElement.AddAttribute("Flags", "NoFlags");
			}
			else if ((this.flags & ReflectionPermissionFlag.AllFlags) == ReflectionPermissionFlag.AllFlags)
			{
				securityElement.AddAttribute("Flags", "AllFlags");
			}
			else
			{
				string text = "";
				if ((this.flags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess)
				{
					text = "MemberAccess";
				}
				if ((this.flags & ReflectionPermissionFlag.ReflectionEmit) == ReflectionPermissionFlag.ReflectionEmit)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "ReflectionEmit";
				}
				if ((this.flags & ReflectionPermissionFlag.TypeInformation) == ReflectionPermissionFlag.TypeInformation)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "TypeInformation";
				}
				securityElement.AddAttribute("Flags", text);
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E60 RID: 7776 RVA: 0x00077BFC File Offset: 0x00075DFC
		public override IPermission Union(IPermission other)
		{
			ReflectionPermission reflectionPermission = this.Cast(other);
			if (other == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || reflectionPermission.IsUnrestricted())
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			ReflectionPermission reflectionPermission2 = (ReflectionPermission)reflectionPermission.Copy();
			reflectionPermission2.Flags |= this.flags;
			return reflectionPermission2;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00077C50 File Offset: 0x00075E50
		private ReflectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ReflectionPermission reflectionPermission = target as ReflectionPermission;
			if (reflectionPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(ReflectionPermission));
			}
			return reflectionPermission;
		}

		// Token: 0x04000E18 RID: 3608
		private ReflectionPermissionFlag flags;
	}
}
