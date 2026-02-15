using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Indicates that the implementing property should be registered at runtime through the <see cref="M:System.Runtime.Remoting.Contexts.Context.RegisterDynamicProperty(System.Runtime.Remoting.Contexts.IDynamicProperty,System.ContextBoundObject,System.Runtime.Remoting.Contexts.Context)" /> method.</summary>
	// Token: 0x0200044D RID: 1101
	[ComVisible(true)]
	public interface IDynamicProperty
	{
		/// <summary>Gets the name of the dynamic property.</summary>
		/// <returns>The name of the dynamic property.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600244D RID: 9293
		string Name { get; }
	}
}
