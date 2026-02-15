using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the inheritance and auditing behavior of an access control entry (ACE).</summary>
	// Token: 0x020003E7 RID: 999
	[Flags]
	public enum AceFlags : byte
	{
		/// <summary>No ACE flags are set.</summary>
		// Token: 0x0400104B RID: 4171
		None = 0,
		/// <summary>The access mask is propagated onto child leaf objects.</summary>
		// Token: 0x0400104C RID: 4172
		ObjectInherit = 1,
		/// <summary>The access mask is propagated to child container objects.</summary>
		// Token: 0x0400104D RID: 4173
		ContainerInherit = 2,
		/// <summary>The access checks do not apply to the object; they only apply to its children.</summary>
		// Token: 0x0400104E RID: 4174
		NoPropagateInherit = 4,
		/// <summary>The access mask is propagated only to child objects. This includes both container and leaf child objects.</summary>
		// Token: 0x0400104F RID: 4175
		InheritOnly = 8,
		/// <summary>A logical OR of <see cref="F:System.Security.AccessControl.AceFlags.ObjectInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.ContainerInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.NoPropagateInherit" />, and <see cref="F:System.Security.AccessControl.AceFlags.InheritOnly" />.</summary>
		// Token: 0x04001050 RID: 4176
		InheritanceFlags = 15,
		/// <summary>An ACE is inherited from a parent container rather than being explicitly set for an object.</summary>
		// Token: 0x04001051 RID: 4177
		Inherited = 16,
		/// <summary>Successful access attempts are audited.</summary>
		// Token: 0x04001052 RID: 4178
		SuccessfulAccess = 64,
		/// <summary>Failed access attempts are audited.</summary>
		// Token: 0x04001053 RID: 4179
		FailedAccess = 128,
		/// <summary>All access attempts are audited.</summary>
		// Token: 0x04001054 RID: 4180
		AuditFlags = 192
	}
}
