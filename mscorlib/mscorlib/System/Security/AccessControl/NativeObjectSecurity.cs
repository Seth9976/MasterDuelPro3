using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to native objects without direct manipulation of Access Control Lists (ACLs). Native object types are defined by the <see cref="T:System.Security.AccessControl.ResourceType" /> enumeration.</summary>
	// Token: 0x020003FB RID: 1019
	public abstract class NativeObjectSecurity : CommonObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class by using the specified values.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions. </param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x06002253 RID: 8787 RVA: 0x0008E380 File Offset: 0x0008C580
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: base(isContainer)
		{
			this.exception_from_error_code = exceptionFromErrorCode;
			this.resource_type = resourceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		// Token: 0x06002254 RID: 8788 RVA: 0x0008E397 File Offset: 0x0008C597
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections)
			: this(isContainer, resourceType, handle, includeSections, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions. </param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x06002255 RID: 8789 RVA: 0x0008E3A6 File Offset: 0x0008C5A6
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: this(isContainer, resourceType, exceptionFromErrorCode, exceptionContext)
		{
			this.RaiseExceptionOnFailure(this.InternalGet(handle, includeSections), null, handle, exceptionContext);
			this.ClearAccessControlSectionsModified();
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0008E3D0 File Offset: 0x0008C5D0
		private void ClearAccessControlSectionsModified()
		{
			base.WriteLock();
			try
			{
				base.AccessControlSectionsModified = AccessControlSections.None;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0008E404 File Offset: 0x0008C604
		internal void PersistModifications(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				this.Persist(handle, base.AccessControlSectionsModified, null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="handle">The handle of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x06002258 RID: 8792 RVA: 0x0008E440 File Offset: 0x0008C640
		protected void Persist(SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
		{
			base.WriteLock();
			try
			{
				this.RaiseExceptionOnFailure(this.InternalSet(handle, includeSections), null, handle, exceptionContext);
				base.AccessControlSectionsModified &= ~includeSections;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0008E48C File Offset: 0x0008C68C
		internal static Exception DefaultExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			switch (errorCode)
			{
			case 2:
				return new FileNotFoundException();
			case 3:
				return new DirectoryNotFoundException();
			case 4:
				break;
			case 5:
				return new UnauthorizedAccessException();
			default:
				if (errorCode == 1314)
				{
					return new PrivilegeNotHeldException();
				}
				break;
			}
			return new InvalidOperationException("OS error code " + errorCode.ToString());
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0008E4E9 File Offset: 0x0008C6E9
		private void RaiseExceptionOnFailure(int errorCode, string name, SafeHandle handle, object context)
		{
			if (errorCode == 0)
			{
				return;
			}
			throw (this.exception_from_error_code ?? new NativeObjectSecurity.ExceptionFromErrorCode(NativeObjectSecurity.DefaultExceptionFromErrorCode))(errorCode, name, handle, context);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0008E510 File Offset: 0x0008C710
		internal virtual int InternalGet(SafeHandle handle, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32GetHelper(delegate(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetSecurityInfo(handle, this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}, includeSections);
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0008E558 File Offset: 0x0008C758
		internal virtual int InternalSet(SafeHandle handle, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32SetHelper((SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl) => NativeObjectSecurity.SetSecurityInfo(handle, this.ResourceType, securityInfos, owner, group, dacl, sacl), includeSections);
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x0008E59F File Offset: 0x0008C79F
		internal ResourceType ResourceType
		{
			get
			{
				return this.resource_type;
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0008E5A8 File Offset: 0x0008C7A8
		private int Win32GetHelper(NativeObjectSecurity.GetSecurityInfoNativeCall nativeCall, AccessControlSections includeSections)
		{
			bool flag = (includeSections & AccessControlSections.Owner) > AccessControlSections.None;
			bool flag2 = (includeSections & AccessControlSections.Group) > AccessControlSections.None;
			bool flag3 = (includeSections & AccessControlSections.Access) > AccessControlSections.None;
			bool flag4 = (includeSections & AccessControlSections.Audit) > AccessControlSections.None;
			SecurityInfos securityInfos = (SecurityInfos)0;
			if (flag)
			{
				securityInfos |= SecurityInfos.Owner;
			}
			if (flag2)
			{
				securityInfos |= SecurityInfos.Group;
			}
			if (flag3)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
			}
			if (flag4)
			{
				securityInfos |= SecurityInfos.SystemAcl;
			}
			IntPtr intPtr;
			IntPtr intPtr2;
			IntPtr intPtr3;
			IntPtr intPtr4;
			IntPtr intPtr5;
			int num = nativeCall(securityInfos, out intPtr, out intPtr2, out intPtr3, out intPtr4, out intPtr5);
			if (num != 0)
			{
				return num;
			}
			try
			{
				int num2 = 0;
				if (NativeObjectSecurity.IsValidSecurityDescriptor(intPtr5))
				{
					num2 = NativeObjectSecurity.GetSecurityDescriptorLength(intPtr5);
				}
				byte[] array = new byte[num2];
				Marshal.Copy(intPtr5, array, 0, num2);
				base.SetSecurityDescriptorBinaryForm(array, includeSections);
			}
			finally
			{
				NativeObjectSecurity.LocalFree(intPtr5);
			}
			return 0;
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x0008E658 File Offset: 0x0008C858
		private int Win32SetHelper(NativeObjectSecurity.SetSecurityInfoNativeCall nativeCall, AccessControlSections includeSections)
		{
			if (includeSections == AccessControlSections.None)
			{
				return 0;
			}
			SecurityInfos securityInfos = (SecurityInfos)0;
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			byte[] array4 = null;
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Owner;
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)base.GetOwner(typeof(SecurityIdentifier));
				if (null != securityIdentifier)
				{
					array = new byte[securityIdentifier.BinaryLength];
					securityIdentifier.GetBinaryForm(array, 0);
				}
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Group;
				SecurityIdentifier securityIdentifier2 = (SecurityIdentifier)base.GetGroup(typeof(SecurityIdentifier));
				if (null != securityIdentifier2)
				{
					array2 = new byte[securityIdentifier2.BinaryLength];
					securityIdentifier2.GetBinaryForm(array2, 0);
				}
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
				if (base.AreAccessRulesProtected)
				{
					securityInfos |= (SecurityInfos)(-2147483648);
				}
				else
				{
					securityInfos |= (SecurityInfos)536870912;
				}
				array3 = new byte[this.descriptor.DiscretionaryAcl.BinaryLength];
				this.descriptor.DiscretionaryAcl.GetBinaryForm(array3, 0);
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None && this.descriptor.SystemAcl != null)
			{
				securityInfos |= SecurityInfos.SystemAcl;
				if (base.AreAuditRulesProtected)
				{
					securityInfos |= (SecurityInfos)1073741824;
				}
				else
				{
					securityInfos |= (SecurityInfos)268435456;
				}
				array4 = new byte[this.descriptor.SystemAcl.BinaryLength];
				this.descriptor.SystemAcl.GetBinaryForm(array4, 0);
			}
			return nativeCall(securityInfos, array, array2, array3, array4);
		}

		// Token: 0x06002260 RID: 8800
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetSecurityInfo(SafeHandle handle, ResourceType resourceType, SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x06002261 RID: 8801
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x06002262 RID: 8802
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int SetSecurityInfo(SafeHandle handle, ResourceType resourceType, SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x06002263 RID: 8803
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetSecurityDescriptorLength(IntPtr descriptor);

		// Token: 0x06002264 RID: 8804
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsValidSecurityDescriptor(IntPtr descriptor);

		// Token: 0x040010AA RID: 4266
		private NativeObjectSecurity.ExceptionFromErrorCode exception_from_error_code;

		// Token: 0x040010AB RID: 4267
		private ResourceType resource_type;

		/// <summary>Provides a way for integrators to map numeric error codes to specific exceptions that they create.</summary>
		/// <returns>The <see cref="T:System.Exception" /> this delegate creates.</returns>
		/// <param name="errorCode">The numeric error code.</param>
		/// <param name="name">The name of the securable object with which the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="context">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x020003FC RID: 1020
		// (Invoke) Token: 0x06002266 RID: 8806
		protected internal delegate Exception ExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context);

		// Token: 0x020003FD RID: 1021
		// (Invoke) Token: 0x06002268 RID: 8808
		private delegate int GetSecurityInfoNativeCall(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x020003FE RID: 1022
		// (Invoke) Token: 0x0600226A RID: 8810
		private delegate int SetSecurityInfoNativeCall(SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);
	}
}
