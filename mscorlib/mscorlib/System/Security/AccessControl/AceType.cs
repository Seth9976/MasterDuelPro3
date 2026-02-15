using System;

namespace System.Security.AccessControl
{
	/// <summary>Defines the available access control entry (ACE) types.</summary>
	// Token: 0x020003E9 RID: 1001
	public enum AceType : byte
	{
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x0400105B RID: 4187
		AccessAllowed,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x0400105C RID: 4188
		AccessDenied,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x0400105D RID: 4189
		SystemAudit,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x0400105E RID: 4190
		SystemAlarm,
		/// <summary>Defined but never used. Included here for completeness.</summary>
		// Token: 0x0400105F RID: 4191
		AccessAllowedCompound,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x04001060 RID: 4192
		AccessAllowedObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x04001061 RID: 4193
		AccessDeniedObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x04001062 RID: 4194
		SystemAuditObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04001063 RID: 4195
		SystemAlarmObject,
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type may contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04001064 RID: 4196
		AccessAllowedCallback,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04001065 RID: 4197
		AccessDeniedCallback,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type may contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04001066 RID: 4198
		AccessAllowedCallbackObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04001067 RID: 4199
		AccessDeniedCallbackObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04001068 RID: 4200
		SystemAuditCallback,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04001069 RID: 4201
		SystemAlarmCallback,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x0400106A RID: 4202
		SystemAuditCallbackObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x0400106B RID: 4203
		SystemAlarmCallbackObject,
		/// <summary>Tracks the maximum defined ACE type in the enumeration.</summary>
		// Token: 0x0400106C RID: 4204
		MaxDefinedAceType = 16
	}
}
