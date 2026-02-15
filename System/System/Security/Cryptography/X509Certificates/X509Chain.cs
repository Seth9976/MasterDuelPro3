using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a chain-building engine for <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificates.</summary>
	// Token: 0x020001C5 RID: 453
	public class X509Chain : IDisposable
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00037274 File Offset: 0x00035474
		internal X509ChainImpl Impl
		{
			get
			{
				X509Helper2.ThrowIfContextInvalid(this.impl);
				return this.impl;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class.</summary>
		// Token: 0x06000ADA RID: 2778 RVA: 0x00037287 File Offset: 0x00035487
		public X509Chain()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class specifying a value that indicates whether the machine context should be used.</summary>
		/// <param name="useMachineContext">true to use the machine context; false to use the current user context. </param>
		// Token: 0x06000ADB RID: 2779 RVA: 0x00037290 File Offset: 0x00035490
		public X509Chain(bool useMachineContext)
		{
			this.impl = X509Helper2.CreateChainImpl(useMachineContext);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000372A4 File Offset: 0x000354A4
		internal X509Chain(X509ChainImpl impl)
		{
			X509Helper2.ThrowIfContextInvalid(impl);
			this.impl = impl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class using an <see cref="T:System.IntPtr" /> handle to an X.509 chain.</summary>
		/// <param name="chainContext">An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chainContext" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="chainContext" /> parameter points to an invalid context.</exception>
		// Token: 0x06000ADD RID: 2781 RVA: 0x000372B9 File Offset: 0x000354B9
		[MonoTODO("Mono's X509Chain is fully managed. All handles are invalid.")]
		public X509Chain(IntPtr chainContext)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</returns>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x000372C6 File Offset: 0x000354C6
		public X509ChainElementCollection ChainElements
		{
			get
			{
				return this.Impl.ChainElements;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> to use when building an X.509 certificate chain.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> object associated with this X.509 chain.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is null.</exception>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x000372D3 File Offset: 0x000354D3
		public X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.Impl.ChainPolicy;
			}
		}

		/// <summary>Builds an X.509 chain using the policy specified in <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" />.</summary>
		/// <returns>true if the X.509 certificate is valid; otherwise, false.</returns>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="certificate" /> is not a valid certificate or is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> is unreadable. </exception>
		// Token: 0x06000AE0 RID: 2784 RVA: 0x000372E0 File Offset: 0x000354E0
		[MonoTODO("Not totally RFC3280 compliant, but neither is MS implementation...")]
		public bool Build(X509Certificate2 certificate)
		{
			return this.Impl.Build(certificate);
		}

		/// <summary>Clears the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		// Token: 0x06000AE1 RID: 2785 RVA: 0x000372EE File Offset: 0x000354EE
		public void Reset()
		{
			this.Impl.Reset();
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object after querying for the mapping defined in the CryptoConfig file, and maps the chain to that mapping.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000AE2 RID: 2786 RVA: 0x000372FB File Offset: 0x000354FB
		public static X509Chain Create()
		{
			return (X509Chain)CryptoConfig.CreateFromName("X509Chain");
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0003730C File Offset: 0x0003550C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0003731B File Offset: 0x0003551B
		protected virtual void Dispose(bool disposing)
		{
			if (this.impl != null)
			{
				this.impl.Dispose();
				this.impl = null;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00037338 File Offset: 0x00035538
		~X509Chain()
		{
			this.Dispose(false);
		}

		// Token: 0x04000830 RID: 2096
		private X509ChainImpl impl;
	}
}
