using System;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the constraints set on a certificate. This class cannot be inherited.</summary>
	// Token: 0x020001BB RID: 443
	public sealed class X509BasicConstraintsExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension" /> class.</summary>
		// Token: 0x06000A57 RID: 2647 RVA: 0x0003531C File Offset: 0x0003351C
		public X509BasicConstraintsExtension()
		{
			this._oid = new Oid("2.5.29.19", "Basic Constraints");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object and a value that identifies whether the extension is critical. </summary>
		/// <param name="encodedBasicConstraints">The encoded data to use to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06000A58 RID: 2648 RVA: 0x0003533C File Offset: 0x0003353C
		public X509BasicConstraintsExtension(AsnEncodedData encodedBasicConstraints, bool critical)
		{
			this._oid = new Oid("2.5.29.19", "Basic Constraints");
			this._raw = encodedBasicConstraints.RawData;
			base.Critical = critical;
			this._status = this.Decode(base.RawData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension" /> class. Parameters specify a value that indicates whether a certificate is a certificate authority (CA) certificate, a value that indicates whether the certificate has a restriction on the number of path levels it allows, the number of levels allowed in a certificate's path, and a value that indicates whether the extension is critical.  </summary>
		/// <param name="certificateAuthority">true if the certificate is a certificate authority (CA) certificate; otherwise, false.</param>
		/// <param name="hasPathLengthConstraint">true if the certificate has a restriction on the number of path levels it allows; otherwise, false.</param>
		/// <param name="pathLengthConstraint">The number of levels allowed in a certificate's path.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06000A59 RID: 2649 RVA: 0x0003538C File Offset: 0x0003358C
		public X509BasicConstraintsExtension(bool certificateAuthority, bool hasPathLengthConstraint, int pathLengthConstraint, bool critical)
		{
			if (hasPathLengthConstraint)
			{
				if (pathLengthConstraint < 0)
				{
					throw new ArgumentOutOfRangeException("pathLengthConstraint");
				}
				this._pathLengthConstraint = pathLengthConstraint;
			}
			this._hasPathLengthConstraint = hasPathLengthConstraint;
			this._certificateAuthority = certificateAuthority;
			this._oid = new Oid("2.5.29.19", "Basic Constraints");
			base.Critical = critical;
			base.RawData = this.Encode();
		}

		/// <summary>Gets a value indicating whether a certificate is a certificate authority (CA) certificate.</summary>
		/// <returns>true if the certificate is a certificate authority (CA) certificate, otherwise, false.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x000353F0 File Offset: 0x000335F0
		public bool CertificateAuthority
		{
			get
			{
				AsnDecodeStatus status = this._status;
				if (status == AsnDecodeStatus.Ok || status == AsnDecodeStatus.InformationNotAvailable)
				{
					return this._certificateAuthority;
				}
				throw new CryptographicException("Badly encoded extension.");
			}
		}

		/// <summary>Gets a value indicating whether a certificate has a restriction on the number of path levels it allows.</summary>
		/// <returns>true if the certificate has a restriction on the number of path levels it allows, otherwise, false.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The extension cannot be decoded. </exception>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0003541C File Offset: 0x0003361C
		public bool HasPathLengthConstraint
		{
			get
			{
				AsnDecodeStatus status = this._status;
				if (status == AsnDecodeStatus.Ok || status == AsnDecodeStatus.InformationNotAvailable)
				{
					return this._hasPathLengthConstraint;
				}
				throw new CryptographicException("Badly encoded extension.");
			}
		}

		/// <summary>Gets the number of levels allowed in a certificate's path.</summary>
		/// <returns>An integer indicating the number of levels allowed in a certificate's path.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The extension cannot be decoded. </exception>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00035448 File Offset: 0x00033648
		public int PathLengthConstraint
		{
			get
			{
				AsnDecodeStatus status = this._status;
				if (status == AsnDecodeStatus.Ok || status == AsnDecodeStatus.InformationNotAvailable)
				{
					return this._pathLengthConstraint;
				}
				throw new CryptographicException("Badly encoded extension.");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The encoded data to use to create the extension.</param>
		// Token: 0x06000A5D RID: 2653 RVA: 0x00035474 File Offset: 0x00033674
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(global::Locale.GetText("Wrong type."), "asnEncodedData");
			}
			if (x509Extension._oid == null)
			{
				this._oid = new Oid("2.5.29.19", "Basic Constraints");
			}
			else
			{
				this._oid = new Oid(x509Extension._oid);
			}
			base.RawData = x509Extension.RawData;
			base.Critical = x509Extension.Critical;
			this._status = this.Decode(base.RawData);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00035508 File Offset: 0x00033708
		internal AsnDecodeStatus Decode(byte[] extension)
		{
			if (extension == null || extension.Length == 0)
			{
				return AsnDecodeStatus.BadAsn;
			}
			if (extension[0] != 48)
			{
				return AsnDecodeStatus.BadTag;
			}
			if (extension.Length < 3 && (extension.Length != 2 || extension[1] != 0))
			{
				return AsnDecodeStatus.BadLength;
			}
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(extension);
				int num = 0;
				Mono.Security.ASN1 asn2 = asn[num++];
				if (asn2 != null && asn2.Tag == 1)
				{
					this._certificateAuthority = asn2.Value[0] == byte.MaxValue;
					asn2 = asn[num++];
				}
				if (asn2 != null && asn2.Tag == 2)
				{
					this._hasPathLengthConstraint = true;
					this._pathLengthConstraint = ASN1Convert.ToInt32(asn2);
				}
			}
			catch
			{
				return AsnDecodeStatus.BadAsn;
			}
			return AsnDecodeStatus.Ok;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000355B8 File Offset: 0x000337B8
		internal byte[] Encode()
		{
			Mono.Security.ASN1 asn = new Mono.Security.ASN1(48);
			if (this._certificateAuthority)
			{
				asn.Add(new Mono.Security.ASN1(1, new byte[] { byte.MaxValue }));
			}
			if (this._hasPathLengthConstraint)
			{
				if (this._pathLengthConstraint == 0)
				{
					asn.Add(new Mono.Security.ASN1(2, new byte[1]));
				}
				else
				{
					asn.Add(ASN1Convert.FromInt32(this._pathLengthConstraint));
				}
			}
			return asn.GetBytes();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00035630 File Offset: 0x00033830
		internal override string ToString(bool multiLine)
		{
			switch (this._status)
			{
			case AsnDecodeStatus.BadAsn:
				return string.Empty;
			case AsnDecodeStatus.BadTag:
			case AsnDecodeStatus.BadLength:
				return base.FormatUnkownData(this._raw);
			case AsnDecodeStatus.InformationNotAvailable:
				return "Information Not Available";
			default:
			{
				if (this._oid.Value != "2.5.29.19")
				{
					return string.Format("Unknown Key Usage ({0})", this._oid.Value);
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Subject Type=");
				if (this._certificateAuthority)
				{
					stringBuilder.Append("CA");
				}
				else
				{
					stringBuilder.Append("End Entity");
				}
				if (multiLine)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				else
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Path Length Constraint=");
				if (this._hasPathLengthConstraint)
				{
					stringBuilder.Append(this._pathLengthConstraint);
				}
				else
				{
					stringBuilder.Append("None");
				}
				if (multiLine)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x04000818 RID: 2072
		internal const string oid = "2.5.29.19";

		// Token: 0x04000819 RID: 2073
		internal const string friendlyName = "Basic Constraints";

		// Token: 0x0400081A RID: 2074
		private bool _certificateAuthority;

		// Token: 0x0400081B RID: 2075
		private bool _hasPathLengthConstraint;

		// Token: 0x0400081C RID: 2076
		private int _pathLengthConstraint;

		// Token: 0x0400081D RID: 2077
		private AsnDecodeStatus _status;
	}
}
