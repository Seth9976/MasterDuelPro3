using System;

namespace System.Security.AccessControl
{
	/// <summary>Inheritance flags specify the semantics of inheritance for access control entries (ACEs).</summary>
	// Token: 0x020003F7 RID: 1015
	[Flags]
	public enum InheritanceFlags
	{
		/// <summary>The ACE is not inherited by child objects.</summary>
		// Token: 0x0400109D RID: 4253
		None = 0,
		/// <summary>The ACE is inherited by child container objects.</summary>
		// Token: 0x0400109E RID: 4254
		ContainerInherit = 1,
		/// <summary>The ACE is inherited by child leaf objects.</summary>
		// Token: 0x0400109F RID: 4255
		ObjectInherit = 2
	}
}
