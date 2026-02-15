using System;
using Mono.Security;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a certificate's public key information. This class cannot be inherited.</summary>
	// Token: 0x020001B9 RID: 441
	public sealed class PublicKey
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.PublicKey" /> class using an object identifier (OID) object of the public key, an ASN.1-encoded representation of the public key parameters, and an ASN.1-encoded representation of the public key value. </summary>
		/// <param name="oid">An object identifier (OID) object that represents the public key.</param>
		/// <param name="parameters">An ASN.1-encoded representation of the public key parameters.</param>
		/// <param name="keyValue">An ASN.1-encoded representation of the public key value.</param>
		// Token: 0x06000A44 RID: 2628 RVA: 0x00034C4C File Offset: 0x00032E4C
		public PublicKey(Oid oid, AsnEncodedData parameters, AsnEncodedData keyValue)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (keyValue == null)
			{
				throw new ArgumentNullException("keyValue");
			}
			this._oid = new Oid(oid);
			this._params = new AsnEncodedData(parameters);
			this._keyValue = new AsnEncodedData(keyValue);
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key value.</summary>
		/// <returns>The ASN.1-encoded representation of the public key value.</returns>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00034CAD File Offset: 0x00032EAD
		public AsnEncodedData EncodedKeyValue
		{
			get
			{
				return this._keyValue;
			}
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key parameters.</summary>
		/// <returns>The ASN.1-encoded representation of the public key parameters.</returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00034CB5 File Offset: 0x00032EB5
		public AsnEncodedData EncodedParameters
		{
			get
			{
				return this._params;
			}
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> or <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object representing the public key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object representing the public key.</returns>
		/// <exception cref="T:System.NotSupportedException">The key algorithm is not supported.</exception>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00034CC0 File Offset: 0x00032EC0
		public AsymmetricAlgorithm Key
		{
			get
			{
				string value = this._oid.Value;
				if (value == "1.2.840.113549.1.1.1")
				{
					return PublicKey.DecodeRSA(this._keyValue.RawData);
				}
				if (!(value == "1.2.840.10040.4.1"))
				{
					throw new NotSupportedException(global::Locale.GetText("Cannot decode public key from unknown OID '{0}'.", new object[] { this._oid.Value }));
				}
				return PublicKey.DecodeDSA(this._keyValue.RawData, this._params.RawData);
			}
		}

		/// <summary>Gets an object identifier (OID) object of the public key.</summary>
		/// <returns>An object identifier (OID) object of the public key.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00034D45 File Offset: 0x00032F45
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00034D50 File Offset: 0x00032F50
		private static byte[] GetUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] != 0)
			{
				return integer;
			}
			int num = integer.Length - 1;
			byte[] array = new byte[num];
			Buffer.BlockCopy(integer, 1, array, 0, num);
			return array;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00034D7C File Offset: 0x00032F7C
		internal static DSA DecodeDSA(byte[] rawPublicKey, byte[] rawParameters)
		{
			DSAParameters dsaparameters = default(DSAParameters);
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(rawPublicKey);
				if (asn.Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Missing DSA Y integer."));
				}
				dsaparameters.Y = PublicKey.GetUnsignedBigInteger(asn.Value);
				Mono.Security.ASN1 asn2 = new Mono.Security.ASN1(rawParameters);
				if (asn2 == null || asn2.Tag != 48 || asn2.Count < 3)
				{
					throw new CryptographicException(global::Locale.GetText("Missing DSA parameters."));
				}
				if (asn2[0].Tag != 2 || asn2[1].Tag != 2 || asn2[2].Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Invalid DSA parameters."));
				}
				dsaparameters.P = PublicKey.GetUnsignedBigInteger(asn2[0].Value);
				dsaparameters.Q = PublicKey.GetUnsignedBigInteger(asn2[1].Value);
				dsaparameters.G = PublicKey.GetUnsignedBigInteger(asn2[2].Value);
			}
			catch (Exception ex)
			{
				throw new CryptographicException(global::Locale.GetText("Error decoding the ASN.1 structure."), ex);
			}
			DSACryptoServiceProvider dsacryptoServiceProvider = new DSACryptoServiceProvider(dsaparameters.Y.Length << 3);
			dsacryptoServiceProvider.ImportParameters(dsaparameters);
			return dsacryptoServiceProvider;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00034EB0 File Offset: 0x000330B0
		internal static RSA DecodeRSA(byte[] rawPublicKey)
		{
			RSAParameters rsaparameters = default(RSAParameters);
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(rawPublicKey);
				if (asn.Count == 0)
				{
					throw new CryptographicException(global::Locale.GetText("Missing RSA modulus and exponent."));
				}
				Mono.Security.ASN1 asn2 = asn[0];
				if (asn2 == null || asn2.Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Missing RSA modulus."));
				}
				Mono.Security.ASN1 asn3 = asn[1];
				if (asn3.Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Missing RSA public exponent."));
				}
				rsaparameters.Modulus = PublicKey.GetUnsignedBigInteger(asn2.Value);
				rsaparameters.Exponent = asn3.Value;
			}
			catch (Exception ex)
			{
				throw new CryptographicException(global::Locale.GetText("Error decoding the ASN.1 structure."), ex);
			}
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(rsaparameters.Modulus.Length << 3);
			rsacryptoServiceProvider.ImportParameters(rsaparameters);
			return rsacryptoServiceProvider;
		}

		// Token: 0x04000812 RID: 2066
		private AsnEncodedData _keyValue;

		// Token: 0x04000813 RID: 2067
		private AsnEncodedData _params;

		// Token: 0x04000814 RID: 2068
		private Oid _oid;

		// Token: 0x04000815 RID: 2069
		private static byte[] Empty = new byte[0];
	}
}
