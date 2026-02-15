using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Internal.Cryptography.Pal
{
	// Token: 0x020000D9 RID: 217
	internal struct CertificateData
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x0000E32C File Offset: 0x0000C52C
		internal CertificateData(byte[] rawData)
		{
			DerSequenceReader derSequenceReader = new DerSequenceReader(rawData);
			DerSequenceReader derSequenceReader2 = derSequenceReader.ReadSequence();
			if (derSequenceReader2.PeekTag() == 160)
			{
				DerSequenceReader derSequenceReader3 = derSequenceReader2.ReadSequence();
				this.Version = derSequenceReader3.ReadInteger();
			}
			else
			{
				if (derSequenceReader2.PeekTag() != 2)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				this.Version = 0;
			}
			if (this.Version < 0 || this.Version > 2)
			{
				throw new CryptographicException();
			}
			this.SerialNumber = derSequenceReader2.ReadIntegerBytes();
			DerSequenceReader derSequenceReader4 = derSequenceReader2.ReadSequence();
			this.TbsSignature.AlgorithmId = derSequenceReader4.ReadOidAsString();
			this.TbsSignature.Parameters = (derSequenceReader4.HasData ? derSequenceReader4.ReadNextEncodedValue() : Array.Empty<byte>());
			if (derSequenceReader4.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			this.Issuer = new X500DistinguishedName(derSequenceReader2.ReadNextEncodedValue());
			DerSequenceReader derSequenceReader5 = derSequenceReader2.ReadSequence();
			this.NotBefore = derSequenceReader5.ReadX509Date();
			this.NotAfter = derSequenceReader5.ReadX509Date();
			if (derSequenceReader5.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			this.Subject = new X500DistinguishedName(derSequenceReader2.ReadNextEncodedValue());
			this.SubjectPublicKeyInfo = derSequenceReader2.ReadNextEncodedValue();
			DerSequenceReader derSequenceReader6 = new DerSequenceReader(this.SubjectPublicKeyInfo);
			DerSequenceReader derSequenceReader7 = derSequenceReader6.ReadSequence();
			this.PublicKeyAlgorithm.AlgorithmId = derSequenceReader7.ReadOidAsString();
			this.PublicKeyAlgorithm.Parameters = (derSequenceReader7.HasData ? derSequenceReader7.ReadNextEncodedValue() : Array.Empty<byte>());
			if (derSequenceReader7.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			this.PublicKey = derSequenceReader6.ReadBitString();
			if (derSequenceReader6.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (this.Version > 0 && derSequenceReader2.HasData && derSequenceReader2.PeekTag() == 161)
			{
				this.IssuerUniqueId = derSequenceReader2.ReadBitString();
			}
			else
			{
				this.IssuerUniqueId = null;
			}
			if (this.Version > 0 && derSequenceReader2.HasData && derSequenceReader2.PeekTag() == 162)
			{
				this.SubjectUniqueId = derSequenceReader2.ReadBitString();
			}
			else
			{
				this.SubjectUniqueId = null;
			}
			this.Extensions = new List<X509Extension>();
			if (this.Version > 1 && derSequenceReader2.HasData && derSequenceReader2.PeekTag() == 163)
			{
				DerSequenceReader derSequenceReader8 = derSequenceReader2.ReadSequence();
				derSequenceReader8 = derSequenceReader8.ReadSequence();
				while (derSequenceReader8.HasData)
				{
					DerSequenceReader derSequenceReader9 = derSequenceReader8.ReadSequence();
					string text = derSequenceReader9.ReadOidAsString();
					bool flag = false;
					if (derSequenceReader9.PeekTag() == 1)
					{
						flag = derSequenceReader9.ReadBoolean();
					}
					byte[] array = derSequenceReader9.ReadOctetString();
					this.Extensions.Add(new X509Extension(text, array, flag));
					if (derSequenceReader9.HasData)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
				}
			}
			if (derSequenceReader2.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			DerSequenceReader derSequenceReader10 = derSequenceReader.ReadSequence();
			this.SignatureAlgorithm.AlgorithmId = derSequenceReader10.ReadOidAsString();
			this.SignatureAlgorithm.Parameters = (derSequenceReader10.HasData ? derSequenceReader10.ReadNextEncodedValue() : Array.Empty<byte>());
			if (derSequenceReader10.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			this.SignatureValue = derSequenceReader.ReadBitString();
			if (derSequenceReader.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			this.RawData = rawData;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000E66C File Offset: 0x0000C86C
		public string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			if (nameType == X509NameType.SimpleName)
			{
				string simpleNameInfo = CertificateData.GetSimpleNameInfo(forIssuer ? this.Issuer : this.Subject);
				if (simpleNameInfo != null)
				{
					return simpleNameInfo;
				}
			}
			string text = (forIssuer ? "2.5.29.18" : "2.5.29.17");
			GeneralNameType? generalNameType = null;
			string text2 = null;
			switch (nameType)
			{
			case X509NameType.SimpleName:
			case X509NameType.EmailName:
				generalNameType = new GeneralNameType?(GeneralNameType.Rfc822Name);
				break;
			case X509NameType.UpnName:
				generalNameType = new GeneralNameType?(GeneralNameType.OtherName);
				text2 = "1.3.6.1.4.1.311.20.2.3";
				break;
			case X509NameType.DnsName:
			case X509NameType.DnsFromAlternativeName:
				generalNameType = new GeneralNameType?(GeneralNameType.DnsName);
				break;
			case X509NameType.UrlName:
				generalNameType = new GeneralNameType?(GeneralNameType.UniformResourceIdentifier);
				break;
			}
			if (generalNameType != null)
			{
				foreach (X509Extension x509Extension in this.Extensions)
				{
					if (x509Extension.Oid.Value == text)
					{
						string text3 = CertificateData.FindAltNameMatch(x509Extension.RawData, generalNameType.Value, text2);
						if (text3 != null)
						{
							return text3;
						}
					}
				}
			}
			string text4 = null;
			if (nameType != X509NameType.EmailName)
			{
				if (nameType == X509NameType.DnsName)
				{
					text4 = "2.5.4.3";
				}
			}
			else
			{
				text4 = "1.2.840.113549.1.9.1";
			}
			if (text4 != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in CertificateData.ReadReverseRdns(forIssuer ? this.Issuer : this.Subject))
				{
					if (keyValuePair.Key == text4)
					{
						return keyValuePair.Value;
					}
				}
			}
			return "";
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000E810 File Offset: 0x0000CA10
		private static string GetSimpleNameInfo(X500DistinguishedName name)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			foreach (KeyValuePair<string, string> keyValuePair in CertificateData.ReadReverseRdns(name))
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (key == "2.5.4.3")
				{
					return value;
				}
				if (!(key == "2.5.4.11"))
				{
					if (!(key == "2.5.4.10"))
					{
						if (!(key == "1.2.840.113549.1.9.1"))
						{
							if (text4 == null)
							{
								text4 = value;
							}
						}
						else
						{
							text3 = value;
						}
					}
					else
					{
						text2 = value;
					}
				}
				else
				{
					text = value;
				}
			}
			string text5;
			if ((text5 = text) == null && (text5 = text2) == null)
			{
				text5 = text3 ?? text4;
			}
			return text5;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		private static string FindAltNameMatch(byte[] extensionBytes, GeneralNameType matchType, string otherOid)
		{
			byte b = 128 | (byte)matchType;
			if (matchType == GeneralNameType.OtherName)
			{
				b |= 32;
			}
			DerSequenceReader derSequenceReader = new DerSequenceReader(extensionBytes);
			while (derSequenceReader.HasData)
			{
				if (derSequenceReader.PeekTag() != b)
				{
					derSequenceReader.SkipValue();
				}
				else
				{
					switch (matchType)
					{
					case GeneralNameType.OtherName:
					{
						DerSequenceReader derSequenceReader2 = derSequenceReader.ReadSequence();
						if (!(derSequenceReader2.ReadOidAsString() == otherOid))
						{
							continue;
						}
						if (derSequenceReader2.PeekTag() != 160)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
						derSequenceReader2 = derSequenceReader2.ReadSequence();
						return derSequenceReader2.ReadUtf8String();
					}
					case GeneralNameType.Rfc822Name:
					case GeneralNameType.DnsName:
					case GeneralNameType.UniformResourceIdentifier:
						return derSequenceReader.ReadIA5String();
					}
					derSequenceReader.SkipValue();
				}
			}
			return null;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000E992 File Offset: 0x0000CB92
		private static IEnumerable<KeyValuePair<string, string>> ReadReverseRdns(X500DistinguishedName name)
		{
			DerSequenceReader derSequenceReader = new DerSequenceReader(name.RawData);
			Stack<DerSequenceReader> rdnReaders = new Stack<DerSequenceReader>();
			while (derSequenceReader.HasData)
			{
				rdnReaders.Push(derSequenceReader.ReadSet());
			}
			while (rdnReaders.Count > 0)
			{
				DerSequenceReader rdnReader = rdnReaders.Pop();
				while (rdnReader.HasData)
				{
					DerSequenceReader derSequenceReader2 = rdnReader.ReadSequence();
					string text = derSequenceReader2.ReadOidAsString();
					DerSequenceReader.DerTag derTag = (DerSequenceReader.DerTag)derSequenceReader2.PeekTag();
					string text2 = null;
					if (derTag != DerSequenceReader.DerTag.UTF8String)
					{
						switch (derTag)
						{
						case DerSequenceReader.DerTag.PrintableString:
							text2 = derSequenceReader2.ReadPrintableString();
							break;
						case DerSequenceReader.DerTag.T61String:
							text2 = derSequenceReader2.ReadT61String();
							break;
						case (DerSequenceReader.DerTag)21:
							break;
						case DerSequenceReader.DerTag.IA5String:
							text2 = derSequenceReader2.ReadIA5String();
							break;
						default:
							if (derTag == DerSequenceReader.DerTag.BMPString)
							{
								text2 = derSequenceReader2.ReadBMPString();
							}
							break;
						}
					}
					else
					{
						text2 = derSequenceReader2.ReadUtf8String();
					}
					if (text2 != null)
					{
						yield return new KeyValuePair<string, string>(text, text2);
					}
				}
				rdnReader = null;
			}
			yield break;
		}

		// Token: 0x0400034B RID: 843
		internal byte[] RawData;

		// Token: 0x0400034C RID: 844
		internal byte[] SubjectPublicKeyInfo;

		// Token: 0x0400034D RID: 845
		internal int Version;

		// Token: 0x0400034E RID: 846
		internal byte[] SerialNumber;

		// Token: 0x0400034F RID: 847
		internal CertificateData.AlgorithmIdentifier TbsSignature;

		// Token: 0x04000350 RID: 848
		internal X500DistinguishedName Issuer;

		// Token: 0x04000351 RID: 849
		internal DateTime NotBefore;

		// Token: 0x04000352 RID: 850
		internal DateTime NotAfter;

		// Token: 0x04000353 RID: 851
		internal X500DistinguishedName Subject;

		// Token: 0x04000354 RID: 852
		internal CertificateData.AlgorithmIdentifier PublicKeyAlgorithm;

		// Token: 0x04000355 RID: 853
		internal byte[] PublicKey;

		// Token: 0x04000356 RID: 854
		internal byte[] IssuerUniqueId;

		// Token: 0x04000357 RID: 855
		internal byte[] SubjectUniqueId;

		// Token: 0x04000358 RID: 856
		internal List<X509Extension> Extensions;

		// Token: 0x04000359 RID: 857
		internal CertificateData.AlgorithmIdentifier SignatureAlgorithm;

		// Token: 0x0400035A RID: 858
		internal byte[] SignatureValue;

		// Token: 0x020000DA RID: 218
		internal struct AlgorithmIdentifier
		{
			// Token: 0x0400035B RID: 859
			internal string AlgorithmId;

			// Token: 0x0400035C RID: 860
			internal byte[] Parameters;
		}
	}
}
