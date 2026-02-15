using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the base attribute class for declarative security from which <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> is derived.</summary>
	// Token: 0x02000369 RID: 873
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class SecurityAttribute : Attribute
	{
		/// <summary>When overridden in a derived class, creates a permission object that can then be serialized into binary form and persistently stored along with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>A serializable permission object.</returns>
		// Token: 0x06001E74 RID: 7796
		public abstract IPermission CreatePermission();
	}
}
