using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Represents the construction call request of an object.</summary>
	// Token: 0x02000466 RID: 1126
	[ComVisible(true)]
	public interface IConstructionCallMessage : IMessage, IMethodCallMessage, IMethodMessage
	{
		/// <summary>Gets the type of the remote object to activate.</summary>
		/// <returns>The type of the remote object to activate.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060024AB RID: 9387
		Type ActivationType { get; }

		/// <summary>Gets the full type name of the remote type to activate.</summary>
		/// <returns>The full type name of the remote type to activate.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060024AC RID: 9388
		string ActivationTypeName { get; }

		/// <summary>Gets or sets the activator that activates the remote object.</summary>
		/// <returns>The activator that activates the remote object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060024AD RID: 9389
		// (set) Token: 0x060024AE RID: 9390
		IActivator Activator { get; set; }

		/// <summary>Gets the call site activation attributes.</summary>
		/// <returns>The call site activation attributes.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060024AF RID: 9391
		object[] CallSiteActivationAttributes { get; }

		/// <summary>Gets a list of context properties that define the context in which the object is to be created.</summary>
		/// <returns>A list of properties of the context in which to construct the object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060024B0 RID: 9392
		IList ContextProperties { get; }
	}
}
