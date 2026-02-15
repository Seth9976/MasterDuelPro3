using System;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace System.Security.Principal
{
	/// <summary>Enables code to check the Windows group membership of a Windows user.</summary>
	// Token: 0x020003DE RID: 990
	[ComVisible(true)]
	[Serializable]
	public class WindowsPrincipal : ClaimsPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsPrincipal" /> class by using the specified <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <param name="ntIdentity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.WindowsPrincipal" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ntIdentity" /> is null. </exception>
		// Token: 0x060021A2 RID: 8610 RVA: 0x0008BEA2 File Offset: 0x0008A0A2
		public WindowsPrincipal(WindowsIdentity ntIdentity)
		{
			if (ntIdentity == null)
			{
				throw new ArgumentNullException("ntIdentity");
			}
			this._identity = ntIdentity;
		}

		/// <summary>Gets the identity of the current principal.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.WindowsIdentity" /> object of the current principal.</returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x0008BEBF File Offset: 0x0008A0BF
		public override IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x0400101B RID: 4123
		private WindowsIdentity _identity;
	}
}
