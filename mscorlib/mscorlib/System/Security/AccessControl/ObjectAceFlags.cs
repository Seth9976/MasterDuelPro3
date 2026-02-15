using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the presence of object types for Access Control Entries (ACEs).</summary>
	// Token: 0x02000402 RID: 1026
	[Flags]
	public enum ObjectAceFlags
	{
		/// <summary>No object types are present.</summary>
		// Token: 0x040010B4 RID: 4276
		None = 0,
		/// <summary>The type of object that is associated with the ACE is present.</summary>
		// Token: 0x040010B5 RID: 4277
		ObjectAceTypePresent = 1,
		/// <summary>The type of object that can inherit the ACE.</summary>
		// Token: 0x040010B6 RID: 4278
		InheritedObjectAceTypePresent = 2
	}
}
