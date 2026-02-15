using System;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace System.Security.Principal
{
	/// <summary>Represents a generic principal.</summary>
	// Token: 0x020003D4 RID: 980
	[ComVisible(true)]
	[Serializable]
	public class GenericPrincipal : ClaimsPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericPrincipal" /> class from a user identity and an array of role names to which the user represented by that identity belongs.</summary>
		/// <param name="identity">A basic implementation of <see cref="T:System.Security.Principal.IIdentity" /> that represents any user. </param>
		/// <param name="roles">An array of role names to which the user represented by the <paramref name="identity" /> parameter belongs. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null. </exception>
		// Token: 0x06002153 RID: 8531 RVA: 0x0008A654 File Offset: 0x00088854
		public GenericPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identity = identity;
			if (roles != null)
			{
				this.m_roles = new string[roles.Length];
				for (int i = 0; i < roles.Length; i++)
				{
					this.m_roles[i] = roles[i];
				}
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x0008A6A6 File Offset: 0x000888A6
		internal string[] Roles
		{
			get
			{
				return this.m_roles;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.GenericIdentity" /> of the user represented by the current <see cref="T:System.Security.Principal.GenericPrincipal" />.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.GenericIdentity" /> of the user represented by the <see cref="T:System.Security.Principal.GenericPrincipal" />.</returns>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x0008A6AE File Offset: 0x000888AE
		public override IIdentity Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x04000F9F RID: 3999
		private IIdentity m_identity;

		// Token: 0x04000FA0 RID: 4000
		private string[] m_roles;
	}
}
