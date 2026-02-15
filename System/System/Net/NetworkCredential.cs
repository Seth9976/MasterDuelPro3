using System;
using System.Security;

namespace System.Net
{
	/// <summary>Provides credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.</summary>
	// Token: 0x020003B7 RID: 951
	public class NetworkCredential : ICredentials
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class.</summary>
		// Token: 0x06001797 RID: 6039 RVA: 0x00063EDF File Offset: 0x000620DF
		public NetworkCredential()
			: this(string.Empty, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials. </param>
		/// <param name="password">The password for the user name associated with the credentials. </param>
		// Token: 0x06001798 RID: 6040 RVA: 0x00064A38 File Offset: 0x00062C38
		public NetworkCredential(string userName, string password)
			: this(userName, password, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials. </param>
		/// <param name="password">The password for the user name associated with the credentials. </param>
		/// <param name="domain">The domain associated with these credentials. </param>
		// Token: 0x06001799 RID: 6041 RVA: 0x00064A47 File Offset: 0x00062C47
		public NetworkCredential(string userName, string password, string domain)
		{
			this.UserName = userName;
			this.Password = password;
			this.Domain = domain;
		}

		/// <summary>Gets or sets the user name associated with the credentials.</summary>
		/// <returns>The user name associated with the credentials.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00064A64 File Offset: 0x00062C64
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x00064A6C File Offset: 0x00062C6C
		public string UserName
		{
			get
			{
				return this.InternalGetUserName();
			}
			set
			{
				if (value == null)
				{
					this.m_userName = string.Empty;
					return;
				}
				this.m_userName = value;
			}
		}

		/// <summary>Gets or sets the password for the user name associated with the credentials.</summary>
		/// <returns>The password associated with the credentials. If this <see cref="T:System.Net.NetworkCredential" /> instance was initialized with the <paramref name="password" /> parameter set to null, then the <see cref="P:System.Net.NetworkCredential.Password" /> property will return an empty string.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00064A84 File Offset: 0x00062C84
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x00064A8C File Offset: 0x00062C8C
		public string Password
		{
			get
			{
				return this.InternalGetPassword();
			}
			set
			{
				this.m_password = UnsafeNclNativeMethods.SecureStringHelper.CreateSecureString(value);
			}
		}

		/// <summary>Gets or sets the domain or computer name that verifies the credentials.</summary>
		/// <returns>The name of the domain associated with the credentials.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00064A9A File Offset: 0x00062C9A
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x00064AA2 File Offset: 0x00062CA2
		public string Domain
		{
			get
			{
				return this.InternalGetDomain();
			}
			set
			{
				if (value == null)
				{
					this.m_domain = string.Empty;
					return;
				}
				this.m_domain = value;
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00064ABA File Offset: 0x00062CBA
		internal string InternalGetUserName()
		{
			return this.m_userName;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00064AC2 File Offset: 0x00062CC2
		internal string InternalGetPassword()
		{
			return UnsafeNclNativeMethods.SecureStringHelper.CreateString(this.m_password);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00064ACF File Offset: 0x00062CCF
		internal string InternalGetDomain()
		{
			return this.m_domain;
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> object.</returns>
		/// <param name="uri">The URI that the client provides authentication for. </param>
		/// <param name="authType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property. </param>
		// Token: 0x060017A3 RID: 6051 RVA: 0x0001AE3D File Offset: 0x0001903D
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			return this;
		}

		// Token: 0x04000ED9 RID: 3801
		private string m_domain;

		// Token: 0x04000EDA RID: 3802
		private string m_userName;

		// Token: 0x04000EDB RID: 3803
		private SecureString m_password;
	}
}
