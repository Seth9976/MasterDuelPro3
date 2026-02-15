using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x020003F6 RID: 1014
	public abstract class GenericSecurityDescriptor
	{
		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06002246 RID: 8774
		public abstract ControlFlags ControlFlags { get; }

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06002247 RID: 8775
		// (set) Token: 0x06002248 RID: 8776
		public abstract SecurityIdentifier Group { get; set; }

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06002249 RID: 8777
		// (set) Token: 0x0600224A RID: 8778
		public abstract SecurityIdentifier Owner { get; set; }
	}
}
