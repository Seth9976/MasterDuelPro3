using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace System.Security.Principal
{
	/// <summary>Represents a generic user.</summary>
	// Token: 0x020003CF RID: 975
	[Serializable]
	public class GenericIdentity : ClaimsIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class representing the user with the specified name and authentication type.</summary>
		/// <param name="name">The name of the user on whose behalf the code is running. </param>
		/// <param name="type">The type of authentication used to identify the user. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null.-or- The <paramref name="type" /> parameter is null. </exception>
		// Token: 0x06002149 RID: 8521 RVA: 0x0008A59F File Offset: 0x0008879F
		public GenericIdentity(string name, string type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.m_name = name;
			this.m_type = type;
			this.AddNameClaim();
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0008A5D7 File Offset: 0x000887D7
		private GenericIdentity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class by using the specified <see cref="T:System.Security.Principal.GenericIdentity" /> object.</summary>
		/// <param name="identity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.GenericIdentity" />.</param>
		// Token: 0x0600214B RID: 8523 RVA: 0x0008A5DF File Offset: 0x000887DF
		protected GenericIdentity(GenericIdentity identity)
			: base(identity)
		{
			this.m_name = identity.m_name;
			this.m_type = identity.m_type;
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x0600214C RID: 8524 RVA: 0x0008A600 File Offset: 0x00088800
		public override ClaimsIdentity Clone()
		{
			return new GenericIdentity(this);
		}

		/// <summary>Gets all claims for the user represented by this generic identity.</summary>
		/// <returns>A collection of claims for this <see cref="T:System.Security.Principal.GenericIdentity" /> object.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x0008A608 File Offset: 0x00088808
		public override IEnumerable<Claim> Claims
		{
			get
			{
				return base.Claims;
			}
		}

		/// <summary>Gets the user's name.</summary>
		/// <returns>The name of the user on whose behalf the code is being run.</returns>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x0008A610 File Offset: 0x00088810
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the type of authentication used to identify the user.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x0008A618 File Offset: 0x00088818
		public override string AuthenticationType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0008A620 File Offset: 0x00088820
		private void AddNameClaim()
		{
			if (this.m_name != null)
			{
				base.AddClaim(new Claim(base.NameClaimType, this.m_name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
		}

		// Token: 0x04000F93 RID: 3987
		private readonly string m_name;

		// Token: 0x04000F94 RID: 3988
		private readonly string m_type;
	}
}
