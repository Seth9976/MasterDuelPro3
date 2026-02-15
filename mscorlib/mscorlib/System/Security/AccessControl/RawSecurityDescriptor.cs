using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x02000407 RID: 1031
	public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of byte values from which to create the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="offset">The offset in the  <paramref name="binaryForm" /> array at which to begin copying.</param>
		// Token: 0x060022A3 RID: 8867 RVA: 0x0008F230 File Offset: 0x0008D430
		public RawSecurityDescriptor(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 20)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			if (binaryForm[offset] != 1)
			{
				throw new ArgumentException("Unrecognized Security Descriptor revision.", "binaryForm");
			}
			this.resourcemgr_control = binaryForm[offset + 1];
			this.control_flags = (ControlFlags)this.ReadUShort(binaryForm, offset + 2);
			int num = this.ReadInt(binaryForm, offset + 4);
			int num2 = this.ReadInt(binaryForm, offset + 8);
			int num3 = this.ReadInt(binaryForm, offset + 12);
			int num4 = this.ReadInt(binaryForm, offset + 16);
			if (num != 0)
			{
				this.owner_sid = new SecurityIdentifier(binaryForm, num);
			}
			if (num2 != 0)
			{
				this.group_sid = new SecurityIdentifier(binaryForm, num2);
			}
			if (num3 != 0)
			{
				this.system_acl = new RawAcl(binaryForm, num3);
			}
			if (num4 != 0)
			{
				this.discretionary_acl = new RawAcl(binaryForm, num4);
			}
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0008F313 File Offset: 0x0008D513
		public override ControlFlags ControlFlags
		{
			get
			{
				return this.control_flags;
			}
		}

		/// <summary>Gets or sets the Discretionary Access Control List (DACL) for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object. The DACL contains access rules.</summary>
		/// <returns>The DACL for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x0008F31B File Offset: 0x0008D51B
		public RawAcl DiscretionaryAcl
		{
			get
			{
				return this.discretionary_acl;
			}
		}

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0008F323 File Offset: 0x0008D523
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0008F32B File Offset: 0x0008D52B
		public override SecurityIdentifier Group
		{
			get
			{
				return this.group_sid;
			}
			set
			{
				this.group_sid = value;
			}
		}

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0008F334 File Offset: 0x0008D534
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x0008F33C File Offset: 0x0008D53C
		public override SecurityIdentifier Owner
		{
			get
			{
				return this.owner_sid;
			}
			set
			{
				this.owner_sid = value;
			}
		}

		/// <summary>Gets or sets the System Access Control List (SACL) for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object. The SACL contains audit rules.</summary>
		/// <returns>The SACL for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x0008F345 File Offset: 0x0008D545
		public RawAcl SystemAcl
		{
			get
			{
				return this.system_acl;
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0008F221 File Offset: 0x0008D421
		private ushort ReadUShort(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | ((int)buffer[offset + 1] << 8));
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0008F34D File Offset: 0x0008D54D
		private int ReadInt(byte[] buffer, int offset)
		{
			return (int)buffer[offset] | ((int)buffer[offset + 1] << 8) | ((int)buffer[offset + 2] << 16) | ((int)buffer[offset + 3] << 24);
		}

		// Token: 0x040010C1 RID: 4289
		private ControlFlags control_flags;

		// Token: 0x040010C2 RID: 4290
		private SecurityIdentifier owner_sid;

		// Token: 0x040010C3 RID: 4291
		private SecurityIdentifier group_sid;

		// Token: 0x040010C4 RID: 4292
		private RawAcl system_acl;

		// Token: 0x040010C5 RID: 4293
		private RawAcl discretionary_acl;

		// Token: 0x040010C6 RID: 4294
		private byte resourcemgr_control;
	}
}
