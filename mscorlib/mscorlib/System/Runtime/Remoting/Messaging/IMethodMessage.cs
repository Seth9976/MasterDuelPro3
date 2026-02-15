using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method message interface.</summary>
	// Token: 0x0200048E RID: 1166
	[ComVisible(true)]
	public interface IMethodMessage : IMessage
	{
		/// <summary>Gets the number of arguments passed to the method.</summary>
		/// <returns>The number of arguments passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06002565 RID: 9573
		int ArgCount { get; }

		/// <summary>Gets an array of arguments passed to the method.</summary>
		/// <returns>An <see cref="T:System.Object" /> array containing the arguments passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06002566 RID: 9574
		object[] Args { get; }

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</summary>
		/// <returns>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06002567 RID: 9575
		LogicalCallContext LogicalCallContext { get; }

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06002568 RID: 9576
		MethodBase MethodBase { get; }

		/// <summary>Gets the name of the invoked method.</summary>
		/// <returns>The name of the invoked method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06002569 RID: 9577
		string MethodName { get; }

		/// <summary>Gets an object containing the method signature.</summary>
		/// <returns>An object containing the method signature.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600256A RID: 9578
		object MethodSignature { get; }

		/// <summary>Gets the full <see cref="T:System.Type" /> name of the specific object that the call is destined for.</summary>
		/// <returns>The full <see cref="T:System.Type" /> name of the specific object that the call is destined for.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600256B RID: 9579
		string TypeName { get; }

		/// <summary>Gets the URI of the specific object that the call is destined for.</summary>
		/// <returns>The URI of the remote object that contains the invoked method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600256C RID: 9580
		string Uri { get; }

		/// <summary>Gets a specific argument as an <see cref="T:System.Object" />.</summary>
		/// <returns>The argument passed to the method.</returns>
		/// <param name="argNum">The number of the requested argument. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x0600256D RID: 9581
		object GetArg(int argNum);
	}
}
