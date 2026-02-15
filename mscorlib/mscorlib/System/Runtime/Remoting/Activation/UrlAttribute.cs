using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Defines an attribute that can be used at the call site to specify the URL where the activation will happen. This class cannot be inherited.</summary>
	// Token: 0x0200046A RID: 1130
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlAttribute : ContextAttribute
	{
		/// <summary>Gets the URL value of the <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</summary>
		/// <returns>The URL value of the <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00096704 File Offset: 0x00094904
		public string UrlValue
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x040011A1 RID: 4513
		private string url;
	}
}
