using System;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the collection of object identifiers (OIDs) that indicates the applications that use the key. This class cannot be inherited.</summary>
	// Token: 0x020001CD RID: 461
	public sealed class X509EnhancedKeyUsageExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class.</summary>
		// Token: 0x06000B3D RID: 2877 RVA: 0x00038C3F File Offset: 0x00036E3F
		public X509EnhancedKeyUsageExtension()
		{
			this._oid = new Oid("2.5.29.37", "Enhanced Key Usage");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object and a value that identifies whether the extension is critical.</summary>
		/// <param name="encodedEnhancedKeyUsages">The encoded data to use to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06000B3E RID: 2878 RVA: 0x00038C5C File Offset: 0x00036E5C
		public X509EnhancedKeyUsageExtension(AsnEncodedData encodedEnhancedKeyUsages, bool critical)
		{
			this._oid = new Oid("2.5.29.37", "Enhanced Key Usage");
			this._raw = encodedEnhancedKeyUsages.RawData;
			base.Critical = critical;
			this._status = this.Decode(base.RawData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.OidCollection" /> and a value that identifies whether the extension is critical. </summary>
		/// <param name="enhancedKeyUsages">An <see cref="T:System.Security.Cryptography.OidCollection" /> collection. </param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The specified <see cref="T:System.Security.Cryptography.OidCollection" />  contains one or more corrupt values.</exception>
		// Token: 0x06000B3F RID: 2879 RVA: 0x00038CAC File Offset: 0x00036EAC
		public X509EnhancedKeyUsageExtension(OidCollection enhancedKeyUsages, bool critical)
		{
			if (enhancedKeyUsages == null)
			{
				throw new ArgumentNullException("enhancedKeyUsages");
			}
			this._oid = new Oid("2.5.29.37", "Enhanced Key Usage");
			base.Critical = critical;
			this._enhKeyUsage = new OidCollection();
			foreach (Oid oid in enhancedKeyUsages)
			{
				this._enhKeyUsage.Add(oid);
			}
			base.RawData = this.Encode();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The encoded data to use to create the extension.</param>
		// Token: 0x06000B40 RID: 2880 RVA: 0x00038D28 File Offset: 0x00036F28
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("encodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(global::Locale.GetText("Wrong type."), "asnEncodedData");
			}
			if (x509Extension._oid == null)
			{
				this._oid = new Oid("2.5.29.37", "Enhanced Key Usage");
			}
			else
			{
				this._oid = new Oid(x509Extension._oid);
			}
			base.RawData = x509Extension.RawData;
			base.Critical = x509Extension.Critical;
			this._status = this.Decode(base.RawData);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00038DBC File Offset: 0x00036FBC
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
			if (this._enhKeyUsage == null)
			{
				this._enhKeyUsage = new OidCollection();
			}
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(extension);
				if (asn.Tag != 48)
				{
					throw new CryptographicException(global::Locale.GetText("Invalid ASN.1 Tag"));
				}
				for (int i = 0; i < asn.Count; i++)
				{
					this._enhKeyUsage.Add(new Oid(ASN1Convert.ToOid(asn[i])));
				}
			}
			catch
			{
				return AsnDecodeStatus.BadAsn;
			}
			return AsnDecodeStatus.Ok;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00038E58 File Offset: 0x00037058
		internal byte[] Encode()
		{
			Mono.Security.ASN1 asn = new Mono.Security.ASN1(48);
			foreach (Oid oid in this._enhKeyUsage)
			{
				asn.Add(ASN1Convert.FromOid(oid.Value));
			}
			return asn.GetBytes();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00038EA4 File Offset: 0x000370A4
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
				if (this._oid.Value != "2.5.29.37")
				{
					return string.Format("Unknown Key Usage ({0})", this._oid.Value);
				}
				if (this._enhKeyUsage.Count == 0)
				{
					return "Information Not Available";
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this._enhKeyUsage.Count; i++)
				{
					Oid oid = this._enhKeyUsage[i];
					if (oid.Value == "1.3.6.1.5.5.7.3.1")
					{
						stringBuilder.Append("Server Authentication (");
					}
					else
					{
						stringBuilder.Append("Unknown Key Usage (");
					}
					stringBuilder.Append(oid.Value);
					stringBuilder.Append(")");
					if (multiLine)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					else if (i != this._enhKeyUsage.Count - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x04000852 RID: 2130
		private OidCollection _enhKeyUsage;

		// Token: 0x04000853 RID: 2131
		private AsnDecodeStatus _status;
	}
}
