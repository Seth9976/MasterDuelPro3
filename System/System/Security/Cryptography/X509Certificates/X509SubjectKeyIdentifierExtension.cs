using System;
using System.Text;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines a string that identifies a certificate's subject key identifier (SKI). This class cannot be inherited.</summary>
	// Token: 0x020001D4 RID: 468
	public sealed class X509SubjectKeyIdentifierExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class.</summary>
		// Token: 0x06000B6E RID: 2926 RVA: 0x00039968 File Offset: 0x00037B68
		public X509SubjectKeyIdentifierExtension()
		{
			this._oid = new Oid("2.5.29.14", "Subject Key Identifier");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using encoded data and a value that identifies whether the extension is critical.</summary>
		/// <param name="encodedSubjectKeyIdentifier">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to use to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06000B6F RID: 2927 RVA: 0x00039988 File Offset: 0x00037B88
		public X509SubjectKeyIdentifierExtension(AsnEncodedData encodedSubjectKeyIdentifier, bool critical)
		{
			this._oid = new Oid("2.5.29.14", "Subject Key Identifier");
			this._raw = encodedSubjectKeyIdentifier.RawData;
			base.Critical = critical;
			this._status = this.Decode(base.RawData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using a byte array and a value that identifies whether the extension is critical.</summary>
		/// <param name="subjectKeyIdentifier">A byte array that represents data to use to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06000B70 RID: 2928 RVA: 0x000399D8 File Offset: 0x00037BD8
		public X509SubjectKeyIdentifierExtension(byte[] subjectKeyIdentifier, bool critical)
		{
			if (subjectKeyIdentifier == null)
			{
				throw new ArgumentNullException("subjectKeyIdentifier");
			}
			if (subjectKeyIdentifier.Length == 0)
			{
				throw new ArgumentException("subjectKeyIdentifier");
			}
			this._oid = new Oid("2.5.29.14", "Subject Key Identifier");
			base.Critical = critical;
			this._subjectKeyIdentifier = (byte[])subjectKeyIdentifier.Clone();
			base.RawData = this.Encode();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using a string and a value that identifies whether the extension is critical.</summary>
		/// <param name="subjectKeyIdentifier">A string, encoded in hexadecimal format, that represents the subject key identifier (SKI) for a certificate.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06000B71 RID: 2929 RVA: 0x00039A44 File Offset: 0x00037C44
		public X509SubjectKeyIdentifierExtension(string subjectKeyIdentifier, bool critical)
		{
			if (subjectKeyIdentifier == null)
			{
				throw new ArgumentNullException("subjectKeyIdentifier");
			}
			if (subjectKeyIdentifier.Length < 2)
			{
				throw new ArgumentException("subjectKeyIdentifier");
			}
			this._oid = new Oid("2.5.29.14", "Subject Key Identifier");
			base.Critical = critical;
			this._subjectKeyIdentifier = X509SubjectKeyIdentifierExtension.FromHex(subjectKeyIdentifier);
			base.RawData = this.Encode();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using a public key and a value indicating whether the extension is critical.</summary>
		/// <param name="key">A <see cref="T:System.Security.Cryptography.X509Certificates.PublicKey" />  object to create a subject key identifier (SKI) from. </param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06000B72 RID: 2930 RVA: 0x00039AAD File Offset: 0x00037CAD
		public X509SubjectKeyIdentifierExtension(PublicKey key, bool critical)
			: this(key, X509SubjectKeyIdentifierHashAlgorithm.Sha1, critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using a public key, a hash algorithm identifier, and a value indicating whether the extension is critical. </summary>
		/// <param name="key">A <see cref="T:System.Security.Cryptography.X509Certificates.PublicKey" /> object to create a subject key identifier (SKI) from.</param>
		/// <param name="algorithm">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierHashAlgorithm" /> values that identifies which hash algorithm to use.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x06000B73 RID: 2931 RVA: 0x00039AB8 File Offset: 0x00037CB8
		public X509SubjectKeyIdentifierExtension(PublicKey key, X509SubjectKeyIdentifierHashAlgorithm algorithm, bool critical)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			byte[] rawData = key.EncodedKeyValue.RawData;
			switch (algorithm)
			{
			case X509SubjectKeyIdentifierHashAlgorithm.Sha1:
				this._subjectKeyIdentifier = SHA1.Create().ComputeHash(rawData);
				break;
			case X509SubjectKeyIdentifierHashAlgorithm.ShortSha1:
			{
				Array array = SHA1.Create().ComputeHash(rawData);
				this._subjectKeyIdentifier = new byte[8];
				Buffer.BlockCopy(array, 12, this._subjectKeyIdentifier, 0, 8);
				this._subjectKeyIdentifier[0] = 64 | (this._subjectKeyIdentifier[0] & 15);
				break;
			}
			case X509SubjectKeyIdentifierHashAlgorithm.CapiSha1:
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(48);
				Mono.Security.ASN1 asn2 = asn.Add(new Mono.Security.ASN1(48));
				asn2.Add(new Mono.Security.ASN1(CryptoConfig.EncodeOID(key.Oid.Value)));
				asn2.Add(new Mono.Security.ASN1(key.EncodedParameters.RawData));
				byte[] array2 = new byte[rawData.Length + 1];
				Buffer.BlockCopy(rawData, 0, array2, 1, rawData.Length);
				asn.Add(new Mono.Security.ASN1(3, array2));
				this._subjectKeyIdentifier = SHA1.Create().ComputeHash(asn.GetBytes());
				break;
			}
			default:
				throw new ArgumentException("algorithm");
			}
			this._oid = new Oid("2.5.29.14", "Subject Key Identifier");
			base.Critical = critical;
			base.RawData = this.Encode();
		}

		/// <summary>Gets a string that represents the subject key identifier (SKI) for a certificate.</summary>
		/// <returns>A string, encoded in hexadecimal format, that represents the subject key identifier (SKI).</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The extension cannot be decoded. </exception>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00039C0C File Offset: 0x00037E0C
		public string SubjectKeyIdentifier
		{
			get
			{
				AsnDecodeStatus status = this._status;
				if (status == AsnDecodeStatus.Ok || status == AsnDecodeStatus.InformationNotAvailable)
				{
					if (this._subjectKeyIdentifier != null)
					{
						this._ski = Mono.Security.Cryptography.CryptoConvert.ToHex(this._subjectKeyIdentifier);
					}
					return this._ski;
				}
				throw new CryptographicException("Badly encoded extension.");
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class by copying information from encoded data.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to use to create the extension.</param>
		// Token: 0x06000B75 RID: 2933 RVA: 0x00039C54 File Offset: 0x00037E54
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
				this._oid = new Oid("2.5.29.14", "Subject Key Identifier");
			}
			else
			{
				this._oid = new Oid(x509Extension._oid);
			}
			base.RawData = x509Extension.RawData;
			base.Critical = x509Extension.Critical;
			this._status = this.Decode(base.RawData);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00039CE8 File Offset: 0x00037EE8
		internal static byte FromHexChar(char c)
		{
			if (c >= 'a' && c <= 'f')
			{
				return (byte)(c - 'a' + '\n');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (byte)(c - 'A' + '\n');
			}
			if (c >= '0' && c <= '9')
			{
				return (byte)(c - '0');
			}
			return byte.MaxValue;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00039D28 File Offset: 0x00037F28
		internal static byte FromHexChars(char c1, char c2)
		{
			byte b = X509SubjectKeyIdentifierExtension.FromHexChar(c1);
			if (b < 255)
			{
				b = (byte)(((int)b << 4) | (int)X509SubjectKeyIdentifierExtension.FromHexChar(c2));
			}
			return b;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00039D54 File Offset: 0x00037F54
		internal static byte[] FromHex(string hex)
		{
			if (hex == null)
			{
				return null;
			}
			int num = hex.Length >> 1;
			byte[] array = new byte[num];
			int i = 0;
			int num2 = 0;
			while (i < num)
			{
				array[i++] = X509SubjectKeyIdentifierExtension.FromHexChars(hex[num2++], hex[num2++]);
			}
			return array;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00039DA4 File Offset: 0x00037FA4
		internal AsnDecodeStatus Decode(byte[] extension)
		{
			if (extension == null || extension.Length == 0)
			{
				return AsnDecodeStatus.BadAsn;
			}
			this._ski = string.Empty;
			if (extension[0] != 4)
			{
				return AsnDecodeStatus.BadTag;
			}
			if (extension.Length == 2)
			{
				return AsnDecodeStatus.InformationNotAvailable;
			}
			if (extension.Length < 3)
			{
				return AsnDecodeStatus.BadLength;
			}
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(extension);
				this._subjectKeyIdentifier = asn.Value;
			}
			catch
			{
				return AsnDecodeStatus.BadAsn;
			}
			return AsnDecodeStatus.Ok;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00039E0C File Offset: 0x0003800C
		internal byte[] Encode()
		{
			return new Mono.Security.ASN1(4, this._subjectKeyIdentifier).GetBytes();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00039E20 File Offset: 0x00038020
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
				if (this._oid.Value != "2.5.29.14")
				{
					return string.Format("Unknown Key Usage ({0})", this._oid.Value);
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this._subjectKeyIdentifier.Length; i++)
				{
					stringBuilder.Append(this._subjectKeyIdentifier[i].ToString("x2"));
					if (i != this._subjectKeyIdentifier.Length - 1)
					{
						stringBuilder.Append(" ");
					}
				}
				if (multiLine)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x04000862 RID: 2146
		internal const string oid = "2.5.29.14";

		// Token: 0x04000863 RID: 2147
		internal const string friendlyName = "Subject Key Identifier";

		// Token: 0x04000864 RID: 2148
		private byte[] _subjectKeyIdentifier;

		// Token: 0x04000865 RID: 2149
		private string _ski;

		// Token: 0x04000866 RID: 2150
		private AsnDecodeStatus _status;
	}
}
