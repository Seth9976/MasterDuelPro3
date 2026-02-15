using System;

namespace System.Security.AccessControl
{
	/// <summary>Controls access to objects without direct manipulation of access control lists (ACLs). This class is the abstract base class for the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class.</summary>
	// Token: 0x020003EE RID: 1006
	public abstract class CommonObjectSecurity : ObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> class.</summary>
		/// <param name="isContainer">true if the new object is a container object.</param>
		// Token: 0x0600220B RID: 8715 RVA: 0x0008DCE0 File Offset: 0x0008BEE0
		protected CommonObjectSecurity(bool isContainer)
			: base(isContainer, false)
		{
		}
	}
}
