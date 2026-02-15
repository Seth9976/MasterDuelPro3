using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Gathers naming information from the context property and determines whether the new context is ok for the context property.</summary>
	// Token: 0x02000446 RID: 1094
	[ComVisible(true)]
	public interface IContextProperty
	{
		/// <summary>Gets the name of the property under which it will be added to the context.</summary>
		/// <returns>The name of the property.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06002443 RID: 9283
		string Name { get; }

		/// <summary>Called when the context is frozen.</summary>
		/// <param name="newContext">The context to freeze. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002444 RID: 9284
		void Freeze(Context newContext);

		/// <summary>Returns a Boolean value indicating whether the context property is compatible with the new context.</summary>
		/// <returns>true if the context property can coexist with the other context properties in the given context; otherwise, false.</returns>
		/// <param name="newCtx">The new context in which the <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> has been created. </param>
		// Token: 0x06002445 RID: 9285
		bool IsNewContextOK(Context newCtx);
	}
}
