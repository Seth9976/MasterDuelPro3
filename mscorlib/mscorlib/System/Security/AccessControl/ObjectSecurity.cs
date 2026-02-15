using System;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to objects without direct manipulation of Access Control Lists (ACLs). This class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> and <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> classes.</summary>
	// Token: 0x02000403 RID: 1027
	public abstract class ObjectSecurity
	{
		// Token: 0x0600227E RID: 8830 RVA: 0x0008EB46 File Offset: 0x0008CD46
		protected ObjectSecurity(CommonSecurityDescriptor securityDescriptor)
		{
			if (securityDescriptor == null)
			{
				throw new ArgumentNullException("securityDescriptor");
			}
			this.descriptor = securityDescriptor;
			this.rw_lock = new ReaderWriterLock();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</param>
		/// <param name="isDS">True if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</param>
		// Token: 0x0600227F RID: 8831 RVA: 0x0008EB70 File Offset: 0x0008CD70
		protected ObjectSecurity(bool isContainer, bool isDS)
			: this(new CommonSecurityDescriptor(isContainer, isDS, ControlFlags.None, null, null, null, new DiscretionaryAcl(isContainer, isDS, 0)))
		{
		}

		/// <summary>Gets a Boolean value that specifies whether the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>true if the DACL is protected; otherwise, false.</returns>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x0008EB98 File Offset: 0x0008CD98
		public bool AreAccessRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this.descriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>true if the SACL is protected; otherwise, false.</returns>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x0008EBDC File Offset: 0x0008CDDC
		public bool AreAuditRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this.descriptor.ControlFlags & ControlFlags.SystemAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x0008EC20 File Offset: 0x0008CE20
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x0008EC2E File Offset: 0x0008CE2E
		internal AccessControlSections AccessControlSectionsModified
		{
			get
			{
				this.Reading();
				return this.sections_modified;
			}
			set
			{
				this.Writing();
				this.sections_modified = value;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</summary>
		/// <returns>true if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object; otherwise, false.</returns>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0008EC3D File Offset: 0x0008CE3D
		protected bool IsContainer
		{
			get
			{
				return this.descriptor.IsContainer;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</summary>
		/// <returns>true if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object; otherwise, false.</returns>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x0008EC4A File Offset: 0x0008CE4A
		protected bool IsDS
		{
			get
			{
				return this.descriptor.IsDS;
			}
		}

		/// <summary>Gets the primary group associated with the specified owner.</summary>
		/// <returns>The primary group associated with the specified owner.</returns>
		/// <param name="targetType">The owner for which to get the primary group. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002286 RID: 8838 RVA: 0x0008EC58 File Offset: 0x0008CE58
		public IdentityReference GetGroup(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this.descriptor.Group == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this.descriptor.Group.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		/// <summary>Gets the owner associated with the specified primary group.</summary>
		/// <returns>The owner associated with the specified group.</returns>
		/// <param name="targetType">The primary group for which to get the owner.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002287 RID: 8839 RVA: 0x0008ECB0 File Offset: 0x0008CEB0
		public IdentityReference GetOwner(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this.descriptor.Owner == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this.descriptor.Owner.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		/// <summary>Sets the specified sections of the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of bytes from which to set the security descriptor.</param>
		/// <param name="includeSections">The sections (access rules, audit rules, owner, primary group) of the security descriptor to set.</param>
		// Token: 0x06002288 RID: 8840 RVA: 0x0008ED08 File Offset: 0x0008CF08
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
		{
			this.CopySddlForm(new CommonSecurityDescriptor(this.IsContainer, this.IsDS, binaryForm, 0), includeSections);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0008ED24 File Offset: 0x0008CF24
		private void CopySddlForm(CommonSecurityDescriptor sourceDescriptor, AccessControlSections includeSections)
		{
			this.WriteLock();
			try
			{
				this.AccessControlSectionsModified |= includeSections;
				if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
				{
					this.descriptor.SystemAcl = sourceDescriptor.SystemAcl;
				}
				if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
				{
					this.descriptor.DiscretionaryAcl = sourceDescriptor.DiscretionaryAcl;
				}
				if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
				{
					this.descriptor.Owner = sourceDescriptor.Owner;
				}
				if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
				{
					this.descriptor.Group = sourceDescriptor.Group;
				}
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0008EDB8 File Offset: 0x0008CFB8
		private void Reading()
		{
			if (!this.rw_lock.IsReaderLockHeld && !this.rw_lock.IsWriterLockHeld)
			{
				throw new InvalidOperationException("Either a read or a write lock must be held.");
			}
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x0600228B RID: 8843 RVA: 0x0008EDDF File Offset: 0x0008CFDF
		protected void ReadLock()
		{
			this.rw_lock.AcquireReaderLock(-1);
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x0600228C RID: 8844 RVA: 0x0008EDED File Offset: 0x0008CFED
		protected void ReadUnlock()
		{
			this.rw_lock.ReleaseReaderLock();
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0008EDFA File Offset: 0x0008CFFA
		private void Writing()
		{
			if (!this.rw_lock.IsWriterLockHeld)
			{
				throw new InvalidOperationException("Write lock must be held.");
			}
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x0600228E RID: 8846 RVA: 0x0008EE14 File Offset: 0x0008D014
		protected void WriteLock()
		{
			this.rw_lock.AcquireWriterLock(-1);
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x0600228F RID: 8847 RVA: 0x0008EE22 File Offset: 0x0008D022
		protected void WriteUnlock()
		{
			this.rw_lock.ReleaseWriterLock();
		}

		// Token: 0x040010B7 RID: 4279
		internal CommonSecurityDescriptor descriptor;

		// Token: 0x040010B8 RID: 4280
		private AccessControlSections sections_modified;

		// Token: 0x040010B9 RID: 4281
		private ReaderWriterLock rw_lock;
	}
}
