using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method call return message interface.</summary>
	// Token: 0x0200048F RID: 1167
	[ComVisible(true)]
	public interface IMethodReturnMessage : IMethodMessage, IMessage
	{
		/// <summary>Gets the exception thrown during the method call.</summary>
		/// <returns>The exception object for the method call, or null if the method did not throw an exception.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600256E RID: 9582
		Exception Exception { get; }

		/// <summary>Returns the specified argument marked as a ref or an out parameter.</summary>
		/// <returns>The specified argument marked as a ref or an out parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600256F RID: 9583
		object[] OutArgs { get; }

		/// <summary>Gets the return value of the method call.</summary>
		/// <returns>The return value of the method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06002570 RID: 9584
		object ReturnValue { get; }
	}
}
