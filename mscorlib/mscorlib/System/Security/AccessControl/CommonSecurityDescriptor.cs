using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x020003EF RID: 1007
	public sealed class CommonSecurityDescriptor : GenericSecurityDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified array of byte values.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="binaryForm">The array of byte values from which to create the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="offset">The offset in the <paramref name="binaryForm" /> array at which to begin copying.</param>
		// Token: 0x0600220C RID: 8716 RVA: 0x0008DCEA File Offset: 0x0008BEEA
		public CommonSecurityDescriptor(bool isContainer, bool isDS, byte[] binaryForm, int offset)
		{
			this.Init(isContainer, isDS, new RawSecurityDescriptor(binaryForm, offset));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified information.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="flags">Flags that specify behavior of the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="owner">The owner for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="group">The primary group for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="systemAcl">The System Access Control List (SACL) for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="discretionaryAcl">The Discretionary Access Control List (DACL) for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		// Token: 0x0600220D RID: 8717 RVA: 0x0008DD02 File Offset: 0x0008BF02
		public CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			this.Init(isContainer, isDS, flags, owner, group, systemAcl, discretionaryAcl);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x0008DD1C File Offset: 0x0008BF1C
		private void Init(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor)
		{
			if (rawSecurityDescriptor == null)
			{
				throw new ArgumentNullException("rawSecurityDescriptor");
			}
			SystemAcl systemAcl = null;
			if (rawSecurityDescriptor.SystemAcl != null)
			{
				systemAcl = new SystemAcl(isContainer, isDS, rawSecurityDescriptor.SystemAcl);
			}
			DiscretionaryAcl discretionaryAcl = null;
			if (rawSecurityDescriptor.DiscretionaryAcl != null)
			{
				discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, rawSecurityDescriptor.DiscretionaryAcl);
			}
			this.Init(isContainer, isDS, rawSecurityDescriptor.ControlFlags, rawSecurityDescriptor.Owner, rawSecurityDescriptor.Group, systemAcl, discretionaryAcl);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0008DD83 File Offset: 0x0008BF83
		private void Init(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			this.flags = flags & ~ControlFlags.SystemAclPresent;
			this.is_container = isContainer;
			this.is_ds = isDS;
			this.Owner = owner;
			this.Group = group;
			this.SystemAcl = systemAcl;
			this.DiscretionaryAcl = discretionaryAcl;
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x0008DDC0 File Offset: 0x0008BFC0
		public override ControlFlags ControlFlags
		{
			get
			{
				ControlFlags controlFlags = this.flags;
				controlFlags |= ControlFlags.DiscretionaryAclPresent;
				controlFlags |= ControlFlags.SelfRelative;
				if (this.SystemAcl != null)
				{
					controlFlags |= ControlFlags.SystemAclPresent;
				}
				return controlFlags;
			}
		}

		/// <summary>Gets or sets the discretionary access control list (DACL) for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. The DACL contains access rules.</summary>
		/// <returns>The DACL for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x0008DDEE File Offset: 0x0008BFEE
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x0008DDF8 File Offset: 0x0008BFF8
		public DiscretionaryAcl DiscretionaryAcl
		{
			get
			{
				return this.discretionary_acl;
			}
			set
			{
				if (value == null)
				{
					value = new DiscretionaryAcl(this.IsContainer, this.IsDS, 1);
					value.AddAccess(AccessControlType.Allow, new SecurityIdentifier("WD"), -1, this.IsContainer ? (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit) : InheritanceFlags.None, PropagationFlags.None);
					value.IsAefa = true;
				}
				this.CheckAclConsistency(value);
				this.discretionary_acl = value;
			}
		}

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x0008DE50 File Offset: 0x0008C050
		// (set) Token: 0x06002214 RID: 8724 RVA: 0x0008DE58 File Offset: 0x0008C058
		public override SecurityIdentifier Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a container object.</summary>
		/// <returns>true if the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a container object; otherwise, false.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x0008DE61 File Offset: 0x0008C061
		public bool IsContainer
		{
			get
			{
				return this.is_container;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a directory object.</summary>
		/// <returns>true if the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a directory object; otherwise, false.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x0008DE69 File Offset: 0x0008C069
		public bool IsDS
		{
			get
			{
				return this.is_ds;
			}
		}

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x0008DE71 File Offset: 0x0008C071
		// (set) Token: 0x06002218 RID: 8728 RVA: 0x0008DE79 File Offset: 0x0008C079
		public override SecurityIdentifier Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		/// <summary>Gets or sets the System Access Control List (SACL) for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. The SACL contains audit rules.</summary>
		/// <returns>The SACL for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x0008DE82 File Offset: 0x0008C082
		// (set) Token: 0x0600221A RID: 8730 RVA: 0x0008DE8A File Offset: 0x0008C08A
		public SystemAcl SystemAcl
		{
			get
			{
				return this.system_acl;
			}
			set
			{
				if (value != null)
				{
					this.CheckAclConsistency(value);
				}
				this.system_acl = value;
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0008DE9D File Offset: 0x0008C09D
		private void CheckAclConsistency(CommonAcl acl)
		{
			if (this.IsContainer != acl.IsContainer)
			{
				throw new ArgumentException("IsContainer must match between descriptor and ACL.");
			}
			if (this.IsDS != acl.IsDS)
			{
				throw new ArgumentException("IsDS must match between descriptor and ACL.");
			}
		}

		// Token: 0x04001076 RID: 4214
		private bool is_container;

		// Token: 0x04001077 RID: 4215
		private bool is_ds;

		// Token: 0x04001078 RID: 4216
		private ControlFlags flags;

		// Token: 0x04001079 RID: 4217
		private SecurityIdentifier owner;

		// Token: 0x0400107A RID: 4218
		private SecurityIdentifier group;

		// Token: 0x0400107B RID: 4219
		private SystemAcl system_acl;

		// Token: 0x0400107C RID: 4220
		private DiscretionaryAcl discretionary_acl;
	}
}
