using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Mono.Security;
using Mono.Security.Authenticode;

namespace Mono
{
	// Token: 0x02000013 RID: 19
	internal abstract class X509PalImpl
	{
		// Token: 0x06000048 RID: 72
		public abstract X509CertificateImpl Import(byte[] data);

		// Token: 0x06000049 RID: 73
		public abstract X509Certificate2Impl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags);

		// Token: 0x0600004A RID: 74
		public abstract X509Certificate2Impl Import(X509Certificate cert);

		// Token: 0x0600004B RID: 75 RVA: 0x000027C4 File Offset: 0x000009C4
		private static byte[] PEM(string type, byte[] data)
		{
			string @string = Encoding.ASCII.GetString(data);
			string text = string.Format("-----BEGIN {0}-----", type);
			string text2 = string.Format("-----END {0}-----", type);
			int num = @string.IndexOf(text) + text.Length;
			int num2 = @string.IndexOf(text2, num);
			return Convert.FromBase64String(@string.Substring(num, num2 - num));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000281C File Offset: 0x00000A1C
		protected static byte[] ConvertData(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return data;
			}
			if (data[0] != 48)
			{
				try
				{
					return X509PalImpl.PEM("CERTIFICATE", data);
				}
				catch
				{
				}
				return data;
			}
			return data;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002860 File Offset: 0x00000A60
		internal X509Certificate2Impl ImportFallback(byte[] data)
		{
			data = X509PalImpl.ConvertData(data);
			X509Certificate2Impl x509Certificate2Impl;
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(null))
			{
				x509Certificate2Impl = new X509Certificate2ImplMono(data, safePasswordHandle, X509KeyStorageFlags.DefaultKeySet);
			}
			return x509Certificate2Impl;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028A4 File Offset: 0x00000AA4
		internal X509Certificate2Impl ImportFallback(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return new X509Certificate2ImplMono(data, password, keyStorageFlags);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool SupportsLegacyBasicConstraintsExtension
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000028B4 File Offset: 0x00000AB4
		public X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("rawData");
			}
			if (rawData[0] == 48)
			{
				try
				{
					Mono.Security.ASN1 asn = new Mono.Security.ASN1(rawData);
					if (asn.Count == 3 && asn[0].Tag == 48 && asn[1].Tag == 48 && asn[2].Tag == 3)
					{
						return X509ContentType.Cert;
					}
					if (asn.Count == 3 && asn[0].Tag == 2 && asn[1].Tag == 48 && asn[2].Tag == 48)
					{
						return X509ContentType.Pfx;
					}
					if (asn.Count > 0 && asn[0].Tag == 6 && asn[0].CompareValue(X509PalImpl.signedData))
					{
						return X509ContentType.Pkcs7;
					}
					return X509ContentType.Unknown;
				}
				catch (Exception)
				{
					return X509ContentType.Unknown;
				}
			}
			if (Encoding.ASCII.GetString(rawData).IndexOf("-----BEGIN CERTIFICATE-----") >= 0)
			{
				return X509ContentType.Cert;
			}
			X509ContentType x509ContentType;
			try
			{
				new AuthenticodeDeformatter(rawData);
				x509ContentType = X509ContentType.Authenticode;
			}
			catch
			{
				x509ContentType = X509ContentType.Unknown;
			}
			return x509ContentType;
		}

		// Token: 0x04000023 RID: 35
		private static byte[] signedData = new byte[] { 42, 134, 72, 134, 247, 13, 1, 7, 2 };
	}
}
