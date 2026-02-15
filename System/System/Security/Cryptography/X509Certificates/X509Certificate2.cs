using System;
using System.Runtime.Serialization;
using System.Text;
using Internal.Cryptography;
using Microsoft.Win32.SafeHandles;
using Mono;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X.509 certificate.  </summary>
	// Token: 0x020001BC RID: 444
	[Serializable]
	public class X509Certificate2 : X509Certificate
	{
		/// <summary>Resets the state of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		// Token: 0x06000A61 RID: 2657 RVA: 0x0003573C File Offset: 0x0003393C
		public override void Reset()
		{
			this.lazyRawData = null;
			this.lazySignatureAlgorithm = null;
			this.lazyVersion = 0;
			this.lazySubjectName = null;
			this.lazyIssuerName = null;
			this.lazyPublicKey = null;
			this.lazyPrivateKey = null;
			this.lazyExtensions = null;
			base.Reset();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class.</summary>
		// Token: 0x06000A62 RID: 2658 RVA: 0x00035797 File Offset: 0x00033997
		public X509Certificate2()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using information from a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06000A63 RID: 2659 RVA: 0x000357A0 File Offset: 0x000339A0
		public X509Certificate2(byte[] rawData)
			: base(rawData)
		{
			if (rawData != null && rawData.Length != 0)
			{
				using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(null))
				{
					X509CertificateImpl x509CertificateImpl = X509Helper.Import(rawData, safePasswordHandle, X509KeyStorageFlags.DefaultKeySet);
					base.ImportHandle(x509CertificateImpl);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06000A64 RID: 2660 RVA: 0x000357F0 File Offset: 0x000339F0
		public X509Certificate2(byte[] rawData, string password)
			: base(rawData, password)
		{
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000357FA File Offset: 0x000339FA
		internal X509Certificate2(X509Certificate2Impl impl)
			: base(impl)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06000A66 RID: 2662 RVA: 0x00035803 File Offset: 0x00033A03
		public X509Certificate2(string fileName)
			: base(fileName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06000A67 RID: 2663 RVA: 0x0003580C File Offset: 0x00033A0C
		public X509Certificate2(X509Certificate certificate)
			: base(certificate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using the specified serialization and stream context information. </summary>
		/// <param name="info">The serialization information required to deserialize the new <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />.</param>
		/// <param name="context">Contextual information about the source of the stream to be deserialized.</param>
		// Token: 0x06000A68 RID: 2664 RVA: 0x00035815 File Offset: 0x00033A15
		protected X509Certificate2(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00035824 File Offset: 0x00033A24
		public X509ExtensionCollection Extensions
		{
			get
			{
				base.ThrowIfInvalid();
				X509ExtensionCollection x509ExtensionCollection = this.lazyExtensions;
				if (x509ExtensionCollection == null)
				{
					x509ExtensionCollection = new X509ExtensionCollection();
					foreach (X509Extension x509Extension in this.Impl.Extensions)
					{
						X509Extension x509Extension2 = X509Certificate2.CreateCustomExtensionIfAny(x509Extension.Oid);
						if (x509Extension2 == null)
						{
							x509ExtensionCollection.Add(x509Extension);
						}
						else
						{
							x509Extension2.CopyFrom(x509Extension);
							x509ExtensionCollection.Add(x509Extension2);
						}
					}
					this.lazyExtensions = x509ExtensionCollection;
				}
				return x509ExtensionCollection;
			}
		}

		/// <summary>Gets a value that indicates whether an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object contains a private key. </summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object contains a private key; otherwise, false. </returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x000358BC File Offset: 0x00033ABC
		public bool HasPrivateKey
		{
			get
			{
				base.ThrowIfInvalid();
				return this.Impl.HasPrivateKey;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object that represents the private key associated with a certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object, which is either an RSA or DSA cryptographic service provider.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key value is not an RSA or DSA key, or the key is unreadable. </exception>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The key algorithm for this private key is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The X.509 keys do not match.</exception>
		/// <exception cref="T:System.ArgumentException">The cryptographic service provider key is null.</exception>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x000358D0 File Offset: 0x00033AD0
		public AsymmetricAlgorithm PrivateKey
		{
			get
			{
				base.ThrowIfInvalid();
				if (!this.HasPrivateKey)
				{
					return null;
				}
				if (this.lazyPrivateKey == null)
				{
					string keyAlgorithm = this.GetKeyAlgorithm();
					if (!(keyAlgorithm == "1.2.840.113549.1.1.1"))
					{
						if (!(keyAlgorithm == "1.2.840.10040.4.1"))
						{
							throw new NotSupportedException("The certificate key algorithm is not supported.");
						}
						this.lazyPrivateKey = this.Impl.GetDSAPrivateKey();
					}
					else
					{
						this.lazyPrivateKey = this.Impl.GetRSAPrivateKey();
					}
				}
				return this.lazyPrivateKey;
			}
		}

		/// <summary>Gets the distinguished name of the certificate issuer.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object that contains the name of the certificate issuer.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00035958 File Offset: 0x00033B58
		public X500DistinguishedName IssuerName
		{
			get
			{
				base.ThrowIfInvalid();
				X500DistinguishedName x500DistinguishedName = this.lazyIssuerName;
				if (x500DistinguishedName == null)
				{
					x500DistinguishedName = (this.lazyIssuerName = this.Impl.IssuerName);
				}
				return x500DistinguishedName;
			}
		}

		/// <summary>Gets the date in local time after which a certificate is no longer valid.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the expiration date for the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0003598F File Offset: 0x00033B8F
		public DateTime NotAfter
		{
			get
			{
				return base.GetNotAfter();
			}
		}

		/// <summary>Gets the date in local time on which a certificate becomes valid.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the effective date of the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00035997 File Offset: 0x00033B97
		public DateTime NotBefore
		{
			get
			{
				return base.GetNotBefore();
			}
		}

		/// <summary>Gets a <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.PublicKey" /> object associated with a certificate.</summary>
		/// <returns>A <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.PublicKey" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key value is not an RSA or DSA key, or the key is unreadable. </exception>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x000359A0 File Offset: 0x00033BA0
		public PublicKey PublicKey
		{
			get
			{
				base.ThrowIfInvalid();
				PublicKey publicKey = this.lazyPublicKey;
				if (publicKey == null)
				{
					string keyAlgorithm = this.GetKeyAlgorithm();
					byte[] keyAlgorithmParameters = this.GetKeyAlgorithmParameters();
					byte[] publicKey2 = this.GetPublicKey();
					Oid oid = new Oid(keyAlgorithm);
					publicKey = (this.lazyPublicKey = new PublicKey(oid, new AsnEncodedData(oid, keyAlgorithmParameters), new AsnEncodedData(oid, publicKey2)));
				}
				return publicKey;
			}
		}

		/// <summary>Gets the raw data of a certificate.</summary>
		/// <returns>The raw data of the certificate as a byte array.</returns>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x000359FC File Offset: 0x00033BFC
		public byte[] RawData
		{
			get
			{
				base.ThrowIfInvalid();
				byte[] array = this.lazyRawData;
				if (array == null)
				{
					array = (this.lazyRawData = this.Impl.RawData);
				}
				return array.CloneByteArray();
			}
		}

		/// <summary>Gets the serial number of a certificate.</summary>
		/// <returns>The serial number of the certificate.</returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00035A38 File Offset: 0x00033C38
		public string SerialNumber
		{
			get
			{
				return this.GetSerialNumberString();
			}
		}

		/// <summary>Gets the algorithm used to create the signature of a certificate.</summary>
		/// <returns>Returns the object identifier (<see cref="T:System.Security.Cryptography.Oid" />) of the signature algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00035A40 File Offset: 0x00033C40
		public Oid SignatureAlgorithm
		{
			get
			{
				base.ThrowIfInvalid();
				Oid oid = this.lazySignatureAlgorithm;
				if (oid == null)
				{
					string signatureAlgorithm = this.Impl.SignatureAlgorithm;
					oid = (this.lazySignatureAlgorithm = Oid.FromOidValue(signatureAlgorithm, OidGroup.SignatureAlgorithm));
				}
				return oid;
			}
		}

		/// <summary>Gets the subject distinguished name from a certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object that represents the name of the certificate subject.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00035A80 File Offset: 0x00033C80
		public X500DistinguishedName SubjectName
		{
			get
			{
				base.ThrowIfInvalid();
				X500DistinguishedName x500DistinguishedName = this.lazySubjectName;
				if (x500DistinguishedName == null)
				{
					x500DistinguishedName = (this.lazySubjectName = this.Impl.SubjectName);
				}
				return x500DistinguishedName;
			}
		}

		/// <summary>Gets the thumbprint of a certificate.</summary>
		/// <returns>The thumbprint of the certificate.</returns>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00035AB7 File Offset: 0x00033CB7
		public string Thumbprint
		{
			get
			{
				return this.GetCertHash().ToHexStringUpper();
			}
		}

		/// <summary>Gets the X.509 format version of a certificate.</summary>
		/// <returns>The certificate format.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00035AC4 File Offset: 0x00033CC4
		public int Version
		{
			get
			{
				base.ThrowIfInvalid();
				int num = this.lazyVersion;
				if (num == 0)
				{
					num = (this.lazyVersion = this.Impl.Version);
				}
				return num;
			}
		}

		/// <summary>Indicates the type of certificate contained in a byte array.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</returns>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rawData" /> has a zero length or is null. </exception>
		// Token: 0x06000A76 RID: 2678 RVA: 0x00035AFB File Offset: 0x00033CFB
		public static X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("Array cannot be empty or null.", "rawData");
			}
			return X509Pal.Instance.GetCertContentType(rawData);
		}

		/// <summary>Gets the subject and issuer names from a certificate.</summary>
		/// <returns>The name of the certificate.</returns>
		/// <param name="nameType">The <see cref="T:System.Security.Cryptography.X509Certificates.X509NameType" /> value for the subject. </param>
		/// <param name="forIssuer">true to include the issuer name; otherwise, false. </param>
		// Token: 0x06000A77 RID: 2679 RVA: 0x00035B1F File Offset: 0x00033D1F
		public string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			return this.Impl.GetNameInfo(nameType, forIssuer);
		}

		/// <summary>Displays an X.509 certificate in text format.</summary>
		/// <returns>The certificate information.</returns>
		// Token: 0x06000A78 RID: 2680 RVA: 0x00035B2E File Offset: 0x00033D2E
		public override string ToString()
		{
			return base.ToString(true);
		}

		/// <summary>Displays an X.509 certificate in text format.</summary>
		/// <returns>The certificate information.</returns>
		/// <param name="verbose">true to display the public key, private key, extensions, and so forth; false to display information that is similar to the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class, including thumbprint, serial number, subject and issuer names, and so on. </param>
		// Token: 0x06000A79 RID: 2681 RVA: 0x00035B38 File Offset: 0x00033D38
		public override string ToString(bool verbose)
		{
			if (!verbose || !base.IsValid)
			{
				return this.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[Version]");
			stringBuilder.Append("  V");
			stringBuilder.Append(this.Version);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Subject]");
			stringBuilder.Append("  ");
			stringBuilder.Append(this.SubjectName.Name);
			string text = this.GetNameInfo(X509NameType.SimpleName, false);
			if (text.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Simple Name: ");
				stringBuilder.Append(text);
			}
			string text2 = this.GetNameInfo(X509NameType.EmailName, false);
			if (text2.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Email Name: ");
				stringBuilder.Append(text2);
			}
			string text3 = this.GetNameInfo(X509NameType.UpnName, false);
			if (text3.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("UPN Name: ");
				stringBuilder.Append(text3);
			}
			string text4 = this.GetNameInfo(X509NameType.DnsName, false);
			if (text4.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("DNS Name: ");
				stringBuilder.Append(text4);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Issuer]");
			stringBuilder.Append("  ");
			stringBuilder.Append(this.IssuerName.Name);
			text = this.GetNameInfo(X509NameType.SimpleName, true);
			if (text.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Simple Name: ");
				stringBuilder.Append(text);
			}
			text2 = this.GetNameInfo(X509NameType.EmailName, true);
			if (text2.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Email Name: ");
				stringBuilder.Append(text2);
			}
			text3 = this.GetNameInfo(X509NameType.UpnName, true);
			if (text3.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("UPN Name: ");
				stringBuilder.Append(text3);
			}
			text4 = this.GetNameInfo(X509NameType.DnsName, true);
			if (text4.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("DNS Name: ");
				stringBuilder.Append(text4);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Serial Number]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(this.SerialNumber);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Not Before]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(X509Certificate.FormatDate(this.NotBefore));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Not After]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(X509Certificate.FormatDate(this.NotAfter));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Thumbprint]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(this.Thumbprint);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Signature Algorithm]");
			stringBuilder.Append("  ");
			stringBuilder.Append(this.SignatureAlgorithm.FriendlyName);
			stringBuilder.Append('(');
			stringBuilder.Append(this.SignatureAlgorithm.Value);
			stringBuilder.AppendLine(")");
			stringBuilder.AppendLine();
			stringBuilder.Append("[Public Key]");
			try
			{
				PublicKey publicKey = this.PublicKey;
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Algorithm: ");
				stringBuilder.Append(publicKey.Oid.FriendlyName);
				try
				{
					stringBuilder.AppendLine();
					stringBuilder.Append("  ");
					stringBuilder.Append("Length: ");
					using (RSA rsapublicKey = this.GetRSAPublicKey())
					{
						if (rsapublicKey != null)
						{
							stringBuilder.Append(rsapublicKey.KeySize);
						}
					}
				}
				catch (NotSupportedException)
				{
				}
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Key Blob: ");
				stringBuilder.AppendLine(publicKey.EncodedKeyValue.Format(true));
				stringBuilder.Append("  ");
				stringBuilder.Append("Parameters: ");
				stringBuilder.Append(publicKey.EncodedParameters.Format(true));
			}
			catch (CryptographicException)
			{
			}
			this.Impl.AppendPrivateKeyInfo(stringBuilder);
			X509ExtensionCollection extensions = this.Extensions;
			if (extensions.Count > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.Append("[Extensions]");
				foreach (X509Extension x509Extension in extensions)
				{
					try
					{
						stringBuilder.AppendLine();
						stringBuilder.Append("* ");
						stringBuilder.Append(x509Extension.Oid.FriendlyName);
						stringBuilder.Append('(');
						stringBuilder.Append(x509Extension.Oid.Value);
						stringBuilder.Append("):");
						stringBuilder.AppendLine();
						stringBuilder.Append("  ");
						stringBuilder.Append(x509Extension.Format(true));
					}
					catch (CryptographicException)
					{
					}
				}
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		/// <summary>Performs a X.509 chain validation using basic validation policy.</summary>
		/// <returns>true if the validation succeeds; false if the validation fails.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x06000A7A RID: 2682 RVA: 0x000360E8 File Offset: 0x000342E8
		public bool Verify()
		{
			return this.Impl.Verify(this);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000360F8 File Offset: 0x000342F8
		private static X509Extension CreateCustomExtensionIfAny(Oid oid)
		{
			string value = oid.Value;
			if (!(value == "2.5.29.10"))
			{
				if (value == "2.5.29.19")
				{
					return new X509BasicConstraintsExtension();
				}
				if (value == "2.5.29.15")
				{
					return new X509KeyUsageExtension();
				}
				if (value == "2.5.29.37")
				{
					return new X509EnhancedKeyUsageExtension();
				}
				if (!(value == "2.5.29.14"))
				{
					return null;
				}
				return new X509SubjectKeyIdentifierExtension();
			}
			else
			{
				if (!X509Pal.Instance.SupportsLegacyBasicConstraintsExtension)
				{
					return null;
				}
				return new X509BasicConstraintsExtension();
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0003617C File Offset: 0x0003437C
		internal new X509Certificate2Impl Impl
		{
			get
			{
				X509Certificate2Impl x509Certificate2Impl = base.Impl as X509Certificate2Impl;
				X509Helper.ThrowIfContextInvalid(x509Certificate2Impl);
				return x509Certificate2Impl;
			}
		}

		// Token: 0x0400081E RID: 2078
		private volatile byte[] lazyRawData;

		// Token: 0x0400081F RID: 2079
		private volatile Oid lazySignatureAlgorithm;

		// Token: 0x04000820 RID: 2080
		private volatile int lazyVersion;

		// Token: 0x04000821 RID: 2081
		private volatile X500DistinguishedName lazySubjectName;

		// Token: 0x04000822 RID: 2082
		private volatile X500DistinguishedName lazyIssuerName;

		// Token: 0x04000823 RID: 2083
		private volatile PublicKey lazyPublicKey;

		// Token: 0x04000824 RID: 2084
		private volatile AsymmetricAlgorithm lazyPrivateKey;

		// Token: 0x04000825 RID: 2085
		private volatile X509ExtensionCollection lazyExtensions;
	}
}
