using System;

namespace System.Security.AccessControl
{
	/// <summary>These flags affect the security descriptor behavior.</summary>
	// Token: 0x020003F0 RID: 1008
	[Flags]
	public enum ControlFlags
	{
		/// <summary>No control flags.</summary>
		// Token: 0x0400107E RID: 4222
		None = 0,
		/// <summary>Specifies that the owner <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.  </summary>
		// Token: 0x0400107F RID: 4223
		OwnerDefaulted = 1,
		/// <summary>Specifies that the group <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.</summary>
		// Token: 0x04001080 RID: 4224
		GroupDefaulted = 2,
		/// <summary>Specifies that the DACL is not null. Set by resource managers or users.  </summary>
		// Token: 0x04001081 RID: 4225
		DiscretionaryAclPresent = 4,
		/// <summary>Specifies that the DACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x04001082 RID: 4226
		DiscretionaryAclDefaulted = 8,
		/// <summary>Specifies that the SACL is not null. Set by resource managers or users.</summary>
		// Token: 0x04001083 RID: 4227
		SystemAclPresent = 16,
		/// <summary>Specifies that the SACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x04001084 RID: 4228
		SystemAclDefaulted = 32,
		/// <summary>Ignored.</summary>
		// Token: 0x04001085 RID: 4229
		DiscretionaryAclUntrusted = 64,
		/// <summary>Ignored.</summary>
		// Token: 0x04001086 RID: 4230
		ServerSecurity = 128,
		/// <summary>Ignored.</summary>
		// Token: 0x04001087 RID: 4231
		DiscretionaryAclAutoInheritRequired = 256,
		/// <summary>Ignored.</summary>
		// Token: 0x04001088 RID: 4232
		SystemAclAutoInheritRequired = 512,
		/// <summary>Specifies that the Discretionary Access Control List (DACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x04001089 RID: 4233
		DiscretionaryAclAutoInherited = 1024,
		/// <summary>Specifies that the System Access Control List (SACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x0400108A RID: 4234
		SystemAclAutoInherited = 2048,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.  </summary>
		// Token: 0x0400108B RID: 4235
		DiscretionaryAclProtected = 4096,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.</summary>
		// Token: 0x0400108C RID: 4236
		SystemAclProtected = 8192,
		/// <summary>Specifies that the contents of the Reserved field are valid.</summary>
		// Token: 0x0400108D RID: 4237
		RMControlValid = 16384,
		/// <summary>Specifies that the security descriptor binary representation is in the self-relative format.  This flag is always set.</summary>
		// Token: 0x0400108E RID: 4238
		SelfRelative = 32768
	}
}
