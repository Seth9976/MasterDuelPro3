using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents the chain policy to be applied when building an X509 certificate chain. This class cannot be inherited.</summary>
	// Token: 0x020001CB RID: 459
	public sealed class X509ChainPolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> class. </summary>
		// Token: 0x06000B2E RID: 2862 RVA: 0x00038985 File Offset: 0x00036B85
		public X509ChainPolicy()
		{
			this.Reset();
		}

		/// <summary>Represents an additional collection of certificates that can be searched by the chaining engine when validating a certificate chain.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00038994 File Offset: 0x00036B94
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00038A1C File Offset: 0x00036C1C
		public X509Certificate2Collection ExtraStore
		{
			get
			{
				if (this.store2 != null)
				{
					return this.store2;
				}
				this.store2 = new X509Certificate2Collection();
				if (this.store != null)
				{
					foreach (X509Certificate x509Certificate in this.store)
					{
						this.store2.Add(new X509Certificate2(x509Certificate));
					}
				}
				return this.store2;
			}
			internal set
			{
				this.store2 = value;
			}
		}

		/// <summary>Gets or sets values for X509 revocation flags.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> value supplied is not a valid flag. </exception>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00038A25 File Offset: 0x00036C25
		public X509RevocationFlag RevocationFlag
		{
			get
			{
				return this.rflag;
			}
		}

		/// <summary>Gets or sets values for X509 certificate revocation mode.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> value supplied is not a valid flag. </exception>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00038A2D File Offset: 0x00036C2D
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x00038A35 File Offset: 0x00036C35
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (value < X509RevocationMode.NoCheck || value > X509RevocationMode.Offline)
				{
					throw new ArgumentException("RevocationMode");
				}
				this.mode = value;
			}
		}

		/// <summary>Gets verification flags for the certificate.</summary>
		/// <returns>A value from the <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> value supplied is not a valid flag. <see cref="F:System.Security.Cryptography.X509Certificates.X509VerificationFlags.NoFlag" /> is the default value. </exception>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00038A51 File Offset: 0x00036C51
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x00038A59 File Offset: 0x00036C59
		public X509VerificationFlags VerificationFlags
		{
			get
			{
				return this.vflags;
			}
			set
			{
				if ((value | X509VerificationFlags.AllFlags) != X509VerificationFlags.AllFlags)
				{
					throw new ArgumentException("VerificationFlags");
				}
				this.vflags = value;
			}
		}

		/// <summary>The time that the certificate was verified expressed in local time.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00038A7B File Offset: 0x00036C7B
		public DateTime VerificationTime
		{
			get
			{
				return this.vtime;
			}
		}

		/// <summary>Resets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> members to their default values.</summary>
		// Token: 0x06000B37 RID: 2871 RVA: 0x00038A84 File Offset: 0x00036C84
		public void Reset()
		{
			this.apps = new OidCollection();
			this.cert = new OidCollection();
			this.store2 = null;
			this.rflag = X509RevocationFlag.ExcludeRoot;
			this.mode = X509RevocationMode.Online;
			this.timeout = TimeSpan.Zero;
			this.vflags = X509VerificationFlags.NoFlag;
			this.vtime = DateTime.Now;
		}

		// Token: 0x04000847 RID: 2119
		private OidCollection apps;

		// Token: 0x04000848 RID: 2120
		private OidCollection cert;

		// Token: 0x04000849 RID: 2121
		private X509CertificateCollection store;

		// Token: 0x0400084A RID: 2122
		private X509Certificate2Collection store2;

		// Token: 0x0400084B RID: 2123
		private X509RevocationFlag rflag;

		// Token: 0x0400084C RID: 2124
		private X509RevocationMode mode;

		// Token: 0x0400084D RID: 2125
		private TimeSpan timeout;

		// Token: 0x0400084E RID: 2126
		private X509VerificationFlags vflags;

		// Token: 0x0400084F RID: 2127
		private DateTime vtime;
	}
}
